using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class TrainingEvaluation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "trainingevaluation.aspx");
        
            GetEmployeeTrainingEvaluation();
          
        }
      
        
    }



    private void GetEmployeeTrainingEvaluation()
    {
        TrainingEvaluationBAL objBAL = new TrainingEvaluationBAL();
        TrainingEvaluationBOL objCy = new TrainingEvaluationBOL();



        objCy.employeeid = Util.ToInt(Session["EMPID"]);
        objCy.TrainingId = Util.ToInt(Request.QueryString["id"]);
        gvTraining.DataSource = objBAL.SelectAllEmployeeTrainingEvaluationList(objCy);
        gvTraining.DataBind();

        if (gvTraining.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");
    }

   
   

    protected void gvTraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTraining.PageIndex = e.NewPageIndex;
        GetEmployeeTrainingEvaluation();
    }

    protected void gvTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            TrainingEvaluationBAL objBAL = new TrainingEvaluationBAL();
            objBAL.Delete(Util.ToInt(Session["EMPID"]).ToString(),Util.ToInt(e.CommandArgument));
            ClientScript.RegisterStartupScript(gvTraining.GetType(), "onclick", "alert('" + hrmlang.GetString("trainingdeleted") + "');", true);
            GetEmployeeTrainingEvaluation();
        }

    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetEmployeeTrainingEvaluation();
    }
    protected void gvTraining_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("title");
            e.Row.Cells[1].Text = hrmlang.GetString("name");
            e.Row.Cells[2].Text = hrmlang.GetString("date");
            e.Row.Cells[3].Text = hrmlang.GetString("description");
          
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvTraining.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvTraining.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvTraining.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvTraining.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (ViewState["permissions"] != null)
            {
               
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

               
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletetraining"));
            }
        }
    }
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmployeeTrainingEvaluation();
    }
}