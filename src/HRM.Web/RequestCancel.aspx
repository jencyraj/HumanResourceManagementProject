<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RequestCancel.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<section class="content-header">
    <h1>Request Cancel<small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active">Manage Cancel Requests</li>
    </ol>
</section>
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
         <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtRole">
                           Request Type</label>
                            </div>
                             <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                    <asp:DropDownList ID="ddlReqtype" CssClass="form-control" runat="server" DataTextField="ReqType" DataValueField="ReqID" Enabled="false"></asp:DropDownList>
                    </div>
                            </div>  
                               <div class="col-mg-12 rowmargin">
                             <asp:Panel ID="pnlreq" runat="server" Visible="false">
                          
                             <asp:GridView ID="gvResignation" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    DataKeyNames="Resgnid"  
                    EnableViewState="True" >
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName"/>
                        <asp:BoundField DataField="NoticeDate" HeaderText="Notice Date"  DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ResgnDate" HeaderText="Resignation Date"  DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Reason" HeaderText="Reason"/>
                        <asp:TemplateField Visible="false">
                        <HeaderTemplate>Status</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Approved") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                      
                    </Columns>
                </asp:GridView> 
                             </asp:Panel>
                            <asp:Label ID="lblresgID" runat="server" Visible="false"></asp:Label>
                             </div>
                             <asp:Panel ID="pnlButtons" runat="server">
                   <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Cancel request" OnClick="btnSave_Click"
                            OnClientClick="return" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Back to List"
                            OnClick="btnCancel_Click" />
                    </div>
                 </asp:Panel>

        </div></div></section>
</asp:Content>

