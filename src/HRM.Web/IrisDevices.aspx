<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="IrisDevices.aspx.cs" Inherits="IrisDevices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("irisdevices") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mirisdevices")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                        CausesValidation="false" OnClick="btnNew_Click" /></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvDevice" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvDevice_RowCommand" DataKeyNames="IRISID" EnableViewState="True"
                        AllowPaging="true" PageSize="20" OnPageIndexChanging="gvDevice_PageIndexChanging"
                        OnRowDataBound="gvDevice_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="IPADDRESS" />
                            <asp:BoundField DataField="DoorName" />
                            <asp:BoundField DataField="securityid" />
                            <asp:BoundField DataField="username" />
                            <asp:BoundField DataField="password" />
                            <asp:BoundField DataField="branch" />
                             <asp:BoundField DataField="masterstatus" />
                           <%-- <asp:TemplateField>
                            <HeaderTemplate>MasterDevice
                                    <asp:CheckBox ID="chkHView" CssClass="hcview" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate> 
                                   <asp:CheckBox ID="chkSelect" runat="server" ValidationGroup="masterID" />                         
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                 <asp:Label ID="lblmstrdev" runat="server" Text='<%# Eval("MasterDevice") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblBranchID" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("IRISID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("IRISID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Role?')"
                                        CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlNew" runat="server" Visible="false" >
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="ddlBranch">
                                <%= hrmlang.GetString("branch")%></label>
                            <div class="clearfix">
                            </div>
                            <div class="col-xs-4" style="margin-left:-15px">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Branch" DataValueField="BranchID"
                                    CssClass="form-control txtround" Width="330px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtIP">
                                <%= hrmlang.GetString("ipaddress")%></label>
                            <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtIP" ErrorMessage="Required"
                                CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtIP" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtDoor">
                                <%= hrmlang.GetString("doorname")%></label>
                            <asp:RequiredFieldValidator ID="rfvDoor" runat="server" ControlToValidate="txtDoor" ErrorMessage="Required"
                                CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtDoor" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtSecurity">
                                <%= hrmlang.GetString("securityid")%></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSecurity"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtSecurity" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtUserID">
                                <%= hrmlang.GetString("userid")%></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserID"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtPassword">
                                <%= hrmlang.GetString("password")%></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="chkSelect">
                                <%= hrmlang.GetString("masterdevice")%></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                             <asp:CheckBox ID="chkSelect" runat="server"  />   
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false"
                            Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
       

       
    <%--Language Popup STARTS--%>
    <div class="modal fade" id="dvConfirm" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H6">Confirmation</h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                       <asp:Label ID="lblQues" runat="server">Master Device already set...Do you want to replace?</asp:Label></div>
                    <div class="clearfix">
                    </div>
                     
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnYes" runat="server" CssClass="btn btn-primary" OnClick="btnYesOnclick" Text="Yes"/>
                    <asp:Button ID="btnNo" type="button" class="btn btn-default" data-dismiss="modal" runat="server" Text="close" OnClick="btnNoOnclick"/>
                       <%-- <%= hrmlang.GetString("close") %>--%>
                </div>
            </div>
        </div>
    </div>
    <%--Language Popup ENDS--%>
    </section>

</asp:Content>
