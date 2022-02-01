<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddAttendanceRegularize.aspx.cs" Inherits="AddAttendanceRegularize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("attregularreq")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("attregularreq")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblmon" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblyr" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblreqID" runat="server" Visible="false"></asp:Label></p>
                    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblindex" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin" id="reg1" runat="server">
                    <div class="col-xs-3 ">
                        <div class="form-group">
                            <label for="txtemp">
                                <%= hrmlang.GetString("employee")%></label>
                            :
                            <asp:Label ID="lblemp" runat="server" /></div>
                    </div>
                    <div class="col-xs-4">
                        <label for="txtyear">
                            <%= hrmlang.GetString("month")%>
                            /
                            <%= hrmlang.GetString("year")%>
                            :
                        </label>
                        <asp:Label ID="lblyear" runat="server" />
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-1">
                        <label for="txtcomment">
                            <%= hrmlang.GetString("comments")%>
                            :
                        </label>
                    </div>
                    <div class="col-xs-5">
                        <asp:Label ID="txtcomments" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="false" GridLines="None"
                    CssClass="table table-bordered dataTable" DataKeyNames="AttendanceID" OnRowDataBound="gvAttendance_RowDataBound"
                    EmptyDataRowStyle-BorderStyle="None" ShowFooter="false" Width="95%">
                    <Columns>
                        <asp:BoundField DataField="workdate" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="150PX" />
                        <asp:BoundField DataField="STARTTIME" />
                        <asp:BoundField DataField="ENDTIME" />
                        <asp:BoundField DataField="BreakHours" />
                        <asp:TemplateField HeaderText="OverTime">
                            <ItemTemplate>
                                <asp:Label ID="lbl_OverTime" runat="server" Text='<%# Eval("OverTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comments" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblATID" runat="server" Visible="false" Text='<%# Eval("AttendanceID") %>'></asp:Label>
                                <asp:Label ID="lblWSID" runat="server" Visible="false" Text='<%# Eval("WSID") %>'></asp:Label>
                                <asp:Label ID="lblDetailID" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <%# Eval("Comments") %>
                                <div class="clearfix">
                                </div>
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
                                <asp:CheckBox ID="chkSelect" CssClass="cadd" runat="server" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <br />
                    <div class="f-left col-xs-6">
                        <div class="col-xs-3">
                            <label>
                                <%= hrmlang.GetString("totallatehours")%></label></div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtlatehr" runat="server" CssClass="form-control txtround" Style="text-align: center"
                                Enabled="false" Text="0.00"></asp:TextBox></div>
                        <div class="col-xs-3">
                            <label>
                                <%= hrmlang.GetString("payableovertime")%></label></div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtovertime" runat="server" CssClass="form-control txtround" Style="text-align: center"
                                Enabled="false" Text="0.00"></asp:TextBox></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-3">
                            <label>
                                <%= hrmlang.GetString("missedhour")%></label></div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtlateWrknghr" runat="server" CssClass="form-control txtround"
                                Style="text-align: center" Enabled="false" Text="0.00"></asp:TextBox></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-3">
                            <label for="txtremark">
                                <%= hrmlang.GetString("remarks")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-11">
                            <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" TextMode="MultiLine"
                                Style="height: 100px"></asp:TextBox></div>
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
                <div class="box-footer">
                    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" OnCommand="btn_Command"
                        CommandName="Approve" />
                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                        OnCommand="btn_Command" CommandName="Reject" />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" OnClick="btnClose_Click" />
                </div>
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
    </script>
</asp:Content>
