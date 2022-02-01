using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class LeaveBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        lblSubMsg.Text = "";
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("search");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            GetCompanyDetails();
            GetDropDownValues();
            if ("" + Session["ROLEID"] == "4")
            {
                pnlAll.Visible = false;
                GETBALANCE();

            }
        }
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
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvBalance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvBalance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvBalance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvBalance.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkView = (LinkButton)e.Row.FindControl("lnkView");
            lnkView.Text = hrmlang.GetString("view");
        }
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

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

    }

    private void GetDropDownValues()
    {
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();
        ddlDept.DataSource = objDept.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDept.DataBind();

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        ddlBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataBind();

        ddlDept.Items.Insert(0, lstItem);
        ddlBranch.Items.Insert(0, lstItem);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEmployee.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        if (ddlBranch.SelectedIndex > 0)
            objBOL.BranchID = int.Parse(ddlBranch.SelectedValue);

        if (ddlDept.SelectedIndex > 0)
            objBOL.DeptId = int.Parse(ddlDept.SelectedValue);

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();
        objBOL.EmpCode = txtEmpCode.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();

        if (dtEmp.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("norecordsfound");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        txtEmpCode.Text = "";
        txtfName.Text = "";
        txtlName.Text = "";
        txtmName.Text = "";
    }
    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWBAL")
        {
            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            Label lblName1 = (Label)gRow.FindControl("lblName1");
            Label lblName2 = (Label)gRow.FindControl("lblName2");
            Label lblName3 = (Label)gRow.FindControl("lblName3");

            LeaveBAL objBAL = new LeaveBAL();
            DataTable dtBalance = objBAL.GetLeaveBalance(Util.ToInt(e.CommandArgument));


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
                else
                    lblSubMsg.Text = hrmlang.GetString("norecordsfound");
            }
            else
            {
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    int nCount = dtBalance.Select("LeaveTypeID =" + dT.Rows[i]["LeaveTypeID"]).Length;
                    if (nCount == 0)
                    {
                        DataRow dRow = dtBalance.NewRow();
                        dRow["LeaveName"] = dT.Rows[i]["LeaveName"];
                        dRow["LeaveDays"] = dT.Rows[i]["LeaveDays"];
                        dRow["CarryOver"] = dT.Rows[i]["CarryOver"];
                        dRow["LeavesTaken"] = "0";
                        dRow["LeavesBalance"] = dT.Rows[i]["LeaveDays"];
                        dRow["PrevYearBalance"] = "0";
                        dtBalance.Rows.Add(dRow);
                    }
                }
                gvBalance.DataSource = dtBalance;
                gvBalance.DataBind();
            }



            lblEmp.Text = lblName1.Text + " " + lblName2.Text + " " + lblName3.Text;
            lblEmp.Text = lblEmp.Text.Trim().Replace("  ", " ");

            ClientScript.RegisterStartupScript(gvEmployee.GetType(), "onclick", "$('#dvLeave').modal();", true);
        }
    }

    private void GETBALANCE()
    {

        EmployeeBAL objeBAL = new EmployeeBAL();
        EmployeeBOL objEmp = new EmployeeBOL();

        objEmp.UserID = HttpContext.Current.User.Identity.Name;
        objEmp = objeBAL.Select(objEmp);
        DataTable dtEmp = objeBAL.Search(objEmp);
        DataView dView = dtEmp.DefaultView;

        if ("" + Session["ROLEID"] == "4")
        {
            dView.RowFilter = "EmployeeID=" + Session["EMPID"];
        }
        gvEmployee.DataSource = dView;
        gvEmployee.DataBind();


        LeaveBAL objBAL = new LeaveBAL();
        DataTable dtBalance = objBAL.GetLeaveBalance(objEmp.EmployeeID);


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
            else
                lblSubMsg.Text = hrmlang.GetString("norecordsfound");
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



        lblEmp.Text = objEmp.FirstName + " " + objEmp.MiddleName + " " + objEmp.LastName;
        lblEmp.Text = lblEmp.Text.Trim().Replace("  ", " ");

        ClientScript.RegisterStartupScript(gvEmployee.GetType(), "onclick", "$('#dvLeave').modal();", true);
    }
}