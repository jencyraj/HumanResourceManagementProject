using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AddCompetency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            GetRoles();
            GetAppraisalPeriods();
            GetCompetencyTypes();
            GetDetails();


            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("description"));
            txtRatingDesc1.Attributes.Add("placeholder", hrmlang.GetString("evaldesc1"));
            txtRatingDesc2.Attributes.Add("placeholder", hrmlang.GetString("evaldesc2"));
            txtRatingDesc3.Attributes.Add("placeholder", hrmlang.GetString("evaldesc3"));
            txtRatingDesc4.Attributes.Add("placeholder", hrmlang.GetString("evaldesc4"));
            txtRatingDesc5.Attributes.Add("placeholder", hrmlang.GetString("evaldesc5"));
            txtWeightage.Attributes.Add("placeholer", hrmlang.GetString("weightage"));
            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
        }
    }

    private void GetAppraisalPeriods()
    {
        AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
        DataView dView = new DataView(objBAL.SelectAll(0).Tables[0]);
        dView.RowFilter = "PeriodStatus <> 'C'";
        ddlAppPeriod.DataSource = dView.ToTable();
        ddlAppPeriod.DataBind();
    }

    private void GetRoles()
    {
        RoleBAL objBAL = new RoleBAL();
        ddlRole.DataSource = objBAL.SelectAll(0);
        ddlRole.DataBind();
    }

    private void GetCompetencyTypes()
    {
        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        ddlCompetencyType.DataSource = objBAL.SelectAllCompetencyTypes(0);
        ddlCompetencyType.DataBind();
    }

    private void GetDetails()
    {
        int ComID = Util.ToInt(Request.QueryString["id"]);
        if (ComID == 0) return;

        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        AppraisalCompetencyBOL objCy = new AppraisalCompetencyBOL();

        objCy = objBAL.SelectByID(ComID);
        if (objCy == null) return;

        lblCompetencyID.Text = ComID.ToString();
        //ddlAppPeriod.SelectedValue = objCy.AppraisalPeriodID.ToString();
        ddlCompetencyType.SelectedValue = objCy.CompetencyTypeID.ToString();
        ddlRole.SelectedValue = objCy.RoleID.ToString();
        txtDesc.Text = objCy.CompetencyName;
        txtRatingDesc1.Text = objCy.RatingDesc1;
        txtRatingDesc2.Text = objCy.RatingDesc2;
        txtRatingDesc3.Text = objCy.RatingDesc3;
        txtRatingDesc4.Text = objCy.RatingDesc4;
        txtRatingDesc5.Text = objCy.RatingDesc5;
        txtWeightage.Text = objCy.Weightage.ToString();
        ddlAppPeriod.ClearSelection();
        string[] sArr = objCy.AppraisalPeriod.Split(',');
        for (int i = 0; i < sArr.Length; i++)
        {
            ListItem lItem = ddlAppPeriod.Items.FindByValue(sArr[i].Replace(",", ""));
            if (lItem != null)
                lItem.Selected = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
            AppraisalCompetencyBOL objBol = new AppraisalCompetencyBOL();

            objBol.CompetencyID = Util.ToInt(lblCompetencyID.Text);
            objBol.CompetencyTypeID = Util.ToInt(ddlCompetencyType.SelectedValue);
            //objBol.RoleID = Util.ToInt(ddlRole.SelectedValue);
            //            objBol.AppraisalPeriodID = Util.ToInt(ddlAppPeriod.SelectedValue);
            for (int i = 0; i < ddlAppPeriod.Items.Count; i++)
                if (ddlAppPeriod.Items[i].Selected)
                    objBol.AppraisalPeriod += ddlAppPeriod.Items[i].Value + ",";
            objBol.CompetencyName = txtDesc.Text.Trim();
            objBol.RatingDesc1 = txtRatingDesc1.Text.Trim();
            objBol.RatingDesc2 = txtRatingDesc2.Text.Trim();
            objBol.RatingDesc3 = txtRatingDesc3.Text.Trim();
            objBol.RatingDesc4 = txtRatingDesc4.Text.Trim();
            objBol.RatingDesc5 = txtRatingDesc5.Text.Trim();
            objBol.Weightage = Util.ToInt(txtWeightage.Text.Trim());
            objBol.Status = "Y";
            objBol.CreatedBy = User.Identity.Name;

            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("appraisalcompetencysaved");
            if (lblCompetencyID.Text == "")
                Clear();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblCompetencyID.Text = "";
        txtDesc.Text = "";
        ddlCompetencyType.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        txtWeightage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCompetency.aspx");
    }
}