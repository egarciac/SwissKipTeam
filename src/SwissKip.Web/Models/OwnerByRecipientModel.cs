namespace SwissKip.Web.Models
{
    public class OwnerByRecipientModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public bool Confirmed { get; set; }

        public bool OwnerDied { get; set; }
    }
}