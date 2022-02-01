using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;


public partial class NotificationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            selectall();

        }
    }
    public void selectall()
    {
        DataTable dt = new DataTable();
        NotificationBAL objBAL = new NotificationBAL();
       
        dt = objBAL.Selectall();
        gvnotifi.DataSource = dt;
        gvnotifi.DataBind();

    }

    protected void gvnotify_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            NotificationBAL objBAL = new NotificationBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            selectall();
        }

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("Notification.aspx");
    }
}