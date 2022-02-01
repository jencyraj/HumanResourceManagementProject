using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class dbbackup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        txtPath.Attributes.Add("placeholder", hrmlang.GetString("enterpath"));
        btnSave.Text = hrmlang.GetString("backup");
        if (!IsPostBack)
            txtPath.Text = "" + System.Configuration.ConfigurationManager.AppSettings["DBBACKUPPATH"];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (System.IO.Directory.Exists(txtPath.Text))
            {
                DBBackUpBAL objBAL = new DBBackUpBAL();
                lblMsg.Text = hrmlang.GetString("dbbackupcompleted") + "." + hrmlang.GetString("pleasecheck") + " " + 
                    txtPath.Text.Replace("/", "\\") + "\\" + objBAL.DBBACKUP(txtPath.Text);
            }
            else
                lblErr.Text = hrmlang.GetString("inputvalidpath");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
}