using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Allowances : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";


        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "allowances.aspx");
            SetLang();
            GetAllowances();
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void SetLang()
    {

        btnNew.Text = hrmlang.GetString("newallowance");
        txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterallowancecode"));
        txtAllowance.Attributes.Add("placeholder", hrmlang.GetString("enterallowancename"));
        txtValue.Attributes.Add("placeholder", hrmlang.GetString("enterallowancevalue"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem(hrmlang.GetString("amount"), "A"));
        ddlType.Items.Add(new ListItem(hrmlang.GetString("percentage"), "P"));

        ddTaxable.Items.Clear();
        ddTaxable.Items.Add(new ListItem(hrmlang.GetString("no"), "N"));
        ddTaxable.Items.Add(new ListItem(hrmlang.GetString("yes"), "Y"));
    }

    private void GetAllowances()
    {
        AllowanceBAL objBAL = new AllowanceBAL();
        gvAllowance.DataSource = objBAL.SelectAll(0);
        gvAllowance.DataBind();

        dvTax.Visible = gvAllowance.Columns[4].Visible = ("" + Session["USETAX"] == "1") ? true : false;

    }

    protected void gvAllowance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblType = (Label)row.FindControl("lblType");
            Label lblTaxableValue = (Label)row.FindControl("lblTaxableValue");
            lblAlwID.Text = e.CommandArgument.ToString();
            txtCode.Text = Util.CleanString(row.Cells[0].Text);
            txtAllowance.Text = Util.CleanString(row.Cells[1].Text);
            txtValue.Text = Util.CleanString(row.Cells[3].Text);
            ddlType.SelectedValue = lblType.Text;
            ddTaxable.SelectedValue = lblTaxableValue.Text;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            AllowanceBAL objBAL = new AllowanceBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("allowancedeleted");
            GetAllowances();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AllowanceBAL objBAL = new AllowanceBAL();
            AllowanceBOL objBol = new AllowanceBOL();
            objBol.AlwID = Util.ToInt(lblAlwID.Text);
            objBol.AlwCode = txtCode.Text.Trim();
            objBol.AllowanceName = txtAllowance.Text.Trim();
            objBol.AlwType = ddlType.SelectedValue;
            objBol.AlwAmount = Util.ToDecimal(txtValue.Text.Trim());
            objBol.CreatedBy = User.Identity.Name;
            objBol.Taxable = ddTaxable.SelectedValue;
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("allowancesaved");
            Clear();
            GetAllowances();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblAlwID.Text = "";
        txtCode.Text = "";
        txtAllowance.Text = "";
        ddlType.SelectedIndex = 0;
        ddTaxable.SelectedIndex = 0;
        txtValue.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvAllowance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllowance.PageIndex = e.NewPageIndex;
        GetAllowances();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("allowancecode");
            e.Row.Cells[1].Text = hrmlang.GetString("allowancename");
            e.Row.Cells[2].Text = hrmlang.GetString("allowancetype");
            e.Row.Cells[3].Text = hrmlang.GetString("allowancevalue");
            e.Row.Cells[4].Text = hrmlang.GetString("taxable");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAllowance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAllowance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAllowance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAllowance.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteallowance"));
            }
        }
    }
}