using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Rating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "rating.aspx");
            GetRatings();
        }

        btnNew.Text = hrmlang.GetString("addnew");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        txtRating.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtScore.Attributes.Add("placeholder", hrmlang.GetString("maxscore"));

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetRatings()
    {
        RatingBAL objBAL = new RatingBAL();
        gvRating.DataSource = objBAL.SelectAll(0);
        gvRating.DataBind();

    }

    protected void gvRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblRatingID.Text = e.CommandArgument.ToString();
            txtRating.Text = Util.CleanString(row.Cells[0].Text);
            txtScore.Text = Util.CleanString(row.Cells[1].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            RatingBAL objBAL = new RatingBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
          
            lblMsg.Text = hrmlang.GetString("ratingdeleted");
            GetRatings();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            RatingBAL objBAL = new RatingBAL();
            RatingBOL objBol = new RatingBOL();

            objBol.RatingID = Util.ToInt(lblRatingID.Text);
            objBol.RatingDesc = txtRating.Text.Trim();
            objBol.MaxScore = Util.ToInt(txtScore.Text.Trim());
            objBol.CreatedBy = User.Identity.Name;
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("ratingsaved");
           // lblMsg.Text = "Rating saved successfully";
            Clear();
            GetRatings();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblRatingID.Text = "";
        txtRating.Text = "";
        txtScore.Text = "";
    }
    protected void gvRating_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRating.PageIndex = e.NewPageIndex;
        GetRatings();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("rating");
            e.Row.Cells[1].Text = hrmlang.GetString("maxscore");
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