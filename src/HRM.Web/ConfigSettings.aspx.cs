using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class ConfigSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    private void BindData()
    {
        DataTable dTable = new SettingsBAL().SelectAll();
        foreach (DataRow dRow in dTable.Rows)
        {
            ListItem lItem = chkSettings.Items.FindByValue("" + dRow["ConfigCode"]);
            if (lItem != null)
            {
                lItem.Selected = ("" + dRow["ConfigValue"] == "Y") ? true : false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < chkSettings.Items.Count; i++)
            {
                new SettingsBAL().Save(chkSettings.Items[i].Value, (chkSettings.Items[i].Selected) ? "Y" : "N");
            }
            BindData();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsignin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signin.aspx");
    }
}