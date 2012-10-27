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
        public ActionResult SignIn()
        {
            //Session["username"] = Current.User.UserName;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(SignInModel model, string returnUrl)
        {
            //TODO: ¿Si aún no se confirmó el email?
            if (!ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptchaPrivateKey"]))
            {
                ModelState.AddModelError("Catpcha", "The verification words are incorrect.");
            }

            Account account = null;
            if (ModelState.IsValid)
            {
                try
                {
                    account=new SignInHandler().Handle(model);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key,e.Message);
                }
            }

            if (!ModelState.IsValid)
                return this.View();

            AuthenticationService.SignIn(account);
            Session["username"]= account.UserName;
            Session["path"] = "~/Swisskip/" + account.UserName;

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return new RedirectToAccountType(account);
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }
    }
}
