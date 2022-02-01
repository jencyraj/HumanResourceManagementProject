using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class ManageContracts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetContractTypes();
            ClearControls();
            LoadControl(Util.ToInt(Request.QueryString["id"]));
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                Save();
               Response.Redirect("ManageContracts.aspx?saved=1");
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message.ToString();
            }
        }
        ClearControls();
    }

    private void GetContractTypes()
    {
        ddlType.DataSource = new ContractTypeBAL().SelectAll(null);
        ddlType.DataBind();
        ddlType.SelectedIndex = -1;
    }

    private void ClearControls()
    {
        hfContractId.Value = "0";
        txtTitle.Text = string.Empty;
        txtDescription.Text = string.Empty;
        ddlType.SelectedIndex = -1;
    }

    private void LoadControl(int nContractId)
    {
        if (nContractId == 0) return;
        ContractsBOL objBOL = new ContractsBOL();
        objBOL = new ContractsBAL().SearchById(nContractId);
        if (objBOL != null)
        {
            hfContractId.Value = objBOL.ContractId.ToString();
            txtTitle.Text = objBOL.Title.ToString();
            txtDescription.Text = objBOL.Description.ToString();
            lnkDoc.Text = objBOL.DocName;
            lnkDoc.NavigateUrl = "~/images/contracts/" + objBOL.ContractId + "/" + objBOL.DocName;
            ddlType.SelectedValue = objBOL.ContractTypeID.ToString();
        }
    }

    private void Save()
    {
        ContractsBAL objBAL = new ContractsBAL();
        ContractsBOL objBOL = new ContractsBOL();
        objBOL.ContractId = Util.ToInt(hfContractId.Value);
        objBOL.Title = txtTitle.Text.Trim();
        objBOL.ContractTypeID = Util.ToInt(ddlType.SelectedValue);
        objBOL.Description = txtDescription.Text.Trim();
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ContractId = objBAL.Save(objBOL);
        hfContractId.Value = objBOL.ContractId.ToString();
        if (UploadDocument() != "")
        {
            objBOL.DocName = fpDoc.FileName;
            objBAL.Save(objBOL);
        }
    }

    private string UploadDocument()
    {
        if (fpDoc.HasFile)
        {
            string sPath = "images\\contracts\\" + hfContractId.Value + "\\";

            if (lnkDoc.Text != "")
            {
                if (System.IO.File.Exists(Server.MapPath(sPath) + lnkDoc.Text))
                    System.IO.File.Delete(Server.MapPath(sPath) + lnkDoc.Text);
            }

            if (!System.IO.Directory.Exists(Server.MapPath(sPath)))
                System.IO.Directory.CreateDirectory(Server.MapPath(sPath));

            fpDoc.SaveAs(Server.MapPath(sPath) + fpDoc.FileName);
            lnkDoc.Text = fpDoc.FileName;
        }

        return lnkDoc.Text;
    }
}