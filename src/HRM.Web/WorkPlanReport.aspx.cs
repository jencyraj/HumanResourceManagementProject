using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;


public partial class WorkPlanReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("enteryear"));
        btnSearch.Text = hrmlang.GetString("viewrpt");
     
        if (!IsPostBack)
        {
            GetMonthList();
         
            BindBranchDD();
        }

    }
    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddlMonth.DataSource = dt;
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }


   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        //WorkPlanBOL objBOL = new WorkPlanBOL();

        //objBOL.EmployeeID = Util.ToInt(hfEmployeeId.Value);
        //objBOL.WPMonth = Util.ToInt(ddlMonth.SelectedValue);
        //objBOL.WPYear = Util.ToInt(txtYear.Text);
        //objBOL.BranchID = Util.ToInt(ddBranches.SelectedValue);
        //DataTable dTable = new WorkPlanBAL().SelectWorkSchedule(objBOL);


        string sParams = "&sp1=" + hfEmployeeId.Value + "&sp2=" + ddlMonth.SelectedItem.Text + "&sp3=" + txtYear.Text + "&sp4=" + ddBranches.SelectedValue ;
        string url = "HRMReports.aspx?rptname=Workplanrpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=WORKSCHE";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

      
    }
    private void BindBranchDD()
    {
        ddBranches.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddBranches.DataValueField = "BranchId";
        ddBranches.DataTextField = "Branch";
        ddBranches.DataBind();
        ddBranches.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "0")));
    }
}