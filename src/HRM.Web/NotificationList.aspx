<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NotificationList.aspx.cs" Inherits="NotificationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<section class="content-header">
    <h1><%= hrmlang.GetString("notificationlist")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("notificationlist")%></li>
    </ol>
</section>
<section class="content">
   <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
   <div class="box box-primary">
       <!-- /.box-header -->
       <div class="box-body">
        
                    <div class="pull-right rowmargin" visible="false" style="margin-right:10px;">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="true"
                            Text="Add New" CausesValidation="false" OnClick="btnNew_Click" /></div>
                    
                    <div class="clearfix">
                    </div>
                
       <div class="clearfix">
               </div>

               <div class="box-body">
               
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:GridView ID="gvnotifi" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="NotifyID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnRowCommand="gvnotify_RowCommand">

                        <%-- 
                        OnPageIndexChanging="gvnotifi_PageIndexChanging"
                        OnRowDataBound="gvnotifi_RowDataBound"
                        --%>
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            
                            <asp:BoundField DataField="Notification" HeaderText="Notification" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                      <asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# Eval("NotifyID", "~/Notification.aspx?NotifyID={0}") %>'
                                        CssClass="fa fa-edit"  data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("NotifyID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Notification!?')"
                                        CausesValidation="false"></asp:LinkButton>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                </div>
                <div class="clearfix">
                </div>
            </div>
               </div>
               </div>
               </section>
</asp:Content>

