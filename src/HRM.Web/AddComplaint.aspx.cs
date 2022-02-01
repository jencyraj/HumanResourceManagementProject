using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;
using System.Collections.Generic;

public partial class AddComplaint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "complaints.aspx");

            lblComplaintID.Text = Request.QueryString["ComplaintID"];

            btnSaveEmp.Text = btnSave.Text = hrmlang.GetString("save");

                //btnSave.Text = hrmlang.GetString("save");
                //btnCancel.Text = hrmlang.GetString("cancel");
                //txtComplaintTitle.Attributes.Add("placeholder", hrmlang.GetString("complainttitle"));
                //txtDescription.Attributes.Add("placeholder", hrmlang.GetString("complaintdescription"));
                //txtComplaintTitle.Attributes.Add("placeholder", hrmlang.GetString("complainttitle"));

            GetCompanyDetails();

            if (!String.IsNullOrEmpty(lblComplaintID.Text))
                GetComplaintDetails();
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        objBOL.EmpStatus = "C";

        //if (ddlDept.SelectedIndex > 0)
        //    objBOL.DeptId = int.Parse(ddlDept.SelectedValue);
        //if (ddlDesgn.SelectedIndex > 0)
        //    objBOL.DesgnID = int.Parse(ddlDesgn.SelectedValue);

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);

        string sBranch = "";
        int selected = 0;

        //if (lstBranch.Items.Count > 0)
        //{
        //    for (int i = 0; i < lstBranch.Items.Count; i++)
        //    {
        //        if (!lstBranch.Items[i].Selected)
        //            sBranch = (sBranch == "") ? lstBranch.Items[i].Value : "," + lstBranch.Items[i].Value;
        //        else
        //            selected = 1;
        //    }
        //}

        if (selected == 1)
        {
            string[] sBR = sBranch.Split(',');
            foreach (string s in sBR)
            {
                DataRow[] dRows = dtEmp.Select("BranchID=" + s);
                for (int i = 0; i < dRows.Length; i++)
                    dtEmp.Rows.Remove(dRows[i]);
            }
        }

        //DataView dView = new DataView(dtEmp);
        //if (ddlSubDept.SelectedValue != "0")
        //    dView.RowFilter = "SubDeptID = " + ddlSubDept.SelectedValue;

        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvEmp').modal();", true);
    }

    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
            e.Row.Cells[1].Text = hrmlang.GetString("employeecode");
            e.Row.Cells[2].Text = hrmlang.GetString("branch");
            e.Row.Cells[3].Text = hrmlang.GetString("department");
            e.Row.Cells[4].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEmployee.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEmployee.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEmployee.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEmployee.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Search();
        gvEmployee.PageIndex = e.NewPageIndex;
    }

    protected void gvEmpSelected_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("name");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }

    protected void gvEmpSelected_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            ViewState["EMPLIST"] = null;
            DataTable dTable = new DataTable();
            dTable.Columns.Add("FirstName");
            dTable.Columns.Add("MiddleName");
            dTable.Columns.Add("LastName");
            dTable.Columns.Add("EmployeeID");

            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                if (e.CommandArgument.ToString() == ((Label)gRow.FindControl("lblEmpID")).Text) continue;

                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
                dTable.Rows.Add(dRow);
            }

            if (dTable.Rows.Count == 0)
                lblTitle.Visible = false;
            else
                lblTitle.Visible = true;

            gvEmpSelected.DataSource = dTable;
            gvEmpSelected.DataBind();
        }
    }

    private void GetComplaintDetails()
    {

        ComplaintsBOL objBOL = new ComplaintsBOL();
        ComplaintsBAL objBAL = new ComplaintsBAL();

        lblComplaintID.Text = "" + Request.QueryString["ComplaintID"];
        if (Util.ToInt(lblComplaintID.Text) == 0) return;
        h1.InnerText = LI1.InnerText = "Edit Complaint Details";
        objBOL = objBAL.SelectByID(Util.ToInt(lblComplaintID.Text));
        txtComplaintTitle.Text = objBOL.ComplaintTitle;
        txtDescription.Text = objBOL.Description;

        gvEmpSelected.DataSource = objBAL.SelectEmployeeByComplaintID(Util.ToInt(Request.QueryString["ComplaintID"]));
        gvEmpSelected.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        if (!Page.IsValid)
        { return; }

        try
        {
            ComplaintsBAL objBAL = new ComplaintsBAL();
            ComplaintsBOL objBol = new ComplaintsBOL();
            
                        objBol.ComplaintID = Util.ToInt(lblComplaintID.Text);

                        string sEmp = "";
                        foreach (GridViewRow gRow in gvEmpSelected.Rows)
                        {
                            string sTemp = ((Label)gRow.FindControl("lblEmpID")).Text;

                            sEmp += (sEmp == "") ? sTemp : "," + sTemp;
                        }

                        objBol.EmployeeID = sEmp;
                        objBol.ComplaintTitle = txtComplaintTitle.Text.Trim();
                        objBol.Description = txtDescription.Text.Trim();
                        objBol.ComplaintBy = Util.ToInt(Session["EMPID"]);
                        objBol.CreatedBy = Util.ToInt(Session["EMPID"]);
                        objBol.Status = "P";
                        objBol.ComplaintID = objBAL.Save(objBol);
                        lblComplaintID.Text = objBol.ComplaintID.ToString();
                        lblMsg.Text = "Complaint saved successfully";

                        Response.Redirect("Complaints.aspx?saved=1");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblComplaintID.Text = "";

    }
     
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }

        try
        {
            ComplaintsBAL objBAL = new ComplaintsBAL();
            ComplaintsBOL objBol = new ComplaintsBOL();

            objBol.ComplaintID = Util.ToInt(lblComplaintID.Text);

            string sEmp = "";
            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                string sTemp = ((Label)gRow.FindControl("lblEmpID")).Text;

                sEmp += (sEmp == "") ? sTemp : "," + sTemp;
            }

            objBol.EmployeeID = sEmp;
            objBol.ComplaintTitle = txtComplaintTitle.Text.Trim();
            objBol.Description = txtDescription.Text.Trim();
            objBol.ComplaintBy = Util.ToInt(Session["EMPID"]);
            objBol.CreatedBy = Util.ToInt(Session["EMPID"]);
            objBol.Status = "Y";
            objBol.ComplaintID = objBAL.Save(objBol);
            lblComplaintID.Text = objBol.ComplaintID.ToString();
            lblMsg.Text = "Complaint saved successfully";

            Response.Redirect("Complaints.aspx?saved=1");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnSaveEmp_Click(object sender, EventArgs e)
    {
        DataTable dTable = new DataTable();
        dTable.Columns.Add("FirstName");
        dTable.Columns.Add("MiddleName");
        dTable.Columns.Add("LastName");
        dTable.Columns.Add("EmployeeID");

        if (ViewState["EMPLIST"] == null)
        {
            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
                dTable.Rows.Add(dRow);
            }

        }
        else
        {
            dTable = (DataTable)ViewState["EMPLIST"];
        }

        foreach (GridViewRow gRow in gvEmployee.Rows)
        {
            DataRow dRow = dTable.NewRow();
            dRow[0] = ((Label)gRow.FindControl("lblName1")).Text;
            dRow[1] = ((Label)gRow.FindControl("lblName2")).Text;
            dRow[2] = ((Label)gRow.FindControl("lblName3")).Text;
            dRow[3] = ((Label)gRow.FindControl("lblEmpID")).Text;
            if (((CheckBox)gRow.FindControl("chkSelect")).Checked)
                dTable.Rows.Add(dRow);
        }

        List<int> pos = new List<int>();
        for (int i = 0; i < dTable.Rows.Count - 1; i++)
            for (int j = i + 1; j < dTable.Rows.Count; j++)
                if ("" + dTable.Rows[i]["EmployeeID"] == "" + dTable.Rows[j]["EmployeeID"])
                    pos.Add(j);

        for (int i = pos.Count - 1; i >= 0; i--)
        {
            dTable.Rows.RemoveAt(i);
        }

        ViewState["EMPLIST"] = dTable;

        gvEmpSelected.DataSource = dTable;
        gvEmpSelected.DataBind();

        if (dTable.Rows.Count == 0)
            lblTitle.Visible = false;
        else
            lblTitle.Visible = true;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", " $('#dvEmp').modal();", true);
    }
    
}