﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//using System.Configuration;
//using DapperExtensions;
//using Microsoft.Web.Helpers;
//using SwissKip.Web.Helpers.ActionResults;
using SwissKip.Web.Queries;

//using DapperExtensions;

using System.Web.Mvc;

using DapperExtensions;

using SwissKip.Web.Core;
using SwissKip.Web.Core.Exceptions;
using SwissKip.Web.Handlers;
using SwissKip.Web.Helpers;
using SwissKip.Web.Helpers.Extensions;
using SwissKip.Web.Models;
using System.Web;
using SwissKip.Web.Security;


namespace SwissKip.Web.Controllers
{
    public class BankAccountController : Controller
    {
        public ActionResult Index()
        {
            var user = AuthenticationService.GetUser();
            var bankAccounts = new BankAccountQuery(user.Id).Execute();
            ViewData["country_list"] = Current.Connection.GetList<Country>().ToSelectList();
            return View(bankAccounts);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            //ViewBag.CountryId = Current.Connection.GetList<Country>().ToSelectList();
            ViewData["country_list"] = Current.Connection.GetList<Country>().ToSelectList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(BankAccountCreateModel form)
        {
            //if (!ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptchaPrivateKey"]))
            //{
            //    ModelState.AddModelError("Catpcha", "The verification words are incorrect.");
            //}

            //if (ModelState.IsValid)
            //{
                try
                {
                    form.UserId = AuthenticationService.GetUser().Id;
                    //form.BankAccountNumber =
                    //    form.texto1 + '*' + form.texto3 + '*' +
                    //    form.texto5 + '*' + form.texto7 + '*' + form.texto9;

                    //form.BankAccountNumber1 =
                    //    form.texto2 + '*' + form.texto4 + '*' +
                    //    form.texto6 + '*' + form.texto8 + '*' + form.texto0;

                    //form.Password =
                    //    form.clave1 + '*' + form.clave3 + '*' + form.clave5;

                    //form.Password1 =
                    //    form.clave2 + '*' + form.clave4 + '*' + form.clave6;

                    BankAccount bankaccount = null;

                    bankaccount = new BankAccountCreateHandler().Handle(form);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            //}

            //if (!ModelState.IsValid)
            //    return this.View();

            return RedirectToAction("Index");
        }

    }
}
