using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HRM.BOL;
using HRM.BAL;

public partial class AppraisalPeriodNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            lblAppPeriodID.Text = Request.QueryString["id"];

            string[] permissions = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "appraisalperiod.aspx");
            if (permissions[0] != "Y")
            {
                if (permissions[3] != "Y")
                {
                    Util.ShowNoPermissionPage();
                    return;
                }
                else 
                {
                    btnSave.Visible = false;
                    btnSearch.Enabled = false;
                    btnCancel.Visible = false;
                }
            }

            btnSaveEmp.Text = btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            txtStart.Attributes.Add("placeholder", hrmlang.GetString("enterstartdate"));
            txtEnd.Attributes.Add("placeholder", hrmlang.GetString("enterenddate"));
            lblTitle.Text = hrmlang.GetString("selectedemployees");

            string sLang = "" + Session["LanguageId"];
            if (sLang == "en-US" || sLang == "")
            {
                txtStart.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
                txtEnd.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }
            else
            {
                txtStart.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                txtEnd.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }

            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("notstarted"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("open"), "O"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("closed"), "C"));

            txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
            txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
            txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));

            GetDepartments();
            GetBranches();
            GetDesignation();

            GetAppraisalPeriodDetails();
        }

    }

    #region BIND DROPDOWNS

    private void GetBranches()
    {

        DataTable dT = null;
        DataTable dTBranch = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        DataTable dTemp = new DataTable();

        dTemp.Columns.Add("BranchID");
        dTemp.Columns.Add("Branch");

        if (ddlSubDept.SelectedValue != "0")
        {
            dT = new OrgDepartmentsBAL().SelectBranchesByDepartmentID(Util.ToInt(ddlSubDept.SelectedValue));
        }
        else if (ddlDept.SelectedValue != "0")
        {
            dT = new OrgDepartmentsBAL().SelectBranchesByDepartmentID(Util.ToInt(ddlDept.SelectedValue));
        }

        if (dT != null)
        {
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                DataRow[] dRow = dTBranch.Select("BranchID=" + dT.Rows[i]["BranchID"]);
                if (dRow.Length > 0)
                {
                    DataRow dR = dTemp.NewRow();
                    dR[0] = "" + dRow[0]["BranchID"];
                    dR[1] = "" + dRow[0]["Branch"];
                    dTemp.Rows.Add(dR);
                }
            }

            dTBranch = null;
            dTBranch = dTemp.Copy();
        }

        lstBranch.DataSource = dTBranch;
        lstBranch.DataBind();
    }

    private void GetDesignation()
    {
        DataTable dt = new OrgDesignationBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlDesgn.DataSource = Util.ReturnDT("DesignationID", "Designation", dt);
        ddlDesgn.DataBind();
    }

    private void GetDepartments()
    {
        DataTable dt = new OrgDepartmentsBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        dt = Util.ReturnDT("DepartmentID", "DepartmentName", dt);
        DataView dView = new DataView(dt);
        dView.RowFilter = "ISNULL(ParentDepartmentID,0) = 0";
        ddlDept.DataSource = dView.ToTable();
        ddlDept.DataBind();
        GetSubDepartments();
    }

    private void GetSubDepartments()
    {
        OrgDepartmentBOL objBOL = new OrgDepartmentBOL();

        objBOL.ParentDeptID = Util.ToInt(ddlDept.SelectedValue);
        DataTable dt = new OrgDepartmentsBAL().SelectAll(objBOL);
        if (Util.ToInt(ddlDept.SelectedValue) == 0)
            dt.Rows.Clear();
        ddlSubDept.DataSource = Util.ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlSubDept.DataBind();

    }

    #endregion

    private void GetAppraisalPeriodDetails()
    {
        if ("" + Request.QueryString["id"] == "") return;

        DataSet dSet = new AppraisalPeriodBAL().SelectAll(Util.ToInt(Request.QueryString["id"]));

        DataTable dTable = dSet.Tables[0];

        if (dTable.Rows.Count > 0)
        {
            DataRow dRow = dTable.Rows[0];
            if ("" + dRow["DepartmentID"] != "")
            {
                ddlDept.SelectedValue = "" + dRow["DepartmentID"];
                GetSubDepartments();
                GetBranches();
            }
            if ("" + dRow["SubDepartmentID"] != "")
            {
                ddlSubDept.SelectedValue = "" + dRow["SubDepartmentID"];
                GetBranches();
            }

            if ("" + dRow["DesignationID"] != "")
                ddlDesgn.SelectedValue = "" + dRow["DesignationID"];

            txtStart.SelectedCalendareDate = Util.ToDateTime(Util.RearrangeDateTime(dRow["StartDate"]));
            txtEnd.SelectedCalendareDate = Util.ToDateTime(Util.RearrangeDateTime(dRow["EndDate"]));

            txtDesc.Text = "" + dRow["DESCRIPTION"];

            ddlStatus.SelectedValue = "" + dRow["PeriodStatus"];
        }

        if (dSet.Tables[1].Rows.Count > 0)
        {
            DataTable dtBranch = dSet.Tables[1];
            for (int i = 0; i < dtBranch.Rows.Count; i++)
            {
                ListItem lItem = lstBranch.Items.FindByValue("" + dtBranch.Rows[i]["BranchID"]);
                if (lItem != null)
                    lItem.Selected = true;
            }
        }

        if (dSet.Tables[2].Rows.Count > 0)
        {
            DataTable dtEmp = dSet.Tables[2];
            gvEmpSelected.DataSource = dSet.Tables[2];
            gvEmpSelected.DataBind();

            if (dSet.Tables[2].Rows.Count == 0)
                lblTitle.Visible = false;
            else
                lblTitle.Visible = true;
        }

        if ("" + Request.QueryString["view"] == "1")
        {
            btnSave.Visible = false;
            btnSearch.Enabled = false;
            btnCancel.Visible = false;
            gvEmpSelected.Columns[1].Visible = false;
        }   
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AppraisalPeriodBOL objBol = new AppraisalPeriodBOL();

            objBol.AppPeriodID = Util.ToInt(lblAppPeriodID.Text);
            objBol.Description = txtDesc.Text.Trim();
            objBol.Designation.DesignationID = Util.ToInt(ddlDesgn.SelectedValue);
            objBol.Department.DeptID = Util.ToInt(ddlDept.SelectedValue);
            objBol.SubDepartment.DeptID = Util.ToInt(ddlSubDept.SelectedValue);
            objBol.StartDate = Util.ToDateTime(txtStart.getGregorianDateText);
            objBol.EndDate = Util.ToDateTime(txtEnd.getGregorianDateText);
            objBol.Status = "Y";
            objBol.CreatedBy = User.Identity.Name;
            objBol.PeriodStatus = ddlStatus.SelectedValue.ToString();

            objBol.AppPeriodID = new AppraisalPeriodBAL().Save(objBol);

            string sBranch = "";
            foreach (ListItem lItem in lstBranch.Items)
            {
                if (lItem.Selected)
                    sBranch += (sBranch == "") ? lItem.Value : "," + lItem.Value;
            }

            new AppraisalPeriodBAL().SaveBranch(objBol.AppPeriodID, sBranch, User.Identity.Name);

            string sEmp = "";
            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                string sTemp = ((Label)gRow.FindControl("lblEmpID")).Text;

                sEmp += (sEmp == "") ? sTemp : "," + sTemp;
            }
            new AppraisalPeriodBAL().SaveEmployees(objBol.AppPeriodID, sEmp, User.Identity.Name);

            lblMsg.Text = hrmlang.GetString("appraisalperiodsaved");
            if ("" + Request.QueryString["id"] == "")
                Clear();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblAppPeriodID.Text = "";
        txtDesc.Text = "";
        txtStart.ClearDate();
        txtEnd.ClearDate();
        ddlStatus.SelectedIndex = 0;
        lstBranch.ClearSelection();
        gvEmpSelected.DataSource = null;
        gvEmpSelected.DataBind();
        ViewState["EMPLIST"] = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();
        GetBranches();
    }

    protected void ddlSubDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranches();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        objBOL.EmpStatus = "C";

        if (ddlDept.SelectedIndex > 0)
            objBOL.DeptId = int.Parse(ddlDept.SelectedValue);
        if (ddlDesgn.SelectedIndex > 0)
            objBOL.DesgnID = int.Parse(ddlDesgn.SelectedValue);

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);

        string sBranch = "";
        int selected = 0;

        if (lstBranch.Items.Count > 0)
        {
            for (int i = 0; i < lstBranch.Items.Count; i++)
            {
                if (!lstBranch.Items[i].Selected)
                    sBranch = (sBranch == "") ? lstBranch.Items[i].Value : "," + lstBranch.Items[i].Value;
                else
                    selected = 1;
            }
        }

        if (selected == 1)
        {
            string[] sBR = sBranch.Split(',');
            foreach (string s in sBR)
            {
                DataRow[] dRows = dtEmp.Select("BranchID=" + s);
                for (int i = 0; i < dRows.Length; i++)
                    dtEmp.Rows.Remove(dRows[i]);
            }
        }

        DataView dView = new DataView(dtEmp);
        if (ddlSubDept.SelectedValue != "0")
            dView.RowFilter = "SubDeptID = " + ddlSubDept.SelectedValue;

        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvEmp').modal();", true);
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
    }

    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Search();
        gvEmployee.PageIndex = e.NewPageIndex;
    }

    protected void btnSaveEmp_Click(object sender, EventArgs e)
    {
        DataTable dTable = new DataTable();
        dTable.Columns.Add("FirstName");
        dTable.Columns.Add("MiddleName");
        dTable.Columns.Add("LastName");
        dTable.Columns.Add("EmployeeID");

        if (ViewState["EMPLIST"] == null)
        {
            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
                dTable.Rows.Add(dRow);
            }

        }
        else
        {
            dTable = (DataTable)ViewState["EMPLIST"];
        }

        foreach (GridViewRow gRow in gvEmployee.Rows)
        {
            DataRow dRow = dTable.NewRow();
            dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
            dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
            dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
            dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
            if (((CheckBox)gRow.FindControl("chkSelect")).Checked)
                dTable.Rows.Add(dRow);
        }

        List<int> pos = new List<int>();
        for (int i = 0; i < dTable.Rows.Count - 1; i++)
            for (int j = i + 1; j < dTable.Rows.Count; j++)
                if ("" + dTable.Rows[i]["EmployeeID"] == "" + dTable.Rows[j]["EmployeeID"])
                    pos.Add(j);

        for (int i = pos.Count - 1; i >= 0; i--)
        {
            dTable.Rows.RemoveAt(i);
        }

        ViewState["EMPLIST"] = dTable;

        gvEmpSelected.DataSource = dTable;
        gvEmpSelected.DataBind();

        if (dTable.Rows.Count == 0)
            lblTitle.Visible = false;
        else
            lblTitle.Visible = true;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvEmp').modal();", true);
    }


    protected void gvEmpSelected_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }

    protected void gvEmpSelected_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            ViewState["EMPLIST"] = null;
            DataTable dTable = new DataTable();
            dTable.Columns.Add("FirstName");
            dTable.Columns.Add("MiddleName");
            dTable.Columns.Add("LastName");
            dTable.Columns.Add("EmployeeID");

            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                if (e.CommandArgument.ToString() == ((Label)gRow.FindControl("lblEmpID")).Text) continue;

                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
                dTable.Rows.Add(dRow);
            }

            if (dTable.Rows.Count == 0)
                lblTitle.Visible = false;
            else
                lblTitle.Visible = true;

            gvEmpSelected.DataSource = dTable;
            gvEmpSelected.DataBind();
        }
    }
}