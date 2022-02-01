<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PublishtoSite.aspx.cs" Inherits="PublishtoSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblRoleID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            Careers<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("careers") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    IHRM is giving your website visitors a chance to apply directly from your website.
                    <ol>
                        <li>Create a new web page in your existing website called careers or jobs or any other
                            name</li>
                        <li>Make sure your new careers page has the same interface as all other pages on the
                            website.</li>
                        <li>Copy following HTML code and place it as the main content of your careers page:</li>
                    </ol>
                    <asp:TextBox ID="ltText" runat="server" TextMode="MultiLine" Width="650px" Height="75px" style="margin-left:40px; border:none;" ></asp:TextBox>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
