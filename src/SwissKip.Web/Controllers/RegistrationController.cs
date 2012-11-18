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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Signin1()
        {
            //var user = Current.User;
            //var model = Mapper.Map<AccountEditModel>(user);
            ViewBag.CountryId = Current.Connection.GetList<Country>();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin1(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Session["Signin1Store"] = model;
                return RedirectToAction("Signin2", "Registration");
            }
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult Signin2()
        {
            Session["Signin1Store"] = Session["Signin1Store"];

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin2(UserCreateModel model)
        {
            //if (ModelState.IsValid)
            //{
                Session["Signin2Store"] = model;
                return RedirectToAction("Signin3", "Registration");
            //}
            //return this.View();
        }

        [AllowAnonymous]
        public ActionResult Signin3()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin3(List<UserCreateModel> witnesses)
        {
            //if (ModelState.IsValid)
            //{
                Session["Signin3Store"] = witnesses;
                return RedirectToAction("Signin4", "Registration");
            //}
            //return this.View();
        }

        [AllowAnonymous]
        public ActionResult Signin4()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin4(MessageModel form)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    //storing information about new user
                    UserCreateModel usersData = (UserCreateModel)Session["Signin1Store"];
                    UserCreateHandler userCreateHandler = new UserCreateHandler();

                    if (userCreateHandler.ExistsUserWithSameEmail(usersData.Email))
                        throw new ValidationException("Email", "Owner's Email already exists");

                    if (userCreateHandler.ExistsUserWithSameUserName(usersData.UserName))
                        throw new ValidationException("UserName", "Owner's UserName already exists");

                    var user = Core.User.CreateOwner(usersData.FirstName, usersData.LastName, usersData.UserName, usersData.Password, usersData.Email, usersData.CountryId, System.DateTime.Now, 0, true, false, false);
                    UsersAddHandler.Save(user);
                    userCreateHandler.Handle(user);

                    //Add path for new photo
                    var path = "~/Swisskip/" + user.UserName;
                    if (!System.IO.Directory.Exists(Server.MapPath(path)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(path));
                    
                    Session["path"] = path;
                    //Session["username"] = user.UserName;

                    //Creating a default photo
                    string newFile = Server.MapPath("~/Content/images/") + user.UserName + ".jpg";
                    if (!System.IO.File.Exists(newFile))
                        System.IO.File.Copy(Server.MapPath("~/Content/images/unknown.jpg"), Server.MapPath("~/Content/images/") + user.UserName + ".jpg");

                    AuthenticationService.SignIn(user);

                    //Link new owner with Trial Mode
                    var user_userType1 = User_UserType.CreateRelationUserAndUserType(0, user.Id, (int)UserRoles.Owner, 1);
                    UsersAddHandler.Save(user_userType1);
                    
                    UserCreateModel dataheirData = (UserCreateModel)Session["Signin2Store"];
                    DataheirAddHandler dataheirAddHandler = new DataheirAddHandler();

                    if (dataheirAddHandler.ExistsUserWithSameEmail(dataheirData.Email))
                        throw new ValidationException("Email", "Dataheir's Email already exists");

                    if (dataheirAddHandler.ExistsUserWithSameUserName(dataheirData.UserName))
                        throw new ValidationException("UserName", "Dataheir's UserName already exists");

                    var dataheir = Core.User.CreateDataheir(dataheirData.FirstName, dataheirData.LastName, dataheirData.UserName, dataheirData.Password, dataheirData.Email, dataheirData.CountryId, System.DateTime.Now, 0, false, true, false);
                    UsersAddHandler.Save(dataheir);
                    dataheirAddHandler.Handle(user, dataheir);

                    //Link new owner with Trial Mode
                    var user_userType2 = User_UserType.CreateRelationUserAndUserType(user.Id, dataheir.Id, (int)UserRoles.Dataheir, 1);
                    UsersAddHandler.Save(user_userType2);

                    //Create Witness
                    var witnessData = (List<UserCreateModel>)Session["Signin3Store"];
                    WitnessesAddHandler witnessAddHandler = new WitnessesAddHandler();
                    for (int i = 0; i < witnessData.Count; i++)
                    {
                        if (witnessAddHandler.ExistsUserWithSameEmail(witnessData[i].Email))
                        throw new ValidationException("Email", "Witness's Email already exists");

                        var witness = Core.User.CreateWitness(witnessData[i].FirstName, witnessData[i].LastName, witnessData[i].UserName, witnessData[i].Password, witnessData[i].Email, witnessData[i].CountryId, System.DateTime.Now, 0, false, false, true);
                        UsersAddHandler.Save(witness);
                        witnessAddHandler.Handle(user, witness);

                        //Link new owner with Trial Mode
                        var user_userType3 = User_UserType.CreateRelationUserAndUserType(user.Id, witness.Id, (int)UserRoles.Witness, 1);
                        UsersAddHandler.Save(user_userType3);
                        witness = null;
                    }

                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            //}

            //if (!ModelState.IsValid)
            //    return this.View();

            //if (invitationId.HasValue)
            //{
            //User user1 = Current.Connection.Get<User>(Current.UserId); 
            //AuthenticationService.SignIn(user1);
            //    return new RedirectToAccountType(user);
            //}

            return RedirectToAction("Signin5", "Registration");

        }

        [AllowAnonymous]
        public ActionResult Signin5()
        {
            return View();
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
    }
}
