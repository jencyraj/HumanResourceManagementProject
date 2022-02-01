<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AssetTypes.aspx.cs" Inherits="AssetTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAssetID" runat="server" Visible="false"></asp:Label>    
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("manageassettypes")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageassettypes")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New AssetType" CausesValidation="false" onclick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvAssetType" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvAssetType_RowCommand" DataKeyNames="AssetTypeID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvAssetType_PageIndexChanging" 
                    onrowdatabound="gvAssetType_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="AssetType" HeaderText="AssetType Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("AssetTypeID") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("AssetTypeID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Asset Type?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>                          
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAssetType"><%= hrmlang.GetString("assettype")%></label> <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtAssetType"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtAssetType" runat="server" CssClass="form-control" placeholder="Enter Asset Type"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
