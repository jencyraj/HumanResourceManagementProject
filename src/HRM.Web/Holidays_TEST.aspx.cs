using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using HRM.BAL;
using HRM.BOL;

public partial class Holidays_TEST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            GetBranches();
            //if (Session["BRANCHID"] != null && ("" + Session["ROLEID"]) == "4")
            //{
            //    ddlBr.ClearSelection();
            //    ddlBr.SelectedValue = Util.ToInt(Session["BRANCHID"]).ToString();
            //    ddlBr.Enabled = false;
            //    pnlNew.Visible = false;
            //}
            LoadCalendar(DateTime.Today.Year.ToString());
            GetHolidays();
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "holidays.aspx");
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void LoadCalendar(string Year)
    {
        dtJan.TodaysDate = DateTime.Parse("01/01/" + Year.ToString());
        dtFeb.TodaysDate = DateTime.Parse("02/01/" + Year.ToString());
        dtMarch.TodaysDate = DateTime.Parse("03/01/" + Year.ToString());
        dtApril.TodaysDate = DateTime.Parse("04/01/" + Year.ToString());
        dtMay.TodaysDate = DateTime.Parse("05/01/" + Year.ToString());
        dtJune.TodaysDate = DateTime.Parse("06/01/" + Year.ToString());
        dtJuly.TodaysDate = DateTime.Parse("07/01/" + Year.ToString());
        dtAug.TodaysDate = DateTime.Parse("08/01/" + Year.ToString());
        dtSep.TodaysDate = DateTime.Parse("09/01/" + Year.ToString());
        dtOct.TodaysDate = DateTime.Parse("10/01/" + Year.ToString());
        dtNov.TodaysDate = DateTime.Parse("11/01/" + Year.ToString());
        dtDec.TodaysDate = DateTime.Parse("12/01/" + Year.ToString());
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        OrganisationBAL objOrg = new OrganisationBAL();
        OrganisationBOL objBOL = objOrg.Select();
        ListItem lstItem = new ListItem("[SELECT]", "");
        if (objBOL != null)
        {
            ddlBr.DataSource = ddlBranches.DataSource = objBAL.SelectAll(objBOL.CompanyID);
            ddlBranches.DataBind();
            ddlBr.DataBind();
        }
        if ("" + Session["ROLEID"] != "4")
        {
            ddlBranches.Items.Insert(0, lstItem);
            ddlBr.Items.Insert(0, lstItem);
        }
    }

    private void GetHolidays()
    {
        HolidayBAL objBAL = new HolidayBAL();
        gvHolidays.DataSource = objBAL.SelectAll(Util.ToInt(ddlBr.SelectedValue));
        gvHolidays.DataBind();

        if (gvHolidays.Rows.Count > 0)
            ddlBr.Visible = lblBranch.Visible = true;
        else
            ddlBr.Visible = lblBranch.Visible = false;
        if ("" + Session["ROLEID"] == "4")
            gvHolidays.Columns[4].Visible = false;
    }

    protected void gvHolidays_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblHolidayID.Text = e.CommandArgument.ToString();
            txtDesc.Text = Util.CleanString(row.Cells[1].Text);
            txtDate.Text = Util.CleanString(row.Cells[2].Text);
            txtComments.Text = ((Label)row.FindControl("lblComm")).Text;
            if (Util.ToInt(((Label)row.FindControl("lblBr")).Text) > 0)
                ddlBranches.SelectedValue = ((Label)row.FindControl("lblBr")).Text;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            HolidayBAL objBAL = new HolidayBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            lblMsg.Text = "Holiday deleted successfully";
            GetHolidays();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            HolidayBAL objBAL = new HolidayBAL();
            HolidayBOL objBol = new HolidayBOL();

            objBol.HolidayID = Util.ToInt(lblHolidayID.Text);
            objBol.BranchID = Util.ToInt(ddlBranches.SelectedValue);
            objBol.Comments = txtComments.Text.Trim();
            objBol.CreatedBy = User.Identity.Name;
            objBol.Description = txtDesc.Text.Trim();
            objBol.Holiday = Util.ToDateTime(txtDate.Text.Trim());
            objBol.Status = "Y";
            objBAL.Save(objBol);

            lblMsg.Text = "Holiday saved successfully";
            Clear();
            GetHolidays();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblHolidayID.Text = "";
        txtDate.Text = "";
        txtDesc.Text = "";
        txtComments.Text = "";
        ddlBranches.SelectedIndex = 0;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvHolidays_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHolidays.PageIndex = e.NewPageIndex;
        GetHolidays();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlBr_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetHolidays();
    }
    protected void gvHolidays_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
            }
        }
    }
    protected void dtJan_SelectionChanged(object sender, EventArgs e)
    {

    }
}