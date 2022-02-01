<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PayrollTemplates.aspx.cs" Inherits="PayrollTemplates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<section class="content-header">
        <h1><%= hrmlang.GetString("payrolltemplates")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("payrolltemplates")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="Add New" CausesValidation="false" OnClick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvPayrollTemplates" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="PMId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvPayrollTemplates_PageIndexChanging" OnRowCommand="gvPayrollTemplates_RowCommand"
                        OnRowDataBound="gvPayrollTemplates_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:BoundField DataField="Employee" HeaderText="Employee" />
                            <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" />
                            <asp:TemplateField>
                               <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("PMId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Payroll Template?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView> 
                </div>
                <div class="clearfix"></div>
            </div>
       </div>
    </section>
</asp:Content>

