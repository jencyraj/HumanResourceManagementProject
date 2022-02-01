<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Designations.aspx.cs" Inherits="Designations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblDesgnID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("company") %><small><%= hrmlang.GetString("designations")%></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("managedesignations")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
            <div  class="pull-right rowmargin" visible="false">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" 
                    Text="New Designation" CausesValidation="false" onclick="btnNew_Click" Visible="false" /></div>
            
           <div  class="pull-left dblock rowmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
                       <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtCode">
                        <%= hrmlang.GetString("seniordesignations")%></label>
                    </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
            <asp:DropDownList ID="ddlDesgn" runat="server" DataTextField="Designation" DataValueField="DesignationID"  CssClass="form-control"
                   ></asp:DropDownList>      </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtCode">
                        <%= hrmlang.GetString("designationcode")%></label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Enter Designation Code"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDesignation">
                        <%= hrmlang.GetString("designation")%></label>
                    <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesignation"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Enter Designation"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDesignation">
                        <%--Report To--%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                   <%-- <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation" DataValueField="DesignationID" Visible="false"></asp:DropDownList>
             --%>   </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
            </asp:Panel>
            <div class="col-mg-12 rowmargin rowpadleft">
                <asp:GridView ID="gvDesignation" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvDesignation_RowCommand" DataKeyNames="DesignationID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvDesignation_PageIndexChanging" 
                    onrowdatabound="gvDesignation_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="DesgnCode" HeaderText="Code" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                        <asp:BoundField DataField="PARENTNAME" HeaderText="Report To" Visible="false" />
                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Label ID="lblParentID" runat="server" Visible="false" Text='<%# Eval("DesignationID") %>'></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("DesignationID") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("DesignationID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Designation?')" CausesValidation="false"
                                      data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
