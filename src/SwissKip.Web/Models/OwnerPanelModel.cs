namespace SwissKip.Web.Models
{
    public class OwnerPanelModel
    {
        public MyAccountWidgetModel MyAccountWidget { get; set; }
    }

    public class MyAccountWidgetModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Dataheir { get; set; }

        public string Confirmation { get; set; }
        
        public int Witness { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}