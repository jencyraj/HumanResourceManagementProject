using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
using HRM.BAL;


public partial class AddEditAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtAdditionalInfo.Attributes.Add("placeholder", hrmlang.GetString("enteradditionalinfo"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        btnDeleteBreak.Text = hrmlang.GetString("deletebreak");
        btnDeleteOvertime.Text = hrmlang.GetString("deleteovertime");
        if (!IsPostBack)
        {
            BindAttendanceTypeDD();
            GetWorkshift();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nAttendanceId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nAttendanceId);
            }
            else
            {
                ctlCalendarAD.SelectedCalendareDate = DateTime.Today;
                hfBreaks.Value = string.Empty;
                hfOvertimes.Value = string.Empty;
                BindEmptyBreakGrid();
                BindEmptyOvertimeGrid();
                ddAttendanceType.SelectedValue = "0";
            }
            
        }

        if ("" + Request.QueryString["fromapp"] == "1")
        {
            dvEmp.Visible = true;
        }
        else
            dvEmp.Visible = false;
    }
    /*
    protected void Page_PreRender(object sender, EventArgs e)
    {
        int isedit = "" + Request.QueryString["isedit"];
        if (isedit == 0)
        {
            ddAttendanceType.Enabled = false;
            ((TextBox)ctlCalendarAD.FindControl("txtHijri")).Attributes.Remove("onclick");
            ((TextBox)ctlCalendarAD.FindControl("txtHijri")).Enabled = false;
            ((TextBox)ctlCalendarAD.FindControl("txtGreg")).Attributes.Remove("onclick");
            ((TextBox)ctlCalendarAD.FindControl("txtGreg")).Enabled = false;
            ((TextBox)ctlCalendarAD.FindControl("txtHijri")).Style.Add("background-color", "#FFF !important");
            ((TextBox)ctlCalendarAD.FindControl("txtGreg")).Style.Add("background-color", "#FFF !important");
            ((Image)ctlCalendarAD.FindControl("imgCalendar")).Attributes.Remove("onclick");
            txtSignInTime.Enabled = false;
            txtSignOutTime.Enabled = false;
            txtAdditionalInfo.Enabled = false;
            txtSignInTime.Style.Add("background-color", "#FFF !important");
            txtSignOutTime.Style.Add("background-color", "#FFF !important");
            txtAdditionalInfo.Style.Add("background-color", "#FFF !important");
            txtSignInTime.Style.Add("cursor", "default !important");
            txtSignOutTime.Style.Add("cursor", "default !important");
            txtAdditionalInfo.Style.Add("cursor", "default !important");
            gvBreak.Columns[3].Visible = false;
            gvOvertime.Columns[3].Visible = false;
            gvBreak.FooterRow.Visible = false;
            gvOvertime.FooterRow.Visible = false;
            btnSave.Visible = false;
        }
    }
    */
    private void GetWorkshift()
    {

        WorkShiftsBAL objBr = new WorkShiftsBAL();

        ddlshift.DataSource = objBr.Selectshift();
        ddlshift.DataBind();
    }
    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }
    protected void gvBreak_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvBreak.EditIndex = e.NewEditIndex;
        DataTable dt = GetDataTableFromXMLString(hfBreaks.Value);
        List<Break> Breaks = new List<Break>();
        Breaks = CreateBreakGenericList(dt);
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
    }

    protected void gvOvertime_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvOvertime.EditIndex = e.NewEditIndex;
        DataTable dt = GetDataTableFromXMLString(hfOvertimes.Value);
        List<Overtime> Overtimes = new List<Overtime>();
        Overtimes = CreateOvertimeGenericList(dt);
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();
    }

    protected void gvBreak_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBreak.EditIndex = -1;
        DataTable dt = GetDataTableFromXMLString(hfBreaks.Value);
        List<Break> Breaks = new List<Break>();
        Breaks = CreateBreakGenericList(dt);
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
    }

    protected void gvOvertime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvOvertime.EditIndex = -1;
        DataTable dt = GetDataTableFromXMLString(hfOvertimes.Value);
        List<Overtime> Overtimes = new List<Overtime>();
        Overtimes = CreateOvertimeGenericList(dt);
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();
    }

    protected void gvBreak_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvBreak.EditIndex = -1;
        DataTable dt = GetDataTableFromXMLString(hfBreaks.Value);
        string Filter = String.Format("BreakId LIKE '{0}'", ((HiddenField)gvBreak.Rows[e.RowIndex].FindControl("hfEBreakId")).Value);
        DataRow dr = dt.Select(Filter)[0];
        dr["StartTime"] = ((TextBox)gvBreak.Rows[e.RowIndex].FindControl("txtBStartTime")).Text;
        dr["EndTime"] = ((TextBox)gvBreak.Rows[e.RowIndex].FindControl("txtBEndTime")).Text;
        dr["Description"] = ((TextBox)gvBreak.Rows[e.RowIndex].FindControl("txtBDescription")).Text;
        List<Break> Breaks = new List<Break>();
        Breaks = CreateBreakGenericList(dt);
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
    }

    protected void gvOvertime_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvOvertime.EditIndex = -1;
        DataTable dt = GetDataTableFromXMLString(hfOvertimes.Value);
        string Filter = String.Format("OvertimeId LIKE '{0}'", ((HiddenField)gvOvertime.Rows[e.RowIndex].FindControl("hfEOvertimeId")).Value);
        DataRow dr = dt.Select(Filter)[0];
        dr["StartTime"] = ((TextBox)gvOvertime.Rows[e.RowIndex].FindControl("txtOStartTime")).Text;
        dr["EndTime"] = ((TextBox)gvOvertime.Rows[e.RowIndex].FindControl("txtOEndTime")).Text;
        dr["Description"] = ((TextBox)gvOvertime.Rows[e.RowIndex].FindControl("txtODescription")).Text;
        List<Overtime> Overtimes = new List<Overtime>();
        Overtimes = CreateOvertimeGenericList(dt);
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();
    }

    protected void gvBreak_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hdnDelete.Value = string.Format("{0}~Break",e.Keys[0].ToString());
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "ConfirmBreak", "ConfirmBreak();", true);
    }

    protected void gvOvertime_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hdnDelete.Value = string.Format("{0}~Overtime", e.Keys[0].ToString());
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "ConfirmOvertime", "ConfirmOvertime();", true);
    }

    protected void lnkPostBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnDelete.Value))
        {
            string[] details = hdnDelete.Value.Split('~');
            if (details[1] == "Overtime")
            {
                if (!details[0].StartsWith("tmp"))
                {
                    int OvertimeId = Util.ToInt(details[0]);
                    AttendanceBAL objBAL = new AttendanceBAL();
                    objBAL.DeleteOvertime(OvertimeId);
                }
                gvOvertime.EditIndex = -1;
                DataTable dt = GetDataTableFromXMLString(hfOvertimes.Value);
                string Filter = String.Format("OvertimeId LIKE '{0}'", details[0]);
                DataRow dr = dt.Select(Filter)[0];
                dt.Rows.Remove(dr);
                List<Overtime> Overtimes = new List<Overtime>();
                Overtimes = CreateOvertimeGenericList(dt);
                hfOvertimes.Value = CreateXMLString(Overtimes);
                gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
                gvOvertime.DataBind();
            }
            else if (details[1] == "Break")
            {
                if (!details[0].StartsWith("tmp"))
                {
                    int BreakId = Util.ToInt(details[0]);
                    AttendanceBAL objBAL = new AttendanceBAL();
                    objBAL.DeleteBreak(BreakId);
                }
                gvBreak.EditIndex = -1;
                DataTable dt = GetDataTableFromXMLString(hfBreaks.Value);
                string Filter = String.Format("BreakId LIKE '{0}'", details[0]);
                DataRow dr = dt.Select(Filter)[0];
                dt.Rows.Remove(dr);
                List<Break> Breaks = new List<Break>();
                Breaks = CreateBreakGenericList(dt);
                hfBreaks.Value = CreateXMLString(Breaks);
                gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
                gvBreak.DataBind();
            }
            hdnDelete.Value = string.Empty;
        }
    }

    protected void gvBreak_RowDataBound(object sender, GridViewRowEventArgs e) 
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("starttime");
            e.Row.Cells[1].Text = hrmlang.GetString("endtime");
            e.Row.Cells[2].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfBreakId = (HiddenField)e.Row.FindControl("hfBreakId");
            if (hfBreakId != null )
            {
                if (hfBreakId.Value == "0")
                {
                    e.Row.Visible = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtBNewDescription = (TextBox)e.Row.FindControl("txtBNewDescription");
            txtBNewDescription.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            LinkButton btnAddNewBreak = (LinkButton)e.Row.FindControl("btnAddNewBreak");
            btnAddNewBreak.Text = hrmlang.GetString("addbreak");
        }
    }

    protected void gvOvertime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("starttime");
            e.Row.Cells[1].Text = hrmlang.GetString("endtime");
            e.Row.Cells[2].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfOvertimeId = (HiddenField)e.Row.FindControl("hfOvertimeId");
            if (hfOvertimeId != null)
            {
                if (hfOvertimeId.Value == "0")
                {
                    e.Row.Visible = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtONewDescription = (TextBox)e.Row.FindControl("txtONewDescription");
            txtONewDescription.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            LinkButton btnAddNewOvertime = (LinkButton)e.Row.FindControl("btnAddNewOvertime");
            btnAddNewOvertime.Text = hrmlang.GetString("addovertime");
        }
    }

    protected void btnAddNewBreak_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataTableFromXMLString(hfBreaks.Value);
        DataRow dr = dt.NewRow();
        dr["BreakId"] = string.Format("tmp{0}", DateTime.Now.ToString("ddMMyyhhmmss"));
        dr["StartTime"] = ((TextBox)gvBreak.FooterRow.FindControl("txtBNewStartTime")).Text;
        dr["EndTime"] = ((TextBox)gvBreak.FooterRow.FindControl("txtBNewEndTime")).Text;
        dr["Description"] = ((TextBox)gvBreak.FooterRow.FindControl("txtBNewDescription")).Text;
        dt.Rows.Add(dr);
        List<Break> Breaks = new List<Break>();
        Breaks = CreateBreakGenericList(dt);
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
    }

    protected void btnAddNewOvertime_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataTableFromXMLString(hfOvertimes.Value);
        DataRow dr = dt.NewRow();
        dr["OvertimeId"] = string.Format("tmp{0}",DateTime.Now.ToString("ddMMyyhhmmss"));
        dr["StartTime"] = ((TextBox)gvOvertime.FooterRow.FindControl("txtONewStartTime")).Text;
        dr["EndTime"] = ((TextBox)gvOvertime.FooterRow.FindControl("txtONewEndTime")).Text;
        dr["Description"] = ((TextBox)gvOvertime.FooterRow.FindControl("txtONewDescription")).Text;
        dt.Rows.Add(dr);
        List<Overtime> Overtimes = new List<Overtime>();
        Overtimes = CreateOvertimeGenericList(dt);
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                
                    string[] AttendanceDateArr = ctlCalendarAD.getGregorianDateText.ToString().Split('/');
                    string  AttendanceDate = AttendanceDateArr[1] + "/" + AttendanceDateArr[0] + "/" + AttendanceDateArr[2];
               
                AttendanceBAL objBAL = new AttendanceBAL();
                int LeaveApproved = objBAL.IsLeaveApproved(Util.ToInt(Session["EMPID"]), AttendanceDate);
                if (LeaveApproved > 0)
                {
                    lblMsg.Text = hrmlang.GetString("leaveapprovedfordate");
                    return;
                }
                Save();
                lblErr.Text = string.Empty;
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("attendancesaveerror");
                return;
            }
        }
        int isedit = Util.ToInt(Request.QueryString["isedit"]);
        if (isedit == 0)
        {
            Response.Redirect("AttendanceApproval.aspx");
        }
        else if (isedit == 1)
        {
            Response.Redirect("Attendance.aspx?saved=1");
        }
        
    }

    private void LoadDetails(int nAttendanceId)
    {
        AttendanceBOL objBOL = new AttendanceBOL();
        objBOL = new AttendanceBAL().SearchById(nAttendanceId);
        ddAttendanceType.SelectedValue = objBOL.AttendanceTypeId.ToString();
        if (!string.IsNullOrEmpty(objBOL.AttendanceDate))
        {
            ctlCalendarAD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarAD.SelectedCalendareDate = DateTime.Parse(objBOL.AttendanceDate);
        }
        hfEmployeeId.Value = objBOL.EmployeeId.ToString();
        txtSignInTime.Text = objBOL.SignInTime;
        txtSignOutTime.Text = objBOL.SignOutTime;
        txtAdditionalInfo.Text = objBOL.AdditionalInfo;
        List<Break> Breaks = new List<Break>();
        Break FirstBreak = new Break();
        FirstBreak.BreakId = "0";
        FirstBreak.StartTime = string.Empty;
        FirstBreak.EndTime = string.Empty;
        FirstBreak.Description = string.Empty;
        Breaks.Add(FirstBreak);
        foreach (AttendanceBreakBOL BreakBOL in objBOL.Breaks)
        {
            Break Break = new Break();
            Break.BreakId = BreakBOL.BreakId.ToString();
            Break.StartTime = BreakBOL.StartTime;
            Break.EndTime = BreakBOL.EndTime;
            Break.Description = BreakBOL.Description;
            Breaks.Add(Break);
        }
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
        List<Overtime> Overtimes = new List<Overtime>();
        Overtime FirstOvertime = new Overtime();
        FirstOvertime.OvertimeId = "0";
        FirstOvertime.StartTime = string.Empty;
        FirstOvertime.EndTime = string.Empty;
        FirstOvertime.Description = string.Empty;
        Overtimes.Add(FirstOvertime);
        foreach (AttendanceOvertimeBOL OvertimeBOL in objBOL.Overtimes)
        {
            Overtime Overtime = new Overtime();
            Overtime.OvertimeId = OvertimeBOL.OvertimeId.ToString();
            Overtime.StartTime = OvertimeBOL.StartTime;
            Overtime.EndTime = OvertimeBOL.EndTime;
            Overtime.Description = OvertimeBOL.Description;
            Overtimes.Add(Overtime);
        }
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();

        EmployeeBOL objEmp = new EmployeeBOL();
        objEmp.EmployeeID = Util.ToInt(hfEmployeeId.Value);
        objEmp = new EmployeeBAL().Select(objEmp);
        txtEmployee.Text = ((objEmp.FirstName + " " + objEmp.MiddleName).Trim() + " " + objEmp.LastName).Trim();
    }

    private void BindAttendanceTypeDD()
    {
        ddAttendanceType.DataSource = new AttendanceTypeBAL().SelectAll(new AttendanceTypeBOL());
        ddAttendanceType.DataValueField = "ATId";
        ddAttendanceType.DataTextField = "AttendanceType";
        ddAttendanceType.DataBind();
    }

    private void BindEmptyBreakGrid()
    { 
        DataTable dt = new DataTable();
        dt.Columns.Add("BreakId");
        dt.Columns.Add("StartTime");
        dt.Columns.Add("EndTime");
        dt.Columns.Add("Description");
        DataRow dr = dt.NewRow();
        dr["BreakId"] = "0";
        dr["StartTime"] = string.Empty;
        dr["EndTime"] = string.Empty;
        dr["Description"] = string.Empty;
        dt.Rows.Add(dr);
        List<Break> Breaks = new List<Break>();
        Breaks = CreateBreakGenericList(dt);
        hfBreaks.Value = CreateXMLString(Breaks);
        gvBreak.DataSource = GetDataTableFromXMLString(hfBreaks.Value);
        gvBreak.DataBind();
    }

    private void BindEmptyOvertimeGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OvertimeId");
        dt.Columns.Add("StartTime");
        dt.Columns.Add("EndTime");
        dt.Columns.Add("Description");
        DataRow dr = dt.NewRow();
        dr["OvertimeId"] = "0";
        dr["StartTime"] = string.Empty;
        dr["EndTime"] = string.Empty;
        dr["Description"] = string.Empty;
        dt.Rows.Add(dr);
        List<Overtime> Overtimes = new List<Overtime>();
        Overtimes = CreateOvertimeGenericList(dt);
        hfOvertimes.Value = CreateXMLString(Overtimes);
        gvOvertime.DataSource = GetDataTableFromXMLString(hfOvertimes.Value);
        gvOvertime.DataBind();
    }

    private List<Break> CreateBreakGenericList(DataTable dt)
    {
        List<Break> listBreaks = new List<Break>();
        if (dt.Rows.Count != 0)
        {
            foreach( DataRow dr in dt.Rows)
            {
                Break Break =  new Break();
                if (dr["BreakId"] != DBNull.Value)
                {
                    Break.BreakId = dr["BreakId"].ToString();
                }
                if(dr["StartTime"] != DBNull.Value)
                {
                    Break.StartTime = dr["StartTime"].ToString();
                }
                if (dr["EndTime"] != DBNull.Value)
                {
                    Break.EndTime = dr["EndTime"].ToString();
                }
                if (dr["Description"] != DBNull.Value)
                {
                    Break.Description = dr["Description"].ToString();
                }
                listBreaks.Add(Break);
            }
        }
        return listBreaks;
    }

    private List<Overtime> CreateOvertimeGenericList(DataTable dt)
    {
        List<Overtime> listOvertimes = new List<Overtime>();
        if (dt.Rows.Count != 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                Overtime Overtime = new Overtime();
                if (dr["OvertimeId"] != DBNull.Value)
                {
                    Overtime.OvertimeId = dr["OvertimeId"].ToString();
                }
                if (dr["StartTime"] != DBNull.Value)
                {
                    Overtime.StartTime = dr["StartTime"].ToString();
                }
                if (dr["EndTime"] != DBNull.Value)
                {
                    Overtime.EndTime = dr["EndTime"].ToString();
                }
                if (dr["Description"] != DBNull.Value)
                {
                    Overtime.Description = dr["Description"].ToString();
                }
                listOvertimes.Add(Overtime);
            }
        }
        return listOvertimes;
    }

    private void Save()
    {
        AttendanceBOL objBOL = new AttendanceBOL();
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            objBOL.AttendanceId = Util.ToInt(Request.QueryString["id"]);
        }
        else
        {
            objBOL.AttendanceId = 0;
        }

        if(hfEmployeeId.Value =="")
            objBOL.EmployeeId = Util.ToInt(Session["EMPID"]);
        else
            objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);

        objBOL.AttendanceTypeId = Util.ToInt(ddAttendanceType.SelectedValue);
        if ("" + ctlCalendarAD.getGregorianDateText != "")
        {
            string[] AttendanceDateArr = ctlCalendarAD.getGregorianDateText.ToString().Split('/');
            objBOL.AttendanceDate = AttendanceDateArr[1] + "/" + AttendanceDateArr[0] + "/" + AttendanceDateArr[2];
        }
        objBOL.SignInTime = txtSignInTime.Text.Trim();
        objBOL.SignOutTime = txtSignOutTime.Text.Trim();
        objBOL.AdditionalInfo = txtAdditionalInfo.Text.Trim();
        objBOL.Status = "Y";
        objBOL.ShiftType = ddlshift.SelectedValue;
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        objBOL.Approved = "P";
        List<AttendanceBreakBOL> lstBreak = new List<AttendanceBreakBOL>();
        foreach (GridViewRow row in gvBreak.Rows)
        {
            HiddenField hfBreakId = (HiddenField)row.FindControl("hfBreakId");
            if (hfBreakId.Value == "0")
                continue;
            AttendanceBreakBOL Break = new AttendanceBreakBOL();
            if (hfBreakId.Value.StartsWith("tmp"))
            {
                Break.BreakId = 0;
            }
            else
            {
                Break.BreakId = Util.ToInt(hfBreakId.Value);
            }
            Label lblBStartTime = (Label)row.FindControl("lblBStartTime");
            Break.StartTime = lblBStartTime.Text.Trim();
            Label lblBEndTime = (Label)row.FindControl("lblBEndTime");
            Break.EndTime = lblBEndTime.Text.Trim();
            Label lblBDescription = (Label)row.FindControl("lblBDescription");
            Break.Description = lblBDescription.Text.Trim();
            Break.Status = "Y";
            Break.CreatedBy = User.Identity.Name;
            Break.ModifiedBy = User.Identity.Name;
            lstBreak.Add(Break);
        }
        objBOL.Breaks = lstBreak;
        List<AttendanceOvertimeBOL> lstOvertime = new List<AttendanceOvertimeBOL>();
        foreach (GridViewRow row in gvOvertime.Rows)
        {
            HiddenField hfOvertimeId = (HiddenField)row.FindControl("hfOvertimeId");
            if (hfOvertimeId.Value == "0")
                continue;
            AttendanceOvertimeBOL Overtime = new AttendanceOvertimeBOL();
            if (hfOvertimeId.Value.StartsWith("tmp"))
            {
                Overtime.OvertimeId = 0;
            }
            else
            {
                Overtime.OvertimeId = Util.ToInt(hfOvertimeId.Value);
            }
            Label lblBStartTime = (Label)row.FindControl("lblOStartTime");
            Overtime.StartTime = lblBStartTime.Text.Trim();
            Label lblBEndTime = (Label)row.FindControl("lblOEndTime");
            Overtime.EndTime = lblBEndTime.Text.Trim();
            Label lblBDescription = (Label)row.FindControl("lblODescription");
            Overtime.Description = lblBDescription.Text.Trim();
            Overtime.Status = "Y";
            Overtime.CreatedBy = User.Identity.Name;
            Overtime.ModifiedBy = User.Identity.Name;
            lstOvertime.Add(Overtime);
        }
        objBOL.Overtimes = lstOvertime;
        AttendanceBAL objBAL = new AttendanceBAL();
        objBAL.Save(objBOL);
        lblMsg.Text = hrmlang.GetString("attendancesaved");
    }

    private string CreateXMLString(object ClassObject)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlSerializer xmlSerializer = new XmlSerializer(ClassObject.GetType());
        MemoryStream xmlStream = new MemoryStream();
        xmlSerializer.Serialize(xmlStream, ClassObject);
        xmlStream.Position = 0;
        xmlDoc.Load(xmlStream);
        return Base64Encode(xmlDoc.InnerXml);
    }

    public DataTable GetDataTableFromXMLString(string XMLString)
    {
        DataTable dt = new DataTable();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(Base64Decode(XMLString));
        XmlReader xmlReader = new XmlNodeReader(doc);
        DataSet ds = new DataSet();
        ds.ReadXml(xmlReader);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }
        return dt;
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public class Break
    {
        public string BreakId;
        public string StartTime;
        public string EndTime;
        public string Description;
    }

    public class Overtime
    {
        public string OvertimeId;
        public string StartTime;
        public string EndTime;
        public string Description;
    }
}