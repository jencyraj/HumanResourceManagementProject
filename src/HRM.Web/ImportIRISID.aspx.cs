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

public partial class ImportIRISID : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            selectUpdatedRcd();
            selectNotupdatedrcd();
           
        }
       // string[] permissions = (string[])ViewState["permissions"];
       // btnImport.Visible = (permissions[0] == "Y") ? true : false;
        btnImport.Visible = true;
        flUpload.Visible = false;
        btnUpload.Visible = false;
        gvirisrecord.Visible = false;
        grdnullview.Visible = false;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        flUpload.Visible = true;
        btnUpload.Visible = true;
    }
    protected void uploadexel(object sender, EventArgs e)
    {
        int matched=0;
        int unmatched=0;

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
        ImportIrisidBAL objCn = new ImportIrisidBAL();
       
        if (DtSet.Tables.Count > 0)
        {

            foreach (DataRow dRow in DtSet.Tables[0].Rows)
            {
                int ID = objCn.UPDATE("" + dRow["FIRSTNAME"], "" + dRow["MIDDLENAME"], "" + dRow["LASTNAME"], "" + dRow["IRISID"],User.Identity.Name);
                if (ID > 0)
                    ++matched;
                else
                    ++unmatched;
            }
            lbldisplycount.Text = matched.ToString();
            lblnullno.Text = unmatched.ToString();
        }

       
    }

    public void selectUpdatedRcd()
    {
        ImportIrisidBAL objBAL = new ImportIrisidBAL();
        ImportIrisidBOL objBOL = new ImportIrisidBOL();
        DataTable dt = new DataTable();
        string createdby = User.Identity.Name;
        String imptyp = "M";
        dt = objBAL.Select(createdby, imptyp);
        gvirisrecord.DataSource = dt;
        gvirisrecord.DataBind();
    }
    public void selectNotupdatedrcd()
    {
        ImportIrisidBAL objBAL = new ImportIrisidBAL();
        ImportIrisidBOL objBOL = new ImportIrisidBOL();
        DataTable dt = new DataTable();
        string createdby = User.Identity.Name;
        String imptyp = "U";
        dt = objBAL.Select(createdby, imptyp);
        grdnullview.DataSource = dt;
        grdnullview.DataBind();
    }
    protected void linkbuttnclick(object sender, EventArgs e)
    {
        gvirisrecord.Visible = true;
        selectUpdatedRcd();
       

    }
    protected void linkbuttclick(object sender, EventArgs e)
    {
        grdnullview.Visible = true;
        selectNotupdatedrcd();
      

    }
}