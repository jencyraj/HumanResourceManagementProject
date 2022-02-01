using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HRM.BAL;
using HRM.BOL;

public partial class PayrollTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        lblDsgnReq.Text = string.Empty;
        lblBscSlrReq.Text = string.Empty;
        if (!IsPostBack)
        {
            hfPMId.Value = "0";
            hfDesignationId.Value = "0";
            hfEmployeeId.Value = "0";
            BindPPDD();
            SetLangData();
            ddlGosiAlw.DataSource = new AllowanceBAL().SelectAll(0);
            ddlGosiAlw.DataTextField = "AllowanceName";
            ddlGosiAlw.DataValueField = "AlwID";
            ddlGosiAlw.DataBind();
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                BindGrids(0);
            }
            else
            {
                int PMId = Util.ToInt(Request.QueryString["id"]);
                BindGrids(PMId);
            }

        }
    }

    private void SetLangData()
    {
        txtDesignation.Attributes.Add("placeholder", hrmlang.GetString("enterdesg"));
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremp"));
        txtBasicSalary.Attributes.Add("placeholder", hrmlang.GetString("enterbasic"));
        txtGOSI.Attributes.Add("placeholder", hrmlang.GetString("gosipercent"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        ddlgosi.Items.Clear();
        ddlgosi.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
        ddlgosi.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));
    }

    protected void gvAllowances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("amount");
            e.Row.Cells[2].Text = hrmlang.GetString("type");
            e.Row.Cells[3].Text = hrmlang.GetString("taxable");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlAlwType = (DropDownList)(e.Row.FindControl("ddlAlwType"));

            ddlAlwType.Items.Clear();
            ddlAlwType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
            ddlAlwType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

            HiddenField hfAlwType = (HiddenField)(e.Row.FindControl("hfAlwType"));
            ddlAlwType.SelectedValue = hfAlwType.Value;
            DropDownList ddlTaxable = (DropDownList)(e.Row.FindControl("ddlTaxable"));
            HiddenField hfTaxable = (HiddenField)(e.Row.FindControl("hfTaxable"));

            ddlTaxable.Items.Clear();
            ddlTaxable.Items.Add(new ListItem(hrmlang.GetString("yes"), "Y"));
            ddlTaxable.Items.Add(new ListItem(hrmlang.GetString("no"), "N"));
            ddlTaxable.SelectedValue = hfTaxable.Value;
        }
    }

    protected void gvPayrollAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("amount");
            e.Row.Cells[2].Text = hrmlang.GetString("type");
            e.Row.Cells[3].Text = hrmlang.GetString("taxable");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPRAlwType = (DropDownList)(e.Row.FindControl("ddlPRAlwType"));
            HiddenField hfPRAlwType = (HiddenField)(e.Row.FindControl("hfPRAlwType"));

            ddlPRAlwType.Items.Clear();
            ddlPRAlwType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
            ddlPRAlwType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

            ddlPRAlwType.SelectedValue = hfPRAlwType.Value;
            DropDownList ddlPRTaxable = (DropDownList)(e.Row.FindControl("ddlPRTaxable"));
            HiddenField hfPRTaxable = (HiddenField)(e.Row.FindControl("hfPRTaxable"));

            ddlPRTaxable.Items.Clear();
            ddlPRTaxable.Items.Add(new ListItem(hrmlang.GetString("yes"), "Y"));
            ddlPRTaxable.Items.Add(new ListItem(hrmlang.GetString("no"), "N"));
            ddlPRTaxable.SelectedValue = hfPRTaxable.Value;
        }
    }

    protected void gvDeductions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("amount");
            e.Row.Cells[2].Text = hrmlang.GetString("type");
            e.Row.Cells[3].Text = hrmlang.GetString("taxex");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlAlwType = (DropDownList)(e.Row.FindControl("ddlDedType"));
            HiddenField hfDedType = (HiddenField)(e.Row.FindControl("hfDedType"));
            ddlAlwType.Items.Clear();
            ddlAlwType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
            ddlAlwType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

            ddlAlwType.SelectedValue = hfDedType.Value;
            CheckBox chkTaxExemption = (CheckBox)(e.Row.FindControl("chkTaxExemption"));
            HiddenField hfTaxExemption = (HiddenField)(e.Row.FindControl("hfTaxExemption"));
            if (hfTaxExemption.Value == "Y")
            {
                chkTaxExemption.Checked = true;
            }
            else if (hfTaxExemption.Value == "N")
            {
                chkTaxExemption.Checked = false;
            }
        }
    }

    protected void gvPayrollDeductions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("amount");
            e.Row.Cells[2].Text = hrmlang.GetString("type");
            e.Row.Cells[3].Text = hrmlang.GetString("taxex");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPRDedType = (DropDownList)(e.Row.FindControl("ddlPRDedType"));
            HiddenField hfPRDedType = (HiddenField)(e.Row.FindControl("hfPRDedType"));

            ddlPRDedType.Items.Clear();
            ddlPRDedType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
            ddlPRDedType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

            ddlPRDedType.SelectedValue = hfPRDedType.Value;
            CheckBox chkPRTaxExemption = (CheckBox)(e.Row.FindControl("chkPRTaxExemption"));
            HiddenField hfPRTaxExemption = (HiddenField)(e.Row.FindControl("hfPRTaxExemption"));
            if (hfPRTaxExemption.Value == "Y")
            {
                chkPRTaxExemption.Checked = true;
            }
            else if (hfPRTaxExemption.Value == "N")
            {
                chkPRTaxExemption.Checked = false;
            }
        }
    }

    protected void gvTaxRules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("salfrom");
            e.Row.Cells[1].Text = hrmlang.GetString("salto");
            e.Row.Cells[2].Text = hrmlang.GetString("ptax");
            e.Row.Cells[3].Text = hrmlang.GetString("extaxamount");
            e.Row.Cells[5].Text = hrmlang.GetString("gender");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlGender = (DropDownList)(e.Row.FindControl("ddlGender"));
            HiddenField hfGender = (HiddenField)(e.Row.FindControl("hfGender"));

            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem(hrmlang.GetString("male"), "M"));
            ddlGender.Items.Add(new ListItem(hrmlang.GetString("female"), "F"));

            ddlGender.SelectedValue = hfGender.Value;
        }
    }

    protected void gvPayrollTaxrules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPRGender = (DropDownList)(e.Row.FindControl("ddlPRGender"));
            HiddenField hfPRGender = (HiddenField)(e.Row.FindControl("hfPRGender"));

            ddlPRGender.Items.Clear();
            ddlPRGender.Items.Add(new ListItem(hrmlang.GetString("male"), "M"));
            ddlPRGender.Items.Add(new ListItem(hrmlang.GetString("female"), "F"));

            ddlPRGender.SelectedValue = hfPRGender.Value;
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                if (txtEmployee.Text.Trim() == "")
                    hfEmployeeId.Value = "";
                lblDsgnReq.Text = string.Empty;
                lblBscSlrReq.Text = string.Empty;
                int saved = SavePayrollTemplate();
                if (saved == -1)
                {
                    lblErr.Text = "Payroll template defined";
                    return;
                }
                lblMsg.Text = hrmlang.GetString("payrolltemplatesaved");
            }
            catch
            {
                lblErr.Text = "Some error occured. Payroll not saved properly";
            }
        }
        if (e.CommandName == "CANCEL")
        {
            Response.Redirect("PayrollTemplates.aspx");
        }

        //  Response.Redirect("PayrollTemplates.aspx");
    }

    private void BindPPDD()
    {
        ddPP.DataSource = new PayrollPeriodBAL().SelectAll(new PayrollPeriodBOL() { PPId = 0, Year = null });
        ddPP.DataValueField = "PPId";
        ddPP.DataTextField = "Title";
        ddPP.DataBind();
    }

    private void BindGrids(int PMId)
    {
        if (PMId == 0)
        {
            LoadDefaultControls();
        }
        else
        {
            DataSet ds = new DataSet();
            PayrollMasterBOL objBOL = new PayrollMasterBOL();
            objBOL.PMId = PMId;
            ds = new PayrollTemplateBAL().SelectAll(objBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LoadMasterControls(ds.Tables[0].Rows[0]);
            }
            lblAllowances.Visible = false;
            lblPayrollAllowance.Visible = true;
            lblDeductions.Visible = false;
            lblPayrollDeductions.Visible = true;
            lblTaxRules.Visible = false;
            lblPayrollTaxrules.Visible = true;
            gvAllowances.Visible = false;
            gvDeductions.Visible = false;
            gvTaxRules.Visible = false;
            gvPayrollAllowance.Visible = true;
            gvPayrollDeductions.Visible = true;
            gvPayrollTaxrules.Visible = true;
            gvPayrollAllowance.DataSource = ds.Tables[1];
            gvPayrollAllowance.DataBind();
            gvPayrollDeductions.DataSource = ds.Tables[2];
            gvPayrollDeductions.DataBind();
            gvPayrollTaxrules.DataSource = ds.Tables[3];
            gvPayrollTaxrules.DataBind();

            lblSalary.InnerText = txtBasicSalary.Text.Trim();
            decimal dAlw = 0;
            foreach (GridViewRow gRow in gvPayrollAllowance.Rows)
            {
                TextBox txtPRAlwAmount = (TextBox)gRow.FindControl("txtPRAlwAmount");
                DropDownList ddlPRAlwType = (DropDownList)gRow.FindControl("ddlPRAlwType");

                if (ddlPRAlwType.SelectedValue == "A")
                    dAlw += Util.ToDecimal(txtPRAlwAmount.Text);
                else
                    dAlw += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtPRAlwAmount.Text)) / 100;
            }

            lblTotAlw.InnerText = Math.Round(dAlw, 2).ToString();

            decimal dDed = 0;
            foreach (GridViewRow gRow in gvPayrollDeductions.Rows)
            {
                TextBox txtPRDedAmount = (TextBox)gRow.FindControl("txtPRDedAmount");
                DropDownList ddlPRDedType = (DropDownList)gRow.FindControl("ddlPRDedType");

                if (ddlPRDedType.SelectedValue == "A")
                    dDed += Util.ToDecimal(txtPRDedAmount.Text);
                else
                    dDed += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtPRDedAmount.Text)) / 100;
            }

            lblTotDed.InnerText = Math.Round(dDed, 2).ToString();

            lblFNetSalary.InnerText = Convert.ToString(Math.Round(Util.ToDecimal(txtBasicSalary.Text.Trim()) + dAlw - dDed, 2));
        }
    }

    private void LoadDefaultControls()
    {
        hfPMId.Value = "0";
        hfDesignationId.Value = "0";
        hfEmployeeId.Value = "0";
        txtDesignation.Text = string.Empty;
        txtEmployee.Text = string.Empty;
        txtBasicSalary.Text = string.Empty;
        ddPP.ClearSelection();
        lblAllowances.Visible = true;
        lblPayrollAllowance.Visible = false;
        lblDeductions.Visible = true;
        lblPayrollDeductions.Visible = false;
        lblTaxRules.Visible = true;
        lblPayrollTaxrules.Visible = false;
        gvPayrollAllowance.Visible = false;
        gvPayrollDeductions.Visible = false;
        gvPayrollTaxrules.Visible = false;
        gvAllowances.Visible = true;
        gvDeductions.Visible = true;
        gvTaxRules.Visible = true;
        gvAllowances.DataSource = new AllowanceBAL().SelectAll(0);
        gvAllowances.DataBind();
        gvDeductions.DataSource = new DeductionBAL().SelectAll(0);
        gvDeductions.DataBind();
        TaxRuleBOL txBOL = new TaxRuleBOL();
        txBOL.TaxRuleId = 0;
        gvTaxRules.DataSource = new TaxRuleBAL().SelectAll(txBOL);
        gvTaxRules.DataBind();
    }

    private void LoadMasterControls(DataRow dr)
    {
        hfPMId.Value = dr["PMId"].ToString();
        txtDesignation.Text = dr["Designation"].ToString();
        hfDesignationId.Value = dr["DesignationId"].ToString();
        txtEmployee.Text = dr["FirstName"].ToString() + ((dr["MiddleName"].ToString() != "") ? " " + dr["MiddleName"].ToString() : "") + ((dr["LastName"].ToString() != "") ? " " + dr["LastName"].ToString() : "");
        hfEmployeeId.Value = dr["EmployeeId"].ToString();
        ddPP.SelectedValue = dr["PPId"].ToString();
        txtBasicSalary.Text = dr["BasicSalary"].ToString();

        ddlgosi.Items.Clear();
        ddlgosi.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
        ddlgosi.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

        txtGOSI.Text = "" + dr["gosiamount"];
        ddlgosi.SelectedValue = "" + dr["gositype"];

        ListItem lItem = ddlGosiAlw.Items.FindByValue("" + dr["gosi_alw_type"]);
        if (lItem != null)
            lItem.Selected = true;
        //  hfGender.Value = dr["Gender"].ToString();
    }

    private int SavePayrollTemplate()
    {
        PayrollMasterBOL objBOL = new PayrollMasterBOL();
        objBOL.PMId = Util.ToInt(hfPMId.Value);
        objBOL.DesignationId = Util.ToInt(hfDesignationId.Value);
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.PPId = Util.ToInt(ddPP.SelectedValue);
        objBOL.BasicSalary = Util.ToDecimal(txtBasicSalary.Text);
        objBOL.gosiamount = Util.ToDecimal(txtGOSI.Text.Trim());
        objBOL.gositype = ddlgosi.SelectedValue;
        if (ddlGosiAlw.Items.Count > 0)
            objBOL.gosialwtype = Util.ToInt(ddlGosiAlw.SelectedValue);
        else
            objBOL.gosialwtype = 0;
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        if (hfPMId.Value == "0")
        {
            List<PayrollAllowanceBOL> lstAlwBOL = new List<PayrollAllowanceBOL>();
            foreach (GridViewRow row in gvAllowances.Rows)
            {
                PayrollAllowanceBOL AlwBOL = new PayrollAllowanceBOL();
                AlwBOL.PAId = 0;
                HiddenField hfAlwId = (HiddenField)row.FindControl("hfAlwId");
                AlwBOL.AllowanceId = Util.ToInt(hfAlwId.Value);
                HiddenField hfAlwCode = (HiddenField)row.FindControl("hfAlwCode");
                AlwBOL.AlwCode = hfAlwCode.Value.ToString();
                Label lblAlwName = (Label)row.FindControl("lblAlwName");
                AlwBOL.AlwName = lblAlwName.Text.ToString();
                TextBox txtAlwAmount = (TextBox)row.FindControl("txtAlwAmount");
                AlwBOL.AlwAmount = Util.ToDecimal(txtAlwAmount.Text);
                DropDownList ddlAlwType = (DropDownList)row.FindControl("ddlAlwType");
                AlwBOL.AlwType = ddlAlwType.SelectedValue;
                DropDownList ddlTaxable = (DropDownList)row.FindControl("ddlTaxable");
                AlwBOL.Taxable = ddlTaxable.SelectedValue;
                AlwBOL.Status = "Y";
                AlwBOL.CreatedBy = User.Identity.Name;
                AlwBOL.ModifiedBy = User.Identity.Name;
                lstAlwBOL.Add(AlwBOL);
            }
            objBOL.PayrollAllowances = lstAlwBOL;
            List<PayrollDeductionBOL> lstDedBOL = new List<PayrollDeductionBOL>();
            foreach (GridViewRow row in gvDeductions.Rows)
            {
                PayrollDeductionBOL DedBOL = new PayrollDeductionBOL();
                DedBOL.PDId = 0;
                HiddenField hfDedId = (HiddenField)row.FindControl("hfDedId");
                DedBOL.DeductionId = Util.ToInt(hfDedId.Value);
                HiddenField hfDedCode = (HiddenField)row.FindControl("hfDedCode");
                DedBOL.DedCode = hfDedCode.Value.ToString();
                Label lblDedName = (Label)row.FindControl("lblDedName");
                DedBOL.DedName = lblDedName.Text.ToString();
                TextBox txtDedAmount = (TextBox)row.FindControl("txtDedAmount");
                DedBOL.DedAmount = Util.ToDecimal(txtDedAmount.Text);
                DropDownList ddlDedType = (DropDownList)row.FindControl("ddlDedType");
                DedBOL.DedType = ddlDedType.SelectedValue;
                CheckBox chkTaxExemption = (CheckBox)row.FindControl("chkTaxExemption");
                DedBOL.TaxExemption = chkTaxExemption.Checked ? "Y" : "N";
                DedBOL.Status = "Y";
                DedBOL.CreatedBy = User.Identity.Name;
                DedBOL.ModifiedBy = User.Identity.Name;
                lstDedBOL.Add(DedBOL);
            }
            objBOL.PayrollDeductions = lstDedBOL;
            List<PayrollTaxRuleBOL> lstTXBOL = new List<PayrollTaxRuleBOL>();
            foreach (GridViewRow row in gvTaxRules.Rows)
            {
                PayrollTaxRuleBOL TXBOL = new PayrollTaxRuleBOL();
                TXBOL.PTId = 0;
                HiddenField hfTaxRuleId = (HiddenField)row.FindControl("hfTaxRuleId");
                TXBOL.TaxRuleId = Util.ToInt(hfTaxRuleId.Value);
                TextBox txtSalaryFrom = (TextBox)row.FindControl("txtSalaryFrom");
                TXBOL.SalaryFrom = Util.ToDecimal(txtSalaryFrom.Text);
                TextBox txtSalaryTo = (TextBox)row.FindControl("txtSalaryTo");
                TXBOL.SalaryTo = Util.ToDecimal(txtSalaryTo.Text);
                TextBox txtTaxPercentage = (TextBox)row.FindControl("txtTaxPercentage");
                TXBOL.TaxPercentage = Util.ToDecimal(txtTaxPercentage.Text);
                TextBox txtExemptedTaxAmount = (TextBox)row.FindControl("txtExemptedTaxAmount");
                TXBOL.ExemptedTaxAmount = Util.ToDecimal(txtExemptedTaxAmount.Text);
                TextBox txtAdditionalTaxAmount = (TextBox)row.FindControl("txtAdditionalTaxAmount");
                TXBOL.AdditionalTaxAmount = Util.ToDecimal(txtAdditionalTaxAmount.Text);
                DropDownList ddlGender = (DropDownList)row.FindControl("ddlGender");
                TXBOL.Gender = ddlGender.SelectedValue;
                TXBOL.Status = "Y";
                TXBOL.CreatedBy = User.Identity.Name;
                TXBOL.ModifiedBy = User.Identity.Name;
                lstTXBOL.Add(TXBOL);
            }
            objBOL.PayrollTaxRules = lstTXBOL;
        }
        else
        {
            List<PayrollAllowanceBOL> lstAlwBOL = new List<PayrollAllowanceBOL>();
            foreach (GridViewRow row in gvPayrollAllowance.Rows)
            {
                PayrollAllowanceBOL AlwBOL = new PayrollAllowanceBOL();
                HiddenField hfPRPAId = (HiddenField)row.FindControl("hfPRPAId");
                AlwBOL.PAId = Util.ToInt(hfPRPAId.Value);
                HiddenField hfPRPMId = (HiddenField)row.FindControl("hfPRPMId");
                AlwBOL.PMId = Util.ToInt(hfPRPMId.Value);
                HiddenField hfPRAlwId = (HiddenField)row.FindControl("hfPRAlwId");
                AlwBOL.AllowanceId = Util.ToInt(hfPRAlwId.Value);
                HiddenField hfPRAlwCode = (HiddenField)row.FindControl("hfPRAlwCode");
                AlwBOL.AlwCode = hfPRAlwCode.Value.ToString();
                Label lblPRAlwName = (Label)row.FindControl("lblPRAlwName");
                AlwBOL.AlwName = lblPRAlwName.Text.ToString();
                TextBox txtPRAlwAmount = (TextBox)row.FindControl("txtPRAlwAmount");
                AlwBOL.AlwAmount = Util.ToDecimal(txtPRAlwAmount.Text);
                DropDownList ddlPRAlwType = (DropDownList)row.FindControl("ddlPRAlwType");
                AlwBOL.AlwType = ddlPRAlwType.SelectedValue;
                DropDownList ddlPRTaxable = (DropDownList)row.FindControl("ddlPRTaxable");
                AlwBOL.Taxable = ddlPRTaxable.SelectedValue;
                AlwBOL.Status = "Y";
                AlwBOL.CreatedBy = User.Identity.Name;
                AlwBOL.ModifiedBy = User.Identity.Name;
                lstAlwBOL.Add(AlwBOL);
            }
            objBOL.PayrollAllowances = lstAlwBOL;
            List<PayrollDeductionBOL> lstDedBOL = new List<PayrollDeductionBOL>();
            foreach (GridViewRow row in gvPayrollDeductions.Rows)
            {
                PayrollDeductionBOL DedBOL = new PayrollDeductionBOL();
                HiddenField hfPRPDId = (HiddenField)row.FindControl("hfPRPDId");
                DedBOL.PDId = Util.ToInt(hfPRPDId.Value);
                HiddenField hfPRDedId = (HiddenField)row.FindControl("hfPRDedId");
                DedBOL.DeductionId = Util.ToInt(hfPRDedId.Value);
                HiddenField hfPRDedCode = (HiddenField)row.FindControl("hfPRDedCode");
                DedBOL.DedCode = hfPRDedCode.Value.ToString();
                Label lblPRDedName = (Label)row.FindControl("lblPRDedName");
                DedBOL.DedName = lblPRDedName.Text.ToString();
                TextBox txtPRDedAmount = (TextBox)row.FindControl("txtPRDedAmount");
                DedBOL.DedAmount = Util.ToDecimal(txtPRDedAmount.Text);
                DropDownList ddlPRDedType = (DropDownList)row.FindControl("ddlPRDedType");
                DedBOL.DedType = ddlPRDedType.SelectedValue;
                CheckBox chkPRTaxExemption = (CheckBox)row.FindControl("chkPRTaxExemption");
                DedBOL.TaxExemption = chkPRTaxExemption.Checked ? "Y" : "N";
                DedBOL.Status = "Y";
                DedBOL.CreatedBy = User.Identity.Name;
                DedBOL.ModifiedBy = User.Identity.Name;
                lstDedBOL.Add(DedBOL);
            }
            objBOL.PayrollDeductions = lstDedBOL;
            /*  List<PayrollTaxRuleBOL> lstTXBOL = new List<PayrollTaxRuleBOL>();
              foreach (GridViewRow row in gvPayrollTaxrules.Rows)
              {
                  PayrollTaxRuleBOL TXBOL = new PayrollTaxRuleBOL();
                  HiddenField hfPRPTId = (HiddenField)row.FindControl("hfPRPTId");
                  TXBOL.PTId = Util.ToInt(hfPRPTId.Value);
                  HiddenField hfPRPMId = (HiddenField)row.FindControl("hfPRPMId");
                  TXBOL.PMId = Util.ToInt(hfPRPMId.Value);
                  HiddenField hfPRTaxRuleId = (HiddenField)row.FindControl("hfPRTaxRuleId");
                  TXBOL.TaxRuleId = Util.ToInt(hfPRTaxRuleId.Value);
                  TextBox txtPRSalaryFrom = (TextBox)row.FindControl("txtPRSalaryFrom");
                  TXBOL.SalaryFrom = Util.ToDecimal(txtPRSalaryFrom.Text);
                  TextBox txtPRSalaryTo = (TextBox)row.FindControl("txtPRSalaryTo");
                  TXBOL.SalaryTo = Util.ToDecimal(txtPRSalaryTo.Text);
                  TextBox txtPRTaxPercentage = (TextBox)row.FindControl("txtPRTaxPercentage");
                  TXBOL.TaxPercentage = Util.ToDecimal(txtPRTaxPercentage.Text);
                  TextBox txtPRExemptedTaxAmount = (TextBox)row.FindControl("txtPRExemptedTaxAmount");
                  TXBOL.ExemptedTaxAmount = Util.ToDecimal(txtPRExemptedTaxAmount.Text);
                  TextBox txtPRAdditionalTaxAmount = (TextBox)row.FindControl("txtPRAdditionalTaxAmount");
                  TXBOL.AdditionalTaxAmount = Util.ToDecimal(txtPRAdditionalTaxAmount.Text);
                  DropDownList ddlPRGender = (DropDownList)row.FindControl("ddlPRGender");
                  TXBOL.Gender = ddlPRGender.SelectedValue;
                  TXBOL.Status = "Y";
                  TXBOL.CreatedBy = User.Identity.Name;
                  TXBOL.ModifiedBy = User.Identity.Name;
                  lstTXBOL.Add(TXBOL);
              }
              objBOL.PayrollTaxRules = lstTXBOL;*/
        }
        PayrollTemplateBAL objBAL = new PayrollTemplateBAL();
        return objBAL.Save(objBOL);
    }

}
