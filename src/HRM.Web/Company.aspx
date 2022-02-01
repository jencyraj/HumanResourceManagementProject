<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Company.aspx.cs" Inherits="Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="css/ekko-lightbox.css" rel="stylesheet">
    <script src="//rawgithub.com/ashleydw/lightbox/master/dist/ekko-lightbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            // delegate calls to data-toggle="lightbox"
		    $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
		        event.preventDefault();
		        return $(this).ekkoLightbox({
		            onShown: function () {
		                if (window.console) {
		                    return console.log('Checking our the events huh?');
		                }
		            }
		        });
		    });
        });
	</script>
    <section class="content-header">
        <h1> <%= hrmlang.GetString("company") %><small> <%= hrmlang.GetString("profile") %></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managecompany")%></li>
        </ol>
    </section>

<!-- Main content -->
<section class="content">
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <div class="box box-primary" id="dvpage" runat="server">
       <%-- <div class="box-header">
            <h3 class="box-title">
                Company Profile</h3>
        </div>--%>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="alert alert-danger alert-dismissable" id="dvMsg" runat="server" visible="false">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtCompany">
                        <%= hrmlang.GetString("companyname")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control txtround validate" placeholder="Enter Company Name"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtAddress"><%= hrmlang.GetString("address")%></label>
               </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control txtround validate" placeholder="Enter Address"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtCity">
                        <%= hrmlang.GetString("city")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control txtround validate" placeholder="Enter City"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtState">
                        <%= hrmlang.GetString("state")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control txtround validate" placeholder="Enter State"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlCountry">
                        <%= hrmlang.GetString("country")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control txtround" DataTextField="CountryName"
                        DataValueField="CountryID">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtZipCode">
                        <%= hrmlang.GetString("zipcode")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control txtround validate" placeholder="Enter ZipCode"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtPhone">
                        <%= hrmlang.GetString("telephone")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control txtround validate" placeholder="Enter Telephone"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtFax">
                        <%= hrmlang.GetString("fax")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtFax" runat="server" CssClass="form-control txtround" placeholder="Enter Fax"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtWebsite">
                        <%= hrmlang.GetString("website")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control txtround" placeholder="Enter Website"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtContact">
                        <%= hrmlang.GetString("contactname")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control txtround" placeholder="Enter Contact Name"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtMobile">
                        <%= hrmlang.GetString("mobile")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control txtround" placeholder="Enter Mobile"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmail">
                        <%= hrmlang.GetString("emailaddress")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txtround" placeholder="Enter Email"
                        type="email"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmpCount">
                        <%= hrmlang.GetString("employeecount")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCount" runat="server" CssClass="form-control txtround" placeholder="Enter Employee Count"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtRegn">
                        <%= hrmlang.GetString("registrationno")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtRegn" runat="server" CssClass="form-control txtround" placeholder="Enter Registration No"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtVAT">
                        <%= hrmlang.GetString("vat")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtVAT" runat="server" CssClass="form-control txtround" placeholder="Enter VAT"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtPAN">
                        <%= hrmlang.GetString("panno")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control txtround" placeholder="Enter PAN No"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtCST">
                        <%= hrmlang.GetString("cst")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtCST" runat="server" CssClass="form-control txtround" placeholder="Enter CST"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtTIN">
                        <%= hrmlang.GetString("tinno")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtTIN" runat="server" CssClass="form-control txtround" placeholder="Enter TIN No"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtESI">
                        <%= hrmlang.GetString("esino")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtESI" runat="server" CssClass="form-control txtround" placeholder="Enter ESI No"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtESI">
                        <%= hrmlang.GetString("pfno")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPF" runat="server" CssClass="form-control txtround" placeholder="Enter PF No"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="fLogo">
                        <%= hrmlang.GetString("logo")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:FileUpload ID="fLogo" runat="server" type="file" Style="display: inline" />
                    <asp:Label ID="lblLogoName" runat="server" CssClass="img-responsive" Visible="false"></asp:Label>
                    <a href="images/Logo/nologo.jpeg" data-toggle="lightbox" data-title="Logo" data-footer="" id="hlnkLogo" runat="server">
                        <img id="imgLogo" runat="server" src="~/images/Logo/nologo.jpeg"
                            class="img-responsive" style="width: 105px; height: 105px" />
                    </a>
                </div>
            </div>
            <div class="row rowmargin" style="display:none">
                <div class="col-xs-4">
                    <label for="txtDtFormat" style="display:none">
                        <%= hrmlang.GetString("dateformat")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDtFormat" runat="server" CssClass="form-control txtround" placeholder="Enter Date Format"></asp:TextBox>
                    <p class="help-block">
                        eg: dd/MM/yyyy (31/12/2014) <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dd/MMM/yyyy (31/Dec/2014)</p>
                </div>
            </div>
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validatectrl();" />
        </div>
    </div>
    </section><!-- /.content -->

    
</asp:Content>
