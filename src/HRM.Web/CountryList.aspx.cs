using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using HRM.BOL;
using HRM.BAL;


public partial class CountryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "countrylist.aspx");
            BindLanguageDD();
            BindCountryList();
        }
        string[] permissions = (string[])ViewState["permissions"];
        btnImport.Visible = (permissions[0] == "Y") ? true : false;
        flUpload.Visible = false;
        btnUpload.Visible = false;
    }

    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCountryList();
    }

    private void BindCountryList()
    {
        CountryBAL objCn = new CountryBAL();
        DataTable dt = objCn.Select(ddlLang.SelectedValue);
        gvCountry.DataSource = dt;
        gvCountry.DataBind();
    }

    private void BindLanguageDD()
    {
        DataTable dt = new LangDataBAL().SelectLanguage(0);
        DataView dView = dt.DefaultView;
        dView.RowFilter = "Active='Y'";
        ddlLang.DataValueField = "LangCulturename";
        ddlLang.DataTextField = "LangName";
        ddlLang.DataSource = dView.ToTable();
        ddlLang.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CountryBAL objCn = new CountryBAL();
        foreach (GridViewRow gRow in gvCountry.Rows)
        {
            Label lblCode = (Label)gRow.FindControl("lblCountry");
            TextBox txtname = (TextBox)gRow.FindControl("txtCountry");
            objCn.UPDATE(lblCode.Text, txtname.Text, ddlLang.SelectedValue);
        }
    }
    protected void cancelclick(object sender, EventArgs e)
    {
        BindCountryList();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        flUpload.Visible = true;
        btnUpload.Visible = true;
    }
    protected void uploadexel(object sender, EventArgs e)
    {
       
        string filename = flUpload.FileName;
        if(!System.IO.Directory.Exists(Server.MapPath("uploads")))
            System.IO.Directory.CreateDirectory(Server.MapPath("uploads"));
        flUpload.SaveAs(Server.MapPath("uploads") + "\\" + filename);
        string sexcelconnectionstring = "";
        sexcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("uploads") + "\\" + filename + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        System.Data.OleDb.OleDbConnection MyConnection;
        System.Data.DataSet DtSet;
        System.Data.OleDb.OleDbDataAdapter MyCommand;
        MyConnection = new System.Data.OleDb.OleDbConnection(sexcelconnectionstring);
        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        MyCommand.TableMappings.Add("Table", "TestTable");
        DtSet = new System.Data.DataSet();
        MyCommand.Fill(DtSet);
        MyConnection.Close();        
        CountryBAL objCn = new CountryBAL();
        if (DtSet.Tables.Count > 0)
        {
            foreach (DataRow dRow in DtSet.Tables[0].Rows)
            objCn.UPDATE("" + dRow["CountryCode"], "" + dRow["CountryName"], "" + dRow["LangCode"]);
        }

        BindCountryList();
       }

}