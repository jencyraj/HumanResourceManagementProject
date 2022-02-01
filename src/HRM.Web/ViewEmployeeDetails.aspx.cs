using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

using HRM.BOL;
using HRM.BAL;

public partial class ViewEmployeeDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            SetLangEmployee();
            lblEmpID.Text = "" + Request.QueryString["empid"];
            if (lblEmpID.Text != "")
                GetEmployeeDetails(Util.ToInt(lblEmpID.Text));
            else
            {
                EmployeeBAL objBAL = new EmployeeBAL();
                txtEmpCode.Text = objBAL.GetNextEmployeeCode();
            }
            txtEmpCode.Enabled = false;
        }
    }

    protected void gvDependent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("dependentname");
            e.Row.Cells[1].Text = hrmlang.GetString("relationship");
            e.Row.Cells[2].Text = hrmlang.GetString("dob");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            Label lblDOB = (Label)e.Row.FindControl("lblDOB");
            if ("" + rowView["DateOfBirth"] != "")
                lblDOB.Text = "" + rowView["DateOfBirth"];// DateTime.Parse("" + rowView["DateOfBirth"]).ToShortDateString();
        }
    }

    protected void gvImmigration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("documenttype");
            e.Row.Cells[1].Text = hrmlang.GetString("documentno");
            e.Row.Cells[2].Text = hrmlang.GetString("issuedate");
            e.Row.Cells[3].Text = hrmlang.GetString("expirydate");
            e.Row.Cells[4].Text = hrmlang.GetString("eligiblestatus");
            e.Row.Cells[5].Text = hrmlang.GetString("eligiblereviewdate");
            e.Row.Cells[6].Text = hrmlang.GetString("comments");
        }
    }

    protected void gvSkill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("skills");
            e.Row.Cells[1].Text = hrmlang.GetString("experienceinyears");
            e.Row.Cells[2].Text = hrmlang.GetString("comments");
        }
    }

    protected void gvEmg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("contactname");
            e.Row.Cells[1].Text = hrmlang.GetString("relationship");
            e.Row.Cells[2].Text = hrmlang.GetString("homephone");
            e.Row.Cells[3].Text = hrmlang.GetString("workphone");
        }
    }

    protected void gvExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("company");
            e.Row.Cells[1].Text = hrmlang.GetString("jobtitle");
            e.Row.Cells[2].Text = hrmlang.GetString("startdate");
            e.Row.Cells[3].Text = hrmlang.GetString("todate");
            e.Row.Cells[4].Text = hrmlang.GetString("comments");
        }
    }

    protected void gvEducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("level");
            e.Row.Cells[1].Text = hrmlang.GetString("university");
            e.Row.Cells[2].Text = hrmlang.GetString("college");
            e.Row.Cells[3].Text = hrmlang.GetString("specialization");
            e.Row.Cells[4].Text = hrmlang.GetString("passedyear");
            e.Row.Cells[5].Text = hrmlang.GetString("scoreperc");
            e.Row.Cells[6].Text = hrmlang.GetString("startdate");
            e.Row.Cells[7].Text = hrmlang.GetString("enddate");
        }
    }


    private void BindGrid(DataTable dt, GridView gv)
    {
        gv.DataSource = dt;
        gv.DataBind();
    }

    private void ClearGrid(GridView gView)
    {
        gView.DataSource = null;
        gView.DataBind();
    }

    private DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }

    private void GetEmployeeDetails(int EmpID)
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        try
        {
            txtUserID.Enabled = false;

            objBOL.EmployeeID = EmpID;
            objBOL = objBAL.Select(objBOL);

            lblBranch.Text = objBOL.Branch.Branch;
            lblDept.Text = objBOL.Department.DepartmentName;
            OrgDepartmentBOL orgBOL = new OrgDepartmentBOL();
            orgBOL.DeptID = objBOL.SubDeptID;

            DataTable dt1 = new OrgDepartmentsBAL().SelectAll(orgBOL);

            lblSubDepartment.Text = dt1.Rows[0]["departmentname"].ToString();
            lblRole.Text = objBOL.Roles.RoleName;
            lblDesgn.Text = objBOL.Designation.Designation;

            DataTable dtDept = new OrgDepartmentsBAL().SelectAll(Util.ToInt(lblCompanyID.Text));

            //if ("" + objBOL.SubDeptID != "")
            //    foreach (DataRow dR in dtDept.Rows)
            //        if ("" + dR["departmentid"] == "" + objBOL.SubDeptID)
            //            lblSubDepartment.Text = "" + dR["departmentname"];

            txtEmpCode.Text = objBOL.EmpCode;
            txtUserID.Text = objBOL.UserID;

            txtBiometricId.Text = objBOL.BiometricID;
            txtIrisId.Text = objBOL.IRISID;
            txtsalute.Text = objBOL.SaluteName;
            txtfName.Text = objBOL.FirstName.ToString();
            txtmName.Text = objBOL.MiddleName.ToString();
            txtlName.Text = objBOL.LastName.ToString();

            lblGender.Text = (objBOL.Gender == "F") ? hrmlang.GetString("female") : hrmlang.GetString("male");
            

            if (objBOL.DateOfBirth != null && objBOL.DateOfBirth != "" )
            {
                if (objBOL.DateOfBirth != null && objBOL.DateOfBirth != "" + DateTime.MinValue)
                {
                    txtDOB.Text = DateTime.Parse(objBOL.DateOfBirth).ToString("dd/MMM/yyyy");
                }
                else
                {
                    txtDOB.Text = DateTime.Parse(objBOL.DateOfBirth).ToString("dd/MMM/yyyy");
                }
                
            }

            switch (objBOL.MaritalStatus)
            {
                case "S":
                    lblMarried.Text = hrmlang.GetString("single");
                    break;
                case "M":
                    lblMarried.Text = hrmlang.GetString("married");
                    break;
                case "W":
                    lblMarried.Text = hrmlang.GetString("widowed");
                    break;
                case "D":
                    lblMarried.Text = hrmlang.GetString("divorced");
                    break;
                case "O":
                    lblMarried.Text = hrmlang.GetString("other");
                    break;
            }


            DataTable dt = new EmployeeBAL().SelectNationality("" + Session["LanguageId"]);
            DataRow[] dRows = dt.Select("NationalityCode='" + objBOL.NationalityCode + "'");
            if (dRows.Length > 0)
                lblNationality.Text = "" + dRows[0]["Nationality"];

            txtIDDesc.Text = objBOL.IDDesc;
            txtIDNo.Text = objBOL.IDNO;
            txtbldgrp.Text = objBOL.BloodGroup;
            lblStatus.Text = (objBOL.EmpStatus.ToUpper() == "C") ? hrmlang.GetString("active") : hrmlang.GetString("inactive");

            DataTable dTable = new EmplStatusBAL().Select().Tables[0];
            DataRow[] dRow = dTable.Select("EmplStatusID=" + objBOL.JobType);
            if (dRow.Length > 0)
                lblEmplStatus.Text = "" + dRow[0]["description"];


            if ("" + objBOL.JoiningDate != "")
                txtJoinDate.Text = DateTime.Parse(objBOL.JoiningDate).ToString("dd/MMM/yyyy");

            if ("" + objBOL.ProbationStartDate != "")
                txtProbStart.Text = DateTime.Parse(objBOL.ProbationStartDate).ToString("dd/MMM/yyyy");

            if ("" + objBOL.ProbationEndDate != "")
                txtProbEnd.Text = DateTime.Parse(objBOL.ProbationEndDate).ToString("dd/MMM/yyyy");

            txtAdd1.Text = objBOL.Address1;
            txtAdd2.Text = ("" + objBOL.Address2 != "") ? "," + objBOL.Address2 : "";
            txtCity.Text = ("" + objBOL.City != "") ? "," + objBOL.City : "";
            txtState.Text = objBOL.State;
            txtZipCode.Text = objBOL.ZipCode;

            dTable = new CountryBAL().Select("" + Session["LanguageId"]);
            dRow = dTable.Select("countryid='" + objBOL.CountryID + "'");
            if (dRow.Length > 0)
                lblCountry.Text = "" + dRow[0]["CountryName"];
            txtHPhone.Text = objBOL.HPhone;
            txtMobile.Text = objBOL.HMobile;
            txtWPhone.Text = objBOL.WPhone;
            txtWEmail.Text = objBOL.WEmail;
            txtEmail.Text = objBOL.HEmail;
            txtWebsite.Text = objBOL.Website;

            txtBank.Text = objBOL.BankName;
            txtBranch.Text = objBOL.BankBranch;
            txtAccNo.Text = objBOL.AccountNumber;
            txtOther.Text = objBOL.OtherBankDetails;

            if (objBOL.PhotoName != "")
            {
                imgPhoto.ImageUrl = "~/images/Employee/PHOTO/" + lblEmpID.Text + "/" + objBOL.PhotoName;
                lblImg.Text = objBOL.PhotoName;
            }
            DataSet dSet = objBAL.GetOtherDetails(EmpID);

            ///Dependents    - 0 
            BindGrid(dSet.Tables[0], gvDependent);
            ///Education     - 1
            BindGrid(dSet.Tables[1], gvEducation);
            ///EmgcyContacts - 2
            BindGrid(dSet.Tables[2], gvEmg);
            ///Experience    - 3
            BindGrid(dSet.Tables[3], gvExp);
            ///Immigration   - 4
            BindGrid(dSet.Tables[4], gvImmigration);
            ///Language      - 5
            BindGrid(dSet.Tables[5], gvLang);
            ///Skills        - 6
            BindGrid(dSet.Tables[6], gvSkill);

        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }


    private void SetLangEmployee()
    {
        rbtnDocType.Items.Clear();
        rbtnDocType.Items.Add(new ListItem(hrmlang.GetString("passport"), "Passport"));
        rbtnDocType.Items.Add(new ListItem(hrmlang.GetString("visa"), "visa"));

        ddlCompetency.Items.Clear();
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("select"), ""));
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("poor"), "P"));
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("basic"), "B"));
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("good"), "G"));
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("excellent"), "E"));
        ddlCompetency.Items.Add(new ListItem(hrmlang.GetString("mtongue"), "M"));

        ddlFluency.Items.Clear();
        ddlFluency.Items.Add(new ListItem(hrmlang.GetString("select"), ""));
        ddlFluency.Items.Add(new ListItem(hrmlang.GetString("rw"), "RW"));
        ddlFluency.Items.Add(new ListItem(hrmlang.GetString("rws"), "RWS"));



    }
}