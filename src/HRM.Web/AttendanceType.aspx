<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AttendanceType.aspx.cs" Inherits="AttendanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("manageattendancetype")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("manageattendancetype")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left;padding-right:14px">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Attendance Type" CausesValidation="false" onclick="btnNew_Click" />
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvAttendanceType" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="ATId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvAttendanceType_PageIndexChanging" OnRowCommand="gvAttendanceType_RowCommand"
                        OnRowDataBound="gvAttendanceType_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                           <asp:BoundField DataField="ATCode" HeaderText="Attendance Code" />
                            <asp:BoundField DataField="AttendanceType" HeaderText="Attendance Type" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="TypeKind" HeaderText="Type Kind" />
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit"
                                        NavigateUrl='<%# "~/AddEditAttendanceType.aspx?id=" + Eval("ATId") %>' data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("ATId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Attendance Type?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView> 
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
</asp:Content>

