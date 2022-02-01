using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

using HRM.BAL;
using HRM.BOL;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            GetProfile();

            ltbDash.Text = hrmlang.GetString("dashboard");
            ltDash.Text = hrmlang.GetString("dashboard");


            ltbHome.Text = hrmlang.GetString("home");
            ltHome.Text = hrmlang.GetString("home");
            ltProfile.Text = hrmlang.GetString("profile");

            ltChartTitle.Text = hrmlang.GetString("employeestatistics");

            LoadPieChart();
            LeaveBalance();
            GetHolidays();
            selectnotify();
            SiteConfiguration();
        }
    }

    private void SiteConfiguration()
    {
        Session["MULTILANG"] = "";
        Session["USETAX"] = "";
        Session["DOWNLOAD"] = "";
        DataTable dTable = new SettingsBAL().SelectAll();
        foreach (DataRow dRow in dTable.Rows)
        {
            if ("" + dRow["ConfigCode"] == "LANG" && "" + dRow["ConfigValue"] == "Y")
                Session["MULTILANG"] = "1";
            if ("" + dRow["ConfigCode"] == "TAX" && "" + dRow["ConfigValue"] == "Y")
                Session["USETAX"] = "1";
            if ("" + dRow["ConfigCode"] == "DOWNLOADSLIP" && "" + dRow["ConfigValue"] == "Y")
                Session["DOWNLOAD"] = "1";
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtTo.Attributes.Add("placeholder", hrmlang.GetString("emailto"));
            txtSubject.Attributes.Add("placeholder", hrmlang.GetString("subject"));
            txtMsg.Attributes.Add("placeholder", hrmlang.GetString("message"));
            btnSendEmail.Text = hrmlang.GetString("send");
            if ("" + Session["STYLESHEET"] == "LR")
                btnSendEmail.Text += "<i class='fa fa-arrow-circle-right'></i>";
            else
                btnSendEmail.Text += "<i class='fa fa-arrow-circle-left'></i>";
        }
    }

    #region EMAIL

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMessage();
            EmailBAL objBAL = new EmailBAL();
            EmailBOL objEmail = new EmailBOL();

            objEmail = objBAL.Select();

            // Email Address from where you send the mail
            var fromAddress = objEmail.FromEmail;
            // any address where the email will be sending
            var toAddress = txtTo.Text;
            //Password of your From Email address
            string fromPassword = objEmail.SmtpPassword;
            // Passing the values and make a email formate to display
            string subject = txtSubject.Text.ToString();
            string body = hrmlang.GetString("from") + ": " + objEmail.FromName + "\n";
            body += hrmlang.GetString("email") + ": " + objEmail.FromEmail + "\n";
            body += hrmlang.GetString("subject") + ": " + txtSubject.Text + "\n";
            body += "\n";
            body += txtMsg.Text.Trim();
            // smtp settings
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
            txtTo.Text = "";
            txtMsg.Text = "";
            txtSubject.Text = "";
            ClientScript.RegisterStartupScript(btnSendEmail.GetType(), "onclick", string.Format("alert('{0}');", hrmlang.GetString("mailsent")), true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(btnSendEmail.GetType(), "onclick", "alert('" + ex.Message + "');", true);
        }
    }

    private void SaveMessage()
    {
        try
        {
            MessagesBOL objBOL = new MessagesBOL();
            MessagesBAL objBAL = new MessagesBAL();

            objBOL.EmailID = txtTo.Text.Trim();
            objBOL.MailSubject = txtSubject.Text.Trim();
            objBOL.MailMessage = txtMsg.Text.Trim();
            objBOL.SentBy = "" + Session["EMPID"];

            objBAL.Save(objBOL);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    private void GetProfile()
    {

        if (Request.Cookies["usersettings"] != null)
        {
            HttpCookie cookie = Request.Cookies["usersettings"];
            Session["USERID"] = cookie.Values["ihrUserName"].ToString();
        }

        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.UserID = "" + Session["USERID"];// HttpContext.Current.User.Identity.Name;
        objBOL = objBAL.Select(objBOL);

        /* EmployeeBOL objBOL = new EmployeeBOL();
         objBOL.EmployeeID = Util.ToInt(Session["EMPID"]);
         objBOL = new EmployeeBAL().Select(objBOL);*/

        lblName.Text = objBOL.FirstName + ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName != "") ? " " + objBOL.LastName : "");
        lblUserID.Text = objBOL.UserID;
        lblBranch.Text = objBOL.Branch.Branch;
        lblRole.Text = objBOL.Roles.RoleName;
        lblDept.Text = objBOL.Department.DepartmentName;
        lblDesgn.Text = objBOL.Designation.Designation;
        if ("" + objBOL.JoiningDate != "")
            lblJoin.Text = DateTime.Parse(objBOL.JoiningDate).ToString("dd/MMM/yyyy");
        DataTable dTable = new UserBAL().LoginHistory(objBOL.UserID, true, 0);
        if (dTable.Rows.Count > 0)
            lblLogin.Text = "" + dTable.Rows[0]["LoginTime"];
    }

    private void LoadPieChart()
    {
        DataTable dTable = new EmployeeBAL().EmployeeStatistics(Util.ToInt("" + Session["BRANCHID"]));

        decimal totCount = Util.ToInt(dTable.Compute("SUM(empcount)", ""));

        dTable.Columns.Add("Percentage", typeof(decimal));
        //  dTable.Columns["Percentage"].Expression = "empcount /" + totCount + " * 100";
        //  dTable.Columns["Percentage"].ReadOnly = false;

        if (dTable.Rows.Count > 0)
        {
            for (int i = 0; i < dTable.Rows.Count; i++)
                dTable.Rows[i]["Percentage"] = Math.Round((Util.ToDecimal(dTable.Rows[i]["empcount"]) / totCount) * 100, 2);

            chPie.Series[0].Points.DataBindXY(dTable.DefaultView, "DepartmentName", dTable.DefaultView, "Percentage");
        }

        int j = -1;
        foreach (DataPoint point in chPie.Series[0].Points)
        {
            DataRow dRow = dTable.Rows[++j];
            point.AxisLabel = point.AxisLabel + "\n" + dRow["Percentage"] + "%";
        }
    }


    protected void chPie_Customize(object sender, EventArgs e)
    {
        chPie.Series[0]["PieLabelStyle"] = "Outside";
    }

    private void LeaveBalance()
    {
        LeaveBAL objBAL = new LeaveBAL();
        DataTable dtBalance = objBAL.GetLeaveBalance(Util.ToInt(Session["EMPID"]));


        LeaveTypesBAL objLT = new LeaveTypesBAL();
        DataTable dT = objLT.SelectAll();

        if (dtBalance.Rows.Count == 0)
        {
            if (dT.Rows.Count > 0)
            {
                dT.Columns.Add("PrevYearBalance");
                dT.Columns.Add("LeavesTaken");
                dT.Columns.Add("LeavesBalance");
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dT.Rows[i][dT.Columns.Count - 3] = "0";
                    dT.Rows[i][dT.Columns.Count - 2] = "0";
                    dT.Rows[i][dT.Columns.Count - 1] = dT.Rows[i]["LeaveDays"];
                }
                gvBalance.DataSource = dT;
                gvBalance.DataBind();
            }
            else
                lblSubMsg.Text = hrmlang.GetString("norecordsfound");
        }
        else
        {
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                int nCount = dtBalance.Select("LeaveTypeID =" + dT.Rows[i]["LeaveTypeID"]).Length;
                if (nCount == 0)
                {
                    DataRow dRow = dtBalance.NewRow();
                    dRow["LeaveYear"] = DateTime.Today.Year;
                    dRow["LeaveName"] = dT.Rows[i]["LeaveName"];
                    dRow["LeaveDays"] = dT.Rows[i]["LeaveDays"];
                    dRow["CarryOver"] = dT.Rows[i]["CarryOver"];
                    dRow["LeavesTaken"] = "0";
                    dRow["LeavesBalance"] = dT.Rows[i]["LeaveDays"];
                    dRow["PrevYearBalance"] = "0";
                    dtBalance.Rows.Add(dRow);
                }
            }
            DataView dtView = dtBalance.DefaultView;
            dtView.RowFilter = "LeaveYear=" + DateTime.Today.Year.ToString();
            gvBalance.DataSource = dtView.ToTable();// dtBalance;
            gvBalance.DataBind();
        }
    }

    protected void gvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("leavetype");
            e.Row.Cells[1].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[2].Text = hrmlang.GetString("carryover");
            e.Row.Cells[3].Text = hrmlang.GetString("leavestaken");
            e.Row.Cells[4].Text = hrmlang.GetString("carryovered");
            e.Row.Cells[5].Text = hrmlang.GetString("balance");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvBalance.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvBalance.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvBalance.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvBalance.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    private void GetHolidays()
    {
        HolidayBAL objBAL = new HolidayBAL();
        DataTable dTable = objBAL.GetHolidays(Util.ToInt("" + Session["BRANCHID"]), DateTime.Today.Month, DateTime.Today.Year);
        gvHolidays.DataSource = dTable;
        gvHolidays.DataBind();

        if (dTable.Rows.Count == 0)
            lblHoliday.Text = hrmlang.GetString("norecordsfound");

        if ("" + Session["LanguageId"] != "en-US")
        {
            DataTable dt = new MonthsBAL().Select("" + Session["LanguageId"]);
            DataRow[] dRows = dt.Select("mONTHID=" + DateTime.Today.Month.ToString());
            if (dRows.Length == 0)
                ltMonth.Text = DateTime.Today.ToString("MMMM");
            else
                ltMonth.Text = "" + dRows[0]["MonthName"];

        }
        else
            ltMonth.Text = DateTime.Today.ToString("MMMM");
    }

    protected void gvHolidays_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("holiday");
            e.Row.Cells[1].Text = hrmlang.GetString("description");
        }
    }
    public void selectnotify()
    {
        NotificationBAL notifyBAL = new NotificationBAL();
        DataTable dTablenot = new DataTable();
        dTablenot = notifyBAL.SelectNOTIFY();

        string sText = "";

        for (int i = 0; i < dTablenot.Rows.Count; i++)
        {
            sText += "<li>" + dTablenot.Rows[i]["Notification"] + "</li>";
        }

        ltNotify.Text = sText;
    }
}