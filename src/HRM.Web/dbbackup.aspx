<%@ Page Title="DB Backup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="dbbackup.aspx.cs" Inherits="dbbackup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
    <h1><%= hrmlang.GetString("databasebackup")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("databasebackup")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtRole">
                            <%= hrmlang.GetString("databasebackuppath")%></label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtPath"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtPath" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" 
                        Text="Backup" onclick="btnSave_Click" />                   
                </div>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

