using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;
using System.Data;

public partial class AddTermination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            lblTID.Text = Request.QueryString["TID"];

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("employeename"));
            txtForwardTo.Attributes.Add("placeholder", hrmlang.GetString("forwardto"));
            txtReason.Attributes.Add("placeholder", hrmlang.GetString("description"));

            GetCompanyDetails();

            if (!String.IsNullOrEmpty(lblTID.Text))
                GetEmployeeTermination();


        }
        pnlNew.Visible = true;
    }

    private void GetEmployeeTermination()
    {
        TerminationBOL objBOL = new TerminationBOL();
        TerminationBAL objBAL = new TerminationBAL();
        objBOL = objBAL.SelectByID(Util.ToInt(lblTID.Text));

        lblTID.Text = objBOL.TID.ToString();
        txtEmployee.Text = objBOL.EmployeeName;
        txtForwardTo.Text = objBOL.ForwardedToName;
            hfEmployeeId.Value = objBOL.EmployeeID.ToString();
            hfForwardTo.Value = objBOL.ForwardedTo.ToString();
            txtReason.Text = objBOL.Reason;
            
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            TerminationBAL objBAL = new TerminationBAL();
            TerminationBOL objBol = new TerminationBOL();

            if (!String.IsNullOrEmpty(lblTID.Text))
                objBol.TID = Convert.ToInt32(lblTID.Text);

            if (hfEmployeeId.Value != "")
                objBol.EmployeeID = Util.ToInt(hfEmployeeId.Value);

            if (hfForwardTo.Value != "")
                objBol.ForwardedTo = Util.ToInt(hfForwardTo.Value);

            objBol.CreatedBy = Util.ToInt(Session["EMPID"]);

            objBol.Reason = txtReason.Text;

            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("datasaved");

            Response.Redirect("~/Termination.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblTID.Text = "";
        txtEmployee.Text = "";
        txtForwardTo.Text = "";
        txtReason.Text = "";

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
}