using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwissKip.Web.Models;
using System.Configuration;
using SwissKip.Web.Core;
using DapperExtensions;

namespace SwissKip.Web.Controllers
{
    using SwissKip.Web.Security;

    public class FileManagerController : Controller
    {
        
        public ActionResult Index()
        {
            User user = Current.Connection.Get<User>(Current.UserId); ;
            var Path = Server.MapPath("~/Swisskip/" + user.UserName);
            FileManagerModel fileManagerModel = new FileManagerModel();
            fileManagerModel.Folders = Directory.GetDirectories(Path);

            for (int i = 0; i < fileManagerModel.Folders.Length; i++)
            {
                string[] folder = fileManagerModel.Folders[i].Split('\\');
                fileManagerModel.Folders[i] = folder[folder.Length-1];
            }

            return View(fileManagerModel);
        }

        public ActionResult ListFiles(string folder)
        {
            User user = Current.Connection.Get<User>(Current.UserId); ;
            var Path = Server.MapPath("~/Swisskip/" + user.UserName + "/" + folder);
            FileManagerModel fileManagerModel = new FileManagerModel();
            fileManagerModel.Folders = Directory.GetFiles(Path);
            fileManagerModel.Files = Directory.GetFiles(Path);

            for (int i = 0; i < fileManagerModel.Folders.Length; i++)
            {
                string[] url = fileManagerModel.Folders[i].Split('\\');
                fileManagerModel.Folders[i] = url[url.Length - 1];
            }

            Session["folder"] = folder;
            return View(fileManagerModel);
        }

        public ActionResult CreateFolder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFolder(string folder)
        {
            User user = Current.Connection.Get<User>(Current.UserId); ;
            var path = "/Swisskip/" + user.UserName + "/" + folder ;
            if (!System.IO.Directory.Exists(Server.MapPath(path)))
                System.IO.Directory.CreateDirectory(Server.MapPath(path));

            return RedirectToAction("Index");
        }

        public ActionResult UploadFile(string folder)
        {
            Session["folder"] = folder;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            string folder = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    folder = (string)Session["folder"];
                    User user = Current.Connection.Get<User>(Current.UserId); ;
                    AuthenticationService.SignIn(user);
                    var serverPath = Server.MapPath("~/Swisskip/" + user.UserName + "/" + folder + "/" + file.FileName);
                    file.SaveAs(serverPath);
                }
            }

            return RedirectToAction("ListFiles", new { folder = folder});

        }

        public ActionResult Download(string file)
        {
            if (file != null)
            {
                string folder = (string)Session["folder"];
                User user = Current.Connection.Get<User>(Current.UserId); ;
                AuthenticationService.SignIn(user);
                var serverPath = Server.MapPath("~/Swisskip/" + user.UserName + "/" + folder + "/" + file);
                Session["folder"] = folder;
                return File(new FileStream(serverPath, FileMode.Open), "content-dispostion", file);
            }
 
            throw new ArgumentException("Invalid file name of file not exist");
        }

        public ActionResult Remove(string file)
        {
            string folder = null;
            if (file != null)
            {
                folder = (string)Session["folder"];
                User user = Current.Connection.Get<User>(Current.UserId); ;
                AuthenticationService.SignIn(user);
                var serverPath = Server.MapPath("~/Swisskip/" + user.UserName + "/" + folder + "/" + file);
                FileInfo newFile = new FileInfo(serverPath);
                newFile.Delete(); 

                Session["folder"] = folder;
                //return File(new FileStream(serverPath, FileMode.Open), "content-dispostion", file);
            }

            return RedirectToAction("ListFiles", new { folder = folder });
            //throw new ArgumentException("Invalid file name of file not exist");
        }
    }
}
