using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class TrainingEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "trainingevents.aspx");
        
            GetEmployeeApprovedTraining();
          
        }
        btnEvaluate.Text = hrmlang.GetString("submit");
        string[] permissions = (string[])ViewState["permissions"];
        
    }



    private void GetEmployeeApprovedTraining()
    {
        TrainingBAL objBAL = new TrainingBAL();
        TrainingBOL objCy = new TrainingBOL();

        objCy.Employee = Util.ToInt(Session["EMPID"]).ToString();
        
        

        gvTraining.DataSource = objBAL.SelectAllEmployeeTrainingEventList(objCy);
        gvTraining.DataBind();

        if (gvTraining.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");
    }

    protected void btnEvaluate_Click(object sender, EventArgs e)
    {

        try
        {

            TrainingEvaluationBAL objBAL = new TrainingEvaluationBAL();
            TrainingEvaluationBOL objCy = new TrainingEvaluationBOL();

            objCy.Evaluateid = Util.ToInt(hdnevaluateid.Value);
            objCy.TrainingId = Util.ToInt(hdntrainingId.Value);
           
            objCy.Description =txtComment.Text.Replace("'","''").Trim();
            objCy.rating = Util.ToInt(txtratingvalue.Value);

            objCy.employeeid = Util.ToInt(Session["EMPID"]);

            
            objBAL.InsertTrainingEvaluation(objCy);

            //lblMsg.Text = hrmlang.GetString("trainingevaluationsaved");
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvevaluate').modal('hide');", true);
         //   Response.Redirect("Training.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void gvTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            int trainingId = Util.ToInt(e.CommandArgument);
            TrainingEvaluationBAL objBAL = new TrainingEvaluationBAL();
            TrainingEvaluationBOL objCy = new TrainingEvaluationBOL();
            int Employee = Util.ToInt(Session["EMPID"]);
            string Eval_id = "";
            objCy = objBAL.SelectTrainingEvaluationByID(trainingId, Employee);
            try
            {
                Eval_id = objCy.Evaluateid.ToString();
            }
            catch { }
            objBAL.Delete(Employee.ToString(), Util.ToInt(Eval_id));
            ClientScript.RegisterStartupScript(gvTraining.GetType(), "onclick", "alert('" + hrmlang.GetString("deleted") + "');", true);
                      
            
        }

        if (e.CommandName.Equals("Evaluate"))
        {
            
            int trainingId = Util.ToInt(e.CommandArgument);
            hdntrainingId.Value = trainingId.ToString();
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            txttrainingtype.Text = Util.CleanString(gRow.Cells[0].Text);
            txtTitle.Text = Util.CleanString(gRow.Cells[1].Text);
            txttrainer.Text = Util.CleanString(gRow.Cells[2].Text);
            txtlocation.Text = Util.CleanString(gRow.Cells[3].Text);
            txtdate.Text = Util.CleanString(gRow.Cells[4].Text);
            TrainingEvaluationBAL objBAL = new TrainingEvaluationBAL();
            TrainingEvaluationBOL objCy = new TrainingEvaluationBOL();
            int ratingval = 0;
            int Employee = Util.ToInt(Session["EMPID"]);
            objCy = objBAL.SelectTrainingEvaluationByID(trainingId,Employee);
            try
            {

                txtComment.Text = objCy.Description;
                ratingval = objCy.rating;
                hdnevaluateid.Value = objCy.Evaluateid.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#rating_simple1').webwidget_rating_simple({rating_star_length: '5',rating_initial_value: '" + ratingval + "',rating_function_name: 'getratingvalue', directory: 'rating'}); $('#dvevaluate').modal();", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#rating_simple1').webwidget_rating_simple({rating_star_length: '5',rating_initial_value: '0',rating_function_name: 'getratingvalue', directory: 'rating'}); $('#dvevaluate').modal();", true);
            }

         
        }

      
    }

    protected void gvTraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTraining.PageIndex = e.NewPageIndex;
        GetEmployeeApprovedTraining();
    }

   
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetEmployeeApprovedTraining();
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
          
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEvaluate = (LinkButton)e.Row.FindControl("lnkEvaluate");
            lnkEvaluate.Attributes.Add("title", hrmlang.GetString("evaluate"));
            if (ViewState["permissions"] != null)
            {

                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];


                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;

                lnkDelete.Attributes.Add("title", hrmlang.GetString("deletecomment"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletetraining"));
            }
        }
    }
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmployeeApprovedTraining();
    }
}