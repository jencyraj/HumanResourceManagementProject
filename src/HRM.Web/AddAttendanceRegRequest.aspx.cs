using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AddAttendanceRegRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "attendanceregularizationrequest.aspx");

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtYear.Text = lblYear.Text = DateTime.Today.Year.ToString();
            lblEmpID.Text = "" + Session["EMPID"];
          
            GetRequestDetails();
        }
    }


   

    private void GetRequestDetails()
    {
        AttRegularBOL obj = new AttRegularBAL().SelectByID(Util.ToInt("" + Request.QueryString["id"]));
        if (obj != null)
        {
            txtDesc.Text = obj.ReqReason;
          
            txtYear.Text = lblYear.Text = obj.ReqYear.ToString();
            lblEmpID.Text = obj.EmployeeID.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AttRegularBOL obj = new AttRegularBOL();

           // obj.ReqID = Util.ToInt("" + Request.QueryString["id"]); obj.ReqMonth = Util.ToInt(ddlMonth.SelectedValue);
            obj.ReqYear = Util.ToInt(txtYear.Text);
            obj.Status = "Y";
            obj.ReqReason = txtDesc.Text.Trim();
            obj.EmployeeID = Util.ToInt(lblEmpID.Text);
            obj.CreatedBy = User.Identity.Name;
            new AttRegularBAL().Save(obj);
            Response.Redirect("AttendanceRegularizationRequest.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtDesc.Text = "";
      
        txtYear.Text = lblYear.Text = DateTime.Today.Year.ToString();
        GetRequestDetails();
    }
}