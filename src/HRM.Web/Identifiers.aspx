<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Identifiers.aspx.cs" Inherits="Identifiers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblIdentifierID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("company") %><small><%= hrmlang.GetString("identifiers")%></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"> <%= hrmlang.GetString("manageidentifiers")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
            <div  class="pull-right rowmargin" visible="false">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                    Text="New Department" CausesValidation="false" onclick="btnNew_Click" /></div>
            
            <div  class="pull-left dblock rowmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDesc">
                        <%= hrmlang.GetString("description") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control txtround validate" placeholder="Enter Description"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtValue">
                        <%= hrmlang.GetString("value") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtValue" runat="server" CssClass="form-control txtround validate" placeholder="Enter Value"></asp:TextBox>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validatectrl();" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
            </asp:Panel>
            <div class="col-mg-12 rowmargin rowpadleft">
                <asp:GridView ID="gvIdentifiers" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvIdentifiers_RowCommand" DataKeyNames="IdentifierID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvIdentifiers_PageIndexChanging" 
                    onrowdatabound="gvIdentifiers_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="IdentifierValue" HeaderText="Value" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("IdentifierID") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("IdentifierID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Identifier?')" CausesValidation="false"
                                     data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
