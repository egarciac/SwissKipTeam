namespace SwissKip.Web.Core
{
    using SwissKip.Web.Data;

    public class OwnerRecipient
    {
        public OwnerRecipient() { }

        public OwnerRecipient(int ownerId, int recipientId)
        {
            this.OwnerId = ownerId;
            this.RecipientId = recipientId;
        }

        /* Para la key sea autoasignada debe de ser de un tipo diferente de int */
        [KeyAssigned]
        public int OwnerId { get; set; }

        public int RecipientId { get; set; }
    }
}