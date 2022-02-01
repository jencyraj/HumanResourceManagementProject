using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Data;
using HRM.BOL;
using HRM.BAL;
public partial class SalaryslipView : System.Web.UI.Page
{
    DataSet Ds; DataTable Dt; DataTable DED;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("search");
        btnCancel.Text = hrmlang.GetString("cancel");
        btnGenerate.Text = hrmlang.GetString("generate");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "SalaryslipView.aspx");
            GetCompanyDetails();
            GetMonthList();
            GetDropDownValues();
            for (int i = 1990; i <= DateTime.Today.Year; i++)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
          //  btnGenerate.Visible = true;
            ddlMonth.Visible = true;
            ddlYear.Visible = true;
            dvGen.Visible = true;

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select("" + Session["LanguageId"]);
        ddlMonth.DataSource = dt;
        ddlMonth.DataTextField = "MonthName";
        ddlMonth.DataValueField = "MonthID";
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int i = -1;
            e.Row.Cells[++i].Text = hrmlang.GetString("name");

            e.Row.Cells[++i].Text = hrmlang.GetString("basicsalary");
            e.Row.Cells[++i].Text = hrmlang.GetString("allowance");
            e.Row.Cells[++i].Text = hrmlang.GetString("overtime");
            e.Row.Cells[++i].Text = hrmlang.GetString("bonus");
            e.Row.Cells[++i].Text = hrmlang.GetString("commission");
            e.Row.Cells[++i].Text = hrmlang.GetString("deduction");
            e.Row.Cells[++i].Text = hrmlang.GetString("lossofpay");
            e.Row.Cells[++i].Text = hrmlang.GetString("totalsalary");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string EmpID = "" + DataBinder.Eval(e.Row.DataItem, "EmployeeID");
            DataTable dTable = new SalarySlip().SalarySlipData(EmpID, Util.ToInt(ddlMonth.SelectedValue), Util.ToInt(ddlYear.SelectedValue));
            if (dTable.Rows.Count > 0)
            {
                int i = 0;
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["BASIC"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["Allowance"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["OverTime"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["Bonus"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["Commission"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["Deduction"];
                e.Row.Cells[++i].Text = "" + dTable.Rows[0]["LOP"];
                e.Row.Cells[++i].Text = "" + (Util.ToDecimal("" + dTable.Rows[0]["BASIC"]) + Util.ToDecimal("" + dTable.Rows[0]["Allowance"]) + Util.ToDecimal("" + dTable.Rows[0]["OverTime"]) + Util.ToDecimal("" + dTable.Rows[0]["Bonus"]) + Util.ToDecimal("" + dTable.Rows[0]["Commission"]) - Util.ToDecimal("" + dTable.Rows[0]["Deduction"]) - Util.ToDecimal("" + dTable.Rows[0]["LOP"]));
            }
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEmployee.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEmployee.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEmployee.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEmployee.PagerSettings.LastPageText = hrmlang.GetString("last");
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
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEmployee.aspx");
    }
    private void Bind()
    {
        int Empid = 0;
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
        int dmon = int.Parse(ddlMonth.SelectedValue);
        int dyear = int.Parse(ddlYear.SelectedValue);
        DataTable dtEmp = objBAL.Search(objBOL);

        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();

        btnGenerate.Visible = dvGen.Visible = (dtEmp.Rows.Count != 0) ? true : false;
        if (dtEmp.Rows.Count == 0)
            lblErr.Text = "No records found";
           

          
           
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();

    }
    private void BindGrid(DataTable dt, GridView gv)
    {
        gv.DataSource = dt.Columns;
        gv.DataBind();
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
        if (e.CommandName == "DEL")
        {
            EmployeeBAL objBAL = new EmployeeBAL();
            objBAL.Delete(int.Parse(e.CommandArgument.ToString()), User.Identity.Name);
            lblMsg.Text = "Employee deleted successfully";
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
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Salaryslip" + DateTime.Now + ".xls"));
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvEmployee.AllowPaging = false;
        Bind();
        //Change the Header Row back to white color
        gvEmployee.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < gvEmployee.HeaderRow.Cells.Count; i++)
        {
            gvEmployee.HeaderRow.Cells[i].Style.Add("background-color", "#3c8dbc");
        }
        gvEmployee.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

       // Response.Clear();
       // Response.Buffer = true;
       // Response.ClearContent();
       // Response.ClearHeaders();
       // Response.Charset = "";
       // string FileName = "Salaryslip" + DateTime.Now + ".xls";
       // StringWriter strwritter = new StringWriter();
       // HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
       // Response.Cache.SetCacheability(HttpCacheability.NoCache);
       // Response.ContentType = "application/vnd.ms-excel";
       // Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
       //// gvEmployee.GridLines = GridLines.Both;
       // gvEmployee.HeaderStyle.Font.Bold = true;
       // gvEmployee.RenderControl(htmltextwrtter);
       // Response.Write(strwritter.ToString());
       // Response.End();
    }
    private DataTable GetTable()
    {
        DataTable dDedDT = new DataTable();
        dDedDT.Columns.Add("EmployeeId");
        dDedDT.Columns.Add("LOP");
        dDedDT.Columns.Add("diff");
        return dDedDT;
    }
    private DataTable GetLateValue1(int mon, int year)
    {
        DataTable dDedDT = GetTable(); DataRow dr = null;
        decimal dDed = 0;
        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();

        objBOL.Month = mon;
        objBOL.Year = year;
        objBOL.Reason = "LateByLeave";
        DataTable dT1 = objBAL.checklatebytime();
        if (dT1.Rows.Count > 0)
        {
            DataSet dSgetlbT = objBAL.Getlatebytype(objBOL);

            if (dSgetlbT.Tables[0].Rows.Count > 0)
            {
                DataTable dTdiff = dSgetlbT.Tables[0];
                DataTable DTregH = dSgetlbT.Tables[1];



                foreach (DataRow row in dTdiff.Rows)
                {
                    for (int i = 0; i < dTdiff.Rows.Count; i++)
                    {


                        int EmpId = Util.ToInt(row["EmployeeId"]);
                        dr = dDedDT.NewRow();
                        dr["EmployeeId"] = EmpId;

                        object tothr1 = dTdiff.Compute("Sum(diff)", "EmployeeId=" + EmpId + "");
                        dr["diff"] = tothr1.ToString();
                        dr["LOP"] = dDed;
                        dDedDT.Rows.Add(dr);

                    }


                }
                DataTable distinctTable = dDedDT.DefaultView.ToTable(true);


                foreach (DataRow row in distinctTable.Rows)
                {
                    for (int i = 0; i < distinctTable.Rows.Count; i++)
                    {
                        decimal hour = Util.ToInt(row["diff"]);

                        decimal RH = Util.ToDecimal(DTregH.Rows[0]["RegularHours"].ToString());

                        decimal toth = Util.ToDecimal(hour.ToString());
                        decimal totm = Util.ToDecimal(hour.ToString()) * 60;
                        decimal LV = decimal.Parse(dT1.Rows[0]["LossValue"].ToString());
                        if (dT1.Rows[0]["LateByType"].ToString() == "PM")
                        {
                            if (dT1.Rows[0]["LossType"].ToString() == "L")
                            {

                                if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                                {
                                    if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                                    {
                                        objBOL.Reason = "LateByLeave";
                                        objBAL.Save(objBOL);
                                    }
                                }
                            }
                            else if (dT1.Rows[0]["LossType"].ToString() == "LOP")
                            {

                                if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                                {
                                    if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                                    {
                                        dDedDT.Clear(); decimal dComm1 = 0;
                                        foreach (DataRow rowd in distinctTable.Rows)
                                        {
                                            for (int j = 0; j < distinctTable.Rows.Count; j++)
                                            {
                                                // int EmpId = Util.ToInt(row["EmployeeId"]);
                                                int EmpId = int.Parse(distinctTable.Rows[j]["EmployeeId"].ToString());
                                                object tothr1 = dTdiff.Compute("Sum(diff)", "EmployeeId=" + EmpId + "");
                                                dComm1 = Util.ToDecimal(tothr1.ToString()) * RH * LV;
                                                dDed = dComm1;
                                                dr = dDedDT.NewRow();
                                                dr["EmployeeId"] = EmpId;


                                                dr["diff"] = tothr1.ToString();
                                                dr["LOP"] = dDed;
                                                dDedDT.Rows.Add(dr);
                                            }

                                        }

                                    }
                                }

                            }
                        }
                        else
                        {
                            if (dT1.Rows[0]["LossType"].ToString() == "L")
                            {

                                if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                                {
                                    if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                                    {
                                        objBOL.Reason = "LateByLeave";
                                        objBAL.Save(objBOL);
                                    }
                                }
                            }
                            else if (dT1.Rows[0]["LossType"].ToString() == "LOP")
                            {
                                decimal dComm1 = 0;
                                if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                                {
                                    if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                                    {
                                        dDedDT.Clear();
                                        foreach (DataRow rowd in distinctTable.Rows)
                                        {
                                            for (int j = 0; j < distinctTable.Rows.Count; j++)
                                            {
                                                int EmpId = Util.ToInt(row["EmployeeId"]);
                                                //  int EmpId =int.Parse( distinctTable.Rows[j]["EmployeeId"].ToString());
                                                object tothr1 = dTdiff.Compute("Sum(diff)", "EmployeeId=" + EmpId + "");
                                                dComm1 = Util.ToDecimal(tothr1.ToString()) * RH * LV;
                                                dDed = dComm1;
                                                dr = dDedDT.NewRow();
                                                dr["EmployeeId"] = EmpId;


                                                dr["diff"] = tothr1.ToString();
                                                dr["LOP"] = dDed;
                                                dDedDT.Rows.Add(dr);
                                            }

                                        }
                                    }


                                }

                            }

                        }

                    }
                }
            }

        }

        return dDedDT;
    }
}