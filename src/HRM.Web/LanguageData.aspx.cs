using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BOL;
using HRM.BAL;

public partial class LanguageData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            BindLanguageDD();
            BindGrid(null);
            ClearControls();
            ddLanguagesSearch.ClearSelection();
            txtLangKeySearch.Text = string.Empty;
            txtLangTextSearch.Text = string.Empty;
            string Text = this.GetType().Name;


          /*  txtLangKeySearch.Attributes.Add("placeholder", hrmlang.GetString("enterbiometricid"));
            txtLangTextSearch.Attributes.Add("placeholder", hrmlang.GetString("enterbiometricid"));
            btnSearch.Text = hrmlang.GetString("search");
            btnNew.Text = hrmlang.GetString("newlanguagedata");
            txtLangKey.Attributes.Add("placeholder", hrmlang.GetString("enterbiometricid"));
            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");*/
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LangDataBOL objBOL = new LangDataBOL();
        if (ddLanguagesSearch.SelectedValue != "")
            objBOL.LangCultureName = ddLanguagesSearch.SelectedValue;
        else
            objBOL.LangCultureName = "";
        if (!string.IsNullOrEmpty(txtLangKeySearch.Text))
            objBOL.LangKey = txtLangKeySearch.Text.Trim();
        else
            objBOL.LangKey = null;
        if (!string.IsNullOrEmpty(txtLangTextSearch.Text))
            objBOL.LangText = txtLangTextSearch.Text.Trim();
        else
            objBOL.LangText = null;
        BindGrid(objBOL);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearControls();
        ClientScript.RegisterStartupScript(btnNew.GetType(), "hash", "location.hash = '#addnew';", true);
        txtLangKey.Focus();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
     {
        hrmlang.GenerateLangResourceFile(ddLanguagesSearch.SelectedValue);
       // hrmlang.ReadResourceValue();
        //ClearControls();
        //ClientScript.RegisterStartupScript(btnNew.GetType(), "hash", "location.hash = '#addnew';", true);
        //txtLangKey.Focus();
    }
    protected void gvLanguageData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITLG")
        {
            LoadControl("" + e.CommandArgument);
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#addnew';", true);
        }
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                LangDataBAL objBAL = new LangDataBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblErr.Text = string.Empty;
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("errorlanguagedatadelete");
            }
            LangDataBOL objBOL = new LangDataBOL();
            if (ddLanguagesSearch.SelectedValue != "0")
                objBOL.LanguageId = Util.ToInt(ddLanguagesSearch.SelectedValue);
            else
                objBOL.LanguageId = 0;
            if (!string.IsNullOrEmpty(txtLangKeySearch.Text))
                objBOL.LangKey = txtLangKeySearch.Text.Trim();
            else
                objBOL.LangKey = null;
            if (!string.IsNullOrEmpty(txtLangTextSearch.Text))
                objBOL.LangText = txtLangTextSearch.Text.Trim();
            else
                objBOL.LangText = null;
            BindGrid(objBOL);
        }
    }

    protected void gvLanguageData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLanguageData.PageIndex = e.NewPageIndex;
        LangDataBOL objBOL = new LangDataBOL();
        if (ddLanguagesSearch.SelectedValue != "0")
            objBOL.LanguageId = Util.ToInt(ddLanguagesSearch.SelectedValue);
        else
            objBOL.LanguageId = 0;
        if (!string.IsNullOrEmpty(txtLangKeySearch.Text))
            objBOL.LangKey = txtLangKeySearch.Text.Trim();
        else
            objBOL.LangKey = null;
        if (!string.IsNullOrEmpty(txtLangTextSearch.Text))
            objBOL.LangText = txtLangTextSearch.Text.Trim();
        else
            objBOL.LangText = null;
        BindGrid(objBOL);
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        int saved = 0;
        if (e.CommandName == "SAVE")
        {
            try
            {
                saved = SaveLangData();
                if (saved == -1)
                    lblErr.Text = hrmlang.GetString("recordsamekeyexistselectedlanguage");
                else
                    lblErr.Text = string.Empty;
                LangDataBOL objBOL = new LangDataBOL();
                if (ddLanguagesSearch.SelectedValue != "")
                    objBOL.LangCultureName = ddLanguagesSearch.SelectedValue;
                else
                    objBOL.LangCultureName = "";
                if (!string.IsNullOrEmpty(txtLangKeySearch.Text))
                    objBOL.LangKey = txtLangKeySearch.Text.Trim();
                else
                    objBOL.LangKey = null;
                if (!string.IsNullOrEmpty(txtLangTextSearch.Text))
                    objBOL.LangText = txtLangTextSearch.Text.Trim();
                else
                    objBOL.LangText = null;
                BindGrid(objBOL);
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "alert('" + hrmlang.GetString("datasaved") + "');", true);
            }
            catch
            {
                lblErr.Text = hrmlang.GetString("errorlanguagedatasave");
            }
        }
        if (saved != -1 || e.CommandName == "CANCEL")
            ClearControls();
    }

    private void BindLanguageDD()
    {
        DataTable dt = new LangDataBAL().SelectLanguage(0);
        ViewState["LANGS"] = dt;

        ddLanguagesSearch.DataSource = dt;
        ddLanguagesSearch.DataValueField = "LangCultureName";
        ddLanguagesSearch.DataTextField = "LangName";
        ddLanguagesSearch.DataBind();
        ddLanguagesSearch.Items.Insert(0, (new ListItem("All", "")));
    }

    private void BindGrid(LangDataBOL objBOL)
    {
        gvLanguageData.DataSource = new LangDataBAL().Select(objBOL);
        gvLanguageData.DataBind();
    }

    private DataTable LangTable()
    {
        DataTable dt = (DataTable)ViewState["LANGS"];

        DataTable dtLang = new DataTable("LANGLIST");
        dtLang.Columns.Add("LangCultureName");
        dtLang.Columns.Add("LangName");
        dtLang.Columns.Add("LangKey");
        dtLang.Columns.Add("LangText");
        dtLang.Columns.Add("todelete");

        DataRow dr = dtLang.NewRow();
        dr[0] = dt.Rows[0]["langculturename"].ToString();
        dr[1] = "";
        dr[2] = "";
        dr[3] = "";
        dr[4] = "N";
        dtLang.Rows.Add(dr);

        return dtLang;
    }

    private void ClearControls()
    {
        hfLID.Value = "";
        txtLangKey.Text = string.Empty;
        gvAdd.DataSource = LangTable();
        gvAdd.DataBind();
        lblErr.Text = string.Empty;
    }

    private void LoadControl(string sLangKey)
    {
        LangDataBOL objBOL = new LangDataBOL();
        objBOL.LangKey = sLangKey;
        DataTable dTable = new LangDataBAL().Select(objBOL);

        if (dTable.Rows.Count > 0)
        {
            dTable.Columns.Add("todelete", typeof(string));
            dTable.Columns["todelete"].DefaultValue = "N";
        }

        txtLangKey.Text = sLangKey;
        gvAdd.DataSource = (dTable.Rows.Count == 0) ? LangTable() : dTable;
        gvAdd.DataBind();

    }

    private int SaveLangData()
    {
        int saved = 0;

        foreach (GridViewRow gRow in gvAdd.Rows)
        {
            LangDataBOL objBOL = new LangDataBOL();

            Label lblCulture = (Label)gRow.FindControl("lblCulture");
            TextBox txtText = (TextBox)gRow.FindControl("txtText");

            objBOL.LangCultureName = lblCulture.Text;
            objBOL.LangKey = txtLangKey.Text.Trim();
            objBOL.LangText = txtText.Text.Trim();
            if (!((CheckBox)gRow.FindControl("chkSelect")).Checked)
                objBOL.Status = "Y";
            else
                objBOL.Status = "N";
            objBOL.CreatedBy = User.Identity.Name;
            objBOL.ModifiedBy = User.Identity.Name;
            LangDataBAL objBAL = new LangDataBAL();
            saved = objBAL.Save(objBOL);
        }
        return saved;
    }

    protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Text = hrmlang.GetString("languages");
           // e.Row.Cells[1].Text = hrmlang.GetString("languagetext");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAdd.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAdd.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAdd.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAdd.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblLang = (Label)e.Row.FindControl("lblLang");
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            chkSelect.Text = hrmlang.GetString("delete");
            if (lblLang.Text.Trim() == "")
            {
                ((TextBox)e.Row.FindControl("txtText")).Visible = false;
                chkSelect.Visible = false;
            }
            else
            {
                ((TextBox)e.Row.FindControl("txtText")).Visible = true;
                chkSelect.Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddLanguages = (DropDownList)e.Row.FindControl("ddLanguages");
            ddLanguages.DataSource = (DataTable)ViewState["LANGS"];
            ddLanguages.DataValueField = "LangCultureName";
            ddLanguages.DataTextField = "LangName";
            ddLanguages.DataBind();
            /* GenerateLangResourceFile TextBox txtLangText = (TextBox)e.Row.FindControl("txtLangText");
              txtLangText.Attributes.Add("placeholder", hrmlang.GetString("enterlanguagetext"));
              Button btnAdd = (Button)e.Row.FindControl("btnAdd");
              btnAdd.Text = hrmlang.GetString("add");*/
        }
    }
    protected void gvAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ADDKEY")
        {
            DataTable dtLang = LangTable();

            foreach (GridViewRow gRow in gvAdd.Rows)
            {
                Label lblLang = (Label)gRow.FindControl("lblLang");
                Label lblCulture = (Label)gRow.FindControl("lblCulture");
                TextBox txtText = (TextBox)gRow.FindControl("txtText");
                CheckBox chkSelect = (CheckBox)gRow.FindControl("chkSelect");

                if (lblLang.Text.Trim() == "") continue;

                DataRow dr = dtLang.NewRow();
                dr[0] = lblCulture.Text;
                dr[1] = lblLang.Text;
                dr[2] = txtLangKey.Text.Trim();
                dr[3] = txtText.Text.Trim();
                dr[4] = (chkSelect.Checked) ? "Y" : "N";
                dtLang.Rows.Add(dr);
            }

            DropDownList ddLanguages = (DropDownList)gvAdd.FooterRow.FindControl("ddLanguages");
            DataRow drnew = dtLang.NewRow();
            drnew[0] = ddLanguages.SelectedValue;
            drnew[1] = ddLanguages.SelectedItem.Text;
            drnew[2] = txtLangKey.Text.Trim();
            drnew[3] = ((TextBox)gvAdd.FooterRow.FindControl("txtLangText")).Text.Trim(); ;
            drnew[4] = "N";
            dtLang.Rows.Add(drnew);

            if (dtLang.Rows.Count > 0)
                dtLang.Rows.RemoveAt(0);

            gvAdd.DataSource = dtLang;
            gvAdd.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void gvLanguageData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // e.Row.Cells[0].Text = hrmlang.GetString("languages");
          //  e.Row.Cells[1].Text = hrmlang.GetString("languagekey");
           // e.Row.Cells[2].Text = hrmlang.GetString("languagetext");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvLanguageData.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvLanguageData.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvLanguageData.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvLanguageData.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletelangdata"));
        }
    }
}