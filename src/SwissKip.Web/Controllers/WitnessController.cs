namespace SwissKip.Web.Controllers
{
    using System.Web.Mvc;
    using SwissKip.Web.Models;
    using SwissKip.Web.Core;
    using SwissKip.Web.Handlers;
    using SwissKip.Web.Queries;
    using System.Collections.Generic;

    public class WitnessController:Controller
    {
        public ActionResult Index()
        {
            var owners = new OwnersByWitnessQuery(Current.UserId).Execute();
            return View(owners);
        }

        public ActionResult MessageSent(int value)
        {
            //Updating info who has sent Notification
            var handled = new WitnessesAddHandler();
            var getOwner = handled.FindIdOwner2(Current.UserId);
            getOwner.ReportedDeath = 1;
            handled.UpdateRelation(getOwner);
            
            //Email Notification
            var userFather = new WitnessesAddHandler().FindFatherInfo(value);
            var sent = new WitnessesAddHandler();
            sent.SendDeadReport(Current.UserId, userFather);

            //var dependedList = new List<User_UserType>
            //            {
            //                    new User_UserType()
            //            };

            var dependedList = new WitnessesAddHandler().FindDataheir(userFather.Id);

            var userDataheir = new WitnessesAddHandler().FindFatherInfo(dependedList[0].UserId);
            sent.SendDataheirInfo(userFather, userDataheir);
            

            //Quantity of Notifications sent
            //var count = userFather.Id;
            //handled.
            
            
            return RedirectToAction("Message", "Witness");
        }

        //[HttpPost]
        //public ActionResult Index(int value)
        //{
        //    var getOwner = new WitnessesAddHandler().FindIdOwner(value); 
        //    var userFather = new WitnessesAddHandler().FindFatherInfo(getOwner.UserIdFather.Value);
        //    var sent = new WitnessesAddHandler();
        //    sent.SendDeadReport(Current.UserId, userFather);
        //    return RedirectToAction("Message", "Witness");
        //}

        public ActionResult Message()
        {
            var owners = new OwnersByWitnessQuery(Current.UserId).Execute1();
            return View(owners);
        }
    }
}