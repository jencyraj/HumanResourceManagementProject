<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LeaveTypes.aspx.cs" Inherits="LeaveTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLeaveTypeID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("leavetypes")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageleavetypes")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Branch" CausesValidation="false" onclick="btnNew_Click" /></div>
             <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlBranches">
                        </label>
                </div>
                 <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList Visible="false" ID="ddlBranches" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID"></asp:DropDownList>
                </div>
            </div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvLType" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvLType_RowCommand" DataKeyNames="LeaveTypeID" 
                    EnableViewState="True" AllowPaging="true" PageSize="5" 
                    OnPageIndexChanging="gvLType_PageIndexChanging" 
                    OnRowDataBound="gvLType_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="LeaveName" HeaderText="Leave Name" />
                        <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                        <asp:TemplateField HeaderText="Leave Type" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTypeCode" runat="server" Text='<%# Eval("LeaveType") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("LeaveType") == "1" ? "Yearly" : "Monthly" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LeaveDays" HeaderText="No. of Days" />
                        <asp:BoundField DataField="CarryOver" HeaderText="Carry Over" />
                        <asp:BoundField DataField="Deduction" HeaderText="Deduction" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("LeaveTypeID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LeaveTypeID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Leave Type?')" CausesValidation="false"></asp:LinkButton>
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
                    <label for="txtName">
                        <%= hrmlang.GetString("leavename")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control validate" placeholder="Enter Leave Name"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtSName">
                        <%= hrmlang.GetString("shortname")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtSName" runat="server" CssClass="form-control validate" placeholder="Enter Short Name"></asp:TextBox>
                </div>
            </div> 
                    <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control" Visible ="false">
                        <asp:ListItem Text="Yearly" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Monthly" Value="2"></asp:ListItem>
                    </asp:DropDownList>
           
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDays">
                        <%= hrmlang.GetString("noofdays") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDays" runat="server" CssClass="form-control validate" placeholder="Enter No. of Days"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlCarryOver">
                        <%= hrmlang.GetString("carryover") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlCarryOver" runat="server" CssClass="form-control" >
                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
             <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlDeduction">
                        <%= hrmlang.GetString("leavded")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlDeduction" runat="server" CssClass="form-control" >  
                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validatectrl();" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
            </asp:Panel>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
