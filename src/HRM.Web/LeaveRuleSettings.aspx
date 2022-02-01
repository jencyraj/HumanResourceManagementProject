<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LeaveRuleSettings.aspx.cs" Inherits="AddNewLeaveRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLeaveTypeID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
    <h1><%= hrmlang.GetString("leavrule")%><small></small></h1>
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
                    Text="Set New Rule" CausesValidation="false" onclick="btnNew_Click" /></div>
                    <div class="clearfix"></div>
            
            <div class="col-xs-12" runat="server" id="grd">
                <asp:GridView ID="gvLType" runat="Server" AutoGenerateColumns="False"  CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvLType_RowCommand" DataKeyNames="LRID" 
                    EnableViewState="True" AllowPaging="true" PageSize="5" 
                    OnPageIndexChanging="gvLType_PageIndexChanging" 
                    OnRowDataBound="gvLType_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="StartFrom" HeaderText="Starts From" />
                       
                        <asp:BoundField DataField="MinimumLeaves" HeaderText="Minimum Leaves" />
                        
                          <asp:BoundField DataField="DActive" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                              <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddNewLeaveRule.aspx?id=" + Eval("LRID") %>'
                                    data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LRID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Leave Type?')" CausesValidation="false"></asp:LinkButton>
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
