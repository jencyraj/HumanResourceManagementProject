<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmailSettings.aspx.cs" Inherits="EmailSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <section class="content-header">
    <h1><%= hrmlang.GetString("email") %><small><%= hrmlang.GetString("settings")%></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageemail") %></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
    <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div class="alert alert-danger alert-dismissable" id="dvMsg" runat="server" visible="false">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtFromMail">
                        <%= hrmlang.GetString("fromemail") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtFromMail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtFromName"><%= hrmlang.GetString("fromname") %></label>
               </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtFromName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="rbtnAuth">
                        <%= hrmlang.GetString("smtpauthentication")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rbtnAuth" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlSecurity">
                        <%= hrmlang.GetString("smtpsecurity")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlSecurity" runat="server" CssClass="form-control">
                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                        <asp:ListItem Text="SSL" Value="SSL"></asp:ListItem>
                        <asp:ListItem Text="TLS" Value="TLS"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtPort">
                        <%= hrmlang.GetString("smtpport") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPort" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtSMTPUserName">
                        <%= hrmlang.GetString("smtpusername")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtSMTPUserName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtSMTPPassword">
                        <%= hrmlang.GetString("smtppassword") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtSMTPPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter SMTP Password"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtHost">
                        <%= hrmlang.GetString("smtphost") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtHost" runat="server" CssClass="form-control" placeholder="Enter SMTP Host"></asp:TextBox>
                </div>
            </div> 
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" 
                onclick="btnSave_Click" />
        </div>
    </div>
    </section><!-- /.content -->
</div>
</asp:Content>

