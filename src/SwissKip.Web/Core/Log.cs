namespace SwissKip.Web.Core
{
    using System;

    public class Log
    {
        public Log() { }

        private Log(int userId, int messageId, string detailed, DateTime createdDate, int status)
        {
            this.UserId = userId;
            this.MessageId = messageId;
            this.Detailed = detailed;
            this.CreatedDate = createdDate;
            this.Status = status;
        }

        //Register info in log table
        public static Log AddInfo(int userId, int messageId, string detailed, DateTime createdDate, int status)
        {
            return new Log(userId, messageId, detailed, createdDate, status);
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int MessageId { get; set; }

        public string Detailed { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }

    }
}