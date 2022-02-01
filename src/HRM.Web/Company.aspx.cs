using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

using HRM.BOL;
using HRM.BAL;

public partial class Company : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErr.Text = "";
        lblMsg.Text = "";
        dvMsg.Visible = false;
        txtCompany.Attributes.Add("placeholder", hrmlang.GetString("entercompanyname"));
        txtAddress.Attributes.Add("placeholder", hrmlang.GetString("enteraddress"));
        txtCity.Attributes.Add("placeholder", hrmlang.GetString("entercity"));
        txtState.Attributes.Add("placeholder", hrmlang.GetString("enterstate"));
        txtZipCode.Attributes.Add("placeholder", hrmlang.GetString("enterzipcode"));
        txtPhone.Attributes.Add("placeholder", hrmlang.GetString("entertelephone"));
        txtFax.Attributes.Add("placeholder", hrmlang.GetString("enterfax"));
        txtWebsite.Attributes.Add("placeholder", hrmlang.GetString("enterwebsite"));
        txtContact.Attributes.Add("placeholder", hrmlang.GetString("entercontactname"));
        txtMobile.Attributes.Add("placeholder", hrmlang.GetString("entermobile"));
        txtEmail.Attributes.Add("placeholder", hrmlang.GetString("enteremail"));
        txtEmpCount.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecount"));
        txtRegn.Attributes.Add("placeholder", hrmlang.GetString("enterregistrationno"));
        txtVAT.Attributes.Add("placeholder", hrmlang.GetString("entervat"));
        txtPAN.Attributes.Add("placeholder", hrmlang.GetString("enterpanno"));
        txtCST.Attributes.Add("placeholder", hrmlang.GetString("entercst"));
        txtTIN.Attributes.Add("placeholder", hrmlang.GetString("entertinno"));
        txtESI.Attributes.Add("placeholder", hrmlang.GetString("enteresino"));
        txtPF.Attributes.Add("placeholder", hrmlang.GetString("enterpfno"));
        txtDtFormat.Attributes.Add("placeholder", hrmlang.GetString("enterdateformat"));
        btnSave.Text = hrmlang.GetString("save");

        if (!IsPostBack)
        {
            imgLogo.Src = "images/Logo/nologo.jpeg";
            GetCountryList();
            GetCompanyDetails();
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "company.aspx");
        }
        string[] permissions = (string[])ViewState["permissions"];
        if (permissions[0] != "Y" && permissions[0] != "Y")
        {
            btnSave.Visible = false;
            fLogo.Visible = false;
            foreach (Control ctl in dvpage.Controls)
            {
                if (ctl is TextBox)
                {
                    TextBox txt = (TextBox)ctl;
                    txt.Enabled = false;
                }
                if (ctl is DropDownList)
                {
                    DropDownList txt = (DropDownList)ctl;
                    txt.Enabled = false;
                }
            }
        }
    }

    private void GetCountryList()
    {
        CountryBAL objBAL = new CountryBAL();
        ddlCountry.DataSource = objBAL.Select("" + Session["LanguageId"]);
        ddlCountry.DataBind();
    }

    private void GetCompanyDetails()
    {
        try
        {
            OrganisationBAL objBAL = new OrganisationBAL();
            OrganisationBOL objBOL = objBAL.Select();
            if (objBOL != null)
            {
                Session["COMPANYID"] = objBOL.CompanyID.ToString();
                lblCompanyID.Text = objBOL.CompanyID.ToString();
                txtCompany.Text = objBOL.CompanyName;
                txtAddress.Text = objBOL.Address;
                txtCity.Text = objBOL.City;
                txtState.Text = objBOL.State;
                ddlCountry.SelectedValue = objBOL.CountryID.ToString();
                txtZipCode.Text = objBOL.ZipCode;
                txtPhone.Text = objBOL.Telephone;
                txtMobile.Text = objBOL.Mobile;
                txtFax.Text = objBOL.Fax;
                txtWebsite.Text = objBOL.Website;
                txtEmail.Text = objBOL.Email;
                txtContact.Text = objBOL.ContactName;
                txtEmpCount.Text = objBOL.EmployeeCount.ToString();
                txtRegn.Text = objBOL.RegistrationNo;
                txtVAT.Text = objBOL.VAT;
                txtPAN.Text = objBOL.PANNO;
                txtCST.Text = objBOL.CST;
                txtTIN.Text = objBOL.TIN;
                txtESI.Text = objBOL.ESI;
                txtPF.Text = objBOL.PF;
                lblLogoName.Text = objBOL.LogoName;
                if (objBOL.LogoName != "")
                    imgLogo.Src =   "images/Logo/" + objBOL.LogoName;
                //imgLogo.Src = ConfigurationManager.AppSettings["ROOTURL"] + "images/Logo/" + objBOL.LogoName;
                hlnkLogo.HRef = imgLogo.Src;
                txtDtFormat.Text = objBOL.DateFormat;
            }
        }
        catch (Exception ex)
        {
            dvMsg.Visible = true;
            lblErr.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            OrganisationBAL objBAL = new OrganisationBAL();
            OrganisationBOL objBOL = new OrganisationBOL();

            objBOL.CompanyID = Util.ToInt(lblCompanyID.Text);
            objBOL.CompanyName = txtCompany.Text.Trim();
            objBOL.Address = txtAddress.Text.Trim();
            objBOL.City = txtCity.Text.Trim();
            objBOL.State = txtState.Text.Trim();
            objBOL.CountryID = int.Parse(ddlCountry.SelectedValue);
            objBOL.ZipCode = txtZipCode.Text.Trim();
            objBOL.Telephone = txtPhone.Text.Trim();
            objBOL.Mobile = txtMobile.Text.Trim();
            objBOL.Fax = txtFax.Text.Trim();
            objBOL.Website = txtWebsite.Text.Trim();
            objBOL.Email = txtEmail.Text.Trim();
            objBOL.ContactName = txtContact.Text.Trim();
            objBOL.EmployeeCount = Util.ToInt(txtEmpCount.Text.Trim());
            objBOL.RegistrationNo = txtRegn.Text.Trim();
            objBOL.VAT = txtVAT.Text.Trim();
            objBOL.PANNO = txtPAN.Text.Trim();
            objBOL.CST = txtCST.Text.Trim();
            objBOL.TIN = txtTIN.Text.Trim();
            objBOL.ESI = txtESI.Text.Trim();
            objBOL.PF = txtPF.Text.Trim();
            objBOL.LogoName = lblLogoName.Text;
            objBOL.DateFormat = txtDtFormat.Text.Trim();
            objBOL.Status = "Y";
            UploadLogo();
            objBOL.LogoName = lblLogoName.Text;
            imgLogo.Src = ConfigurationManager.AppSettings["ROOTURL"] + "images/Logo/" + objBOL.LogoName;
            hlnkLogo.HRef = imgLogo.Src;
            lblCompanyID.Text = objBAL.Save(objBOL).ToString();
            lblMsg.Text = hrmlang.GetString("companyprofilesaved");
            dvMsg.Visible = true;
        }
        catch (Exception ex)
        {
            dvMsg.Visible = true;
            lblErr.Text += ex.Message;
        }
    }

    private string UploadLogo()
    {
        try
        {
            if (fLogo.HasFile)
            {
                if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["LOGOFILEPATH"]) + lblLogoName.Text))
                    System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["LOGOFILEPATH"]) + lblLogoName.Text);
                fLogo.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["LOGOFILEPATH"]) + fLogo.FileName);
                lblLogoName.Text = fLogo.FileName;
            }
        }
        catch (Exception ex)
        {
            dvMsg.Visible = true;
            lblErr.Text = ex.Message.ToString();
        }
        return lblLogoName.Text;
    }
}