using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newuser");
        txtUserID.Attributes.Add("placeholder", hrmlang.GetString("enteruserid"));
        txtPwd.Attributes.Add("placeholder", hrmlang.GetString("enterpassword"));
        txtBiometricId.Attributes.Add("placeholder", hrmlang.GetString("enterbiometricid"));
        txtIrisId.Attributes.Add("placeholder", hrmlang.GetString("enteririsid"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "users.aspx");
            GetRoles();
            GetUsers();
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;     
    }

    private void GetRoles()
    {
        RoleBAL objBAL = new RoleBAL();
        ddlRole.DataSource = objBAL.SelectAll(0);
        ddlRole.DataBind();

    }

    private void GetUsers()
    {
        UserBAL objBAL = new UserBAL();
        gvUsers.DataSource = objBAL.SelectAll("");
        gvUsers.DataBind();

    }

    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblUID.Text = e.CommandArgument.ToString();
            txtUserID.Text = row.Cells[0].Text;
            txtBiometricId.Text = row.Cells[2].Text != "&nbsp;" ? row.Cells[2].Text : "";
            txtIrisId.Text = row.Cells[3].Text != "&nbsp;" ? row.Cells[3].Text : "";
            string Password = ((Label)row.FindControl("lblPWD")).Text;
            ddlRole.SelectedValue = ((Label)row.FindControl("lblRoleID")).Text;
            txtPwd.Attributes.Add("value", Password);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            UserBAL objBAL = new UserBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("userdeleted");
            GetUsers();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;
       
        try
        {
            UserBAL objBAL = new UserBAL();
            UserBOL objBol = new UserBOL();

            objBol.UID = Util.ToInt(lblUID.Text);
            objBol.RoleID = Util.ToInt(ddlRole.SelectedValue);
            objBol.UserID = txtUserID.Text.Trim();
            objBol.Password = txtPwd.Text.Trim();
            objBol.BiometricID = txtBiometricId.Text.ToString();
            objBol.IRISID = txtIrisId.Text.ToString();
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("usraddsucess");
            Clear();
            GetUsers();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblUID.Text = "";
        txtUserID.Text = "";
        txtPwd.Attributes.Add("value", "");
        txtBiometricId.Text = "";
        txtIrisId.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        GetUsers();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("userid");
            e.Row.Cells[1].Text = hrmlang.GetString("role");
            e.Row.Cells[2].Text = hrmlang.GetString("biometricid");
            e.Row.Cells[3].Text = hrmlang.GetString("irisid");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvUsers.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvUsers.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvUsers.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvUsers.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteuser"));
            }
        }
    }
}