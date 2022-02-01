using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AttendanceRegularizationRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("newregrequest");
        btnReject.Text = hrmlang.GetString("reject");
        btnCancel.Text = hrmlang.GetString("cancel");
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "attendanceregularizationrequest.aspx");
            ddlFilter.Items.Add(new ListItem(hrmlang.GetString("all"), ""));
            ddlFilter.Items.Add(new ListItem(hrmlang.GetString("open"), "Y"));
            ddlFilter.Items.Add(new ListItem(hrmlang.GetString("closed"), "N"));
            GetRequests();
        }

        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "attendanceregularizationrequest.aspx");

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    public void GetRequests()
    {
        DataTable dTable = new AttRegularBAL().Select(Util.ToInt(Session["EMPID"]));
        DataView dView = dTable.DefaultView;

        if (ddlFilter.SelectedValue == "Y") //open requests
            dView.RowFilter = "RequestClosed='' OR RequestClosed ='N'";
        else if (ddlFilter.SelectedValue == "N") //closed requests
            dView.RowFilter = "RequestClosed='Y'";

        gvRequests.DataSource = dView.ToTable();
        gvRequests.DataBind();
    }



    protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("month");
            e.Row.Cells[2].Text = hrmlang.GetString("year");
            e.Row.Cells[3].Text = hrmlang.GetString("comments");
            e.Row.Cells[4].Text = hrmlang.GetString("requestdate");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvRequests.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvRequests.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvRequests.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvRequests.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");

            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            HyperLink lnkApprove = (HyperLink)e.Row.FindControl("lnkApprove");
            HyperLink lnkReject = (HyperLink)e.Row.FindControl("lnkReject");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            lnkApprove.Attributes.Add("title", hrmlang.GetString("approverequest"));
            lnkReject.Attributes.Add("title", hrmlang.GetString("rejectrequest"));
        }
    }

    protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                new AttRegularBAL().Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
                lblMsg.Text = hrmlang.GetString("datadeleted");
                GetRequests();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
        else if (e.CommandName == "APPROVE")
        {
            new AttRegularBAL().Approve(Util.ToInt(e.CommandArgument), User.Identity.Name, "Y", "");
            lblMsg.Text = hrmlang.GetString("attregapprovalsuccess");
            GetRequests();
        }
        else if (e.CommandName == "REJECT")
        {
            lblReqID.Text = e.CommandArgument.ToString();
            dvReject.Visible = true;
        }
    }

    protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRequests.PageIndex = e.NewPageIndex;
        GetRequests();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AttendanceRegularize.aspx");
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        new AttRegularBAL().Approve(Util.ToInt(lblReqID.Text), User.Identity.Name, "N", "");
        lblMsg.Text = hrmlang.GetString("attregrejectsuccess");
        dvReject.Visible = false;
        lblReqID.Text = "";
        GetRequests();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvReject.Visible = false;
        lblReqID.Text = "";
    }
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRequests();
    }
}