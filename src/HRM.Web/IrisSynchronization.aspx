<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="IrisSynchronization.aspx.cs" Inherits="IrisSynchronization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("synchronization")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("synchronization")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblIp" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblSecurityId" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lbluname" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblpwd" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDIp" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDSecurityId" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDuname" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDpwd" runat="server" Visible="false"></asp:Label></p>
                </div>
               
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="txtMaster">
                           <%= hrmlang.GetString("master")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:TextBox ID="txtmaster" runat="server" CssClass="form-control validate" placeholder="Master Device"></asp:TextBox>
                   </div>
               </div>
                <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="txtDest">
                           <%= hrmlang.GetString("destination")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:TextBox ID="txtdest" runat="server" CssClass="form-control validate"  placeholder="Destination Device"></asp:TextBox>
                   </div>
               </div>
                    
                </div>
               
              <div class="row rowmargin">
               <div class="col-xs-4">
               <asp:Button ID="btncompare" runat="server" CssClass="btn btn-primary" 
                    Text="Compare" Visible="true" onclick="btncompare_Click"  OnClientClick="return validatectrl();" />
                     
                    </div>
              </div>
            <div class="clearfix"></div>
           <asp:Panel ID="Pnlsynch" runat="server" Visible="false">
              <div class="col-xs-12">
                    
                    <asp:GridView ID="gv_Synch" runat="server" AutoGenerateColumns="false" GridLines="None"
                        CssClass="table table-bordered dataTable" DataKeyNames="UserID"
                        EmptyDataRowStyle-BorderStyle="None"
                        ShowFooter="false" onrowdatabound="gv_Synch_RowDataBound" 
                        onpageindexchanging="gv_Synch_PageIndexChanging">
                        <Columns>
                             <asp:TemplateField HeaderText="userId">
                       <ItemTemplate>
                           <asp:Label ID="lbluserId" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                           
                       </ItemTemplate>
                     
                   </asp:TemplateField>
                            <asp:BoundField DataField="FirstName" />
                            <asp:BoundField DataField="LastName" />
                            <asp:BoundField DataField="PIN"/>
                         
                            <asp:BoundField DataField="EEyeType"/>
                            <asp:BoundField DataField="StartDate" />
                           <asp:TemplateField>
                           <HeaderTemplate>
                           <asp:CheckBox ID="chkHSelect" CssClass="hcadd" runat="server" />
                           </HeaderTemplate>
                           <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="cadd" Style="font-weight: normal !important;
                            font-family: sans-serif; font-size: 12px;" />    
                            
                             </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>     
                         </asp:GridView>           
                     
                </div>
                
            <div class="clearfix"></div>
            <!-- /.box-body -->
           <div class="box-footer">
          
               <asp:Button ID="btnSynch" runat="server" CssClass="btn btn-primary" OnCommand="btn_Command"
                   CommandName="Synch" Text="Synchronize"  />
          
       </div>
          </asp:Panel>
        </div>
            <div class="clearfix"></div>
        <!-- /.box-primary -->
    </section>
      <script type="text/javascript">
          $(function () {

              $('input[type="checkbox"]').iCheck({
                  checkboxClass: 'icheckbox_minimal-blue',
                  radioClass: 'iradio_minimal-blue'
              });

              $(".hcadd").on('ifUnchecked', function (event) {
                  $(".cadd", ".dataTable").iCheck("uncheck");
              });

              $(".hcadd").on('ifChecked', function (event) {
                  $(".cadd", ".dataTable").iCheck("check");
              });
          });

        
          }
    </script>
</asp:Content>

