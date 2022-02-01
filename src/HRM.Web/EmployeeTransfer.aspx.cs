using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class EmployeeTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetTransferList();
    }

    #region Functions

    private void GetTransferList()
    {
        EmpTransferBOL objBOL = new EmpTransferBOL();
        objBOL.ForwardTo = Util.ToInt(Session["EMPID"]);
        DataTable dT = new EmpTransferBAL().SelectAll(objBOL);
        objBOL = new EmpTransferBOL();
        objBOL.ReportTo = Util.ToInt(Session["EMPID"]);
        objBOL.Approve_Old_Branch = "Y";
        dT.Merge(new EmpTransferBAL().SelectAll(objBOL));
        dT.DefaultView.Sort = "TransferDate DESC";
        gvTransfer.DataSource = dT;
        gvTransfer.DataBind();
    }

    #endregion

    #region Grid Events

    protected void gvTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransfer.PageIndex = e.NewPageIndex;
        GetTransferList();
    }

    protected void gvTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("branch");
            e.Row.Cells[2].Text = hrmlang.GetString("department");
            e.Row.Cells[3].Text = hrmlang.GetString("subdepartment");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvTransfer.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvTransfer.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvTransfer.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvTransfer.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
            

            if (ViewState["permissions"] != null)
            {

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                
            }


            Label lblFwd = (Label)e.Row.FindControl("lblFwd");
            Label lblRpt = (Label)e.Row.FindControl("lblRpt");
            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");

            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            lnkApprove.Visible = false;
            lnkReject.Visible = false;
            
            if (lblFwd.Text == Session["EMPID"].ToString())
            {
                if ("" + drv["Approve_Old_Branch"] == "Y" || "" + drv["Approve_Old_Branch"] == "P")
                    lnkReject.Visible = true;
                if ("" + drv["Approve_Old_Branch"] == "P")
                    lnkApprove.Visible = true;
                if ("" + drv["Approve_New_Branch"] == "Y")
                {
                    lnkApprove.Visible = false;
                    lnkReject.Visible = false;
                    lnkEdit.Visible = false;
                    lnkDelete.Visible = false;
                }
            }
            else if (lblRpt.Text == Session["EMPID"].ToString())
            {
                if ("" + drv["Approve_New_Branch"] == "Y" || "" + drv["Approve_New_Branch"] == "P")
                    lnkReject.Visible = true;
                if ("" + drv["Approve_New_Branch"] == "P" || "" + drv["Approve_New_Branch"] == "")
                    lnkApprove.Visible = true;
            }
            lnkApprove.Attributes.Add("title", hrmlang.GetString("approveemployeetransfer"));
            lnkReject.Attributes.Add("title", hrmlang.GetString("rejectemployeetransfer"));
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }

    protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "APPROVE")
        {
            //  ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvTransfer').modal();", true);
            EmpTransferBOL objTr = new EmpTransferBOL();

            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            Label lblFwd = (Label)gRow.FindControl("lblFwd");
            Label lblRpt = (Label)gRow.FindControl("lblRpt");

            objTr.TransferID = Util.ToInt(e.CommandArgument);

            if (lblFwd.Text == Session["EMPID"].ToString())
            {
                objTr.Approve_Old_Branch = "Y";
                objTr.ApprovedBy = User.Identity.Name;
                new EmpTransferBAL().ApprovalRejectByCurrentBranch(objTr);
            }
            else if (lblRpt.Text == Session["EMPID"].ToString())
            {
                objTr.Approve_New_Branch = "Y";
                objTr.Approved_New_By = User.Identity.Name;
                new EmpTransferBAL().ApprovalRejectByNewBranch(objTr);
            }
            GetTransferList();
        }
        if (e.CommandName == "REJECT")
        {
            //  ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvTransfer').modal();", true);
            EmpTransferBOL objTr = new EmpTransferBOL();

            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            Label lblFwd = (Label)gRow.FindControl("lblFwd");
            Label lblRpt = (Label)gRow.FindControl("lblRpt");

            objTr.TransferID = Util.ToInt(e.CommandArgument);

            if (lblFwd.Text == Session["EMPID"].ToString())
            {
                objTr.Approve_Old_Branch = "N";
                objTr.ApprovedBy = User.Identity.Name;
                new EmpTransferBAL().ApprovalRejectByCurrentBranch(objTr);
            }
            else if (lblRpt.Text == Session["EMPID"].ToString())
            {
                objTr.Approve_New_Branch = "N";
                objTr.Approved_New_By = User.Identity.Name;
                new EmpTransferBAL().ApprovalRejectByNewBranch(objTr);
            }
            GetTransferList();
        }
        else if (e.CommandName.Equals("DEL"))
        {
            new EmpTransferBAL().Delete(Util.ToInt(e.CommandArgument));
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", string.Format("alert('{0}');", hrmlang.GetString("transferdatadeleted")), true);
            GetTransferList();
        }

    }
    #endregion
}