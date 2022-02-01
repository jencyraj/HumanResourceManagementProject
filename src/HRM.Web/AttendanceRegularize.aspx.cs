using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;
using System.Data;

public partial class AttendanceRegularize : System.Web.UI.Page
{
    string mon = null; static decimal minhr = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {

            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "attendanceregularize.aspx");

            GetMonthList();
            txtyear.Text = DateTime.Now.Year.ToString();
            mon = DateTime.Now.Month.ToString();
            ddMonth.SelectedValue = mon;

            lblID.Text = "" + Request.QueryString["id"];
            if (lblID.Text != "")
            {
                ddMonth.Enabled = false;
                txtyear.Enabled = false;
            }
            DataSet DS_Table = Check_Attendance();
            DataTable Dt_Attendance = DS_Table.Tables[0];
            DataTable Dt_Workschedule = DS_Table.Tables[1];

            btnsave.Enabled = (Dt_Workschedule.Rows.Count == 0) ? false : true;
            if (btnsave.Enabled == false)
                lblErr.Text = hrmlang.GetString("noworkscheduleset");
            if (Dt_Attendance.Rows.Count > 0)
            {
                BindgvAttendance();
            }
            if (Dt_Attendance.Rows.Count == 0)
            {
                if (Dt_Workschedule.Rows.Count > 0)
                {
                    BindgvAttendance();
                    pnl_load.Visible = (Dt_Workschedule.Rows.Count > 0) ? true : false;
                }
                else
                    pnl_load.Visible = (Dt_Workschedule.Rows.Count == 0) ? false : true;
                if (Dt_Workschedule.Rows.Count == 0)
                    lblErr.Text = hrmlang.GetString("noworkscheduleset");
            }
            btnSearch.Text = hrmlang.GetString("search");
            btnsave.Text = hrmlang.GetString("sendregrequest");
        }
    }
    private void GetPayable_Overtime()
    {
        DataTable dt_OvTime = new AttRegularBAL().GetPayable_Overtime();
        if (dt_OvTime.Rows.Count > 0)
        {
            minhr = Util.ToDecimal(dt_OvTime.Rows[0]["MinimumHr"].ToString());

            //        string[] sVal = minhr.ToString().Split('.');
            //        if (Util.ToInt(sVal[1]) > 59)
            //        {
            //          sVal[0] = Convert.ToString((Util.ToInt(sVal[0])) + Util.ToInt(sVal[1]) / 60);
            //          sVal[1] = Convert.ToString((Util.ToInt(sVal[1]) % 60) / 100);
            //          minhr = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);  
            //        }
        }
    }


    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddMonth.DataSource = dt;
        ddMonth.DataBind();
        //ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    private void BindgvAttendance()
    {
        decimal OT = 0; decimal lop = 0;
        AttRegularBOL objBOL1 = new AttRegularBOL();

        DataTable dTable = new AttRegularBAL().showAttendance(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
        // BindTimesheet(dTable);
        OT = BindTimesheet(dTable);
        Get_LOP_WrkngHour(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
        lop = Get_LOP_WrkngHour(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
        txtlateWrknghr.Text = Convert.ToString(lop);
        txtovertime.Text = Convert.ToString(OT);

        if ("" + lblID.Text != "")
        {

            DataTable DetailDT = new AttRegularBAL().SelectBy(Util.ToInt("" + lblID.Text));

            if (DetailDT.Rows.Count == 0)
            {
                lblErr.Text = hrmlang.GetString("recordnotfound");
                lblID.Text = "";
                return;
            }

            txtyear.Text = "" + DetailDT.Rows[0]["ReqYear"];
            mon = "" + DetailDT.Rows[0]["ReqMonth"];
            ddMonth.SelectedValue = mon;

            dTable = new AttRegularBAL().showAttendance(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));

            //    BindTimesheet(dTable);
            OT = BindTimesheet(dTable);
            Get_LOP_WrkngHour(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
            lop = Get_LOP_WrkngHour(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
            txtlateWrknghr.Text = Convert.ToString(lop);
            txtovertime.Text = Convert.ToString(OT);

            string app = hrmlang.GetString("approved"); ;
            string rej = hrmlang.GetString("rejected");

            txtremark.Text = "" + DetailDT.Rows[0]["ReqReason"];
            for (int i = 0; i < gvAttendance.Rows.Count; i++)
            {
                DataRow[] dr = DetailDT.Select("AttendanceDate='" + ((Label)gvAttendance.Rows[i].FindControl("lbldate")).Text + "'");

                if (dr.Length > 0)
                {
                    CheckBox chk = (CheckBox)gvAttendance.Rows[i].FindControl("chkSelect");
                    chk.Checked = true;
                    ((Label)gvAttendance.Rows[i].FindControl("lblDetailID")).Text = "" + dr[0]["AID"];

                    if ("" + dr[0]["ApprovedStatus"] == "Y")
                        ((Label)gvAttendance.Rows[i].FindControl("lblApp")).Text = "(" + app + ")";
                    else if ("" + dr[0]["ApprovedStatus"] == "N")
                        ((Label)gvAttendance.Rows[i].FindControl("lblRej")).Text = "(" + rej + ")";
                }
            }

            DataRow[] dR = DetailDT.Select("ApprovedStatus='Y' OR ApprovedStatus='N'");
            if (dR != null)
            {
                if (dR.Length > 0)
                {
                    if (dR.Length == DetailDT.Rows.Count)
                        lblMsg.Text = hrmlang.GetString("appprocesscompleted");
                    else
                        lblMsg.Text = hrmlang.GetString("appprocessinprogress");
                    btnsave.Enabled = false;
                }
                else
                    btnsave.Enabled = true;
            }

            if ("" + DetailDT.Rows[0]["RequestClosed"] == "Y")
            {
                lblMsg.Text = hrmlang.GetString("requestclosed");
                btnsave.Enabled = false;
            }
        }



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

                    if ("" + lbl_OverTime.Text != "")
                    {
                        // overtime = Util.Get_Hours(Util.ToDecimal(lbl_OverTime.Text));
                        ////string[] sVal = lbl_OverTime.Text.ToString().Split('.');
                        ////if (Util.ToInt(sVal[1]) > 59)
                        ////{
                        ////    sVal[0] = Convert.ToString((Util.ToInt(sVal[0])) + Util.ToInt(sVal[1]) / 60);
                        ////    sVal[1] = Convert.ToString((Util.ToInt(sVal[1]) % 60) / 100);

                        ////    overtime = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);
                        ////}

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
        dt.Columns.Add("DetailID");

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
                    drow["AttendanceMonth"] = date.Month.ToString();
                    drow["AttendanceYear"] = txtyear.Text;
                    drow["AttendanceDay"] = date.Day.ToString();
                    drow["Status"] = 'Y';
                    drow["ApprovedBy"] = "";
                    drow["ApprovedStatus"] = "";
                    drow["ApprovedDay"] = "";
                    drow["RejectReason"] = "";
                    drow["DetailID"] = Util.ToInt(((Label)rows.FindControl("lblDetailID")).Text);
                    dt.Rows.Add(drow);
                }
            }
        }
        return dt;
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
                    chkSelect.Visible = true;
                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y")
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);

                    chkSelect.Visible = false;
                }
                if ("" + DataBinder.Eval(e.Row.DataItem, "Regularize") == "Y")
                {
                    chkSelect.Visible = false;
                }
                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y" && Util.ToInt(DataBinder.Eval(e.Row.DataItem, "AttendanceID")) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 255, 139);
                    chkSelect.Visible = true;
                }

                if ("" + DataBinder.Eval(e.Row.DataItem, "offday") == "N" && Util.ToInt(DataBinder.Eval(e.Row.DataItem, "AttendanceID")) == 0 && "" + DataBinder.Eval(e.Row.DataItem, "LeaveDay") == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(177, 223, 252);
                    chkSelect.Visible = true;
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

                       // DateTime end = DateTime.Parse("" + DataBinder.Eval(e.Row.DataItem, "ENDTIME"));

                        decimal Hours = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "WorkedHours"));
                        //  decimal Hours = Util.ToDecimal(span.TotalHours);
                        decimal dover = 0;
                        decimal WH = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "WORKNGHOUR"));
                        decimal BH = Util.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BREAKHOURS"));


                        if (Util.Get_Hours_MinusFromHundred(Hours, BH) < WH)
                        {
                            e.Row.BackColor = System.Drawing.Color.FromArgb(255, 208, 215);
                        }

                        /* if ("" + DataBinder.Eval(e.Row.DataItem, "OverTime") == "")
                         {
                             if (Util.Get_Hours(Hours - BH) > WH)
                             {
                                 Label lbl_OverTime = (e.Row.FindControl("lbl_OverTime") as Label);
                                 dover = Hours - (BH + WH);
                                 dover = Util.Get_Hours_MinusFromHundred(dover);
                                 lbl_OverTime.Text = Convert.ToString(dover);

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
                chkSelect.Visible = false;
                e.Row.BackColor = System.Drawing.Color.FromArgb(186, 186, 186);
            }
            if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") == "")
            {

                e.Row.Cells[5].Text = "" + DataBinder.Eval(e.Row.DataItem, "Comments") + "<br />" + "<font style='color:Red'>" + "(Work Schedule not assigned)";
                chkSelect.Visible = false;
            }
            if ("" + DataBinder.Eval(e.Row.DataItem, "STARTTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "ENDTIME") == "" && "" + DataBinder.Eval(e.Row.DataItem, "offday") == "Y")
            {
                e.Row.Cells[5].Text = "" + DataBinder.Eval(e.Row.DataItem, "Comments");

            }

            Label lblws = (Label)e.Row.FindControl("lblws");
            string lb = lblws.Text;
            Label lblsrt = (Label)e.Row.FindControl("lblsrt");


        }
    }
    private DataSet Check_Attendance()
    {
        AttRegularBOL objBOL1 = new AttRegularBOL();

        DataSet dsTable = new AttRegularBAL().TimeSheet_CheckAttendance(Util.ToInt(Session["EMPID"]), Util.ToInt(ddMonth.SelectedValue), Util.ToInt(txtyear.Text));
        return dsTable;
    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Regulrize")
        {
            try
            {

                AttRegularBAL objBAL = new AttRegularBAL();
                AttRegularBOL objBol = new AttRegularBOL();
                objBol.DTAttendance = ReturnDays();

                if ("" + lblID.Text != "")
                {
                    objBAL.ClearRequestDetail(Util.ToInt("" + lblID.Text));
                }

                if (objBol.DTAttendance != null)
                {
                    if (objBol.DTAttendance.Rows.Count > 0)
                    {

                        objBol.ReqID = Util.ToInt(lblID.Text);
                        objBol.EmployeeID = Util.ToInt(Session["EMPID"]);
                        objBol.ReqMonth = DateTime.Parse("" + objBol.DTAttendance.Rows[0]["attendancedate"]).Month;
                        objBol.ReqYear = DateTime.Parse("" + objBol.DTAttendance.Rows[0]["attendancedate"]).Year;
                        objBol.ReqReason = txtremark.Text;
                        objBol.Status = "Y";
                        objBol.CreatedBy = User.Identity.Name;

                        objBol.ReqID = objBAL.Detail_Save(objBol);
                        lblID.Text = objBol.ReqID.ToString();
                        for (int i = 0; i < objBol.DTAttendance.Rows.Count; i++)
                        {
                            objBol.ReqDetailID = Util.ToInt(objBol.DTAttendance.Rows[i]["DetailID"]);
                            objBol.AttendanceDate = DateTime.Parse("" + objBol.DTAttendance.Rows[i]["AttendanceDate"]);
                            objBol.AttendanceDay = Util.ToInt(objBol.DTAttendance.Rows[i]["AttendanceDay"]);
                            objBol.AttendanceMon = objBol.ReqMonth;// Util.ToInt(objBol.DTAttendance.Rows[i]["AttendanceMonth"]);
                            objBol.Attendanceyear = objBol.ReqYear;// Util.ToInt(objBol.DTAttendance.Rows[i]["Attendanceyear"]);
                            objBAL.ReqDetail_Save(objBol);

                        }


                        lblMsg.Text = hrmlang.GetString("datasaved");
                        BindgvAttendance();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = hrmlang.GetString("datasavederror");
            }

        }

        else if (e.CommandName == "Cancel")
        {
            Response.Redirect("AttendanceRegularize.aspx");
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        lblID.Text = "";
        txtremark.Text = "";
        DataSet DS_Table = Check_Attendance();
        DataTable Dt_Attendance = DS_Table.Tables[0];
        DataTable Dt_Workschedule = DS_Table.Tables[1];

        btnsave.Enabled = (Dt_Workschedule.Rows.Count == 0) ? false : true;
        lblErr.Text = hrmlang.GetString("noworkscheduleset");
        if (Dt_Attendance.Rows.Count > 0)
        {
            BindgvAttendance();
            lblErr.Text = "";
            pnl_load.Visible = (Dt_Workschedule.Rows.Count > 0) ? true : false;
        }
        if (Dt_Attendance.Rows.Count == 0)
        {
            if (Dt_Workschedule.Rows.Count > 0)
            {
                lblErr.Text = "";
                BindgvAttendance();
                pnl_load.Visible = (Dt_Workschedule.Rows.Count > 0) ? true : false;
            }
            else

                pnl_load.Visible = (Dt_Workschedule.Rows.Count == 0) ? false : true;
            if (Dt_Workschedule.Rows.Count == 0)
                lblErr.Text = hrmlang.GetString("noworkscheduleset");

        }
    }

    protected void ddMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}