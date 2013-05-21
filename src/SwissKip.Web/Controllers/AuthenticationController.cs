using System.Web.Mvc;

namespace SwissKip.Web.Controllers
{
    using System.Configuration;
    using System.Web.Security;

    using Microsoft.Web.Helpers;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Helpers.ActionResults;
    using SwissKip.Web.Models;
    using SwissKip.Web.Security;

    //TODO: Si ya está logeado ingresar directamente
    public class AuthenticationController : Controller
    {
        [AllowAnonymous]
        public ActionResult SignIn(int? V)
        {
            try
            {
                var user = AuthenticationService.GetUser();
                User user1 = new SignInHandler().Find3(user.Id);

                if (user1.Banned)
                {
                    if (user1.ModifiedDate <= System.DateTime.Now)
                        return RedirectToAction("BannedAccount");
                }
            }
            catch { 
            
            }
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignInTimeOut()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Maintained(string aspxerrorpath)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MailVerification()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(string V, string type, SignInModel model, string returnUrl)
        {
            User user = null;
            if (ModelState.IsValid)
            {
                try
                {
                    user = new SignInHandler().Handle(model);
                    if (user.Banned)
                        return RedirectToAction("BannedAccount");
                    //if (user.Blocked)
                    //    return RedirectToAction("BlockedAccount");      

                    if (type != null)
                    {
                        Session["AccessDenied"] = 1;
                        return RedirectToAction("Signin2", "Registration", new { userId = V, type = type });
                    }
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Forgot username or passoword");
                return this.View();
            }

            if (!ModelState.IsValid)
                return this.View();

            AuthenticationService.SignIn(user);
            Session["path"] = Server.MapPath("~/Swisskip/") + user.UserName;

            //Sent TOKEN by email
            int newValue = SignInHandler.SendInvitation(user);

            //Added Token into account
            UsersAddHandler usersAddHandler = new UsersAddHandler();
            user.TokenNumber = newValue;
            usersAddHandler.Update(user);

            //Validating data entry

            if (user.ColourId == 0 && user.IsOwner == false)
            {
                return RedirectToAction("Reminder");
            }
            else if (user.ColourId == 0 && user.IsOwner)
            {
                return RedirectToAction("MissingInfo", new { id = user.Id });
            }

            Session["AccessDenied"] = 1;
            return RedirectToAction("Confirm");
        }
        
        public ActionResult Confirm()
        {
            var user = AuthenticationService.GetUser();
            User user1 = new SignInHandler().Find3(user.Id);

            if (user1.Banned)
            {
                if (user1.ModifiedDate <= System.DateTime.Now)
                    return RedirectToAction("BannedAccount");
            }

            Master master = new SignInHandler().Find4();
            var Token = new SwissKip.Web.Queries.TokenQuery(user.Id).ExecuteNew();
            //if (Token.Count != 0)
            //{
            Token[0].TokenSeconds = master.TokenExpirationTime;
            Session["Trying"] = Token[0];
            Session["AccessDenied"] = 1;
            return View(Token[0]);
            //}
            //else
            //{
            //    string message = "You have not completed your personal info yet!";
            //    ModelState.AddModelError("Error", message);
            //    return this.View();
            //}
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Confirm(int? userId, BasicInfoModel form)
        {
            BasicInfoModel data = (BasicInfoModel)Session["Trying"];
            User user = null;
            if (ModelState.IsValid)
            {
                try
                {
                    user = new SignInHandler().Handle3(form, data);
                    if (user.Banned)
                        return RedirectToAction("BannedAccount");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                Session["Trying"] = data;
                Session["AccessDenied"] = 1;
                return this.View(data);
            }

            Session["AccessDenied"] = 0;
            if (!user.IsOwner)
                return new RedirectToAccountType(user);
            
            return RedirectToAction("Index", "Owner");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Maintained(int? userId, SignInModel model, string returnUrl)
        {
            User user = null;
            if (ModelState.IsValid)
            {
                try
                {
                    user = new SignInHandler().Handle(model);
                    if (user.Banned)
                        return RedirectToAction("BannedAccount");
                    //if (user.Blocked)
                    //    return RedirectToAction("BlockedAccount");        
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Forgot username or passoword");
                return this.View();
            }

            if (!ModelState.IsValid)
                return this.View();

            AuthenticationService.SignIn(user);
            Session["path"] = Server.MapPath("~/Swisskip/") + user.UserName;

            //Sent TOKEN by email
            int newValue = SignInHandler.SendInvitation(user);

            //Added Token into account
            UsersAddHandler usersAddHandler = new UsersAddHandler();
            user.TokenNumber = newValue;
            usersAddHandler.Update(user);

            return RedirectToAction("Confirm");
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            Response.Cookies.Add(new System.Web.HttpCookie("ASP.NET_SessionId", ""));
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        public ActionResult BannedAccount()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Reminder()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MissingInfo(int id)
        {
            //Handled User ID
            User user = new SignInHandler().Find3(id);
            Session["user"] = user;
            return View();
        }

        [HttpPost]
        public ActionResult MissingInfo()
        {
            Session["user"] = Session["user"];
            return RedirectToAction("SecretInfo", "NewAccount");
        }

        [AllowAnonymous]
        public ActionResult BlockedAccount()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(SignInModel model, string returnUrl)
        {
            User user = null;
            
            try
            {
                user = new SignInHandler().Handle2(model);
                return RedirectToAction("SignIn");
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(e.Key, e.Message);
                return this.View();
            }
            
        }

    }
}
