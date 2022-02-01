using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Permissions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            GetRoles();
            BindLanguageDD();

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
        }

    }

    private void GetRoles()
    {
        RoleBAL objBAL = new RoleBAL();
        ddlRole.DataSource = objBAL.SelectAll(0);
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    private void BindLanguageDD()
    {
        DataTable dt = new LangDataBAL().SelectLanguage(0);
        DataView dView = dt.DefaultView;
        dView.RowFilter = "Active='Y'";
        ddlLang.DataSource = dView.ToTable();
        ddlLang.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            PermissionBAL objBAL = new PermissionBAL();
            PermissionBOL objBOL = new PermissionBOL();

            objBOL.RoleID = Util.ToInt(ddlRole.SelectedValue);

            foreach (GridViewRow gRow in gvPermissions.Rows)
            {
                objBOL.ModuleID = Util.ToInt(((Label)gRow.FindControl("lblModuleID")).Text);
                objBOL.EmpID = Util.ToInt(hfEmployeeId.Value);
                objBOL.AllowInsert = (((CheckBox)gRow.FindControl("chkAdd")).Checked) ? "Y" : "N";
                objBOL.AllowUpdate = (((CheckBox)gRow.FindControl("chkEdit")).Checked) ? "Y" : "N";
                objBOL.AllowDelete = (((CheckBox)gRow.FindControl("chkDelete")).Checked) ? "Y" : "N";
                objBOL.AllowView = (((CheckBox)gRow.FindControl("chkView")).Checked) ? "Y" : "N";
                objBAL.Save(objBOL);
            }
            lblMsg.Text = "Permissions saved successfully";
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblRoleID.Text = "";
        ddlRole.SelectedValue = "";
        gvPermissions.DataSource = null;
        gvPermissions.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void gvPermissions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPermissions.PageIndex = e.NewPageIndex;
        BindModuleGrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedValue == "")
        {
            Clear();
        }
        else
        {
            btnSave.Visible = true;
            btnCancel.Visible = true;
            BindModuleGrid();

            txtEmployee.Text = "";
            hfEmployeeId.Value = "";
        }
    }

    private void BindModuleGrid()
    {
        PermissionBAL objBAL = new PermissionBAL();
        DataSet dSet = objBAL.SelectAll(Util.ToInt(ddlRole.SelectedValue), Util.ToInt(hfEmployeeId.Value), ddlLang.SelectedValue);
        DataTable dt = dSet.Tables[0];
        dt.DefaultView.RowFilter = "ParentModuleID>0";
        ViewState["PERMISSIONS"] = dSet.Tables[1];

        gvPermissions.DataSource = dt;
        gvPermissions.DataBind();

        if (dt.Rows.Count == 0)
        {
            btnSave.Visible = false;
        }
    }

    protected void gvPermissions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("module");
            ((CheckBox)e.Row.Cells[1].FindControl("chkHAdd")).Text = hrmlang.GetString("insert");
            ((CheckBox)e.Row.Cells[2].FindControl("chkHEdit")).Text = hrmlang.GetString("edit");
            ((CheckBox)e.Row.Cells[3].FindControl("chkHDelete")).Text = hrmlang.GetString("delete");
            ((CheckBox)e.Row.Cells[4].FindControl("chkHView")).Text = hrmlang.GetString("view");

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblModuleID = (Label)e.Row.FindControl("lblModuleID");
            CheckBox chkAdd = (CheckBox)e.Row.FindControl("chkAdd");
            CheckBox chkEdit = (CheckBox)e.Row.FindControl("chkEdit");
            CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
            CheckBox chkView = (CheckBox)e.Row.FindControl("chkView");

            DataTable dt = (DataTable)ViewState["PERMISSIONS"];

            foreach (DataRow dRow in dt.Rows)
            {
                if (lblModuleID.Text == dRow["ModuleID"].ToString())
                {
                    if ("" + dRow["AllowInsert"] == "Y")
                        chkAdd.Checked = true;
                    if ("" + dRow["AllowUpdate"] == "Y")
                        chkEdit.Checked = true;
                    if ("" + dRow["AllowDelete"] == "Y")
                        chkDelete.Checked = true;
                    if ("" + dRow["AllowView"] == "Y")
                        chkView.Checked = true;
                }
            }
        }
    }

    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnCancel.Visible = true;
        BindModuleGrid();
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnCancel.Visible = true;
        BindModuleGrid();
    }
}