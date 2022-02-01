using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;
using System.Data;

public partial class AddEditHourlyWage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtRegularHours.Attributes.Add("placeholder", hrmlang.GetString("enterregularhours"));
        txtOverTimeHours.Attributes.Add("placeholder", hrmlang.GetString("enterovertimehours"));
        txtAddInfo.Attributes.Add("placeholder", hrmlang.GetString("enteradditionalinfo"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        btnSaveDialog.Text = hrmlang.GetString("ok");
        if (!IsPostBack)
        {
            LoadDesignation();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nHourlyWageId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nHourlyWageId);
                hfNew.Value = "0";
            }
            else
            {
                hfNew.Value = "1";
                chkActive.Checked = true;
            }
            hfActive.Value = "" + Request.QueryString["isactive"];
            GetMonthList();
        }
    }
    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddlMonth.DataSource = dt;
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }
    private void LoadDesignation()
    {
        OrgDesignationBAL objDesgn = new OrgDesignationBAL();
        ddlDesgn.DataSource = objDesgn.SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlDesgn.DataBind();

        ListItem lstItem = new ListItem();
        lstItem.Text = "[SELECT]";
        lstItem.Value = "";

        ddlDesgn.Items.Insert(0, lstItem);
    }
    protected void lnkActive_Click(object sender, EventArgs e)
    {
        if (hfNew.Value != "1")
        {
            hfNew.Value = "1";
        }
        if (hfActive.Value != "Y")
        {
            hfActive.Value = "Y";
        }
        hfIsHourlyWagePresent.Value = new HourlyWageBAL().ActiveHourWagePresent(Util.ToInt(hfEmployeeId.Value)).ToString();
        txtEmployee.Focus();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("HourlyWages.aspx");
    }

    protected void lnkPostBack_Click(object sender, EventArgs e)
    {
        SaveHourlyWage();
        Response.Redirect("HourlyWages.aspx");
    }

    private void LoadDetails(int nHourlyWageId)
    {
        HourlyWageBOL objBOL = new HourlyWageBOL();
        objBOL = new HourlyWageBAL().SearchById(nHourlyWageId);
        if (objBOL.DesignationId == 0)
            ddlDesgn.SelectedValue = "";
        else
            ddlDesgn.SelectedValue = "" + objBOL.DesignationId;
        hfEmployeeId.Value = "" + objBOL.EmployeeId;
        txtRegularHours.Text = objBOL.RegularHours.ToString();
        txtOverTimeHours.Text = objBOL.OverTimeHours.ToString();
        txtovertimeweknd.Text = objBOL.OverTimewekend.ToString();
        txtEmployee.Text = objBOL.FirstName + ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName != "") ? " " + objBOL.LastName : "");
        txtAddInfo.Text = objBOL.AdditionalInfo;
        chkActive.Checked = objBOL.ActiveWage == "Y" ? true : false;
    }

    private void SaveHourlyWage()
    {
        int nHourlyWageId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]) && string.IsNullOrEmpty(hfIsHourlyWagePresent.Value) && hfNew.Value == "0")
        {
            nHourlyWageId = Util.ToInt(Request.QueryString["id"]);
        }
        HourlyWageBOL objBOL = new HourlyWageBOL();
        objBOL.HourlyWageId = nHourlyWageId;
        objBOL.DesignationId = Util.ToInt(ddlDesgn.SelectedValue);
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.RegularHours = Util.ToDecimal(txtRegularHours.Text.Trim());
        objBOL.OverTimeHours = Util.ToDecimal(txtOverTimeHours.Text.Trim());
        objBOL.OverTimewekend = Util.ToDecimal(txtovertimeweknd.Text.Trim());
        objBOL.AdditionalInfo = txtAddInfo.Text.Trim();
        objBOL.HMonth = Util.ToInt(ddlMonth.SelectedValue);
        objBOL.HYear = Util.ToInt(txtyear.Text);
        objBOL.ActiveWage = chkActive.Checked ? "Y" : "N";
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        if (chkActive.Checked)
        {
            if (!string.IsNullOrEmpty(hfIsHourlyWagePresent.Value))
            {
                objBOL.HourlyWagePresent = Util.ToInt(hfIsHourlyWagePresent.Value);
            }
            else if (string.IsNullOrEmpty(hfIsHourlyWagePresent.Value))
            {
                objBOL.HourlyWagePresent = new HourlyWageBAL().ActiveHourWagePresent(Util.ToInt(hfEmployeeId.Value));
            }
        }
        else
        {
            objBOL.HourlyWagePresent = 0;
        }
        HourlyWageBAL objBAL = new HourlyWageBAL();
        objBAL.Save(objBOL);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (new HourlyWageBAL().ActiveHourWagePresent(Util.ToInt(hfEmployeeId.Value)) > 0)
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "$('#dvConfirm').modal();", true);
        else
        {
            SaveHourlyWage();
            lblMsg.Text = hrmlang.GetString("hourlywagesaved");
          //  Response.Redirect("HourlyWages.aspx");
        }
    }
  
}