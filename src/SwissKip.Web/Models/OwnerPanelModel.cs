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

        public int Witnesses { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}