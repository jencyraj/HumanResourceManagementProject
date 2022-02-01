using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AddTransfer : System.Web.UI.Page
{
    #region PAGE LOAD

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCompanyID.Text = "" + Session["COMPANYID"];
            lblTransferID.Text = "" + Request.QueryString["id"];
            GetBranches();

            GetTransferDetails();

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
            txtReportTo.Attributes.Add("placeholder", hrmlang.GetString("enterreportto"));
        }
    }

    #endregion

    #region GetTransferDetails

    private void GetTransferDetails()
    {
        EmpTransferBOL objBOL = new EmpTransferBOL();
        objBOL.TransferID = Util.ToInt(lblTransferID.Text);
        if(objBOL.TransferID == 0) return;

        DataTable dT = new EmpTransferBAL().SelectAll(objBOL);

        if (dT.Rows.Count > 0)
        {
            DataRow dRow = dT.Rows[0];
            if ("" + dRow["TransferID"] == lblTransferID.Text)
            {
                txtEmployee.Text = "" + dRow["EmployeeName"];
                hfEmployeeId.Value = "" + dRow["EmployeeID"];
                BindForwardTo();
                ddlForward.SelectedValue = "" + dRow["ForwardedTo"];
                ctlCalendarBD.SelectedCalendareDate = DateTime.Parse("" + dRow["TransferDate"]);
                ddlBranch.SelectedValue = "" + dRow["BranchTo"];
                GetDepartments();
                ddlDept.SelectedValue = "" + dRow["DeptTo"];
                GetSubDepartments();
                ddlSubDept.SelectedValue = "" + dRow["SubDeptTo"];
                txtReportTo.Text = "" + dRow["ReportEmployee"];
                hfReportId.Value = "" + dRow["ReportTo"];
            }
            else
            {
                lblTransferID.Text = "";
                ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('No data exists');", true);
                Clear();
            }
        }
        else
        {
            lblTransferID.Text = "";
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('No data exists');", true);
            Clear();
        }
    }

    #endregion

    #region BIND DROPDOWNS

    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataSource = ReturnDT("BranchID", "Branch", dt);
        ddlBranch.DataBind();
    }

    private void BindForwardTo()
    {
        ddlForward.Items.Clear();
        if (hfEmployeeId.Value != "")
        {
            DataTable dt = new ReportingOfficersBAL().SelectSuperiors(Util.ToInt(hfEmployeeId.Value));
            ddlForward.DataSource = ReturnDT("SuperiorID", "FullName", dt);
            ddlForward.DataBind();
        }
    }

    private void GetDepartments()
    {
        ddlDept.Items.Clear();
        ddlSubDept.Items.Clear();
        if (ddlBranch.SelectedValue != "")
        {
            DataTable dt = new OrgDepartmentsBAL().SelectDepartmentsByBranchID(Util.ToInt(ddlBranch.SelectedValue));
            ddlDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
            ddlDept.DataBind();
        }
    }

    private void GetSubDepartments()
    {
        ddlSubDept.Items.Clear();
        if (ddlDept.SelectedValue != "")
        {
            OrgDepartmentBOL objBOL = new OrgDepartmentBOL();
            objBOL.ParentDeptID = Util.ToInt(ddlDept.SelectedValue);

            DataTable dt = new OrgDepartmentsBAL().SelectAll(objBOL);
            ddlSubDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
            ddlSubDept.DataBind();
        }
    }

    #endregion

    #region VALIDATE INPUTS

    private string ValidateInput()
    {
        if (hfEmployeeId.Value == "" || txtEmployee.Text.Trim() == "")
        {
            txtEmployee.Focus();
            return "Please select an Employee.";
        }

        if (ddlForward.SelectedValue == "0")
        {
            ddlForward.Focus();
            return "Please select the Forwad To Officer.";
        }

        if (ctlCalendarBD.getGregorianDateText == "")
        {
            ctlCalendarBD.Focus();
            return "Please select the Request Date.";
        }

        if (ddlBranch.SelectedValue == "0")
        {
            ddlBranch.Focus();
            return "Please select the Branch.";
        }

        if (ddlDept.SelectedValue == "0")
        {
            ddlDept.Focus();
            return "Please select the Department.";
        }

        /* if (ddlSubDept.SelectedValue == "0")
         {
             ddlSubDept.Focus();
             return "Please select the Sub Department.";
         }*/

        if (txtReportTo.Text.Trim() == "0")
        {
            txtReportTo.Focus();
            return "Please select the Report To Officer.";
        }

        return "";
    }

    #endregion

    #region Save Data

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sErr = ValidateInput();
            if (sErr != "")
            {
                ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + sErr + "');", true);
                return;
            }

            EmpTransferBOL objBOL = new EmpTransferBOL();
            objBOL.TransferID = Util.ToInt(lblTransferID.Text);
            objBOL.EmployeeID = Util.ToInt(hfEmployeeId.Value);
            objBOL.ForwardTo = Util.ToInt(ddlForward.SelectedValue);
            objBOL.BranchTo = Util.ToInt(ddlBranch.SelectedValue);
            objBOL.DeptTo = Util.ToInt(ddlDept.SelectedValue);
            objBOL.SubDeptTo = Util.ToInt(ddlSubDept.SelectedValue);
            objBOL.ReportTo = Util.ToInt(hfReportId.Value);
            objBOL.CreatedBy = User.Identity.Name;
            objBOL.TransferDate = Util.ToDateTime(ctlCalendarBD.getGregorianDateText);

            lblTransferID.Text = new EmpTransferBAL().Save(objBOL).ToString();

            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('Data saved successfully.');", true);

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + ex.Message + "');", true);
        }
    }

    #endregion

    #region Clear Data

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtEmployee.Text = "";
        hfEmployeeId.Value = "";
        txtReportTo.Text = "";
        hfReportId.Value = "";
        ddlBranch.SelectedIndex = 0;
        ddlDept.Items.Clear();
        ddlForward.Items.Clear();
        ddlSubDept.Items.Clear();
        lblTransferID.Text = "";
    }

    #endregion

    #region Control Data Change Events

    protected void txtEmployee_TextChanged(object sender, EventArgs e)
    {
        BindForwardTo();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();
    }

    #endregion
}