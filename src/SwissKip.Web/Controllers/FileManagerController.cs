using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwissKip.Web.Models;
using System.Configuration;

namespace SwissKip.Web.Controllers
{
    public class FileManagerController : Controller
    {
        public ActionResult ListFiles()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Upload(int? chunk, string name)
        //{
        //    var fileUpload = Request.Files[0];
        //    var uploadPath = Server.MapPath("~/App_Data");
        //    chunk = chunk ?? 0;
        //    using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
        //    {
        //        var buffer = new byte[fileUpload.InputStream.Length];
        //        fileUpload.InputStream.Read(buffer, 0, buffer.Length);
        //        fs.Write(buffer, 0, buffer.Length);
        //    }
        //    return Content("chunk uploaded", "text/plain");
        //}

        ////TODO: move this into a manager/repository class
        //private string SaveUploadedFile(MediaAssetUploadModel uploadedFileMeta)
        //{
        //    string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(uploadedFileMeta.Filename);
        //    string fullSavePath = Path.Combine(ConfigurationManager.AppSettings["MediaAssetFolder"], fileName);
        //    uploadedFileMeta.fileData.SaveAs(fullSavePath);

        //    return fullSavePath;
        //} 
    }
}
