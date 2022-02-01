using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AppraisalCompetency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "appraisalcompetency.aspx");
            GetRoles();
            GetAppraisalPeriods();
            GetCompetencyTypes();
            GetAppraisalCompetency();

            SetData();
        }

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void SetData()
    {
        lblBranch.Text = hrmlang.GetString("appraisalperiod");
        lblRole.Text = hrmlang.GetString("role");
        lblType.Text = hrmlang.GetString("competencytype");
        lnkSearch.Text = hrmlang.GetString("go");
        btnNew.Text = hrmlang.GetString("addnew");
    }

    private void GetAppraisalPeriods()
    {
        AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
        ddlSApp.DataSource = objBAL.SelectAll(0);
        ddlSApp.DataBind();

        ddlSApp.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    private void GetRoles()
    {
        RoleBAL objBAL = new RoleBAL();
        ddlSRole.DataSource = objBAL.SelectAll(0);
        ddlSRole.DataBind();
        ddlSRole.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    private void GetAppraisalCompetency()
    {
        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        AppraisalCompetencyBOL objCy = new AppraisalCompetencyBOL();

        objCy.AppraisalPeriodID = Util.ToInt(ddlSApp.SelectedValue);
        objCy.CompetencyTypeID = Util.ToInt(ddlSType.SelectedValue);
        objCy.RoleID = Util.ToInt(ddlSRole.SelectedValue);
        gvApp.DataSource = objBAL.SelectAll(objCy);
        gvApp.DataBind();

        if (gvApp.Rows.Count == 0)
            lblErr.Text = hrmlang.GetString("nodatafound");// "No competencies found";
    }

    private void GetCompetencyTypes()
    {
        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        ddlSType.DataSource = objBAL.SelectAllCompetencyTypes(0);
        ddlSType.DataBind();
        ddlSType.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    protected void gvApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWAPPPERIOD")
        {
            string sArg = e.CommandArgument.ToString();

            txtAppPeriod.Text = "";
            DataTable dt = new AppraisalPeriodBAL().SelectAll(0).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sArg == "")
                        txtAppPeriod.Text += (txtAppPeriod.Text == "") ? "" + dt.Rows[i]["Description"] : "," + dt.Rows[i]["Description"];
                    else
                    {
                        if (sArg.IndexOf(dt.Rows[i]["AppPeriodID"] + ",") > -1)
                            txtAppPeriod.Text += (txtAppPeriod.Text == "") ? "" + dt.Rows[i]["Description"] : "," + dt.Rows[i]["Description"];
                    }
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvAppPeriod').modal();", true);
        }
        if (e.CommandName.Equals("EDITBR"))
        {
            Response.Redirect("AddCompetency.aspx?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName.Equals("DEL"))
        {
            AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            ClientScript.RegisterStartupScript(gvApp.GetType(), "onclick", "alert('" + hrmlang.GetString("appraisalcompetencydeleted") + "');", true);
            GetAppraisalCompetency();
        }
    }

    protected void gvApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApp.PageIndex = e.NewPageIndex;
        GetAppraisalCompetency();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCompetency.aspx");
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetAppraisalCompetency();
    }
    protected void gvApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("appraisalperiod");
            e.Row.Cells[1].Text = hrmlang.GetString("competencytype");
            e.Row.Cells[2].Text = hrmlang.GetString("description");
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