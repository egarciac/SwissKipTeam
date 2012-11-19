namespace SwissKip.Web.Models
{
    public class OwnerByRecipientModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserIdFather { get; set; }

        public string OwnerFullName { get; set; }

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