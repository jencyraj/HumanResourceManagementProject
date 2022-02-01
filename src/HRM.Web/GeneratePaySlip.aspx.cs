using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Ionic.Zip;

using HRM.BOL;
using HRM.BAL;

public partial class GeneratePaySlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ("" + Session["EMPID"] == "")
            Response.Redirect("signin.aspx");
        lblMsg.Text = "";
        lblErr.Text = "";
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("search");
        btnCancel.Text = hrmlang.GetString("cancel");
        btnGenerate.Text = hrmlang.GetString("generate");
        lbldownload.Text = "" + Request.QueryString["downloadpayslip"];

        string[] permissions = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "generatepayslip.aspx", true);
        bool bPermission = false;

        if (permissions != null)
        {
            foreach (string x in permissions)
            {
                if ("" + x == "Y")
                {
                    bPermission = true;
                }
            }

            if (!bPermission || "" + Request.QueryString["download"] == "1")
            {
                permissions = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "generatepayslip.aspx?download=1");
                foreach (string x in permissions)
                {
                    if ("" + x == "Y")
                    {
                        bPermission = true;
                    }
                }

                if (bPermission)
                {
                    PnlPayslip.Visible = false;
                    dvGen.Visible = true;
                    ddlMonth.Visible = true;
                    ddlYear.Visible = true;
                    btnGenerate.Visible = true;
                    if ("" + Session["LanguageId"] == "en-US")
                        dvGen.Style.Add("text-align", "left");
                }
            }

        }

        else
        {
            Util.ShowNoPermissionPage();
        }

        if (!IsPostBack)
        {
            GetCompanyDetails();
            GetMonthList();
            GetDropDownValues();
            for (int i = 1990; i <= DateTime.Today.Year; i++)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
        }
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
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("employeecode");
            e.Row.Cells[2].Text = hrmlang.GetString("branch");
            e.Row.Cells[3].Text = hrmlang.GetString("department");
            e.Row.Cells[4].Text = hrmlang.GetString("designation");
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
    protected void btnSearch_Click(object sender, EventArgs e)
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
        {
            btnGenerate.Visible = false;
            ddlMonth.Visible = false;
            ddlYear.Visible = false;
            dvGen.Visible = false;
            lblErr.Text = "No records found";
        }
        else
        {
            btnGenerate.Visible = true;
            ddlMonth.Visible = true;
            ddlYear.Visible = true;
            dvGen.Visible = true;
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

        string sPath =Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["SALARYSLIP"]) + "slipby" + User.Identity.Name + "\\";
        if (System.IO.Directory.Exists(sPath))
        {
            System.IO.Directory.Delete(sPath, true);
        }
        System.IO.Directory.CreateDirectory(sPath);
        string EmployeeIds = string.Empty;
        string sEmp = "";

        foreach (GridViewRow row in gvEmployee.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk.Checked == true)
            {
                HiddenField hfEmployeeId = (HiddenField)row.FindControl("hfEmployeeId");
                if (!string.IsNullOrEmpty(EmployeeIds))
                {
                    EmployeeIds = string.Format("{0},{1}", EmployeeIds, hfEmployeeId.Value);
                }
                else
                {
                    EmployeeIds = hfEmployeeId.Value;
                }


            }
        }
        if ("" + Request.QueryString["download"] != "")
            EmployeeIds = "" + Session["EMPID"];
        if (EmployeeIds == "")
        {
            ClientScript.RegisterStartupScript(btnGenerate.GetType(), "onclick", "alert('Please select an employee');", true);
            return;
        }


        new SalarySlip().CreateSlip(sPath, EmployeeIds, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue), chkSendEmail.Checked, out sEmp);

        //string retMsg = PDFGenerator.CreatePDF(sPath, EmployeeIds, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue), chkSendEmail.Checked, out sEmp);

        if (sEmp != "")
            ClientScript.RegisterStartupScript(btnGenerate.GetType(), "onclick", "alert('No payroll template has been set for the employees => " + sEmp + "');", true);
        else
        {
            lblMsg.Text = "Salary slip generated successfully.";
        }

 if ("" + Session["DOWNLOAD"] == "1")
            {
        System.IO.DirectoryInfo objDir = new DirectoryInfo(sPath);
        if (objDir.Exists)
        {
            if (objDir.GetFiles().Length > 0)
            {
                /*
                //Zip Files

                ZipFile zip = new ZipFile();

                // add this map file into the "images" directory in the zip archive
                DirectoryInfo f = new DirectoryInfo(sPath);
                FileInfo[] a = f.GetFiles();
                for (int i = 0; i < a.Length; i++)
                {
                    zip.AddFile(sPath + a[i].Name, "salaryslip");
                zip.Save(sPath + "salaryslip.zip");
                }
                 * */

                String FileName = "salaryslip.zip";

                using (ZipFile zip = new ZipFile())
                {

                    string[] files = Directory.GetFiles(sPath);
                    // add all those files to the ProjectX folder in the zip file
                    zip.AddFiles(files,false,"");
                    zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                    zip.Save(sPath + FileName);
                }

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                /// response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(sPath+ FileName);
                response.Flush();
                response.End();
}
            }
        }
    }

}