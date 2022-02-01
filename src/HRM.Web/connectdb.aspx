<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="connectdb.aspx.cs" Inherits="connectdb" Title="Database Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAssetID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1><%= hrmlang.GetString("dbsettings")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home")%></a></li>
            <li class="active"><%= hrmlang.GetString("dbsettings")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtServer">
                                <%= hrmlang.GetString("dbserver")%></label><asp:RequiredFieldValidator ID="rfvServer"
                                    runat="server" ControlToValidate="txtServer" ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtServer" runat="server" CssClass="form-control txtround" placeholder="Enter Database Server Name"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtDbName">
                                <%= hrmlang.GetString("dbname")%></label><asp:RequiredFieldValidator ID="rfvDBname" runat="server" ControlToValidate="txtDbName"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDbName" runat="server" CssClass="form-control txtround" placeholder="Enter Database Name"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtUserID">
                                <%= hrmlang.GetString("userid")%></label><asp:RequiredFieldValidator ID="rfvuserid" runat="server" ControlToValidate="txtUserID"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control txtround" placeholder="Enter User ID"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtPassword">
                                <%= hrmlang.GetString("password")%></label><asp:RequiredFieldValidator ID="rfpwd" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control txtround" placeholder="Enter Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" style="padding-left:30px;padding-right:30px;" />
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
