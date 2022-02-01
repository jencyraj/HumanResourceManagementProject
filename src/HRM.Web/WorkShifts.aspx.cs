using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class WorkShifts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newworkshift");
        txtName.Attributes.Add("placeholder", hrmlang.GetString("enterworkshift"));
        txtStart.Attributes.Add("placeholder", hrmlang.GetString("enterstarttime"));
        txtEnd.Attributes.Add("placeholder", hrmlang.GetString("enterendtime"));
        txtworkhours.Attributes.Add("placeholder", hrmlang.GetString("workhours"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "workshifts.aspx");
            GetWorkShifts();
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetWorkShifts()
    {
        WorkShiftsBAL objBAL = new WorkShiftsBAL();
        gvWorkShift.DataSource = objBAL.SelectAll(0);
        gvWorkShift.DataBind();

    }

    protected void gvWorkShift_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblStartTime = (Label)row.FindControl("lblStartTime");
            Label lblEndTime = (Label)row.FindControl("lblEndTime");
            Label lblStart1 = (Label)row.FindControl("lblStart1");
            Label lblEnd1 = (Label)row.FindControl("lblEnd1");
            Label lblStart2 = (Label)row.FindControl("lblStart2");
            Label lblEnd2 = (Label)row.FindControl("lblEnd2");
            Label lblStart3 = (Label)row.FindControl("lblStart3");
            Label lblEnd3 = (Label)row.FindControl("lblEnd3");

            lblWSID.Text = e.CommandArgument.ToString();
            txtName.Text = Util.CleanString(row.Cells[0].Text);
            txtStart.Text = lblStartTime.Text;
            txtEnd.Text = lblEndTime.Text;

            txtBreak1Start.Text = lblStart1.Text;
            txtBreak1End.Text = lblEnd1.Text;
            txtBreak2Start.Text = lblStart2.Text;
            txtBreak2End.Text = lblEnd2.Text;
            txtBreak3Start.Text = lblStart3.Text;
            txtBreak3End.Text = lblEnd3.Text;
            txtworkhours.Text = Util.ToDecimal(row.Cells[1].Text).ToString();
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            WorkShiftsBAL objBAL = new WorkShiftsBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = "Work Shift deleted successfully";
            GetWorkShifts();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            WorkShiftsBAL objBAL = new WorkShiftsBAL();
            WorkShiftsBOL objBol = new WorkShiftsBOL();
            objBol.WSID = Util.ToInt(lblWSID.Text);
            objBol.WorkShiftName = txtName.Text.Trim();
            objBol.StartTime = txtStart.Text.Trim();
            objBol.EndTime = txtEnd.Text.Trim();
            objBol.CreatedBy = User.Identity.Name;
            objBol.BreakHour1Start = txtBreak1Start.Text.Trim();
            objBol.BreakHour1End = txtBreak1End.Text.Trim();
            objBol.BreakHour2Start = txtBreak2Start.Text.Trim();
            objBol.BreakHour2End = txtBreak2End.Text.Trim();
            objBol.BreakHour3Start = txtBreak3Start.Text.Trim();
            objBol.BreakHour3End = txtBreak3End.Text.Trim();

          
            objBol.WorkingHours = txtworkhours.Text.Trim();
            objBAL.Save(objBol);
          
            lblMsg.Text = hrmlang.GetString("workshiftsaved");
            Clear();
            GetWorkShifts();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblWSID.Text = "";
        txtName.Text = "";
        txtStart.Text = "";
        txtEnd.Text = "";
        txtworkhours.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvWorkShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWorkShift.PageIndex = e.NewPageIndex;
        GetWorkShifts();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvWorkShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("workshiftname");
            e.Row.Cells[1].Text = hrmlang.GetString("workinghours");
            e.Row.Cells[2].Text = hrmlang.GetString("starttime") + " - " + hrmlang.GetString("endtime");
            e.Row.Cells[3].Text = string.Format("{0} #1", hrmlang.GetString("breakhour"));
            e.Row.Cells[4].Text = string.Format("{0} #2", hrmlang.GetString("breakhour"));
            e.Row.Cells[5].Text = string.Format("{0} #3", hrmlang.GetString("breakhour"));
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvWorkShift.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvWorkShift.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvWorkShift.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvWorkShift.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteworkshift"));
            }
        }
    }
}