using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;
using System.Data;

public partial class JobTitle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //btnNew.Text = hrmlang.GetString("newdepartment");
        //txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterdepartmentcode"));
        //txtDepartment.Attributes.Add("placeholder", hrmlang.GetString("enterdepartment"));
        
        if (!IsPostBack)
        {
            lblJID.Text = Request.QueryString["JID"];

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtAddInfo.Attributes.Add("placeholder", hrmlang.GetString("additionalinformation"));
            txtDescription.Attributes.Add("placeholder", hrmlang.GetString("jobpostdescription"));
            txtExperience.Attributes.Add("placeholder", hrmlang.GetString("candidateexperience"));
            txtJobTitle.Attributes.Add("placeholder", hrmlang.GetString("jobtitle"));
            txtPositions.Attributes.Add("placeholder", hrmlang.GetString("numberofpositions"));
            txtQualification.Attributes.Add("placeholder", hrmlang.GetString("candidatequalification"));
            txtSalFrom.Attributes.Add("placeholder", hrmlang.GetString("salaryrangefrom"));
            txtSalTo.Attributes.Add("placeholder", hrmlang.GetString("salaryrangeto"));
            txtSalTo.Attributes.Add("placeholder", hrmlang.GetString("closingdate"));

            for (int i = 16; i <= 55; i++)
            {
                ddlRangeFrom.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlRangeTo.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            GetCompanyDetails();
            GetDepartments();
            GetBranches();
            GetJobTypes();

            if (!String.IsNullOrEmpty(lblJID.Text))
                GetJobTitles();


        }
        pnlNew.Visible = true;
    }

    private void GetJobTitles()
    {
        JobTitleBAL objBAL = new JobTitleBAL();
        DataTable dTable = objBAL.SelectJobTitleByJID(Util.ToInt(lblJID.Text));

        if (dTable.Rows.Count > 0)
        {
            lblJID.Text = dTable.Rows[0][0].ToString();
            ddlDepartment.SelectedValue = dTable.Rows[0][1].ToString();
            txtJobTitle.Text = dTable.Rows[0][2].ToString();
            ddlJobType.SelectedValue = dTable.Rows[0][3].ToString();
            txtPositions.Text = dTable.Rows[0][4].ToString();
            ddlRangeFrom.Text = dTable.Rows[0][5].ToString();
            ddlRangeTo.Text = dTable.Rows[0][6].ToString();
            txtSalFrom.Text = dTable.Rows[0][7].ToString();
            txtSalTo.Text = dTable.Rows[0][8].ToString();
            txtQualification.Text = dTable.Rows[0][9].ToString();
            txtExperience.Text = dTable.Rows[0][10].ToString();
            txtDescription.Text = dTable.Rows[0][11].ToString();
            txtAddInfo.Text = dTable.Rows[0][12].ToString();

            if ("" + dTable.Rows[0][21].ToString() != "")
                ctlClosingDate.SelectedCalendareDate = DateTime.Parse(dTable.Rows[0][21].ToString());

            DataTable dtTable = objBAL.SelectJobRequestBranchsByJID(Util.ToInt(lblJID.Text));
            if (dtTable.Rows.Count > 0)
            {

                foreach (DataRow DR in dtTable.Rows)
                {
                    ListItem lItem = lstBranch.Items.FindByValue("" + DR["BranchID"]);
                    if (lItem != null)
                    {
                        lItem.Selected = true;
                    }
                }
            }
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

        if (lblCompanyID.Text == "")
            Response.Redirect("Company.aspx");
    }

    private void GetDepartments()
    {
        OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
        ddlDepartment.DataSource = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDepartment.DataBind();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        ddlDepartment.Items.Insert(0, lstItem);
        ddlDepartment.ClearSelection();
    }

    private void GetJobTypes()
    {
        EmplStatusBAL objBAL = new EmplStatusBAL();
        ddlJobType.DataSource = objBAL.Select();
        ddlJobType.DataBind();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        ddlJobType.Items.Insert(0, lstItem);
        ddlJobType.ClearSelection();
    }

    private void GetBranches()
    {
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        lstBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        lstBranch.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            JobTitleBAL objBAL = new JobTitleBAL();
            JobTitleBOL objBol = new JobTitleBOL();

            objBol.JID = Util.ToInt(lblJID.Text);
            objBol.DepartmentID = Util.ToInt(ddlDepartment.SelectedValue);
            objBol.JobTitle = txtJobTitle.Text.Trim();
            objBol.EmplStatusID = Util.ToInt(ddlJobType.SelectedValue);
            objBol.VacancyNos = Util.ToInt(txtPositions.Text);
            objBol.AgeFrom = Util.ToInt(ddlRangeFrom.SelectedItem);
            objBol.AgeTo = Util.ToInt(ddlRangeTo.SelectedItem);
            objBol.SalaryFrom = Util.ToInt(txtSalFrom.Text);
            objBol.SalaryTo = Util.ToInt(txtSalTo.Text);
            objBol.Qualification = txtQualification.Text.Trim();
            objBol.Experience = txtExperience.Text.Trim();
            objBol.JobPostDescription = txtDescription.Text.Trim();
            objBol.AdditionalInfo = txtAddInfo.Text.Trim();
            if (ctlClosingDate.getGregorianDateText!="")
            objBol.ClosingDate = Util.RearrangeDateTime(ctlClosingDate.getGregorianDateText);
            objBol.CreatedBy = "";
            objBol.Branches = "";

            for (int i = 0; i < lstBranch.Items.Count; i++)
                if (lstBranch.Items[i].Selected)
                    objBol.Branches = (objBol.Branches == "") ? lstBranch.Items[i].Value : objBol.Branches + "," + lstBranch.Items[i].Value;

            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("datasaved");

            Response.Redirect("~/JobRequests.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblJID.Text = "";
        txtJobTitle.Text = "";
        ddlJobType.ClearSelection();
        txtPositions.Text = "";
        ddlRangeFrom.ClearSelection();
        ddlRangeTo.ClearSelection();
        txtSalFrom.Text = "";
        txtSalTo.Text = "";
        txtQualification.Text = "";
        txtExperience.Text = "";
        txtDescription.Text = "";
        txtAddInfo.Text = "";
        txtClosingDate.Text = "";

        lstBranch.ClearSelection();

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetDepartments();
        GetBranches();
        GetJobTypes();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
}