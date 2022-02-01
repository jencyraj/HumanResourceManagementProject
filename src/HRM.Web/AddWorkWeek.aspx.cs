using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using HRM.BOL;
using HRM.BAL;

public partial class WorkWeek : System.Web.UI.Page
{
    public int Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErr.Text = "";
        lblMsg.Text = "";
        lblID.Text = "";
        txtDesignation.Attributes.Add("placeholder", hrmlang.GetString("enterdesg"));
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremp"));
        lblID.Text = "" + Request.QueryString["id"];
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "workweek.aspx");

            ddlbind();
            if (lblID.Text != "")
            {
                Mid = int.Parse(lblID.Text);
                LoadControl(Mid);
            }
            else
            {
                blankrow();
            }
        }


    }
    private DataTable blankrow()
    {
        DataTable dt = GetTable();
        dt.Rows.Add("", "", "", "", "", "", "");

        gvWork.DataSource = dt;
        gvWork.DataBind();

        return dt;
    }
    private DataTable GetTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Sunday");
        dt.Columns.Add("Monday");
        dt.Columns.Add("Tuesday");

        dt.Columns.Add("Wednesday");

        dt.Columns.Add("Thursday");
        dt.Columns.Add("Friday");

        dt.Columns.Add("Saturday");



        return dt;
    }
    private void LoadControl(int sKey)
    {

        DataTable dTable = new WorkWeekBAL().Workweek_edit(sKey);

        if (dTable.Rows.Count > 0)
        {

            ddlbrnch.SelectedValue = dTable.Rows[0]["BranchID"].ToString();

            OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();

            DataTable dt = objDept.SelectDepartmentsByBranchID(Util.ToInt(ddlbrnch.SelectedValue));
            ddlDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
            ddlDept.DataBind();

            ddlDept.SelectedValue = dTable.Rows[0]["DeptID"].ToString();

            txtDesignation.Text = dTable.Rows[0]["Designation"].ToString();
            txtEmployee.Text = dTable.Rows[0]["FirstName"].ToString();

           
        }
        gvWork.DataSource = (dTable.Rows.Count == 0) ? blankrow() : dTable;
        gvWork.DataBind();


    }
    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }
    private void ddlbind()
    {
        try
        {
            OrgBranchesBAL objBr = new OrgBranchesBAL();
            DataTable dt = objBr.SelectAll(Util.ToInt(Session["COMPANYID"]));
            ddlbrnch.DataSource = ReturnDT("BranchID", "Branch", dt);
            ddlbrnch.DataBind();

            //OrgDesignationBAL objDesgn = new OrgDesignationBAL();
            //dt = objDesgn.SelectAll(Util.ToInt(Session["COMPANYID"]));
            //ddlDesgn.DataSource = ReturnDT("DesignationID", "Designation", dt);
            //ddlDesgn.DataBind();



        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    private void GetWorkWeek()
    {
        try
        {
            WorkWeekBAL objWork = new WorkWeekBAL();
            DataSet dSet = objWork.Select();

            gvWork.DataSource = dSet;
            gvWork.DataBind();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string[] sDays = new string[12];
            sDays[0] = ddlbrnch.SelectedValue;
            sDays[1] = ddlDept.SelectedValue;
            sDays[2] = hfDesignationId.Value;
            sDays[3] = hfEmployeeId.Value;
            sDays[4] = ((DropDownList)gvWork.Rows[0].FindControl("ddlSunday")).SelectedValue;
            sDays[5] = ((DropDownList)gvWork.Rows[0].FindControl("ddlMonday")).SelectedValue;
            sDays[6] = ((DropDownList)gvWork.Rows[0].FindControl("ddlTuesday")).SelectedValue;
            sDays[7] = ((DropDownList)gvWork.Rows[0].FindControl("ddlWednesday")).SelectedValue;
            sDays[8] = ((DropDownList)gvWork.Rows[0].FindControl("ddlThursday")).SelectedValue;
            sDays[9] = ((DropDownList)gvWork.Rows[0].FindControl("ddlFriday")).SelectedValue;
            sDays[10] = ((DropDownList)gvWork.Rows[0].FindControl("ddlSaturday")).SelectedValue;
            sDays[11] = lblID.Text;

            if (ddlDept.Text != "" || txtDesignation.Text != "")
            {
                WorkWeekBAL objBAL = new WorkWeekBAL();

                objBAL.Save(sDays, User.Identity.Name);

                lblMsg.Text = hrmlang.GetString("workweeksvd");
            }
            else
            {
                lblErr.Text = hrmlang.GetString("workweekwarn");
            }
        }
        catch (Exception ex)
        {
            lblErr.Text += ex.Message;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("WorkWeek.aspx");
    }
    protected void gvWork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMonday = (DropDownList)e.Row.FindControl("ddlMonday");
            DropDownList ddlTuesday = (DropDownList)e.Row.FindControl("ddlTuesday");
            DropDownList ddlWednesday = (DropDownList)e.Row.FindControl("ddlWednesday");
            DropDownList ddlThursday = (DropDownList)e.Row.FindControl("ddlThursday");
            DropDownList ddlFriday = (DropDownList)e.Row.FindControl("ddlFriday");
            DropDownList ddlSaturday = (DropDownList)e.Row.FindControl("ddlSaturday");
            DropDownList ddlSunday = (DropDownList)e.Row.FindControl("ddlSunday");

            ddlMonday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Monday").ToString();
            ddlTuesday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Tuesday").ToString();
            ddlWednesday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Wednesday").ToString();
            ddlThursday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Thursday").ToString();
            ddlFriday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Friday").ToString();
            ddlSaturday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Saturday").ToString();
            ddlSunday.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Sunday").ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("monday");
            e.Row.Cells[1].Text = hrmlang.GetString("tuesday");
            e.Row.Cells[2].Text = hrmlang.GetString("wednesday");
            e.Row.Cells[3].Text = hrmlang.GetString("thursday");
            e.Row.Cells[4].Text = hrmlang.GetString("friday");
            e.Row.Cells[5].Text = hrmlang.GetString("saturday");
            e.Row.Cells[6].Text = hrmlang.GetString("sunday");
        }
    }
    protected void ddlbrnch_SelectedIndexChanged(object sender, EventArgs e)
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();

        DataTable dt = objDept.SelectDepartmentsByBranchID(Util.ToInt(ddlbrnch.SelectedValue));
        ddlDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlDept.DataBind();

    }

}