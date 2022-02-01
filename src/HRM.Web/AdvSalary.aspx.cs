using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AdvSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("employee"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newadvancedsalary");
        if (!IsPostBack)
        {
            BindGrid(null);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AdvSalaryBOL objBOL = new AdvSalaryBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
        txtEmployee.Focus();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditAdvSalary.aspx");
    }

    protected void gvAdvSalary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("title");
            e.Row.Cells[2].Text = hrmlang.GetString("amount");
            e.Row.Cells[3].Text = hrmlang.GetString("salarydate");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAdvSalary.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAdvSalary.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAdvSalary.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAdvSalary.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteadvsalary"));
        }
    }

    protected void gvAdvSalary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                AdvSalaryBAL objBAL = new AdvSalaryBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("advsalarydeleted");
                lblErr.Text = string.Empty;
                AdvSalaryBOL objBOL = new AdvSalaryBOL();
                if (txtEmployee.Text.Trim() == "")
                    hfEmployeeId.Value = "";
                objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
                BindGrid(objBOL);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("advsalarydeleteerror");
            }
        }
    }

    protected void gvAdvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAdvSalary.PageIndex = e.NewPageIndex;
        AdvSalaryBOL objBOL = new AdvSalaryBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
    }

    private void BindGrid(AdvSalaryBOL objBOL)
    {
        gvAdvSalary.DataSource = new AdvSalaryBAL().SelectAll(objBOL);
        gvAdvSalary.DataBind();
    }

}