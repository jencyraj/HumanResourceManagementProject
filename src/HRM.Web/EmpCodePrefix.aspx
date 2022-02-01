<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmpCodePrefix.aspx.cs" Inherits="EmpCodePrefix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <section class="content-header">
    <h1><%= hrmlang.GetString("employeecode") %> <small> <%= hrmlang.GetString("settings") %></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageemployeecode")%></li>
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
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmpCodePrefix">
                        <%= hrmlang.GetString("employeecodeprefix")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCodePrefix" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmpCodeCtrPrefix"><%= hrmlang.GetString("employeecodecounterprefix")%></label>
               </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCodeCtrPrefix" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmpCodeCtrStart">
                        <%= hrmlang.GetString("counterstartsfrom")%></label><asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtEmpCodeCtrStart"
                     ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCodeCtrStart" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtEmpCodeTotalLength">
                        <%= hrmlang.GetString("employeecodetotallength")%></label><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEmpCodeTotalLength"
                     ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCodeTotalLength" runat="server" CssClass="form-control validate"></asp:TextBox>
                    <p class="help-block"><asp:Label ID="lblEx" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> 
                </div>
            </div>
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" 
                OnClientClick="return validatectrl();" onclick="btnSave_Click" />
        </div>
    </div>
    </section><!-- /.content -->
</asp:Content>

