﻿namespace SwissKip.Web.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using DapperExtensions;

    using Microsoft.Web.Helpers;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Helpers;
    using SwissKip.Web.Helpers.ActionResults;
    using SwissKip.Web.Models;
    using SwissKip.Web.Security;
    using System.IO;
    using System.Web;
    using System.Diagnostics;
    using System;

    public class NewAccountController : Controller
    {
        //TODO: Si ya están logeados ingresar automáticamente
        [AllowAnonymous]
        public ActionResult Create(int? invitationId)
        {
            if (invitationId.HasValue)
            {
                //TODO: ¿Si no existe la cuenta?
                var invitation = Current.Connection.Get<Account>(invitationId.Value);
                var model = Mapper.Map<AccountCreateModel>(invitation, new[] { "UserName", "Password" });
                return View(model);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(int? invitationId, AccountCreateModel form)
        {
            if (!ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptchaPrivateKey"]))
            {
                ModelState.AddModelError("Catpcha", "The verification words are incorrect.");
            }

            Account account = null;
            if (ModelState.IsValid)
            {
                try
                {
                    account = new AccountCreateHandler().Handle(invitationId, form);
                    var path = "~/Swisskip/" + form.Username;
                    Directory.CreateDirectory(Server.MapPath(path));
                    Session["path"] = path;
                    Session["username"] = form.Username;

                    //Creating a default photo
                    string newFile = Server.MapPath("~/Content/images/") + form.Username + ".jpg";
                    if (!System.IO.File.Exists(newFile))
                        System.IO.File.Copy(Server.MapPath("~/Content/images/unknown.jpg"), Server.MapPath("~/Content/images/") + form.Username + ".jpg");
                    
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }

            if (!ModelState.IsValid)
                return this.View();

            if (invitationId.HasValue)
            {
                AuthenticationService.SignIn(account);
                return new RedirectToAccountType(account);
            }
            return RedirectToAction("ConfirmYourEmailAddress");
        }

        [AllowAnonymous]
        public ActionResult ConfirmYourEmailAddress()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Confirm(int accountId)
        {
            //TODO: ¿Si el usuario no existe? ¿Si el usuario ya confirmó su cuenta?
            new AccountConfirmHandler().Handle(accountId);
            return this.RedirectToAction("AddWitnesses");
        }

        //Solo permitir el ingreso si son owners
        public ActionResult AddWitnesses()
        {
            ViewBag.Email = AuthenticationService.GetUser().Email;
            var witnesses = new List<WitnessAddModel>
                {
                    new WitnessAddModel()
                };
            return View(witnesses);
        }

        public ActionResult BlanckWitness()
        {
            return this.PartialView("_Witness");
        }

        [HttpPost]
        public ActionResult AddWitnesses(List<WitnessAddModel> witnesses)
        {
            if (ModelState.IsValid)
            {
                new WitnessesAddHandler().Handle(Current.User, RemoveEmptyItems(witnesses));
                return RedirectToAction("Index", "Owner");
            }
            return this.View(witnesses);
        }

        private List<WitnessAddModel> RemoveEmptyItems(List<WitnessAddModel> witnesses)
        {
            return witnesses.Where(witness => !this.AllPropertiesEmpty(witness)).ToList();
        }

        private bool AllPropertiesEmpty(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj, null) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
