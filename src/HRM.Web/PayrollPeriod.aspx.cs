using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;


public partial class PayrollPeriod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtYear.Attributes.Add("placeholder", hrmlang.GetString("enteryear"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newpayrollperiod");

        if (!IsPostBack)
        {
            BindGrid(true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblErr.Text = string.Empty;
        lblMsg.Text = string.Empty;
        BindGrid(false);
    }

    protected void gvPayrollPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("title");
            e.Row.Cells[1].Text = hrmlang.GetString("startperiod");
            e.Row.Cells[2].Text = hrmlang.GetString("endperiod");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvPayrollPeriod.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvPayrollPeriod.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvPayrollPeriod.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvPayrollPeriod.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletepayrollperiod"));
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditPayrollPeriod.aspx");
    }

    protected void gvPayrollPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                PayrollPeriodBAL objBAL = new PayrollPeriodBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("payrollperioddeleted");
                lblErr.Text = string.Empty;
                BindGrid(false);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("payrollperioddeleteerror");
            }
        }
    }

    protected void gvPayrollPeriod_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayrollPeriod.PageIndex = e.NewPageIndex;
        BindGrid(false);
    }

    private void BindGrid(bool IsLoad)
    {
        if (IsLoad)
        {
            PayrollPeriodBAL objBAL = new PayrollPeriodBAL();
            gvPayrollPeriod.DataSource = objBAL.SelectAll(new PayrollPeriodBOL());
            gvPayrollPeriod.DataBind();
        }
        else
        {
            PayrollPeriodBAL objBAL = new PayrollPeriodBAL();
            PayrollPeriodBOL objBOL = new PayrollPeriodBOL();
            if (!string.IsNullOrEmpty(txtYear.Text))
            {
                objBOL.Year = txtYear.Text;
            }
            gvPayrollPeriod.DataSource = objBAL.SelectAll(objBOL);
            gvPayrollPeriod.DataBind();
        }
    }
}