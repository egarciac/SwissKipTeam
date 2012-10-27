namespace SwissKip.Web.Controllers
{
    using System.Web.Mvc;

    using SwissKip.Web.Queries;

    public class WitnessController:Controller
    {
        public ActionResult Index()
        {
            var owners = new OwnersByWitnessQuery(Current.UserId).Execute();
            return View(owners);
        }
    }
}