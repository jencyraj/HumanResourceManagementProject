using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

using HRM.BAL;
using HRM.BOL;

//using System.Net.NetworkInformation;

public partial class AddWorkPlan : System.Web.UI.Page
{
    string sLang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sLang = Session["LanguageId"].ToString();
        lblErr.Text = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            lblWPMID.Text = "" + Request.QueryString["id"];
            Clear();
            getbranch();
            GetWorkShiftTypes();
            GetWorkPlan();
            GetMonthList();

            if (sLang == "en-US")
            {
                txtFrom.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
                txtTo.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }
            else
            {
                txtFrom.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                txtTo.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }

            btnNew.Text = hrmlang.GetString("addnew");
        }
        /* NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
         for (int i = 0; i < nics.Length; i++)
             Response.Write(nics[i].GetPhysicalAddress());*/
    }

    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(sLang);
        ddlMonth.DataSource = dt;
        ddlMonth.DataBind();
    }

    private void GetWorkShiftTypes()
    {
        ddlType.DataSource = new WorkShiftsBAL().SelectAll(0);
        ddlType.DataBind();
    }

    protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblWSType = (Label)e.Row.FindControl("lblWSType");
            ListItem lItem = ddlType.Items.FindByValue(((Label)e.Row.FindControl("lblWSID")).Text);
            if (lItem != null)
                lblWSType.Text = lItem.Text;

            if (lblWSType.Text == "")
                ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = false;

            Label lblFrom = (Label)e.Row.FindControl("lblFrom");
            Label lblTo = (Label)e.Row.FindControl("lblTo");

            if (lblFrom.Text != "")
                lblFrom.Text = DateTime.Parse(lblFrom.Text).ToShortDateString();
            if (lblTo.Text != "")
                lblTo.Text = DateTime.Parse(lblTo.Text).ToShortDateString();
        }
    }

    private DataTable WorkPlanTable()
    {
        DataTable dtPlan = new DataTable("WORKPLAN");

        dtPlan.Columns.Add("WPMID");
        dtPlan.Columns.Add("WPID");
        dtPlan.Columns.Add("WSID");
        dtPlan.Columns.Add("FromDate");
        dtPlan.Columns.Add("ToDate");
        dtPlan.Columns.Add("Status");

        return dtPlan;
    }

    private DataRow AddEmptyRow(DataTable dtPlan)
    {
        DataRow dtRow = dtPlan.NewRow();
        return dtRow;
    }

    //Get data from db and add a blank row
    private void GetWorkPlan()
    {
        if (Util.ToInt(lblWPMID.Text) == 0) return;

        WorkPlanBOL objBOL = new WorkPlanBOL();
        objBOL.WPMID = Util.ToInt(lblWPMID.Text);

        DataTable dPlan = new WorkPlanBAL().SelectWorkPlanMaster(objBOL);

        if (dPlan.Rows.Count > 0)
        {

            DataRow dRow = dPlan.Rows[0];
            hfEmployeeId.Value = "" + dRow["EmployeeID"];
            txtEmployee.Text = "" + dRow["fullname"];
            txtYear.Text = "" + dRow["WPYear"];
            ddlMonth.SelectedValue = "" + dRow["WPMonth"];

            DataTable dt = new WorkPlanBAL().SelectAll(objBOL);
            if (dt.Rows.Count > 0)
            {
                gvPlan.DataSource = dt;
                gvPlan.DataBind();
            }
        }
    }

    private void BindGrid(int nRowIndex, bool IsDeleted)
    {
        DataTable dTable = WorkPlanTable();

        DataRow dEmptyRow = AddEmptyRow(dTable);

        foreach (GridViewRow gRow in gvPlan.Rows)
        {
            if (IsDeleted)
                if (gRow.RowIndex == nRowIndex) continue;
            DataRow dRow = dTable.NewRow();

            dRow[0] = ((LinkButton)gRow.FindControl("lnkDel")).CommandArgument;                        //    WPID    \\
            dRow[1] = ((DropDownList)gRow.FindControl("ddlType")).SelectedValue;                      //     WSID     \\
            dRow[2] = hfEmployeeId.Value;                                                            //   EmployeeID   \\
            dRow[3] = ((UserControls_ctlCalendar)gRow.FindControl("txtFrom")).getGregorianDateText; //     FromDate     \\
            dRow[4] = ((UserControls_ctlCalendar)gRow.FindControl("txtTo")).getGregorianDateText;  //       ToDate       \\
            dRow[4] = "Y";                                                                        //        Status        \\
            dTable.Rows.Add(dRow);
        }

        dTable.Rows.Add(dEmptyRow);

        gvPlan.DataSource = dTable;
        gvPlan.DataBind();
    }

    protected void gvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            GridViewRow gRow1 = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int sWPID = Util.ToInt(((Label)gRow1.FindControl("lblWPID")).Text);
            if (sWPID > 0)
                new WorkPlanBAL().Delete(sWPID);

            DataTable dPlan = WorkPlanTable();

            foreach (GridViewRow gRow in gvPlan.Rows)
            {
                if (gRow.RowIndex == gRow1.RowIndex) continue;
                if (((Label)gRow.FindControl("lblFrom")).Text == "") continue;

                DataRow dRow = dPlan.NewRow();

                dRow["WPMID"] = Util.ToInt(Request.QueryString["id"]).ToString();
                dRow["WPID"] = ((Label)gRow.FindControl("lblWPID")).Text;
                dRow["WSID"] = ((Label)gRow.FindControl("lblWSID")).Text;
                dRow["FromDate"] = ((Label)gRow.FindControl("lblFrom")).Text;
                dRow["ToDate"] = ((Label)gRow.FindControl("lblTo")).Text;
                dRow["Status"] = "Y";

                dPlan.Rows.Add(dRow);
            }

            gvPlan.DataSource = dPlan;
            gvPlan.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gRow in gvPlan.Rows)
            {
                string sFrom1 = ((Label)gRow.FindControl("lblFrom")).Text;
                string sTo1 = ((Label)gRow.FindControl("lblTo")).Text;

                if (DateTime.Parse(sTo1).Month.ToString() != ddlMonth.SelectedValue.ToString())
                {
                    ClientScript.RegisterStartupScript(btnAdd.GetType(), "onclick", "alert('Please select dates in " + ddlMonth.SelectedItem.Text + " " + txtYear.Text + "');", true);
                    return;
                }

                if (DateTime.Parse(sFrom1).Year.ToString() != txtYear.Text.Trim() || DateTime.Parse(sTo1).Year.ToString() != txtYear.Text.Trim())
                {
                    ClientScript.RegisterStartupScript(btnAdd.GetType(), "onclick", "alert('Please select dates in " + ddlMonth.SelectedItem.Text + " " + txtYear.Text + "');", true);
                    return;
                }
            }

            WorkPlanBOL objWP = new WorkPlanBOL();

            objWP.WPMID = Util.ToInt(lblWPMID.Text);
            objWP.EmployeeID = Util.ToInt(hfEmployeeId.Value);
            objWP.CreatedBy = User.Identity.Name;
            objWP.WPYear = Util.ToInt(txtYear.Text.Trim());
            objWP.WPMonth = Util.ToInt(ddlMonth.SelectedValue);
            objWP.Status = "Y";
            objWP.BranchID = Util.ToInt(ddlBranch.SelectedValue);
            objWP.WPMID = new WorkPlanBAL().SaveMaster(objWP);

            foreach (GridViewRow gRow in gvPlan.Rows)
            {
                objWP.WPID = Util.ToInt(((Label)gRow.FindControl("lblWPID")).Text);
                objWP.WSID = Util.ToInt(((Label)gRow.FindControl("lblWSID")).Text);
                objWP.FromDate = DateTime.Parse(((Label)gRow.FindControl("lblFrom")).Text);
                objWP.ToDate = DateTime.Parse(((Label)gRow.FindControl("lblTo")).Text);
                objWP.Status = "Y";
                new WorkPlanBAL().Save(objWP);
            }

            lblMsg.Text = "Work Plan added successfully";
            lblWPMID.Text = objWP.WPMID.ToString();
            Clear();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message.ToString();
        }
    }

    public string returndate(string dtval)
    {
        if (dtval == "") return DateTime.MinValue.ToShortDateString();

        string[] dtvalue = dtval.Split('/');
        return dtvalue[1] + "/" + dtvalue[0] + "/" + dtvalue[2];
    }

    private void Clear()
    {
        txtEmployee.Text = "";
        txtYear.Text = "";
        hfEmployeeId.Value = "";
        getbranch();
        DataTable dPlan = WorkPlanTable();
        dPlan.Rows.Add(dPlan.NewRow());
        gvPlan.DataSource = dPlan;
        gvPlan.DataBind();

        GetWorkPlan();
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dPlan = WorkPlanTable();
        DataRow dRow = null;

        string sFrom1 = returndate(txtFrom.getGregorianDateText);
        string sTo1 = returndate(txtTo.getGregorianDateText);

        if (DateTime.Parse(sTo1).Month.ToString() != ddlMonth.SelectedValue.ToString())
        {
            ClientScript.RegisterStartupScript(btnAdd.GetType(), "onclick", "alert('Please select dates in " + ddlMonth.SelectedItem.Text + " " + txtYear.Text + "');", true);
            return;
        }

        if (DateTime.Parse(sFrom1).Year.ToString() != txtYear.Text.Trim() || DateTime.Parse(sTo1).Year.ToString() != txtYear.Text.Trim())
        {
            ClientScript.RegisterStartupScript(btnAdd.GetType(), "onclick", "alert('Please select dates in " + ddlMonth.SelectedItem.Text + " " + txtYear.Text + "');", true);
            return;
        }

        foreach (GridViewRow gRow in gvPlan.Rows)
        {
            if (((Label)gRow.FindControl("lblFrom")).Text == "") continue;

            string sFrom = ((Label)gRow.FindControl("lblFrom")).Text;
            string sTo = ((Label)gRow.FindControl("lblTo")).Text;

            if (DateTime.Parse(sFrom1) >= DateTime.Parse(sFrom) && DateTime.Parse(sTo1) <= DateTime.Parse(sTo))
            {
                ClientScript.RegisterStartupScript(btnAdd.GetType(), "onclick", "alert('Work shift already set for this date period');", true);
                return;
            }
            else
            {
                dRow = dPlan.NewRow();
                dRow["WPMID"] = Util.ToInt(Request.QueryString["id"]).ToString();
                dRow["WPID"] = ((Label)gRow.FindControl("lblWPID")).Text;
                dRow["WSID"] = ((Label)gRow.FindControl("lblWSID")).Text;
                dRow["FromDate"] = sFrom;
                dRow["ToDate"] = sTo;
                dRow["Status"] = "Y";

                dPlan.Rows.Add(dRow);
            }
        }

        dRow = dPlan.NewRow();
        dRow["WPMID"] = Util.ToInt(lblWPMID.Text).ToString();
        dRow["WPID"] = "0";
        dRow["WSID"] = ddlType.SelectedValue;
        dRow["FromDate"] = sFrom1;
        dRow["ToDate"] = sTo1;
        dRow["Status"] = "Y";

        dPlan.Rows.Add(dRow);

        gvPlan.DataSource = dPlan;
        gvPlan.DataBind();
    }
    public void getbranch()
    {


        ddlBranch.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataValueField = "BranchId";
        ddlBranch.DataTextField = "Branch";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "")));
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

      
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddWorkPlan.aspx");
    }
}