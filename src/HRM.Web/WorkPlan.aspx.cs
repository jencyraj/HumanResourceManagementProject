using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class WorkPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("enteryear"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("addnew");
        if (!IsPostBack)
        {
            getbranch();
            GetMonthList();
            Search();
        }
    }

    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddlMonth.DataSource = dt;
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddWorkPlan.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        WorkPlanBOL objBOL = new WorkPlanBOL();

        objBOL.EmployeeID = Util.ToInt(hfEmployeeId.Value);
        objBOL.WPMonth = Util.ToInt(ddlMonth.SelectedValue);
        objBOL.WPYear = Util.ToInt(txtYear.Text);
        objBOL.BranchID = Util.ToInt(ddlBranch.SelectedValue);
        DataTable dTable = new WorkPlanBAL().SelectWorkPlanMaster(objBOL);
        gvPlan.DataSource = dTable;
        gvPlan.DataBind();
    }
    protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("year");
            e.Row.Cells[2].Text = hrmlang.GetString("month");
            e.Row.Cells[3].Text = hrmlang.GetString("createdon");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvPlan.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvPlan.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvPlan.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvPlan.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = ddlMonth.Items.FindByValue(lblMonth.Text).Text;

            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }
    protected void gvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            new WorkPlanBAL().DeleteMaster(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("datadeleted");
            Search();
        }
    }
    public void getbranch()
    {

        ddlBranch.DataSource = new OrgBranchesBAL().SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataValueField = "BranchId";
        ddlBranch.DataTextField = "Branch";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, (new ListItem(hrmlang.GetString("all"), "")));
        ListItem lstItem = new ListItem(hrmlang.GetString("select"), "0");


    }
   
   
}