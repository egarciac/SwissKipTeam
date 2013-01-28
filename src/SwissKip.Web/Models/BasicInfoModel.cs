namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    using SwissKip.Web.Helpers.Attributes;

    public class BasicInfoModel
    {

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int IconId { get; set; }

        public string Description1 { get; set; }

        public int ColourId { get; set; }

        public string Description2 { get; set; }

        public string SecretPhrase { get; set; }

        public int TokenNumber { get; set; }

        public int? TokenNumberNew { get; set; }

        public int Count { get; set; }

    }
}