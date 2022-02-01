<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("users") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageusers")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" 
                    Text="New User" CausesValidation="false" onclick="btnNew_Click" Visible="false" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvUsers" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvUsers_RowCommand" DataKeyNames="UID" 
                    EnableViewState="True" AllowPaging="true" PageSize="5" 
                    OnPageIndexChanging="gvUsers_PageIndexChanging" 
                    OnRowDataBound="gvUsers_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="USERID" HeaderText="User ID" />
                        <asp:BoundField DataField="RoleName" HeaderText="Role" />
                        <asp:BoundField DataField="BiometricId" HeaderText="Biometric Id" />
                        <asp:BoundField DataField="IrisId" HeaderText="Iris Id" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblPWD" runat="server" Text='<%# Eval("Password") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("UID") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("UID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this User?')" 
                                    CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                        <div class="clearfix"></div>
                     <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtUserID">
                        <%= hrmlang.GetString("userid") %></label>
                    <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtUserID"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control txtround" placeholder="Enter User ID"></asp:TextBox>
                </div>
            </div>
                     <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtPwd">
                        <%= hrmlang.GetString("password")%></label>
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtPwd"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control txtround" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                </div>
            </div>
                     <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlRole">
                        <%= hrmlang.GetString("role")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control txtround" DataTextField="RoleName"
                        DataValueField="RoleID">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtBiometricId">
                        <%= hrmlang.GetString("biometricid")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtBiometricId" runat="server" CssClass="form-control txtround" placeholder="Enter Biometric Id"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtIrisId">
                        <%= hrmlang.GetString("irisid")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtIrisId" runat="server" CssClass="form-control txtround" placeholder="Enter Iris Id"></asp:TextBox>
                </div>
            </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
           </asp:Panel>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
