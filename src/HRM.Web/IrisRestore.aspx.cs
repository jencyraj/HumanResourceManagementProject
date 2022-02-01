using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BAL;
using HRM.BOL;
using iCAM70003SDKCLib;

public partial class IrisRestore : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        
        if (!IsPostBack)
        {
           // ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "IrisRestore.aspx");
           
            
        }
    }
   
    protected void btnshow_Click(object sender, EventArgs e)
    {
      
        DataTable DT_User = new IrisRestoreBAL().show_Backupondevice(txtIP.Text);
        gv_Backup.DataSource = DT_User;
        gv_Backup.DataBind();
        if(DT_User.Rows.Count==0)
            lblMsg.Text = hrmlang.GetString("nobackup");
    }
    private DataTable SelectUserOnBackupID(int ID)
    {
       return new IrisRestoreBAL().SelectUserOnBackupID(ID);
    }
    private string getconnection()
    { 

        return "";
    }
    protected void gv_Backup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lbl_IPAddress = (Label)row.FindControl("lbl_IPAddress");
            LinkButton lnkrestore = (LinkButton)row.FindControl("lnkrestore");
            string BackupID = lnkrestore.CommandArgument;
            Label lbl_SecurityId = (Label)row.FindControl("lbl_SecurityId");
            Label lbl_Password = (Label)row.FindControl("lbl_Password");
            Label lbl_Username = (Label)row.FindControl("lbl_Username");

            IrisRestoreBAL objBAL = new IrisRestoreBAL();
            objBAL.Connect_Iris(Util.ToInt(BackupID), lbl_IPAddress.Text, lbl_SecurityId.Text, lbl_Username.Text,lbl_Password.Text);
           //DataTable DTUser=  SelectUserOnBackupID(Util.ToInt(BackupID));
    }
    }
}