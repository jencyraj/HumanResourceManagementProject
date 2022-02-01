<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkWeek.aspx.cs" Inherits="WorkWeek" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("workweek")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("mngwrkweek")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="Set New Week" CausesValidation="false" OnClick="btnNew_Click" />
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
                        DataKeyNames="WeekID" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvPayrollTemplates_PageIndexChanging" OnRowCommand="gvPayrollTemplates_RowCommand"
                        OnRowDataBound="gvPayrollTemplates_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                           
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                             <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:BoundField DataField="FirstName" HeaderText="Employee" />
                             <asp:TemplateField>
                             <ItemTemplate>
                             <%# GetOffDays("" + Eval("Sunday") ,"" + Eval("Monday"),"" +  Eval("Tuesday") , "" + Eval("Wednesday") , "" + Eval("Thursday") , "" + Eval("Friday") , "" +  Eval("Saturday") ) %>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                           
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CommandName="Edit" CssClass="fa fa-edit"
                                        NavigateUrl='<%# "~/AddWorkWeek.aspx?id=" + Eval("WeekID") %>' data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("WeekID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete Work week?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
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
