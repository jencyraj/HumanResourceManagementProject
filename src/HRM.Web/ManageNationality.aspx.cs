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

public partial class ManageNationality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "ManageNationality.aspx");
            BindLanguageDD();
            BindNationalList();
            
           
        }
        string[] permissions = (string[])ViewState["permissions"];
    }

    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNationalList();
    }
    private void BindNationalList()
    {
        NationalityBAL objCn = new NationalityBAL();
        DataTable dt = objCn.Select(ddlLang.SelectedValue);
        gvNationality.DataSource = dt;
        gvNationality.DataBind();
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
        NationalityBAL objCn = new NationalityBAL();
        foreach (GridViewRow gRow in gvNationality.Rows)
        {
            Label lblCode = (Label)gRow.FindControl("lblnational");
            TextBox txtname = (TextBox)gRow.FindControl("txtNational");
            objCn.UPDATE(lblCode.Text, txtname.Text, ddlLang.SelectedValue);
        }
    }
    protected void cancelclick(object sender, EventArgs e)
    {
        BindNationalList();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        flUpload.Visible = true;
        btnUpload.Visible = true;
    }
    protected void uploadexel(object sender, EventArgs e)
    {

        string filename = flUpload.FileName;
        if (!System.IO.Directory.Exists(Server.MapPath("uploads")))
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
        NationalityBAL objCn = new NationalityBAL();
        if (DtSet.Tables.Count > 0)
        {
            foreach (DataRow dRow in DtSet.Tables[0].Rows)
                objCn.UPDATE("" + dRow["NationalityCode"], "" + dRow["Nationality"], "" + dRow["LangCode"]);
        }

        BindNationalList();
    }

}