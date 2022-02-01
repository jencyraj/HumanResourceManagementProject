using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddToIris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "jobrequests.aspx");
            GetBranches();
            GetIrisDevices();

        }
    }

    protected void gvIrisData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("IPAddress");
            e.Row.Cells[1].Text = hrmlang.GetString("UserName");
            //e.Row.Cells[2].Text = hrmlang.GetString("branch");
            //e.Row.Cells[3].Text = hrmlang.GetString("department");
            //e.Row.Cells[4].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvIrisData.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvIrisData.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvIrisData.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvIrisData.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    protected void gvIrisData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GetIrisDevices();
        gvIrisData.PageIndex = e.NewPageIndex;
    }

    protected void btnHolidays_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            IrisDataBAL objBAL = new IrisDataBAL();

            DataTable dTable = new DataTable();

            dTable.Columns.Add("IPAddresss");
            dTable.Columns.Add("UserName");
            dTable.Columns.Add("SecurityId");
            dTable.Columns.Add("Password");
            dTable.Columns.Add("IRisId");

            bool isChecked = false;

            foreach (GridViewRow gRow in gvIrisData.Rows)
            {
                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblIP")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblUserName")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblSecurityId")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblPassword")).Text;
                dRow[4] = ((Label)gRow.FindControl("lblIrisId")).Text;

                if (((CheckBox)gRow.FindControl("chkSelect")).Checked)
                {
                    isChecked = true;
                    dTable.Rows.Add(dRow);
                }
            }

            if (isChecked == false)
            {
                lblErr.Text = "Please select a device to synchronize";
                lblMsg.Text = string.Empty;
                return;
            }

            string errorMsg = objBAL.SaveHolidays(dTable, Util.ToInt(ddlBr.SelectedValue));

            if (!string.IsNullOrEmpty(errorMsg))
            {
                lblErr.Text = errorMsg;
                lblMsg.Text = string.Empty;
            }
            else
            {
                lblErr.Text = string.Empty;
                lblMsg.Text = hrmlang.GetString("datasaved");
            }

            //Response.Redirect("~/JobRequests.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
            lblMsg.Text = string.Empty;
        }
    }

    protected void btnTransactions_Click(object sender, EventArgs e)
    {
        try
        {
            IrisDataBAL objBAL = new IrisDataBAL();

            DataTable dTable = new DataTable();
            dTable.Columns.Add("IPAddresss");
            dTable.Columns.Add("UserName");
            dTable.Columns.Add("SecurityId");
            dTable.Columns.Add("Password");
            dTable.Columns.Add("IrisId");

            bool isChecked = false;

            foreach (GridViewRow gRow in gvIrisData.Rows)
            {
                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblIP")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblUserName")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblSecurityId")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblPassword")).Text;
                dRow[4] = ((Label)gRow.FindControl("lblIrisId")).Text;

                if (((CheckBox)gRow.FindControl("chkSelect")).Checked)
                {
                    dTable.Rows.Add(dRow);
                    isChecked = true;
                }
            }

            if (isChecked == false)
            {
                lblErr.Text = "Please select a device to synchronize";
                lblMsg.Text = string.Empty;
                return;
            }

            string startDate = "";
            string endDate = "";

            if (ctlStartDate.getGregorianDateText != "")
            {
                startDate = Util.RearrangeDateTime(ctlStartDate.getGregorianDateText);
            }
            else 
            {
                lblErr.Text = "Please select a start date";
                lblMsg.Text = string.Empty;
                return;
            }

            if (ctlEndDate.getGregorianDateText != "")
            {
                endDate = Util.RearrangeDateTime(ctlEndDate.getGregorianDateText);
            }
            else
            {
                lblErr.Text = "Please select a end date";
                lblMsg.Text = string.Empty;
                return;
            }

            string errorMsg = objBAL.SaveTransactions(dTable, startDate, endDate);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                lblErr.Text = errorMsg;
                lblMsg.Text = string.Empty;
            }
            else
            {
                lblErr.Text = string.Empty;
                lblMsg.Text = hrmlang.GetString("datasaved");
            }

            //Response.Redirect("~/JobRequests.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
            lblMsg.Text = string.Empty;
        }
    }

    protected void ddlBr_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIrisDevices();
    }

    private void GetIrisDevices()
    {
        IrisDataBAL objBAL = new IrisDataBAL();
        gvIrisData.DataSource = objBAL.SelectAll(Util.ToInt(ddlBr.SelectedValue));
        gvIrisData.DataBind();
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
        if ("" + Session["ROLEID"] != "4")
        {
            ddlBr.Items.Insert(0, lstItem);
        }
    }
}