<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HolidaysReport.aspx.cs" Inherits="HolidaysReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblHolidayID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("holidaysreport") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("mholidaysreport") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row  rowmargin" style="">
                    <div class="col-xs-2" style="width:82px">
                        <label><%= hrmlang.GetString("branch") %> : </label>
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlBr" runat="server" CssClass="form-control"  
                            DataTextField="Branch" DataValueField="BranchID">
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
                        <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
