using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;

using HRM.BAL;
using HRM.BOL;

public partial class Holidays : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newbranch");
        lblBranch.Text = string.Format("{0} : ", hrmlang.GetString("branch"));
        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtComments.Attributes.Add("placeholder", hrmlang.GetString("entercomments"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "holidays.aspx");
            GetBranches();
            //if (Session["BRANCHID"] != null && ("" + Session["ROLEID"]) == "4")
            //{
            //    ddlBr.ClearSelection();
            //    ddlBr.SelectedValue = Util.ToInt(Session["BRANCHID"]).ToString();
            //    ddlBr.Enabled = false;
            //    pnlNew.Visible = false;
            //}
            GetHolidays();
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        OrganisationBAL objOrg = new OrganisationBAL();
        OrganisationBOL objBOL = objOrg.Select();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
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

            if (Thread.CurrentThread.CurrentCulture.Name.StartsWith("ar"))
            {
                string sDate = ConvertDates.GregToHijri(Util.RearrangeDateTime(row.Cells[2].Text));
                ctlHoliday.SelectedCalendareDate = Convert.ToDateTime(sDate);
            }
            else
                ctlHoliday.SelectedCalendareDate = Convert.ToDateTime(Util.CleanString(row.Cells[2].Text));

            txtComments.Text = ((Label)row.FindControl("lblComm")).Text;
            if (Util.ToInt(((Label)row.FindControl("lblBr")).Text) > 0)
                ddlBranches.SelectedValue = ((Label)row.FindControl("lblBr")).Text;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            HolidayBAL objBAL = new HolidayBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("holidaydeleted");
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
            objBol.Holiday = Util.ToDateTime(ctlHoliday.getGregorianDateText);
            objBol.Status = "Y";
            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("holidaysaved");
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
        ctlHoliday.ClearDate();
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("branch");
            e.Row.Cells[1].Text = hrmlang.GetString("description");
            e.Row.Cells[2].Text = hrmlang.GetString("holiday");
            e.Row.Cells[3].Text = hrmlang.GetString("comments");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvHolidays.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvHolidays.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvHolidays.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvHolidays.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteholiday"));
            }
            if (Util.CleanString(e.Row.Cells[2].Text) != "")
                e.Row.Cells[2].Text = Convert.ToDateTime(Util.CleanString(e.Row.Cells[2].Text), System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat).ToShortDateString();
        }
    }
    protected void dtJan_SelectionChanged(object sender, EventArgs e)
    {

    }
}