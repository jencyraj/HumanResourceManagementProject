using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class ReportingOfficers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblSupMsg.Text = "";
        lblSubMsg.Text = "";
        lblErr.Text = "";
        txtReportTo.Attributes.Add("placeholder", hrmlang.GetString("enterreportto"));
        chkImmediate.Text = hrmlang.GetString("immediatemanager");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            EmployeeBAL objBAL = new EmployeeBAL();
            EmployeeBOL objBOL = new EmployeeBOL();

            objBOL.EmployeeID = Util.ToInt("" + Request.QueryString["empid"]);
            objBOL = objBAL.Select(objBOL);
            string sEmp = objBOL.FirstName + " " + objBOL.MiddleName + " " + objBOL.LastName;
            lblEmp.Text = sEmp.Trim().Replace("  ", " ");

            GetReportingOfficers(objBOL.EmployeeID);
            GetSubordinates(objBOL.EmployeeID);
        }
    }

    protected void gvSuperiors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("reportto");
            e.Row.Cells[1].Text = hrmlang.GetString("immediatemanager");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvSuperiors.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvSuperiors.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvSuperiors.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvSuperiors.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleterecord"));
        }
    }

    protected void gvSub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("subordinates");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvSub.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvSub.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvSub.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvSub.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    private void GetReportingOfficers(int EmpID)
    {
        ReportingOfficersBAL objBAL = new ReportingOfficersBAL();
        DataTable dT = objBAL.SelectSuperiors(EmpID);

        gvSuperiors.DataSource = dT;
        gvSuperiors.DataBind();

        /*  if (dT.Rows.Count == 0)
              lblSupMsg.Text = "No records found";*/
    }

    private void GetSubordinates(int EmpID)
    {
        ReportingOfficersBAL objBAL = new ReportingOfficersBAL();
        DataTable dT = objBAL.SelectSubordinates(EmpID);

        gvSub.DataSource = dT;
        gvSub.DataBind();

        /* if (dT.Rows.Count == 0)
             lblSubMsg.Text = "No records found";*/
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtReportTo.Text = "";
        chkImmediate.Checked = false;
        hfEmployeeId.Value = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ReportingOfficersBAL objBAL = new ReportingOfficersBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        try
        {
            objBOL.EmployeeID = Util.ToInt("" + Request.QueryString["empid"]);
            objBOL.SuperiorID = Util.ToInt(hfEmployeeId.Value);
            objBOL.ImmediateSuperior = (chkImmediate.Checked) ? "Y" : "N";
            objBOL.Status = "Y";
            objBOL.CreatedBy = User.Identity.Name;

            objBAL.Save(objBOL);

            lblMsg.Text = hrmlang.GetString("datasaved");
            txtReportTo.Text = "";
            chkImmediate.Checked = false;
            hfEmployeeId.Value = "";

            GetReportingOfficers(objBOL.EmployeeID);
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void gvSuperiors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblImmed = (Label)gRow.FindControl("lblImmed");
            hfEmployeeId.Value = "" + e.CommandArgument;
            txtReportTo.Text = Convert.ToString(((Label)gRow.FindControl("lblName1")).Text + " " + ((Label)gRow.FindControl("lblName2")).Text + " " + ((Label)gRow.FindControl("lblName3")).Text).Trim().Replace("  ", " ");
            chkImmediate.Checked = (lblImmed.Text.ToUpper() == "YES") ? true : false;
        }

        if (e.CommandName == "DEL")
        {
            ReportingOfficersBAL objBAL = new ReportingOfficersBAL();
            EmployeeBOL objEmp = new EmployeeBOL();

            objEmp.EmployeeID = Util.ToInt("" + Request.QueryString["empid"]);
            objEmp.SuperiorID = Util.ToInt(e.CommandArgument);
            objBAL.Delete(objEmp);

            lblMsg.Text = hrmlang.GetString("recorddeleted");

            GetReportingOfficers(objEmp.EmployeeID);
        }
    }
}