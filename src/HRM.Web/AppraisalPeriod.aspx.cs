using System;
using System.Globalization;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HRM.BOL;
using HRM.BAL;

public partial class AppraisalPeriod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "appraisalperiod.aspx");
            GetAppraisalPeriods();
            string sLang = "" + Session["LanguageId"];
            btnNew.Text = hrmlang.GetString("addnew");

            /*btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            txtStart.Attributes.Add("placeholder", hrmlang.GetString("enterstartdate"));
            txtEnd.Attributes.Add("placeholder", hrmlang.GetString("enterenddate"));

            if (sLang == "en-US" || sLang == "")
            {
                txtStart.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
                txtEnd.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }
            else
            {
                txtStart.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Hijri;
                txtEnd.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            }*/

            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("notstarted"), "P"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("open"), "O"));
            ddlStatus.Items.Add(new ListItem(hrmlang.GetString("closed"), "C"));
        }

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;

    }

    private void GetAppraisalPeriods()
    {
        AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
        gvApp.DataSource = objBAL.SelectAll(0);
        gvApp.DataBind();

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalPeriodNew.aspx");
    }

    protected void gvApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            Response.Redirect("AppraisalPeriodNew.aspx?id=" + e.CommandArgument.ToString());
            /*
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        lblAppPeriodID.Text = e.CommandArgument.ToString();
                        txtDesc.Text = Util.CleanString(row.Cells[0].Text);
                        string sDate = Util.CleanString(row.Cells[1].Text);
                        if (sDate != "")
                        {
                            string[] sDt = sDate.Split('/');
                            sDate = sDt[0].PadLeft(2, '0') + "/" + sDt[1].PadLeft(2, '0') + "/" + sDt[2];
                        }
                        txtStart.SelectedCalendareDate = DateTime.ParseExact(sDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        sDate = Util.CleanString(row.Cells[2].Text);
                        if (sDate != "")
                        {
                            string[] sDt = sDate.Split('/');
                            sDate = sDt[0].PadLeft(2, '0') + "/" + sDt[1].PadLeft(2, '0') + "/" + sDt[2];
                        }
                        txtEnd.SelectedCalendareDate = DateTime.ParseExact(sDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        ddlStatus.SelectedValue = ((Label)row.FindControl("lblStatus")).Text;
                        pnlNew.Visible = true;
             * */
        }

        if (e.CommandName.Equals("DEL"))
        {
            AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);
            lblMsg.Text = hrmlang.GetString("appraisalperioddeleted");
            GetAppraisalPeriods();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            AppraisalPeriodBAL objBAL = new AppraisalPeriodBAL();
            AppraisalPeriodBOL objBol = new AppraisalPeriodBOL();

            objBol.AppPeriodID = Util.ToInt(lblAppPeriodID.Text);
            objBol.Description = txtDesc.Text.Trim();
           // objBol.StartDate = Util.ToDateTime(txtStart.getGregorianDateText);
           // objBol.EndDate = Util.ToDateTime(txtEnd.getGregorianDateText);
            objBol.Status = "Y";
            objBol.CreatedBy = User.Identity.Name;
            objBol.PeriodStatus = ddlStatus.SelectedValue.ToString();

            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("appraisalperiodsaved");
            Clear();
            GetAppraisalPeriods();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblAppPeriodID.Text = "";
        txtDesc.Text = "";
       // txtStart.ClearDate();
        //txtEnd.ClearDate();
        ddlStatus.SelectedIndex = 0;
    }
    protected void gvApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApp.PageIndex = e.NewPageIndex;
        GetAppraisalPeriods();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void gvApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("startdate");
            e.Row.Cells[2].Text = hrmlang.GetString("enddate");
            e.Row.Cells[3].Text = hrmlang.GetString("status");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            HyperLink lnkView = (HyperLink)e.Row.FindControl("lnkView");
            HyperLink lnkTemplate = (HyperLink)e.Row.FindControl("lnkTemplate");

            string[] permissions = (string[])ViewState["permissions"];

            lnkTemplate.Visible = lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
            lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
            lnkView.Visible = (permissions[3] == "Y") ? true : false;
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkTemplate.Attributes.Add("title", hrmlang.GetString("setapptemplate"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblStatusDesc = (Label)e.Row.FindControl("lblStatusDesc");

            if (lblStatus.Text == "P")
                lblStatusDesc.Text = hrmlang.GetString("notstarted");
            if (lblStatus.Text == "O")
                lblStatusDesc.Text = hrmlang.GetString("open");
            if (lblStatus.Text == "C")
                lblStatusDesc.Text = hrmlang.GetString("closed");
        }
    }
}