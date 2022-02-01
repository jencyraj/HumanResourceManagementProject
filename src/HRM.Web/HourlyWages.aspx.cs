using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class HourlyWages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("addnewhourlywage");
        if (!IsPostBack)
        {
            chkActive.Checked = true;
            BindGrid(null);


            OrgDesignationBAL objDesgn = new OrgDesignationBAL();
            ddlDesgn.DataSource = objDesgn.SelectAll(Util.ToInt(Session["COMPANYID"]));
            ddlDesgn.DataBind();

            ListItem lstItem = new ListItem();
            lstItem.Text = "[SELECT]";
            lstItem.Value = "";

            ddlDesgn.Items.Insert(0, lstItem);
        }
    }

    protected void gvHourlyWages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("hourlywagefor");
            e.Row.Cells[1].Text = hrmlang.GetString("regularhours");
            e.Row.Cells[2].Text = hrmlang.GetString("overtimehours");
            e.Row.Cells[3].Text = hrmlang.GetString("overtimehourswk");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvHourlyWages.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvHourlyWages.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvHourlyWages.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvHourlyWages.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));

            Label lblWageFor = (Label)e.Row.FindControl("lblWageFor");
            Label lblEmployee = (Label)e.Row.FindControl("lblEmployee");
            Label lblDesignation = (Label)e.Row.FindControl("lblDesignation");

            if (lblEmployee.Text != "")
                lblWageFor.Text = lblEmployee.Text;
            else
                lblWageFor.Text = lblDesignation.Text;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HourlyWageBOL objBOL = new HourlyWageBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.ActiveWage = chkActive.Checked ? "Y" : "N";
        objBOL.DesignationId = Util.ToInt(ddlDesgn.SelectedValue);
        BindGrid(objBOL);
        txtEmployee.Focus();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("AddEditHourlyWage.aspx?isactive={0}", chkActive.Checked ? "Y" : "N"));
    }

    protected void gvHourlyWages_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                HourlyWageBAL objBAL = new HourlyWageBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("datadeleted");
                lblErr.Text = string.Empty;
                HourlyWageBOL objBOL = new HourlyWageBOL();
                if (txtEmployee.Text.Trim() == "")
                    hfEmployeeId.Value = "";
                objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
                objBOL.ActiveWage = chkActive.Checked ? "Y" : "N";
                BindGrid(objBOL);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("hourlywagedeleteerror");
            }
        }
    }

    protected void gvHourlyWages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHourlyWages.PageIndex = e.NewPageIndex;
        HourlyWageBOL objBOL = new HourlyWageBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        objBOL.ActiveWage = chkActive.Checked ? "Y" : "N";
        objBOL.DesignationId = Util.ToInt(ddlDesgn.SelectedValue);
        BindGrid(objBOL);
    }

    private void BindGrid(HourlyWageBOL objBOL)
    {
        if (objBOL == null)
        {
            objBOL = new HourlyWageBOL();
            objBOL.EmployeeId = 0;
            objBOL.ActiveWage = chkActive.Checked ? "Y" : "N";
        }
        gvHourlyWages.DataSource = new HourlyWageBAL().SelectAll(objBOL);
        gvHourlyWages.DataBind();
    }
}