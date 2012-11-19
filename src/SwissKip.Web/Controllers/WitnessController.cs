namespace SwissKip.Web.Controllers
{
    using System.Web.Mvc;
    using SwissKip.Web.Models;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Queries;

    public class WitnessController:Controller
    {
        public ActionResult Index()
        {
            var owners = new OwnersByWitnessQuery(Current.UserId).Execute();
            return View(owners);
        }

        [HttpPost]
        public ActionResult Index(OwnerByWitnessModel witness)
        {
            var getOwner = new WitnessesAddHandler().FindIdOwner(Current.UserId); 
            var userFather = new WitnessesAddHandler().FindFatherInfo(getOwner.UserIdFather.Value);
            var sent = new WitnessesAddHandler();
            sent.SendDeadReport(Current.UserId, userFather);
            return RedirectToAction("Message", "Witness");
        }

        public ActionResult Message()
        {
            var owners = new OwnersByWitnessQuery(Current.UserId).Execute();
            return View(owners);
        }
    }
}