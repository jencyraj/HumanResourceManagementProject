using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using HRM.BAL;
using HRM.BOL;

public partial class InBox : System.Web.UI.Page
{
    protected string sClass = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            sClass = ("" + Session["STYLESHEET"] == "LR") ? "f-right" : "f-left";
            GetMessages();
        }
    }


    private void GetMessages()
    {
        MessagesBOL objBOL = new MessagesBOL();
        objBOL.SentTo = "" + Session["EMPID"];
        DataTable dTable = new MessagesBAL().SelectAll(objBOL);
        if (dTable.Rows.Count > 0)
        {
            DataView dView = dTable.DefaultView;
            dView.Sort = "SentDate DESC";
            rptrMsg.DataSource = dView;
            rptrMsg.DataBind();
        }
    }
    protected void rptrMsg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HtmlContainerControl dvcollapse = (HtmlContainerControl)e.Row.FindControl("collapse1");
            HtmlAnchor lnkType = (HtmlAnchor)e.Row.FindControl("lnkType");

            if (e.Row.RowIndex == 0)
            {
                dvcollapse.Attributes.Add("class", "panel-collapse collapse in");
            }

            lnkType.HRef = "#" + dvcollapse.ClientID;
            
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }
    protected void rptrMsg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptrMsg.PageIndex = e.NewPageIndex;
        GetMessages();
    }
    protected void rptrMsg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            try
            {
                new MessagesBAL().Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("datadeleted");
                lblMsg.ForeColor = System.Drawing.Color.Green ;
                GetMessages();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}