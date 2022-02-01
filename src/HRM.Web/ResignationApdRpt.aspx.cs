using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class EmployeeTimeSheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranchDD();
          
        }
    }

    private void BindBranchDD()
    {
        ddBranches.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddBranches.DataValueField = "BranchId";
        ddBranches.DataTextField = "Branch";
        ddBranches.DataBind();
        ddBranches.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";

        string sParams = "&sp1=" + hfEmployeeId.Value + "&sp2=" + ddlStatus.SelectedValue;
        sParams += (chkBreak.Checked) ? "&sp5=Y" : "&sp5=N";
        sParams += (chkOverTime.Checked) ? "&sp6=Y" : "&sp6=N";

        string url = "HRMReports.aspx?rptname=Resignationrpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=Resg";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnresignatnclick(object sender, EventArgs e)
    {
      //  latedetails();
    }
}