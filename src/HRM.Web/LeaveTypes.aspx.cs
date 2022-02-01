using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class LeaveTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
       // btnNew.Text = hrmlang.GetString("newbranch");
        txtName.Attributes.Add("placeholder", hrmlang.GetString("enterleavename"));
        txtSName.Attributes.Add("placeholder", hrmlang.GetString("entershortname"));
        txtDays.Attributes.Add("placeholder", hrmlang.GetString("enternoofdays"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ddlCarryOver.Items.Clear();
            ddlCarryOver.Items.Add(new ListItem(hrmlang.GetString("yes"), "YES"));
            ddlCarryOver.Items.Add(new ListItem(hrmlang.GetString("no"), "NO"));
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "leavetypes.aspx");
            GetBranches();
            GetLeaveTypes();
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        OrganisationBAL objOrg = new OrganisationBAL();
        OrganisationBOL objBOL = objOrg.Select();
        if (objBOL != null)
        {
            ddlBranches.DataSource = objBAL.SelectAll(objBOL.CompanyID);
            ddlBranches.DataBind();
        }
    }

    private void GetLeaveTypes()
    {
        LeaveTypesBAL objBAL = new LeaveTypesBAL();
        gvLType.DataSource = objBAL.SelectAll();
        gvLType.DataBind();

    }

    protected void gvLType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblLeaveTypeID.Text = e.CommandArgument.ToString();
            txtName.Text = Util.CleanString(row.Cells[0].Text);
            txtSName.Text = Util.CleanString(row.Cells[1].Text);
            ddlLeaveType.SelectedValue = ((Label)row.FindControl("lblTypeCode")).Text;
            txtDays.Text = Util.CleanString(row.Cells[3].Text);
            ddlDeduction.SelectedValue = row.Cells[5].Text;
            ddlCarryOver.SelectedValue = Util.CleanString(row.Cells[4].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            LeaveTypesBAL objBAL = new LeaveTypesBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("leavetypedeleted");
            GetLeaveTypes();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LeaveTypesBAL objBAL = new LeaveTypesBAL();
            LeaveTypesBOL objBol = new LeaveTypesBOL();

            objBol.LeaveTypeID = Util.ToInt(lblLeaveTypeID.Text);
            objBol.BranchID = Util.ToInt(ddlBranches.SelectedValue);
            objBol.CarryOver = ddlCarryOver.SelectedValue;
            objBol.CreatedBy = User.Identity.Name;
            objBol.LeaveDays = Util.ToInt(txtDays.Text.Trim());
            objBol.LeaveName = txtName.Text.Trim();
            objBol.LeaveType = Util.ToInt(ddlLeaveType.SelectedValue);
            objBol.Deduction = ddlDeduction.SelectedValue;
            objBol.ShortName = txtSName.Text.Trim();
            objBol.Status = "Y";
            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("leavetypesaved");
            Clear();
            GetLeaveTypes();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblLeaveTypeID.Text = "";
        txtDays.Text = "";
        txtName.Text = "";
        txtSName.Text = "";
        ddlBranches.SelectedIndex = 0;
        ddlLeaveType.SelectedIndex = 0;
        ddlCarryOver.SelectedIndex = 0;
        ddlDeduction.SelectedIndex = 0;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvLType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLType.PageIndex = e.NewPageIndex;
        GetLeaveTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvLType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("leavename");
            e.Row.Cells[1].Text = hrmlang.GetString("shortname");
            e.Row.Cells[2].Text = hrmlang.GetString("leavetypes");
            e.Row.Cells[3].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[4].Text = hrmlang.GetString("carryover");
            e.Row.Cells[5].Text = hrmlang.GetString("leavded");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvLType.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvLType.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvLType.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvLType.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteleavetype"));
            }
        }
    }
}