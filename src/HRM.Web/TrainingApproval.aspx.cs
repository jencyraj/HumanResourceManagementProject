using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class TrainingApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "trainingapproval.aspx");
        
            GetTraining();
            ddlApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("All"), ""));
            ddlApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("pending"), "P"));
            ddlApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("approved"), "Y"));
            ddlApprovalStatus.Items.Add(new ListItem(hrmlang.GetString("rejected"), "N"));
            SetData();
        }

        string[] permissions = (string[])ViewState["permissions"];
        
    }

    private void SetData()
    {
        lblTrainingApprovalStatus.Text = hrmlang.GetString("approvalstatus");
       
    }

    private void GetTraining()
    {
        TrainingBAL objBAL = new TrainingBAL();
        TrainingBOL objCy = new TrainingBOL();

        objCy.ApprovalStatus = ddlApprovalStatus.SelectedValue;
        
        

        gvTraining.DataSource = objBAL.SelectAllTrainingApprovalList(objCy);
        gvTraining.DataBind();

        if (gvTraining.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");
    }

  
    protected void gvTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Y"))
        {
            TrainingBAL objBAL = new TrainingBAL();
            objBAL.UpdateStatus(Util.ToInt(e.CommandArgument), User.Identity.Name,"Y");
            ClientScript.RegisterStartupScript(gvTraining.GetType(), "onclick", "alert('" + hrmlang.GetString("trainingapproved") + "');", true);
            GetTraining();
        }

        if (e.CommandName.Equals("N"))
        {
            TrainingBAL objBAL = new TrainingBAL();
            objBAL.UpdateStatus(Util.ToInt(e.CommandArgument), User.Identity.Name,"N");
            ClientScript.RegisterStartupScript(gvTraining.GetType(), "onclick", "alert('" + hrmlang.GetString("trainingrejected") + "');", true);
            GetTraining();
        }
        if (e.CommandName.Equals("EDITBR"))
        {
            Response.Redirect("AddTraining.aspx?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName.Equals("DEL"))
        {

            new TrainingBAL().Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            ClientScript.RegisterStartupScript(gvTraining.GetType(), "onclick", "alert('" + hrmlang.GetString("trainingdeleted") + "');", true);
            GetTraining();
        }
    }

    protected void gvTraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTraining.PageIndex = e.NewPageIndex;
        GetTraining();
    }

   
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetTraining();
    }
    protected void gvTraining_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("trainingtypes");
            e.Row.Cells[1].Text = hrmlang.GetString("title");
            e.Row.Cells[2].Text = hrmlang.GetString("trainer");
            e.Row.Cells[3].Text = hrmlang.GetString("location");
            e.Row.Cells[4].Text = hrmlang.GetString("date");
            e.Row.Cells[5].Text = hrmlang.GetString("status");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            Label lnkReject = (Label)e.Row.FindControl("lnkReject");

           
            string status = e.Row.Cells[5].Text;


            LinkButton lnkApproval = (LinkButton)e.Row.FindControl("lnkApproval");
            LinkButton lnkRejected = (LinkButton)e.Row.FindControl("lnkRejected");
            string[] permissions = (string[])ViewState["permissions"];
            if (e.Row.Cells[5].Text == "Pending")
            {
                lnkApproval.Visible = (permissions[1] == "Y") ? true : false;
                lnkRejected.Visible = (permissions[2] == "Y") ? true : false;
            }
            else if (e.Row.Cells[5].Text == "Approved")
            {
                lnkApproval.Visible = false; 
                lnkRejected.Visible = (permissions[2] == "Y") ? true : false;
            }
            else if (e.Row.Cells[5].Text == "Rejected")
            {
                lnkApproval.Visible =  (permissions[2] == "Y") ? true : false; 
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
            lnkRejected.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("rejecttraining"));
        }
    }
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTraining();
    }
}