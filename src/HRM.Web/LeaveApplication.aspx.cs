using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class LeaveApplication : System.Web.UI.Page
{
    string Langculturename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErr.Text = "";
        lblMsg.Text = "";
        Langculturename = "" + Session["LanguageId"];

        if (!IsPostBack)
        {
            setlang();
            txtDate.Text = DateTime.Today.ToShortDateString();
            GetEmployeeDetails();

            lblLID.Text = "" + Request.QueryString["leaveid"];
            GetLeaveDetails();
            GETBALANCE();
        }
    }

    private void setlang()
    {
        if (Langculturename == "en-US")
            ctlCalendardob.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
        else
            ctlCalendardob.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;

        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtReason.Attributes.Add("placeholder", hrmlang.GetString("enterreasonforleave"));

        btnAdd.Text = hrmlang.GetString("add");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        ddlSession.Items.Clear();

        ddlSession.Items.Add(new ListItem(hrmlang.GetString("full"), "FULL"));
        ddlSession.Items.Add(new ListItem(hrmlang.GetString("fn"), "FN"));
        ddlSession.Items.Add(new ListItem(hrmlang.GetString("an"), "AN"));
    }
    protected void gvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("leavetype");
            e.Row.Cells[1].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[2].Text = hrmlang.GetString("carryover");
            e.Row.Cells[3].Text = hrmlang.GetString("leavestaken");
            e.Row.Cells[4].Text = hrmlang.GetString("carryovered");
            e.Row.Cells[5].Text = hrmlang.GetString("balance");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvBalance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvBalance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvBalance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvBalance.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    protected void gvLeaves_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("leavedate");
            e.Row.Cells[1].Text = hrmlang.GetString("leavesession");
            e.Row.Cells[2].Text = hrmlang.GetString("leavedays");
            e.Row.Cells[3].Text = hrmlang.GetString("leavetype");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvBalance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvBalance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvBalance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvBalance.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteleave"));
        }
    }
    private void GetLeaveDetails()
    {
        if (lblLID.Text == "") return;

        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();
        EmployeeBAL objEBAL = new EmployeeBAL();
        EmployeeBOL objEBOL = new EmployeeBOL();

        objBOL.LeaveID = Util.ToInt(lblLID.Text);
        DataSet dSet = objBAL.SelectByID(objBOL.LeaveID);

        txtDate.Text = DateTime.Parse("" + dSet.Tables[0].Rows[0]["CreatedDate"]).ToShortDateString();
        lblEmpID.Text = "" + dSet.Tables[0].Rows[0]["EmployeeID"];

        objEBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
        objEBOL = objEBAL.Select(objEBOL);
        string sEmp = objEBOL.FirstName + ((objEBOL.LastName != "") ? " " + objEBOL.LastName : ((objEBOL.MiddleName != "") ? " " + objEBOL.MiddleName : ""));
        txtEmployee.Text = sEmp;

        txtReason.Text = dSet.Tables[0].Rows[0]["Reason"].ToString();

        gvLeaves.DataSource = dSet.Tables[1];
        gvLeaves.DataBind();

        objBOL.ApprovalStatus = "" + dSet.Tables[0].Rows[0]["ApprovalStatus"];
        if (objBOL.ApprovalStatus != "P" && objEBOL.RoleID == 4)
            btnSave.Visible = false;
        else if (objBOL.ApprovalStatus == "P")
            btnSave.Visible = true;

        btnAdd.Visible = btnSave.Visible;
        gvLeaves.Columns[4].Visible = btnSave.Visible;
    }

    private void GetEmployeeDetails()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objEmp = new EmployeeBOL();

        objEmp.UserID = "" + Session["USERID"];
        objEmp = objBAL.Select(objEmp);

        string sEmp = objEmp.FirstName + ((objEmp.LastName != "") ? " " + objEmp.LastName : ((objEmp.MiddleName != "") ? " " + objEmp.MiddleName : ""));
        txtEmployee.Text = sEmp;

        LeaveTypesBAL objLT = new LeaveTypesBAL();
        ddlType.DataSource = objLT.SelectAll();
        ddlType.DataBind();

        lblEmpID.Text = objEmp.EmployeeID.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvLeaves.Rows.Count == 0)
        {
            lblErr.Text = hrmlang.GetString("pleaseaddleavedays");
            return;
        }

        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();

        objBOL.LeaveID = Util.ToInt(lblLID.Text);
        objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
        objBOL.Reason = txtReason.Text.Trim();
        objBOL.CreatedBy = User.Identity.Name;

        objBOL.LeaveID = objBAL.Save(objBOL);
        lblLID.Text = objBOL.LeaveID.ToString();
        foreach (GridViewRow gRow in gvLeaves.Rows)
        {
            objBOL.LeaveDetailsID = Util.ToInt(((LinkButton)gRow.FindControl("lnkDelete")).CommandArgument);


            objBOL.LeaveDate = DateTime.Parse(Util.CleanString(gRow.Cells[0].Text));

            objBOL.LeaveSession = Util.CleanString(gRow.Cells[1].Text);
            objBOL.LeaveDays = Util.CleanString(gRow.Cells[2].Text);
            objBOL.LeaveTypeID = Util.ToInt(((Label)gRow.FindControl("lblLtype")).Text);
            objBAL.SaveLeaveDates(objBOL);
        }

        lblMsg.Text = hrmlang.GetString("leaveapplied");
        GetLeaveDetails();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblLID.Text = "";
        txtReason.Text = "";
        ctlCalendardob.ClearDate();
        ddlSession.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        gvLeaves.DataSource = null;
        gvLeaves.DataBind();
    }

    private DataTable GetLeaveTable()
    {
        DataTable dTLeave = new DataTable("LEAVES");

        dTLeave.Columns.Add("LeaveDetailsID");
        dTLeave.Columns.Add("LeaveDate");
        dTLeave.Columns.Add("LeaveSession");
        dTLeave.Columns.Add("LeaveDays");
        dTLeave.Columns.Add("LeaveName");
        dTLeave.Columns.Add("LeaveTypeID");

        return dTLeave;
    }

    protected bool LeaveAvailable()
    {
        bool bAvail = false;
        LeaveBAL objBAL = new LeaveBAL();
        decimal nCnt = objBAL.CheckAvailability(Util.ToInt(lblEmpID.Text), Util.ToInt(ddlType.SelectedValue), Util.ToDateTime(ctlCalendardob.getGregorianDateText).Year);
        if (nCnt > 0)
            bAvail = true;
        else
            ClientScript.RegisterStartupScript(btnAdd.GetType(), "ONCLICK", string.Format("alert('{0}');", hrmlang.GetString("noleavebalance")), true);
        return bAvail;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LeaveBAL objBAL = new LeaveBAL();
        if (!LeaveAvailable()) return;



        int EmployeeID = Util.ToInt(lblEmpID.Text);
        string latedate = Util.ToDateTime(ctlCalendardob.getGregorianDateText).ToString("MM/dd/yyyy");
        bool offday = (objBAL.offdayselect(EmployeeID, latedate) == 1) ? true : false;
        if (offday)
        {
            lblErr.Text = hrmlang.GetString("offday");
        }
        else
        {
            DataTable dTLeave = GetLeaveTable();

            DataRow dRow = null;

            foreach (GridViewRow gRow in gvLeaves.Rows)
            {
                dRow = dTLeave.NewRow();
                dRow[0] = ((LinkButton)gRow.FindControl("lnkDelete")).CommandArgument;
                dRow[1] = Util.CleanString(gRow.Cells[0].Text);
                dRow[2] = Util.CleanString(gRow.Cells[1].Text);
                dRow[3] = Util.CleanString(gRow.Cells[2].Text);
                dRow[4] = Util.CleanString(gRow.Cells[3].Text);
                dRow[5] = ((Label)gRow.FindControl("lblLtype")).Text;
                dTLeave.Rows.Add(dRow);
            }

            dRow = dTLeave.NewRow();
            dRow[0] = 0;
            dRow[1] = Util.ToDateTime(ctlCalendardob.getGregorianDateText).ToString("MM/dd/yyyy");
            dRow[2] = ddlSession.SelectedValue;
            dRow[3] = (ddlSession.SelectedValue == "FULL") ? "1" : "0.5";
            dRow[4] = ddlType.SelectedItem.Text;
            dRow[5] = ddlType.SelectedValue;
            dTLeave.Rows.Add(dRow);

            gvLeaves.DataSource = dTLeave;
            gvLeaves.DataBind();
            if (gvLeaves.Rows.Count > 0)
            {
                btnSave.Visible = true;
            }
            ctlCalendardob.ClearDate();
            ddlSession.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
        }
    }
    protected void gvLeaves_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {

            if (e.CommandArgument.ToString() != "0")
            {
                LeaveBAL objLV = new LeaveBAL();
                objLV.DeleteLeaveDetails(Util.ToInt(e.CommandArgument));
            }

            if (gvLeaves.Rows.Count == 1)
            {
                LeaveBAL objLV = new LeaveBAL();
                objLV.Delete(Util.ToInt(lblLID.Text), User.Identity.Name);
                btnCancel_Click(null, null);
                lblMsg.Text = hrmlang.GetString("leaveapplicationdeleted");
                return;
            }
            DataTable dTLeave = GetLeaveTable();

            foreach (GridViewRow gRow in gvLeaves.Rows)
            {
                if (gRow.RowIndex == ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex) continue;

                DataRow dRow = dTLeave.NewRow();
                dRow[0] = ((LinkButton)gRow.FindControl("lnkDelete")).CommandArgument;
                dRow[1] = Util.CleanString(gRow.Cells[0].Text);
                dRow[2] = Util.CleanString(gRow.Cells[1].Text);
                dRow[3] = Util.CleanString(gRow.Cells[2].Text);
                dRow[4] = Util.CleanString(gRow.Cells[3].Text);
                dRow[5] = ((Label)gRow.FindControl("lblLtype")).Text;
                dTLeave.Rows.Add(dRow);
            }

            gvLeaves.DataSource = dTLeave;
            gvLeaves.DataBind();
        }
    }

    private void GETBALANCE()
    {
        LeaveBAL objBAL = new LeaveBAL();
        DataTable dtBalance = objBAL.GetLeaveBalance(Util.ToInt(lblEmpID.Text));


        LeaveTypesBAL objLT = new LeaveTypesBAL();
        DataTable dT = objLT.SelectAll();

        if (dtBalance.Rows.Count == 0)
        {
            if (dT.Rows.Count > 0)
            {
                dT.Columns.Add("PrevYearBalance");
                dT.Columns.Add("LeavesTaken");
                dT.Columns.Add("LeavesBalance");
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dT.Rows[i][dT.Columns.Count - 3] = "0";
                    dT.Rows[i][dT.Columns.Count - 2] = "0";
                    dT.Rows[i][dT.Columns.Count - 1] = dT.Rows[i]["LeaveDays"];
                }
                gvBalance.DataSource = dT;
                gvBalance.DataBind();
            }
            /*  else
                  lblSubMsg.Text = "No records found";*/
        }
        else
        {
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                int nCount = dtBalance.Select("LeaveTypeID =" + dT.Rows[i]["LeaveTypeID"]).Length;
                if (nCount == 0)
                {
                    DataRow dRow = dtBalance.NewRow();
                    dRow["LeaveYear"] = DateTime.Today.Year;
                    dRow["LeaveName"] = dT.Rows[i]["LeaveName"];
                    dRow["LeaveDays"] = dT.Rows[i]["LeaveDays"];
                    dRow["CarryOver"] = dT.Rows[i]["CarryOver"];
                    dRow["LeavesTaken"] = "0";
                    dRow["LeavesBalance"] = dT.Rows[i]["LeaveDays"];
                    dRow["PrevYearBalance"] = "0";
                    dtBalance.Rows.Add(dRow);
                }
            }

            DataView dtView = dtBalance.DefaultView;
            dtView.RowFilter = "LeaveYear=" + DateTime.Today.Year.ToString();
            gvBalance.DataSource = dtView.ToTable();// dtBalance;
            gvBalance.DataBind();
        }
    }
}