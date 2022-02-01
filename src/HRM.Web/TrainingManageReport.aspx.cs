using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class TrainingManagementReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "TrainingManageReport.aspx");
            //GetRoles();
            GetTrainingTypes();
       

            SetData();
        }

        string[] permissions = (string[])ViewState["permissions"];
      

    }
    private void SetData()
    {
        lblTrainingType.Text = hrmlang.GetString("trainingtypes");
        lblDesc.Text = hrmlang.GetString("description");
        lnkSearch.Text = hrmlang.GetString("search");
     
        txtSearch.Attributes.Add("placeholder", hrmlang.GetString("searchtext"));
    }

    private void GetTrainingTypes()
    {
        TrainingTypeBAL objBAL = new TrainingTypeBAL();
        ddlStype.DataSource = objBAL.SelectAll(0);
        ddlStype.DataBind();
        ddlStype.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }
    private void GetTraining()
    {
        //TrainingBAL objBAL = new TrainingBAL();
        //TrainingBOL objCy = new TrainingBOL();

        //objCy.trainingtype = Util.ToInt(ddlStype.SelectedValue);
        //objCy.title = txtSearch.Text;

        //gvTraining.DataSource = objBAL.SelectAll(objCy);
        //gvTraining.DataBind();

        //if (gvTraining.Rows.Count == 0)
        //    lblErr.Text = hrmlang.GetString("nodatafound");// "No competencies found";

        string sParams = "&sp1=" + ddlStype.SelectedValue + "&sp2=" + txtSearch.Text;
        string url = "HRMReports.aspx?rptname=TrainingManageRpt&printtype=" + rbtnPrint.SelectedValue + sParams + "&rptcase=TRAININGRPT";
        string s = "window.open('" + url + "', 'popup_window', 'top=0,resizable=yes');";
        //  string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


    }
   

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        GetTraining();
    }

}