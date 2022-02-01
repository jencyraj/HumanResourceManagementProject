using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Roles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        txtRole.Attributes.Add("placeholder", hrmlang.GetString("enterrole"));
        btnNew.Text = hrmlang.GetString("newrole");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "roles.aspx");
            GetRoles();
        }

        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "roles.aspx");

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetRoles()
    {
        RoleBAL objBAL = new RoleBAL();
        gvRole.DataSource = objBAL.SelectAll(0);
        gvRole.DataBind();

    }

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblRoleID.Text = e.CommandArgument.ToString();
            txtRole.Text = Util.CleanString(row.Cells[0].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            RoleBAL objBAL = new RoleBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("roledelete");
            GetRoles();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            RoleBAL objBAL = new RoleBAL();
            RoleBOL objBol = new RoleBOL();

            objBol.RoleID = Util.ToInt(lblRoleID.Text);
            objBol.RoleName = txtRole.Text.Trim();
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("rolesaved");
            Clear();
            GetRoles();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblRoleID.Text = "";
        txtRole.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRole.PageIndex = e.NewPageIndex;
        GetRoles();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("role");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvRole.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvRole.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvRole.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvRole.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleterole"));
            }
        }
    }
}