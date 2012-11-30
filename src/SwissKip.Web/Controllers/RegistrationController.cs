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
                try
                {
                    UserCreateHandler userCreateHandler = new UserCreateHandler();
                    UsersAddHandler usersAddHandler = new UsersAddHandler();

                    User user =  usersAddHandler.Find(model.Email);

                    if (user != null && user.IsOwner)
                        throw new ValidationException("Email", "Owner's Email already exists");

                    if (userCreateHandler.ExistsUserWithSameUserName(model.UserName))
                        throw new ValidationException("UserName", "Owner's UserName already exists");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View();
                }

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
        public ActionResult Signin2(DataheirCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserCreateModel user = (UserCreateModel)Session["Signin1Store"];
                
                    if (user != null && user.Email == model.Email)
                        throw new ValidationException("Email", "Dataheir's Email should different to the new owner");
                
                    Session["Signin1Store"] = Session["Signin1Store"];
                    Session["Signin2Store"] = model;
                    return RedirectToAction("Signin3", "Registration");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View();
                }
            }
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult Signin3()
        {
            Session["Signin1Store"] = Session["Signin1Store"];
            Session["Signin2Store"] = Session["Signin2Store"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin3(List<DataheirCreateModel> witnesses)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserCreateModel user = (UserCreateModel)Session["Signin1Store"];
                    DataheirCreateModel dataheir = (DataheirCreateModel)Session["Signin2Store"];

                    for (int i=0; i<witnesses.Count; i++)
                    {
                        if (user != null && user.Email == witnesses[i].Email)
                                throw new ValidationException("Email", "Witness's Email should different to the new owner");
                    
                        if (dataheir != null && dataheir.Email == witnesses[i].Email)
                                throw new ValidationException("Email", "Witness's Email should different to the new dataheir");
                    
                    }

                    Session["Signin1Store"] = Session["Signin1Store"];
                    Session["Signin2Store"] = Session["Signin2Store"];
                    Session["Signin3Store"] = witnesses;
                    return RedirectToAction("Signin4", "Registration");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View();
                }
            }
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult Signin4()
        {
            Session["Signin1Store"] = Session["Signin1Store"];
            Session["Signin2Store"] = Session["Signin2Store"];
            Session["Signin3Store"] = Session["Signin3Store"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin4(MessageModel form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //storing information about new user
                    UserCreateModel usersData = (UserCreateModel)Session["Signin1Store"];
                    UserCreateHandler userCreateHandler = new UserCreateHandler();

                    var user = Core.User.CreateOwner(usersData.FirstName, usersData.LastName, usersData.UserName, usersData.Password, usersData.Email, usersData.CountryId, System.DateTime.Now, 1, true, false, false);
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

                    //Link new owner with Trial Mode - Status=1 : new owner
                    var user_userType1 = User_UserType.CreateRelationUserAndUserType(0, user.Id, (int)UserRoles.Owner, 1);
                    UsersAddHandler.Save(user_userType1);
                    
                    DataheirCreateModel dataheirData = (DataheirCreateModel)Session["Signin2Store"];
                    DataheirAddHandler dataheirAddHandler = new DataheirAddHandler();

                    //Validating Dataheir's Email already existed
                    UsersAddHandler usersAddHandler = new UsersAddHandler();
                    User ExistedUser =  usersAddHandler.Find(dataheirData.Email);
                    User dataheir = new User();
                    
                    //Find info already existed in DB
                    if (ExistedUser != null && ExistedUser.Email == dataheirData.Email)
                    {
                        ExistedUser.IsDataheir = true;
                        usersAddHandler.Update(ExistedUser);
                        dataheir = ExistedUser;
                        ExistedUser = null;
                    }
                    else
                    {
                        dataheir = Core.User.CreateDataheir(dataheirData.FirstName, dataheirData.LastName, dataheirData.UserName, dataheirData.Password, dataheirData.Email, dataheirData.CountryId, System.DateTime.Now, 0, false, true, false);
                        UsersAddHandler.Save(dataheir);
                    }

                    //Sent Confirmation
                    dataheirAddHandler.Handle(user, dataheir);

                    //Link new owner with Trial Mode
                    var user_userType2 = User_UserType.CreateRelationUserAndUserType(user.Id, dataheir.Id, (int)UserRoles.Dataheir, 0);
                    UsersAddHandler.Save(user_userType2);

                    //Create Witness
                    var witnessData = (List<DataheirCreateModel>)Session["Signin3Store"];
                    WitnessesAddHandler witnessAddHandler = new WitnessesAddHandler();
                    User witness = new User();

                    for (int i=0; i<witnessData.Count; i++)
                    {
                        //Adding validations - new!
                        User ExistedUser1 = usersAddHandler.Find(witnessData[i].Email);

                        if (ExistedUser1 != null && ExistedUser1.Email == witnessData[i].Email)
                        {
                            ExistedUser1.IsWitness = true;
                            usersAddHandler.Update(ExistedUser1);
                            witness = ExistedUser1;
                        }
                        else
                        {
                            witness = Core.User.CreateWitness(witnessData[i].FirstName, witnessData[i].LastName, witnessData[i].UserName, witnessData[i].Password, witnessData[i].Email, witnessData[i].CountryId, System.DateTime.Now, 0, false, false, true);
                            UsersAddHandler.Save(witness);
                        }

                        //Sent Confirmation
                        witnessAddHandler.Handle(user, witness);

                        //Link new owner with Trial Mode
                        var user_userType3 = User_UserType.CreateRelationUserAndUserType(user.Id, witness.Id, (int)UserRoles.Witness, 0);
                        UsersAddHandler.Save(user_userType3);
                        witness = null;
                        ExistedUser1 = null;
                    }

                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View(); 
                }
            }

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
