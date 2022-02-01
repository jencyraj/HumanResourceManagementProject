using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HRM.BOL;
using HRM.BAL;

public partial class EmployeeIrisReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetBranch();
        ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "EmployeeIrisReport.aspx");

    }
    private void GetBranch()
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();

        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        ddlBranch.DataSource = objBr.SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, lstItem);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string sParams = "&sp1=" + ddlBranch.SelectedValue + "&sp2=" + ctlCalDepDob.getGregorianDateText + "&sp3=" + CtlJoin1.getGregorianDateText + "&sp4=" + txteName.Text + "&sp5=" + txtEmpCode.Text; ;
        string url = "HRMReports.aspx?rptname=EmployeeIrisRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=IRISRPT";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}