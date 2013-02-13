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
        public ActionResult SignIn(int? userId)
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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(int? userId, SignInModel model, string returnUrl)
        {
            User user = null;
            if (ModelState.IsValid)
            {
                try
                {
                    user=new SignInHandler().Handle(model);
                    if (user.Banned)
                        return RedirectToAction("BannedAccount");
                    //if (user.Blocked)
                    //    return RedirectToAction("BlockedAccount");        
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key,e.Message);
                }
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
            Token[0].TokenSeconds = master.TokenExpirationTime;
            Session["Trying"] = Token[0];
            return View(Token[0]);
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
                return this.View(data);
            }

            if (!user.IsOwner)
                return new RedirectToAccountType(user);   

            return RedirectToAction("Index", "Owner");
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        public ActionResult BannedAccount()
        {
            return View();
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
