using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using HRM.BOL;
using HRM.BAL;

public partial class ViewAllResignation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "ViewAllResignation.aspx");
            BindBranchDD();

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("all"), "A"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("pendingapproval"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "N"));

            GetResignation();
        }
        string[] permissions = (string[])ViewState["permissions"];
    }
    private void GetResignation()
    {
        ResignationBAL objBAL = new ResignationBAL();
        int EmployeeId = Util.ToInt(hfEmployeeId.Value);
        int branchId = Util.ToInt(ddlBranch.SelectedValue);
        string Status = "" + ddlStatus.SelectedValue;
        if (ddlStatus.SelectedValue == "A")
        {
            Status = "";
        }
        else
        {
            Status = "" + ddlStatus.SelectedValue;
        }
        gvResignation.DataSource = objBAL.SelectAll(EmployeeId,branchId,Status);
        gvResignation.DataBind();

        if (gvResignation.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");
        else
            lblErr.Text = "";
    }
    protected void gvResignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            Response.Redirect("AddResignation.aspx?id=" + e.CommandArgument.ToString());
        }
        if (e.CommandName.Equals("DEL"))
        {
            new ResignationBAL().Delete(Util.ToInt(e.CommandArgument));
            ClientScript.RegisterStartupScript(gvResignation.GetType(), "onclick", "alert('" + hrmlang.GetString("Resignationdeleted") + "');", true);
            GetResignation();
        }
        if (e.CommandName.Equals("Y"))//Approval
        {

            pnlApprove.Visible = true;
            pnlButtons.Visible = true;
            pnlDeny.Visible = false;
            lblTID.Text = e.CommandArgument.ToString();
            //ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvApprove').modal();", true);

        }
        if (e.CommandName.Equals("N"))//Rejected
        {
            pnlDeny.Visible = true;
            pnlButtons.Visible = true;
            pnlApprove.Visible = false;
            lblTID.Text = e.CommandArgument.ToString();
            //ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvApprove').modal();", true);

        }
    }
    private void BindBranchDD()
    {
        ddlBranch.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataValueField = "BranchId";
        ddlBranch.DataTextField = "Branch";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }
    protected void gvResignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvResignation.PageIndex = e.NewPageIndex;
        GetResignation();
    }

    protected void gvResignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("EmployeeName");
            e.Row.Cells[1].Text = hrmlang.GetString("NoticeDate");
            e.Row.Cells[2].Text = hrmlang.GetString("ResgnDate");
            e.Row.Cells[3].Text = hrmlang.GetString("reason");
            e.Row.Cells[4].Text = hrmlang.GetString("status");
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = DataBinder.Eval(e.Row.DataItem, "Approved").ToString();
            string Log_EmployeeId = "" + Session["EMPID"];
            string Emp_id = ((Label)e.Row.FindControl("lblEmployeeId")).Text;
            ((Label)e.Row.FindControl("lblStatus")).Text = (status == "P") ? "PENDING" : ((status == "Y") ? "APPROVED" : "REJECTED");


            if (ViewState["permissions"] != null)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnkApproval = (LinkButton)e.Row.FindControl("lnkApproval");
                LinkButton lnkRejected = (LinkButton)e.Row.FindControl("lnkRejected");
                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
               
                if (Log_EmployeeId == Emp_id)
                {
                    lnkApproval.Visible = false;
                    lnkRejected.Visible = false;
                }
                else
                {
                    if (status == "Y")
                    {
                        lnkApproval.Visible = false;
                        lnkRejected.Visible = true;
                    }
                    else if (status == "N")
                    {
                        lnkApproval.Visible = true;
                        lnkRejected.Visible = false;
                    }
                    else
                    {
                        lnkApproval.Visible = true;
                        lnkRejected.Visible = true;
                    }
                   
                    lnkApproval.Attributes.Add("title", hrmlang.GetString("approve"));
                    lnkRejected.Attributes.Add("title", hrmlang.GetString("reject"));
                }
               
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            }
           
        }
    }
    
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddResignation.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetResignation();
    }
    protected void rbtnIntrv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIntrv.SelectedValue == "Y")
        {
            pnlIntrv.Visible = true;
        }
        else
        {
            pnlIntrv.Visible = false;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            ResignationBAL objBAL = new ResignationBAL();
            ResignationBOL objBol = new ResignationBOL();

            if (!String.IsNullOrEmpty(lblTID.Text))
                objBol.ResgnID = Convert.ToInt32(lblTID.Text);

            objBol.ApprovedBy = "" + Session["EMPID"];

            if (rbtnIntrv.SelectedValue == "Y")
            {
                objBol.IsExitInterview = true;

                if (hfexitEmployeeId.Value != "")
                    objBol.InterviewerId = Util.ToInt(hfexitEmployeeId.Value);

                if (ctlInterviewDate.getGregorianDateText != "")
                    objBol.InterviewDate = Util.RearrangeDateTime(ctlInterviewDate.getGregorianDateText);
            }
            else if (rbtnIntrv.SelectedValue == "N")
            {
                objBol.IsExitInterview = false;
            }

            if (pnlApprove.Visible == true)
            {
                objBol.Approved = "Y";
                objBol.ApprovalReason = txtApprReason.Text;
            }
            else if (pnlDeny.Visible == true)
            {
                objBol.Approved = "N";
                objBol.ApprovalReason = txtDnyReason.Text;
            }

            new ResignationBAL().Approve(objBol);
            EmployeeBAL emp_BAL= new EmployeeBAL();
            EmployeeBOL emp_BOL= new EmployeeBOL();
            emp_BOL.EmployeeID=Convert.ToInt32(lblTID.Text);
            emp_BOL=emp_BAL.Select(emp_BOL);
            string Resig_EmployeeEmail = emp_BOL.WEmail;
            string Resig_EmployeeName = emp_BOL.FirstName + " " + emp_BOL.MiddleName + " " + emp_BOL.LastName;
            if (string.IsNullOrWhiteSpace(Resig_EmployeeEmail))
            {
                Resig_EmployeeEmail = emp_BOL.HEmail;
            }

            EmployeeBAL exit_BAL = new EmployeeBAL();
            EmployeeBOL exit_BOL = new EmployeeBOL();
            exit_BOL.EmployeeID = Util.ToInt(hfexitEmployeeId.Value);
            exit_BOL = exit_BAL.Select(exit_BOL);
            string Interviewer_Email = exit_BOL.WEmail;
            string Interviewer_Name = exit_BOL.FirstName + " " + exit_BOL.MiddleName + " " + exit_BOL.LastName;
            if (string.IsNullOrWhiteSpace(Resig_EmployeeEmail))
            {
                Interviewer_Email = exit_BOL.HEmail;
            }
            // send email
            if (!string.IsNullOrWhiteSpace(Resig_EmployeeEmail))
            {
                string subject = "Exit Interview";
                string toAddress = Resig_EmployeeEmail.Trim();
                string mailbody = "Dear " +Resig_EmployeeName+",";
                mailbody += "\n";
                mailbody += "<p>As you leave your position , we believe you have a unique perspective on the work environment here.  In order to continue improving our work environment, we encourage you to participate in the Exit Interview survey before your last day of employment.  Your individual response will not be attributed specifically to you and you have the choice to remain anonymous. Your honesty is greatly appreciated and your opinions are highly valued.  Your feedback provides insight into thework environment and the factors that may lead to an employee’s decision to leave employment.</p>" ;
                mailbody += "\n";
                mailbody += "<p> Your Exit Interview is scheduled on " + Util.RearrangeDateTime(ctlInterviewDate.getGregorianDateText) + " by " + Interviewer_Name +".If you have any questions or concerns about the exit interview process, please feel free to contact. </p>";
                mailbody += "\n";
                mailbody += "<p>We wish you the best in your future endeavors. </p>";                
                // .... is going to resign and the exit interview is appointed on ...... day
                SendEmail(toAddress, subject, mailbody);
            }
            if (!string.IsNullOrWhiteSpace(Interviewer_Email))
            {
                string subject = "Exit Interview";
                string toAddress = Interviewer_Email.Trim();
                string mailbody = "Dear " + Interviewer_Name + ",";
                mailbody += "\n";
                mailbody += "<p> " + Resig_EmployeeName + " is going to resign from our organization. so the exit interview is appointed on " + Util.RearrangeDateTime(ctlInterviewDate.getGregorianDateText) + "</p>";
                mailbody += "\n";
               
                // .... is going to resign and the exit interview is appointed on ...... day
                SendEmail(toAddress, subject, mailbody);
            }
            GetResignation();

            ClientScript.RegisterStartupScript(gvResignation.GetType(), "onclick", "alert('" + hrmlang.GetString("Resignationapproved") + "');", true);
            Response.Redirect("ViewAllResignation.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    private void SendEmail(string toAddress, string subject, string emailbody)
    {

        //Email sending Start

        try
        {
            SaveMessage(toAddress, subject, emailbody);
            EmailBAL eobjBAL = new EmailBAL();
            EmailBOL eobjEmail = new EmailBOL();

            eobjEmail = eobjBAL.Select();

            // Email Address from where you send the mail
            var fromAddress = eobjEmail.FromEmail;
            // any address where the email will be sending
            //var toAddress = ""; //txtTo.Text;
            //Password of your From Email address
            string fromPassword = eobjEmail.SmtpPassword;
            // Passing the values and make a email formate to display  
           
            string body = hrmlang.GetString("from") + ": " + eobjEmail.FromName + "\n";
            body += hrmlang.GetString("email") + ": " + eobjEmail.FromEmail + "\n";
            body += hrmlang.GetString("subject") + ": " + subject + "\n";
            body += "\n";
            body += emailbody;
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = eobjEmail.SmtpHost;// "smtp.gmail.com";
                smtp.Port = eobjEmail.SmtpPort;// 587;
                if (eobjEmail.SmtpSecurity != "None")
                    smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(gvResignation.GetType(), "onclick", "alert('" + ex.Message + "');", true);
        }
        //Email Sending Failed

    }
    private void SaveMessage(string toAddress, string subject, string emailbody)
    {
        try
        {
            MessagesBOL objBOL = new MessagesBOL();
            MessagesBAL objBAL = new MessagesBAL();

            objBOL.EmailID = toAddress.Trim();
            objBOL.MailSubject = subject.Trim();
            objBOL.MailMessage = emailbody.Trim();
            objBOL.SentBy = "" + Session["EMPID"];

            objBAL.Save(objBOL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
    }
}