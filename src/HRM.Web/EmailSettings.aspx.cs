using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class EmailSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        dvMsg.Visible = false;
        txtFromMail.Attributes.Add("placeholder", hrmlang.GetString("enterfromemail"));
        txtFromName.Attributes.Add("placeholder", hrmlang.GetString("enterfromname"));
        txtPort.Attributes.Add("placeholder", hrmlang.GetString("entersmtpport"));
        txtSMTPUserName.Attributes.Add("placeholder", hrmlang.GetString("entersmtpusername"));
        txtSMTPPassword.Attributes.Add("placeholder", hrmlang.GetString("entersmtppassword"));
        txtHost.Attributes.Add("placeholder", hrmlang.GetString("entersmtphost"));
        btnSave.Text = hrmlang.GetString("save");
        if (!IsPostBack)
        {
            GetEmailSettings();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmailBAL objBAL = new EmailBAL();
        EmailBOL objEmail = new EmailBOL();
        dvMsg.Visible = true;

        try
        {
            objEmail.FromEmail = txtFromMail.Text.Trim();
            objEmail.FromName = txtFromName.Text.Trim();
            objEmail.SmtpHost = txtHost.Text.Trim();
            objEmail.SmtpPassword = txtSMTPPassword.Text.Trim();
            objEmail.SmtpPort = Util.ToInt(txtPort.Text.Trim());
            objEmail.SmtpUserName = txtSMTPUserName.Text.Trim();
            objEmail.SmtpAuth = rbtnAuth.SelectedValue.Trim();
            objEmail.SmtpSecurity = ddlSecurity.SelectedValue.Trim();
            objBAL.Save(objEmail);
            lblMsg.Text = "Email settings saved successfully";
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void GetEmailSettings()
    {
        EmailBAL objBAL = new EmailBAL();
        EmailBOL objEmail = objBAL.Select();
        if (objEmail != null)
        {
            txtFromMail.Text = objEmail.FromEmail;
            txtFromName.Text = objEmail.FromName;
            txtHost.Text = objEmail.SmtpHost;
           // txtSMTPPassword.Text = objEmail.SmtpPassword;
            txtPort.Text = objEmail.SmtpPort.ToString();
            txtSMTPUserName.Text = objEmail.SmtpUserName;
            rbtnAuth.SelectedValue = objEmail.SmtpAuth;
            ddlSecurity.SelectedValue = objEmail.SmtpSecurity;
            txtSMTPPassword.Attributes.Add("value", objEmail.SmtpPassword);
        }
    }
}