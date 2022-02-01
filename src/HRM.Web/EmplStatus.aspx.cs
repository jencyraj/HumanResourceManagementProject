using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class EmplStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newemployementstatus");
        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
            GetEmplStatus();
    }

    private void GetEmplStatus()
    {
        EmplStatusBAL objBAL = new EmplStatusBAL();
        gvEmplStatus.DataSource = objBAL.Select();
        gvEmplStatus.DataBind();
    }

    protected void gvEmplStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEmplStatus.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEmplStatus.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEmplStatus.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEmplStatus.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteempstatus"));
        }
    }

    protected void gvEmplStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblID.Text = e.CommandArgument.ToString();
            txtDesc.Text = Util.CleanString(row.Cells[0].Text);
        }

        if (e.CommandName.Equals("DEL"))
        {
            EmplStatusBAL objBAL = new EmplStatusBAL();
            objBAL.Delete(e.CommandArgument.ToString(),User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("empstatusdeleted");
            GetEmplStatus();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            EmplStatusBAL objBAL = new EmplStatusBAL();

            objBAL.Save(lblID.Text,txtDesc.Text.Trim(),User.Identity.Name);

            lblMsg.Text = hrmlang.GetString("empstatussaved");
            Clear();
            GetEmplStatus();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblID.Text = "";
        txtDesc.Text = "";
    }
    protected void gvEmplStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmplStatus.PageIndex = e.NewPageIndex;
        GetEmplStatus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
}