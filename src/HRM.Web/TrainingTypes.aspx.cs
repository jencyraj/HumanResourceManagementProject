using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class TrainingTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        txtDescription.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        btnNew.Text = hrmlang.GetString("newtrainingtype");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "trainingtypes.aspx");
            GetTrainingTypes();
        }
       
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;        
    }

    private void GetTrainingTypes()
    {
        TrainingTypeBAL objBAL = new TrainingTypeBAL();
        gvTrainingTypes.DataSource = objBAL.SelectAll(0);
        gvTrainingTypes.DataBind();

    }

    protected void gvTrainingTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblTID.Text = e.CommandArgument.ToString();
            txtDescription.Text = Util.CleanString(row.Cells[0].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            TrainingTypeBAL objBAL = new TrainingTypeBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("trainingtypedelete");
            GetTrainingTypes();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            TrainingTypeBAL objBAL = new TrainingTypeBAL();
            TrainingTypeBOL objBol = new TrainingTypeBOL();

            objBol.TID = Util.ToInt(lblTID.Text);
            objBol.Description = txtDescription.Text.Trim();
            objBol.CreatedBy = User.Identity.Name;
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("trainingtypesaved");
            Clear();
            GetTrainingTypes();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblTID.Text = "";
        txtDescription.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvTrainingTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTrainingTypes.PageIndex = e.NewPageIndex;
        GetTrainingTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvTrainingTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvTrainingTypes.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvTrainingTypes.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvTrainingTypes.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvTrainingTypes.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletetrainingtype"));
            }
        }
    }
}