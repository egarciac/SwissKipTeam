namespace SwissKip.Web.Handlers
{
    using System.Web;

    using DapperExtensions;
    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;
    using SwissKip.Web.Security;

    public class DigitalAccountCreateHandler
    {
        public DigitalAccount Handle(DigitalAccountCreateModel form)
        {
            var owner = DigitalAccount.CreateDigitalAccount(form.UserId, form.WebSite, form.Username, form.Password, System.DateTime.Now, 1);
            Save(owner);
            return owner;
        }
        
        private static void Save(DigitalAccount digitalaccount)
        {
            Current.Connection.Insert(digitalaccount);
        }

        private static DigitalAccount Find(int Accountid)
        {
            return Current.Connection.Get<DigitalAccount>(Accountid);
        }

    }
}