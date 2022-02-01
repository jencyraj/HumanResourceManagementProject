<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmployeeTransfer.aspx.cs" Inherits="EmployeeTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblTransferID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1 id="h1" runat="server">
            <%= hrmlang.GetString("employeetransfer")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active" id="LI1" runat="server"><%= hrmlang.GetString("employeetransfer")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvTransfer" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvTransfer_RowCommand" DataKeyNames="TransferID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvTransfer_PageIndexChanging"
                        OnRowDataBound="gvTransfer_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="EmployeeName" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <strong><%= hrmlang.GetString("from") %> : </strong>
                                    <%# Eval("BranchFromName")%><br />
                                    <strong><%= hrmlang.GetString("to") %> : </strong>
                                    <%# Eval("BranchToName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <strong><%= hrmlang.GetString("from") %> : </strong>
                                    <%# Eval("DeptFromName")%><br />
                                   <strong><%= hrmlang.GetString("to") %> : </strong>
                                    <%# Eval("DeptToName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <strong style='display: <%# ("" + Eval("SubDeptFromName")) == "" ? "none" : "inline" %>'>
                                        <%= hrmlang.GetString("from") %> : </strong>
                                    <%# Eval("SubDeptFromName") %><br />
                                    <strong style='display: <%# ("" + Eval("SubDeptToName")) == "" ? "none" : "inline" %>'>
                                        <%= hrmlang.GetString("to") %> : </strong>
                                    <%# Eval("SubDeptToName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblRpt" runat="server" Text='<%# Eval("ReportTo") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFwd" runat="server" Text='<%# Eval("ForwardedTo") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkApprove" runat="server" CssClass="fa fa-thumbs-up" data-toggle="tooltip"
                                        CommandArgument='<%# Eval("TransferID") %>'
                                        CommandName="APPROVE"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkReject" runat="server" CssClass="fa fa-thumbs-down" data-toggle="tooltip"
                                        CommandArgument='<%# Eval("TransferID") %>'
                                        CommandName="REJECT"></asp:LinkButton>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddTransfer.aspx?id=" + Eval("TransferID") %>'
                                        data-toggle="tooltip"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TransferID") %>'
                                        CommandName="DEL" CausesValidation="false" data-toggle="tooltip"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </section>
</asp:Content>
