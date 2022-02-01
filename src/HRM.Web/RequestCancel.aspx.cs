using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BAL;
using HRM.BOL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            selectReqtype();
            GetResignation();
            pnlreq.Visible = true;
            int val = Util.ToInt(Request.QueryString["RID"]);
            ddlReqtype.SelectedValue =""+ val;
        }
    }
    public void selectReqtype()
    {
        ReqTypeBAL objBAL = new ReqTypeBAL();
        ddlReqtype.DataSource = objBAL.SelectAll(0);
        ddlReqtype.DataBind();
    }
    private void GetResignation()
    {
        ResignationBAL objBAL = new ResignationBAL();
        ResignationBOL objCy = new ResignationBOL();
        objCy.EmployeeID = Util.ToInt(Request.QueryString["id"]);// Util.ToInt(Session["EMPID"]);
        DataTable DT = new DataTable();
        DT = objBAL.SelectAll(objCy);
        gvResignation.DataSource = DT;
        gvResignation.DataBind();
        if (DT.Rows.Count > 0)
        {
            lblresgID.Text = "" + (DT.Rows[0]["Resgnid"]);
        }
        //ddlReqtype.SelectedValue = ""+ resgID;
     //   if (gvResignation.Rows.Count == 0)
            //lblErr.Text = hrmlang.GetString("nodatafound");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ResignationBAL objBAL = new ResignationBAL();
        objBAL.cancel(Util.ToInt(lblresgID.Text),"P");
        new AlertsBAL().Save( Util.ToInt(Request.QueryString["id"]), "Your Resignation Cancel Request Listed for the Approval", "RESIGNATION", "" + Session["LanguageId"]);
         
        //ClientScript.RegisterStartupScript(gvResignation.GetType(), "onclick", "alert('" + "Resignation Canceled" + "');", true);
        Response.Redirect("Resignation.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Resignation.aspx");
    }
}