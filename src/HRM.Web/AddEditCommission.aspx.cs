using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AddEditCommission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtTitle.Attributes.Add("placeholder", hrmlang.GetString("entertitle"));
        txtAmount.Attributes.Add("placeholder", hrmlang.GetString("enteramount"));
        txtDescription.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtAddInfo.Attributes.Add("placeholder", hrmlang.GetString("enteradditionalinfo"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ctlCalendarCD.SelectedCalendareDate = DateTime.Today;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nCommissionId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nCommissionId);
            }
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                SaveCommission();
                lblMsg.Text = hrmlang.GetString("commisionsaved");
            }
            catch 
            {
                lblErr.Text = hrmlang.GetString("commissionsaveerror");
            }
        }
        if (e.CommandName=="CANCEL")
        {
            Response.Redirect("Commission.aspx");
            
        }
      //  Response.Redirect("Commission.aspx");
    }


    private void LoadDetails(int nCommissionId)
    {
        CommissionBOL objBOL = new CommissionBOL();
        objBOL = new CommissionBAL().SearchById(nCommissionId);
        hfEmployeeId.Value = "" + objBOL.EmployeeId;
        txtTitle.Text = objBOL.Title;
        txtAmount.Text = objBOL.Amount.ToString();
        if (!string.IsNullOrEmpty(objBOL.CommissionDate))
        {
            ctlCalendarCD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarCD.SelectedCalendareDate = DateTime.Parse(objBOL.CommissionDate);
        }
        else
        {
            if ("" + objBOL.CommissionDateAR != "")
            {
                ctlCalendarCD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                ctlCalendarCD.SelectedCalendareDate = DateTime.Parse(objBOL.CommissionDate);
            }
        }
        txtEmployee.Text = objBOL.FirstName + ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName != "") ? " " + objBOL.LastName : "");
        txtDescription.Text = objBOL.Description;
        txtAddInfo.Text = objBOL.AdditionalInfo;
    }

    private void SaveCommission()
    {
        int nCommissionId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            nCommissionId = Util.ToInt(Request.QueryString["id"]);
        }
        CommissionBOL objBOL = new CommissionBOL();
        objBOL.CommissionId = nCommissionId;
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.Title = txtTitle.Text.Trim();
        objBOL.Amount = Util.ToDecimal(txtAmount.Text.Trim());
        string[] CommissionDateArr = ctlCalendarCD.getGregorianDateText.ToString().Split('/');
        objBOL.CommissionDate = CommissionDateArr[1] + "/" + CommissionDateArr[0] + "/" + CommissionDateArr[2];
        objBOL.CommissionDateAR = ctlCalendarCD.getHijriDateText;
        objBOL.Description = txtDescription.Text.Trim();
        objBOL.AdditionalInfo = txtAddInfo.Text.Trim();
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        CommissionBAL objBAL = new CommissionBAL();
        objBAL.Save(objBOL);
    }

}