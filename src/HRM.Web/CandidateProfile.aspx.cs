using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using HRM.BOL;
using HRM.BAL;


using System.Net;

public partial class CandidateProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

          //  ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "jobcandidates.aspx");
            ddlStatusFill();
            GetcandidateDetails();
        }
    }

    public void GetcandidateDetails()
    {

        int id = Util.ToInt(Request.QueryString["Candidate_id"]);
        CandidateProfileBOL objbol = new CandidateProfileBAL().selectprofile(id);
        lblapplieddate.Text = objbol.AppliedDate.ToString("dd/MMM/yyyy hh:mm:ss tt");
        ddlStatus.SelectedValue = objbol.AppliedStatus;
        lblappstatus.Text = ddlStatus.SelectedItem.Text;
        lblsalutename.Text = objbol.SaluteName;
        lblfname.Text = objbol.FirstName;
        lblmname.Text = objbol.MiddleName;
        lblname.Text = objbol.LastName;
        try
        {
            if (objbol.DateOfBirth != "")
                lbldob.Text = DateTime.Parse(objbol.DateOfBirth).ToString("dd/MMM/yyyy");
        }
        catch
        { }
        lblgen.Text = (objbol.Gender == "F") ? "Female" : "Male";
        lblnatn.Text = objbol.Nationality;
        lbladd.Text = objbol.AddressLine;
        lblcity.Text = objbol.City;
        lblstate.Text = objbol.State;
        lblzip.Text = objbol.ZipCode;
        lblcountry.Text = objbol.Country;
        lblemail.Text = objbol.EmailAddress;
        lblphnno.Text = objbol.PhoneNumber;
        lblmobn.Text = objbol.MobileNumber;
        lbljobtitle.Text = objbol.JobTitle;
        lblSkills.Text = objbol.Skills;
        lblInterests.Text = objbol.Interests;
        lblAchievements.Text = objbol.Achievements;
        lblAdditional.Text = objbol.AdditionalInfo;

        GetOtherDetails();
    }

    public void GetOtherDetails()
    {
        DataSet dSet = new CandidateProfileBAL().GetCandidateData(Util.ToInt(Request.QueryString["Candidate_id"]));

        gvqualification.DataSource = dSet.Tables[0];
        gvqualification.DataBind();

        gvExperience.DataSource = dSet.Tables[1];
        gvExperience.DataBind();

        gvLanguage.DataSource = dSet.Tables[2];
        gvLanguage.DataBind();

        gvReference.DataSource = dSet.Tables[3];
        gvReference.DataBind();
    }


    public void ddlStatusFill()
    {
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("new"), "NEW"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rwprogress"), "RWP"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rwcompleted"), "RWC"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("slforinterview"), "SHI"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("ivsheduled"), "IVW"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("selected"), "SEL"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("joined"), "JND"));
    }


    protected void btnSaveCandidateStatus_Click(object sender, EventArgs e)
    {
        CandidateProfileBAL objBAL = new CandidateProfileBAL();
        CandidateProfileBOL objBOL = new CandidateProfileBOL();
        objBOL.CandidateID = Util.ToInt(Request.QueryString["Candidate_id"]);
        objBOL.Status = (ddlStatus.SelectedValue);
        objBOL.ApplicationStatus = (ddlStatus.SelectedValue);
        if (objBOL.ApplicationStatus == "IVW")
        {
            objBOL.InterviewDate = Util.ToDateTime(ctlCalDepDob.getGregorianDateText);
        }

        objBAL.Savestatus(objBOL);
        GetcandidateDetails();

    }

    public void btnsendEmail(object sender, EventArgs e)
    {
        CandidateProfileBOL objemail = new CandidateProfileBOL();
        CandidateProfileBAL objEBAL = new CandidateProfileBAL();
        try
        {
            // SaveMessage();
            EmailBAL objBAL = new EmailBAL();
            EmailBOL objEmail = new EmailBOL();

            objEmail = objBAL.Select();

            // Email Address from where you send the mail
            var fromAddress = objEmail.FromEmail;
            // any address where the email will be sending
            var toAddress = lblemail.Text;
            //Password of your From Email address
            string fromPassword = objEmail.SmtpPassword;
            // Passing the values and make a email formate to display
            string subject = "TEST";
            string body = hrmlang.GetString("from") + ": " + objEmail.FromName + "\n";
            body += hrmlang.GetString("email") + ": " + objEmail.FromEmail + "\n";
            body += hrmlang.GetString("subject") + ": " + "Job Status" + "\n";
            body += "\n";

            DataTable dt = objEBAL.Selectemail();
            string message = dt.Rows[0]["EmailContent"].ToString();
            message = message.Replace("@jobtitle", lbljobtitle.Text);
            message = message.Replace("@date", lblapplieddate.Text);
            message = message.Replace("@status", ddlStatus.SelectedValue);
            body += message;

            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = objEmail.SmtpHost;// "smtp.gmail.com";
                smtp.Port = objEmail.SmtpPort;// 587;
                if (objEmail.SmtpSecurity != "None")
                    smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
            lblemail.Text = "";
            // txtMsg.Text = "";
            //    txtSubject.Text = "";
            ClientScript.RegisterStartupScript(btnSendEmail.GetType(), "onclick", string.Format("alert('{0}');", hrmlang.GetString("mailsent")), true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(btnSendEmail.GetType(), "onclick", "alert('" + ex.Message + "');", true);
        }


    }


}