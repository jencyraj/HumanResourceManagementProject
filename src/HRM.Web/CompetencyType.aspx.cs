using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class CompetencyType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "appraisalperiod.aspx");
            GetCompetencyTypes();

            txtCompetencyType.Attributes.Add("placeholder", hrmlang.GetString("entercompetencytype"));
            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetCompetencyTypes()
    {
        AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
        gvCompetencyType.DataSource = objBAL.SelectAllCompetencyTypes(0);
        gvCompetencyType.DataBind();

    }

    protected void gvCompetencyType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblCompetencyTypeID.Text = e.CommandArgument.ToString();
            txtCompetencyType.Text = Util.CleanString(row.Cells[0].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
            objBAL.DeleteCompetencyType(Util.ToInt(e.CommandArgument), User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("comtypedeleted");
           // lblMsg.Text = "CompetencyType deleted successfully";
            GetCompetencyTypes();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            AppraisalCompetencyBAL objBAL = new AppraisalCompetencyBAL();
            AppraisalCompetencyBOL objBol = new AppraisalCompetencyBOL();

            objBol.CompetencyTypeID = Util.ToInt(lblCompetencyTypeID.Text);
            objBol.CompetencyType = txtCompetencyType.Text.Trim();
            objBAL.SaveCompetencyType(objBol);
            lblMsg.Text = hrmlang.GetString("comtypesaved");
          //  lblMsg.Text = "CompetencyType saved successfully";
            Clear();
            GetCompetencyTypes();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblCompetencyTypeID.Text = "";
        txtCompetencyType.Text = "";
    }
    protected void gvCompetencyType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCompetencyType.PageIndex = e.NewPageIndex;
        GetCompetencyTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvCompetencyType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("competencytype");
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