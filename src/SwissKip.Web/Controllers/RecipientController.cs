namespace SwissKip.Web.Controllers
{
    using System.Web.Mvc;

    using SwissKip.Web.Queries;

    public class RecipientController : Controller
    {
        public ActionResult Index()
        {
            var owners = new OwnersByRecipientQuery(Current.UserId).Execute();
            return View(owners);
        }
    }
}