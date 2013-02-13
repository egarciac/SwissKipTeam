using System;
namespace SwissKip.Web.Core
{
    public class Master
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int TokenExpirationTime { get; set; }

        public int Size { get; set; }

        public int Expiration_Time { get; set; }

        public Decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }
    }
}