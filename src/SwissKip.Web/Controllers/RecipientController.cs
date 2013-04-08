namespace SwissKip.Web.Controllers
{
    using System.Web.Mvc;
    using SwissKip.Web.Models;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Queries;

    public class RecipientController : Controller
    {
        public ActionResult Index()
        {
            var owners = new OwnersByRecipientQuery(Current.UserId).Execute();
            Session["username"] = Current.User.UserName;
            return View(owners);
        }

        public ActionResult MessageSent(int value)
        {
            //var getOwner = new DataheirAddHandler().FindIdOwner(Current.UserId);
            var userFather = new DataheirAddHandler().FindFatherInfo(value);
            var sent = new DataheirAddHandler();
            sent.SendDeadReport(value, userFather);
            return RedirectToAction("Message", "Recipient");
        }

        //[HttpPost]
        //public ActionResult Index(OwnerByRecipientModel dataheir)
        //{
        //    var getOwner = new DataheirAddHandler().FindIdOwner(Current.UserId);
        //    var userFather = new DataheirAddHandler().FindFatherInfo(getOwner.UserIdFather.Value);
        //    var sent = new DataheirAddHandler();
        //    sent.SendDeadReport(getOwner.UserIdFather.Value, userFather);
        //    return RedirectToAction("Message", "Recipient");
        //}

        public ActionResult Message()
        {
            var owners = new OwnersByRecipientQuery(Current.UserId).Execute1();
            return View(owners);
        }
    }
}