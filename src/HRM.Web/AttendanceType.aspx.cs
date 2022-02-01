using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AttendanceType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("newattendancetype");
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditAttendanceType.aspx");
    }

    protected void gvAttendanceType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("attendancecode");
            e.Row.Cells[1].Text = hrmlang.GetString("attendancetype");
            e.Row.Cells[2].Text = hrmlang.GetString("category");
            e.Row.Cells[3].Text = hrmlang.GetString("typekind");
           
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAttendanceType.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAttendanceType.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAttendanceType.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAttendanceType.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "A")
                e.Row.Cells[2].Text = "Attendance";
            else if (e.Row.Cells[2].Text == "L")
                e.Row.Cells[2].Text = "Leave";
            else if (e.Row.Cells[2].Text == "H")
                e.Row.Cells[2].Text = "Holiday";
            if (e.Row.Cells[3].Text == "D")
                e.Row.Cells[3].Text = "Day";
            else if (e.Row.Cells[3].Text == "H")
                e.Row.Cells[3].Text = "Hour";
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteattendancetype"));

            if (Util.ToInt(lnkDelete.CommandArgument) <= 2)
            {
                lnkEdit.Visible = lnkDelete.Visible = false;
            }
        }
    }

    protected void gvAttendanceType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                AttendanceTypeBAL objBAL = new AttendanceTypeBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("attendancetypedeleted");
                lblErr.Text = string.Empty;
                BindGrid();
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("attendancetypedeleteerror");
            }
        }
    }

    protected void gvAttendanceType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendanceType.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        gvAttendanceType.DataSource = new AttendanceTypeBAL().SelectAll(new AttendanceTypeBOL());
        gvAttendanceType.DataBind();
    }
}