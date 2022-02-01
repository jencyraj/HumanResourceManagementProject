using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Branches : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "branches.aspx");
            GetCountryList();
            GetCompanyDetails();
            GetBranches();

            SetLangData();
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void SetLangData()
    {
        txtBranch.Attributes.Add("placeholder", hrmlang.GetString("enterbranch"));
        txtAddress.Attributes.Add("placeholder", hrmlang.GetString("enteraddress"));
        txtCity.Attributes.Add("placeholder", hrmlang.GetString("entercity"));
        txtState.Attributes.Add("placeholder", hrmlang.GetString("enterstate"));
        txtPhone.Attributes.Add("placeholder", hrmlang.GetString("entertelephone"));
        txtMobile.Attributes.Add("placeholder", hrmlang.GetString("entermobile"));
        txtEmail.Attributes.Add("placeholder", hrmlang.GetString("enteremail"));

        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
    }

    private void GetCountryList()
    {
        CountryBAL objBAL = new CountryBAL();
        ddlCountry.DataSource = objBAL.Select("" + Session["LanguageId"]);
        ddlCountry.DataBind();
       // ddlCountry.Items.Insert(0, new ListItem("", "0"));
    }

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        if (objBOL != null)
        {
            lblCompanyID.Text = objBOL.CompanyID.ToString();
        }

        if (lblCompanyID.Text == "")
            Response.Redirect("Company.aspx");
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        gvBranches.DataSource = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        gvBranches.DataBind();

    }

    protected void gvBranches_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);


            lblBranchID.Text = e.CommandArgument.ToString();
            txtBranch.Text = Util.CleanString(row.Cells[0].Text);
            txtAddress.Text = Util.CleanString(((Label)row.FindControl("lblAddress")).Text);
            txtCity.Text = ((Label)row.FindControl("lblCity")).Text;
            txtState.Text = Util.CleanString(row.Cells[2].Text);
            txtPhone.Text = ((Label)row.FindControl("lblPhone")).Text;
            txtMobile.Text = ((Label)row.FindControl("lblMobile")).Text;
            txtEmail.Text = Util.CleanString(row.Cells[5].Text);
            ddlCountry.SelectedValue = ((Label)row.FindControl("lblCountry")).Text;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            OrgBranchesBAL objBAL = new OrgBranchesBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("brnchdltsucess");

            GetBranches();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            OrgBranchesBAL objBAL = new OrgBranchesBAL();
            OrgBranchesBOL objBol = new OrgBranchesBOL();

            objBol.BranchID = Util.ToInt(lblBranchID.Text);
            objBol.CompanyID = Util.ToInt(lblCompanyID.Text);
            objBol.Branch = txtBranch.Text.Trim();
            objBol.Address = txtAddress.Text.Trim();
            objBol.City = txtCity.Text.Trim();
            objBol.State = txtState.Text.Trim();
            objBol.CountryID = Util.ToInt(ddlCountry.SelectedValue);
            objBol.Email = txtEmail.Text.Trim();
            objBol.Phone1 = txtPhone.Text.Trim();
            objBol.Phone2 = txtMobile.Text.Trim();
            objBAL.Save(objBol);

            lblMsg.Text = hrmlang.GetString("brnchaddsucess");

            Clear();
            GetBranches();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblBranchID.Text = "";
        txtBranch.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtPhone.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        ddlCountry.SelectedIndex = 0;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvBranches_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBranches.PageIndex = e.NewPageIndex;
        GetBranches();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvBranches_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("branch");
            e.Row.Cells[1].Text = hrmlang.GetString("address");
            e.Row.Cells[2].Text = hrmlang.GetString("state");
            e.Row.Cells[3].Text = hrmlang.GetString("country");
            e.Row.Cells[4].Text = hrmlang.GetString("telephone");
            e.Row.Cells[5].Text = hrmlang.GetString("emailaddress");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;

                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletebranch"));
            }
        }
    }
}