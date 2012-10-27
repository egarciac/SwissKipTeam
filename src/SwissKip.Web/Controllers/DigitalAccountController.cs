namespace SwissKip.Web.Controllers
{
    
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using DapperExtensions;

    using Microsoft.Web.Helpers;

    using SwissKip.Web.Helpers.ActionResults;
    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Helpers;
    using SwissKip.Web.Models;
    using SwissKip.Web.Security;
    using SwissKip.Web.Queries;

    public class DigitalAccountController : Controller
    {
        public ActionResult Index()
        {
            var user = AuthenticationService.GetUser();
            var digitalAccounts = new DigitalAccountQuery(user.Id).Execute();
            return View(digitalAccounts);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(DigitalAccountCreateModel form)
        {
            if (!ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptchaPrivateKey"]))
            {
                ModelState.AddModelError("Catpcha", "The verification words are incorrect.");
            }

            form.AccountId = AuthenticationService.GetUser().Id;
            form.Password = Change(form.Password);
            DigitalAccount digitalaccount = null;
            if (ModelState.IsValid)
            {
                try
                {
                    digitalaccount = new DigitalAccountCreateHandler().Handle(form);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }

            if (!ModelState.IsValid)
                return this.View();

            return RedirectToAction("Index");
        }

        private string Change(string p)
        {
            string newValue="";
            
            for (int i = 0; i < p.Length; i++) {
                newValue += p.Substring(i, 1) + '*';
                i++;
            }

            return newValue; 
        }
    }
}
