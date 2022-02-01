<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BackUpIris.aspx.cs" Inherits="AddToIris" ValidateRequest="false"%>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <script type="text/javascript">
     $(function () {

         $('input[type="checkbox"]').iCheck({
             checkboxClass: 'icheckbox_minimal-blue',
             radioClass: 'iradio_minimal-blue'
         });

         $(".hcview").on('ifUnchecked', function (event) {
             $(".hview", ".dataTable").iCheck("uncheck");
         });

         $(".hcview").on('ifChecked', function (event) {
             $(".hview", ".dataTable").iCheck("check");
         });
     });


    </script>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("irisbackup")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageIrisbackup")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="true">
                    <table style="width:100%">
                        <tr>
                            <td colspan="3">
                            <div class="pull-left dblock rowmargin">
                                <p class="text-red">
                                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                                <p class="text-green">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                            </div>
                            </td>
                        </tr>
                        <tr>

                     
                    <td>
                       <div class="pull-right rowmargin" visible="true">
                        <asp:DropDownList ID="ddlBr" runat="server" 
                    CssClass="form-control" DataTextField="Branch" DataValueField="BranchID" Visible="true" 
                    AutoPostBack="True" onselectedindexchanged="ddlBr_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                   </td>
                        </tr>
                    </table>
                    <div class="clearfix">
                    </div>
                </asp:Panel>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:GridView ID="gvIrisData" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                         DataKeyNames="IrisID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvIrisData_PageIndexChanging"
                        OnRowDataBound="gvIrisData_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IPAddress") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                    <asp:Label ID="lblIrisId" runat="server" Text='<%# Eval("IrisID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lblSecurityId" runat="server" Text='<%# Eval("SecurityId") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("Password") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblbackupid" runat="server" Text='<%# Eval("BackupID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblDoorName" runat="server" Text='<%# Eval("DoorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:TemplateField>
                            <HeaderTemplate>
                                    <asp:CheckBox ID="chkHView" CssClass="hcview" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate> 
                                   <asp:CheckBox ID="chkSelect" runat="server" CssClass="hview"  />                         
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>

                                    <div class="box-footer">
                        <asp:Button ID="btnHoliday" runat="server" CssClass="btn btn-primary" Text="Backup" OnClick="btnBackup_Click"
                             /><%-- OnClick="btnBackup_Click"--%>
                      
                    </div>

            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>


