using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class EduLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newrole");
        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtOrder.Attributes.Add("placeholder", hrmlang.GetString("entersortorder"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
            GetEduLevel();
    }

    private void GetEduLevel()
    {
        EduLevelBAL objBAL = new EduLevelBAL();
        gvEduLevel.DataSource = objBAL.Select();
        gvEduLevel.DataBind();
    }

    protected void gvEduLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("educationlevel");
            e.Row.Cells[1].Text = hrmlang.GetString("sortorder");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEduLevel.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEduLevel.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEduLevel.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEduLevel.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteedulevel"));
        }
    }

    protected void gvEduLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblID.Text = e.CommandArgument.ToString();
            txtDesc.Text = Util.CleanString(row.Cells[0].Text);
            txtOrder.Text = Util.CleanString(row.Cells[1].Text);
        }

        if (e.CommandName.Equals("DEL"))
        {
            EduLevelBAL objBAL = new EduLevelBAL();
            objBAL.Delete(e.CommandArgument.ToString(),User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("eduleveldeleted");
            GetEduLevel();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            EduLevelBAL objBAL = new EduLevelBAL();

            objBAL.Save(lblID.Text,txtDesc.Text.Trim(),txtOrder.Text.Trim(),User.Identity.Name);

            lblMsg.Text = hrmlang.GetString("edulevelsaved");
            Clear();
            GetEduLevel();
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
        txtOrder.Text = "";
    }
    protected void gvEduLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEduLevel.PageIndex = e.NewPageIndex;
        GetEduLevel();
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