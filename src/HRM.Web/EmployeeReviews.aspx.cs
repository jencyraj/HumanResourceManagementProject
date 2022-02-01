using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class EmployeeReviews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        lblSMsg.Text = "";
        lblErrMsg.Text = "";
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("search");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "employeereviews.aspx");
            GetCompanyDetails();
            GetDropDownValues();
        }
    }

    #region APPRAISAL & REVIEWS

    private void GetAppraisalPeriods()
    {
        AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
        DataTable dTable = objBAL.GetAppraisalPeriods(Util.ToInt(lblEmpID.Text)).Tables[0];// objBAL.SelectAll(0).Tables[0];
        gvAppraisal.DataSource = dTable;
        gvAppraisal.DataBind();

        if (gvAppraisal.Rows.Count > 0)
        {
            gvAppraisal.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAppraisal.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAppraisal.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAppraisal.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        else
            lblErrMsg.Text = hrmlang.GetString("norecordsfound");
    }

    protected void gvAppraisal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GetAppraisalPeriods();
    }

    protected void gvAppraisal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("startdate");
            e.Row.Cells[2].Text = hrmlang.GetString("enddate");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkview = (LinkButton)e.Row.FindControl("lnkview");
            lnkview.Attributes.Add("title", hrmlang.GetString("viewreviews"));
        }
    }

    protected void gvAppraisal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "FILL")
        {
            lblAppPeriodID.Text = "" + e.CommandArgument;
            BindReviews();
            ClientScript.RegisterStartupScript(gvAppraisal.GetType(), "onclick", "$('.reviewtitle').css(\"display\", \"\"); $('#dvForm').modal();", true);
        }
    }

    private void BindReviews()
    {
        /*try
        {
            gvReviews.DataSource = new AppraisalReviewBAL().SelectReviews(Util.ToInt(lblEmpID.Text), Util.ToInt(lblAppPeriodID.Text));
            gvReviews.DataBind();

            if (gvReviews.Rows.Count > 0)
            {
                gvReviews.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
                gvReviews.PagerSettings.NextPageText = hrmlang.GetString("next");
                gvReviews.PagerSettings.FirstPageText = hrmlang.GetString("first");
                gvReviews.PagerSettings.LastPageText = hrmlang.GetString("last");
            }
            else
                lblErrMsg.Text = hrmlang.GetString("norecordsfound");
        }
        catch (Exception ex)
        {
            throw ex;
        }*/

        appraisaldocs.GenerateAppraisalReview(lblEmpID.Text, lblAppPeriodID.Text);
    }
    protected void gvReviews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindReviews();
    }

    protected void gvReviews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("reviewer");
            e.Row.Cells[1].Text = hrmlang.GetString("reviewedon");
            e.Row.Cells[2].Text = hrmlang.GetString("comments");
            e.Row.Cells[3].Text = hrmlang.GetString("avgrating");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkview = (LinkButton)e.Row.FindControl("lnkview");
            lnkview.Attributes.Add("title", hrmlang.GetString("view"));
        }
    }


    protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWDETAILS")
        {
            DataTable dTable = new EmpAppraisalBAL().SelectAll(Util.ToInt(lblAppPeriodID.Text), Util.ToInt(lblEmpID.Text)).Tables[1];
            ViewState["COMP"] = dTable;

            DataSet dSet = new AppraisalReviewBAL().Select(Util.ToInt(e.CommandArgument));

            if (dSet.Tables.Count > 0)
            {
                DataTable dT = dSet.Tables[0];
                if (dT.Rows.Count > 0)
                    txtComments.Text = "" + dT.Rows[0]["Comments"];
            }

            ViewState["REVIEW"] = dSet;

            DataView dView = dTable.DefaultView;
            rptrForm.DataSource = dView.ToTable(true, "CompetencyTypeID", "CompetencyType");
            rptrForm.DataBind();

            pnlList.Style.Add("display", "none");
            pnlDetails.Style.Add("display", "");

            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            lblFname.Text = ((Label)gRow.Cells[0].FindControl("lblfname")).Text;
            lblMname.Text = ((Label)gRow.Cells[0].FindControl("lblmname")).Text;
            lblLname.Text = ((Label)gRow.Cells[0].FindControl("lbllname")).Text;
            ClientScript.RegisterStartupScript(gvReviews.GetType(), "onclick", "OpenPanel();$('#dvForm').modal();", true);
        }
    }

    protected void rptrForm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblCTID = (Label)e.Item.FindControl("lblCTID");

            if (ViewState["COMP"] != null)
            {
                DataTable dTable = (DataTable)ViewState["COMP"];
                if (dTable != null)
                {
                    if (dTable.Rows.Count > 0)
                    {
                        GridView gvForm = (GridView)e.Item.FindControl("gvForm");

                        DataView dView = dTable.DefaultView;
                        dView.RowFilter = "CompetencyTypeID=" + lblCTID.Text;
                        gvForm.DataSource = dView.ToTable();
                        gvForm.DataBind();
                    }
                }
            }
        }
    }

    protected void gvForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("employeeratings");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRating = (Label)e.Row.FindControl("lblRating");
            Label lblRvwRating = (Label)e.Row.FindControl("lblRvwRating");

            ListItem lItem = ddlRating.Items.FindByValue(lblRating.Text);
            if (lItem != null)
                lblRating.Text = lItem.Text;

            if (ViewState["REVIEW"] != null)
            {
                DataSet dSet = (DataSet)ViewState["REVIEW"];
                if (dSet != null)
                {
                    if (dSet.Tables.Count > 1)
                    {
                        DataTable dT = dSet.Tables[1];
                        if (dT.Rows.Count > 0)
                        {
                            DataRow[] dRow = dT.Select("competencyid=" + ((Label)e.Row.FindControl("lblCID")).Text);
                            lItem = ddlRating.Items.FindByValue("" + dRow[0]["RatingID"]);
                            if (lItem != null)
                                lblRvwRating.Text = lItem.Text;
                        }
                    }
                }
            }
        }
    }

    #endregion


    #region EMPLOYEE SEARCH

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

    }

    private void GetDepartments()
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();

        DataTable dt = objDept.SelectDepartmentsByBranchID(Util.ToInt(ddlBranch.SelectedValue));
        ddlDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlDept.DataBind();
        GetSubDepartments();
    }

    private void GetSubDepartments()
    {
        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();
        OrgDepartmentBOL objBOL = new OrgDepartmentBOL();

        objBOL.ParentDeptID = Util.ToInt(ddlDept.SelectedValue);
        DataTable dt = objDept.SelectAll(objBOL); ;
        if (Util.ToInt(ddlDept.SelectedValue) == 0)
            dt.Rows.Clear();
        ddlSubDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlSubDept.DataBind();
    }

    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }

    private void GetDropDownValues()
    {
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        ddlBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataBind();

        OrgDesignationBAL objDesgn = new OrgDesignationBAL();
        ddlDesgn.DataSource = objDesgn.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDesgn.DataBind();

        RoleBAL objRole = new RoleBAL();
        ddlRole.DataSource = objRole.SelectAll(0);
        ddlRole.DataBind();

        ddlDept.Items.Insert(0, lstItem);
        ddlSubDept.Items.Insert(0, lstItem);
        ddlBranch.Items.Insert(0, lstItem);
        ddlDesgn.Items.Insert(0, lstItem);
        ddlRole.Items.Insert(0, lstItem);

        RatingBAL objBAL = new RatingBAL();
        ddlRating.DataSource = objBAL.SelectAll(0);
        ddlRating.DataBind();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEmployee.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        if (ddlBranch.SelectedIndex > 0)
            objBOL.BranchID = int.Parse(ddlBranch.SelectedValue);

        if (ddlDept.SelectedIndex > 0)
            objBOL.DeptId = int.Parse(ddlDept.SelectedValue);

        if (ddlStatus.SelectedIndex > 0)
            objBOL.EmpStatus = ddlStatus.SelectedValue;

        if (ddlRole.SelectedIndex > 0)
            objBOL.RoleID = int.Parse(ddlRole.SelectedValue);

        if (ddlDesgn.SelectedIndex > 0)
            objBOL.DesgnID = int.Parse(ddlDesgn.SelectedValue);

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();
        objBOL.EmpCode = txtEmpCode.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();

        if (dtEmp.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("norecordsfound");
        else
        {
            gvEmployee.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEmployee.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEmployee.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEmployee.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlDesgn.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtEmpCode.Text = "";
        txtfName.Text = "";
        txtlName.Text = "";
        txtmName.Text = "";
    }
    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "REVIEWS")
        {
            lblEmpID.Text = e.CommandArgument.ToString();
            GetAppraisalPeriods();
            gvReviews.DataSource = null;
            gvReviews.DataBind();
            pnlList.Style.Add("display", "");
            pnlDetails.Style.Add("display", "none");

            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            ltEmp.Text = ((Label)gRow.Cells[0].FindControl("lblName1")).Text;
            ltEmp.Text += (" " + ((Label)gRow.Cells[0].FindControl("lblName2")).Text).TrimEnd();
            ltEmp.Text += (" " + ((Label)gRow.Cells[0].FindControl("lblName3")).Text).TrimEnd();
            lblEmpName.Text = ltEmp.Text;
            ClientScript.RegisterStartupScript(gvEmployee.GetType(), "onclick", "ClosePanel(); $('.reviewtitle').css(\"display\", \"none\");$('#dvForm').modal();", true);
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments();
        ClientScript.RegisterStartupScript(ddlBranch.GetType(), "onchange", "ShowFilter();", true);
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();
        ClientScript.RegisterStartupScript(ddlBranch.GetType(), "onchange", "ShowFilter();", true);
    }
    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("employeecode");
            e.Row.Cells[2].Text = hrmlang.GetString("branch");
            e.Row.Cells[3].Text = hrmlang.GetString("department");
            e.Row.Cells[4].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkReview");
            lnkReview.Attributes.Add("title", hrmlang.GetString("viewappraisalreview"));
        }
    }

    #endregion
}