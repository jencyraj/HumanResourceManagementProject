using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class Bonus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmployee.Attributes.Add("placeholder", hrmlang.GetString("enteremployee"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newbonus");
        if (!IsPostBack)
        {
            BindGrid(null);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BonusBOL objBOL = new BonusBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
        txtEmployee.Focus();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditBonus.aspx");
       
    }

    protected void gvBonus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                BonusBAL objBAL = new BonusBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("bonusdeleted");
                lblErr.Text = string.Empty;
                BonusBOL objBOL = new BonusBOL();
                if (txtEmployee.Text.Trim() == "")
                    hfEmployeeId.Value = "";
                objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
                BindGrid(objBOL);
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("bonusdeleteerror");
            }
        }
    }

    protected void gvBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBonus.PageIndex = e.NewPageIndex;
        BonusBOL objBOL = new BonusBOL();
        if (txtEmployee.Text.Trim() == "")
            hfEmployeeId.Value = "";
        objBOL.EmployeeId = Util.ToInt(hfEmployeeId.Value);
        BindGrid(objBOL);
    }

    protected void gvBonus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("title");
            e.Row.Cells[2].Text = hrmlang.GetString("amount");
            e.Row.Cells[3].Text = hrmlang.GetString("bonusdate");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvBonus.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvBonus.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvBonus.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvBonus.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletebonus"));
        }
    }

    private void BindGrid(BonusBOL objBOL)
    {
        gvBonus.DataSource = new BonusBAL().SelectAll(objBOL);
        gvBonus.DataBind();
    }
}