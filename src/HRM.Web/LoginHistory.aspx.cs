using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class LoginHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "loginhistory.aspx");
            GetBranches();
        }
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(Session["COMPANYID"]));
        dt = Util.ReturnDT("BranchID", "Branch", dt);
        ddlBranch.DataSource = dt;
        ddlBranch.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sUserID = "";

        if (hfEmployeeId.Value != "" && txtEmployee.Text != "")
        {
            EmployeeBOL objBOL = new EmployeeBOL();
            objBOL.EmployeeID = Util.ToInt(hfEmployeeId.Value);
            objBOL = new EmployeeBAL().Select(objBOL);
            sUserID = objBOL.UserID;
        }

        /*DataTable dtHistory = new UserBAL().LoginHistory(sUserID, false, Util.ToInt(ddlBranch.SelectedValue));
        DataView dView = dtHistory.DefaultView;

         if (dtHistory.Rows.Count == 0)
             lblErr.Text = hrmlang.GetString("recordnotfound");
         else
         {
             Session["rptdata"] = dView.ToTable();*/
        string url = "HRMReports.aspx?rptname=LoginHistoryRpt&printtype=" + rbtnPrint.SelectedValue + "&sp1=" + sUserID + "&sp2=" + ddlBranch.SelectedValue + "&rptcase=LOGINHISTORY";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        // }
    }
}