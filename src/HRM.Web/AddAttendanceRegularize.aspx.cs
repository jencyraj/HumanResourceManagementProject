using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AddAttendanceRegularize : System.Web.UI.Page
{
    decimal minhr = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblreqID.Text = "" + Request.QueryString["id"];
        if (!IsPostBack)
        {
            //  ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "AddAttendanceRegularize.aspx");
            GetRequestDetails();
            btnsave.Text = hrmlang.GetString("approve");
            btncancel.Text = hrmlang.GetString("reject");
            btnClose.Text = hrmlang.GetString("close");
        }
    }

    private void GetRequestDetails()
    {
        DataTable DT = new AttRegularBAL().SelectBy(Util.ToInt("" + Request.QueryString["id"]));

        if (DT.Rows.Count > 0)
        {
            txtcomments.Text = DT.Rows[0]["ReqReason"].ToString();
            string year = DT.Rows[0]["ReqYear"].ToString();
            lblyr.Text = DT.Rows[0]["ReqYear"].ToString();
            lblEmpID.Text = DT.Rows[0]["EmployeeID"].ToString();
            lblmon.Text = DT.Rows[0]["ReqMonth"].ToString();
            string mon = DT.Rows[0]["MONTHNAME"].ToString();
            lblemp.Text = DT.Rows[0]["Employee"].ToString();
            string attnd = String.Concat(mon, "/", year);
            lblyear.Text = attnd;

            if ("" + DT.Rows[0]["RequestClosed"] == "Y")
            {
                btnsave.Visible = false;
                btncancel.Visible = false;
            }

            chekAttendnce(DT);
        }
    }
    private void GetPayable_Overtime()
    {
        DataTable dt_OvTime = new AttRegularBAL().GetPayable_Overtime();
        if (dt_OvTime.Rows.Count > 0)
        {
            minhr = Util.ToDecimal(dt_OvTime.Rows[0]["MinimumHr"].ToString());

        }
    }
    private decimal BindTimesheet(DataTable dTable)
    {
        decimal payablehr = 0; decimal overtime = 0;
        if (dTable.Rows.Count > 0)
        {
            gvAttendance.DataSource = dTable;
            gvAttendance.DataBind();

            decimal dDiff = 0;

            for (int k = 0; k < dTable.Rows.Count; k++)
            {
                dDiff += Util.ToDecimal(dTable.Rows[k]["diff"]);
                dDiff = Util.Get_Hours(dDiff);
            }

            txtlatehr.Text = "" + dDiff;// dTable.Compute("Sum(diff)", "").ToString();

            txtlatehr.Text = Util.Get_Hours(Util.ToDecimal(txtlatehr.Text)).ToString();
            GetPayable_Overtime();
            for (int i = 0; i < gvAttendance.Rows.Count; i++)
            {

                Label lbl_OverTime = (Label)gvAttendance.Rows[i].FindControl("lbl_OverTime");

                try
                {

                    if (lbl_OverTime.Text != "")
                    {
                        overtime = Util.ToDecimal(lbl_OverTime.Text);


                        if (overtime > minhr)
                        {
                            payablehr += Util.Get_Hours_MinusFromHundred(overtime, minhr);
                            payablehr = Util.Get_Hours(payablehr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErr.Text = ex.ToString();
                }
            }
        }
        return payablehr;
    }
    private void chekAttendnce(DataTable DetailDT)
    {
        decimal lop = 0; decimal OT = 0;

        DataTable dTable = new AttRegularBAL().showAttendance(Util.ToInt(lblEmpID.Text), Util.ToInt(lblmon.Text), Util.ToInt(lblyr.Text));
        gvAttendance.DataSource = dTable;
        gvAttendance.DataBind();
        OT = BindTimesheet(dTable);

        lop = Get_LOP_WrkngHour(Util.ToInt(lblEmpID.Text), Util.ToInt(lblmon.Text), Util.ToInt(lblyr.Text));
        txtlateWrknghr.Text = Convert.ToString(lop);
        txtovertime.Text = Convert.ToString(OT);
        GetPayable_Overtime();
        for (int i = 0; i < gvAttendance.Rows.Count; i++)
        {
            DataRow[] dr = DetailDT.Select("AttendanceDate='" + ((Label)gvAttendance.Rows[i].FindControl("lbldate")).Text + "'");

            if (dr.Length > 0)
            {
                CheckBox chk = (CheckBox)gvAttendance.Rows[i].FindControl("chkSelect");
                chk.Visible = true;
                ((Label)gvAttendance.Rows[i].FindControl("lblDetailID")).Text = "" + dr[0]["AID"];
            }
        }

    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {
        string AppStatus = "Y";
        if (e.CommandName == "Approve")
            AppStatus = "Y";
        else
        {
            AppStatus = "N";
            if (txtReason.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "alert('Please enter Comments');", true);
                txtReason.Focus();
                return;
            }
        }

        try
        {
            if (gvAttendance.Rows.Count > 0)
            {
                AttRegularBAL objBAL = new AttRegularBAL();
                foreach (GridViewRow rows in gvAttendance.Rows)
                {
                    CheckBox chk = (CheckBox)rows.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        AttRegularBOL objBol = new AttRegularBOL();
                        objBol.ReqID = Util.ToInt(Request.QueryString["id"]);
                        objBol.EmployeeID = Util.ToInt(lblEmpID.Text);
                        objBol.AttendanceDate = DateTime.Parse(((Label)rows.FindControl("lbldate")).Text);
                        objBol.ApprovedBy = "" + Session["EMPID"];
                        objBol.ApprovedStatus = AppStatus;
                        objBol.RejectReason = txtReason.Text;
                        objBol.AttendanceId = Util.ToInt((((Label)rows.FindControl("lblATID")).Text));
                        objBol.WorkShiftId = Util.ToInt((((Label)rows.FindControl("lblWSID")).Text));
                        objBol.StartTime = rows.Cells[1].Text;
                        objBol.EndTime = rows.Cells[2].Text;
                        objBol.ReqDetailID = Util.ToInt((((Label)rows.FindControl("lblDetailID")).Text));
                        objBAL.ApproveRequest(objBol);
                    }
                }
            }

            lblMsg.Text = (AppStatus == "Y") ? hrmlang.GetString("attregapprovalsuccess") : hrmlang.GetString("attregrejectsuccess");


        }
        catch (Exception ex)
        {
            lblErr.Text = hrmlang.GetString("assetapprvfail");
        }
    }
    private DataTable GetTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ReqID");
        dt.Columns.Add("AttendanceDate");

        dt.Columns.Add("AttendanceMonth");
        dt.Columns.Add("AttendanceDay");
        dt.Columns.Add("AttendanceYear");
        dt.Columns.Add("Status");

        dt.Columns.Add("ApprovedBy");

        dt.Columns.Add("ApprovedStatus");
        dt.Columns.Add("ApprovedDay");

        dt.Columns.Add("RejectReason");
        dt.Columns.Add("AttendanceID");
        dt.Columns.Add("WSID");


        return dt;
    }
    private DataTable ReturnDays()
    {
        DataRow drow = null;
        DataTable dt = GetTable();
        if (gvAttendance.Rows.Count > 0)
        {
            foreach (GridViewRow rows in gvAttendance.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("chkSelect");
                if (chk.Checked)
                {
                    drow = dt.NewRow();

                    drow["ReqID"] = "";
                    DateTime date = DateTime.Parse(((Label)rows.FindControl("lbldate")).Text);
                    drow["AttendanceDate"] = date;
                    drow["AttendanceMonth"] = lblmon.Text;
                    drow["AttendanceYear"] = lblyr.Text;
                    drow["AttendanceDay"] = date.Day.ToString();
                    drow["Status"] = 'Y';
                    drow["ApprovedBy"] = "";
                    drow["ApprovedStatus"] = "";
                    drow["ApprovedDay"] = "";
                    drow["RejectReason"] = "";
                    drow["AttendanceID"] = (((Label)rows.FindControl("lblATID")).Text);
                    drow["WSID"] = (((Label)rows.FindControl("lblWSID")).Text);
                    dt.Rows.Add(drow);
                }
            }

        }
        return dt;
    }
    private DataTable Checkyear_timesheet()
    {
        return new LeaveRuleBAL().Checkyear_timesheet();
    }
    private decimal Get_LOP_WrkngHour(int EmpID, int sMonth, int sYear)
    {
        decimal dDed = 0; decimal ZeroAttendanceFrom = 0; decimal ZeroAttendanceTo = 0; decimal HalfAttendanceFrom = 0;
        decimal HalfAttendanceTo = 0;
        bool bLeaveRuleApplied = true;

        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = EmpID;
        objBOL.Month = sMonth;
        objBOL.Year = sYear;
        DataTable dT = objBAL.Get_LOP_WrkngHour(objBOL);
        DataTable DTRule = new AttendanceBAL().Get_Active_AttendanceRule();
        ZeroAttendanceFrom = Util.ToDecimal(DTRule.Rows[0]["ZeroAttendanceFrom"].ToString());
        ZeroAttendanceTo = Util.ToDecimal(DTRule.Rows[0]["ZeroAttendanceTo"].ToString());
        HalfAttendanceFrom = Util.ToDecimal(DTRule.Rows[0]["HalfAttendanceFrom"].ToString());
        HalfAttendanceTo = Util.ToDecimal(DTRule.Rows[0]["HalfAttendanceTo"].ToString());
        DataTable DTLrule = Checkyear_timesheet();

        if (DTLrule.Rows.Count > 0)
            bLeaveRuleApplied = true;

        if (DTRule.Rows[0]["UseValue"].ToString() == "Y")
        {
            foreach (DataRow row in dT.Rows)
            {
                //if (Util.ToInt(row["WMon"]) == nMonth && Util.ToInt(row["WYer"]) == nYear)
                if (Util.ToDecimal(row["lophr"]) > 0)
                {
                    if (bLeaveRuleApplied)
                    {
                        if (Util.ToDecimal(row["lophr"]) >= Util.ToDecimal(row["latehour"]))
                        {
                            dDed += Util.Get_Hours_MinusFromHundred(Util.ToDecimal(row["lophr"]), Util.ToDecimal(row["latehour"]));
                            dDed = Util.Get_Hours(dDed);
                        }
                    }
                    else
                    {
                        dDed += Util.ToDecimal(row["lophr"]);
                        dDed = Util.Get_Hours(dDed);
                    }
                }
            }

            //calculate hr:minutes correctly <here>
        }

        if (DTRule.Rows[0]["UseRule"].ToString() == "Y")
        {

            foreach (DataRow row in dT.Rows)
            {
                decimal WH = Util.ToDecimal(row["WorkShiftHours"]);
                decimal WKDHR = Util.ToDecimal(row["WorkedHours"]);
                decimal BH = Util.ToDecimal(row["BreakHours"]);
                decimal LH = Util.ToDecimal(row["latehour"]);
                decimal dVal = 0;

                if (bLeaveRuleApplied)
                    dVal = (Util.Get_Hours_MinusFromHundred(WKDHR, BH + LH) / WH) * 100;
                else
                    dVal = (Util.Get_Hours_MinusFromHundred(WKDHR, BH) / WH) * 100;

                if (ZeroAttendanceFrom <= dVal && ZeroAttendanceTo >= dVal)
                {
                    dDed += WH;
                    dDed = Util.Get_Hours(dDed);
                }
                else if (HalfAttendanceFrom <= dVal && HalfAttendanceTo >= dVal)
                {
                    dDed += (WH / 2);
                    dDed = Util.Get_Hours(dDed);
                }

                //DataRow[] dr = DTRule.Select("ZeroAttendanceFrom >='" + dDed + "' AND  ZeroAttendanceTo <='" + dDed + "'");
                //if (dr.Length > 0)
                //{
                //    dVal += WH;
                //}
                //DataRow[] drh = DTRule.Select("HalfAttendanceFrom >='" + dDed + "' AND  HalfAttendanceTo <='" + dDed + "'");
                //if (drh.Length > 0)
                //{
                //    dVal += (WH / 2);
                //}
            }
        }


        return dDed;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        GetRequestDetails();
    }
    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("attendancedate");

            e.Row.Cells[1].Text = hrmlang.GetString("signin");
            e.Row.Cells[2].Text = hrmlang.GetString("signout");

            e.Row.Cells[3].Text = hrmlang.GetString("breakhours");
            e.Row.Cells[4].Text = hrmlang.GetString("overtime");
            e.Row.Cells[5].Text = hrmlang.GetString("comments");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");



            if ("" + DataBinder.Eval(e.Row.DataItem, "LEAVEDAY") == "N")
            {
                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y")
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);

                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y" && Util.ToInt(DataBinder.Eval(e.Row.DataItem, "AttendanceID")) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 255, 139);
                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "N" && Util.ToInt(DataBinder.Eval(e.Row.DataItem, "AttendanceID")) == 0 && "" + DataBinder.Eval(e.Row.DataItem, "LeaveDay") == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(177, 223, 252);
                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "N" && Util.ToInt(DataBinder.Eval(e.Row.DataItem, "AttendanceID")) > 0)
                {

                    DateTime start = DateTime.MinValue;
                    DateTime ws_start = DateTime.MinValue;

                    if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") != "")
                        start = DateTime.Parse("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME").ToString());
                    if ("" + DataBinder.Eval(e.Row.DataItem, "ws_start") != "")
                        ws_start = DateTime.Parse("" + DataBinder.Eval(e.Row.DataItem, "ws_start").ToString());

                    if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") == "" || "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") == "")
                    {
                        e.Row.BackColor = System.Drawing.Color.FromArgb(255, 208, 215);
                    }


                    if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") != "" || "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") != "")
                    {

                        DateTime end = DateTime.Parse("" + DataBinder.Eval(e.Row.DataItem, "ENDTIME"));

                        decimal Hours = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "WorkedHours"));
                        //  decimal Hours = Util.ToDecimal(span.TotalHours);
                       //// decimal dover = 0;
                        decimal WH = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "WORKNGHOUR"));
                        decimal BH = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BREAKHOURS"));


                        if (Util.Get_Hours_MinusFromHundred(Hours, BH) < WH)
                        {
                            e.Row.BackColor = System.Drawing.Color.FromArgb(255, 208, 215);
                        }

                        /*if ("" + DataBinder.Eval(e.Row.DataItem, "OverTime") == "")
                        {
                            if (Util.Get_Hours(Hours - BH) > WH)
                            {
                                Label lbl_OverTime = (e.Row.FindControl("lbl_OverTime") as Label);
                                dover = Hours - (BH + WH);
                               
                                lbl_OverTime.Text = dover.ToString();

                            }
                        }*/
                    }

                    if (start != DateTime.MinValue && ws_start != DateTime.MinValue)
                    {
                        if (start > ws_start)
                        {
                            e.Row.BackColor = System.Drawing.Color.FromArgb(121, 222, 222);

                        }
                    }
                }
            }

            else
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(186, 186, 186);
            }
            if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") == "")
            {

                e.Row.Cells[5].Text = "" + DataBinder.Eval(e.Row.DataItem, "Comments") + "<br />" + "<font style='color:Red'>" + "(Work Schedule not assigned)";
            }
            if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y")
            {
                e.Row.Cells[5].Text = "" + DataBinder.Eval(e.Row.DataItem, "Comments");

            }



        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            new AttRegularBAL().CloseRequest(Util.ToInt(Request.QueryString["id"]));

            lblMsg.Text = hrmlang.GetString("attreqclosed");
            btnsave.Visible = false;
            btncancel.Visible = false;
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
}