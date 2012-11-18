using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SwissKip.Web
{
    public partial class ListFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IZ.WebFileManager.RootDirectory rootNew = new IZ.WebFileManager.RootDirectory(); 
            FileManager1.RootDirectories.Add(rootNew);
            FileManager1.RootDirectories[0].DirectoryPath = "~/SwissKip/" +  Session["username"] + "/"; 
        }
    }
}