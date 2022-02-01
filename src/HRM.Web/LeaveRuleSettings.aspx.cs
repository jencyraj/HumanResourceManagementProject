using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BOL;
using HRM.BAL;
using System.Data;
public partial class AddNewLeaveRule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // btnNew.Text = hrmlang.GetString("newbranch");


        if (!IsPostBack)
        {

            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "leaverulesettings.aspx");

          

        }

        string[] permissions = (string[])ViewState["permissions"];

        btnNew.Visible = (permissions[0] == "Y") ? true : false;
        GetLeaveTypes();

    }





    private void GetLeaveTypes()
    {
        LeaveRuleBAL objBAL = new LeaveRuleBAL();
        gvLType.DataSource = objBAL.Select();
        gvLType.DataBind();

    }

    protected void gvLType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITBR")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            lblLeaveTypeID.Text = Convert.ToString(id);


            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#addnew';", true);
        }


        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                LeaveRuleBAL objBAL = new LeaveRuleBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument), User.Identity.Name);

                GetLeaveTypes();
            }
            catch
            {
                // lblErr.Text = hrmlang.GetString("errorleaveruledatadelete");
            }
            GetLeaveTypes();
        }

    }



    private void Clear()
    {
        lblLeaveTypeID.Text = "";

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }
    protected void gvLType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLType.PageIndex = e.NewPageIndex;
        GetLeaveTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        Response.Redirect("AddNewLeaveRule.aspx");
        //  ClientScript.RegisterStartupScript(btnNew.GetType(), "hash", "location.hash = '#addnew';", true);

    }
    protected void gvLType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
            e.Row.Cells[1].Text = hrmlang.GetString("start");
            e.Row.Cells[2].Text = hrmlang.GetString("minleav");

        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvLType.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvLType.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvLType.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvLType.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {

                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteleaverule"));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddllateby = (DropDownList)e.Row.FindControl("ddllateby");
            //DataTable dt = new LeaveRuleBAL().Select();
            //ddllateby.DataSource = dt;
            //ddllateby.DataValueField = "LRID";
            //ddllateby.DataTextField = "LateByType";
            //ddllateby.DataBind();

        }
    }

}