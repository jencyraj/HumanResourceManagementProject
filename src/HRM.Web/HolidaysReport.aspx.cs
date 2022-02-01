using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using HRM.BAL;
using HRM.BOL;

public partial class HolidaysReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetBranches();
            btnSearch.Text = hrmlang.GetString("search");
        }
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        OrganisationBAL objOrg = new OrganisationBAL();
        OrganisationBOL objBOL = objOrg.Select();
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");
        if (objBOL != null)
        {
            ddlBr.DataSource = objBAL.SelectAll(objBOL.CompanyID);

            ddlBr.DataBind();
        }
        ddlBr.Items.Insert(0, lstItem);
    }

    private void GetHolidays()
    {
       /* HolidayBAL objBAL = new HolidayBAL();
        DataTable dTable = objBAL.SelectAll(Util.ToInt(ddlBr.SelectedValue));

        if (dTable.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("recordnotfound");
        else
        {
            Session["rptdata"] = dTable;*/
            string url = "HRMReports.aspx?rptname=HolidayRpt&printtype=" + rbtnPrint.SelectedValue+"&sp1=" + ddlBr.SelectedValue+"&rptcase=HOLIDAY";
            string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
            //  string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetHolidays();
    }
}