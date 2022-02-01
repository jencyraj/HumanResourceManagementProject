using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Assets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnSearch.Text = hrmlang.GetString("search");
        btnNew.Text = hrmlang.GetString("newasset");
        btnTransfer.Text = hrmlang.GetString("transfer");
        btnAssign.Text = hrmlang.GetString("assign");
        txtAssignTo.Attributes.Add("placeholder", hrmlang.GetString("assignto"));
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "assets.aspx");
            GetBranches();
            GetAssets();

            if ("" + Request.QueryString["saved"] == "1")
                lblMsg.Text = hrmlang.GetString("assetsaved");
        }

        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetAssets()
    {
        AssetsBAL objBAL = new AssetsBAL();
        AssetsBOL objBOL = new AssetsBOL();

        objBOL.BranchID = Util.ToInt(ddlBranch.SelectedValue);
        gvAsset.DataSource = objBAL.SelectAll(objBOL);
        gvAsset.DataBind();

        if (gvAsset.Rows.Count == 0)
            lblMsg.Text = hrmlang.GetString("noassetsfound");
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(Session["COMPANYID"]));
        dt = Util.ReturnDT("BranchID", "Branch", dt);
        ddlNewBranch.DataSource = ddlBranch.DataSource = dt;
        ddlBranch.DataBind();
        ddlNewBranch.DataBind();
        ViewState["BRANCHLIST"] = dt;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAssets();
    }

    protected void gvAsset_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "TRANSFER")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            lblAssetID.Text = e.CommandArgument.ToString().Split('|')[0];
            lblCurrBranchID.Text = e.CommandArgument.ToString().Split('|')[1];
            lblCurrBranch.Text = gRow.Cells[0].Text;
            lblAsset.Text = gRow.Cells[3].Text;

            if (ViewState["BRANCHLIST"] != null)
            {
                ddlNewBranch.DataSource = (DataTable)ViewState["BRANCHLIST"];
                ddlNewBranch.DataBind();
            }
            if (ddlNewBranch.Items.Count > 0)
            {
                ListItem lItem = ddlNewBranch.Items.FindByText(lblCurrBranch.Text);
                if (lItem != null)
                    ddlNewBranch.Items.Remove(lItem);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvTransfer').modal();", true);
        }
        else if (e.CommandName == "ASSIGN")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            lblAssetID.Text = e.CommandArgument.ToString().Split('|')[0];
            lblCurrBrnchID.Text = e.CommandArgument.ToString().Split('|')[1];
            lblCurBranch.Text = gRow.Cells[0].Text;
            lblAssignAsset.Text = gRow.Cells[3].Text;
            txtAssignTo.Text = "";

            AssetsBOL objBOL = new AssetsBOL();
            objBOL.AssetID = Util.ToInt(lblAssetID.Text);
            DataTable dTable = new AssetsBAL().SelectAssignedAssets(objBOL);
            if (dTable.Rows.Count > 0)
            {
                DataRow dRow = dTable.Rows[0];
                dvAssigned.Visible = true;
                lblAssignTo.Text = "" + dRow["FirstName"] + (("" + dRow["MiddleName"] == "") ? "" : " " + dRow["MiddleName"]) + (("" + dRow["LastName"] == "") ? "" : " " + dRow["LastName"]);
            }
            else
                dvAssigned.Visible = false;

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvAssign').modal();", true);
        }
        else if (e.CommandName == "Return")
        {
            GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            lblAssetID.Text = e.CommandArgument.ToString().Split('|')[0];
            Label lblEID = (Label)gRow.FindControl("lblEID");
            lblEmp.Text = lblEID.Text;
            ReturnAsset();
        }
        else if (e.CommandName.Equals("DEL"))
        {
            AssetsBAL objBAL = new AssetsBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("assetdeleted");
            GetAssets();
        }

    }

    protected void gvAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAsset.PageIndex = e.NewPageIndex;
        GetAssets();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("addasset.aspx");
    }

    protected void gvAsset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("branch");
            e.Row.Cells[1].Text = hrmlang.GetString("assettype");
            e.Row.Cells[2].Text = hrmlang.GetString("assetcode");
            e.Row.Cells[3].Text = hrmlang.GetString("assetname");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAsset.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAsset.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAsset.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAsset.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (ViewState["permissions"] != null)
            {
                LinkButton lnkTransfer = (LinkButton)e.Row.FindControl("lnkTransfer");
                LinkButton lnkAssign = (LinkButton)e.Row.FindControl("lnkAssign");
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;


 				lnkTransfer.Attributes.Add("title", hrmlang.GetString("transfer"));
                lnkAssign.Attributes.Add("title", hrmlang.GetString("assigntoemployee"));
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeleteasset"));
            }

            Label lblName = (Label)e.Row.FindControl("lblName");
            Label lblEID = (Label)e.Row.FindControl("lblEID");
            string assetid = DataBinder.Eval(e.Row.DataItem, "assetid").ToString();
            AssetsBOL objBOL = new AssetsBOL();
            objBOL.AssetID = Util.ToInt(assetid);
            DataTable dTable = new AssetsBAL().SelectAssignedAssets(objBOL);
            if (dTable.Rows.Count > 0)
            {
                DataRow dRow = dTable.Rows[0];
                lblName.Text = "" + dRow["FirstName"] + (("" + dRow["MiddleName"] == "") ? "" : " " + dRow["MiddleName"]) + (("" + dRow["LastName"] == "") ? "" : " " + dRow["LastName"]);
                lblEID.Text = "" + dRow["EmployeeID"];
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAssets();
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            AssetsBAL objBAL = new AssetsBAL();
            AssetsBOL objAsset = new AssetsBOL();
            objAsset.AssetTransferID = 0;
            objAsset.AssetID = Util.ToInt(lblAssetID.Text);
            objAsset.BranchFrom = Util.ToInt(lblCurrBranchID.Text);
            objAsset.BranchTo = Util.ToInt(ddlNewBranch.SelectedValue);
            objAsset.TransferredBy = User.Identity.Name;
            objBAL.TransferAsset(objAsset);
            lblMsg.Text = hrmlang.GetString("assettransferredto") + ddlNewBranch.SelectedItem.Text + hrmlang.GetString("successfully");
            GetAssets();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            AssetsBAL objBAL = new AssetsBAL();
            AssetsBOL objAsset = new AssetsBOL();
            objAsset.AssetID = Util.ToInt(lblAssetID.Text);
            objAsset.EmployeeID = Util.ToInt(hfEmployeeId.Value);
            objAsset.AssignedBy = User.Identity.Name;
            objBAL.AssignToEmployee(objAsset);
            lblMsg.Text = "Asset has been assigned successfully";
            //GetAssets();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    private void ReturnAsset()
    {
        try
        {
            AssetsBAL objBAL = new AssetsBAL();
            AssetsBOL objAsset = new AssetsBOL();
            objAsset.AssetID = Util.ToInt(lblAssetID.Text);
            objAsset.EmployeeID = Util.ToInt(lblEmp.Text);
            objAsset.AssignedBy = User.Identity.Name;
            objBAL.RemoveAssignment(objAsset);
           
            GetAssets();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

   
}