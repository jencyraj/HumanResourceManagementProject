using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Employees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newemployee");
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("search");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "employees.aspx");
            GetCompanyDetails();
            GetDropDownValues();
        }
        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
        ViewState["resetpermissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "resetleavebalance.aspx", false);
    }

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

    }

    private void GetDepartments()
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();

        DataTable dt = objDept.SelectDepartmentsByBranchID(Util.ToInt(ddlBranch.SelectedValue));
        ddlDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlDept.DataBind();
        GetSubDepartments();
    }

    private void GetSubDepartments()
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();
        OrgDepartmentBOL objBOL = new OrgDepartmentBOL();

        objBOL.ParentDeptID = Util.ToInt(ddlDept.SelectedValue);
        DataTable dt = objDept.SelectAll(objBOL); ;
        if (Util.ToInt(ddlDept.SelectedValue) == 0)
            dt.Rows.Clear();
        ddlSubDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlSubDept.DataBind();
    }

    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }

    private void GetDropDownValues()
    {
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        ddlBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataBind();

        OrgDesignationBAL objDesgn = new OrgDesignationBAL();
        ddlDesgn.DataSource = objDesgn.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDesgn.DataBind();

        RoleBAL objRole = new RoleBAL();
        ddlRole.DataSource = objRole.SelectAll(0);
        ddlRole.DataBind();

        ddlDept.Items.Insert(0, lstItem);
        ddlSubDept.Items.Insert(0, lstItem);
        ddlBranch.Items.Insert(0, lstItem);
        ddlDesgn.Items.Insert(0, lstItem);
        ddlRole.Items.Insert(0, lstItem);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEmployee.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        if (ddlBranch.SelectedIndex > 0)
            objBOL.BranchID = int.Parse(ddlBranch.SelectedValue);

        if (ddlDept.SelectedIndex > 0)
            objBOL.DeptId = int.Parse(ddlDept.SelectedValue);

        if (ddlStatus.SelectedIndex > 0)
            objBOL.EmpStatus = ddlStatus.SelectedValue;

        if (ddlRole.SelectedIndex > 0)
            objBOL.RoleID = int.Parse(ddlRole.SelectedValue);

        if (ddlDesgn.SelectedIndex > 0)
            objBOL.DesgnID = int.Parse(ddlDesgn.SelectedValue);

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();
        objBOL.EmpCode = txtEmpCode.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();

        if (dtEmp.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("norecordsfound");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlDesgn.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;

        txtEmpCode.Text = "";
        txtfName.Text = "";
        txtlName.Text = "";
        txtmName.Text = "";
    }
    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            EmployeeBAL objBAL = new EmployeeBAL();
            objBAL.Delete(int.Parse(e.CommandArgument.ToString()),User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("employeedeleted");
            Search();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments();
        ClientScript.RegisterStartupScript(ddlBranch.GetType(), "onchange", "ShowFilter();", true);
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();
        ClientScript.RegisterStartupScript(ddlBranch.GetType(), "onchange", "ShowFilter();", true);
    }
    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("employeecode");
            e.Row.Cells[2].Text = hrmlang.GetString("branch");
            e.Row.Cells[3].Text = hrmlang.GetString("department");
            e.Row.Cells[4].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEmployee.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEmployee.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEmployee.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEmployee.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HyperLink1");
                HyperLink HyperLink2 = (HyperLink)e.Row.FindControl("HyperLink2");
                HyperLink lnkView = (HyperLink)e.Row.FindControl("lnkView");
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                HyperLink lnksync = (HyperLink)e.Row.FindControl("lnksync");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                string[] permissions = (string[])ViewState["permissions"];
                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                lnkView.Visible = (permissions[3] == "Y") ? true : false;
                HyperLink1.Attributes.Add("title", hrmlang.GetString("addreportingofficer"));
                HyperLink2.Attributes.Add("title", hrmlang.GetString("viewuploaddocument"));
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnksync.Attributes.Add("title", hrmlang.GetString("sync"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteemployee"));
            }

            string[] sPermission =(string[]) ViewState["resetpermissions"];
            if (sPermission[0] == "Y" || sPermission[2] == "Y" || sPermission[2] == "Y")
            {
                HyperLink lnkReset=(HyperLink)e.Row.FindControl("lnkReset");
              //  lnkReset.Visible = true;
                //lnkReset.Attributes.Add("title", hrmlang.GetString("resetleavebalance"));
            }
        }
    }
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Search();
        gvEmployee.PageIndex = e.NewPageIndex;
    }
}