using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Departments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newdepartment");
        txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterdepartmentcode"));
        txtDepartment.Attributes.Add("placeholder", hrmlang.GetString("enterdepartment"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "departments.aspx");
            GetCompanyDetails();
            GetDepartments();
            GetBranches();
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

        if (lblCompanyID.Text == "")
            Response.Redirect("Company.aspx");
    }

    private void GetDepartments()
    {
        OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
        ddlDept.DataSource = gvDepts.DataSource = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        gvDepts.DataBind();
        ddlDept.DataBind();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        ddlDept.Items.Insert(0, lstItem);
        ddlDept.ClearSelection();
    }

    private void GetBranches()
    {
        //ListItem lstItem = new ListItem("[SELECT]", "");

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        lstBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        lstBranch.DataBind();

        //ddlBranch.Items.Insert(0, lstItem);
    }

    protected void gvDepts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWBRANCH")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblDeptID.Text = e.CommandArgument.ToString();

            OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();

            DataTable dTBranch = objBAL.SelectBranchesByDepartmentID(Util.ToInt(lblDeptID.Text));
            string sBranches = "";
            foreach (DataRow DR in dTBranch.Rows)
            {
                ListItem lItem = lstBranch.Items.FindByValue("" + DR["BranchID"]);
                if (lItem != null)
                {
                    sBranches = (sBranches == "") ? lItem.Text : sBranches + "," + lItem.Text;
                }
            }
            txtBranches.Text = (sBranches == "") ? "No Branches" : sBranches;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvBranches').modal();", true);
        }
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblDeptID.Text = e.CommandArgument.ToString();
            txtCode.Text = Util.CleanString(row.Cells[1].Text);
            txtDepartment.Text = Util.CleanString(row.Cells[2].Text);

            for (int i = 0; i < lstBranch.Items.Count; i++)
                lstBranch.Items[i].Selected = false;

            ddlDept.Items.Clear();
            OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
            DataTable dTable = objBAL.SelectParentDepartments(Util.ToInt(lblDeptID.Text));
            DataRow dRow = dTable.NewRow();
            dRow["DepartmentID"] = "0";
            dRow["DepartmentName"] = hrmlang.GetString("select");
            dTable.Rows.InsertAt(dRow, 0);
            ddlDept.DataSource = dTable;
            ddlDept.DataBind();
            ddlDept.ClearSelection();
            if (((Label)row.FindControl("lblParentDeptID")).Text != "")
                ddlDept.SelectedValue = ((Label)row.FindControl("lblParentDeptID")).Text;
            else
                ddlDept.SelectedIndex = 0;

            DataTable dTBranch = objBAL.SelectBranchesByDepartmentID(Util.ToInt(lblDeptID.Text));
            foreach (DataRow DR in dTBranch.Rows)
            {
                ListItem lItem = lstBranch.Items.FindByValue("" + DR["BranchID"]);
                if (lItem != null)
                    lItem.Selected = true;
            }
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("departmentdeleted");
            GetDepartments();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
            OrgDepartmentBOL objBol = new OrgDepartmentBOL();

            objBol.DeptID = Util.ToInt(lblDeptID.Text);
            objBol.ParentDeptID = Util.ToInt(ddlDept.SelectedValue);
            objBol.CompanyID = Util.ToInt(lblCompanyID.Text);
            //objBol.BranchID = Util.ToInt(ddlBranch.SelectedValue);
            objBol.DeptCode = txtCode.Text.Trim();
            objBol.DepartmentName = txtDepartment.Text.Trim();
            objBol.CreatedBy = "";
            objBol.Branches = "";

            for (int i = 0; i < lstBranch.Items.Count; i++)
                if (lstBranch.Items[i].Selected)
                    objBol.Branches = (objBol.Branches == "") ? lstBranch.Items[i].Value : objBol.Branches + "," + lstBranch.Items[i].Value;

            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("departmentsaved");
            Clear();
            GetDepartments();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblDeptID.Text = "";
        txtCode.Text = "";
        txtDepartment.Text = "";
        lstBranch.ClearSelection();
        ddlDept.ClearSelection();
        //  ddlBranch.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvDepts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepts.PageIndex = e.NewPageIndex;
        GetDepartments();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetDepartments();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvDepts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("branch");
            e.Row.Cells[3].Text = hrmlang.GetString("parentdepartment");
            e.Row.Cells[1].Text = hrmlang.GetString("departmentcode");
            e.Row.Cells[2].Text = hrmlang.GetString("department");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDepts.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDepts.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDepts.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDepts.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletedepartment"));
            }
        }
    }
}