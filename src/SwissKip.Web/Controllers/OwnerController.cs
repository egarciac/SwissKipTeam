﻿using System.Web.Mvc;

namespace SwissKip.Web.Controllers
{
    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Helpers;
    using SwissKip.Web.Helpers.ActionResults;
    using SwissKip.Web.Models;
    using SwissKip.Web.Queries;
    using SwissKip.Web.Security;
    using System.Web.UI;
    using System.Linq;
    using System.Collections.Generic;
    
    public class OwnerController : Controller
    {
        public ActionResult Index()
        {
            var ownerPanelModel = new OwnerPanelQuery(Current.UserId).Execute();
            Session["username"] = Current.User.UserName;
            return View(ownerPanelModel);
        }

        public ActionResult EditWitness()
        {
            var user = AuthenticationService.GetUser();
            var witness = new OwnersByWitnessQuery(user.Id).ExecuteNew();
            return View(witness);
        }

        public ActionResult AddRecipient()
        {
            var user = AuthenticationService.GetUser();
            var ownerRecipient = new OwnersByRecipientQuery(user.Id).ExecuteNew();
            //var ownerRecipient = Current.Connection.Get<User>(Current.UserId);
            //if (ownerRecipient != null)
            //{
            //    var recipient = Current.Connection.Get<User>(ownerRecipient.Id);
            //    var model = Mapper.Map<RecipientAddModel>(recipient);
            //    return View(model);
            //}
            return this.View(ownerRecipient);

            //var recipient = new List<RecipientAddModel>
            //    {
            //        new RecipientAddModel()
            //    };
            //return View(recipient);
        }

        [HttpPost]
        public ActionResult AddRecipient(RecipientAddModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new RecipientAddHandler().Handle(Current.User,model);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            }

            if (!ModelState.IsValid)
                return this.View();

            return RedirectToAction("Index", "MyAccount");
        }

        [HttpGet] 
        public ActionResult Delete(int? id)
        {
            var idremoved = id;
            return RedirectToAction("Index", "MyAccount");
        }

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
                //new WitnessesAddHandler().Handle(Current.User, RemoveEmptyItems(witnesses));
                return RedirectToAction("EditWitness");
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

        public ActionResult ChangePassword()
        {
            var user = Current.User;
            var model = Mapper.Map<AccountEditModel>(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(AccountEditModel form)
        {   
            try
            {
                new AccountEditHandler().Handle2(Current.UserId, form);
                    
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(e.Key, e.Message);
            }
            

            if (!ModelState.IsValid)
                return this.View();

            return RedirectToAction("Index", "MyAccount");
        }

        //[HttpPost]
        //public ActionResult Delete(WitnessAddModel model)
        //{
        //    var id = model.id; 
        //    //if (ModelState.IsValid)
        //    //{
        //    //    try
        //    //    {
        //    //        new RecipientAddHandler().Handle(Current.User, model);
        //    //    }
        //    //    catch (ValidationException e)
        //    //    {
        //    //        ModelState.AddModelError(e.Key, e.Message);
        //    //    }
        //    //}

        //    //if (!ModelState.IsValid)
        //    //    return this.View();

        //    return RedirectToAction("Index", "MyAccount");
        //}
    }
}



