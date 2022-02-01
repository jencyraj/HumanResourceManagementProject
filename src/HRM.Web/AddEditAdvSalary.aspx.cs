using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AddEditAdvSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtTitle.Attributes.Add("placeholder", hrmlang.GetString("entertitle"));
        txtAmount.Attributes.Add("placeholder", hrmlang.GetString("enteramount"));
        txtAddInfo.Attributes.Add("placeholder", hrmlang.GetString("enteradditionalinfo"));
        btnCancel.Text = hrmlang.GetString("cancel");
        btnSave.Text = hrmlang.GetString("save");
        if (!IsPostBack)
        {
            ctlCalendarASD.SelectedCalendareDate = DateTime.Today;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nAdvSalaryId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nAdvSalaryId);
            }
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        try
        {
            SaveAdvSalary();
            Response.Redirect("AdvSalary.aspx");
        }
        catch
        {
            lblErr.Text = hrmlang.GetString("advsalsaveerror");
        }
    }

    private void LoadDetails(int nAdvSalaryId)
    {
        AdvSalaryBOL objBOL = new AdvSalaryBOL();
        objBOL = new AdvSalaryBAL().SearchById(nAdvSalaryId);
        hfEmployeeId.Value = "" + objBOL.EmployeeId;
        txtTitle.Text = objBOL.Title;
        txtAmount.Text = objBOL.Amount.ToString();
        if (!string.IsNullOrEmpty(objBOL.SalaryDate))
        {
            ctlCalendarASD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarASD.SelectedCalendareDate = DateTime.Parse(objBOL.SalaryDate);
        }
        else
        {
            if ("" + objBOL.SalaryDateAR != "")
            {
                ctlCalendarASD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                ctlCalendarASD.SelectedCalendareDate = DateTime.Parse(objBOL.SalaryDate);
            }
        }
        txtEmployee.Text = objBOL.FirstName + ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName != "") ? " " + objBOL.LastName : "");
        txtAddInfo.Text = objBOL.AdditionalInfo;
    }

    private void SaveAdvSalary()
    {
        int nAdvSalaryId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            nAdvSalaryId = Util.ToInt(Request.QueryString["id"]);
        }
        AdvSalaryBOL objBOL = new AdvSalaryBOL();
        objBOL.AdvSalaryId = nAdvSalaryId;
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.Title = txtTitle.Text.Trim();
        objBOL.Amount = Util.ToDecimal(txtAmount.Text.Trim());
        if ("" + ctlCalendarASD.getGregorianDateText != "")
        {
            string[] SalaryDateArr = ctlCalendarASD.getGregorianDateText.ToString().Split('/');
            objBOL.SalaryDate = SalaryDateArr[1] + "/" + SalaryDateArr[0] + "/" + SalaryDateArr[2];
        }
        objBOL.SalaryDateAR = ctlCalendarASD.getHijriDateText;
        objBOL.AdditionalInfo = txtAddInfo.Text.Trim();
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        AdvSalaryBAL objBAL = new AdvSalaryBAL();
        objBAL.Save(objBOL);
    }
}