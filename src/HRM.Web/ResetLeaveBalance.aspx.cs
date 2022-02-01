using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class ResetLeaveBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ("" + Request.QueryString["empid"] == "")
                Util.ShowNoPermissionPage();
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "resetleavebalance.aspx");

            GetLeaveBalance();
        }
    }


    private void GetLeaveBalance()
    {
        LeaveBAL objBAL = new LeaveBAL();
        DataTable dtBalance = objBAL.GetLeaveBalance(Util.ToInt(Request.QueryString["empid"]));


        LeaveTypesBAL objLT = new LeaveTypesBAL();
        DataTable dT = objLT.SelectAll();

        if (dtBalance.Rows.Count == 0)
        {
            if (dT.Rows.Count > 0)
            {
                dT.Columns.Add("LeaveMonth");
                dT.Columns.Add("LeaveYear");
                dT.Columns.Add("PrevYearBalance");
                dT.Columns.Add("LeavesTaken");
                dT.Columns.Add("LeavesBalance");
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dT.Rows[i][dT.Columns.Count - 5] = DateTime.Today.Month;
                    dT.Rows[i][dT.Columns.Count - 4] = DateTime.Today.Year;
                    dT.Rows[i][dT.Columns.Count - 3] = "0";
                    dT.Rows[i][dT.Columns.Count - 2] = "0";
                    dT.Rows[i][dT.Columns.Count - 1] = dT.Rows[i]["LeaveDays"];
                }
                gvBalance.DataSource = dT;
                gvBalance.DataBind();
            }
        }
        else
        {
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                int nCount = dtBalance.Select("LeaveTypeID =" + dT.Rows[i]["LeaveTypeID"]).Length;
                if (nCount == 0)
                {
                    DataRow dRow = dtBalance.NewRow();
                    dRow["LeaveName"] = dT.Rows[i]["LeaveName"];
                    dRow["LeaveDays"] = dT.Rows[i]["LeaveDays"];
                    dRow["CarryOver"] = dT.Rows[i]["CarryOver"];
                    dRow["LeavesTaken"] = "0";
                    dRow["LeavesBalance"] = dT.Rows[i]["LeaveDays"];
                    dRow["PrevYearBalance"] = "0";
                    dRow["LeaveMonth"] = DateTime.Today.Month;
                    dRow["LeaveYear"] = DateTime.Today.Year;
                    dtBalance.Rows.Add(dRow);
                }
            }
            gvBalance.DataSource = dtBalance;
            gvBalance.DataBind();
        }
    }

    protected void gvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int i = -1;
            e.Row.Cells[++i].Text = hrmlang.GetString("year");
            e.Row.Cells[++i].Text = hrmlang.GetString("leavetype");
            e.Row.Cells[++i].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[++i].Text = hrmlang.GetString("carryover");
            e.Row.Cells[++i].Text = hrmlang.GetString("leavestaken");
            e.Row.Cells[++i].Text = hrmlang.GetString("carryovered");
            e.Row.Cells[++i].Text = hrmlang.GetString("balancecuryear");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow gRow in gvBalance.Rows)
            {
                LeaveBOL objBOL = new LeaveBOL();
                Label lblYear =(Label)gRow.FindControl("lblYear");
                Label lblMonth =(Label)gRow.FindControl("lblMonth");
                Label lblTaken =(Label)gRow.FindControl("lblTaken");
                Label lblLTID =(Label)gRow.FindControl("lblLTID");
                TextBox txtCarry =(TextBox)gRow.FindControl("txtCarry");
                TextBox txtBalance =(TextBox)gRow.FindControl("txtBalance");

                objBOL.EmployeeID = Util.ToInt(Request.QueryString["empid"]);
                objBOL.LeaveYear = (lblYear.Text == "") ? DateTime.Today.Year : Util.ToInt(lblYear.Text);
                objBOL.LeaveMonth = (lblMonth.Text == "") ? DateTime.Today.Month : Util.ToInt(lblMonth.Text);
                objBOL.LeaveTypeID = Util.ToInt(lblLTID.Text);
                objBOL.TotalLeaves = Util.ToDecimal(gRow.Cells[2].Text);
                objBOL.LeavesTaken = Util.ToDecimal(lblTaken.Text);
                objBOL.PrevYearBalance = Util.ToDecimal(txtCarry.Text);
                objBOL.LeavesBalance = Util.ToDecimal(txtBalance.Text);

                new LeaveBAL().SaveLeaveBalance(objBOL);
            }
   
            GetLeaveBalance();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GetLeaveBalance();
    }
}