using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

using HRM.BAL;
using HRM.BOL;

public partial class UploadDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            EmployeeBAL objBAL = new EmployeeBAL();
            EmployeeBOL objBOL = new EmployeeBOL();

            objBOL.EmployeeID = Util.ToInt("" + Request.QueryString["empid"]);
            objBOL = objBAL.Select(objBOL);
            string sEmp = objBOL.FirstName + ((objBOL.LastName != "") ? " " + objBOL.LastName : ((objBOL.MiddleName != "") ? " " + objBOL.MiddleName : ""));
            lblEmp.Text = sEmp;

            GetDocuments(objBOL.EmployeeID);
        }
    }

    protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDocs.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDocs.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDocs.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDocs.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletedocument"));
        }
    }

    private void GetDocuments(int EmpID)
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        DataTable dT = objBAL.SelectAllDocuments(EmpID);

        gvDocs.DataSource = dT;
        gvDocs.DataBind();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtDesc.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        string sFileName = "";

        try
        {
            if (fpDoc.HasFile)
            {
                string sFolder = "" + ConfigurationManager.AppSettings["EMPDOCS"] + Request.QueryString["empid"];
                sFolder = Server.MapPath(sFolder);
                if (!Directory.Exists(sFolder))
                    Directory.CreateDirectory(sFolder);
                sFileName = fpDoc.FileName;
                string sFilePath = sFolder + "\\" + sFileName;
                if (System.IO.File.Exists(sFilePath))
                {
                    sFileName = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + sFileName;
                }

                sFilePath = sFolder + "\\" + sFileName;
                fpDoc.SaveAs(sFilePath);

                objBAL.UploadDocument(Util.ToInt("" + Request.QueryString["empid"]), txtDesc.Text.Trim(), sFileName, User.Identity.Name);

                lblMsg.Text = hrmlang.GetString("datasaved");
                txtDesc.Text = "";

                GetDocuments(Util.ToInt("" + Request.QueryString["empid"]));
            }
            else
            {
                lblErr.Text = hrmlang.GetString("pleaseselectfile");
            }
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblFile = (Label)gRow.FindControl("lblFile");

            EmployeeBAL objBAL = new EmployeeBAL();

            objBAL.DeleteDocument(Util.ToInt(e.CommandArgument));

            lblMsg.Text = hrmlang.GetString("documentdeleted");
            string sFilePath = ConfigurationManager.AppSettings["EMPDOCS"] + Request.QueryString["empid"] + "\\" + lblFile.Text;
            if (System.IO.File.Exists(sFilePath))
                System.IO.File.Delete(sFilePath);

            GetDocuments(Util.ToInt("" + Request.QueryString["empid"]));
        }
    }
}