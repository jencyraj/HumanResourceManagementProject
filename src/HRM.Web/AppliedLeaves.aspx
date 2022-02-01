<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AppliedLeaves.aspx.cs" Inherits="AppliedLeaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <section class="content-header">
    <h1><%= hrmlang.GetString("appliedleaves")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("appliedleaves")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <div class="box box-primary">
       <%-- <div class="box-header">
            <h3 class="box-title">
                Company Profile</h3>
        </div>--%>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvLeave" runat="Server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped dataTable" AllowPaging="true" PageSize="25" 
                    onpageindexchanging="gvLeave_PageIndexChanging" 
                    onrowcommand="gvLeave_RowCommand" onrowdatabound="gvLeave_RowDataBound" 
                     >
                    <Columns>
                        <asp:BoundField HeaderText="Applied Date" DataField="CreatedDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="No. of Days" DataField="TotalDays" />
                        <asp:BoundField HeaderText="Reason" DataField="Reason" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# String.Concat("LeaveApplication.aspx?leaveid=",Eval("LeaveID")) %>' CausesValidation="false"></asp:HyperLink>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LeaveID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Leave?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
            
        </div>
    </div>
    </section><!-- /.content -->
</asp:Content>

