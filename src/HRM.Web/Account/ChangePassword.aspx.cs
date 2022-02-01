using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;


public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {

        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        objBOL.EmployeeID = Util.ToInt("" + Session["EMPID"]);
        objBOL = objBAL.Select(objBOL);

        if (CurrentPassword.Text != objBOL.Password)
        {
            ClientScript.RegisterStartupScript(ChangePasswordPushButton.GetType(), "onclick", "alert('Current password is wrong.')", true);
        }
        else
        {
            objBOL.Password = NewPassword.Text.Trim();
            objBAL.ChangePassword(objBOL);
            ClientScript.RegisterStartupScript(ChangePasswordPushButton.GetType(), "onclick", "alert('Password changed successfully.')", true);
        }
    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        CurrentPassword.Text = "";
        NewPassword.Text = "";
        ConfirmNewPassword.Text = "";
    }
}
