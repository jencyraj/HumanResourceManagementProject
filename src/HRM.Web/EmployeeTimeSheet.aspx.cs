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
            GetMonthList();
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


    private void GetMonthList()
    {
       
            DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
            ddMonth.DataSource = dt;
            ddMonth.DataBind();
            ddMonth.Items.Insert(0, hrmlang.GetString("all"));
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";

        string sParams = "&sp1=" + ddBranches.SelectedValue + "&sp2=" + hfEmployeeId.Value + "&sp3=" + txtYear.Text.Trim() + "&sp4=" + ddMonth.SelectedValue;
        sParams += (chkBreak.Checked) ? "&sp5=Y" : "&sp5=N";
        sParams += (chkOverTime.Checked) ? "&sp6=Y" : "&sp6=N";

        string url = "HRMReports.aspx?rptname=rptEmpTimeSheet&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=TS";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    public void latedetails()
    {
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";

        string sParams = "&sp1=" + ddBranches.SelectedValue + "&sp2=" + hfEmployeeId.Value + "&sp3=" + txtYear.Text.Trim() + "&sp4=" + ddMonth.SelectedValue;

        string url = "HRMReports.aspx?rptname=LatetimeSheetRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=LateTS";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


       

    }
    protected void latetimeclick(object sender, EventArgs e)
    {
        latedetails();
    }
}