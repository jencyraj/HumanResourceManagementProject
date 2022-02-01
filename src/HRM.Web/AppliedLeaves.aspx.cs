using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AppliedLeaves : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLeaves();
        }
    }

    private void BindLeaves()
    {
        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();

        objBOL.EmployeeID = Util.ToInt(Session["EMPID"]);
        DataTable dT = objBAL.SelectAll(objBOL);

        gvLeave.DataSource = dT;
        gvLeave.DataBind();
    }

    protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeave.PageIndex = e.NewPageIndex;
        BindLeaves();
    }
    protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            LeaveBAL objBAL = new LeaveBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument.ToString()), User.Identity.Name);
            BindLeaves();
            lblMsg.Text = hrmlang.GetString("leavedeleted");
        }
    }
    protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("applieddate");
            e.Row.Cells[1].Text = hrmlang.GetString("fromdate");
            e.Row.Cells[2].Text = hrmlang.GetString("todate");
            e.Row.Cells[3].Text = hrmlang.GetString("noofdays");
            e.Row.Cells[4].Text = hrmlang.GetString("reason");
            e.Row.Cells[5].Text = hrmlang.GetString("status");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvLeave.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvLeave.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvLeave.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvLeave.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status=DataBinder.Eval(e.Row.DataItem,"ApprovalStatus").ToString();

            ((Label)e.Row.FindControl("lblStatus")).Text = (status == "P") ? "PENDING" : ((status == "Y") ? "APPROVED" : "REJECTED");
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteleave"));
        }
    }
}