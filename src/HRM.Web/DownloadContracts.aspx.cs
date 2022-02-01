using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using HRM.BAL;

public partial class DownloadContracts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dTable = new ContractsBAL().SelectAll(null);
            ViewState["DTABLE"] = dTable;

            DataView view = new DataView(dTable);
            DataTable distinctValues = view.ToTable(true, "ContractTypeID", "ContractTypeName");
            rptrDownloads.DataSource = distinctValues;
            rptrDownloads.DataBind();

            if (dTable.Rows.Count == 0)
                lblMsg.Text = "No documents found";
        }
    }


    protected void rptrDownloads_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvDownloads = (GridView)e.Item.FindControl("gvDownloads");

            DataView view = new DataView((DataTable)ViewState["DTABLE"]);
            view.RowFilter = "ContractTypeID=" + DataBinder.Eval(e.Item.DataItem, "ContractTypeID");
            DataTable dTable = view.ToTable();
            gvDownloads.DataSource = dTable;
            gvDownloads.DataBind();

            HtmlContainerControl dvcollapse = (HtmlContainerControl)e.Item.FindControl("collapse1");
            HtmlAnchor lnkType = (HtmlAnchor)e.Item.FindControl("lnkType");

            if (e.Item.ItemIndex == 0)
            {
                dvcollapse.Attributes.Add("class", "panel-collapse collapse in");
            }

            lnkType.HRef = "#" + dvcollapse.ClientID;
        }
    }

    protected void gvDownloads_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTitle = (Label)e.Row.FindControl("lblTitle");
            lblTitle.Text = "" + DataBinder.Eval(e.Row.DataItem, "Title");

            if ("" + DataBinder.Eval(e.Row.DataItem, "DocName") != "")
            {
                HyperLink lnkDocName = (HyperLink)e.Row.FindControl("lnkDocName");
                lnkDocName.NavigateUrl = "~/images/contracts/" + DataBinder.Eval(e.Row.DataItem, "ContractId") + "/" + DataBinder.Eval(e.Row.DataItem, "DocName");
            //    lnkDocName.Text = "" + DataBinder.Eval(e.Row.DataItem, "DocName");

                lnkDocName.Text = hrmlang.GetString("clickhere"); ;
            }
        }
    }
}