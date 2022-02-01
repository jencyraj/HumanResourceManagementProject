using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class TaxRule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("newtaxrule");
        txtSalaryFrom.Attributes.Add("placeholder", hrmlang.GetString("entersalaryfrom"));
        txtSalaryTo.Attributes.Add("placeholder", hrmlang.GetString("entersalaryto"));
        txtTaxPercentage.Attributes.Add("placeholder", hrmlang.GetString("entertaxpercentage"));
        txtExemptedTaxAmount.Attributes.Add("placeholder", hrmlang.GetString("enterexemptedtaxamount"));
        txtAdditionalTaxAmount.Attributes.Add("placeholder", hrmlang.GetString("enteradditionaltaxamount"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        lblErr.Text = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            BindGrid(null);
            ClearControls();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void gvTaxRule_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int nTaxRuleId = 0;
            LinkButton lnkEdit = (LinkButton)((GridView)sender).Rows[e.NewEditIndex].FindControl("lnkEdit");
            nTaxRuleId = Util.ToInt(lnkEdit.CommandArgument);
            LoadControl(nTaxRuleId);
            e.Cancel = true;
        }
        catch
        {
            lblErr.Text = hrmlang.GetString("recordnotfound");
        }
    }

    protected void gvTaxRule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                TaxRuleBAL objBAL = new TaxRuleBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblErr.Text = string.Empty;
                BindGrid(null);
                lblMsg.Text = hrmlang.GetString("taxruledeleted");
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("taxruledeleteerror");
            }
        }
    }

    protected void gvTaxRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTaxRule.PageIndex = e.NewPageIndex;
        BindGrid(null);
    }

    protected void gvTaxRule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("salfrom");
            e.Row.Cells[1].Text = hrmlang.GetString("salto");
            e.Row.Cells[2].Text = hrmlang.GetString("ptax");
            e.Row.Cells[3].Text = hrmlang.GetString("extaxamount");
            e.Row.Cells[4].Text = hrmlang.GetString("addtaxamount");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvTaxRule.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvTaxRule.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvTaxRule.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvTaxRule.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "M")
                e.Row.Cells[5].Text = "Male";
            else if (e.Row.Cells[5].Text == "F")
                e.Row.Cells[5].Text = "Female";
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletetaxrule"));
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                Save();
                BindGrid(null);
                lblErr.Text = string.Empty;
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("taxrulesaveerror");
            }
        }
        lblMsg.Text = hrmlang.GetString("taxrulesaved");
        ClearControls();
    }

    private void BindGrid(TaxRuleBOL objBOL)
    {
        gvTaxRule.DataSource = new TaxRuleBAL().SelectAll(objBOL);
        gvTaxRule.DataBind();
    }

    private void ClearControls()
    { 
        hfTaxRuleId.Value = "0";
        txtSalaryFrom.Text = string.Empty;
        txtSalaryTo.Text = string.Empty;
        txtTaxPercentage.Text = string.Empty;
        txtExemptedTaxAmount.Text = string.Empty;
        txtAdditionalTaxAmount.Text = string.Empty;
        ddlGender.ClearSelection();
    }

    private void LoadControl(int nTaxRuleId)
    {
        TaxRuleBOL objBOL = new TaxRuleBOL();
        objBOL = new TaxRuleBAL().SearchById(nTaxRuleId);
        if (objBOL != null)
        {
            hfTaxRuleId.Value = objBOL.TaxRuleId.ToString();
            txtSalaryFrom.Text = objBOL.SalaryFrom.ToString();
            txtSalaryTo.Text = objBOL.SalaryTo.ToString();
            txtTaxPercentage.Text = objBOL.TaxPercentage.ToString();
            txtExemptedTaxAmount.Text = objBOL.ExemptedTaxAmount.ToString();
            txtAdditionalTaxAmount.Text = objBOL.AdditionalTaxAmount.ToString();
            ddlGender.SelectedValue = objBOL.Gender.ToString();
        }
    }

    private void Save()
    {
        TaxRuleBAL objBAL = new TaxRuleBAL();
        TaxRuleBOL objBOL = new TaxRuleBOL();
        objBOL.TaxRuleId = Util.ToInt(hfTaxRuleId.Value);
        objBOL.SalaryFrom = Util.ToDecimal(txtSalaryFrom.Text);
        objBOL.SalaryTo = Util.ToDecimal(txtSalaryTo.Text);
        objBOL.TaxPercentage = Util.ToDecimal(txtTaxPercentage.Text);
        objBOL.ExemptedTaxAmount = Util.ToDecimal(txtExemptedTaxAmount.Text);
        objBOL.AdditionalTaxAmount = Util.ToDecimal(txtAdditionalTaxAmount.Text);
        objBOL.Gender = ddlGender.SelectedValue;
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        objBAL.Save(objBOL);
    }

}