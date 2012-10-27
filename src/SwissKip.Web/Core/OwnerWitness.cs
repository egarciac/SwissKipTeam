namespace SwissKip.Web.Core
{
    public class OwnerWitness
    {
        public OwnerWitness(int ownerId, int witnessId)
        {
            this.OwnerId = ownerId;
            this.WitnessId = witnessId;
        }

        public int Id { get; set; }

        public int OwnerId { get; set; }

        public int WitnessId { get; set; }

        public bool Confirmed { get; set; }

        public bool OwnerDied { get; set; }
    }
}