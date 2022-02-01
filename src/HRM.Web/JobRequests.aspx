<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobRequests.aspx.cs" Inherits="JobRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblDeptID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("jobtitles")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managejobtitles")%></li>
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
                            Text="New Job Request" CausesValidation="false" OnClick="btnNew_Click" /></div>
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
                    <asp:GridView ID="gvDepts" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvDepts_RowCommand" DataKeyNames="DepartmentID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDepts_PageIndexChanging"
                        OnRowDataBound="gvDepts_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <%--                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewBranch" runat="server" Text="View" CommandName="VIEWBRANCH" CommandArgument='<%# Eval("DepartmentID") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
                            <asp:BoundField DataField="VacancyNos" HeaderText="VacancyNos" />
                            <asp:BoundField DataField="JobPostDescription" HeaderText="Job Description" />
                            <asp:BoundField DataField="ClosingDate" HeaderText="Closing Date" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblBranchID" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>--%>
                                    <%--                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("JID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>--%>
                                    <asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# Eval("JID", "~/JobTitle.aspx?JID={0}") %>'
                                        CssClass="fa fa-edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("JID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Job Title?')"
                                        CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkPublish" runat="server" CssClass="fa-angle-double-up fa-anchor" CommandArgument='<%# Eval("JID") %>'
                                        data-toggle="tooltip" title="Publish" CommandName="PUBLISH" OnClientClick="return confirm('Are you sure to publish this Job Title?')"
                                        CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkUnPublish" runat="server" CssClass="fa-angle-double-up fa-anchor" CommandArgument='<%# Eval("JID") %>'
                                        data-toggle="tooltip" title="UnPublish" CommandName="UNPUBLISH" OnClientClick="return confirm('Are you sure to Unpublish this Job Title?')"
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
    <div class="modal fade" id="dvBranches" tabindex="-1" deduction="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">
                        <%= hrmlang.GetString("branches") %></h4>
                </div>
                <br />
                <div class="col-xs-12">
                    <asp:TextBox ID="txtBranches" runat="server" TextMode="MultiLine" Height="150px"
                        CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
