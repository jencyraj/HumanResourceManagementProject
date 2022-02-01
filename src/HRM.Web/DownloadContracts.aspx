<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DownloadContracts.aspx.cs" Inherits="DownloadContracts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("contractspolicies") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("contractspolicies") %></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="panel-group" id="accordion">
                    <asp:Repeater ID="rptrDownloads" runat="server" OnItemDataBound="rptrDownloads_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href='#collapse1' runat="server"
                                            id="lnkType">
                                            <%# Eval("ContractTypeName") %></a>
                                    </h4>
                                </div>
                                <div id='collapse1' class="panel-collapse collapse" runat="server">
                                    <div class="panel-body">
                                        <asp:GridView ID="gvDownloads" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDownloads_RowDataBound"
                                            GridLines="None" ShowHeader="false" CssClass="table  table-striped dataTable" Width="100%">
                                            <Columns>
                                             <asp:TemplateField ItemStyle-Width="90%">
                                              <ItemTemplate>
                                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                             </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkDocName" runat="server" Text="Click Here" Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
