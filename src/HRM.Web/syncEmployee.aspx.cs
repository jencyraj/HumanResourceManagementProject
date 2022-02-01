using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddToIris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSyncB.Text = hrmlang.GetString("synchronize");
        btnsyncT.Text = hrmlang.GetString("synchronize");
        if (!IsPostBack)
        {
            //ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "jobrequests.aspx");
           // GetBranches();
            EmployeeBAL objBAL = new EmployeeBAL();
            EmployeeBOL objBOL = new EmployeeBOL();

            objBOL.EmployeeID = Util.ToInt("" + Request.QueryString["empid"]);
            objBOL = objBAL.Select(objBOL);
         
            string sEmp = objBOL.FirstName + " " + objBOL.MiddleName + " " + objBOL.LastName;
            lblEmp.Text = sEmp.Trim().Replace("  ", " ");
          lblhidden.Text=  objBOL.IRISID;
          lblbranchid.Text = objBOL.BranchID.ToString();
            GetIrisDevices(objBOL.BranchID);
            
        }
    }

    protected void gvIrisData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("IPAddress");
            e.Row.Cells[1].Text = hrmlang.GetString("UserName");
            e.Row.Cells[2].Text = hrmlang.GetString("doorname");
            //e.Row.Cells[2].Text = hrmlang.GetString("branch");
            //e.Row.Cells[3].Text = hrmlang.GetString("department");
            //e.Row.Cells[4].Text = hrmlang.GetString("designation");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvIrisData.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvIrisData.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvIrisData.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvIrisData.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

    protected void gvIrisData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      
        gvIrisData.PageIndex = e.NewPageIndex;
    }

    protected void btnsync_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        { return; }
        sync((lblhidden.Text).ToString(), int.Parse(lblbranchid.Text));
    }

    protected void btnsyncT_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        sync((lblhidden.Text).ToString(), int.Parse(lblbranchid.Text));
       
    }
    private void sync(string useriD,int branchid)
    {
        try
        {
            irishbackupBAL objBAL = new irishbackupBAL();
            DataTable dt = objBAL.SelectAllMaster(branchid);
            string[] Mcredentials = new string[5];
            Mcredentials[0] = dt.Rows[0]["IrisId"].ToString();
            Mcredentials[1] = dt.Rows[0]["IPAddress"].ToString();
            Mcredentials[2] = dt.Rows[0]["SecurityId"].ToString();
            Mcredentials[3] = dt.Rows[0]["UserName"].ToString();
            Mcredentials[4] = dt.Rows[0]["Password"].ToString();
            //string irisIdm = dt.Rows[0]["IrisId"].ToString();
            //string ipAddressM = dt.Rows[0]["IPAddress"].ToString();
            //string securityIdM = dt.Rows[0]["SecurityId"].ToString();
            //string userNameM = dt.Rows[0]["UserName"].ToString();
            //string passwordM = dt.Rows[0]["Password"].ToString();
           
           // string errorMsgM = objBAL.SyncIRISmaster(ipAddressM, irisIdm, userNameM, passwordM, securityIdM, useriD);

            DataTable dTable = new DataTable();

            dTable.Columns.Add("IPAddresss");
            dTable.Columns.Add("UserName");
            dTable.Columns.Add("SecurityId");
            dTable.Columns.Add("Password");
            dTable.Columns.Add("IRisId");

            bool isChecked = false;

            foreach (GridViewRow gRow in gvIrisData.Rows)
            {
                DataRow dRow = dTable.NewRow();
                dRow[0] = ((Label)gRow.FindControl("lblIP")).Text;
                dRow[1] = ((Label)gRow.FindControl("lblUserName")).Text;
                dRow[2] = ((Label)gRow.FindControl("lblSecurityId")).Text;
                dRow[3] = ((Label)gRow.FindControl("lblPassword")).Text;
                dRow[4] = ((Label)gRow.FindControl("lblIrisId")).Text;

                if (((CheckBox)gRow.FindControl("chkSelect")).Checked)
                {
                    isChecked = true;
                  
                    dTable.Rows.Add(dRow);
                    string[] Dcredentials = new string[5];
                    Dcredentials[0] = dRow["IrisId"].ToString();
                    Dcredentials[1] = dRow["IPAddresss"].ToString();
                    Dcredentials[2] = dRow["SecurityId"].ToString();
                    Dcredentials[3] = dRow["UserName"].ToString();
                    Dcredentials[4] = dRow["Password"].ToString();
                    string errmsg;
                    objBAL.connectMD(dTable, Mcredentials, Dcredentials, useriD,out errmsg);
                    if (errmsg == null)
                    {
                        lblMsg.Text = "Succesfull";
                    }
                    else
                    {
                        lblMsg.Text = "Error";
                    }
                    

                  //  string errorMsg = objBAL.SyncIRISDestination(ipAddress, irisId, userName, password, securityId, useriD);

                    lblhidden.Text = "";
                }


              
            }

            if (isChecked == false)
            {
                lblErr.Text = "Please select a device to synchronize";
                lblMsg.Text = string.Empty;
                return;
            }

        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
            lblMsg.Text = string.Empty;
        }
    }

   

    public void GetIrisDevices(int brachid)
    {
        IrisDataBAL objBAL = new IrisDataBAL();
        irishbackupBAL objirisBAL = new irishbackupBAL();
        DataTable dt = new DataTable();
        dt = objBAL.SelectAll(Util.ToInt(brachid));
        DataView dView = dt.DefaultView;
        dView.RowFilter = "MasterDevice='N'";
        gvIrisData.DataSource = dView.ToTable();
      
        gvIrisData.DataBind();
    }

    
}