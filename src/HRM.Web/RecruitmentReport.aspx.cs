using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using HRM.BOL;
using HRM.BAL;

public partial class RecruitmentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "recruitmentreport.aspx");
         
            ddlStatusFill();
            GetJobTitles();
           
        }

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
        ddlStatus.Items.Insert(0, new ListItem(hrmlang.GetString("SELECT"), ""));
    }
    private void GetJobTitles()
    {
        JobTitleBAL objBAL = new JobTitleBAL();
        DataTable dt = objBAL.SelectAll();
        ddlJobTitle.DataSource = dt;
        ddlJobTitle.DataBind();
        ddlJobTitle.Items.Insert(0, new ListItem(hrmlang.GetString("SELECT"), ""));
    
    }

    private void Bindrecruitementreport()
    {
   
        string sParams = "&sp1=" + ddlJobTitle.SelectedValue + "&sp2=" + ddlStatus.SelectedValue + "&sp3=" + ctlCalendardob.getGregorianDateText + "&sp4=" + CtlDOB1.getGregorianDateText;
        string url = "HRMReports.aspx?rptname=recrumntReport&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=RECRUITMENT";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');"; 
         ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindrecruitementreport();
    }

 

}