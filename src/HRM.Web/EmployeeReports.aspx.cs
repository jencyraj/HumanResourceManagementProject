using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HRM.BOL;
using HRM.BAL;

public partial class Reports_EmployeeReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompanyDetails();
            GetDropDownValues();
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "employeereports.aspx");
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
        dRow[sFldName] =hrmlang.GetString("select");
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


        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("employeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("fname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("mname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("lname"));
        btnSearch.Text = hrmlang.GetString("search");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sParams = "";
       
        if (ddlBranch.SelectedIndex > 0)
           sParams +="&sp1=" + ddlBranch.SelectedValue;

        if (ddlDept.SelectedIndex > 0)
            sParams += "&sp2=" + ddlDept.SelectedValue;

        if (ddlStatus.SelectedIndex > 0)
            sParams += "&sp3=" + ddlStatus.SelectedValue;

        if (ddlRole.SelectedIndex > 0)
            sParams += "&sp4=" + ddlRole.SelectedValue;

        if (ddlDesgn.SelectedIndex > 0)
            sParams += "&sp5=" + ddlDesgn.SelectedValue;

        sParams += "&sp6=" + txtfName.Text.Trim();
        sParams += "&sp7=" + txtmName.Text.Trim();
        sParams += "&sp8=" + txtlName.Text.Trim();
        sParams += "&sp9=" + txtEmpCode.Text.Trim();

     
            string url = "HRMReports.aspx?rptname=EmployeeRpt&printtype=" + rbtnPrint.SelectedValue+sParams+"&rptcase=EMPL";
            string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
       
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
      
    }


    protected void btnbloodgrp_Report(object sender, EventArgs e)
    {
        string sParams = "";

        if (ddlBranch.SelectedIndex > 0)
            sParams += "&sp1=" + ddlBranch.SelectedValue;

        if (ddlDept.SelectedIndex > 0)
            sParams += "&sp2=" + ddlDept.SelectedValue;

        if (ddlStatus.SelectedIndex > 0)
            sParams += "&sp3=" + ddlStatus.SelectedValue;

        if (ddlRole.SelectedIndex > 0)
            sParams += "&sp4=" + ddlRole.SelectedValue;

        if (ddlDesgn.SelectedIndex > 0)
            sParams += "&sp5=" + ddlDesgn.SelectedValue;

        sParams += "&sp6=" + txtfName.Text.Trim();
        sParams += "&sp7=" + txtmName.Text.Trim();
        sParams += "&sp8=" + txtlName.Text.Trim();
        sParams += "&sp9=" + txtEmpCode.Text.Trim();


        string url = "HRMReports.aspx?rptname=EmployeeBloodGrpRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=EMPL";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
     
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
     
    }




    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();

    }



protected void btnView_Report(object sender, EventArgs e)
    {

       
        string sParams = "&sp1=" + ddlBranch.SelectedValue + "&sp2=" +ddlDept.SelectedValue + "&sp3=" +ddlStatus.SelectedValue + "&sp4=" +ddlRole.SelectedValue + "&sp5=" +ddlDesgn.SelectedValue + "&sp6=" +txtfName.Text + "&sp7=" +txtmName.Text  + "&sp8=" +txtlName.Text + "&sp9=" +txtEmpCode.Text ;
            string url = "HRMReports.aspx?rptname=EmployeeBenefitRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=EMPBRPT";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');"; 
         ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
}