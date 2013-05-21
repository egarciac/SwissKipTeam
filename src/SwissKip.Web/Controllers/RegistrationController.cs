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
        public ActionResult Signin1(UserCreateModel usersData, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserCreateHandler userCreateHandler = new UserCreateHandler();
                    UsersAddHandler usersAddHandler = new UsersAddHandler();
                    User user1 = usersAddHandler.Find(usersData.Email);

                    if (user1 != null && user1.IsOwner)
                        throw new ValidationException("Email", "Owner's Email already exists");

                    if (userCreateHandler.ExistsUserWithSameUserName(usersData.UserName))
                        throw new ValidationException("UserName", "Owner's UserName already exists");

                    string url = GetRandomString(20);
                    //storing information about new user
                    var user = Core.User.CreateOwner(usersData.FirstName, usersData.LastName, usersData.UserName, usersData.Password, usersData.Email, null, null, usersData.CountryId, null, 0, 0, null, 0, System.DateTime.Now, 1, 0, url, false, false, true, false, false);
                    UsersAddHandler.Save(user);
                    userCreateHandler.Handle(user);

                    //Add path for new photo
                    var path = "/Swisskip/" + user.UserName;
                    if (!System.IO.Directory.Exists(Server.MapPath(path)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(path));

                    //Creating a default photo
                    string newFile = Server.MapPath("/Content/images/") + user.UserName + ".jpg";
                    if (file != null)
                    {
                        file.SaveAs(newFile);
                    }
                    else
                    {
                        if (!System.IO.File.Exists(newFile))
                            System.IO.File.Copy(Server.MapPath("/Content/images/unknown.jpg"), Server.MapPath("/Content/images/") + user.UserName + ".jpg");
                    }

                    //Link new owner with Trial Mode - Status=1 : new owner
                    var user_userType1 = User_UserType.CreateRelationUserAndUserType(0, user.Id, (int)UserRoles.Owner, 0, System.DateTime.Now, 0, 0, 1);
                    UsersAddHandler.Save(user_userType1);

                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View();
                }

                return RedirectToAction("MailVerification", "Authentication");
            }
            return this.View();
        }
        
        [AllowAnonymous]
        public ActionResult Signin2(string userId, string type)
        {
            UsersAddHandler usersAddHandler = new UsersAddHandler();
            User user = usersAddHandler.Find2(userId, type);
            Session["Signin1Store"] = user;
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
                    User user = (User)Session["Signin1Store"];
                
                    if (user != null && user.Email == model.Email)
                        throw new ValidationException("Email", "Data-Heir's Email should different to the new owner");
                
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
        public ActionResult BlanckWitness()
        {
            return this.PartialView("_Witness1");
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
                    User user = (User)Session["Signin1Store"];
                    DataheirCreateModel dataheir = (DataheirCreateModel)Session["Signin2Store"];

                    for (int i=0; i<witnesses.Count; i++)
                    {
                        if (user != null && user.Email == witnesses[i].Email)
                                throw new ValidationException("Email", "Witness's Email should different to the new owner");
                    
                        if (dataheir != null && dataheir.Email == witnesses[i].Email)
                                throw new ValidationException("Email", "Witness's Email should different to the new dataheir");
                    
                    }

                    Session["Signin1Store"] = user;
                    Session["Signin2Store"] = dataheir;
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
            User usersData = null ;
            DataheirCreateModel dataheirData = null;
            List<DataheirCreateModel> witnessData = null;
            
            if (ModelState.IsValid)
            {
                try
                {
                    //storing information about new user
                    UsersAddHandler usersAddHandler1 = new UsersAddHandler();
                    usersData = (User)Session["Signin1Store"];
                    User user = usersAddHandler1.Find(usersData.Email);
                    //UserCreateHandler userCreateHandler = new UserCreateHandler();

                    //var user = Core.User.CreateOwner(usersData.FirstName, usersData.LastName, usersData.UserName, usersData.Password, usersData.Email, usersData.CountryId, 0, 0, null, 0, System.DateTime.Now, 1, 0, false, false, true, false, false);
                    //UsersAddHandler.Save(user);
                    //userCreateHandler.Handle(user);

                    ////Add path for new photo
                    //var path = "~/Swisskip/" + user.UserName;
                    //if (!System.IO.Directory.Exists(Server.MapPath(path)))
                    //    System.IO.Directory.CreateDirectory(Server.MapPath(path));

                    //Session["path"] = path;
                    ////Session["username"] = user.UserName;

                    ////Creating a default photo
                    //string newFile = Server.MapPath("~/Content/images/") + user.UserName + ".jpg";
                    //if (!System.IO.File.Exists(newFile))
                    //    System.IO.File.Copy(Server.MapPath("~/Content/images/unknown.jpg"), Server.MapPath("~/Content/images/") + user.UserName + ".jpg");

                    //AuthenticationService.SignIn(user);

                    ////Link new owner with Trial Mode - Status=1 : new owner
                    //var user_userType1 = User_UserType.CreateRelationUserAndUserType(0, user.Id, (int)UserRoles.Owner, 0, System.DateTime.Now, 0, 0, 1);
                    //UsersAddHandler.Save(user_userType1);

                    dataheirData = (DataheirCreateModel)Session["Signin2Store"];
                    DataheirAddHandler dataheirAddHandler = new DataheirAddHandler();

                    //Validating Dataheir's Email already existed
                    UsersAddHandler usersAddHandler = new UsersAddHandler();
                    User ExistedUser = usersAddHandler.Find(dataheirData.Email);
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
                        dataheir = Core.User.CreateDataheir(dataheirData.FirstName, dataheirData.LastName, dataheirData.UserName, dataheirData.Password, dataheirData.Email, null, null, dataheirData.CountryId, null, 0, 0, null, 0, System.DateTime.Now, 0, 0, null, false, false, false, true, false);
                        UsersAddHandler.Save(dataheir);
                    }

                    //Sent Confirmation
                    dataheirAddHandler.Handle(user, dataheir, form.DataheirMsg);

                    //Link new owner with Trial Mode
                    var user_userType2 = User_UserType.CreateRelationUserAndUserType(user.Id, dataheir.Id, (int)UserRoles.Dataheir, 0, System.DateTime.Now, 0, 0, 0);
                    UsersAddHandler.Save(user_userType2);

                    //Create Witness
                    witnessData = (List<DataheirCreateModel>)Session["Signin3Store"];
                    WitnessesAddHandler witnessAddHandler = new WitnessesAddHandler();
                    User witness = new User();

                    for (int i = 0; i < witnessData.Count; i++)
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
                            witness = Core.User.CreateWitness(witnessData[i].FirstName, witnessData[i].LastName, witnessData[i].UserName, witnessData[i].Password, witnessData[i].Email, null, null, witnessData[i].CountryId, null, 0, 0, null, 0, System.DateTime.Now, 0, 0, null, false, false, false, false, true);
                            UsersAddHandler.Save(witness);
                        }

                        //Sent Confirmation
                        witnessAddHandler.Handle(user, witness, form.WitnessMsg);

                        //Link new owner with Trial Mode
                        var user_userType3 = User_UserType.CreateRelationUserAndUserType(user.Id, witness.Id, (int)UserRoles.Witness, 0, System.DateTime.Now, 0, 0, 0);
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

            Session["Signin1Store"] = usersData;
            Session["Signin2Store"] = dataheirData;
            Session["Signin3Store"] = witnessData;
            return RedirectToAction("Signin5", "Registration");

        }

        [AllowAnonymous]
        public ActionResult Signin5()
        {
            Session["Signin1Store"] = Session["Signin1Store"];
            Session["Signin2Store"] = Session["Signin2Store"];
            Session["Signin3Store"] = Session["Signin3Store"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin5(AccountEditModel form)
        {
            //if (ModelState.IsValid)
            //{
                try
                {

                    User currentUser = (User)Session["Signin1Store"];
                    UsersAddHandler usersAddHandler = new UsersAddHandler();
                    User ExistedUser = usersAddHandler.Find(currentUser.Email);
                    ExistedUser.ColourId = form.ColourId;
                    ExistedUser.IconId = form.IconId;
                    ExistedUser.SecretPhrase = form.SecretPhrase;
                    usersAddHandler.Update(ExistedUser);

                    Session["Signin1Store"] = currentUser;
                    Session["Signin2Store"] = Session["Signin2Store"];
                    Session["Signin3Store"] = Session["Signin3Store"];
                    //Session["Signin4Store"] = Session["Signin4Store"];
                    return RedirectToAction("Signin6", "Registration");

                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                    return this.View();
                }
            //}

            return RedirectToAction("Signin6", "Registration");

        }

        [AllowAnonymous]
        public ActionResult Signin6()
        {
            Session["Signin1Store"] = Session["Signin1Store"];
            Session["Signin2Store"] = Session["Signin2Store"];
            Session["Signin3Store"] = Session["Signin3Store"];
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

        public int GetRandomNumber(int maxNumber)
        {
            if (maxNumber < 1)
                throw new System.Exception("The maxNumber value should be greater than 1");
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            int seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
            System.Random r = new System.Random(seed);
            return r.Next(1, maxNumber);
        }

        public string GetRandomString(int length)
        {
            string[] array = new string[55]
	        {
		        "0","1","2","3","4","5","6","8","9",
		        "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z",
		        "A","B","C","D","E","F","G","H","J","K","L","M","N","P","R","S","T","U","V","W","X","Y","Z"
	        };
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < length; i++) sb.Append(array[GetRandomNumber(53)]);
            return sb.ToString();
        }
    }
}
