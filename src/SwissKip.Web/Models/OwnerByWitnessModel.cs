namespace SwissKip.Web.Models
{
    public class WitnessPanelModel
    {
        public OwnerByWitnessModel MyAccountWidget2 { get; set; }
    }

    public class OwnerByWitnessModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsOwner { get; set; }

        public bool IsDataheir { get; set; }

        public bool IsWitness { get; set; }
        
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