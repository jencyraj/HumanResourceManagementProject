using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Trainings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "training.aspx");
            //GetRoles();
            GetTrainingTypes();
            GetTraining();

            SetData();
        }

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void SetData()
    {
        lblTrainingType.Text = hrmlang.GetString("trainingtypes");
        lblDesc.Text = hrmlang.GetString("description");       
        lnkSearch.Text = hrmlang.GetString("go");
        btnNew.Text = hrmlang.GetString("addnew");
        txtSearch.Attributes.Add("placeholder", hrmlang.GetString("searchtext"));
    }

    private void GetTrainingTypes()
    {
        TrainingTypeBAL objBAL = new TrainingTypeBAL();
        ddlStype.DataSource = objBAL.SelectAll(0);
        ddlStype.DataBind();
        ddlStype.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

  

    private void GetTraining()
    {
        TrainingBAL objBAL = new TrainingBAL();
        TrainingBOL objCy = new TrainingBOL();

        objCy.trainingtype = Util.ToInt(ddlStype.SelectedValue);
        objCy.title = txtSearch.Text;
       
        gvTraining.DataSource = objBAL.SelectAll(objCy);
        gvTraining.DataBind();

        if (gvTraining.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");// "No competencies found";
    }

  
    protected void gvTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTraining.aspx");
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
            e.Row.Cells[4].Text = hrmlang.GetString("startdate");
            e.Row.Cells[5].Text = hrmlang.GetString("enddate");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

            string[] permissions = (string[])ViewState["permissions"];

            lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
            lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }
}