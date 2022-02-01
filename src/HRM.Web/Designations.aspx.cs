using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Designations : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newdesignation");
        txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterdesignationcode"));
        txtDesignation.Attributes.Add("placeholder", hrmlang.GetString("enterdesignation"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "designations.aspx");
          
            GetCompanyDetails();

            GetDesignations();
            BindParent();
        }
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
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

    private void GetDesignations()
    {
        OrgDesignationBAL objBAL = new OrgDesignationBAL();
        DataTable dtDesgn = objBAL.SelectAll(Util.ToInt(lblCompanyID.Text));
        gvDesignation.DataSource = dtDesgn;
        gvDesignation.DataBind();
        ViewState["dtDesgn"] = dtDesgn;
    }

    private void BindParent()
    {
        OrgDesignationBAL objDesgn = new OrgDesignationBAL();
        ddlDesgn.DataSource = objDesgn.SelectAll(Util.ToInt(lblCompanyID.Text));
        ddlDesgn.DataBind();
       
        ddlDesgn.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblDesgnID.Text = e.CommandArgument.ToString();
            txtCode.Text = Util.CleanString(row.Cells[0].Text);
            txtDesignation.Text = Util.CleanString(row.Cells[1].Text);
            BindParent();
              if ("" + ((Label)row.FindControl("lblParentID")).Text != "")
                  ddlDesgn.SelectedValue = ((Label)row.FindControl("lblParentID")).Text;
           if(ddlDesgn.SelectedValue !=null)
           {
              
               ddlDesgn.Items.Remove(ddlDesgn.SelectedItem);
         
               ddlDesgn.SelectedIndex = 0;
           }
             
           
           pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            OrgDesignationBAL objBAL = new OrgDesignationBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("designationdeleted");
            GetDesignations();
            BindParent();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {
            OrgDesignationBAL objBAL = new OrgDesignationBAL();
            OrgDesignationBOL objBol = new OrgDesignationBOL();

            objBol.DesignationID = Util.ToInt(lblDesgnID.Text);
            objBol.CompanyID = Util.ToInt(lblCompanyID.Text);
            objBol.DesgnCode = txtCode.Text.Trim();
            objBol.Designation = txtDesignation.Text.Trim();
            objBol.ParentID = Util.ToInt(ddlDesgn.SelectedValue);
            objBol.CreatedBy = "";
            objBAL.Save(objBol);
            lblMsg.Text = hrmlang.GetString("designationsaved");
            Clear();
            GetDesignations();
            BindParent();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblDesgnID.Text = "";
        txtDesignation.Text = "";
        txtCode.Text = "";
       // ddlDesgn.SelectedIndex = 0;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDesignation.PageIndex = e.NewPageIndex;
        GetDesignations();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
       
       BindParent();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
        
        BindParent();
    }
    protected void gvDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("designationcode");
            e.Row.Cells[1].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDesignation.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDesignation.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDesignation.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDesignation.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletedesignation"));
            }
        }
    }
}