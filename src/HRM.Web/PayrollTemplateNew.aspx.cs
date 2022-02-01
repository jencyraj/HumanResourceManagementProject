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

public partial class PayrollTemplateNew : System.Web.UI.Page
{
    static int PMId;
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
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                BindGrids(0);
            }
            else
            {
                PMId = Util.ToInt(Request.QueryString["id"]);
                BindGrids(PMId);
            }

            SetLangData();
        }


        string parameter = Request["__EVENTARGUMENT"];

        if (parameter == "CALCULATE")
        {

            Get_change_taxrules();
        }

    }

    private void SetLangData()
    {
        txtDesignation.Attributes.Add("placeholder", hrmlang.GetString("enterdesg"));
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremp"));
        txtBasicSalary.Attributes.Add("placeholder", hrmlang.GetString("enterbasic"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
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
            DropDownList ddchkTaxExemption = (DropDownList)(e.Row.FindControl("ddchkTaxExemption"));
            HiddenField hfTaxExemption = (HiddenField)(e.Row.FindControl("hfTaxExemption"));

            ddchkTaxExemption.Items.Clear();
            ddchkTaxExemption.Items.Add(new ListItem(hrmlang.GetString("yes"), "Y"));
            ddchkTaxExemption.Items.Add(new ListItem(hrmlang.GetString("no"), "N"));
            ddchkTaxExemption.SelectedValue = hfTaxExemption.Value;
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
            DropDownList ddchkPRTaxExemption = (DropDownList)(e.Row.FindControl("ddchkPRTaxExemption"));
            HiddenField hfPRTaxExemption = (HiddenField)(e.Row.FindControl("hfPRTaxExemption"));
            ddchkPRTaxExemption.Items.Clear();
            ddchkPRTaxExemption.Items.Add(new ListItem(hrmlang.GetString("yes"), "Y"));
            ddchkPRTaxExemption.Items.Add(new ListItem(hrmlang.GetString("no"), "N"));
            ddchkPRTaxExemption.SelectedValue = hfPRTaxExemption.Value;
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
            HiddenField hf = (HiddenField)(e.Row.FindControl("hfGender"));

            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem(hrmlang.GetString("male"), "M"));
            ddlGender.Items.Add(new ListItem(hrmlang.GetString("female"), "F"));

            ddlGender.SelectedValue = hf.Value;
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
                lblDsgnReq.Text = string.Empty;
                lblBscSlrReq.Text = string.Empty;
                lblgender.Text = "";
                int saved = SavePayrollTemplate();
                if (saved == -1)
                {
                    lblErr.Text = "Payroll template defined";
                    return;
                }
            }
            catch
            {
                lblErr.Text = "Some error occured. Payroll not saved properly";
            }
        }
        Response.Redirect("PayrollTemplates.aspx");
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
            BindPayroll(ds);
            gvPayrollTaxrules.DataSource = ds.Tables[3];
            gvPayrollTaxrules.DataBind();
            Get_change_taxrules();
        }
    }
    private void BindPayroll(DataSet DS)
    {
        DataTable DTallwnc = DS.Tables[1];
        DataTable DTded = DS.Tables[2];
        //gvPayrollAllowance.DataSource = new AllowanceBAL().SelectAll(0);
        //gvPayrollAllowance.DataBind();
        //gvPayrollDeductions.DataSource = new DeductionBAL().SelectAll(0);
        //gvPayrollDeductions.DataBind();
        for (int i = 0; i < gvPayrollAllowance.Rows.Count; i++)
        {
            GridViewRow gRow = gvPayrollAllowance.Rows[i];
            CheckBox chkPallw = ((CheckBox)gRow.FindControl("chkPallw"));
            HiddenField hfPRAlwId = ((HiddenField)gRow.FindControl("hfPRAlwId"));
            DataRow[] Rrow = DTallwnc.Select("AllowanceId=" + hfPRAlwId.Value + "AND status='Y'");
            if (Rrow.Length > 0)
            {
                chkPallw.Checked = true;

            }
        }
        for (int i = 0; i < gvPayrollDeductions.Rows.Count; i++)
        {
            GridViewRow gRow = gvPayrollDeductions.Rows[i];
            CheckBox chkPded = ((CheckBox)gRow.FindControl("chkPded"));
            HiddenField hfPRDedId = ((HiddenField)gRow.FindControl("hfPRDedId"));
            DataRow[] Rrow = DTded.Select("DeductionId=" + hfPRDedId.Value + "AND status='Y'");
            if (Rrow.Length > 0)
            {
                chkPded.Checked = true;

            }
        }
    }

    private void Get_change_taxrules()
    {
        lblSalary.InnerText = txtBasicSalary.Text.Trim();
        decimal dAlw = 0; decimal dtax = 0;
        foreach (GridViewRow gRow in gvPayrollAllowance.Rows)
        {
            TextBox txtPRAlwAmount = (TextBox)gRow.FindControl("txtPRAlwAmount");
            DropDownList ddlPRAlwType = (DropDownList)gRow.FindControl("ddlPRAlwType");
            DropDownList ddlPRTaxable = (DropDownList)gRow.FindControl("ddlPRTaxable");
            CheckBox chkPallw = (CheckBox)gRow.FindControl("chkPallw");
            if (chkPallw.Checked)
            {
                if (ddlPRAlwType.SelectedValue == "A")
                    dAlw += Util.ToDecimal(txtPRAlwAmount.Text);
                else
                    dAlw += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtPRAlwAmount.Text)) / 100;

                if (ddlPRTaxable.SelectedValue == "Y")

                    dtax = dAlw + Util.ToDecimal(txtBasicSalary.Text.Trim());
            }
        }

        lblTotAlw.InnerText = Math.Round(dAlw, 2).ToString();

        decimal dDed = 0;
        foreach (GridViewRow gRow in gvPayrollDeductions.Rows)
        {
            TextBox txtPRDedAmount = (TextBox)gRow.FindControl("txtPRDedAmount");
            DropDownList ddlPRDedType = (DropDownList)gRow.FindControl("ddlPRDedType");
            DropDownList chkPRTaxExemption = (DropDownList)gRow.FindControl("ddchkPRTaxExemption");
            CheckBox chkPded = (CheckBox)gRow.FindControl("chkPded");
            if (chkPded.Checked)
            {
                if (ddlPRDedType.SelectedValue == "A")
                    dDed += Util.ToDecimal(txtPRDedAmount.Text);
                else
                    dDed += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtPRDedAmount.Text)) / 100;
                if (chkPRTaxExemption.SelectedItem.Text == "Yes")

                    dtax = Math.Abs(Util.ToDecimal(txtBasicSalary.Text.Trim()) - dDed);
            }
        }

        decimal dFTax = 0; decimal dMTax = 0;
        foreach (GridViewRow gRows in gvPayrollTaxrules.Rows)
        {
            TextBox txtSalaryFrom = (TextBox)gRows.FindControl("txtPRSalaryFrom");
            TextBox txtSalaryTo = (TextBox)gRows.FindControl("txtPRSalaryTo");
            TextBox txtTaxPercentage = (TextBox)gRows.FindControl("txtPRTaxPercentage");
            TextBox txtExemptedTaxAmount = (TextBox)gRows.FindControl("txtPRExemptedTaxAmount");
            DropDownList ddlPRGender = (DropDownList)gRows.FindControl("ddlPRGender");
            decimal totearnings = 0;
            totearnings = Math.Round(dtax, 2) * 12;
            if (totearnings >= Util.ToDecimal(txtSalaryFrom.Text) && totearnings <= Util.ToDecimal(txtSalaryTo.Text))
            {
                if (ddlPRGender.SelectedValue == "F")
                {
                    dFTax = ((totearnings) - (Util.ToDecimal((txtExemptedTaxAmount.Text))));
                    dFTax = (dFTax * (Util.ToDecimal(txtTaxPercentage.Text)) / 100);
                    dFTax = dFTax / 12;
                }

                if (ddlPRGender.SelectedValue == "M")
                {
                    dMTax = ((totearnings) - (Util.ToDecimal((txtExemptedTaxAmount.Text))));
                    dMTax = (dMTax * (Util.ToDecimal(txtTaxPercentage.Text)) / 100);
                    dMTax = dMTax / 12;
                }
            }

        }
        lblTotDed.InnerText = Math.Round(dDed, 2).ToString();
        if (lblgender.Text == "F")
        {
            lblFTax.InnerText = Math.Round(dFTax, 2).ToString();
            lblMTax.InnerText = "0";
        }
        if (lblgender.Text == "M")
        {
            lblFTax.InnerText = "0";
            lblMTax.InnerText = Math.Round(dMTax, 2).ToString();
        }
        lblFNetSalary.InnerText = Convert.ToString(Math.Round(Util.ToDecimal(txtBasicSalary.Text.Trim()) + dAlw - (dDed - dFTax), 2));
        lblMNetSalary.InnerText = Convert.ToString(Math.Round(Util.ToDecimal(txtBasicSalary.Text.Trim()) + dAlw - (dDed - dMTax), 2));
    }
    private void Get_change_taxrules_add()
    {
        lblSalary.InnerText = txtBasicSalary.Text.Trim();
        decimal dAlw = 0; decimal dtax = 0;
        foreach (GridViewRow gRow in gvAllowances.Rows)
        {
            TextBox txtAlwAmount = (TextBox)gRow.FindControl("txtAlwAmount");
            DropDownList ddlAlwType = (DropDownList)gRow.FindControl("ddlAlwType");
            DropDownList ddlTaxable = (DropDownList)gRow.FindControl("ddlTaxable");
            CheckBox chkallw = (CheckBox)gRow.FindControl("chkallw");
            if (chkallw.Checked)
            {
                if (ddlAlwType.SelectedValue == "A")
                    dAlw += Util.ToDecimal(txtAlwAmount.Text);
                else
                    dAlw += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtAlwAmount.Text)) / 100;

                if (ddlTaxable.SelectedValue == "Y")

                    dtax = dAlw + Util.ToDecimal(txtBasicSalary.Text.Trim());
            }
        }

        lblTotAlw.InnerText = Math.Round(dAlw, 2).ToString();

        decimal dDed = 0;
        foreach (GridViewRow gRow in gvDeductions.Rows)
        {
            TextBox txtDedAmount = (TextBox)gRow.FindControl("txtDedAmount");
            DropDownList ddlDedType = (DropDownList)gRow.FindControl("ddlDedType");
            DropDownList ddchkTaxExemption = (DropDownList)gRow.FindControl("ddchkTaxExemption");
            CheckBox chkded = (CheckBox)gRow.FindControl("chkded");
            if (chkded.Checked)
            {
                if (ddlDedType.SelectedValue == "A")
                    dDed += Util.ToDecimal(txtDedAmount.Text);
                else
                    dDed += (Util.ToDecimal(txtBasicSalary.Text.Trim()) * Util.ToDecimal(txtDedAmount.Text)) / 100;
                if (ddchkTaxExemption.SelectedItem.Text == "Yes")

                    dtax = Math.Abs(Util.ToDecimal(txtBasicSalary.Text.Trim()) - dDed);
            }
        }

        decimal dFTax = 0; decimal dMTax = 0;
        foreach (GridViewRow gRows in gvTaxRules.Rows)
        {
            TextBox txtSalaryFrom = (TextBox)gRows.FindControl("txtSalaryFrom");
            TextBox txtSalaryTo = (TextBox)gRows.FindControl("txtSalaryTo");
            TextBox txtTaxPercentage = (TextBox)gRows.FindControl("txtTaxPercentage");
            TextBox txtExemptedTaxAmount = (TextBox)gRows.FindControl("txtExemptedTaxAmount");
            DropDownList ddlGender = (DropDownList)gRows.FindControl("ddlGender");
            decimal totearnings = 0;
            totearnings = Math.Round(dtax, 2) * 12;
            if (totearnings >= Util.ToDecimal(txtSalaryFrom.Text) && totearnings <= Util.ToDecimal(txtSalaryTo.Text))
            {
                if (ddlGender.SelectedValue == "F")
                {
                    dFTax = ((totearnings) - (Util.ToDecimal((txtExemptedTaxAmount.Text))));
                    dFTax = (dFTax * (Util.ToDecimal(txtTaxPercentage.Text)) / 100);
                    dFTax = dFTax / 12;
                }

                if (ddlGender.SelectedValue == "M")
                {
                    dMTax = ((totearnings) - (Util.ToDecimal((txtExemptedTaxAmount.Text))));
                    dMTax = (dMTax * (Util.ToDecimal(txtTaxPercentage.Text)) / 100);
                    dMTax = dMTax / 12;
                }
            }

        }
        lblTotDed.InnerText = Math.Round(dDed, 2).ToString();
        if (lblgender.Text == "F")
        {
            lblFTax.InnerText = Math.Round(dFTax, 2).ToString();
            lblMTax.InnerText = "0";
        }
        if (lblgender.Text == "M")
        {
            lblFTax.InnerText = "0";
            lblMTax.InnerText = Math.Round(dMTax, 2).ToString();
        }
        lblFNetSalary.InnerText = Convert.ToString(Math.Round(Util.ToDecimal(txtBasicSalary.Text.Trim()) + dAlw - (dDed - dFTax), 2));
        lblMNetSalary.InnerText = Convert.ToString(Math.Round(Util.ToDecimal(txtBasicSalary.Text.Trim()) + dAlw - (dDed - dMTax), 2));
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
        // hfGender.Value = "" + dr["Gender"];
        lblgender.Text = hfGender.Value;
    }

    private int SavePayrollTemplate()
    {
        PayrollMasterBOL objBOL = new PayrollMasterBOL();
        objBOL.PMId = Util.ToInt(hfPMId.Value);
        objBOL.DesignationId = Util.ToInt(hfDesignationId.Value);
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.PPId = Util.ToInt(ddPP.SelectedValue);
        objBOL.BasicSalary = Util.ToDecimal(txtBasicSalary.Text);
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;

        if (hfPMId.Value == "0")
        {
            List<PayrollAllowanceBOL> lstAlwBOL = new List<PayrollAllowanceBOL>();
            foreach (GridViewRow row in gvAllowances.Rows)
            {
                CheckBox chkallw = (CheckBox)row.FindControl("chkallw");
                if (chkallw.Checked)
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

                    AlwBOL.CreatedBy = User.Identity.Name;
                    AlwBOL.ModifiedBy = User.Identity.Name;
                    lstAlwBOL.Add(AlwBOL);
                }

            }
            objBOL.PayrollAllowances = lstAlwBOL;
            List<PayrollDeductionBOL> lstDedBOL = new List<PayrollDeductionBOL>();
            foreach (GridViewRow row in gvDeductions.Rows)
            {
                CheckBox chkded = (CheckBox)row.FindControl("chkded");
                if (chkded.Checked)
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
                    DropDownList ddchkTaxExemption = (DropDownList)row.FindControl("ddchkTaxExemption");
                    DedBOL.TaxExemption = ddchkTaxExemption.SelectedValue;
                    DedBOL.CreatedBy = User.Identity.Name;
                    DedBOL.ModifiedBy = User.Identity.Name;
                    lstDedBOL.Add(DedBOL);
                }

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
           // new PayrollTemplateBAL().Delete(Util.ToInt(hfPMId.Value));
            List<PayrollAllowanceBOL> lstAlwBOL = new List<PayrollAllowanceBOL>();
            foreach (GridViewRow row in gvPayrollAllowance.Rows)
            {
                CheckBox chkPallw = (CheckBox)row.FindControl("chkPallw");
                if (chkPallw.Checked)
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
            }
            objBOL.PayrollAllowances = lstAlwBOL;
            List<PayrollDeductionBOL> lstDedBOL = new List<PayrollDeductionBOL>();

            foreach (GridViewRow row in gvPayrollDeductions.Rows)
            {
                CheckBox chkPded = (CheckBox)row.FindControl("chkPded");
                if (chkPded.Checked)
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
                    DropDownList ddchkPRTaxExemption = (DropDownList)row.FindControl("ddchkPRTaxExemption");
                    DedBOL.TaxExemption = ddchkPRTaxExemption.SelectedValue;
                    DedBOL.Status = "Y";
                    DedBOL.CreatedBy = User.Identity.Name;
                    DedBOL.ModifiedBy = User.Identity.Name;
                    lstDedBOL.Add(DedBOL);
                }
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


    protected void ddltaxable_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_change_taxrules();

    }
    protected void chkPRTaxExemption_CheckedChanged(object sender, EventArgs e)
    {
        Get_change_taxrules();
    }

    protected void btnCalculate_Click(object sendr, EventArgs e)
    {

        Get_change_taxrules();
    }
    protected void ddchkTaxExemption_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_change_taxrules();
    }
    protected void ddchkPRTaxExemption_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_change_taxrules();
    }

    protected void chkHallw_CheckedChanged(object sender, EventArgs e)
    {
        Get_change_taxrules_add();
    }

    protected void chkallw_CheckedChanged(object sender, EventArgs e)
    {
        Get_change_taxrules_add();
    }
}
