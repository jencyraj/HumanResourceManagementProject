using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class Notification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblID.Text = Request.QueryString["NotifyID"];
            selectall();

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        NotificationBAL objBAL = new NotificationBAL();
        NotificationBOL objBOL = new NotificationBOL();
        objBOL.NotifyID =Util.ToInt( lblID.Text);
        objBOL.Notification = txtnotify.Text;
        objBOL.StartDate = Util.ToDateTime(ctlCalDepDob.getGregorianDateText);
        objBOL.EndDate = Util.ToDateTime(CtlJoin1.getGregorianDateText);
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        objBAL.Savenotify(objBOL);
        Response.Redirect("NotificationList.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Notification.aspx");
       
       
    }
    public void selectall()
    {
       // if ("" + Request.QueryString["NotifyID"] == "") return;
        DataTable dt = new DataTable();
       NotificationBOL noti = new NotificationBOL();
       noti.NotifyID = Util.ToInt(Request.QueryString["NotifyID"]); 
        NotificationBAL objBAL = new NotificationBAL();
        dt = objBAL.Selectbyid(noti.NotifyID);

        if (dt.Rows.Count > 0)
        {
            DataRow dRow = dt.Rows[0];


            ctlCalDepDob.SelectedCalendareDate = Util.ToDateTime(Util.RearrangeDateTime(dRow["StartDate"]));
            CtlJoin1.SelectedCalendareDate=Util.ToDateTime(Util.RearrangeDateTime(dRow["EndDate"]));
            txtnotify.Text = "" + dRow["Notification"];
         
         

        }
    }
}