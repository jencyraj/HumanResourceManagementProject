using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Termination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "Termination.aspx");
            GetTermination();

        }


        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

        btnNew.Text = hrmlang.GetString("newtermination");
    }

    private void GetTermination()
    {
        TerminationBAL objBAL = new TerminationBAL();
        TerminationBOL objBOL = new TerminationBOL();

        objBOL.CreatedBy = Util.ToInt(Session["EMPID"]);

        gvTermination.DataSource = objBAL.SelectAll(objBOL);
        gvTermination.DataBind();
    }

    protected void gvTermination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            TerminationBAL objBAL = new TerminationBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("datadeleted");
            GetTermination();
        }
    }

    protected void gvTermination_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTermination.PageIndex = e.NewPageIndex;
        GetTermination();
    }

    protected void gvTermination_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employeename");
            e.Row.Cells[1].Text = hrmlang.GetString("forwardedto");
            e.Row.Cells[2].Text = hrmlang.GetString("requestdate");
            e.Row.Cells[3].Text = hrmlang.GetString("approvedstatus");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvTermination.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvTermination.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvTermination.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvTermination.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            }

            //string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
            //decodedText = decodedText.Replace("<br>", "");
            //decodedText = (decodedText.Length > 30) ? decodedText.Substring(0, 30) + "..." : decodedText;
            //e.Row.Cells[2].Text = decodedText;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddTermination.aspx");
    }
}