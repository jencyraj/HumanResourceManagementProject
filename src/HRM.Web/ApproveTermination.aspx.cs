using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveTermination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "ApproveTermination.aspx");
            GetTermination();

        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetTermination()
    {
        TerminationBAL objBAL = new TerminationBAL();
        TerminationBOL objBOL = new TerminationBOL();

        objBOL.ForwardedTo = Util.ToInt(Session["EMPID"]);

        gvTermination.DataSource = objBAL.SelectAll(objBOL);
        gvTermination.DataBind();
    }

    protected void gvTermination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("APPROVE"))
        {
            pnlApprove.Visible = true;
            pnlButtons.Visible = true;
            pnlDeny.Visible = false;
            lblTID.Text = e.CommandArgument.ToString();
        }
        if (e.CommandName.Equals("DENY"))
        {
            pnlDeny.Visible = true;
            pnlButtons.Visible = true;
            pnlApprove.Visible = false;
            lblTID.Text = e.CommandArgument.ToString();
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
                string[] permissions = (string[])ViewState["permissions"];

                string approvestatus = DataBinder.Eval(e.Row.DataItem, "Approved").ToString();

                LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
                LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");

                lnkApprove.Attributes.Add("title", hrmlang.GetString("approve"));
                lnkReject.Attributes.Add("title", hrmlang.GetString("deny"));

                if (approvestatus == "P")
                {
                    lnkApprove.Visible = true;
                    lnkReject.Visible = true;
                }
                else
                {
                    lnkApprove.Visible = false;
                    lnkReject.Visible = false;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        try
        {
            TerminationBAL objBAL = new TerminationBAL();
            TerminationBOL objBol = new TerminationBOL();

            if (!String.IsNullOrEmpty(lblTID.Text))
                objBol.TID = Convert.ToInt32(lblTID.Text);

            objBol.ApprovedBy = Util.ToInt(Session["EMPID"]);

            if (rbtnIntrv.SelectedValue == "Y")
            {
                objBol.IsExitInterview = true;

                if (hfEmployeeId.Value != "")
                    objBol.InterviewerId = Util.ToInt(hfEmployeeId.Value);

                if (ctlInterviewDate.getGregorianDateText != "")
                    objBol.InterviewDate = Util.RearrangeDateTime(ctlInterviewDate.getGregorianDateText);
            }
            else if (rbtnIntrv.SelectedValue == "N")
            {
                objBol.IsExitInterview = false;
            }

            if(pnlApprove.Visible == true)
            {
                objBol.Approved = "Y";
                objBol.ApprovalReason = txtApprReason.Text;
            }
            else if (pnlDeny.Visible == true)
            {
                objBol.Approved = "N";
                objBol.ApprovalReason = txtDnyReason.Text;
            }

            objBAL.Approve(objBol);
            lblMsg.Text = hrmlang.GetString("datasaved");

            Response.Redirect("~/ApproveTermination.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
    }
    protected void rbtnIntrv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rbtnIntrv.SelectedValue=="Y")
        {
            pnlIntrv.Visible = true;
        }
        else
        {
            pnlIntrv.Visible = false;
        }
    }
}