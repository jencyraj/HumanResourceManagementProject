using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class careers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErr.Text = "";
        lblMsg.Text = "";

        SetLangEmployee();

        if (!IsPostBack)
        {
            GetCompanyDetails();
            GetDropDownValues();
            SetQualificationInitialRow();
            SetExperienceInitialRow();
            SetLanguageInitialRow();
            SetReferenceTable();
        }
    }

    private void SetQualificationInitialRow()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("EducationLevel", typeof(string)));
        dt.Columns.Add(new DataColumn("EduLevel", typeof(int)));
        dt.Columns.Add(new DataColumn("College", typeof(string)));
        dt.Columns.Add(new DataColumn("University", typeof(string)));
        dt.Columns.Add(new DataColumn("Specialization", typeof(string)));
        dt.Columns.Add(new DataColumn("Year", typeof(string)));
        dt.Columns.Add(new DataColumn("Score", typeof(string)));

        dt.Rows.Add();

        Session["QfTable"] = dt;

        gvqualification.DataSource = dt;
        gvqualification.DataBind();
    }

    private void SetExperienceInitialRow()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("Company", typeof(string)));
        dt.Columns.Add(new DataColumn("JobTitle", typeof(string)));
        dt.Columns.Add(new DataColumn("FromDate", typeof(string)));
        dt.Columns.Add(new DataColumn("ToDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Reason", typeof(string)));

        dt.Rows.Add();

        Session["ExpTable"] = dt;

        gvExperience.DataSource = dt;
        gvExperience.DataBind();
    }

    private void SetLanguageInitialRow()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("LanguageName", typeof(string)));
        dt.Columns.Add(new DataColumn("Fluency", typeof(string)));
        dt.Columns.Add(new DataColumn("Competency", typeof(string)));
        dt.Columns.Add(new DataColumn("Comments", typeof(string)));

        dt.Rows.Add();

        Session["LgTable"] = dt;

        gvLanguage.DataSource = dt;
        gvLanguage.DataBind();
    }

    private void AddQualificationToGrid()
    {

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

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));

        EmployeeBAL objEmp = new EmployeeBAL();
        // dt = objEmp.SelectNationality("" + Session["LanguageId"]);
        dt = objEmp.SelectNationality("en-US");
        ddlNationality.DataSource = ReturnDT("NationalityCode", "Nationality", dt);
        ddlNationality.DataBind();

        CountryBAL objCn = new CountryBAL();
        dt = objCn.Select("en-US");
        // dt = objCn.Select("" + Session["LanguageId"]);
        ddlCountry.DataSource = ReturnDT("CountryCode", "CountryName", dt);
        ddlCountry.DataBind();

        JobTitleBAL objBAL = new JobTitleBAL();
        ddlJobTitle.DataSource = objBAL.SelectAll();
        ddlJobTitle.DataBind();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        ddlJobTitle.Items.Insert(0, lstItem);
        ddlJobTitle.ClearSelection();
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

    private void SetLangEmployee()
    {

        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        //   txtAdd1.Attributes.Add("placeholder", hrmlang.GetString("enteraddressline1"));
        txtCity.Attributes.Add("placeholder", hrmlang.GetString("entercity"));
        txtState.Attributes.Add("placeholder", hrmlang.GetString("enterstate"));
        txtZipCode.Attributes.Add("placeholder", hrmlang.GetString("enterzipcode"));
        txtHPhone.Attributes.Add("placeholder", hrmlang.GetString("enterphoneno"));
        txtMobile.Attributes.Add("placeholder", hrmlang.GetString("entermobileno"));
        txtEmail.Attributes.Add("placeholder", hrmlang.GetString("emailaddress"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        //GetDepartments();
        //GetBranches();
        //GetJobTypes();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int candidateid = 0;

        if (!Page.IsValid)
        { return; }
        try
        {
            CandidateProfileBAL objBAL = new CandidateProfileBAL();
            CandidateProfileBOL objBol = new CandidateProfileBOL();

            objBol.JID = Util.ToInt(ddlJobTitle.Text);
            objBol.FirstName = txtfName.Text.Trim();
            objBol.MiddleName = txtmName.Text.Trim();
            objBol.LastName = txtlName.Text.Trim();
            objBol.SaluteName = ddlsalute.SelectedItem.Text;
            objBol.Gender = rbtnGender.SelectedValue;
            objBol.NationalityCode = ddlNationality.SelectedValue;
            objBol.AddressLine = txtAdd1.Text.Trim();
            objBol.City = txtCity.Text.Trim();
            objBol.State = txtState.Text.Trim();
            objBol.ZipCode = txtZipCode.Text.Trim();
            objBol.PhoneNumber = txtHPhone.Text.Trim();
            objBol.MobileNumber = txtMobile.Text.Trim();
            objBol.EmailAddress = txtEmail.Text.Trim();
            objBol.Country = ddlCountry.SelectedValue;
            objBol.Interests = txtinterests.Text.Trim();
            objBol.Achievements = txtachievements.Text.Trim();
            objBol.AdditionalInfo = txtaddInfo.Text.Trim();

        if (ctlCalendardob.getGregorianDateText != "" )
                objBol.DateOfBirth = Util.ToDateTime(ctlCalendardob.getGregorianDateText).ToString();
          

            candidateid = objBAL.Save(objBol, (DataTable)Session["QfTable"], (DataTable)Session["ExpTable"], (DataTable)Session["LgTable"]);

            DataTable dtRef = new DataTable();
            dtRef.Columns.Add("REFNAME");
            dtRef.Columns.Add("ORG");
            dtRef.Columns.Add("EMAIL");
            dtRef.Columns.Add("PHONE");

            GridViewRow gRow = gvReference.Rows[0];

            for (int i = 1; i < 4; i++)
            {
                if (((TextBox)gRow.FindControl("txtRefName" + i.ToString())).Text.Trim() == "") continue;
                DataRow dR = dtRef.NewRow();
                dR["REFNAME"] = ((TextBox)gRow.FindControl("txtRefName" + i.ToString())).Text.Trim();
                dR["ORG"] = ((TextBox)gRow.FindControl("txtOrg" + i.ToString())).Text.Trim();
                dR["EMAIL"] = ((TextBox)gRow.FindControl("txtEmail" + i.ToString())).Text.Trim();
                dR["PHONE"] = ((TextBox)gRow.FindControl("txtPhone" + i.ToString())).Text.Trim();
                dtRef.Rows.Add(dR);
            }

            objBAL.SaveReference(candidateid, dtRef);

            lblMsg.Text = "Your application has been submitted successfully";// hrmlang.GetString("datasaved");

            Clear();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
            if (candidateid > 0)
                new CandidateProfileBAL().Recycle(candidateid);
        }
    }

    protected void AddQualificationDetails(object sender, EventArgs e)
    {
        if (Session["QfTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)Session["QfTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {

                DropDownList ddlEducationLevel = (DropDownList)gvqualification.FooterRow.FindControl("ddlEducationLevel");

                TextBox txtCollege = (TextBox)gvqualification.FooterRow.FindControl("txtCollege");
                TextBox txtUniversity = (TextBox)gvqualification.FooterRow.FindControl("txtUniversity");
                TextBox txtSpecialization = (TextBox)gvqualification.FooterRow.FindControl("txtSpecialization");
                TextBox txtYear = (TextBox)gvqualification.FooterRow.FindControl("txtYear");
                TextBox txtScore = (TextBox)gvqualification.FooterRow.FindControl("txtScore");

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["RowNumber"] = dtCurrentTable.Rows.Count;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["EducationLevel"] = ddlEducationLevel.SelectedItem.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["EduLevel"] = ddlEducationLevel.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["College"] = txtCollege.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["University"] = txtUniversity.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Specialization"] = txtSpecialization.Text;

                //if (ctlCalendarYear.getGregorianDateText != "")
                //    objBol.DateOfBirth = Util.RearrangeDateTime(ctlCalendardob.getGregorianDateText);

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Year"] = txtYear.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Score"] = txtScore.Text;

                dtCurrentTable.Rows.Add();
                Session["QfTable"] = dtCurrentTable;

                gvqualification.DataSource = dtCurrentTable;
                gvqualification.DataBind();
            }
        }
    }

    protected void AddExperienceDetails(object sender, EventArgs e)
    {
        if (Session["ExpTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)Session["ExpTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {

                TextBox txtCompany = (TextBox)gvExperience.FooterRow.FindControl("txtCompany");
                TextBox txtJobTitle = (TextBox)gvExperience.FooterRow.FindControl("txtJobTitle");
                TextBox txtFromDate = (TextBox)gvExperience.FooterRow.FindControl("txtFromDate");
                TextBox txtToDate = (TextBox)gvExperience.FooterRow.FindControl("txtToDate");
                TextBox txtReason = (TextBox)gvExperience.FooterRow.FindControl("txtReason");

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["RowNumber"] = dtCurrentTable.Rows.Count;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Company"] = txtCompany.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["JobTitle"] = txtJobTitle.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["FromDate"] = txtFromDate.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["ToDate"] = txtToDate.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Reason"] = txtReason.Text;

                dtCurrentTable.Rows.Add();
                Session["ExpTable"] = dtCurrentTable;

                gvExperience.DataSource = dtCurrentTable;
                gvExperience.DataBind();
            }
        }
    }

    protected void AddLanguageDetails(object sender, EventArgs e)
    {
        if (Session["LgTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)Session["LgTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {
                TextBox txtLanguageName = (TextBox)gvLanguage.FooterRow.FindControl("txtLanguageName");
                DropDownList ddlFluency = (DropDownList)gvLanguage.FooterRow.FindControl("ddlFluency");
                DropDownList ddlCompetency = (DropDownList)gvLanguage.FooterRow.FindControl("ddlCompetency");
                TextBox txtComments = (TextBox)gvLanguage.FooterRow.FindControl("txtComments");

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["RowNumber"] = dtCurrentTable.Rows.Count;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["LanguageName"] = txtLanguageName.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Fluency"] = ddlFluency.SelectedItem.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Competency"] = ddlCompetency.SelectedItem.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Comments"] = txtComments.Text;

                dtCurrentTable.Rows.Add();
                Session["LgTable"] = dtCurrentTable;

                gvLanguage.DataSource = dtCurrentTable;
                gvLanguage.DataBind();
            }
        }
    }

    protected void gvQualification_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                DataTable currentTable = (DataTable)Session["QfTable"];

                currentTable.Rows.RemoveAt(Util.ToInt(e.CommandArgument) - 1);

                Session["QfTable"] = currentTable;

                gvqualification.DataSource = currentTable;
                gvqualification.DataBind();
            }
        }
    }

    protected void gvExperience_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                DataTable currentTable = (DataTable)Session["ExpTable"];

                currentTable.Rows.RemoveAt(Util.ToInt(e.CommandArgument) - 1);

                Session["ExpTable"] = currentTable;

                gvExperience.DataSource = currentTable;
                gvExperience.DataBind();
            }
        }
    }

    protected void gvLanguage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                DataTable currentTable = (DataTable)Session["LgTable"];

                currentTable.Rows.RemoveAt(Util.ToInt(e.CommandArgument) - 1);

                Session["LgTable"] = currentTable;

                gvLanguage.DataSource = currentTable;
                gvLanguage.DataBind();
            }
        }
    }

    protected void gvQualification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlEduLevel = (DropDownList)e.Row.FindControl("ddlEducationLevel");
            OrgBranchesBAL objBr = new OrgBranchesBAL();

            EduLevelBAL objLevel = new EduLevelBAL();
            DataSet dSet = objLevel.Select();
            ddlEduLevel.DataSource = ReturnDT("EduLevelID", "EduLevelName", dSet.Tables[0]);
            ddlEduLevel.DataTextField = "EduLevelName";
            ddlEduLevel.DataValueField = "EduLevelID";
            ddlEduLevel.DataBind();
        }
    }

    #region References

    private void SetReferenceTable()
    {
        DataTable dtRef = new DataTable();
        dtRef.Columns.Add("REFNAME");
        dtRef.Columns.Add("ORG");
        dtRef.Columns.Add("EMAIL");
        dtRef.Columns.Add("PHONE");

        dtRef.Rows.Add("", "", "", "");

        gvReference.DataSource = dtRef;
        gvReference.DataBind();

    }

    #endregion

    private void Clear()
    {
        ddlCountry.SelectedIndex = 0;
        ddlJobTitle.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;

        SetQualificationInitialRow();
        SetExperienceInitialRow();
        SetLanguageInitialRow();
        SetReferenceTable();

        txtfName.Text = "";
        txtmName.Text = "";
        txtlName.Text = "";
        txtAdd1.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZipCode.Text = "";
        txtMobile.Text = "";
        txtHPhone.Text = "";
        txtEmail.Text = "";

        txtachievements.Text = "";
        txtSkills.Text = "";
        txtinterests.Text = "";
        txtaddInfo.Text = "";

        ctlCalendardob.ClearDate();
        rbtnGender.ClearSelection();
    }

}