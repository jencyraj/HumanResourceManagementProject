using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AppraisalCompetencyTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompetencyTypes();
            GetTemplate();

            btnSave.Text = hrmlang.GetString("save");
        }
    }

    private void GetCompetencyTypes()
    {
        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        ddlType.DataSource = Util.ReturnDT("CompetencyTypeID", "CompetencyType", objBAL.SelectAllCompetencyTypes(0));
        ddlType.DataBind();
    }

    private void GetTemplate()
    {
        DataTable dTable = new AppraisalTemplateBAL().GetAppraisalTemplate(Util.ToInt(Request.QueryString["appperiodid"]));
        DataView dView = new DataView(dTable);
        if (Util.ToInt(ddlType.SelectedValue) > 0)
        {
            dView.RowFilter = "CompetencyTypeID=" + ddlType.SelectedValue;
        }
        gvAppraisal.DataSource = dView.ToTable();
        gvAppraisal.DataBind();
    }

    protected void gvAppraisal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("competency")  + "/" + hrmlang.GetString("weightage") ;
            e.Row.Cells[1].Text = hrmlang.GetString("evaldesc1");
            e.Row.Cells[2].Text = hrmlang.GetString("evaldesc2");
            e.Row.Cells[3].Text = hrmlang.GetString("evaldesc3");
            e.Row.Cells[4].Text = hrmlang.GetString("evaldesc4");
            e.Row.Cells[5].Text = hrmlang.GetString("evaldesc5");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in gvAppraisal.Rows)
        {
            AppraisalCompetencyBOL objBOL = new AppraisalCompetencyBOL();

            objBOL.AppraisalPeriodID = Util.ToInt(Request.QueryString["appperiodid"]);
            objBOL.CompetencyID = Util.ToInt(((Label)gRow.FindControl("lblCompetencyID")).Text);
            objBOL.CompetencyTypeID = Util.ToInt(((Label)gRow.FindControl("lblCompetencyTypeID")).Text);
            objBOL.CompetencyName = ((Label)gRow.FindControl("lblDesc")).Text;
            objBOL.RatingDesc1 = ((TextBox)gRow.FindControl("txtRating1")).Text;
            objBOL.RatingDesc2 = ((TextBox)gRow.FindControl("txtRating2")).Text;
            objBOL.RatingDesc3 = ((TextBox)gRow.FindControl("txtRating3")).Text;
            objBOL.RatingDesc4 = ((TextBox)gRow.FindControl("txtRating4")).Text;
            objBOL.RatingDesc5 = ((TextBox)gRow.FindControl("txtRating5")).Text;
            objBOL.Weightage = Util.ToInt(((TextBox)gRow.FindControl("txtWeightage")).Text);
            objBOL.CreatedBy = User.Identity.Name;
            new AppraisalTemplateBAL().Save(objBOL);

            ClientScript.RegisterStartupScript(btnSave.GetType(), "onclick", "alert('" + hrmlang.GetString("datasaved") + "')", true);
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTemplate();
    }
}