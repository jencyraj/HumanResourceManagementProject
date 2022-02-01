using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Identifiers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newdepartment");
        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtValue.Attributes.Add("placeholder", hrmlang.GetString("entervalue"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "identifiers.aspx");
            GetCompanyDetails();
            GetIdentifiers();
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
    }

    private void GetIdentifiers()
    {
        RegnTypesBAL objBAL = new RegnTypesBAL();
        gvIdentifiers.DataSource = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        gvIdentifiers.DataBind();

    }

    protected void gvIdentifiers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblIdentifierID.Text = e.CommandArgument.ToString();
            txtDesc.Text = Util.CleanString(row.Cells[0].Text);
            txtValue.Text = Util.CleanString(row.Cells[1].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            RegnTypesBAL objBAL = new RegnTypesBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("identifierdeleted");
            GetIdentifiers();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            RegnTypesBAL objBAL = new RegnTypesBAL();
            RegnTypesBOL objBol = new RegnTypesBOL();

            objBol.IdentifierID = Util.ToInt(lblIdentifierID.Text);
            objBol.CompanyID = Util.ToInt(lblCompanyID.Text);
            objBol.Description = txtDesc.Text.Trim();
            objBol.IdentifierValue = txtValue.Text.Trim();
            objBol.CreatedBy = "";
            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("identifiersaved");
            Clear();
            GetIdentifiers();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblIdentifierID.Text = "";
        txtDesc.Text = "";
        txtValue.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvIdentifiers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIdentifiers.PageIndex = e.NewPageIndex;
        GetIdentifiers(); 
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvIdentifiers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("value");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvIdentifiers.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvIdentifiers.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvIdentifiers.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvIdentifiers.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteidentifier"));
            }
        }
    }
}