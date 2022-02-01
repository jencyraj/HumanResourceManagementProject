using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BAL;
using HRM.BOL;

public partial class AttendanceApproval : System.Web.UI.Page
{
    int SuperAdminRoleId = Util.ToInt(System.Configuration.ConfigurationManager.AppSettings["SuperAdminRoleId"]);
    int AdministratorRoleId = Util.ToInt(System.Configuration.ConfigurationManager.AppSettings["AdministratorRoleId"]);
    int HRManagerRoleId = Util.ToInt(System.Configuration.ConfigurationManager.AppSettings["HRManagerRoleId"]);

    protected void Page_Load(object sender, EventArgs e)
    {

        btnNew.Text = hrmlang.GetString("newattendance");
        if (!IsPostBack)
        {
            SetLanguageData();
            if (Util.ToInt(Session["ROLEID"]) == SuperAdminRoleId || Util.ToInt(Session["ROLEID"]) == AdministratorRoleId || Util.ToInt(Session["ROLEID"]) == HRManagerRoleId)
                hfRoleId.Value = Session["ROLEID"].ToString();
            else
                hfRoleId.Value = string.Empty;
            ctlCalendarAAD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarAAD.SelectedCalendareDate = DateTime.Now;
            BindAttendanceTypeDD();
            if (!string.IsNullOrEmpty(hfRoleId.Value))
            {
                divBranch.Style.Add("display", "");
                BindBranchDD();
            }
            else
            {
                divBranch.Style.Add("display", "none");

            }
            GetMonthList();
            BindGrid(true);
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(true);", true);
        }
    }

    private void SetLanguageData()
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("employee"));
        btnSearch.Text = hrmlang.GetString("search");
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("year"));
        btnApprove.Text = hrmlang.GetString("approve");
        btnReject.Text = hrmlang.GetString("reject");
        txtRejectReason.Attributes.Add("placeholder", hrmlang.GetString("enterrejectreason"));
        btnDeleteOvertime.Text = hrmlang.GetString("ok");
        hfMessage.Value = string.Format("{0}~{1}~{2}~3", hrmlang.GetString("pleaseselectdate"), hrmlang.GetString("notvalidyear"), hrmlang.GetString("pleaseenteryear"),
            hrmlang.GetString("mentionrejectreason"));

        ddApprovalStatus.Items.Clear();
        ddFilterBy.Items.Clear();

        ddApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("all"), ""));
        ddApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("pendingapproval"), "P"));
        ddApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
        ddApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "N"));

        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("today"), "0"));
        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("date"), "1"));
        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("monthyear"), "2"));

    }

    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddMonth.DataSource = dt;
        ddMonth.DataBind();
        ddMonth.Items.Insert(0, hrmlang.GetString("all"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        BindGrid(false);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(false);", true);
    }

    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int i = -1;
            e.Row.Cells[++i].Text = hrmlang.GetString("employee");
            e.Row.Cells[++i].Text = hrmlang.GetString("attendancedate");
            e.Row.Cells[++i].Text = hrmlang.GetString("signintime");
            e.Row.Cells[++i].Text = hrmlang.GetString("signouttime");
            e.Row.Cells[++i].Text = hrmlang.GetString("breakhours");
            e.Row.Cells[++i].Text = hrmlang.GetString("overtimehours");
            e.Row.Cells[++i].Text = hrmlang.GetString("comments");
            e.Row.Cells[++i].Text = hrmlang.GetString("irisentry");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAttendance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAttendance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAttendance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAttendance.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkView = (HyperLink)e.Row.FindControl("lnkView");
            lnkView.Attributes.Add("title", hrmlang.GetString("view"));
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "APPROVE")
        {
            bool bFlag = false;
            List<int> listAttendanceId = new List<int>();
            foreach (GridViewRow row in gvAttendance.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk.Checked)
                {
                    HiddenField hfAttendanceId = (HiddenField)row.FindControl("hfAttendanceId");
                    listAttendanceId.Add(Util.ToInt(hfAttendanceId.Value));
                    bFlag = true;
                }
            }
            if (bFlag)
            {
                AttendanceBAL objBAL = new AttendanceBAL();
                objBAL.ApproveAttendance(listAttendanceId, "Y", User.Identity.Name, string.Empty);
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(false);", true);
                BindGrid(false);
            }
        }
        else if (e.CommandName == "REJECT")
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "ConfirmReject", "ConfirmReject();", true);
        }
    }

    protected void lnkPostBack_Click(object sender, EventArgs e)
    {
        bool bFlag = false;
        List<int> listAttendanceId = new List<int>();
        foreach (GridViewRow row in gvAttendance.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk.Checked)
            {
                HiddenField hfAttendanceId = (HiddenField)row.FindControl("hfAttendanceId");
                listAttendanceId.Add(Util.ToInt(hfAttendanceId.Value));
                bFlag = true;
            }
        }
        if (bFlag)
        {
            AttendanceBAL objBAL = new AttendanceBAL();
            objBAL.ApproveAttendance(listAttendanceId, "N", User.Identity.Name, txtRejectReason.Text);
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(false);", true);
        }
    }

    private void BindAttendanceTypeDD()
    {
        ddAttendanceType.DataSource = new AttendanceTypeBAL().SelectAll(new AttendanceTypeBOL());
        ddAttendanceType.DataValueField = "ATId";
        ddAttendanceType.DataTextField = "AttendanceType";
        ddAttendanceType.DataBind();
        ddAttendanceType.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }

    private void BindBranchDD()
    {
        ddBranches.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddBranches.DataValueField = "BranchId";
        ddBranches.DataTextField = "Branch";
        ddBranches.DataBind();
        ddBranches.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }

    private void BindGrid(bool IsLoad)
    {
        if (IsLoad)
        {
            AttendanceBOL objBOL = new AttendanceBOL();
            objBOL.AttendanceId = 0;
            objBOL.AttendanceTypeId = 0;
            objBOL.BranchId = 0;
            objBOL.EmployeeId = 0;
            objBOL.AttendanceDate = DateTime.Now.ToString("yyyy/MM/dd");
            objBOL.Year = null;
            objBOL.Month = null;
            objBOL.Approved = null;
            if (!string.IsNullOrEmpty(hfRoleId.Value))
            {
                objBOL.RoleId = Util.ToInt(hfRoleId.Value);
            }
            else
            {
                objBOL.RoleId = 0;
            }
            objBOL.LoggedInEmployeeId = Util.ToInt(Session["EMPID"]);
            gvAttendance.DataSource = new AttendanceBAL().SearchForApproval(objBOL);
            gvAttendance.DataBind();
        }
        else
        {
            AttendanceBOL objBOL = new AttendanceBOL();
            objBOL.AttendanceId = 0;
            objBOL.AttendanceTypeId = Util.ToInt(ddAttendanceType.SelectedValue);
            if (!string.IsNullOrEmpty(hfRoleId.Value))
            {
                objBOL.BranchId = Util.ToInt(ddBranches.SelectedValue);
            }
            else
            {
                objBOL.BranchId = 0;
            }
            objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
            if (ddFilterBy.SelectedValue == "0")
            {
                objBOL.AttendanceDate = DateTime.Now.ToString("yyyy/MM/dd");
                objBOL.Year = null;
                objBOL.Month = null;
            }
            else if (ddFilterBy.SelectedValue == "1")
            {
                string[] AttendanceDateArr = ctlCalendarAAD.getGregorianDateText.ToString().Split('/');
                objBOL.AttendanceDate = AttendanceDateArr[2] + "/" + AttendanceDateArr[1] + "/" + AttendanceDateArr[0];
                objBOL.Year = null;
                objBOL.Month = null;
            }
            else if (ddFilterBy.SelectedValue == "2")
            {
                objBOL.AttendanceDate = null;
                objBOL.Year = txtYear.Text;
                if (ddMonth.SelectedIndex > 0)
                {
                    objBOL.Month = ddMonth.SelectedValue;
                }
                else
                {
                    objBOL.Month = null;
                }
            }
            if (ddApprovalStatus.SelectedValue == "")
            {
                objBOL.Approved = null;
            }
            else
            {
                objBOL.Approved = ddApprovalStatus.SelectedValue;
            }
            if (!string.IsNullOrEmpty(hfRoleId.Value))
            {
                objBOL.RoleId = Util.ToInt(hfRoleId.Value);
            }
            else
            {
                objBOL.RoleId = 0;
            }
            objBOL.LoggedInEmployeeId = Util.ToInt(Session["EMPID"]);
            gvAttendance.DataSource = new AttendanceBAL().SearchForApproval(objBOL);
            gvAttendance.DataBind();
        }

        btnReject.Visible = btnApprove.Visible = (gvAttendance.Rows.Count > 0) ? true : false;

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("addeditattendance.aspx?fromapp=1");
    }
    protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendance.PageIndex = e.NewPageIndex;
        BindGrid(false);
    }
}