using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        new UserBAL().SignOut("" + Session["USERID"]);

        HttpContext.Current.Cache.Remove("LangData");
        Session.Abandon();
        System.Web.Security.FormsAuthentication.SignOut();
        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
    }
}