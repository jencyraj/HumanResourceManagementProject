using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AddEditBonus : System.Web.UI.Page
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
            ctlCalendarBD.SelectedCalendareDate = DateTime.Today;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nBonusId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nBonusId);
            }
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                SaveBonus();
                lblMsg.Text = hrmlang.GetString("bonussaved");
               // Response.Redirect("Bonus.aspx");

            }
            catch
            {
                lblErr.Text = hrmlang.GetString("bonussaveerror");
            }
        }

        if (e.CommandName == "CANCEL")
        {
            Response.Redirect("Bonus.aspx");
        }

    }

    private void LoadDetails(int nBonusId)
    {
        BonusBOL objBOL = new BonusBOL();
        objBOL = new BonusBAL().SearchById(nBonusId);
        hfEmployeeId.Value = "" + objBOL.EmployeeId;
        txtTitle.Text = objBOL.Title;
        txtAmount.Text = objBOL.Amount.ToString();
        if (!string.IsNullOrEmpty(objBOL.BonusDate))
        {
            ctlCalendarBD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            ctlCalendarBD.SelectedCalendareDate = DateTime.Parse(objBOL.BonusDate);
        }
        else
        {
            if ("" + objBOL.BonusDateAR != "")
            {
                ctlCalendarBD.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                ctlCalendarBD.SelectedCalendareDate = DateTime.Parse(objBOL.BonusDate);
            }
        }
        txtEmployee.Text = objBOL.FirstName + ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName != "") ? " " + objBOL.LastName : "");
        txtDescription.Text = objBOL.Description;
        txtAddInfo.Text = objBOL.AdditionalInfo;
    }

    private void SaveBonus()
    {
        int nBonusId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            nBonusId = Util.ToInt(Request.QueryString["id"]);
        }
        BonusBOL objBOL = new BonusBOL();
        objBOL.BonusId = nBonusId;
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.Title = txtTitle.Text.Trim();
        objBOL.Amount = Util.ToDecimal(txtAmount.Text.Trim());
        if ("" + ctlCalendarBD.getGregorianDateText != "")
        {
            string[] BonusDateArr = ctlCalendarBD.getGregorianDateText.ToString().Split('/');
            objBOL.BonusDate = BonusDateArr[1] + "/" + BonusDateArr[0] + "/" + BonusDateArr[2];
        }
        objBOL.BonusDateAR = ctlCalendarBD.getHijriDateText;
        objBOL.Description = txtDescription.Text.Trim();
        objBOL.AdditionalInfo = txtAddInfo.Text.Trim();
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        BonusBAL objBAL = new BonusBAL();
        objBAL.Save(objBOL);
    }

}