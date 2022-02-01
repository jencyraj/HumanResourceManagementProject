using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AddEditPayrollPeriod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtTitle.Attributes.Add("placeholder", hrmlang.GetString("entertitle"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        hfMessage.Value = hrmlang.GetString("pleaseentertitle");
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nPPId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nPPId);
            }
            else
            {
                ctlCalendarSP.SelectedCalendareDate = DateTime.Now;
                ctlCalendarEP.SelectedCalendareDate = DateTime.Now;
            }
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                SavePayrollPeriod();
                lblMsg.Text = hrmlang.GetString("payrollperiodsaved");
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("payrollperiodsaveerror");
            }
        }
        if (e.CommandName == "CANCEL")
        {
            Response.Redirect("PayrollPeriod.aspx");
        }


      //  Response.Redirect("PayrollPeriod.aspx");
    }

    private void LoadDetails(int nPPId)
    {
        PayrollPeriodBOL objBOL = new PayrollPeriodBAL().SearchById(nPPId);
        txtTitle.Text = objBOL.Title;
        ctlCalendarSP.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
        ctlCalendarSP.SelectedCalendareDate = DateTime.Parse(string.Format("{0}/{1}/{2}", objBOL.MonthStart, objBOL.DayStart, objBOL.YearStart));
        ctlCalendarEP.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
        ctlCalendarEP.SelectedCalendareDate = DateTime.Parse(string.Format("{0}/{1}/{2}", objBOL.MonthEnd, objBOL.DayEnd, objBOL.YearEnd));
        chkActive.Checked = objBOL.Active == "Y" ? true : false;
    }

    private void SavePayrollPeriod()
    {
        int nPPId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            nPPId = Util.ToInt(Request.QueryString["id"]);
        }
        PayrollPeriodBOL objBOL = new PayrollPeriodBOL();
        objBOL.PPId = nPPId;
        objBOL.Title = txtTitle.Text.Trim();
        if ("" + ctlCalendarSP.getGregorianDateText != "")
        {
            string[] StartPeriodArr = ctlCalendarSP.getGregorianDateText.ToString().Split('/');
            objBOL.DayStart = Util.ToInt(StartPeriodArr[0]);
            objBOL.MonthStart = Util.ToInt(StartPeriodArr[1]);
            objBOL.YearStart = Util.ToInt(StartPeriodArr[2]);
        }
        if ("" + ctlCalendarEP.getGregorianDateText != "")
        {
            string[] EndPeriodArr = ctlCalendarEP.getGregorianDateText.ToString().Split('/');
            objBOL.DayEnd = Util.ToInt(EndPeriodArr[0]);
            objBOL.MonthEnd = Util.ToInt(EndPeriodArr[1]);
            objBOL.YearEnd = Util.ToInt(EndPeriodArr[2]);
        }
        objBOL.Active = chkActive.Checked ? "Y" : "N";
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        PayrollPeriodBAL objBAL = new PayrollPeriodBAL();
        objBAL.Save(objBOL);
    }

}