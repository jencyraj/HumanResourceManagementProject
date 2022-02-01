using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;
using HRM.BOL;

public partial class AppraisalForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "appraisalform.aspx");
            GetAppraisalPeriods();
            GetRatings();
            ViewState["submittedfrm"] = "";

            btnSave.Text = hrmlang.GetString("save");
            btnSubmit.Text = hrmlang.GetString("submit");

        }
    }

    private void GetRatings()
    {
        RatingBAL objBAL = new RatingBAL();
        ViewState["rating"] = objBAL.SelectAll(0);
    }

    protected void gvAppraisal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("FILL"))
        {
            AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
            AppraisalCompetencyBOL objCy = new AppraisalCompetencyBOL();
            lblAppPeriodID.Text = e.CommandArgument.ToString();
            objCy.AppraisalPeriodID = Util.ToInt(e.CommandArgument);
            objCy.RoleID = Util.ToInt(Session["ROLEID"]);
            /*gvForm.DataSource = objBAL.SelectAll(objCy);
            gvForm.DataBind();*/
            ViewState["EMPAPP"] = new EmpAppraisalBAL().SelectAll(Util.ToInt(lblAppPeriodID.Text), Util.ToInt(Session["EMPID"]));

            DataTable dTable = new AppraisalTemplateBAL().GetAppraisalTemplate(Util.ToInt(e.CommandArgument)); //objBAL.SelectAll(objCy);

            if (dTable.Rows.Count > 0)
                lblTemplateID.Text = "" + dTable.Rows[0]["AppTemplateID"];
            ViewState["COMP"] = dTable;


            DataView dView = dTable.DefaultView;
            rptrForm.DataSource = dView.ToTable(true, "CompetencyTypeID", "CompetencyType");
            rptrForm.DataBind();

            if (lblEAID.Text == "")
                btnSubmit.Visible = false;
            else
                btnSubmit.Visible = true;

            if ("" + ViewState["submittedfrm"] == "1")
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
            }

            pNote.Visible = btnSave.Visible;

            ClientScript.RegisterStartupScript(gvAppraisal.GetType(), "onclick", "$('#dvForm').modal();", true);
        }
    }

    protected void gvAppraisal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAppraisal.PageIndex = e.NewPageIndex;
        GetAppraisalPeriods();
    }

    private void GetAppraisalPeriods()
    {
        AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
        DataTable dTable = objBAL.GetAppraisalPeriods(Util.ToInt(Session["EMPID"])).Tables[0];// objBAL.SelectAll(0).Tables[0];
        //  DataView dView = dTable.DefaultView;
        // dView.RowFilter = "PeriodStatus='O'";
        gvAppraisal.DataSource = dTable;// dView.ToTable();
        gvAppraisal.DataBind();
    }

    protected void gvForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("competency");
            e.Row.Cells[1].Text = hrmlang.GetString("rateyourself");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["EMPAPP"] != null)
            {
                DataSet dSet = (DataSet)ViewState["EMPAPP"];
                if (dSet != null)
                {
                    if (dSet.Tables.Count > 0)
                    {
                        DataTable dTable = dSet.Tables[0];
                        if (dTable.Rows.Count > 0)
                        {
                            lblEAID.Text = "" + dTable.Rows[0]["EAID"];

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
        ViewState["EMPAPP"] = new EmpAppraisalBAL().SelectAll(Util.ToInt(lblAppPeriodID.Text), Util.ToInt(Session["EMPID"]));

    }

    private void Save()
    {
        try
        {
            EmpAppraisalBOL objBOL = new EmpAppraisalBOL();
            objBOL.CreatedBy = User.Identity.Name;
            objBOL.Status = "Y";
            objBOL.EmployeeID = Util.ToInt(Session["EMPID"]);
            objBOL.AppPeriodID = Util.ToInt(lblAppPeriodID.Text);
            objBOL.AppTemplateID = Util.ToInt(lblTemplateID.Text);
            objBOL.EAID = new EmpAppraisalBAL().SaveMaster(objBOL);

            foreach (RepeaterItem rItem in rptrForm.Items)
            {
                GridView gvForm = (GridView)rItem.FindControl("gvForm");
                for (int i = 0; i < gvForm.Rows.Count; i++)
                {
                    GridViewRow gRow = gvForm.Rows[i];
                    Label lblCID = (Label)gRow.FindControl("lblCID");

                    objBOL.CompetencyID = Util.ToInt(lblCID.Text);
                    objBOL.RatingID = Util.ToDecimal(((TextBox)gRow.FindControl("txtWeightage")).Text);
                    objBOL.Comments = ((TextBox)gRow.FindControl("txtComments")).Text.Trim();
                    new EmpAppraisalBAL().SaveDetails(objBOL);
                }
            }
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + hrmlang.GetString("datasaved") + "');", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + ex.Message + "');", true);
        }
    }

    protected void gvAppraisal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("startdate");
            e.Row.Cells[2].Text = hrmlang.GetString("enddate");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");

            string[] permissions = (string[])ViewState["permissions"];

            lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
            lnkEdit.Attributes.Add("title", hrmlang.GetString("fillappraisalform"));

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            new EmpAppraisalBAL().SubmitAppraisal(Util.ToInt(lblEAID.Text), Util.ToInt(Session["EMPID"]));
            ViewState["EMPAPP"] = new EmpAppraisalBAL().SelectAll(Util.ToInt(lblAppPeriodID.Text), Util.ToInt(Session["EMPID"]));
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + hrmlang.GetString("datasubmitted") + "');", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('" + ex.Message + "');", true);
        }
    }
}