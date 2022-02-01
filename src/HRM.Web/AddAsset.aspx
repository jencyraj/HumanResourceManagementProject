<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddAsset.aspx.cs" Inherits="AddAsset" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlCal" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAssetID" runat="server" Visible="false"></asp:Label>    
 <section class="content-header">
    <h1 id="h1" runat="server"><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home")%></a></li>
        <li class="active" id="LI1" runat="server"></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlBranch"><%= hrmlang.GetString("branch")%></label> 
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID"></asp:DropDownList>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlAssetType"><%= hrmlang.GetString("assettype")%></label>                     
                            <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="form-control" DataTextField="AssetType" DataValueField="AssetTypeID"></asp:DropDownList>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtCode"><%= hrmlang.GetString("assetcode")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Enter Asset Code"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAsset"><%= hrmlang.GetString("assetname")%></label> <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtAsset"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtAsset" runat="server" CssClass="form-control" placeholder="Enter Asset Name"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtDesc"><%= hrmlang.GetString("description")%></label>                     
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtPrice"><%= hrmlang.GetString("price")%></label>                     
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Price"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtPurchDate"><%= hrmlang.GetString("purchasedon")%></label>     
                             <uc1:ctlCal ID="ctlPurchDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="1"
                            MinYearCountFromNow="-25" />                
                            <asp:TextBox ID="txtPurchDate" runat="server" CssClass="form-control hrmhide" placeholder="Enter Purchased Date"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtExpiry"><%= hrmlang.GetString("expirydate")%></label>    
                             <uc1:ctlCal ID="ctlExpiry" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="30"
                            MinYearCountFromNow="-15" />                                 
                            <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control hrmhide" placeholder="Enter Expiry Date"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtPurchFrom"><%= hrmlang.GetString("purchasedfrom")%></label>                     
                            <asp:TextBox ID="txtPurchFrom" runat="server" CssClass="form-control" placeholder="Enter Purchased From"></asp:TextBox>
                        </div>
                    </div>
                </div>                           
                <div class="row hrmhide">
                    <div class="col-md-3">
                        <div class="form-group hrmhide">
                            <label for="txtStock"><%= hrmlang.GetString("currentstock")%></label>                     
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control hrmhide" placeholder="Enter Current Stock"></asp:TextBox>
                        </div>
                    </div>
                </div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAddInfo"><%= hrmlang.GetString("additionalinfo")%></label>                     
                            <asp:TextBox ID="txtAddInfo" runat="server" CssClass="form-control" placeholder="Enter Additional Info" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>                                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="fpImg1"><%= hrmlang.GetString("assetimage1")%></label>                     
                            <asp:FileUpload ID="fpImg1" runat="server" /><asp:HyperLink ID="lnkImage1" runat="server" data-toggle="lightbox" data-title="Image" data-footer=""  Target="_blank" ></asp:HyperLink>
                        </div>
                    </div>
                </div>                                         
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="fpImg2"><%= hrmlang.GetString("assetimage2")%></label>                     
                            <asp:FileUpload ID="fpImg2" runat="server" /><asp:HyperLink ID="lnkImage2" runat="server" data-toggle="lightbox" data-title="Image" data-footer="" Target="_blank" ></asp:HyperLink>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
