namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using DataAnnotationsExtensions;
    using SwissKip.Web.Helpers.Attributes;
    using System.Collections.Generic;

    public class FileManagerModel
    {

        public string[] Folders { get; set; }

        public string[] Files { get; set; }

    }
}