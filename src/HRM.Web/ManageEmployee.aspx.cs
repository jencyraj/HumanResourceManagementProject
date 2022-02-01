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
using System.Text;

public partial class ManageEmployee : System.Web.UI.Page
{
    string M = ""; string Y = ""; string D = ""; string De = ""; string sn = ""; string ot = ""; string empcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            SetCalendarLanguage();
            SetLangEmployee();
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "employees.aspx");
            GetCompanyDetails();
            GetDropDownValues();
            lblEmpID.Text = "" + Request.QueryString["empid"];
            if (lblEmpID.Text != "")
                GetEmployeeDetails(Util.ToInt(lblEmpID.Text));
            else
            {
                // EmployeeBAL objBAL = new EmployeeBAL();
                txtEmpCode.Text = hrmlang.GetString("newemployee");// objBAL.GetNextEmployeeCode();
            }
            txtEmpCode.Enabled = false;
        }
        if (!string.IsNullOrEmpty(txtPassword.Text))
        {
            txtPassword.Attributes.Add("value", txtPassword.Text);
        }
        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "employees.aspx");
        string[] permissions = (string[])ViewState["permissions"];
        btnSave.Visible = (permissions[0] == "Y" || permissions[1] == "Y") ? true : false;
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteimmigration"));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteskill"));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletecontact"));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteexperience"));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteeducationdetails"));
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

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataSource = ReturnDT("BranchID", "Branch", dt);
        ddlBranch.DataBind();

        OrgDesignationBAL objDesgn = new OrgDesignationBAL();
        dt = objDesgn.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDesgn.DataSource = ReturnDT("DesignationID", "Designation", dt);
        ddlDesgn.DataBind();

        EmployeeBAL objEmp = new EmployeeBAL();
        dt = objEmp.SelectNationality("" + Session["LanguageId"]);
        ddlNationality.DataSource = ReturnDT("NationalityCode", "Nationality", dt);
        ddlNationality.DataBind();

        CountryBAL objCn = new CountryBAL();
        dt = objCn.Select("" + Session["LanguageId"]);
        ddlCountry.DataSource = ReturnDT("CountryCode", "CountryName", dt);
        ddlCountry.DataBind();

        ddlIssueCountry.DataSource = ReturnDT("CountryCode", "CountryName", dt);
        ddlIssueCountry.DataBind();

        RoleBAL objRole = new RoleBAL();
        dt = objRole.SelectAll(0);
        ddlRole.DataSource = ReturnDT("RoleID", "RoleName", dt);
        ddlRole.DataBind();

        EmplStatusBAL objStatus = new EmplStatusBAL();
        DataSet dSet = objStatus.Select();
        ddlJobType.DataSource = ReturnDT("EmplStatusID", "Description", dSet.Tables[0]);
        ddlJobType.DataBind();

        EduLevelBAL objLevel = new EduLevelBAL();
        dSet = objLevel.Select();
        ddlEduLevel.DataSource = ReturnDT("EduLevelID", "EduLevelName", dSet.Tables[0]);
        ddlEduLevel.DataBind();
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

    private void SetCalendarLanguage()
    {
        /* ctlCalendardob.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalJoin.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalProbStart.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalProbEnd.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalDepDob.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalStartDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalEndDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalFromDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalToDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalExpDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalIssueDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
         ctlCalReviewDate.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;*/
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

            ddlBranch.SelectedValue = objBOL.BranchID.ToString();
            GetDepartments();
            ddlDept.SelectedValue = objBOL.DeptId.ToString();
            GetSubDepartments();
            ddlSubDept.ClearSelection();
            ddlSubDept.SelectedValue = objBOL.SubDeptID.ToString();
            ddlRole.SelectedValue = objBOL.RoleID.ToString();
            ddlDesgn.SelectedValue = objBOL.DesgnID.ToString();
            txtEmpCode.Text = objBOL.EmpCode;
            txtUserID.Text = objBOL.UserID;
            txtPassword.Attributes.Add("value", objBOL.Password);
            ddlsalute.SelectedItem.Text = objBOL.SaluteName.ToString();
            txtbloodgrp.Text = objBOL.BloodGroup;
            txtBiometricId.Text = objBOL.BiometricID;
            txtIrisId.Text = objBOL.IRISID;
            ddlsalute.SelectedValue = objBOL.SaluteName;
            txtfName.Text = objBOL.FirstName.ToString();
            txtmName.Text = objBOL.MiddleName.ToString();
            txtlName.Text = objBOL.LastName.ToString();

            if ("" + objBOL.Gender != "")
                rbtnGender.SelectedValue = objBOL.Gender;


            if ("" + objBOL.DateOfBirth != "" && objBOL.DateOfBirth != "" + DateTime.MinValue)
                ctlCalendardob.SelectedCalendareDate = DateTime.Parse(objBOL.DateOfBirth);//.ToShortDateString());
            //txtDOB.Text = objBOL.DateOfBirth.ToShortDateString();


            ddlMarital.SelectedValue = objBOL.MaritalStatus;
            ddlNationality.SelectedValue = objBOL.NationalityCode;
            txtIDDesc.Text = objBOL.IDDesc;
            txtIDNo.Text = objBOL.IDNO;

            ddlStatus.SelectedValue = objBOL.EmpStatus;
            ddlJobType.SelectedValue = objBOL.JobType;

            // if (objBOL.JoiningDate != null && objBOL.JoiningDate != DateTime.MinValue)
            if ("" + objBOL.JoiningDate != "")
                ctlCalJoin.SelectedCalendareDate = DateTime.Parse(objBOL.JoiningDate);
            //txtJoinDate.Text = objBOL.JoiningDate.ToShortDateString();

            //if (objBOL.ProbationStartDate != null && objBOL.ProbationStartDate != DateTime.MinValue)
            if ("" + objBOL.ProbationStartDate != "")
                ctlCalProbStart.SelectedCalendareDate = DateTime.Parse(objBOL.ProbationStartDate);
            //txtProbStart.Text = objBOL.ProbationStartDate.ToShortDateString();

            // if (objBOL.ProbationEndDate != null && objBOL.ProbationEndDate != DateTime.MinValue)
            if ("" + objBOL.ProbationEndDate != "")
                ctlCalProbEnd.SelectedCalendareDate = DateTime.Parse(objBOL.ProbationEndDate);
            // txtProbEnd.Text = objBOL.ProbationEndDate.ToShortDateString();

            txtAdd1.Text = objBOL.Address1;
            txtAdd2.Text = objBOL.Address2;
            txtCity.Text = objBOL.City;
            txtState.Text = objBOL.State;
            txtZipCode.Text = objBOL.ZipCode;
            ddlCountry.SelectedValue = objBOL.CountryID.ToString();
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
                //imgPhoto.ImageUrl = ConfigurationManager.AppSettings["ROOTURL"] + "images/Employee/PHOTO/" + lblEmpID.Text + "/" + objBOL.PhotoName;
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
            ltHdr.Text = hrmlang.GetString("editemployeedetails");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
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
            if (ctlCalJoin.getGregorianDateText == "")
            {
                lblErr.Text =hrmlang.GetString("plsselectjoindate");
                return;
            }
            Save();
            UploadImage();
            Clear();
            lblMsg.Text = hrmlang.GetString("employeedetailssaved"); ;

        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    public System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight, int maxWidth)
    {
        var ratio = (double)maxHeight / image.Height;

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        if (newWidth > maxWidth)
        {
            decimal wratio = Math.Round((decimal)((decimal)maxWidth / (decimal)image.Width), 2);
            wratio = (wratio > 0) ? wratio : 1;
            newWidth = (int)(image.Width * wratio);
        }

        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }

    private void UploadImage()
    {
        try
        {
            if (fpPhoto.HasFile)
            {
                string sFolder = "" + ConfigurationManager.AppSettings["EMPPHOTO"] + lblEmpID.Text;
                sFolder = Server.MapPath(sFolder);
                if (!Directory.Exists(sFolder))
                    Directory.CreateDirectory(sFolder);

                //  fpPhoto.SaveAs(sFolder + "\\" + fpPhoto.FileName);
                string sFile = sFolder + "\\" + fpPhoto.FileName;
                string sExtn = Path.GetExtension(fpPhoto.PostedFile.FileName).Replace(".", "");

                //Remove OLD Photo
                if (File.Exists(sFolder + "\\" + lblImg.Text))
                    File.Delete(sFolder + "\\" + lblImg.Text);
                //Resize Image
                Bitmap bmpPostedImage = new Bitmap(fpPhoto.PostedFile.InputStream);
                System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 135, 115);
                switch (sExtn.ToUpper())
                {
                    case "PNG":
                        objImage.Save(sFile, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "JPG":
                    case "JPEG":
                        objImage.Save(sFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "GIF":
                        objImage.Save(sFile, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "BMP":
                        objImage.Save(sFile, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }
                imgPhoto.ImageUrl = "~/images/Employee/PHOTO/" + lblEmpID.Text + "/" + fpPhoto.FileName;
                //                imgPhoto.ImageUrl = ConfigurationManager.AppSettings["ROOTURL"] + "images/Employee/PHOTO/" + lblEmpID.Text + "/" + fpPhoto.FileName;
                EmployeeBAL objBAL = new EmployeeBAL();
                objBAL.UpdatePhoto(Util.ToInt(lblEmpID.Text), fpPhoto.FileName);
            }
        }
        catch (Exception ex)
        {
            lblErr.Text = hrmlang.GetString("photouploadfailed.") + ex.Message;
        }
    }

    private void Save()
    {
        EmployeeBOL objEmp = new EmployeeBOL();
        EmployeeBAL objBAL = new EmployeeBAL();

        try
        {

            objEmp.EmployeeID = Util.ToInt(lblEmpID.Text);
            DataTable ID = objBAL.GetCount();
            if (objEmp.EmployeeID == 0)
            {
                EmpCodeSettBAL objBAL1 = new EmpCodeSettBAL();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = objBAL1.SelectAll();
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {


                    string temp = Convert.ToString(row["CodeItem"]);
                    switch (temp)
                    {
                        case "Month":

                            M = DateTime.Now.ToString("MMM");
                            break;

                        case "Year":

                            Y = DateTime.Now.Year.ToString();
                            Y = Y.Substring(Y.Length - 2);
                            break;

                        case "Department":

                            OrgDepartmentsBAL objBAL2 = new OrgDepartmentsBAL();
                            DataTable dt1 = new DataTable();

                            dt1 = objBAL2.SelectDepartmentsByBranchID(Util.ToInt(ddlBranch.SelectedValue));
                            if (dt1.Rows.Count > 0)
                            {

                                D = dt1.Rows[0]["DeptCode"].ToString();
                                D = D.Substring(D.Length - 2);
                            }

                            break;

                        case "Designation":

                            OrgDesignationBAL objBAL3 = new OrgDesignationBAL();
                            DataTable dt2 = new DataTable();

                            dt2 = objBAL3.Selectdesgn(Util.ToInt(ddlDesgn.SelectedValue));
                            if (dt2.Rows.Count > 0)
                            {

                                De = dt2.Rows[0]["DesgnCode"].ToString();
                                De = De.Substring(De.Length - 3);
                            }

                            break;

                        case "SerialNo":

                            EmpCodeSettBAL objBAL5 = new EmpCodeSettBAL();
                            DataTable dt5 = new DataTable();
                            DataSet d1 = new DataSet();
                            d1 = objBAL5.GetNextEmployeeCode("SerialNo", "");
                            dt5 = d1.Tables[0];
                            if (dt5.Rows.Count > 0)
                            {

                                sn = dt5.Rows[0]["EMPCODE"].ToString();
                                sn = sn.Substring(sn.Length - 2);
                            }

                            break;

                        case "OtherText":

                            EmpCodeSettBAL objBAL4 = new EmpCodeSettBAL();
                            DataTable dt3 = new DataTable();
                            DataSet d = new DataSet();
                            d = objBAL4.GetNextEmployeeCode("OtherText", "");
                            dt3 = d.Tables[0];
                            if (dt3.Rows.Count > 0)
                            {

                                ot = dt3.Rows[0]["EMPCODE"].ToString();
                                ot = ot.Substring(ot.Length - 2);
                            }



                            break;

                    }


                }

                empcode = new StringBuilder().Append(Y).Append(M).Append(D).Append(De).Append(sn).Append(ot).ToString();
                int count = Util.ToInt(ID.Rows[0]["ID"].ToString()) + 1;
                //int i = 01;
                //empcode += "" + i.ToString();
                txtEmpCode.Text = empcode + count;
            }
            else
                empcode = txtEmpCode.Text;

            objEmp.BranchID = Util.ToInt(ddlBranch.SelectedValue);
            objEmp.DeptId = Util.ToInt(ddlDept.SelectedValue);
            objEmp.SubDeptID = Util.ToInt(ddlSubDept.SelectedValue);
            objEmp.DesgnID = Util.ToInt(ddlDesgn.SelectedValue);
            objEmp.RoleID = Util.ToInt(ddlRole.SelectedValue);
            objEmp.CountryID = ddlCountry.SelectedValue;
            objEmp.UserID = txtUserID.Text.Trim();
            objEmp.Password = txtPassword.Text.Trim();
            objEmp.BiometricID = txtBiometricId.Text.Trim();
            objEmp.IRISID = txtIrisId.Text.Trim();
            txtPassword.Attributes.Add("value", objEmp.Password);
            objEmp.EmpStatus = ddlStatus.SelectedValue;
            objEmp.EmpCode = txtEmpCode.Text;
            objEmp.IDDesc = txtIDDesc.Text.Trim();
            objEmp.IDNO = txtIDNo.Text.Trim();
            objEmp.FirstName = txtfName.Text.Trim();
            objEmp.MiddleName = txtmName.Text.Trim();
            objEmp.LastName = txtlName.Text.Trim();
            objEmp.Gender = rbtnGender.SelectedValue;
            objEmp.BloodGroup = txtbloodgrp.Text;
            objEmp.SaluteName = ddlsalute.SelectedItem.Text;
            objEmp.Address1 = txtAdd1.Text.Trim();
            objEmp.Address2 = txtAdd2.Text.Trim();
            objEmp.City = txtCity.Text.Trim();
            objEmp.State = txtState.Text.Trim();
            objEmp.Country = ddlCountry.SelectedItem.Text;
            objEmp.ZipCode = txtZipCode.Text.Trim();
            objEmp.HPhone = txtHPhone.Text.Trim();
            objEmp.HMobile = txtMobile.Text.Trim();
            objEmp.HEmail = txtEmail.Text.Trim();
            objEmp.Website = txtWebsite.Text.Trim();
            objEmp.NationalityCode = ddlNationality.SelectedValue;
            objEmp.MaritalStatus = ddlMarital.SelectedValue;
            objEmp.WPhone = txtWPhone.Text.Trim();
            objEmp.WEmail = txtWEmail.Text.Trim();
            objEmp.BankName = txtBank.Text.Trim();
            objEmp.BankBranch = txtBranch.Text.Trim();
            objEmp.OtherBankDetails = txtOther.Text.Trim();
            objEmp.AccountNumber = txtAccNo.Text.Trim();
            objEmp.JobType = ddlJobType.SelectedValue;
            objEmp.Status = "Y";
            objEmp.CreatedBy = User.Identity.Name;

            if (ctlCalendardob.getGregorianDateText != "")
                objEmp.DateOfBirth = Util.RearrangeDateTime(ctlCalendardob.getGregorianDateText);

            objEmp.DateOfBirthAR = ctlCalendardob.getHijriDateText;


            objEmp.JoiningDate = Util.RearrangeDateTime(ctlCalJoin.getGregorianDateText);
            objEmp.ProbationStartDate = Util.RearrangeDateTime(ctlCalProbStart.getGregorianDateText);
            objEmp.ProbationEndDate = Util.RearrangeDateTime(ctlCalProbEnd.getGregorianDateText);

            if (lblEmpID.Text == "")
                lblEmpID.Text = "" + objBAL.Save(objEmp);
            else
                objBAL.Save(objEmp);

            if (lblEmpID.Text != "")
            {
                SaveEmergenyContacts();
                SaveDependents();
                SaveEducation();
                SaveExperience();
                SaveSkills();
                SaveLanguage();
                SaveImmigration();
            }
            ClientScript.RegisterStartupScript(btnSave.GetType(), "onclick", "alert('Employee Details Saved Successfully.');", true);
            GetEmployeeDetails(int.Parse(lblEmpID.Text));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveEmergenyContacts()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvEmg.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.EmgContactID = Util.ToInt(((Label)gRow.FindControl("lblEID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.ContactName = Util.CleanString(gRow.Cells[0].Text);
                objBOL.Relationship = Util.CleanString(gRow.Cells[1].Text);
                objBOL.HPhone = Util.CleanString(gRow.Cells[2].Text);
                objBOL.WPhone = Util.CleanString(gRow.Cells[3].Text);
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveEmergencyContacts(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveDependents()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvDependent.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.DependentID = Util.ToInt(((Label)gRow.FindControl("lblDID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.DependentName = Util.CleanString(gRow.Cells[0].Text);
                objBOL.Relationship = Util.CleanString(gRow.Cells[1].Text);
                objBOL.DateOfBirth = ((Label)gRow.FindControl("lblDOB")).Text;// Util.ToDateTime(((Label)gRow.FindControl("lblDOB")).Text);
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveDependents(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveEducation()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvEducation.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.EducationID = Util.ToInt(((Label)gRow.FindControl("lblEdID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.EduLevel = ((Label)gRow.FindControl("EduLevel")).Text;
                objBOL.University = Util.CleanString(gRow.Cells[1].Text);
                objBOL.College = gRow.Cells[2].Text;
                objBOL.Specialization = gRow.Cells[3].Text;
                objBOL.PassedYear = Util.ToInt(gRow.Cells[4].Text);
                objBOL.ScorePercentage = gRow.Cells[5].Text;
                objBOL.StartDate = gRow.Cells[6].Text;
                objBOL.EndDate = gRow.Cells[7].Text;
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveEducation(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveExperience()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvExp.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.ExpID = Util.ToInt(((Label)gRow.FindControl("lblExID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.Company = Util.CleanString(gRow.Cells[0].Text);
                objBOL.JobTitle = Util.CleanString(gRow.Cells[1].Text);
                objBOL.FromDate = gRow.Cells[2].Text;
                objBOL.ToDate = gRow.Cells[3].Text;
                objBOL.Comments = ((Label)gRow.FindControl("lblComm")).Text;
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveExperience(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveSkills()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvSkill.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.SkillID = Util.ToInt(((Label)gRow.FindControl("lblSID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.Description = Util.CleanString(gRow.Cells[0].Text);
                objBOL.ExpinYears = Util.CleanString(gRow.Cells[1].Text);
                objBOL.Comments = ((Label)gRow.FindControl("lblComm")).Text;
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveSkills(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveLanguage()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvLang.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.LangID = Util.ToInt(((Label)gRow.FindControl("lblLID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.LanguageName = Util.CleanString(gRow.Cells[0].Text);
                objBOL.Fluency = ((Label)gRow.FindControl("lblFy")).Text;
                objBOL.Competency = ((Label)gRow.FindControl("lblCy")).Text;
                objBOL.Comments = ((Label)gRow.FindControl("lblComm")).Text;
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveLanguage(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveImmigration()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = null;

        try
        {
            foreach (GridViewRow gRow in gvImmigration.Rows)
            {
                objBOL = new EmployeeBOL();
                objBOL.ImmigrationId = Util.ToInt(((Label)gRow.FindControl("lblIID")).Text);
                objBOL.EmployeeID = Util.ToInt(lblEmpID.Text);
                objBOL.IssuedCountryID = ((Label)gRow.FindControl("lblCnID")).Text;
                objBOL.DocType = Util.CleanString(gRow.Cells[0].Text);
                objBOL.DocNo = Util.CleanString(gRow.Cells[1].Text);
                objBOL.IssueDate = Util.CleanString(gRow.Cells[2].Text);
                objBOL.ExpiryDate = Util.CleanString(gRow.Cells[3].Text);
                objBOL.EligibleStatus = Util.CleanString(gRow.Cells[4].Text);
                objBOL.EligibleReviewDate = Util.CleanString(gRow.Cells[5].Text);
                objBOL.Comments = Util.CleanString(gRow.Cells[6].Text);
                objBOL.Status = "Y";
                objBOL.CreatedBy = User.Identity.Name;

                objBAL.SaveImmigration(objBOL);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Clear()
    {
        if ("" + Request.QueryString["empid"] != "")
            Response.Redirect("ManageEmployee.aspx");
        else
        {
            lblEmpID.Text = "";
            ddlBranch.SelectedIndex = 0;
            ddlDept.Items.Clear();
            ddlSubDept.Items.Clear();
            ddlDesgn.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtBiometricId.Text = "";
            txtIrisId.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtEmpCode.Text = "";
            txtIDDesc.Text = "";
            txtIDNo.Text = "";
            txtfName.Text = "";
            txtmName.Text = "";
            txtlName.Text = "";
            rbtnGender.SelectedIndex = 0;
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipCode.Text = "";
            txtHPhone.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            ddlNationality.SelectedIndex = 0;
            ddlMarital.SelectedIndex = 0;
            txtWPhone.Text = "";
            txtWEmail.Text = "";
            txtBank.Text = "";
            txtBranch.Text = "";
            txtOther.Text = "";
            txtAccNo.Text = "";

            txtDOB.Text = "";
            txtJoinDate.Text = "";
            txtProbStart.Text = "";
            txtProbEnd.Text = "";

            SetCalendarLanguage();
            ctlCalendardob.SelectedCalendareDate = DateTime.Today;
            ctlCalJoin.SelectedCalendareDate = DateTime.Today;
            ctlCalProbStart.SelectedCalendareDate = DateTime.Today;
            ctlCalProbEnd.SelectedCalendareDate = DateTime.Today;

            ClearGrid(gvEmg);
            ClearGrid(gvDependent);
            ClearGrid(gvEducation);
            ClearGrid(gvExp);
            ClearGrid(gvImmigration);
            ClearGrid(gvLang);
            ClearGrid(gvSkill);
        }
    }

    protected void btnSaveEmplStatus_Click(object sender, EventArgs e)
    {
        EmplStatusBAL objBAL = new EmplStatusBAL();
        objBAL.Save("0", txtType.Text, User.Identity.Name);
        ClientScript.RegisterStartupScript(btnSaveEmplStatus.GetType(), "onclick", "alert('Data saved successfully');", true);

        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        EmplStatusBAL objStatus = new EmplStatusBAL();
        ddlJobType.DataSource = objStatus.Select();
        ddlJobType.DataBind();
        ddlJobType.Items.Insert(0, lstItem);

        txtType.Text = "";
    }

    protected void btnSaveLevel_Click(object sender, EventArgs e)
    {
        EduLevelBAL objBAL = new EduLevelBAL();
        objBAL.Save("0", txtLevel.Text, User.Identity.Name);
        ClientScript.RegisterStartupScript(btnSaveLevel.GetType(), "onclick", "alert('Data saved successfully');   $('.educationdt').sliAlwown();", true);

        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        ddlEduLevel.DataSource = objBAL.Select();
        ddlEduLevel.DataBind();
        ddlEduLevel.Items.Insert(0, lstItem);

        txtLevel.Text = "";

    }

    #region EMERGENCY CONTACTS

    private DataTable GetEmergencyTable()
    {
        DataTable dtEmg = new DataTable("EMG");
        dtEmg.Columns.Add("EmgContactID");
        dtEmg.Columns.Add("EmployeeID");
        dtEmg.Columns.Add("ContactName");
        dtEmg.Columns.Add("Relationship");
        dtEmg.Columns.Add("HPhone");
        dtEmg.Columns.Add("WPhone");

        return dtEmg;
    }

    protected void btnSaveEmg_Click(object sender, EventArgs e)
    {
        DataTable dtEmg = GetEmergencyTable();
        DataRow dRow = null;

        if ("" + ViewState["editemdtrow"] == "")
        {
            foreach (GridViewRow gRow in gvEmg.Rows)
            {
                dRow = dtEmg.NewRow();

                dRow["EmgContactID"] = Util.ToInt(((Label)gRow.FindControl("lblEID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["ContactName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["Relationship"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["HPhone"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["WPhone"] = Util.CleanString(gRow.Cells[3].Text);

                dtEmg.Rows.Add(dRow);
            }

            dRow = dtEmg.NewRow();

            dRow["EmgContactID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["ContactName"] = txtEmgName.Text.Trim();
            dRow["Relationship"] = txtRelation.Text.Trim();
            dRow["HPhone"] = txtEmgHPhone.Text.Trim();
            dRow["WPhone"] = txtEmgWPhone.Text.Trim();

            dtEmg.Rows.Add(dRow);

            gvEmg.DataSource = dtEmg;
            gvEmg.DataBind();
        }
        else
        {
            GridViewRow gRow = gvEmg.Rows[int.Parse("" + ViewState["editemdtrow"])];
            gRow.Cells[0].Text = txtEmgName.Text.Trim();
            gRow.Cells[1].Text = txtRelation.Text.Trim();
            gRow.Cells[2].Text = txtEmgHPhone.Text.Trim();
            gRow.Cells[3].Text = txtEmgWPhone.Text.Trim();
        }
        txtEmgName.Text = "";
        txtRelation.Text = "";
        txtEmgWPhone.Text = "";
        txtEmgHPhone.Text = "";
        ViewState["editemdtrow"] = "";
    }

    protected void gvEmg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            txtEmgName.Text = Util.CleanString(gRow.Cells[0].Text);
            txtRelation.Text = Util.CleanString(gRow.Cells[1].Text);
            txtEmgHPhone.Text = Util.CleanString(gRow.Cells[2].Text);
            txtEmgWPhone.Text = Util.CleanString(gRow.Cells[3].Text);
            ViewState["editemdtrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#emgcontact').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblEID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteEmergencyContact(nID, User.Identity.Name);

            DataTable dT = GetEmergencyTable();
            foreach (GridViewRow gRow in gvEmg.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();

                dRow["EmgContactID"] = ((Label)gRow.FindControl("lblEID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["ContactName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["Relationship"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["HPhone"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["WPhone"] = Util.CleanString(gRow.Cells[3].Text);

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvEmg);
        }
    }

    #endregion

    #region DEPENDENTS

    private DataTable GetDependTable()
    {
        DataTable dtDep = new DataTable("DEP");
        dtDep.Columns.Add("DependentID");
        dtDep.Columns.Add("EmployeeID");
        dtDep.Columns.Add("DependentName");
        dtDep.Columns.Add("Relationship");
        dtDep.Columns.Add("DateOfBirth");

        return dtDep;
    }

    protected void btnSaveDep_Click(object sender, EventArgs e)
    {
        DataTable dtDep = GetDependTable();
        DataRow dRow = null;
        if ("" + ViewState["editdtrow"] == "")
        {
            foreach (GridViewRow gRow in gvDependent.Rows)
            {
                dRow = dtDep.NewRow();

                dRow["DependentID"] = Util.ToInt(((Label)gRow.FindControl("lblDID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["DependentName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["Relationship"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["DateOfBirth"] = Util.CleanString(((Label)gRow.FindControl("lblDOB")).Text);

                dtDep.Rows.Add(dRow);
            }

            dRow = dtDep.NewRow();

            dRow["DependentID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["DependentName"] = txtDepName.Text.Trim();
            dRow["Relationship"] = txtDepRelation.Text.Trim();
            dRow["DateOfBirth"] = ctlCalDepDob.getGregorianDateText;// txtDepDob.Text.Trim();

            dtDep.Rows.Add(dRow);

            gvDependent.DataSource = dtDep;
            gvDependent.DataBind();
        }
        else
        {
            GridViewRow gRow = gvDependent.Rows[int.Parse("" + ViewState["editdtrow"])];
            gRow.Cells[0].Text = txtDepName.Text.Trim();
            gRow.Cells[1].Text = txtDepRelation.Text.Trim();
            ((Label)gRow.FindControl("lblDOB")).Text = ctlCalDepDob.getGregorianDateText;// txtDepDob.Text.Trim();
        }
        txtDepName.Text = "";
        txtDepRelation.Text = "";
        txtDepDob.Text = "";
        ctlCalDepDob.SelectedCalendareDate = DateTime.Today;
        ViewState["editdtrow"] = "";
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
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletedependent"));
        }
    }

    protected void gvDependent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            txtDepName.Text = Util.CleanString(gRow.Cells[0].Text);
            txtDepRelation.Text = Util.CleanString(gRow.Cells[1].Text);
            //txtDepDob.Text = Util.CleanString(((Label)gRow.FindControl("lblDOB")).Text);
            string sDate = Util.CleanString(((Label)gRow.FindControl("lblDOB")).Text);
            if (sDate != "")
            {
                // sDate = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                ctlCalDepDob.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            ViewState["editdtrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvdependent').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblDID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteDependent(nID, User.Identity.Name);

            DataTable dT = GetDependTable();
            foreach (GridViewRow gRow in gvEmg.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();

                dRow["DependentID"] = ((Label)gRow.FindControl("lblDID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["DependentName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["Relationship"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["DateOfBirth"] = Util.CleanString(gRow.Cells[2].Text);

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvDependent);
        }
    }

    #endregion

    #region EDUCATION

    private DataTable GetEducationTable()
    {

        DataTable dtEdu = new DataTable("EDU");

        dtEdu.Columns.Add("EducationID");
        dtEdu.Columns.Add("EmployeeID");
        dtEdu.Columns.Add("EduLevel");
        dtEdu.Columns.Add("EduLevelName");
        dtEdu.Columns.Add("University");
        dtEdu.Columns.Add("College");
        dtEdu.Columns.Add("Specialization");
        dtEdu.Columns.Add("PassedYear");
        dtEdu.Columns.Add("ScorePercentage");
        dtEdu.Columns.Add("StartDate");
        dtEdu.Columns.Add("EndDate");

        return dtEdu;
    }

    protected void btnSaveEdu_Click(object sender, EventArgs e)
    {
        DataTable dtEdu = GetEducationTable();
        DataRow dRow = null;
        if ("" + ViewState["editdtedrow"] == "")
        {
            foreach (GridViewRow gRow in gvEducation.Rows)
            {
                dRow = dtEdu.NewRow();

                dRow["EducationID"] = Util.ToInt(((Label)gRow.FindControl("lblEdID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["EduLevel"] = ((Label)gRow.FindControl("EduLevel")).Text;
                dRow["EduLevelName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["University"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["College"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["Specialization"] = Util.CleanString(gRow.Cells[3].Text);
                dRow["PassedYear"] = Util.CleanString(gRow.Cells[4].Text);
                dRow["ScorePercentage"] = Util.CleanString(gRow.Cells[5].Text);
                dRow["StartDate"] = Util.CleanString(gRow.Cells[6].Text);
                dRow["EndDate"] = Util.CleanString(gRow.Cells[7].Text);

                dtEdu.Rows.Add(dRow);
            }

            dRow = dtEdu.NewRow();

            dRow["EducationID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["EduLevel"] = ddlEduLevel.SelectedValue;
            dRow["EduLevelName"] = ddlEduLevel.SelectedItem.Text;
            dRow["University"] = txtUniversity.Text.Trim();
            dRow["College"] = txtCollege.Text.Trim();
            dRow["Specialization"] = txtSpec.Text.Trim();
            dRow["PassedYear"] = txtYear.Text.Trim();
            dRow["ScorePercentage"] = txtScore.Text.Trim();
            dRow["StartDate"] = ctlCalStartDate.getGregorianDateText;// txtStartDate.Text.Trim();
            dRow["EndDate"] = ctlCalEndDate.getGregorianDateText;// txtEndDate.Text.Trim();

            dtEdu.Rows.Add(dRow);

            gvEducation.DataSource = dtEdu;
            gvEducation.DataBind();
        }
        else
        {
            GridViewRow gRow = gvEducation.Rows[Util.ToInt("" + ViewState["editdtedrow"])];
            ((Label)gRow.FindControl("edulevel")).Text = ddlEduLevel.SelectedValue;
            gRow.Cells[0].Text = ddlEduLevel.SelectedItem.Text;
            gRow.Cells[1].Text = txtUniversity.Text.Trim();
            gRow.Cells[2].Text = txtCollege.Text.Trim();
            gRow.Cells[3].Text = txtSpec.Text.Trim();
            gRow.Cells[4].Text = txtYear.Text.Trim();
            gRow.Cells[5].Text = txtScore.Text.Trim();
            gRow.Cells[6].Text = ctlCalStartDate.getGregorianDateText;// txtStartDate.Text.Trim();
            gRow.Cells[7].Text = ctlCalEndDate.getGregorianDateText;//txtEndDate.Text.Trim();
        }
        ddlEduLevel.SelectedIndex = 0;
        txtUniversity.Text = "";
        txtCollege.Text = "";
        txtSpec.Text = "";
        txtYear.Text = "";
        txtScore.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ctlCalStartDate.SelectedCalendareDate = DateTime.Today;
        ctlCalEndDate.SelectedCalendareDate = DateTime.Today;

        ViewState["editdtedrow"] = "";
    }

    protected void gvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            ddlEduLevel.SelectedValue = ((Label)gRow.FindControl("EduLevel")).Text;
            txtUniversity.Text = Util.CleanString(gRow.Cells[1].Text);
            txtCollege.Text = Util.CleanString(gRow.Cells[2].Text);
            txtSpec.Text = Util.CleanString(gRow.Cells[3].Text);
            txtYear.Text = Util.CleanString(gRow.Cells[4].Text);
            txtScore.Text = Util.CleanString(gRow.Cells[5].Text);
            //txtStartDate.Text = Util.CleanString(gRow.Cells[6].Text);
            //txtEndDate.Text = Util.CleanString(gRow.Cells[7].Text);
            string sDate = Util.CleanString(gRow.Cells[6].Text);
            if (sDate != "")
            {
                ctlCalStartDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            sDate = Util.CleanString(gRow.Cells[7].Text);
            if (sDate != "")
            {
                ctlCalEndDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            ViewState["editdtedrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvEducation').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblEdID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteEducation(nID, User.Identity.Name);

            DataTable dT = GetEducationTable();
            foreach (GridViewRow gRow in gvEducation.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();

                dRow["EducationID"] = ((Label)gRow.FindControl("lblEdID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["EduLevel"] = ((Label)gRow.FindControl("edulevel")).Text;
                dRow["EduLevelName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["University"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["College"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["Specialization"] = Util.CleanString(gRow.Cells[3].Text);
                dRow["PassedYear"] = Util.CleanString(gRow.Cells[4].Text);
                dRow["ScorePercentage"] = Util.CleanString(gRow.Cells[5].Text);
                dRow["StartDate"] = Util.CleanString(gRow.Cells[6].Text);
                dRow["EndDate"] = Util.CleanString(gRow.Cells[7].Text);

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvEducation);
        }
    }

    #endregion

    #region EXPERIENCE

    private DataTable GetExperienceTable()
    {

        DataTable dtExp = new DataTable("EXP");

        dtExp.Columns.Add("ExpID");
        dtExp.Columns.Add("EmployeeID");
        dtExp.Columns.Add("Company");
        dtExp.Columns.Add("JobTitle");
        dtExp.Columns.Add("FromDate");
        dtExp.Columns.Add("ToDate");
        dtExp.Columns.Add("Comments");

        return dtExp;
    }

    protected void btnSaveExp_Click(object sender, EventArgs e)
    {
        DataTable dtExp = GetExperienceTable();
        DataRow dRow = null;
        if ("" + ViewState["editexdtrow"] == "")
        {
            foreach (GridViewRow gRow in gvExp.Rows)
            {
                dRow = dtExp.NewRow();

                dRow["ExpID"] = Util.ToInt(((Label)gRow.FindControl("lblExID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["Company"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["JobTitle"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["FromDate"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["ToDate"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["Comments"] = ((Label)gRow.FindControl("lblComm")).Text;

                dtExp.Rows.Add(dRow);
            }

            dRow = dtExp.NewRow();

            dRow["ExpID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["Company"] = txtCompany.Text.Trim();
            dRow["JobTitle"] = txtJobTitle.Text.Trim();
            dRow["FromDate"] = ctlCalFromDate.getGregorianDateText;// txtFromDate.Text.Trim();
            dRow["ToDate"] = ctlCalToDate.getGregorianDateText;// txtToDate.Text.Trim();
            dRow["Comments"] = txtComments.Text.Trim();

            dtExp.Rows.Add(dRow);

            gvExp.DataSource = dtExp;
            gvExp.DataBind();
        }
        else
        {
            GridViewRow gRow = gvExp.Rows[int.Parse("" + ViewState["editexdtrow"])];
            gRow.Cells[0].Text = txtCompany.Text.Trim();
            gRow.Cells[1].Text = txtJobTitle.Text.Trim();
            gRow.Cells[2].Text = ctlCalFromDate.getGregorianDateText;// txtFromDate.Text.Trim();
            gRow.Cells[3].Text = ctlCalToDate.getGregorianDateText;//txtToDate.Text.Trim();
            ((Label)gRow.FindControl("lblComm")).Text = txtComments.Text.Trim();
            ((HtmlAnchor)gRow.FindControl("lnkCom")).Title = txtComments.Text.Trim();
        }
        txtCompany.Text = "";
        txtJobTitle.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtComments.Text = "";

        ctlCalFromDate.SelectedCalendareDate = DateTime.Today;
        ctlCalToDate.SelectedCalendareDate = DateTime.Today;

        ViewState["editexdtrow"] = "";
    }

    protected void gvExp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            txtCompany.Text = Util.CleanString(gRow.Cells[0].Text);
            txtJobTitle.Text = Util.CleanString(gRow.Cells[1].Text);
            //  txtFromDate.Text = Util.CleanString(gRow.Cells[2].Text);
            // txtToDate.Text = Util.CleanString(gRow.Cells[3].Text);
            string sDate = Util.CleanString(gRow.Cells[2].Text);
            if (sDate != "")
            {

                ctlCalFromDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            sDate = Util.CleanString(gRow.Cells[3].Text);
            if (sDate != "")
            {
                // sDate = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                ctlCalToDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            txtComments.Text = ((Label)gRow.FindControl("lblComm")).Text;
            ViewState["editexdtrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvExperience').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblExID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteExperience(nID, User.Identity.Name);

            DataTable dT = GetExperienceTable();
            foreach (GridViewRow gRow in gvExp.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();

                dRow["ExpID"] = ((Label)gRow.FindControl("lblExID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["Company"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["JobTitle"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["FromDate"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["ToDate"] = Util.CleanString(gRow.Cells[3].Text);
                dRow["Comments"] = ((Label)gRow.FindControl("lblComm")).Text;

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvExp);
        }
    }

    #endregion

    #region SKILLS

    private DataTable GetSkillsTable()
    {

        DataTable dtSkill = new DataTable("SKILLS");

        dtSkill.Columns.Add("SkillID");
        dtSkill.Columns.Add("EmployeeID");
        dtSkill.Columns.Add("Description");
        dtSkill.Columns.Add("ExpinYears");
        dtSkill.Columns.Add("Comments");

        return dtSkill;
    }

    protected void btnSaveSkill_Click(object sender, EventArgs e)
    {
        DataTable dtSkill = GetSkillsTable();
        DataRow dRow = null;
        if ("" + ViewState["editskdtrow"] == "")
        {
            foreach (GridViewRow gRow in gvSkill.Rows)
            {
                dRow = dtSkill.NewRow();

                dRow["SkillID"] = Util.ToInt(((Label)gRow.FindControl("lblSID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["Description"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["ExpinYears"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["Comments"] = ((Label)gRow.FindControl("lblComm")).Text;

                dtSkill.Rows.Add(dRow);
            }

            dRow = dtSkill.NewRow();

            dRow["SkillID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["Description"] = txtSkill.Text.Trim();
            dRow["ExpinYears"] = txtSkillYear.Text.Trim();
            dRow["Comments"] = txtSkillComments.Text.Trim();

            dtSkill.Rows.Add(dRow);

            gvSkill.DataSource = dtSkill;
            gvSkill.DataBind();
        }
        else
        {
            GridViewRow gRow = gvSkill.Rows[int.Parse("" + ViewState["editskdtrow"])];
            gRow.Cells[0].Text = txtSkill.Text.Trim();
            gRow.Cells[1].Text = txtSkillYear.Text.Trim();
            ((Label)gRow.FindControl("lblComm")).Text = txtSkillComments.Text.Trim();
        }
        txtSkill.Text = "";
        txtSkillYear.Text = "";
        txtSkillComments.Text = "";
        ViewState["editskdtrow"] = "";
    }

    protected void gvSkill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            txtSkill.Text = Util.CleanString(gRow.Cells[0].Text);
            txtSkillYear.Text = Util.CleanString(gRow.Cells[1].Text);
            txtSkillComments.Text = ((Label)gRow.FindControl("lblComm")).Text;
            ViewState["editskdtrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvSkills').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblSID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteSkill(nID, User.Identity.Name);

            DataTable dT = GetSkillsTable();
            foreach (GridViewRow gRow in gvSkill.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();
                dRow["SkillID"] = ((Label)gRow.FindControl("lblSID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["Description"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["ExpinYears"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["Comments"] = ((Label)gRow.FindControl("lblComm")).Text;

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvSkill);
        }
    }

    #endregion

    #region LANGUAGES

    private DataTable GetLangTable()
    {

        DataTable dtSkill = new DataTable("LANG");

        dtSkill.Columns.Add("LangID");
        dtSkill.Columns.Add("EmployeeID");
        dtSkill.Columns.Add("LanguageName");
        dtSkill.Columns.Add("FluencyCode");
        dtSkill.Columns.Add("Fluency");
        dtSkill.Columns.Add("CompetencyCode");
        dtSkill.Columns.Add("Competency");
        dtSkill.Columns.Add("Comments");

        return dtSkill;
    }

    protected void btnSaveLang_Click(object sender, EventArgs e)
    {
        DataTable dtSkill = GetLangTable();
        DataRow dRow = null;
        if ("" + ViewState["editlndtrow"] == "")
        {
            foreach (GridViewRow gRow in gvLang.Rows)
            {
                dRow = dtSkill.NewRow();

                dRow["LangID"] = Util.ToInt(((Label)gRow.FindControl("lblLID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["LanguageName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["Fluency"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["FluencyCode"] = ((Label)gRow.FindControl("lblFy")).Text;
                dRow["Competency"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["CompetencyCode"] = ((Label)gRow.FindControl("lblCy")).Text;
                dRow["Comments"] = Util.CleanString(gRow.Cells[3].Text);
                dtSkill.Rows.Add(dRow);
            }

            dRow = dtSkill.NewRow();

            dRow["LangID"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["LanguageName"] = txtLang.Text.Trim();
            dRow["Fluencycode"] = ddlFluency.SelectedValue;
            dRow["Fluency"] = ddlFluency.SelectedItem.Text;
            dRow["CompetencyCode"] = ddlCompetency.SelectedValue;
            dRow["Competency"] = ddlCompetency.SelectedItem.Text;
            dRow["Comments"] = txtLangComments.Text.Trim();

            dtSkill.Rows.Add(dRow);

            gvLang.DataSource = dtSkill;
            gvLang.DataBind();
        }
        else
        {
            GridViewRow gRow = gvLang.Rows[int.Parse("" + ViewState["editlndtrow"])];
            gRow.Cells[0].Text = txtLang.Text.Trim();
            ((Label)gRow.FindControl("lblFluency")).Text =ddlFluency.SelectedItem.Text;
            ((Label)gRow.FindControl("lblFy")).Text = ddlFluency.SelectedValue;
            ((Label)gRow.FindControl("lblCompetency")).Text = ddlCompetency.SelectedItem.Text;
            ((Label)gRow.FindControl("lblCy")).Text = ddlCompetency.SelectedValue;
            ((Label)gRow.FindControl("lComm")).Text = txtLangComments.Text.Trim();
            ((Label)gRow.FindControl("lblComm")).Text = txtLangComments.Text.Trim();
            //gRow.Cells[3].Text = txtLangComments.Text.Trim();
        }
        txtLang.Text = "";
        ddlFluency.SelectedIndex = 0;
        ddlCompetency.SelectedIndex = 0;
        txtLangComments.Text = "";
        ViewState["editlndtrow"] = "";
    }


    protected void gvLang_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("language");
            e.Row.Cells[1].Text = hrmlang.GetString("fluency");
            e.Row.Cells[2].Text = hrmlang.GetString("competency");
            e.Row.Cells[3].Text = hrmlang.GetString("comments");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            Label lblFluency = (Label)e.Row.FindControl("lblFluency");
            lblFluency.Text = ddlFluency.Items.FindByValue("" + rowView["FluencyCode"]).Text;
            Label lblCompetency = (Label)e.Row.FindControl("lblCompetency");
            lblCompetency.Text = ddlCompetency.Items.FindByValue("" + rowView["CompetencyCode"]).Text;
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletelanguage"));
        }
    }

    protected void gvLang_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            txtLang.Text = Util.CleanString(gRow.Cells[0].Text);
            txtJobTitle.Text = Util.CleanString(gRow.Cells[1].Text);
            txtFromDate.Text = Util.CleanString(gRow.Cells[2].Text);
            txtToDate.Text = Util.CleanString(gRow.Cells[3].Text);
            txtLangComments.Text = ((Label)gRow.FindControl("lblComm")).Text;
            ViewState["editlndtrow"] = gRow.RowIndex;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvLanguage').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblID = (Label)gvRow.FindControl("lblLID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblID.Text);
            if (nID > 0)
                objBAL.DeleteLanguage(nID, User.Identity.Name);

            DataTable dT = GetLangTable();
            foreach (GridViewRow gRow in gvSkill.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();
                dRow["LangID"] = ((Label)gRow.FindControl("lblLID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["LanguageName"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["FluencyCode"] = ((Label)gRow.FindControl("lblFy")).Text;
                dRow["Fluency"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["CompetencyCode"] = ((Label)gRow.FindControl("lblCy")).Text;
                dRow["Competency"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["Comments"] = ((Label)gRow.FindControl("lblComm")).Text;

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvLang);
        }
    }

    #endregion

    #region IMMIGRATION

    private DataTable GetImmgTable()
    {

        DataTable dtImmg = new DataTable("IMMG");

        dtImmg.Columns.Add("ImmigrationId");
        dtImmg.Columns.Add("EmployeeID");
        dtImmg.Columns.Add("DocType");
        dtImmg.Columns.Add("DocNo");
        dtImmg.Columns.Add("IssueDate");
        dtImmg.Columns.Add("ExpiryDate");
        dtImmg.Columns.Add("EligibleStatus");
        dtImmg.Columns.Add("IssuedCountryID");
        dtImmg.Columns.Add("EligibleReviewDate");
        dtImmg.Columns.Add("Comments");

        return dtImmg;
    }

    protected void btnSaveImmg_Click(object sender, EventArgs e)
    {
        DataTable dtImmg = GetImmgTable();
        DataRow dRow = null;
        if ("" + ViewState["editimdtrow"] == "")
        {
            foreach (GridViewRow gRow in gvImmigration.Rows)
            {
                dRow = dtImmg.NewRow();

                dRow["ImmigrationId"] = Util.ToInt(((Label)gRow.FindControl("lblIID")).Text);
                dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
                dRow["DocType"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["DocNo"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["IssueDate"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["ExpiryDate"] = Util.CleanString(gRow.Cells[3].Text);
                dRow["EligibleStatus"] = Util.CleanString(gRow.Cells[4].Text);
                dRow["IssuedCountryID"] = ((Label)gRow.FindControl("lblCnID")).Text;
                dRow["EligibleReviewDate"] = Util.CleanString(gRow.Cells[6].Text);
                dRow["Comments"] = Util.CleanString(gRow.Cells[7].Text);

                dtImmg.Rows.Add(dRow);
            }

            dRow = dtImmg.NewRow();

            dRow["ImmigrationId"] = 0;
            dRow["EmployeeID"] = Util.ToInt(lblEmpID.Text);
            dRow["DocType"] = rbtnDocType.SelectedValue;
            dRow["DocNo"] = txtDocNo.Text.Trim();
            dRow["IssueDate"] = ctlCalIssueDate.getGregorianDateText;// Util.ToDateTime(txtIssueDate.Text.Trim());
            dRow["ExpiryDate"] = ctlCalExpDate.getGregorianDateText;//Util.ToDateTime(txtExpDate.Text.Trim());
            dRow["EligibleStatus"] = txtElgStatus.Text.Trim();
            dRow["IssuedCountryID"] = ddlIssueCountry.SelectedValue;
            dRow["EligibleReviewDate"] = ctlCalReviewDate.getGregorianDateText;// txtReviewdate.Text.Trim();
            dRow["Comments"] = txtImmgComments.Text.Trim();

            dtImmg.Rows.Add(dRow);

            gvImmigration.DataSource = dtImmg;
            gvImmigration.DataBind();
        }
        else
        {
            GridViewRow gRow = gvImmigration.Rows[int.Parse("" + ViewState["editimdtrow"])];
            gRow.Cells[0].Text = rbtnDocType.SelectedValue;
            gRow.Cells[1].Text = txtDocNo.Text.Trim();
            gRow.Cells[2].Text = ctlCalIssueDate.getGregorianDateText;// txtIssueDate.Text.Trim();
            gRow.Cells[3].Text = ctlCalExpDate.getGregorianDateText;//txtExpDate.Text.Trim();
            gRow.Cells[4].Text = txtElgStatus.Text.Trim();
            ((Label)gRow.FindControl("lblCnID")).Text = ddlIssueCountry.SelectedValue;
            gRow.Cells[5].Text = ctlCalReviewDate.getGregorianDateText;// txtReviewdate.Text.Trim();
            gRow.Cells[6].Text = txtImmgComments.Text.Trim();
        }
        rbtnDocType.SelectedIndex = -1;
        txtDocNo.Text = "";
        ddlCompetency.SelectedIndex = 0;
        txtImmgComments.Text = "";

        ctlCalIssueDate.SelectedCalendareDate = DateTime.Today;
        ctlCalExpDate.SelectedCalendareDate = DateTime.Today;
        ctlCalReviewDate.SelectedCalendareDate = DateTime.Today;
        ViewState["editimdtrow"] = "";
    }

    protected void gvImmigration_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "EDITDT")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            rbtnDocType.SelectedValue = Util.CleanString(gRow.Cells[0].Text);
            txtDocNo.Text = Util.CleanString(gRow.Cells[1].Text);
            //txtIssueDate.Text = Util.CleanString(gRow.Cells[2].Text);
            //txtExpDate.Text = Util.CleanString(gRow.Cells[3].Text);
            //txtReviewdate.Text = Util.CleanString(gRow.Cells[5].Text);
            txtElgStatus.Text = Util.CleanString(gRow.Cells[4].Text);
            ddlIssueCountry.SelectedValue = ((Label)gRow.FindControl("lblCnID")).Text;
            ViewState["editimdtrow"] = gRow.RowIndex;

            string sDate = Util.CleanString(gRow.Cells[2].Text);
            if (sDate != "")
            {
                //sDate = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                ctlCalIssueDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            sDate = Util.CleanString(gRow.Cells[3].Text);
            if (sDate != "")
            {
                // sDate = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                ctlCalExpDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            sDate = Util.CleanString(gRow.Cells[5].Text);
            if (sDate != "")
            {
                // sDate = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                ctlCalReviewDate.SelectedCalendareDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            txtImmgComments.Text = Util.CleanString(gRow.Cells[6].Text);
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvImmigration').modal();", true);
        }
        else if (e.CommandName == "DEL")
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblIID = (Label)gvRow.FindControl("lblIID");

            EmployeeBAL objBAL = new EmployeeBAL();
            int nID = int.Parse(lblIID.Text);
            if (nID > 0)
                objBAL.DeleteImmigration(nID, User.Identity.Name);

            DataTable dT = GetImmgTable();
            foreach (GridViewRow gRow in gvImmigration.Rows)
            {
                if (gvRow.RowIndex == gRow.RowIndex) continue;

                DataRow dRow = dT.NewRow();

                dRow["ImmigrationId"] = ((Label)gRow.FindControl("lblIID")).Text;
                dRow["EmployeeID"] = lblEmpID.Text;
                dRow["DocType"] = Util.CleanString(gRow.Cells[0].Text);
                dRow["DocNo"] = Util.CleanString(gRow.Cells[1].Text);
                dRow["IssueDate"] = Util.CleanString(gRow.Cells[2].Text);
                dRow["ExpiryDate"] = Util.CleanString(gRow.Cells[3].Text);
                dRow["EligibleStatus"] = Util.CleanString(gRow.Cells[4].Text);
                dRow["IssuedCountryID"] = Util.CleanString(gRow.Cells[5].Text);
                dRow["EligibleReviewDate"] = Util.CleanString(gRow.Cells[6].Text);
                dRow["Comments"] = Util.CleanString(gRow.Cells[7].Text);

                dT.Rows.Add(dRow);
            }

            BindGrid(dT, gvImmigration);
        }
    }

    #endregion

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepartments();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubDepartments();

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
        DataTable dt = objDept.SelectAll(objBOL); 
        if (Util.ToInt(ddlDept.SelectedValue) == 0)
            dt.Rows.Clear();
        ddlSubDept.DataSource = ReturnDT("DepartmentID", "DepartmentName", dt);
        ddlSubDept.DataBind();

    }

    private void SetLangEmployee()
    {
        ltHdr.Text = hrmlang.GetString("newemployee");
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtBiometricId.Attributes.Add("placeholder", hrmlang.GetString("enterbiometricid"));
        txtUserID.Attributes.Add("placeholder", hrmlang.GetString("enteruserid"));
        txtIrisId.Attributes.Add("placeholder", hrmlang.GetString("enteririsid"));
        txtPassword.Attributes.Add("placeholder", hrmlang.GetString("enterpassword"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        txtIDDesc.Attributes.Add("placeholder", hrmlang.GetString("enteriddescription"));
        txtIDNo.Attributes.Add("placeholder", hrmlang.GetString("enteridno"));
        lblNewStatus.Text = hrmlang.GetString("new");
        txtJoinDate.Attributes.Add("placeholder", hrmlang.GetString("enterjoiningdate"));
        txtProbStart.Attributes.Add("placeholder", hrmlang.GetString("enterprobationstartdate"));
        txtProbEnd.Attributes.Add("placeholder", hrmlang.GetString("enterprobationenddate"));
        txtBank.Attributes.Add("placeholder", hrmlang.GetString("enterbankname"));
        txtBranch.Attributes.Add("placeholder", hrmlang.GetString("enterbankbranch"));
        txtAccNo.Attributes.Add("placeholder", hrmlang.GetString("enteraccountno"));
        txtOther.Attributes.Add("placeholder", hrmlang.GetString("enterotherdetails"));
        txtAdd1.Attributes.Add("placeholder", hrmlang.GetString("enteraddressline1"));
        txtAdd2.Attributes.Add("placeholder", hrmlang.GetString("enteraddressline2"));
        txtCity.Attributes.Add("placeholder", hrmlang.GetString("entercity"));
        txtState.Attributes.Add("placeholder", hrmlang.GetString("enterstate"));
        txtZipCode.Attributes.Add("placeholder", hrmlang.GetString("enterzipcode"));
        txtHPhone.Attributes.Add("placeholder", hrmlang.GetString("enterhomephone"));
        txtMobile.Attributes.Add("placeholder", hrmlang.GetString("entermobile"));
        txtWPhone.Attributes.Add("placeholder", hrmlang.GetString("enterworkphone"));
        txtWEmail.Attributes.Add("placeholder", hrmlang.GetString("enterworkemail"));
        txtEmail.Attributes.Add("placeholder", hrmlang.GetString("enterotheremail"));
        txtWebsite.Attributes.Add("placeholder", hrmlang.GetString("enterwebsite"));
        lblAddEmg.Text = hrmlang.GetString("add");
        lblAddDep.Text = hrmlang.GetString("add");
        Label1.Text = hrmlang.GetString("new");
        lblAdAlwu.Text = hrmlang.GetString("add");
        lblAddExp.Text = hrmlang.GetString("add");
        lblAddSkill.Text = hrmlang.GetString("add");
        lblAddLanguage.Text = hrmlang.GetString("add");
        lblAddImmg.Text = hrmlang.GetString("add");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        txtType.Attributes.Add("placeholder", hrmlang.GetString("enteremploymentstatus"));
        btnSaveEmplStatus.Text = hrmlang.GetString("add");
        txtLevel.Attributes.Add("placeholder", hrmlang.GetString("entereducationlevel"));
        btnSaveLevel.Text = hrmlang.GetString("add");
        txtEmgName.Attributes.Add("placeholder", hrmlang.GetString("entername"));
        txtRelation.Attributes.Add("placeholder", hrmlang.GetString("enterrelationship"));
        txtEmgHPhone.Attributes.Add("placeholder", hrmlang.GetString("enterhomephone"));
        txtEmgWPhone.Attributes.Add("placeholder", hrmlang.GetString("enterworkphone"));
        btnSaveEmg.Text = hrmlang.GetString("add");
        txtDepName.Attributes.Add("placeholder", hrmlang.GetString("enterdependentname"));
        txtDepRelation.Attributes.Add("placeholder", hrmlang.GetString("enterrelationship"));
        txtDepDob.Attributes.Add("placeholder", hrmlang.GetString("enterdob"));
        btnSaveDep.Text = hrmlang.GetString("add");
        txtUniversity.Attributes.Add("placeholder", hrmlang.GetString("enteruniversity"));
        txtCollege.Attributes.Add("placeholder", hrmlang.GetString("entercollege"));
        txtSpec.Attributes.Add("placeholder", hrmlang.GetString("enterspecialization"));
        txtStartDate.Attributes.Add("placeholder", hrmlang.GetString("enterstartdate"));
        txtEndDate.Attributes.Add("placeholder", hrmlang.GetString("enterenddate"));
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("enteryearofpassing"));
        txtScore.Attributes.Add("placeholder", hrmlang.GetString("enterscore"));
        btnSaveEdu.Text = hrmlang.GetString("add");
        txtCompany.Attributes.Add("placeholder", hrmlang.GetString("entercompany"));
        txtJobTitle.Attributes.Add("placeholder", hrmlang.GetString("enterjobtitle"));
        txtFromDate.Attributes.Add("placeholder", hrmlang.GetString("enterfromdate"));
        txtToDate.Attributes.Add("placeholder", hrmlang.GetString("entertodate"));
        txtComments.Attributes.Add("placeholder", hrmlang.GetString("entertocomments"));
        btnSaveExp.Text = hrmlang.GetString("add");
        txtSkill.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtSkillYear.Attributes.Add("placeholder", hrmlang.GetString("enterexperienceinyears"));
        txtSkillComments.Attributes.Add("placeholder", hrmlang.GetString("entercomments"));
        btnSaveSkill.Text = hrmlang.GetString("add");
        txtLang.Attributes.Add("placeholder", hrmlang.GetString("enterlanguage"));
        txtLangComments.Attributes.Add("placeholder", hrmlang.GetString("entercomments"));
        btnSaveLang.Text = hrmlang.GetString("add");
        txtDocNo.Attributes.Add("placeholder", hrmlang.GetString("enterdocumentno"));
        txtIssueDate.Attributes.Add("placeholder", hrmlang.GetString("enterissuedate"));
        txtExpDate.Attributes.Add("placeholder", hrmlang.GetString("enterexpirydate"));
        txtElgStatus.Attributes.Add("placeholder", hrmlang.GetString("entereligiblestatus"));
        txtReviewdate.Attributes.Add("placeholder", hrmlang.GetString("entereligiblereviewdate"));
        txtImmgComments.Attributes.Add("placeholder", hrmlang.GetString("entercomments"));
        btnSaveImmg.Text = hrmlang.GetString("add");

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

        ddlMarital.Items.Clear();
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("select"), ""));
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("single"), "S"));
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("married"), "M"));
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("widowed"), "W"));
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("divorced"), "D"));
        ddlMarital.Items.Add(new ListItem(hrmlang.GetString("other"), "O"));

        rbtnGender.Items.Clear();
        rbtnGender.Items.Add(new ListItem(hrmlang.GetString("male"), "M"));
        rbtnGender.Items.Add(new ListItem(hrmlang.GetString("female"), "F"));

        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("Active"), "C"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("Inactive"), "P"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("terminated"), "T"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("resigned"), "R"));
    }
}