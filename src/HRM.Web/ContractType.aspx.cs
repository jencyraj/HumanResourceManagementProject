using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class ContractType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNew.Text = hrmlang.GetString("newcontracttype");
        txtContractTypeName.Attributes.Add("placeholder", hrmlang.GetString("entercontracttypename"));
        txtDescription.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            BindGrid(null);
            ClearControls();
            lblMsg.Text = string.Empty;
            lblErr.Text = string.Empty;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMsg.Text = string.Empty;
        lblErr.Text = string.Empty;
    }

    protected void gvContractType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int nCTId = 0;
            LinkButton lnkEdit = (LinkButton)((GridView)sender).Rows[e.NewEditIndex].FindControl("lnkEdit");
            nCTId = Util.ToInt(lnkEdit.CommandArgument);
            LoadControl(nCTId);
            e.Cancel = true;
        }
        catch
        {
            lblErr.Text = hrmlang.GetString("recordnotfound");
        }
    }

    protected void gvContractType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            try
            {
                ContractTypeBAL objBAL = new ContractTypeBAL();
                objBAL.Delete(Util.ToInt(e.CommandArgument));
                lblErr.Text = string.Empty;
                BindGrid(null);
                lblErr.Text = string.Empty;
                lblMsg.Text = hrmlang.GetString("contracttypedeleted");
            }
            catch(Exception ex)
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = ex.Message + hrmlang.GetString("contracttypedeleteerror");
            }
        }
    }

    protected void gvContractType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvContractType.PageIndex = e.NewPageIndex;
        BindGrid(null);
    }

    protected void gvContractType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("contracttypename");
            e.Row.Cells[1].Text = hrmlang.GetString("description");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvContractType.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvContractType.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvContractType.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvContractType.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("confirmdeletecontracttype"));
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                Save();
                BindGrid(null);
                lblErr.Text = string.Empty;
                lblMsg.Text = hrmlang.GetString("contracttypesaved");
            }
            catch
            {
                lblMsg.Text = string.Empty;
                lblErr.Text = hrmlang.GetString("contracttypesaveerror");
            }
        }
        ClearControls();
    }

    private void BindGrid(ContractTypeBOL objBOL)
    {
        gvContractType.DataSource = new ContractTypeBAL().SelectAll(objBOL);
        gvContractType.DataBind();
    }

    private void ClearControls()
    { 
        hfCTId.Value = "0";
        txtContractTypeName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        
    }

    private void LoadControl(int nCTId)
    {
        ContractTypeBOL objBOL = new ContractTypeBOL();
        objBOL = new ContractTypeBAL().SearchById(nCTId);
        if (objBOL != null)
        {
            hfCTId.Value = objBOL.CTId.ToString();
            txtContractTypeName.Text = objBOL.ContractTypeName.ToString();
            txtDescription.Text = objBOL.Description.ToString();
        }
    }

    private void Save()
    {
        ContractTypeBAL objBAL = new ContractTypeBAL();
        ContractTypeBOL objBOL = new ContractTypeBOL();
        objBOL.CTId = Util.ToInt(hfCTId.Value);
        objBOL.ContractTypeName = txtContractTypeName.Text.Trim();
        objBOL.Description = txtDescription.Text.Trim();
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        objBAL.Save(objBOL);
    }
}