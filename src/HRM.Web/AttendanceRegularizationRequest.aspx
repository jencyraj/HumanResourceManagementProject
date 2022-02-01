<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AttendanceRegularizationRequest.aspx.cs" Inherits="AttendanceRegularizationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("attregular")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("attregular")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <div style="margin: auto; float: left; padding-right: 14px">
                      <label><%= hrmlang.GetString("filterby") %></label> : 
                        <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control" 
                            AutoPostBack="true" style="width:100px; display:inline" 
                            onselectedindexchanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false"
                            OnClick="btnNew_Click" />
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin" runat="server" id="dvReject" visible="false">
                    <div class="col-xs-4">
                        <label for="txtReason">
                            <%= hrmlang.GetString("rejectreason")%></label><asp:Label ID="lblDesc" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtReason"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="form-control txtround"
                            Height="100px" Width="450px" Style="display: inline"></asp:TextBox>
                        <asp:Label ID="lblReqID" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 50px"
                            OnClick="btnReject_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 50px"
                            OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvRequests" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true"
                        OnPageIndexChanging="gvRequests_PageIndexChanging" OnRowCommand="gvRequests_RowCommand"
                        OnRowDataBound="gvRequests_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Employee" />
                            <asp:BoundField DataField="MonthName" />
                            <asp:BoundField DataField="ReqYear" />
                            <asp:TemplateField ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lComm" runat="server" Text='<%# (Eval("ReqReason").ToString().Length>100) ? Eval("ReqReason").ToString().Substring(0,100)+"..." : Eval("ReqReason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("CreatedDate", "{0:dd MMM yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AttendanceRegularize.aspx?id=" + Eval("reqid") %>'
                                        data-toggle="tooltip" title="Edit"  Visible='<%# (""+ Eval("ShowApprove") == "Y") ? false : true %>'></asp:HyperLink><%--if showapprove Y user will not be able to edit his subordinate's request--%>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("reqid") %>'
                                        CommandName="DEL" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                    <asp:HyperLink ID="lnkApprove" runat="server" NavigateUrl='<%# "~/AddAttendanceRegularize.aspx?id=" + Eval("reqid") %>'
                                         CssClass="fa fa-thumbs-up" data-toggle="tooltip" Visible='<%# (""+ Eval("ShowApprove") == "Y" && "" + Eval("RequestClosed") != "Y") ? true : false %>'></asp:HyperLink>
                                    <asp:HyperLink ID="lnkReject" runat="server" NavigateUrl='<%# "~/AddAttendanceRegularize.aspx?id=" + Eval("reqid") %>'
                                         CssClass="fa fa-thumbs-down" data-toggle="tooltip" Visible='<%# (""+ Eval("ShowApprove") == "Y"  && "" + Eval("RequestClosed") != "Y") ? true : false %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </section>
</asp:Content>
