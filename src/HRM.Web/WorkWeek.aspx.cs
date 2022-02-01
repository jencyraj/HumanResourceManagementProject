using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;
public partial class WorkWeek : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("addnew");
        if (!IsPostBack)
        {
            BindGrid();

        }
    }

    public string GetOffDays(string su, string mo, string tu, string we, string th, string fr, string sat)
    {
        string sOffDays = "";
        if (su == "N")
            sOffDays = "Sunday";        
        if (mo == "N")
            sOffDays += (sOffDays == "") ? "Monday" : "," + "Monday";
         if (tu == "N")
            sOffDays += (sOffDays == "") ? "Tuesday" : "," + "Tuesday";
         if (we == "N")
            sOffDays += (sOffDays == "") ? "Wednesday" : "," + "Wednesday";
         if (th == "N")
            sOffDays += (sOffDays == "") ? "Thursday" : "," + "Thursday";
         if (fr == "N")
            sOffDays += (sOffDays == "") ? "Friday" : "," + "Friday";
         if (sat == "N")
            sOffDays += (sOffDays == "") ? "Saturday" : "," + "Saturday";
       

        return sOffDays;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        Response.Redirect("AddWorkWeek.aspx");
    }

    protected void gvPayrollTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("branch");
            e.Row.Cells[1].Text = hrmlang.GetString("department");
            e.Row.Cells[2].Text = hrmlang.GetString("designation");
            e.Row.Cells[3].Text = hrmlang.GetString("employee");
            e.Row.Cells[4].Text = hrmlang.GetString("offday");


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
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("cnfrmdltworkweek"));
        }
    }
    protected void gvPayrollTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                WorkWeekBAL objBAL = new WorkWeekBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblMsg.Text = hrmlang.GetString("workwkdlt");
                lblErr.Text = string.Empty;
                BindGrid();
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("workwkdlterr");
            }
        }
        else if (e.CommandName.Equals("Edit"))
        {

            int id = Convert.ToInt32(e.CommandArgument.ToString());

               
             
           
        }
    }

    protected void gvPayrollTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayrollTemplates.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        gvPayrollTemplates.DataSource = new WorkWeekBAL().Workweek_select();
        gvPayrollTemplates.DataBind();
    }
}