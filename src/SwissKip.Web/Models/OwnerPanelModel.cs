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

        public bool IsOwner { get; set; }

        public bool IsDataheir { get; set; }

        public bool IsWitness { get; set; }
        
        public string Dataheir { get; set; }

        public int Confirmed { get; set; }
        
        public string Size { get; set; }

        public string MaxSize { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}