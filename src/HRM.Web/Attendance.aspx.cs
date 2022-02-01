using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class Attendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newattendance");
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("year"));
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("employee"));
        if (!IsPostBack)
        {
            EmployeeBOL objBOL = new EmployeeBOL();
            objBOL.EmployeeID = Util.ToInt(Session["EMPID"]);
            DataTable dtEmp = new EmployeeBAL().GetJuniorEmployees(objBOL);
            for (int i = 0; i < dtEmp.Rows.Count; i++)
                lblJr.Text += (lblJr.Text == "") ? "" + dtEmp.Rows[i]["EmployeeID"] : "," + "" + dtEmp.Rows[i]["EmployeeID"];

            objBOL = new EmployeeBAL().Select(objBOL);

            if ("" + Session["RoleID"] == "" + ConfigurationManager.AppSettings["SuperAdminRoleId"] || "" + Session["RoleID"] == "" + ConfigurationManager.AppSettings["AdministratorRoleId"] || "" + Session["RoleID"] == "" + ConfigurationManager.AppSettings["HRManagerRoleId"])
            {
                txtEmployee.Visible = true;
                txtJEmployee.Visible = false;
            }
            else
            {
                txtEmployee.Visible = false;
                if (dtEmp.Rows.Count > 0)
                {
                    txtJEmployee.Visible = true;
                }
                else
                {
                    txtJEmployee.Visible = false;
                    txtJEmployee.Text = ((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim();
                    hfEmployeeId.Value = "" + Session["EMPID"];
                }
            }

            if ("" + Request.QueryString["saved"] == "1")
                lblMsg.Text = hrmlang.GetString("attendancesaved");
            BindDropDowns();
            hfMessage.Value = string.Format("{0}~{1}~{2}", hrmlang.GetString("pleaseselectdate"), hrmlang.GetString("notvalidyear"), hrmlang.GetString("pleaseenteryear"));
            ctlCalendarAD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarAD.SelectedCalendareDate = DateTime.Now;
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(true);", true);
            BindAttendanceTypeDD();
            BindGrid(true);
        }
    }

    private void BindDropDowns()
    {
        ddFilterBy.Items.Clear();
        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("select"), "0"));
        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("date"), "1"));
        ddFilterBy.Items.Add(new ListItem(hrmlang.GetString("monthyear"), "2"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        BindGrid(false);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Toggle", "Toggle(false);", true);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditAttendance.aspx?isedit=1");
    }

    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("attendancetype");
            e.Row.Cells[2].Text = hrmlang.GetString("attendancedate");
            e.Row.Cells[3].Text = hrmlang.GetString("signintime");
            e.Row.Cells[4].Text = hrmlang.GetString("signouttime");
            e.Row.Cells[5].Text = hrmlang.GetString("irisentry");
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
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteattendance"));
        }
    }

    protected void gvAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                AttendanceBAL objBAL = new AttendanceBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("attendancedeleted");
                lblErr.Text = string.Empty;
                BindGrid(false);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("attendancedeleteerror");
            }
        }
    }

    protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendance.PageIndex = e.NewPageIndex;
        BindGrid(false);
    }

    private void BindAttendanceTypeDD()
    {
        ddAttendanceType.DataSource = new AttendanceTypeBAL().SelectAll(new AttendanceTypeBOL());
        ddAttendanceType.DataValueField = "ATId";
        ddAttendanceType.DataTextField = "AttendanceType";
        ddAttendanceType.DataBind();
        ddAttendanceType.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }

    private void BindGrid(bool IsLoad)
    {
        if (IsLoad)
        {
            gvAttendance.DataSource = new System.Data.DataTable();
            gvAttendance.DataBind();
        }
        else
        {
            AttendanceBOL objBOL = new AttendanceBOL();
            objBOL.AttendanceId = 0;
            objBOL.AttendanceTypeId = Util.ToInt(ddAttendanceType.SelectedValue);
            objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
            if (ddFilterBy.SelectedValue == "0")
            {
                objBOL.AttendanceDate = null;
                objBOL.Year = null;
                objBOL.Month = null;
            }
            else if (ddFilterBy.SelectedValue == "1")
            {
                string[] AttendanceDateArr = ctlCalendarAD.getGregorianDateText.ToString().Split('/');
                objBOL.AttendanceDate = AttendanceDateArr[2] + "/" + AttendanceDateArr[1] + "/" + AttendanceDateArr[0];
                objBOL.Year = null;
                objBOL.Month = null;
            }
            else if (ddFilterBy.SelectedValue == "2")
            {
                objBOL.AttendanceDate = null;
                objBOL.Year = txtYear.Text;
                if (ddMonth.SelectedValue != "0")
                {
                    objBOL.Month = ddMonth.SelectedValue;
                }
                else
                {
                    objBOL.Month = null;
                }
            }

            DataTable dTable = new AttendanceBAL().SelectAll(objBOL);
            DataView dView = dTable.DefaultView;
            if (hfEmployeeId.Value == "" && "" + Session["RoleID"] != "" + ConfigurationManager.AppSettings["SuperAdminRoleId"] && "" + Session["RoleID"] != "" + ConfigurationManager.AppSettings["AdministratorRoleId"] && "" + Session["RoleID"] != "" + ConfigurationManager.AppSettings["HRManagerRoleId"])
                dView.RowFilter = "EmployeeID in (" + lblJr.Text + ((lblJr.Text == "") ? "" : ",") + Session["EMPID"] + ")";
            gvAttendance.DataSource = dView.ToTable();
            gvAttendance.DataBind();
        }
    }
}