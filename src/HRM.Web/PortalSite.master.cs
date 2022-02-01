using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using HRM.BAL;
using HRM.BOL;

public partial class PortalSite : System.Web.UI.MasterPage
{
    protected string strCULTURE = "en-US";
    protected string STYLESHEET = "LR";

    protected void Page_Load(object sender, EventArgs e)
    {
    }

     
    public string GetPageName()
    {
        string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo info = new System.IO.FileInfo(path);
        return info.Name;
    }
   
}
