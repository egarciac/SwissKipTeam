namespace SwissKip.Web.Helpers.ActionResults
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using SwissKip.Web.Core;

    public class RedirectToAccountType : ActionResult
    {
        private readonly Account account;

        public RedirectToAccountType(Account account)
        {
            this.account = account;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            RedirectToRouteResult result = null;
            if (this.account.IsOwner)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Owner" },
                            { "Action", "Index" } });
            }
            else if (this.account.IsWitness)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Witness" },
                            { "Action", "Index" } });
            }
            else if (this.account.IsRecipient)
            {
                result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "Controller", "Recipient" },
                            { "Action", "Index" } });
            }

            result.ExecuteResult(context);
        }
    }
}