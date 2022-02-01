using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublishtoSite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltText.Text = "<iframe src=\"" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + "careers.aspx\" style=\"margin-top:0px; margin-left:0px; width:800px; height:600px;\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\" scrolling=\"auto\"></iframe>";
    }
}