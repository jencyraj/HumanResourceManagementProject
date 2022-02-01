using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class LeaveApprovalReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            ddlStatus.Items.Clear();

            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("all"), ""));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("pending"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "N"));

            btnSearch.Text = hrmlang.GetString("search");
        }
    }

    private void BindLeaves()
    {
        /*LeaveBAL objBAL = new LeaveBAL();

        DataTable dT = objBAL.SelectAll(new LeaveBOL());
        DataView dView = dT.DefaultView;*/
        string sParams = "";
        if (ddlStatus.SelectedValue != "")
            sParams = "&sp1=" + ddlStatus.SelectedValue;
        //dView.RowFilter = "ApprovalStatus='" + ddlStatus.SelectedValue + "'";


        /*  if (dT.Rows.Count == 0)
              lblErr.Text = hrmlang.GetString("recordnotfound");
          else
          {
              Session["rptdata"] = dView.ToTable();*/
        string url = "HRMReports.aspx?rptname=LeaveApprovalRpt&printtype=" + rbtnPrint.SelectedValue + sParams+"&rptcase=LEAVEAPPROVAL";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        //  string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaves();
    }
}