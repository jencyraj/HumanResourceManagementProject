using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class BenefitPackages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterpacktype"));
        btnNew.Text = hrmlang.GetString("addnew");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "benefitpackages.aspx");
            GetPackages();
        }

        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "benefitpackages.aspx");

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetPackages()
    {
        BenefitBAL objBAL = new BenefitBAL();
        gvPackage.DataSource = objBAL.SelectBenefitType(0);
        gvPackage.DataBind();

    }

    protected void gvPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblBFTID.Text = e.CommandArgument.ToString();
            txtDesc.Text = Util.CleanString(row.Cells[0].Text);
            chkActive.Checked = (((Label)row.FindControl("lblActive")).Text == "Y") ? true : false;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            BenefitBAL objBAL = new BenefitBAL();
            objBAL.DeleteBenefitType(Util.ToInt(e.CommandArgument),User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("datadeleted");
            GetPackages();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            BenefitBAL objBAL = new BenefitBAL();
            BenefitBOL objBol = new BenefitBOL();

            objBol.BenefitTypeID = Util.ToInt(lblBFTID.Text);
            objBol.BenefitType = txtDesc.Text.Trim();
            objBol.ActivePack = (chkActive.Checked) ? "Y" : "N";
            objBol.CreatedBy = User.Identity.Name;
            objBAL.SaveBenefitType(objBol);
            lblMsg.Text = hrmlang.GetString("datasaved");
            Clear();
            GetPackages();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblBFTID.Text = "";
        txtDesc.Text = "";
        chkActive.Checked = false;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPackage.PageIndex = e.NewPageIndex;
        GetPackages();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("packname");
            e.Row.Cells[1].Text = hrmlang.GetString("active");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvPackage.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvPackage.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvPackage.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvPackage.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            }
        }
    }
}