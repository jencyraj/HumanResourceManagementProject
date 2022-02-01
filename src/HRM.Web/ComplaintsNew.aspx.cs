using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComplaintsNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            lblComplaintId.Text = Request.QueryString["ComplaintID"];

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtDescription.Attributes.Add("placeholder", hrmlang.GetString("complaintdescription"));
            txtComplaintTitle.Attributes.Add("placeholder", hrmlang.GetString("complainttitle"));

            GetCompanyDetails();
            GetEmployeeDetails();

        }
        pnlNew.Visible = true;
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

    private void GetEmployeeDetails()
    {
        OrgDepartmentsBAL objBAL = new OrgDepartmentsBAL();
        ddlComplaintFrom.DataSource = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlComplaintFrom.DataBind();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        ddlComplaintFrom.Items.Insert(0, lstItem);
        ddlComplaintFrom.ClearSelection();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            JobTitleBAL objBAL = new JobTitleBAL();
            JobTitleBOL objBol = new JobTitleBOL();

            //objBol.JID = Util.ToInt(lblJID.Text);
            //objBol.DepartmentID = Util.ToInt(ddlDepartment.SelectedValue);
            //objBol.JobTitle = txtJobTitle.Text.Trim();
            //objBol.EmplStatusID = Util.ToInt(ddlJobType.SelectedValue);
            //objBol.VacancyNos = Util.ToInt(txtPositions.Text);
            //objBol.AgeFrom = Util.ToInt(ddlRangeFrom.SelectedItem);
            //objBol.AgeTo = Util.ToInt(ddlRangeTo.SelectedItem);
            //objBol.SalaryFrom = Util.ToInt(txtSalFrom.Text);
            //objBol.SalaryTo = Util.ToInt(txtSalTo.Text);
            //objBol.Qualification = txtQualification.Text.Trim();
            //objBol.Experience = txtExperience.Text.Trim();
            //objBol.JobPostDescription = txtDescription.Text.Trim();
            //objBol.AdditionalInfo = txtAddInfo.Text.Trim();
            //if (ctlClosingDate.getGregorianDateText != "")
            //    objBol.ClosingDate = Util.RearrangeDateTime(ctlClosingDate.getGregorianDateText);
            //objBol.CreatedBy = "";
            //objBol.Branches = "";

            //for (int i = 0; i < lstBranch.Items.Count; i++)
            //    if (lstBranch.Items[i].Selected)
            //        objBol.Branches = (objBol.Branches == "") ? lstBranch.Items[i].Value : objBol.Branches + "," + lstBranch.Items[i].Value;

            //objBAL.Save(objBol);
            //lblMsg.Text = hrmlang.GetString("datasaved");

            //Response.Redirect("~/JobRequests.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        //GetDepartments();
        //GetBranches();
        //GetJobTypes();
    }
}