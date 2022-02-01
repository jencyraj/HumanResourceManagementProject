<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="col-md-12">
            <center><div><img src="images/access_denied.jpg" alt="" style="border:0" /></div>
            <div class="clearfix"></div>
            <div class="col-md-12" style="margin-top: -133px;font-size: 20px;font-weight: 600;margin-left: -5px;"><%= hrmlang.GetString("accessdenied") %></div></center>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>
