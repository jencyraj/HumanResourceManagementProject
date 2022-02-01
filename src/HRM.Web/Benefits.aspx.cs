using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Benefits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        btnShow.Text = hrmlang.GetString("search");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "benefits.aspx");
        }

        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "benefits.aspx");


    }



    protected void gvPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("packname");
            e.Row.Cells[1].Text = hrmlang.GetString("availedby");
            e.Row.Cells[2].Text = hrmlang.GetString("empshare");
            e.Row.Cells[3].Text = hrmlang.GetString("orgshare");
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

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Button btnAdd = (Button)e.Row.FindControl("btnAdd");
            btnAdd.Text = hrmlang.GetString("add");
            Button btnReset = (Button)e.Row.FindControl("btnReset");
            btnReset.Text = hrmlang.GetString("cancel");

            if (ViewState["permissions"] != null)
            {
                string[] permissions = (string[])ViewState["permissions"];
                btnSave.Visible = gvPackage.ShowFooter = (permissions[0] == "Y") ? true : false;
                if (gvPackage.ShowFooter)
                {
                    DataView dView = new BenefitBAL().SelectBenefitType(0).DefaultView;
                    dView.RowFilter = "ActivePack='Y'";

                    DropDownList ddlBfType = (DropDownList)e.Row.FindControl("ddlBfType");
                    ddlBfType.Items.Clear();
                    ddlBfType.DataSource = dView.ToTable();
                    ddlBfType.DataTextField = "BFType";
                    ddlBfType.DataValueField = "BFTID";
                    ddlBfType.DataBind();

                    DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                    ddlType.Items.Clear();

                    ddlType.Items.Add(new ListItem(hrmlang.GetString("permonth"), "M"));
                    ddlType.Items.Add(new ListItem(hrmlang.GetString("peryear"), "Y"));
                }
            }
        }
    }

    private DataTable GetExistingRows()
    {
        DataTable dTable = new DataTable();
        dTable.Columns.Add("BFTID");
        dTable.Columns.Add("BFType");
        dTable.Columns.Add("availedby");
        dTable.Columns.Add("EmployeeAmount");
        dTable.Columns.Add("org_amount");

        for (int i = 0; i < gvPackage.Rows.Count; i++)
        {
            Label lblBFTID = (Label)gvPackage.Rows[i].FindControl("lblBFTID");
            if (lblBFTID.Text != "")
            {
                if (lblRowID.Text == i.ToString()) continue;
                DataRow dRow = dTable.NewRow();

                dRow["BFTID"] = ((Label)gvPackage.Rows[i].FindControl("lblBFTID")).Text;
                dRow["BFType"] = ((Label)gvPackage.Rows[i].FindControl("lblBFType")).Text;
                dRow["availedby"] = ((Label)gvPackage.Rows[i].FindControl("lblAvailed")).Text;
                dRow["EmployeeAmount"] = ((Label)gvPackage.Rows[i].FindControl("lblEmpShare")).Text;
                dRow["org_amount"] = ((Label)gvPackage.Rows[i].FindControl("lblOrgShare")).Text;
                dTable.Rows.Add(dRow);
            }
        }

        return dTable;
    }

    protected void gvPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ADDNEW":
                if (lblRowID.Text == "")
                {
                    DataTable dTable = GetExistingRows();
                    DataRow dRow = dTable.NewRow();
                    GridViewRow frow = gvPackage.FooterRow;
                    dRow["BFTID"] = ((DropDownList)frow.FindControl("ddlBfType")).SelectedValue;
                    dRow["BFType"] = ((DropDownList)frow.FindControl("ddlBfType")).SelectedItem.Text;
                    dRow["availedby"] = ((DropDownList)frow.FindControl("ddlType")).SelectedValue;
                    dRow["EmployeeAmount"] = ((TextBox)frow.FindControl("txtEmpShare")).Text;
                    dRow["org_amount"] = ((TextBox)frow.FindControl("txtOrgShare")).Text;
                    dTable.Rows.Add(dRow);
                    gvPackage.DataSource = dTable;
                    gvPackage.DataBind();
                }
                else
                {
                    int idx = Util.ToInt(lblRowID.Text);
                    Label lblAvailed = (Label)gvPackage.Rows[idx].FindControl("lblAvailed");
                    Label lblAvail = (Label)gvPackage.Rows[idx].FindControl("lblAvail");
                    lblAvail.Text = ((DropDownList)gvPackage.FooterRow.FindControl("ddlType")).SelectedItem.Text;
                    lblAvailed.Text = ((DropDownList)gvPackage.FooterRow.FindControl("ddlType")).SelectedValue;
                    ((Label)gvPackage.Rows[idx].FindControl("lblEmpShare")).Text = ((TextBox)gvPackage.FooterRow.FindControl("txtEmpShare")).Text;
                    ((Label)gvPackage.Rows[idx].FindControl("lblOrgShare")).Text = ((TextBox)gvPackage.FooterRow.FindControl("txtOrgShare")).Text;
                }

                // lblMsg.Text = hrmlang.GetString("datasaved");
                lblRowID.Text = "";
                ClearFooter();
                break;
            case "EDITBR":
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridViewRow ftrow = gvPackage.FooterRow;

                DropDownList ddlBfType = (DropDownList)ftrow.FindControl("ddlBfType");
                ddlBfType.ClearSelection();

                ListItem lItem = ddlBfType.Items.FindByValue(((Label)row.FindControl("lblBFTID")).Text);
                if (lItem != null)
                    lItem.Selected = true;

                ddlBfType.Enabled = false;

                DropDownList ddlType = (DropDownList)ftrow.FindControl("ddlType");
                ddlType.ClearSelection();
                ddlType.SelectedValue = ((Label)row.FindControl("lblAvailed")).Text;
                ((TextBox)ftrow.FindControl("txtEmpShare")).Text = ((Label)row.FindControl("lblEmpShare")).Text;
                ((TextBox)ftrow.FindControl("txtOrgShare")).Text = ((Label)row.FindControl("lblOrgShare")).Text;

                lblRowID.Text = row.RowIndex.ToString();
                break;
            case "DEL":
                GridViewRow grow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                lblRowID.Text = grow.RowIndex.ToString();
                new BenefitBAL().DeleteEmployeeBenefit(Util.ToInt(((Label)grow.FindControl("lblBFTID")).Text), Util.ToInt(hfEmployeeId.Value), User.Identity.Name);
                gvPackage.DataSource = GetExistingRows();
                gvPackage.DataBind();
                lblRowID.Text = "";
                lblMsg.Text = hrmlang.GetString("datadeleted");
                break;
            case "RESET":
                ClearFooter();
                break;
        }
    }

    private void ClearFooter()
    {
        ((DropDownList)gvPackage.FooterRow.FindControl("ddlBfType")).Enabled = true;
        ((DropDownList)gvPackage.FooterRow.FindControl("ddlBfType")).SelectedIndex = 0;
        ((DropDownList)gvPackage.FooterRow.FindControl("ddlType")).SelectedIndex = 0;
        ((TextBox)gvPackage.FooterRow.FindControl("txtEmpShare")).Text = "";
        ((TextBox)gvPackage.FooterRow.FindControl("txtOrgShare")).Text = "";
        lblRowID.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ClearControls()
    {
        txtEmp.Text = "";
        hfEmployeeId.Value = "";
        txtRemarks.Text = "";
        gvPackage.DataSource = null;
        gvPackage.DataBind();
        dvRemarks.Visible = btnSave.Visible = btnCancel.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bFlag = false;

        if (hfEmployeeId.Value != "")
        {

            BenefitBAL objBAL = new BenefitBAL();
            BenefitBOL objBOL = null;

            for (int i = 0; i < gvPackage.Rows.Count; i++)
            {
                objBOL = new BenefitBOL();
                objBOL.EmployeeID = Util.ToInt(hfEmployeeId.Value);
                objBOL.AdditionalInfo = txtRemarks.Text.Trim();
                objBOL.CreatedBy = User.Identity.Name;
                objBOL.Status = "Y";

                objBOL.BenefitTypeID = Util.ToInt(((Label)gvPackage.Rows[i].FindControl("lblBFTID")).Text);

                if (objBOL.BenefitTypeID == 0) continue;

                objBOL.AvailedBy = ((Label)gvPackage.Rows[i].FindControl("lblAvailed")).Text;
                objBOL.EmployeeShare = Util.ToDecimal(((Label)gvPackage.Rows[i].FindControl("lblEmpShare")).Text);
                objBOL.CompanyShare = Util.ToDecimal(((Label)gvPackage.Rows[i].FindControl("lblOrgShare")).Text);

                objBAL.SaveEmployeeBenefits(objBOL);
                bFlag = true;
            }

            if (bFlag)
            {
                ClearControls();
                lblMsg.Text = hrmlang.GetString("datasaved");
            }
        }
        else
        {
            lblErr.Text = hrmlang.GetString("selectemp");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] permissions = (string[])ViewState["permissions"];
        btnSave.Visible = (permissions[0] == "Y") ? true : false;

        if (btnSave.Visible)
        {
            btnCancel.Visible = true;
            dvRemarks.Visible = true;
            FillEmptyRow();
        }

        if (Util.ToInt(hfEmployeeId.Value) > 0)
        {
            FillData();
        }
    }

    private void FillData()
    {
        DataTable dT = new BenefitBAL().SelectEmployeeBenefit(Util.ToInt(hfEmployeeId.Value));
        gvPackage.DataSource = dT;
        gvPackage.DataBind();

        if (dT.Rows.Count == 0)
            FillEmptyRow();
        else
        {
            txtRemarks.Text = "" + dT.Rows[0]["AdditionalInfo"];
        }
    }

    private void FillEmptyRow()
    {
        DataTable dT = new DataTable();
        dT.Columns.Add("BFTID");
        dT.Columns.Add("BFType");
        dT.Columns.Add("AvailedBy");
        dT.Columns.Add("EmployeeAmount");
        dT.Columns.Add("Org_Amount");

        dT.Rows.Add("", "", "", "", "");
        gvPackage.DataSource = dT;
        gvPackage.DataBind();

        gvPackage.Rows[0].FindControl("lnkEdit").Visible = false;
        gvPackage.Rows[0].FindControl("lnkDelete").Visible = false;
    }
}