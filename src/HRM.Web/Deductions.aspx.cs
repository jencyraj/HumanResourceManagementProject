using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Deductions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "deductions.aspx");
            SetLang();
            GetDeductions();
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void SetLang()
    {
        btnNew.Text = hrmlang.GetString("newdeduction");
        txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterdeductioncode"));
        txtDeduction.Attributes.Add("placeholder", hrmlang.GetString("enterdeductionname"));
        txtValue.Attributes.Add("placeholder", hrmlang.GetString("enterdeductionvalue"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
        ddlType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

    }

    private void GetDeductions()
    {
        DeductionBAL objBAL = new DeductionBAL();
        gvDeduction.DataSource = objBAL.SelectAll(0);
        gvDeduction.DataBind();

        dvTax.Visible = gvDeduction.Columns[4].Visible = ("" + Session["USETAX"] == "1") ? true : false;

    }

    protected void gvDeduction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblType = (Label)row.FindControl("lblType");
            Label lblTaxType = (Label)row.FindControl("lblTaxType");

            lblDedID.Text = e.CommandArgument.ToString();
            txtCode.Text = Util.CleanString(row.Cells[0].Text);
            txtDeduction.Text = Util.CleanString(row.Cells[1].Text);
            txtValue.Text = Util.CleanString(row.Cells[3].Text);
            ddlType.SelectedValue = lblType.Text;
            chkTax.Checked = (lblTaxType.Text == "Y") ? true : false;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            DeductionBAL objBAL = new DeductionBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("deductiondeleted");
            GetDeductions();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            DeductionBAL objBAL = new DeductionBAL();
            DeductionBOL objBol = new DeductionBOL();

            objBol.DedID = Util.ToInt(lblDedID.Text);
            objBol.DedCode = txtCode.Text.Trim();
            objBol.DeductionName = txtDeduction.Text.Trim();
            objBol.DedType = ddlType.SelectedValue;
            objBol.DedAmount = Util.ToDecimal(txtValue.Text.Trim());
            objBol.CreatedBy = User.Identity.Name;
            objBol.TaxExemption = (chkTax.Checked) ? "Y" : "N";
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("deductionsaved");
            Clear();
            GetDeductions();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblDedID.Text = "";
        txtCode.Text = "";
        txtDeduction.Text = "";
        ddlType.SelectedIndex = 0;
        txtValue.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
        chkTax.Checked = false;
    }
    protected void gvDeduction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeduction.PageIndex = e.NewPageIndex;
        GetDeductions();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvDeduction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("deductioncode");
            e.Row.Cells[1].Text = hrmlang.GetString("deductionname");
            e.Row.Cells[2].Text = hrmlang.GetString("deductiontype");
            e.Row.Cells[3].Text = hrmlang.GetString("deductionvalue");
            e.Row.Cells[4].Text = hrmlang.GetString("usefortaxexemption");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDeduction.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDeduction.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDeduction.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDeduction.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletededuction"));
            }
        }
    }
}