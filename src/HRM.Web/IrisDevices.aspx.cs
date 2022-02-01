using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class IrisDevices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;



        if (!IsPostBack)
        {
            btnNew.Text = hrmlang.GetString("newdevice");
            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");

            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "irisdevices.aspx");
            GetDevices();
            GetBranches();
        }

        if (ViewState["permissions"] == null)
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "irisdevices.aspx");

        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;
    }

    private void GetDevices()
    {
        IrisDeviceBAL objBAL = new IrisDeviceBAL();
        gvDevice.DataSource = objBAL.SelectAll();
        gvDevice.DataBind();

    }

    private void GetBranches()
    {
        OrgBranchesBAL objBAL = new OrgBranchesBAL();
        ddlBranch.DataSource = objBAL.SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataBind();

        if (ddlBranch.Items.Count == 0)
            ddlBranch.Items.Add(hrmlang.GetString("select"));
    }

    protected void gvDevice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblID.Text = e.CommandArgument.ToString();
            txtIP.Text = Util.CleanString(row.Cells[0].Text);
            txtDoor.Text = row.Cells[1].Text;
            txtSecurity.Text = row.Cells[2].Text;
            txtUserID.Text = row.Cells[3].Text;
            txtPassword.Text = row.Cells[4].Text;
            ddlBranch.ClearSelection();
            ListItem lItem = ddlBranch.Items.FindByValue(((Label)row.FindControl("lblBranchID")).Text);
            if (lItem != null)
                lItem.Selected = true;
            else
                ddlBranch.SelectedIndex = 0;
            pnlNew.Visible = true;
        }

        if (e.CommandName.Equals("DEL"))
        {
            IrisDeviceBAL objBAL = new IrisDeviceBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("datadeleted");
            GetDevices();
        }

    }
    protected void btnYesOnclick(object sender, EventArgs e)
    {
        IrisDeviceBAL objBAL = new IrisDeviceBAL();
        objBAL.Update();
        save();
    }
    protected void btnNoOnclick(object sender, EventArgs e)
    {
        chkSelect.Checked = false;
        save();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;
        // DataTable dTable = new DataTable();
        try
        {
            IrisDeviceBAL objBAL = new IrisDeviceBAL();

            if (chkSelect.Checked)
            {


                for (int count = 0; count < gvDevice.Rows.Count; count++)
                {
                    string master = ((Label)gvDevice.Rows[count].Cells[0].FindControl("lblmstrdev")).Text;
                    if (master == "Y")
                    {
                        if (txtIP.Text == gvDevice.Rows[0].Cells[1].ToString())
                            save();
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvConfirm').modal();", true);
                            return;
                        }
                    }
                }
            }
            else
                save();

            Clear();
            GetDevices();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    private void save()
    {
        IrisDeviceBAL objBAL = new IrisDeviceBAL();

        objBAL.Save(Util.ToInt(lblID.Text), txtIP.Text.Trim(), txtSecurity.Text.Trim(), txtUserID.Text.Trim(), txtPassword.Text.Trim(), "Y", Util.ToInt(ddlBranch.SelectedValue), txtDoor.Text.Trim(), (chkSelect.Checked) ? "Y" : "N");
        lblMsg.Text = hrmlang.GetString("datasaved");
    }

    private void Clear()
    {
        lblID.Text = "";
        txtIP.Text = "";
        txtSecurity.Text = "";
        txtUserID.Text = "";
        txtPassword.Text = "";
        ddlBranch.ClearSelection();
        ddlBranch.SelectedIndex = 0;
        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

    }
    protected void gvDevice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDevice.PageIndex = e.NewPageIndex;
        GetDevices();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void gvDevice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("ipaddress");
            e.Row.Cells[1].Text = hrmlang.GetString("doorname");
            e.Row.Cells[2].Text = hrmlang.GetString("securityid");
            e.Row.Cells[3].Text = hrmlang.GetString("userid");
            e.Row.Cells[4].Text = hrmlang.GetString("password");
            e.Row.Cells[5].Text = hrmlang.GetString("branch");
            e.Row.Cells[6].Text = hrmlang.GetString("masterdevice");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDevice.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDevice.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDevice.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDevice.PagerSettings.LastPageText = hrmlang.GetString("last");
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
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
            }
        }
    }
}