namespace SwissKip.Web.Helpers.ActionResults
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using SwissKip.Web.Core;

    public class RedirectToAccountType : ActionResult
    {
        private readonly User user;

        public RedirectToAccountType(User user)
        {
            this.user = user;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            RedirectToRouteResult result = null;
            if (this.user.IsOwner)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Owner" },
                            { "Action", "Index" } });
            }
            else if (this.user.IsWitness)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Witness" },
                            { "Action", "Index" } });
            }
            else if (this.user.IsDataheir)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Recipient" },
                            { "Action", "Index" } });
            }

            result.ExecuteResult(context);
        }
    }
}