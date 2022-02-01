using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class connectdb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Data Source=ANTZ18\SQLEXPRESS;Initial Catalog=HRMIANTZPROD;User ID=sa;Password=admin@antz

        if (Util.ToInt(Session["ROLEID"]) != Util.ToInt(System.Configuration.ConfigurationManager.AppSettings["SuperAdminRoleId"]))
            Util.ShowNoPermissionPage();

        if (!IsPostBack)
        {

            txtServer.Attributes.Add("placeholder", hrmlang.GetString("dbserver"));
            txtDbName.Attributes.Add("placeholder", hrmlang.GetString("dbname"));
            txtUserID.Attributes.Add("placeholder", hrmlang.GetString("userid"));
            txtPassword.Attributes.Add("placeholder", hrmlang.GetString("password"));
            btnSave.Text = hrmlang.GetString("save");

            string sConnValue = "" + ConfigurationManager.AppSettings["DBCONN"];

            if (sConnValue != "")
            {
                string[] sVAL = sConnValue.Split(';');
                txtServer.Text = sVAL[0].Split('=')[1];
                txtDbName.Text = sVAL[1].Split('=')[1];
                txtUserID.Text = sVAL[2].Split('=')[1];
                txtPassword.Text = sVAL[3].Split('=')[1];
            }

            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Attributes.Add("value", txtPassword.Text);
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sConnValue = "" + ConfigurationManager.AppSettings["DBCONN"];

        string sConnstring = "Data Source=" + txtServer.Text.Trim() + ";";
        sConnstring += "Initial Catalog=" + txtDbName.Text.Trim() + ";";
        sConnstring += "User ID=" + txtUserID.Text.Trim() + ";";
        sConnstring += "Password=" + txtPassword.Text.Trim();

        Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
        //Modifying the AppKey from AppValue to AppValue1
        webConfigApp.AppSettings.Settings["DBCONN"].Value = sConnstring;
        //Save the Modified settings of AppSettings.
        webConfigApp.Save();

        Util.Log("Database connection changed.Previous Connectionstring was " + sConnValue);
    }
}