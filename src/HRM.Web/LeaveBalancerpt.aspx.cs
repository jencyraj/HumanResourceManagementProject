using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class LeaveBalancerpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        lblErr.Text = "";
        lblSubMsg.Text = "";
        txtEmpCode.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecode"));
        txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
        txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
        txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));
        btnSearch.Text = hrmlang.GetString("viewrpt");
       
        if (!IsPostBack)
        {
            GetCompanyDetails();
            GetDropDownValues();
            GetMonthList();
            ddMonth.SelectedValue =""+( DateTime.Now.Month);// modified By jency on 27/09/16
            txtYear.Text =""+ DateTime.Now.Year;// "
            if ("" + Session["ROLEID"] == "4")
            {
                pnlAll.Visible = false;
            
            }
        }

    }
    private void GetMonthList()// modified By jency on 27/09/16
    {

        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddMonth.DataSource = dt;
        ddMonth.DataBind();
        ddMonth.Items.Insert(0, hrmlang.GetString("all"));

    }
    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

    }

    private void GetDropDownValues()
    {
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "");

        OrgDepartmentsBAL objDept = new OrgDepartmentsBAL();
        ddlDept.DataSource = objDept.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDept.DataBind();

        OrgBranchesBAL objBr = new OrgBranchesBAL();
        ddlBranch.DataSource = objBr.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlBranch.DataBind();

        ddlDept.Items.Insert(0, lstItem);
        ddlBranch.Items.Insert(0, lstItem);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtYear.Text == "")// modified By jency on 27/09/16
        {
            ClientScript.RegisterStartupScript(btnSearch.GetType(), "onclick", "alert('Please Enter the  Year');", true);
            txtYear.Focus();
            return;
        }
        else if (ddMonth.SelectedValue == "All")// modified By jency on 27/09/16
        {
            ClientScript.RegisterStartupScript(btnSearch.GetType(), "onclick", "alert('Please Select the  Month');", true);
            ddMonth.Focus();
            return;

        }
        else
        {
            string sParams = "&sp1=" + ddlBranch.SelectedValue + "&sp2=" + ddlDept.SelectedValue + "&sp3=" + txtfName.Text + "&sp4=" + txtmName.Text + "&sp5=" + txtlName.Text + "&sp6=" + txtEmpCode.Text + "&sp7=" + txtYear.Text + "&sp8=" + ddMonth.SelectedValue;
            string url = "HRMReports.aspx?rptname=LeaveBalanceRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=LVBAL";
            string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
    }
    
   
 }



     
