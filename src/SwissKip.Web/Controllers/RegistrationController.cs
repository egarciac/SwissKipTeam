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
using System.Collections.Generic;

namespace SwissKip.Web.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signin1()
        {
            //var user = Current.User;
            //var model = Mapper.Map<AccountEditModel>(user);
            ViewBag.CountryId = Current.Connection.GetList<Country>();
            return View();
        }

        [HttpPost]
        public ActionResult Signin1(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Session["Signin1Store"] = model;
                return RedirectToAction("Signin2", "Registration");
            }
            return this.View();
        }

        public ActionResult Signin2()
        {
            Session["Signin1Store"] = Session["Signin1Store"];

            return View();
        }

        [HttpPost]
        public ActionResult Signin2(UserCreateModel model)
        {
            //if (ModelState.IsValid)
            //{
                Session["Signin2Store"] = model;
                return RedirectToAction("Signin3", "Registration");
            //}
            //return this.View();
        }

        public ActionResult Signin3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin3(List<UserCreateModel> witnesses)
        {
            //if (ModelState.IsValid)
            //{
                Session["Signin3Store"] = witnesses;
                return RedirectToAction("Signin4", "Registration");
            //}
            //return this.View();
        }

        public ActionResult Signin4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin4(MessageModel form)
        {
            //if (ModelState.IsValid)
            //{

            //storing information about new user
            UserCreateModel usersData;
            UserCreateModel dataheirData;
            List<UserCreateModel> witnessData;
            MessageModel messages;
            usersData = (UserCreateModel)Session["Signin1Store"];
            dataheirData = (UserCreateModel)Session["Signin2Store"];
            witnessData = (List<UserCreateModel>)Session["Signin3Store"];
            messages = form;

            return RedirectToAction("Signin5", "Registration");
            //}
            //return this.View();
        }


        public ActionResult Signin5()
        {
            return View();
        }
    }
}
