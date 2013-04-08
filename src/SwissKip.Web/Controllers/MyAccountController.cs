namespace SwissKip.Web.Controllers
{
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

    public class MyAccountController : Controller
    {
        public ActionResult Index()
        {
            var user = Current.User;
            var model = Mapper.Map<AccountEditModel>(user);
            if (model.Birthday == null)
                model.Birthday = Birthday.CreateFrom(user.Birthday);
            
            model.fileName = "/Content/images/" + user.UserName + ".jpg"; 
            ViewBag.CountryId = Current.Connection.GetList<Country>().ToSelectList(model.CountryId.ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AccountEditModel model, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    if (file != null )
                    {
                        User user = Current.Connection.Get<User>(Current.UserId); ;
                        AuthenticationService.SignIn(user);
                        var serverPath = Server.MapPath("~/Content/images/" + user.UserName + ".jpg");
                        file.SaveAs(serverPath);
                    }

                    new AccountEditHandler().Handle(Current.UserId, model);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Key, e.Message);
                }
            //}

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.CountryId = Current.Connection.GetList<Country>().ToSelectList(model.CountryId.ToString());
            //    return this.View(model);
            //}
            return RedirectToAction("index","Owner");
        }
    }
}