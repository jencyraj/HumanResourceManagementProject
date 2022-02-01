using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AddAsset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        txtCode.Attributes.Add("placeholder", hrmlang.GetString("enterassetcode"));
        txtAsset.Attributes.Add("placeholder", hrmlang.GetString("enterassetname"));
        txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
        txtPrice.Attributes.Add("placeholder", hrmlang.GetString("enterprice"));
        //  txtPurchDate.Attributes.Add("placeholder", hrmlang.GetString("enterpurchaseddate"));
        //  txtExpiry.Attributes.Add("placeholder", hrmlang.GetString("enterexpirydate"));
        txtPurchFrom.Attributes.Add("placeholder", hrmlang.GetString("enterpurchasedfrom"));
        txtStock.Attributes.Add("placeholder", hrmlang.GetString("entercurrentstock"));
        txtAddInfo.Attributes.Add("placeholder", hrmlang.GetString("enteradditionalinfo"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        h1.InnerText = LI1.InnerText = hrmlang.GetString("addnewasset");

        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "assets.aspx");
            GetBranches();
            GetAssetTypes();
            GetAssetDetails();
        }
    }

    private void GetBranches()
    {
        OrgBranchesBAL objBr = new OrgBranchesBAL();
        DataTable dt = objBr.SelectAll(Util.ToInt(Session["COMPANYID"]));
        ddlBranch.DataSource = dt;
        ddlBranch.DataBind();
    }

    private void GetAssetTypes()
    {
        AssetTypesBAL objBr = new AssetTypesBAL();
        DataTable dt = objBr.SelectAll(0);
        ddlAssetType.DataSource = dt;
        ddlAssetType.DataBind();
    }

    private void GetAssetDetails()
    {

        AssetsBOL objBOL = new AssetsBOL();
        AssetsBAL objBAL = new AssetsBAL();

        lblAssetID.Text = "" + Request.QueryString["id"];
        if (Util.ToInt(lblAssetID.Text) == 0) return;
        h1.InnerText = LI1.InnerText = hrmlang.GetString("editassetdetails");
        objBOL = objBAL.SelectByID(Util.ToInt(lblAssetID.Text));
        txtCode.Text = objBOL.AssetCode;
        txtAsset.Text = objBOL.AssetName;
        txtPrice.Text = objBOL.AssetPrice.ToString();
        txtDesc.Text = objBOL.AssetDesc;
        txtAddInfo.Text = objBOL.AdditionalInfo;
        /* txtExpiry.Text = objBOL.ExpiryDate;
         txtPurchDate.Text = objBOL.PurchasedOn;*/
        txtPurchFrom.Text = objBOL.PurchasedFrom;

        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("ar"))
        {
            if ("" + objBOL.PurchasedOn != "")
            {
                string sDate = ConvertDates.GregToHijri(Util.RearrangeDateTime(objBOL.PurchasedOn));
                ctlPurchDate.SelectedCalendareDate = Convert.ToDateTime(sDate);
            }
            if ("" + objBOL.ExpiryDate != "")
            {
                string sDate = ConvertDates.GregToHijri(Util.RearrangeDateTime(objBOL.ExpiryDate));
                ctlExpiry.SelectedCalendareDate = Convert.ToDateTime(sDate);
            }
        }
        else
        {
            if ("" + objBOL.PurchasedOn != "")
                ctlPurchDate.SelectedCalendareDate = Util.ToDateTime(Util.CleanString(objBOL.PurchasedOn));
            if ("" + objBOL.ExpiryDate != "")
                ctlExpiry.SelectedCalendareDate = Util.ToDateTime(Util.CleanString(objBOL.ExpiryDate));
        }

        txtStock.Text = objBOL.AssetCount;
        ddlAssetType.SelectedValue = objBOL.AssetTypeID.ToString();
        ddlBranch.SelectedValue = objBOL.BranchID.ToString();
        lnkImage1.Text = objBOL.AssetImage1;
        lnkImage2.Text = objBOL.AssetImage2;

        string sPath = "~//" + ConfigurationManager.AppSettings["ASSETPHOTO"].Replace("\\", "/") + lblAssetID.Text + "//";
        if (lnkImage1.Text != "")
            lnkImage1.NavigateUrl = sPath + lnkImage1.Text;
        if (lnkImage2.Text != "")
            lnkImage2.NavigateUrl = sPath + lnkImage2.Text;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        if (ddlBranch.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(btnSave.GetType(), "onclick", "alert('Please select Branch');", true);
            return;
        }
        if (ddlAssetType.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(btnSave.GetType(), "onclick", "alert('Please select Asset Type');", true);
            return;
        }
        try
        {
            AssetsBAL objBAL = new AssetsBAL();
            AssetsBOL objBol = new AssetsBOL();

            objBol.AssetID = Util.ToInt(lblAssetID.Text);
            objBol.BranchID = Util.ToInt(ddlBranch.SelectedValue);
            objBol.AssetTypeID = Util.ToInt(ddlAssetType.SelectedValue);
            objBol.AssetCode = txtCode.Text.Trim();
            objBol.AssetName = txtAsset.Text.Trim();
            objBol.AssetDesc = txtDesc.Text.Trim();
            objBol.AssetPrice = Util.ToDecimal(txtPrice.Text.Trim());
            objBol.PurchasedFrom = txtPurchFrom.Text.Trim();
            objBol.PurchasedOn = ctlPurchDate.getGregorianDateText;
            objBol.ExpiryDate = ctlExpiry.getGregorianDateText;// txtExpiry.Text.Trim();
            objBol.AssetCount = txtStock.Text.Trim();
            objBol.AdditionalInfo = txtAddInfo.Text.Trim();
            objBol.CreatedBy = User.Identity.Name;
            objBol.AssetID = objBAL.Save(objBol);
            lblAssetID.Text = objBol.AssetID.ToString();
            UploadImages(objBol);
            lblMsg.Text = hrmlang.GetString("assetsaved");
            Clear();
            Response.Redirect("Assets.aspx?saved=1");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void UploadImages(AssetsBOL objBOL)
    {
        AssetsBAL objBAL = new AssetsBAL();

        if (fpImg1.HasFile)
        {
            string sPath = ConfigurationManager.AppSettings["ASSETPHOTO"];
            sPath += lblAssetID.Text + "\\";
            if (System.IO.File.Exists(Server.MapPath(sPath) + lnkImage1.Text))
                System.IO.File.Delete(Server.MapPath(sPath) + lnkImage1.Text);
            if (!System.IO.Directory.Exists(Server.MapPath(sPath)))
                System.IO.Directory.CreateDirectory(Server.MapPath(sPath));
            fpImg1.SaveAs(Server.MapPath(sPath) + fpImg1.FileName);
            lnkImage1.Text = fpImg1.FileName;
        }
        if (fpImg2.HasFile)
        {
            string sPath = ConfigurationManager.AppSettings["ASSETPHOTO"];
            sPath = Server.MapPath(sPath);
            sPath += lblAssetID.Text + "\\";
            if (System.IO.File.Exists(Server.MapPath(sPath) + lnkImage2.Text))
                System.IO.File.Delete(Server.MapPath(sPath) + lnkImage2.Text);
            if (!System.IO.Directory.Exists(Server.MapPath(sPath)))
                System.IO.Directory.CreateDirectory(Server.MapPath(sPath));
            fpImg2.SaveAs(Server.MapPath(sPath) + fpImg2.FileName);
            lnkImage2.Text = fpImg2.FileName;
        }

        objBOL.AssetImage1 = lnkImage1.Text;
        objBOL.AssetImage2 = lnkImage2.Text;

        objBAL.Save(objBOL);
    }
    private void Clear()
    {
        lblAssetID.Text = "";
        ddlAssetType.ClearSelection();
        ddlBranch.ClearSelection();
        txtCode.Text = string.Empty;
        txtAsset.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtPrice.Text = string.Empty;
        txtPurchDate.Text = string.Empty;
        txtExpiry.Text = string.Empty;
        txtPurchFrom.Text = string.Empty;
        txtStock.Text = string.Empty;
        txtAddInfo.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }

}