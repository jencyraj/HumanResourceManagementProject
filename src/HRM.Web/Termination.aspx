<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Termination.aspx.cs" Inherits="Termination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblDeptID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("employeetermination")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageemployeetermination")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-right rowmargin" visible="false">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="true"
                            Text="New Termination" CausesValidation="false" OnClick="btnNew_Click" /></div>
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                </asp:Panel>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:GridView ID="gvTermination" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvTermination_RowCommand" DataKeyNames="TID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvTermination_PageIndexChanging"
                        OnRowDataBound="gvTermination_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField  HeaderText="Employee Name">
                                <ItemTemplate>
                                    <div><%#Eval("EFName")%> <%#Eval("EMName")%> <%#Eval("ELName")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Forwarded To">
                                <ItemTemplate>
                                    <div><%#Eval("FFName")%> <%#Eval("FMName")%> <%#Eval("FLName")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                            <asp:BoundField DataField="Approved" HeaderText="Approved Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblBranchID" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>--%>
                                    <%--                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("JID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>--%>
                                    <asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# Eval("TID", "~/AddTermination.aspx?TID={0}") %>'
                                        CssClass="fa fa-edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Employee Termination?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>

