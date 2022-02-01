using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class Commission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("employee"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newcommission");
        if (!IsPostBack)
        {
            BindGrid(null);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CommissionBOL objBOL = new CommissionBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
        txtEmployee.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditCommission.aspx");
    }

    protected void gvCommission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                CommissionBAL objBAL = new CommissionBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("commissiondeleted");
                lblErr.Text = string.Empty;
                CommissionBOL objBOL = new CommissionBOL();
                if (txtEmployee.Text.Trim() == "")
                    hfEmployeeId.Value = "";
                objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
                BindGrid(objBOL);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("commissiondeleteerror");
            }
        }
    }

    protected void gvCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCommission.PageIndex = e.NewPageIndex;
        CommissionBOL objBOL = new CommissionBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
    }

    protected void gvCommission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("title");
            e.Row.Cells[1].Text = hrmlang.GetString("amount");
            e.Row.Cells[2].Text = hrmlang.GetString("commissiondate");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvCommission.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvCommission.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvCommission.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvCommission.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletecommission"));
        }
    }

    private void BindGrid(CommissionBOL objBOL)
    {
        gvCommission.DataSource = new CommissionBAL().SelectAll(objBOL);
        gvCommission.DataBind();
    }
}