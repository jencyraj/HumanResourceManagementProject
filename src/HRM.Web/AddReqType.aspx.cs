using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using HRM.BAL;
using HRM.BOL;

public partial class CheckList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        txtRole.Attributes.Add("placeholder", hrmlang.GetString("entrchecklist"));
        btnNew.Text = hrmlang.GetString("newrole");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");

        if (!IsPostBack)
        {
          //  ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "AddReqType.aspx");
            GetReqType();
        }

      //  if (ViewState["permissions"] == null)
          //  ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "AddReqType.aspx");

       // string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = true;// (permissions[0] == "Y") ? true : false;
    }
    private void GetReqType()
    {
        ReqTypeBAL objBAL = new ReqTypeBAL();
        gvRole.DataSource = objBAL.SelectAll(0);
        gvRole.DataBind();
    }
    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblReqID.Text = e.CommandArgument.ToString();
            txtRole.Text = Util.CleanString(row.Cells[0].Text);
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            ReqTypeBAL objBAL = new ReqTypeBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("chekdlt");
            GetReqType();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    
    {
        //dvMsg.Visible = true;

        try
        {
            ReqTypeBAL objBAL = new ReqTypeBAL();
            ReqTypeBOL objBol = new ReqTypeBOL();

            objBol.ReqID = Util.ToInt(lblReqID.Text);
            objBol.ReqType = txtRole.Text.Trim();
            objBol.ReqDesc = txtdesc.Text.Trim();
            objBol.Status = "Y";// txtRole.Text.Trim();
            objBAL.Save(objBol);
            lblMsg.Text = "Request Type Saved";//hrmlang.GetString("cheksaved");
            Clear();
            GetReqType();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblReqID.Text = "";
        txtRole.Text = "";
     //   string[] permissions = (string[])ViewState["permissions"];
      //  pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRole.PageIndex = e.NewPageIndex;
        GetReqType();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Manage Request Type"; //hrmlang.GetString("managechecklist");

        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvRole.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvRole.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvRole.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvRole.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleterole"));
            }
        }
    }
}