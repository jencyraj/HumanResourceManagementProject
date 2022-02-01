using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using HRM.BAL;
using HRM.BOL;

public partial class Signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        if (!IsPostBack)
        {
            SiteConfiguration();
            if (Request.Cookies["ihrUserName"] != null && Request.Cookies["ihrPassword"] != null)
            {
                txtUserID.Text = Request.Cookies["ihrUserName"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["ihrPassword"].Value;
            }
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

    protected void btnSignin_Click(object sender, EventArgs e)
    {
        UserBAL objBAL = new UserBAL();

        HttpCookie hrCookie = new HttpCookie("usersettings");
        Response.Cookies.Add(hrCookie);
        hrCookie.Values.Add("ihrUserName", txtUserID.Text.Trim());

        if (chkRemember.Checked)
        {
            Response.Cookies["usersettings"].Expires = DateTime.Now.AddYears(1);
        }
        else
        {
            Response.Cookies["usersettings"].Expires = DateTime.Now.AddDays(1);
        }


        if (objBAL.SignIn(txtUserID.Text.Trim(), txtPassword.Text.Trim()).ToUpper() == txtUserID.Text.Trim().ToUpper())
        {
            DataTable dT = objBAL.SelectAll(txtUserID.Text.Trim());


            EmployeeBOL objEmp = new EmployeeBOL();
            objEmp.UserID = txtUserID.Text.Trim();// HttpContext.Current.User.Identity.Name;
            objEmp = new EmployeeBAL().Select(objEmp);

            if (0 + objEmp.EmployeeID > 0)
            {

                SetLeaveBalance(objEmp.EmployeeID);

                FormsAuthentication.SetAuthCookie(dT.Rows[0]["UID"].ToString(), true);
                Response.Redirect("Dashboard.aspx");
            }
            else
                ClientScript.RegisterStartupScript(btnSignin.GetType(), "onclick", "alert('No employee profile has been set for this user.Please contact Administrator to create an Employee Profile to signin.');", true);
        }
    }

    private void SetLeaveBalance(int EmployeeID)
    {
        new LeaveBAL().SetLeaveBalance_NewYear(EmployeeID);
    }
}