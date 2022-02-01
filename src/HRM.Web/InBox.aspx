<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InBox.aspx.cs" Inherits="InBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("messages") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("readmessages") %></li>
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
                    <asp:GridView ID="rptrMsg" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                        AllowPaging="true" PageSize="20" OnRowDataBound="rptrMsg_RowDataBound" OnPageIndexChanging="rptrMsg_PageIndexChanging"
                        BorderStyle="None" GridLines="None" Width="100%" 
                        onrowcommand="rptrMsg_RowCommand">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href='#collapse1' runat="server"
                                                    id="lnkType">
                                                    <%# Eval("mailsubject") %></a>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o fr-right" CommandArgument='<%# Eval("EMID") %>'
                                                 CommandName="DEL" data-toggle="tooltip"></asp:LinkButton>
                                            </h4>
                                        </div>
                                        <div id='collapse1' class="panel-collapse collapse" runat="server">
                                            <div class="panel-body">
                                                <span class='<%= sClass %>'>
                                                    <%= hrmlang.GetString("sentby") %>
                                                    : <b>
                                                        <%# Eval("fullname") %></b>
                                                    <%= hrmlang.GetString("on") %>
                                                    <b>
                                                        <%# Eval("sentdate") %></b></span><br />
                                                <%# Eval("mailmsg") %>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
