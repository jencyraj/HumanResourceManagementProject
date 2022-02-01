using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class Complaints : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "Complaints.aspx");
        }
        string[] permissions = (string[])ViewState["permissions"];
        btnNew.Visible = true; //(permissions[0] == "Y") ? true : false;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddComplaint.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ComplaintsBOL objCom = new ComplaintsBOL();

        string complaintById = null;
        string complaintAgainstId = null;

        if (hfEmployeeId.Value != "")
            complaintById = hfEmployeeId.Value;

        if (hfAgainst.Value != "")
            complaintAgainstId = hfAgainst.Value;

        if((!string.IsNullOrEmpty(txtEmployee.Text) && string.IsNullOrEmpty(hfEmployeeId.Value))
            ||(!string.IsNullOrEmpty(txtAgainst.Text) && string.IsNullOrEmpty(hfAgainst.Value)))
        {
            lblErr.Text = "No complaints found";
            gvComplaint.DataSource = null;
            gvComplaint.DataBind();
            return;
        }

        DataTable dT = new ComplaintsBAL().Search(complaintById, complaintAgainstId);
        gvComplaint.DataSource = dT;
        gvComplaint.DataBind();

        if (dT.Rows.Count == 0)
            lblErr.Text = "No complaints found";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtEmployee.Text = "";
        txtAgainst.Text = "";
        hfEmployeeId.Value = "";
        hfAgainst.Value = "";
    }
    protected void gvComplaint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            ComplaintsBOL objCom = new ComplaintsBOL();

            new ComplaintsBAL().Delete(int.Parse(e.CommandArgument.ToString()), User.Identity.Name);
            if (hfEmployeeId.Value != "")
                objCom.ComplaintBy = Util.ToInt(hfEmployeeId.Value);

            //if (hfAgainst.Value != "")
            //    objCom.EmployeeID = Util.ToInt(hfAgainst.Value);

            DataTable dT = new ComplaintsBAL().SelectAll(objCom);
            gvComplaint.DataSource = dT;
            gvComplaint.DataBind();
            lblMsg.Text = "Complaint deleted successfully";
        }
    }

    protected void gvComplaint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
            }
        }
    }

    //protected void btnNew_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/ComplaintsNew.aspx");
    //}
}