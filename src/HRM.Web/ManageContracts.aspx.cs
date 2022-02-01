using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class ManageContracts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearch.Attributes.Add("placeholder", hrmlang.GetString("searchtext"));
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newcontract");
        if (!IsPostBack)
        {
            BindGrid();
            if (Request.QueryString["saved"] == "1")
            {
                if ("" + Session["contractsaved"] == "")
                {
                    Session["contractsaved"] = "1";
                    lblMsg.Text = hrmlang.GetString("datasaved");
                }
            }
            GetContractTypes();
        }
    }

    private void GetContractTypes()
    {
        DataTable dt = new ContractTypeBAL().SelectAll(null);
        DataRow dRow = dt.NewRow();
        dRow["CTID"] = "0";
        dRow["ContractTypeName"] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow,0);
        ddlType.DataSource = dt;
        ddlType.DataBind();
        ddlType.SelectedIndex = 0;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddContracts.aspx");
    }

    protected void gvContracts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                ContractsBAL objBAL = new ContractsBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblErr.Text = hrmlang.GetString("contracttypedeleted");
                BindGrid();
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("contracttypedeleteerror");
            }
        }
    }

    protected void gvContracts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvContracts.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        ContractsBOL objBOL = new ContractsBOL();
        objBOL.Title = txtSearch.Text;
        objBOL.Description = txtSearch.Text;
        objBOL.ContractTypeID = Util.ToInt(ddlType.SelectedValue);
        gvContracts.DataSource = new ContractsBAL().SelectAll(objBOL);
        gvContracts.DataBind();
    }

    protected void gvContracts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("contracttype");
            e.Row.Cells[1].Text = hrmlang.GetString("title");
            e.Row.Cells[2].Text = hrmlang.GetString("description");
            e.Row.Cells[3].Text = hrmlang.GetString("document");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvContracts.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvContracts.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvContracts.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvContracts.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ("" + DataBinder.Eval(e.Row.DataItem, "DocName") != "")
            {
                HyperLink lnkDocName = (HyperLink)e.Row.FindControl("lnkDocName");
                lnkDocName.NavigateUrl = "~/images/contracts/" + DataBinder.Eval(e.Row.DataItem, "ContractId") + "/" + DataBinder.Eval(e.Row.DataItem, "DocName");
                lnkDocName.Text = "" + DataBinder.Eval(e.Row.DataItem, "DocName");
            }
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletedocument"));
        }
      
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}