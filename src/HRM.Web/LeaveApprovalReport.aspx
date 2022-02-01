<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LeaveApprovalReport.aspx.cs" Inherits="LeaveApprovalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("leaveapprovalsreport") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("mleaveapprovalsreport")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <%-- <div class="box-header">
            <h3 class="box-title">
                Company Profile</h3>
        </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-md-12 rowmargin">
                    <div class="col-xs-3">
                        <strong><%= hrmlang.GetString("approvalstatus") %></strong>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="P" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
                            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary"
                            OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box-footer">
            </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>
