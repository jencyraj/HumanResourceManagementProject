using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Memo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "training.aspx");
            //GetRoles();
           // GetTrainingTypes();
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("All"), ""));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("pending"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "R"));
            GetMemo();

           // SetData();
        }

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }

 
   

  

    private void GetMemo()
    {
        lblErr.Text = "";
        MemoBAL objBAL = new MemoBAL();
        MemoBOL objCy = new MemoBOL();

      //  objCy.trainingtype = Util.ToInt(ddlStype.SelectedValue);
        if (ddlStatus.SelectedIndex > 0)
        {
            objCy.Status = ddlStatus.SelectedValue;
        }
        objCy.Subject = txtsub.Text;
        objCy.Description = txtSearch.Text;
        gvMemo.DataSource = objBAL.SelectAll(objCy);
        gvMemo.DataBind();
        if (gvMemo.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");// "No competencies found";
    }

  
    protected void gvMemo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Y"))
        {
            MemoBAL objBAL = new MemoBAL();
            objBAL.ChangeStatus(Util.ToInt(e.CommandArgument),"Y", Util.ToInt(Session["EMPID"]));
            ClientScript.RegisterStartupScript(gvMemo.GetType(), "onclick", "alert('" + hrmlang.GetString("memoapproved") + "');", true);
            GetMemo();
        }

        if (e.CommandName.Equals("R"))
        {
            MemoBAL objBAL = new MemoBAL();
            objBAL.ChangeStatus(Util.ToInt(e.CommandArgument), "R", Util.ToInt(Session["EMPID"]));
            ClientScript.RegisterStartupScript(gvMemo.GetType(), "onclick", "alert('" + hrmlang.GetString("memorejected") + "');", true);
            GetMemo();
        }
        if (e.CommandName.Equals("EDITBR"))
        {
            Response.Redirect("AddMemo.aspx?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName.Equals("DEL"))
        {

            new MemoBAL().Delete(Util.ToInt(e.CommandArgument));
            ClientScript.RegisterStartupScript(gvMemo.GetType(), "onclick", "alert('" + hrmlang.GetString("memodeleted") + "');", true);
            GetMemo();
        }
    }

    protected void gvMemo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMemo.PageIndex = e.NewPageIndex;
        GetMemo();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddMemo.aspx");
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetMemo();
    }
    protected void gvMemo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          //  e.Row.Cells[0].Text = hrmlang.GetString("memoto");
            e.Row.Cells[0].Text = hrmlang.GetString("memofrom");
            e.Row.Cells[1].Text = hrmlang.GetString("memodate");
            e.Row.Cells[2].Text = hrmlang.GetString("subject");
            e.Row.Cells[3].Text = hrmlang.GetString("description");
            e.Row.Cells[4].Text = hrmlang.GetString("status");
           
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            Label lnkReject = (Label)e.Row.FindControl("lnkReject");
            string[] permissions = (string[])ViewState["permissions"];
            LinkButton lnkApproval = (LinkButton)e.Row.FindControl("lnkApproval");
            LinkButton lnkRejected = (LinkButton)e.Row.FindControl("lnkRejected");
            string status = e.Row.Cells[4].Text;
            lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
            lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
            if (e.Row.Cells[4].Text == "Pending")
            {
                lnkApproval.Visible = (permissions[1] == "Y") ? true : false;
                lnkRejected.Visible = (permissions[2] == "Y") ? true : false;
            }
            else if (e.Row.Cells[4].Text == "Approved")
            {
                lnkApproval.Visible = false;
                lnkRejected.Visible = (permissions[2] == "Y") ? true : false;
            }
            else if (e.Row.Cells[4].Text == "Rejected")
            {
                lnkApproval.Visible = (permissions[2] == "Y") ? true : false;
                lnkRejected.Visible = false;
            }
            if (status == "Pending")
            {
                lnkRejected.Visible = true;
                lnkApproval.Visible = true;
            }
            else if (status == "Approved")
            {
                lnkRejected.Visible = true;
                lnkApproval.Visible = false;
            }
            else if (status == "Rejected")
            {
                lnkRejected.Visible = false;
                lnkApproval.Visible = true;
            }

            lnkApproval.Attributes.Add("title", hrmlang.GetString("approve"));
            lnkRejected.Attributes.Add("title", hrmlang.GetString("reject"));
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            lnkRejected.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("rejecttraining"));
        }
    }
}