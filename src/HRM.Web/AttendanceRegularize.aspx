<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AttendanceRegularize.aspx.cs" Inherits="AttendanceRegularize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("timesheet")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home")%></a></li>
            <li class="active">
                <%= hrmlang.GetString("mtimesheet")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    <asp:Label ID="lblindex" runat="server" Visible="false"></asp:Label>
                     <asp:Label ID="lbl_hours" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-2">
                        <label for="txtintdt">
                            <%= hrmlang.GetString("month")%></label><br />
                        <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                            DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <label for="txtapp">
                            <%= hrmlang.GetString("year")%></label>
                        <br />
                        <asp:TextBox ID="txtyear" runat="server" Width="100px" MaxLength="4" CssClass="form-control"
                            Style="display: inline; margin-right:15px;"></asp:TextBox><asp:Button ID="btnSearch" 
                            runat="server" onclick="btnSearch_Click" CssClass="btn btn-primary" /> </div>
                </div>
                <div class="clearfix">
                </div>
              <asp:Panel ID="pnl_load" runat="server" >  
                <div class="col-md-12 row">
                    
                    <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="false" GridLines="None"
                        CssClass="table table-bordered dataTable" DataKeyNames="AttendanceID"
                        OnRowDataBound="gvAttendance_RowDataBound" EmptyDataRowStyle-BorderStyle="None"
                        ShowFooter="false">
                        <Columns>
                            <asp:BoundField DataField="workdate" DataFormatString="{0:MM/dd/yyyy}" />
                            <asp:BoundField DataField="STARTTIME" />
                            <asp:BoundField DataField="ENDTIME" />
                            <asp:BoundField DataField="BreakHours" />
                          
                             <asp:TemplateField HeaderText="OverTime">
                       <ItemTemplate>
                           <asp:Label ID="lbl_OverTime" runat="server" Text='<%# Eval("OverTime") %>'></asp:Label>
                           
                       </ItemTemplate>
                     
                   </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Comments")%>
                                    <div class="clearfix"></div>
                                    <asp:Label ID="lblApp" runat="server" ForeColor="Green"></asp:Label>
                                    <asp:Label ID="lblRej" runat="server" ForeColor="Red"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <%# Eval("Comments")%>
                                    <div class="clearfix"></div>
                                    <asp:Label ID="lblws" runat="server" ForeColor="Green" Text='<%# Eval("ws_start") %>'></asp:Label>
                                    <asp:Label ID="lblsrt" runat="server" ForeColor="Green" Text='<%# Eval("STARTTIME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                           <HeaderTemplate>
                                        <asp:CheckBox ID="chkHSelect" CssClass="hcadd" runat="server" />
                                    </HeaderTemplate>
                       
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" Visible="false" Text='<%# Eval("workdate") %>'></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="cadd" Style="font-weight: normal !important;
                                        font-family: sans-serif; font-size: 12px;" />
                                    <asp:Label ID="lblDetailID" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                   
                </div>
                
                <div class="clearfix">
                </div>
               
                <div class="row rowmargin">
                    <br />
                    <div class="f-left col-xs-6">
                          <div class="col-xs-3"><label><%= hrmlang.GetString("totallatehours")%></label></div>
                          <div class="col-xs-3"><asp:TextBox ID="txtlatehr" runat="server" CssClass="form-control txtround" style="text-align:center" Enabled="false" Text="0.00"></asp:TextBox></div>
                           <div class="col-xs-3"><label><%= hrmlang.GetString("payableovertime")%></label></div>
                           <div class="col-xs-3"><asp:TextBox ID="txtovertime" runat="server" CssClass="form-control txtround" style="text-align:center" Enabled="false" Text="0.00"></asp:TextBox></div>
                          <div class="clearfix"></div>
                           <div class="col-xs-3"><label><%= hrmlang.GetString("missedhour")%></label></div>
                           <div class="col-xs-3"><asp:TextBox ID="txtlateWrknghr" runat="server" CssClass="form-control txtround" style="text-align:center" Enabled="false" Text="0.00"></asp:TextBox></div>
                          <div class="clearfix"></div>
                          <div class="col-xs-3"><label for="txtremark"><%= hrmlang.GetString("remarks")%></label></div>
                          <div class="clearfix"></div>
                          <div class="col-xs-11"><asp:TextBox ID="txtremark" CssClass="form-control" runat="server" TextMode="MultiLine"   Style="height: 100px"></asp:TextBox></div>
                          
                    </div>
                    <div class="f-left col-xs-6">
                         <div class="f-left">
                            <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary" Enabled="false"  BackColor="#ffd0d7" style="margin-right:10px" /><label>
                            <%= hrmlang.GetString("attendancemismatched")%></label>
                         </div> 
                            
                          <div class="clearfix"></div>
                          <div class="f-left">
                              <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" Enabled="false"  BackColor="#ffff8b" style="margin-right:10px"  />
                                <label>
                                <%= hrmlang.GetString("offdayattndnce")%></label>
                          </div>
                          <div class="clearfix"></div>
                   
                          <div class="f-left">
                             <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Enabled="false"  BackColor="#e6e6e6" style="margin-right:10px"  />
                             <label>
                                <%= hrmlang.GetString("offday")%></label>
                          </div>
                          <div class="clearfix"></div>
                    
                          <div class="f-left">
                             <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Enabled="false"  BackColor="#b1dffc" style="margin-right:10px"   />
                                  <label>
                                <%= hrmlang.GetString("attendancemissed")%></label>
                          </div>
                          <div class="clearfix"></div>
                          <div class="f-left">
                               <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Enabled="false"   BackColor="#79dede" style="margin-right:10px"  />
                              <label>
                                        <%= hrmlang.GetString("latemark")%></label>
                          </div>
                          <div class="clearfix"></div>
                    
                          <div class="f-left">
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Enabled="false"   BackColor="#FFFFFF" style="margin-right:10px"   />
                            <label>
                                        <%= hrmlang.GetString("attendancematched")%></label>
                          </div>
                    <div class="clearfix"></div>
                     <div class="f-left">
                            <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary" Enabled="false"   BackColor="#bababa" style="margin-right:10px"   />
                            <label>
                                        <%= hrmlang.GetString("onleave")%></label>
                          </div>

                      </div>
                 
                 </div>
                 </asp:Panel> 

            </div>
            <div class="clearfix">
            </div>
            <div class="box-footer">
                <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" OnCommand="btn_Command"
                   CommandName="Regulrize" Text="Regularize" OnClientClick="return checkremarks();"  />
                <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                    OnCommand="btn_Command" CommandName="Cancel" CausesValidation="false" />
            </div>
          
        </div>
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

        function checkremarks() {
            var remarkctrl = document.getElementById('<%= txtremark.ClientID %>');
            if (remarkctrl.value.trim() == '') {
                alert('Please enter remarks');
                remarkctrl.focus();
                return false;
            }
        }
    </script>
</asp:Content>
