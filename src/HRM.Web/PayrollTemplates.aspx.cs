using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class PayrollTemplates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("addnew");
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if ("" + Session["USETAX"] != "")
        {
            string sQueryString = ("" + Request.QueryString["id"] == "") ? "" : "?id=" + Request.QueryString["id"];
            Response.Redirect("PayrollTemplateNew.aspx" + sQueryString);
        }
        else
            Response.Redirect("PayrollTemplate.aspx");
    }

    protected void gvPayrollTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("designation");
            e.Row.Cells[1].Text = hrmlang.GetString("employee");
            e.Row.Cells[2].Text = hrmlang.GetString("basicsalary");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvPayrollTemplates.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvPayrollTemplates.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvPayrollTemplates.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvPayrollTemplates.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletepayrolltemplate"));

            if ("" + Session["USETAX"] != "")
                lnkEdit.NavigateUrl = "PayrollTemplateNew.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "PMId");
            else
                lnkEdit.NavigateUrl = "PayrollTemplate.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "PMId");
        }

    }
    protected void gvPayrollTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                PayrollTemplateBAL objBAL = new PayrollTemplateBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("payrolltemplatedeleted");
                lblErr.Text = string.Empty;
                BindGrid();
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("payrolltemplatedeleteerror");
            }
        }
    }

    protected void gvPayrollTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayrollTemplates.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        gvPayrollTemplates.DataSource = new PayrollTemplateBAL().SelectAllPayrollTemplates();
        gvPayrollTemplates.DataBind();
    }
}