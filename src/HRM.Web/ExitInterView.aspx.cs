using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class ExitInterView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetExitInterviews();
        }
    }

    private void GetExitInterviews()
    {
        DataSet dSet = new ExitInterviewBAL().GetInterviews(Util.ToInt(Session["EMPID"]));
        gvResignation.DataSource = dSet.Tables[0];
        gvResignation.DataBind();

        gvTermination.DataSource = dSet.Tables[1];
        gvTermination.DataBind();
    }

    protected void gvResignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

        }
    }     

    protected void gvResignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("EmployeeName");
            e.Row.Cells[1].Text = hrmlang.GetString("NoticeDate");
            e.Row.Cells[2].Text = hrmlang.GetString("ResgnDate");
            e.Row.Cells[3].Text = hrmlang.GetString("reason");
            e.Row.Cells[4].Text = hrmlang.GetString("interviewdate");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((HyperLink)e.Row.FindControl("lnkInterview")).Text = hrmlang.GetString("interviewresult");
        }
    }


    protected void gvTermination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            
        }
    }

    protected void gvTermination_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employeename");
            e.Row.Cells[1].Text = hrmlang.GetString("forwardedto");
            e.Row.Cells[2].Text = hrmlang.GetString("requestdate");
            e.Row.Cells[3].Text = hrmlang.GetString("interviewdate");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((HyperLink)e.Row.FindControl("lnkInterview")).Text = hrmlang.GetString("interviewresult");
        }
    }

}