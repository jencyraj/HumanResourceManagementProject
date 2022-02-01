<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddEditAttendanceType.aspx.cs" Inherits="AddEditAttendanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1 id="h1" runat="server"><%= hrmlang.GetString("addnewattendancetype")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home")%></a></li>
            <li class="active" id="LI1" runat="server"><%= hrmlang.GetString("addnewattendancetype")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                     <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtATCode"><%= hrmlang.GetString("attendancecode")%></label>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" CssClass="text-red" ControlToValidate="txtATCode"></asp:RequiredFieldValidator>                      
                            <asp:Label ID="lblATCodeReq" runat="server" CssClass="text-red" />                        
                            <asp:TextBox ID="txtATCode" runat="server" placeholder="Enter Attendance Type Code" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAttendanceType"><%= hrmlang.GetString("attendancetype")%></label>                      
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="text-red" ControlToValidate="txtAttendanceType"></asp:RequiredFieldValidator>                      
                            <asp:Label ID="lblAtReq" runat="server" CssClass="text-red" />                        
                            <asp:TextBox ID="txtAttendanceType" runat="server" placeholder="Enter Attendance Type" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddCategory"><%= hrmlang.GetString("category")%></label><br />
                            <asp:DropDownList ID="ddCategory" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Attendance" Value="A" Selected="True" />
                                <asp:ListItem Text="Leave" Value="L" />
                                <asp:ListItem Text="Holiday" Value="H" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddTypeKind"><%= hrmlang.GetString("typekind")%></label><br />
                            <asp:DropDownList ID="ddTypeKind" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Day" Value="D" Selected="True" />
                                <asp:ListItem Text="Hour" Value="H" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
</asp:Content>

