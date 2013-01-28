namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class IconsModel
    {
        
        public int IconId { get; set; }

        public string IconPath { get; set; }

        public int IconSelected { get; set; }

    }
}