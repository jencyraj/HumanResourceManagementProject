using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class LeaveApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        txtReason.Attributes.Add("placeholder", hrmlang.GetString("enterreason"));
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        btnReject.Text = hrmlang.GetString("submit");
        if (!IsPostBack)
        {
            btnSearch.Text = hrmlang.GetString("search");
            ddlStatus.Items.Clear();

            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("all"), ""));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("pending"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "N"));

            BindLeaves();
        }
    }

    private void BindLeaves()
    {
        LeaveBAL objBAL = new LeaveBAL();

        DataTable dT = objBAL.SelectAll(new LeaveBOL());

        if (txtEmployee.Text == "")
            hfEmployeeId.Value = "";

        string sFilter = "";
        if (ddlStatus.SelectedValue != "")
            sFilter += "ApprovalStatus='" + ddlStatus.SelectedValue + "'";
        if (hfEmployeeId.Value != "")
        {
            if (sFilter != "")
                sFilter += " AND ";
            sFilter += "EmployeeID=" + hfEmployeeId.Value;
        }
        dT.DefaultView.RowFilter = sFilter;
        gvLeave.DataSource = dT;
        gvLeave.DataBind();

        if (gvLeave.Rows.Count == 0)
            lblMsg.Text = "No records found";
    }

    protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeave.PageIndex = e.NewPageIndex;
        BindLeaves();
    }

    protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            LeaveBAL objBAL = new LeaveBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument.ToString()), User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("leaveapprovaldeleted");
            BindLeaves();
        }
        else if (e.CommandName == "APPROVE")
        {

            LeaveBAL objBAL = new LeaveBAL();
            LeaveBOL objBOL = new LeaveBOL();

            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblEmpID = (Label)gRow.FindControl("lblEmpID");

            objBOL.LeaveID = Util.ToInt(e.CommandArgument.ToString());
            objBOL.ApprovalStatus = "Y";
            objBOL.ApprovedBy = User.Identity.Name;
            objBAL.ApproveLeave(objBOL);

            //Update Leave Balance

            objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);

            DataSet dSet = objBAL.SelectByID(Util.ToInt(e.CommandArgument.ToString()));
            foreach (DataRow dRow in dSet.Tables[1].Rows)
            {
                try
                {
                    objBOL.LeaveDate = DateTime.Parse("" + dRow["LeaveDate"]);
                }
                catch (Exception)
                {
                    objBOL.LeaveDate = new DateTime(Util.ToInt(((Label)gRow.FindControl("lblLeaveRuleYear")).Text), Util.ToInt(((Label)gRow.FindControl("lblLeaveRuleMonth")).Text), 1);

                }
                if (objBOL.LeaveDate == null || objBOL.LeaveDate == DateTime.MinValue)
                    objBOL.LeaveDate = new DateTime(Util.ToInt(((Label)gRow.FindControl("lblLeaveRuleYear")).Text), Util.ToInt(((Label)gRow.FindControl("lblLeaveRuleMonth")).Text), 1);

                objBOL.LeaveTypeID = Util.ToInt(dRow["LeaveTypeID"]);
                objBOL.TotalLeaves = objBAL.CheckAvailability(Util.ToInt(lblEmpID.Text), objBOL.LeaveTypeID, objBOL.LeaveDate.Year);
                objBOL.LeavesTaken = decimal.Parse("" + dRow["LeaveDays"]);
                objBAL.UpdateLeaveBalance(objBOL, 1);
            }

            new AlertsBAL().Save(objBOL.EmployeeID, hrmlang.GetString("leaveapprovedalert"), "LEAVE", "" + Session["LanguageId"]);
            BindLeaves();
            lblMsg.Text = hrmlang.GetString("leaveapplicationapproved");
        }
    }

    protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("applieddate");
            e.Row.Cells[2].Text = hrmlang.GetString("fromdate");
            e.Row.Cells[3].Text = hrmlang.GetString("todate");
            e.Row.Cells[4].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[5].Text = hrmlang.GetString("reason");
            e.Row.Cells[6].Text = hrmlang.GetString("status");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvLeave.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvLeave.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvLeave.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvLeave.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = DataBinder.Eval(e.Row.DataItem, "ApprovalStatus").ToString();

            ((Label)e.Row.FindControl("lblStatus")).Text = (status == "P") ? "PENDING" : ((status == "Y") ? "APPROVED" : "REJECTED");

            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            lnkApprove.Attributes.Add("title", hrmlang.GetString("approve"));
            lnkApprove.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("approveleave"));
            Label lnkReject = (Label)e.Row.FindControl("lnkReject");
            lnkReject.Attributes.Add("title", hrmlang.GetString("reject"));
            HyperLink lnkView = (HyperLink)e.Row.FindControl("lnkView");
            lnkView.Attributes.Add("title", hrmlang.GetString("view"));
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteleave"));
            if (status == "P")
            {
                lnkReject.Visible = true;
                lnkApprove.Visible = true;
            }
            else if (status == "Y")
            {
                lnkReject.Visible = true;
                lnkApprove.Visible = false;
            }
            else if (status == "N")
            {
                lnkReject.Visible = false;
                lnkApprove.Visible = true;
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();

        objBOL.LeaveID = Util.ToInt(hdPostBack.Value);
        objBOL.ApprovalStatus = "N";
        objBOL.ApprovedBy = User.Identity.Name;
        objBOL.RejectReason = txtReason.Text.ToString();
        objBAL.ApproveLeave(objBOL);

        //Update Leave Balance

        DataSet dSet = objBAL.SelectByID(objBOL.LeaveID);
        objBOL.EmployeeID = Util.ToInt("" + dSet.Tables[0].Rows[0]["EmployeeID"]);

        int LeaveRuleyr = Util.ToInt("" + dSet.Tables[0].Rows[0]["Lyr"]);
        int LeaveRulemonth = Util.ToInt("" + dSet.Tables[0].Rows[0]["Lmon"]);

        foreach (DataRow dRow in dSet.Tables[1].Rows)
        {
            objBOL.LeaveTypeID = Util.ToInt(dRow["LeaveTypeID"]);
            objBOL.TotalLeaves = objBAL.CheckAvailability(objBOL.EmployeeID, objBOL.LeaveTypeID, DateTime.Parse("" + dRow["LeaveDate"]).Year);
            objBOL.LeavesTaken = decimal.Parse("" + dRow["LeaveDays"]);
            objBOL.LeaveDate = DateTime.Parse("" + dRow["LeaveDate"]);
            if (objBOL.LeaveDate == null)
                objBOL.LeaveDate = new DateTime(LeaveRuleyr, LeaveRulemonth, 1);
            objBAL.UpdateLeaveBalance(objBOL, 2);
        }
        new AlertsBAL().Save(objBOL.EmployeeID, hrmlang.GetString("leaverejectedalert"), "LEAVE", "" + Session["LanguageId"]);
        BindLeaves();
        lblMsg.Text = hrmlang.GetString("leaveapplicationrejected");
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLeaves();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaves();
    }
}