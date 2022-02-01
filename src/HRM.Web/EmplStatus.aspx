<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmplStatus.aspx.cs" Inherits="EmplStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("employmentstatus")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home")%></a></li>
        <li class="active"><%= hrmlang.GetString("manageemploymentstatus")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                 CausesValidation="false" OnClick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvEmplStatus" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvEmplStatus_RowCommand" DataKeyNames="EmplStatusID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    OnPageIndexChanging="gvEmplStatus_PageIndexChanging" OnRowDataBound="gvEmplStatus_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Description" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("EmplStatusID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EmplStatusID") %>'
                                    CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
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
                        <%= hrmlang.GetString("description") %></label>
                    <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

