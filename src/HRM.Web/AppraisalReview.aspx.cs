using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using HRM.BAL;
using HRM.BOL;

public partial class AppraisalReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRatings();
            BindAppraisalsForReview();
        }
    }

    private void BindAppraisalsForReview()
    {
        gvAppraisals.DataSource = new AppraisalReviewBAL().SelectAppraisalsForReview(Util.ToInt("" + Session["EMPID"]));
        gvAppraisals.DataBind();

        if (gvAppraisals.Rows.Count > 0)
        {
            gvAppraisals.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAppraisals.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAppraisals.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAppraisals.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        else
            lblMsg.Text = hrmlang.GetString("norecordsfound");

    }

    protected void gvAppraisals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("employee");
            e.Row.Cells[1].Text = hrmlang.GetString("appraisalperiod");
            e.Row.Cells[2].Text = hrmlang.GetString("sumittedon");
            e.Row.Cells[3].Text = hrmlang.GetString("reviewedon");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkReview");
            lnkReview.Attributes.Add("title", hrmlang.GetString("review"));

            LinkButton lnkSummary = (LinkButton)e.Row.FindControl("lnkSummary");
            lnkSummary.Attributes.Add("title", hrmlang.GetString("viewappraisalsummary"));
        }
    }
    protected void gvAppraisals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "REVIEW")
        {
            GridViewRow gRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblEmpID = (Label)gRow.FindControl("lblEmpID");
            Label lblEAppID = (Label)gRow.FindControl("lblEAppID");
            Label lblRID = (Label)gRow.FindControl("lblRID");

            lblEAID.Text = e.CommandArgument.ToString();
            lblAppPeriodID.Text = lblEAppID.Text;
            lblReviewID.Text = lblRID.Text;
            txtComments.Text = "";

            DataTable dTable = new EmpAppraisalBAL().SelectAll(Util.ToInt(lblAppPeriodID.Text), Util.ToInt(lblEmpID.Text)).Tables[1];
            ViewState["COMP"] = dTable;

            ViewState["REVIEW"] = new AppraisalReviewBAL().Select(Util.ToInt(lblReviewID.Text));

            DataView dView = dTable.DefaultView;
            rptrForm.DataSource = dView.ToTable(true, "CompetencyTypeID", "CompetencyType");
            rptrForm.DataBind();

            if (lblReviewID.Text == "")
                btnSubmit.Visible = false;
            else
                btnSubmit.Visible = true;

            if ("" + ViewState["submittedfrm"] == "1")
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
            }

            pNote.Visible = btnSave.Visible;

            ClientScript.RegisterStartupScript(gvAppraisals.GetType(), "onclick", "$('#dvForm').modal();", true);
        }

        if (e.CommandName == "REVIEWSUMMARY")
        {
            GridViewRow gRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblEmpID = (Label)gRow.FindControl("lblEmpID");
            Label lblEAppID = (Label)gRow.FindControl("lblEAppID");

            appraisaldocs.GenerateAppraisalSummary(lblEmpID.Text,lblEAppID.Text);
        }
    }

    

    protected void rptrForm_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblCTID = (Label)e.Item.FindControl("lblCTID");

            if (ViewState["COMP"] != null)
            {
                DataTable dTable = (DataTable)ViewState["COMP"];
                if (dTable != null)
                {
                    if (dTable.Rows.Count > 0)
                    {
                        GridView gvForm = (GridView)e.Item.FindControl("gvForm");

                        DataView dView = dTable.DefaultView;
                        dView.RowFilter = "CompetencyTypeID=" + lblCTID.Text;
                        gvForm.DataSource = dView.ToTable();
                        gvForm.DataBind();
                    }
                }
            }
        }
    }

    protected void gvForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("competency");
            e.Row.Cells[1].Text = hrmlang.GetString("rateyouself");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRating = (Label)e.Row.FindControl("lblRating");
            Label lblComments = (Label)e.Row.FindControl("lblComments");
            LinkButton lnkReviews = (LinkButton)e.Row.FindControl("lnkReviews");

            lnkReviews.Attributes.Add("title", hrmlang.GetString("viewotherreviews"));

            if (ViewState["COMP"] != null)
            {
                DataTable dTC = (DataTable)ViewState["COMP"];
                DataRow[] dRows = dTC.Select("competencyid=" + ((Label)e.Row.FindControl("lblCID")).Text);
                lblRating.Text = "" + dRows[0]["RatingID"];
                lblComments.Text = "" + dRows[0]["Comments"];
            }

            if (ViewState["REVIEW"] != null)
            {
                DataSet dSet = (DataSet)ViewState["REVIEW"];
                if (dSet != null)
                {
                    if (dSet.Tables.Count > 0)
                    {
                        DataTable dTable = dSet.Tables[0];
                        if (dTable.Rows.Count > 0)
                        {
                            txtComments.Text = "" + dTable.Rows[0]["Comments"];
                            if ("" + dTable.Rows[0]["Submitted"] == "Y")
                            {
                                ViewState["submittedfrm"] = "1";

                            }
                        }
                    }

                    if (dSet.Tables.Count > 1)
                    {
                        DataTable dT = dSet.Tables[1];
                        if (dT.Rows.Count > 0)
                        {
                            DataRow[] dRow = dT.Select("competencyid=" + ((Label)e.Row.FindControl("lblCID")).Text);
                            ((TextBox)e.Row.FindControl("txtWeightage")).Text = "" + dRow[0]["RatingID"];
                            ((TextBox)e.Row.FindControl("txtComments")).Text = "" + dRow[0]["Comments"];
                        }
                    }
                }
            }
        }
    }

    protected void gvForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "OTHERREVIEWS")
        {
            DataSet dSet = new AppraisalReviewBAL().SelectReviewsByCompetencyID(Util.ToInt(lblEAID.Text), Util.ToInt(e.CommandArgument));
            DataView dView = new DataView(dSet.Tables[0]);
            dView.RowFilter = "ReviewerID <> " + Session["EMPID"];

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txtAllReviews = (TextBox)row.FindControl("txtAllReviews");

            DataTable dT = dView.ToTable();
            if (dT.Rows.Count > 0)
            {
                txtAllReviews.Text = "";
                foreach (DataRow dR in dT.Rows)
                {
                    txtAllReviews.Text += hrmlang.GetString("reviewer") + " : " + dR["FullName"] + "\nRating : " + dR["RatingID"] + "\n";
                    txtAllReviews.Text += "Comments : " + dR["Comments"] + "\n";
                }
            }
            else
                txtAllReviews.Text = hrmlang.GetString("nootherreviews");
            txtAllReviews.Visible = true;
            txtAllReviews.ReadOnly = true;
            txtAllReviews.Focus();
            ClientScript.RegisterStartupScript(gvAppraisals.GetType(), "onclick", "$('#dvForm').modal();", true);
        }
    }
    private void GetRatings()
    {
        RatingBAL objBAL = new RatingBAL();
        ViewState["rating"] = objBAL.SelectAll(0);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AppraisalReviewBOL objBOL = new AppraisalReviewBOL();
            AppraisalReviewBAL objBAL = new AppraisalReviewBAL();

            objBOL.ReviewerID = Util.ToInt("" + Session["EMPID"]);
            objBOL.EAID = Util.ToInt(lblEAID.Text);
            objBOL.Comments = txtComments.Text.Trim();
            objBOL.CreatedBy = User.Identity.Name;
            objBOL.ReviewID = objBAL.SaveMaster(objBOL);

            foreach (RepeaterItem rItem in rptrForm.Items)
            {
                GridView gvForm = (GridView)rItem.FindControl("gvForm");
                for (int i = 0; i < gvForm.Rows.Count; i++)
                {
                    GridViewRow gRow = gvForm.Rows[i];
                    Label lblCID = (Label)gRow.FindControl("lblCID");

                    objBOL.CompetencyID = Util.ToInt(lblCID.Text);
                    objBOL.RatingID = Util.ToDecimal(((TextBox)gRow.FindControl("txtWeightage")).Text.Trim());
                    objBOL.Comments = ((TextBox)gRow.FindControl("txtComments")).Text.Trim();
                    objBAL.SaveDetails(objBOL);
                }
            }

            txtComments.Text = "";
            lblMsg.Text = hrmlang.GetString("datasaved");
            BindAppraisalsForReview();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "alert('" + ex.Message + "');", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AppraisalReviewBOL objBOL = new AppraisalReviewBOL();
            AppraisalReviewBAL objBAL = new AppraisalReviewBAL();

            objBOL.ReviewID = Util.ToInt(lblReviewID.Text);
            objBOL.Comments = txtComments.Text.Trim();
            objBOL.CreatedBy = User.Identity.Name;
            objBAL.SubmitReview(objBOL);
            lblMsg.Text = hrmlang.GetString("datasubmitted");
            BindAppraisalsForReview();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "alert('" + ex.Message + "');", true);
        }
    }
}