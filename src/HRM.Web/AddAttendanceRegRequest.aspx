<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddAttendanceRegRequest.aspx.cs" Inherits="AddAttendanceRegRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("attregularreq")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("attregularreq")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblYear" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
                  <div class="row rowmargin" id="reg1" runat="server">
                    
           
                    <div class="col-xs-2 ">
                     <div class="form-group">
                        <label for="txtintdt">
                            <%= hrmlang.GetString("employee")%></label></div>
                            </div>
                    <div class="col-xs-2 ">
                       
                             <asp:Label ID="lblemp" runat="server"  /></div>
                    <div class="clearfix">
                    </div>
                  
                    <div class="col-xs-2 ">
                        <label for="txtapp">
                            <%= hrmlang.GetString("attndnconmon&yr")%></label>
                    </div>
                    <div class="col-xs-2 ">
                       <asp:Label ID="lblapp1" runat="server"  />
                    </div>
                    <div class="col-xs-2 ">
                        <label for="txtapp">
                            <%= hrmlang.GetString("comments")%></label>
                    </div>
                    <div class="col-xs-2 ">
                       <asp:TextBox ID="txtcomments" runat="server" TextMode="MultiLine" CssClass="form-control txtround"
                            Height="100px"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtYear">
                            <%= hrmlang.GetString("year")%></label><asp:RequiredFieldValidator ID="rfvYear" runat="server"
                                ControlToValidate="txtYear" ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="rfv" runat="server" ControlToValidate="txtYear" Operator="DataTypeCheck"
                            Type="Integer" ErrorMessage="Invalid year" CssClass="text-red"></asp:CompareValidator>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control txtound" MaxLength="4"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtDesc">
                            <%= hrmlang.GetString("comments")%></label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" CssClass="form-control txtround"
                            Height="100px"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false"
                        Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
