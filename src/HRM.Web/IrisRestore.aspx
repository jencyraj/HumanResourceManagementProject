
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
   CodeFile="IrisRestore.aspx.cs" Inherits="IrisRestore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("memos")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("memos")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
     <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <!-- /.box-header -->
            <!-- /.box-body -->
            <div class="box-body">
            <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
               
                 <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                      <label><%= hrmlang.GetString("device")%></label></div>
                    
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:TextBox ID="txtIP" runat="server" placeholder="Enter IP Address" CssClass="form-control"  ></asp:TextBox>
                       
                    </div>
                    <div class="col-lg-4" style="padding-left:7px; padding-right:0px">
                        <asp:Button ID="btnshow" runat="server" CssClass="btn btn-primary btn-sm" Text="Show Backups"
                            CausesValidation="false"  ClientIDMode="Static" onclick="btnshow_Click" />
                        
                    </div>
                    
                </div>
                <div class="clearfix"></div>
                 <asp:Panel ID="pnl_User" runat="server" Visible="true" >  
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gv_Backup" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-bordered table-striped dataTable"  
                        DataKeyNames="BackupID" onrowcommand="gv_Backup_RowCommand"> 
                      
                        <Columns>
                            <asp:BoundField HeaderText="BackupDevice" DataField="BackupDevice" />
                            <asp:BoundField HeaderText="BackupDate"   DataField="BackupDate"/>
                            <asp:BoundField HeaderText="CreatedBy"    DataField="CreatedBy" />
                          
                            <asp:TemplateField HeaderText="Restore">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkrestore" runat="server" CommandArgument='<%#Eval("BackupID") %>'
                                    CommandName="Restore">Restore</asp:LinkButton>
                                     <asp:Label ID="lbl_IPAddress" runat="server" Visible="false" Text='<%#Eval("IPAddress") %>'></asp:Label>
                                     <asp:Label ID="lbl_SecurityId" runat="server" Visible="false" Text='<%#Eval("SecurityId") %>'></asp:Label>
                                     <asp:Label ID="lbl_Username" runat="server" Visible="false" Text='<%#Eval("UserName") %>'></asp:Label>
                                     <asp:Label ID="lbl_Password" runat="server" Visible="false" Text='<%#Eval("Password") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                        </Columns>
                        
                            
                     
                 
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                </asp:Panel>
                <asp:Panel ID="pnl_Card" runat="server" visible="false">  
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gv_Card" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable" 
                      >
                        <Columns>
                            <asp:BoundField HeaderText="CardID" DataField="UserID" />
                            <asp:BoundField HeaderText="User" DataField="FirstName" />
                            <asp:BoundField HeaderText="CardNumber" DataField="CardNumber"/>
                            <asp:BoundField HeaderText="CardKind" DataField="CardKind" />
                           
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                </asp:Panel>
                 <%--<div class="box-footer">
                 <div class="col-lg-2" style="padding-left:17px; padding-right:0px">
                      <label><%= hrmlang.GetString("destination")%></label></div>
                    
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:TextBox ID="txtdest" runat="server" placeholder="Enter Destination" CssClass="form-control"  ></asp:TextBox>
                       
                    </div>
                    <div class="col-lg-4" style="padding-left:7px; padding-right:0px">
                        <asp:Button ID="btnrestore" runat="server" CssClass="btn btn-primary btn-sm" Text="Restore"
                            CausesValidation="false"  ClientIDMode="Static" />
                        
                    </div>
            </div>--%>
                
            </div>
            <!-- /.box-body -->
    
        <!-- /.box-primary -->
    </section>
</asp:Content>

