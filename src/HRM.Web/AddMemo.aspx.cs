using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HRM.BOL;
using HRM.BAL;

public partial class AddMemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            lblMemoID.Text = Request.QueryString["id"];

        //    string[] permissions = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "addmemo.aspx");
            //if (permissions[0] != "Y")
            //{
            //    if (permissions[3] != "Y")
            //    {
            //        Util.ShowNoPermissionPage();
            //        return;
            //    }
            //    else 
            //    {
            //        btnSave.Visible = false;
            //        btnSearch.Enabled = false;
            //        btnCancel.Visible = false;
            //    }
            //}

            btnSaveEmp.Text = btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
          //  txtStart.Attributes.Add("placeholder", hrmlang.GetString("entermemodate"));
           
            lblTitle.Text = hrmlang.GetString("selectedemployees");

            string sLang = "" + Session["LanguageId"];
           
            
            txtfName.Attributes.Add("placeholder", hrmlang.GetString("enterfname"));
            txtmName.Attributes.Add("placeholder", hrmlang.GetString("entermname"));
            txtlName.Attributes.Add("placeholder", hrmlang.GetString("enterlname"));                   

            GetMemoDetails();
        }

    }


    private void GetMemoDetails()
    {
        if ("" + Request.QueryString["id"] == "") return;
        MemoBOL memo = new MemoBOL();
        memo.MemoID = Util.ToInt(Request.QueryString["id"]);
        DataTable dTable = new MemoBAL().SelectAll(memo);

     //   DataTable dTable = dSet.Tables[0];

        if (dTable.Rows.Count > 0)
        {
            DataRow dRow = dTable.Rows[0];
           

           // txtStart.SelectedCalendareDate = Util.ToDateTime(Util.RearrangeDateTime(dRow["StartDate"]));
            txtsubject.Text = "" + dRow["SUBJECT"];
            txtDesc.Text = "" + dRow["DESCRIPTION"];

        }

        gvEmpSelected.DataSource = new MemoBAL().SelectEmployeeByMemoID(Util.ToInt(Request.QueryString["id"]));
        gvEmpSelected.DataBind();

       

        if ("" + Request.QueryString["view"] == "1")
        {
            btnSave.Visible = false;
            btnSearch.Enabled = false;
            btnCancel.Visible = false;
            gvEmpSelected.Columns[1].Visible = false;
        }   
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            MemoBOL objBol = new MemoBOL();

            objBol.MemoID = Util.ToInt(lblMemoID.Text);
            objBol.Description = txtDesc.Text.Trim();
            objBol.MemoFrom = Util.ToInt(Session["EMPID"]);
            objBol.Subject = txtsubject.Text.Trim();
            if (Util.ToInt(Request.QueryString["id"])<=0)
            {
                objBol.Status = "P";
            }
            objBol.CreatedBy = Util.ToInt(Session["EMPID"]);
            string sEmp = "";
            foreach (GridViewRow gRow in gvEmpSelected.Rows)
            {
                string sTemp = ((Label)gRow.FindControl("lblEmpID")).Text;

                sEmp += (sEmp == "") ? sTemp : "," + sTemp;
            }
            objBol.MemoTo = sEmp;
            objBol.MemoID = new MemoBAL().Save(objBol);

            lblMsg.Text = hrmlang.GetString("memosaved");
            if ("" + Request.QueryString["id"] == "")
                Clear();

            Response.Redirect("Memo.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblMemoID.Text = "";
        txtDesc.Text = "";
        txtsubject.Text = "";
        lblTitle.Visible = false;
        gvEmpSelected.DataSource = null;
        gvEmpSelected.DataBind();
        ViewState["EMPLIST"] = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddMemo.aspx");
    }

  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();

        objBOL.EmpStatus = "C";

        objBOL.FirstName = txtfName.Text.Trim();
        objBOL.MiddleName = txtmName.Text.Trim();
        objBOL.LastName = txtlName.Text.Trim();

        DataTable dtEmp = objBAL.Search(objBOL);     

        DataView dView = new DataView(dtEmp);

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
}