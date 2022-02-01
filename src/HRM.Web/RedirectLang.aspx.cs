using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RedirectLang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string PageName = HttpContext.Current.Cache["PageName"].ToString();
        HttpContext.Current.Cache["PageName"] = string.Empty;
        Session["LanguageId"] = Request.QueryString["LanguageId"];
        Response.Redirect(PageName);
    }
}