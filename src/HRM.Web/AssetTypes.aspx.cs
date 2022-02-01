using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AssetTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newassettype");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        txtAssetType.Attributes.Add("placeholder", hrmlang.GetString("enterassettype"));
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "assettypes.aspx");
            GetAssetTypes();
        }
       
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;        
    }

    private void GetAssetTypes()
    {
        AssetTypesBAL objBAL = new AssetTypesBAL();
        gvAssetType.DataSource = objBAL.SelectAll(0);
        gvAssetType.DataBind();

    }

    protected void gvAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
          
            lblAssetID.Text = e.CommandArgument.ToString();           
            txtAssetType.Text = Util.CleanString(row.Cells[0].Text);
         
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            AssetTypesBAL objBAL = new AssetTypesBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("assettypedeleted");
            GetAssetTypes();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            AssetTypesBAL objBAL = new AssetTypesBAL();
            AssetTypesBOL objBol = new AssetTypesBOL();

            objBol.AssetTypeID = Util.ToInt(lblAssetID.Text);
            objBol.AssetType = txtAssetType.Text.Trim();            
            objBol.CreatedBy = User.Identity.Name;
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("assettypesaved");
            Clear();
            GetAssetTypes();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblAssetID.Text = "";
        txtAssetType.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvAssetType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssetType.PageIndex = e.NewPageIndex;
        GetAssetTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvAssetType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("assettypename");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAssetType.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAssetType.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAssetType.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAssetType.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteassettype"));
            }
        }
    }
}