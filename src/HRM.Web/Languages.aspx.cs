using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using HRM.BOL;
using HRM.BAL;

public partial class Languages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "languages.aspx");
            GetLanguages();
        }

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetLanguages()
    {
        LangDataBAL objBAL = new LangDataBAL();
        DataTable dt = objBAL.SelectLanguage(0);
        gvLanguages.DataSource = dt;
        gvLanguages.DataBind(); 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            foreach (GridViewRow gRow in gvLanguages.Rows)
            {
                int nLID = Util.ToInt(gvLanguages.DataKeys[gRow.RowIndex].Value);
                CheckBox chkActive = (CheckBox)gRow.FindControl("chkActive");
                RadioButtonList rbtnStyle = (RadioButtonList)gRow.FindControl("rbtnStyle");
                DropDownList ddlLang = (DropDownList)gRow.FindControl("ddlLang");

                new LangDataBAL().SaveLanguage(nLID, (chkActive.Checked) ? "Y" : "N", ddlLang.SelectedValue, rbtnStyle.SelectedValue);
            }

            lblMsg.Text = "Data saved successfully";
            Clear();
            GetLanguages();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblLangID.Text = "";
        txtLang.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvLanguages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLanguages.PageIndex = e.NewPageIndex;
        GetLanguages();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
     

    protected void gvLanguages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStyle = (Label)e.Row.FindControl("lblStyle");
            Label lblDefault = (Label)e.Row.FindControl("lblDefault");
            
            RadioButtonList rbtnStyle = (RadioButtonList)e.Row.FindControl("rbtnStyle");
            DropDownList ddlLang = (DropDownList)e.Row.FindControl("ddlLang");
            
            rbtnStyle.SelectedValue = lblStyle.Text;
            ddlLang.SelectedValue = lblDefault.Text;
        }
    }
}