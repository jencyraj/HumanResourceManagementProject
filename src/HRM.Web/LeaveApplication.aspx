<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LeaveApplication.aspx.cs" Inherits="LeaveApplication" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("applyforleave")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("applyforleave")%></li>
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
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                    OnRowDataBound="gvBalance_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Leave Type" DataField="LeaveName" />
                        <asp:BoundField HeaderText="No. of Days" DataField="LeaveDays" />
                        <asp:BoundField HeaderText="Carry Over" DataField="CarryOver" />
                        <asp:TemplateField HeaderText="Leaves Taken">
                            <ItemTemplate>
                                <asp:Label ID="lblTaken" runat="server" Text='<%# Eval("LeavesTaken") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Carry Overed" DataField="PrevYearBalance" />
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" Text='<%#Util.ToDecimal(Eval("PrevYearBalance")) +  Util.ToDecimal(Eval("LeavesBalance")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-3">
                        <label for="txtDate">
                            <%= hrmlang.GetString("dateofapplication")%></label></div>
                    <div class="col-xs-3">
                        <label for="txtEmployee">
                            <%= hrmlang.GetString("employee") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Enabled="false"
                            placeholder="Date of Application"></asp:TextBox>
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" Enabled="false"
                            placeholder="Employee"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <br />
                        <label for="txtReason">
                            <%= hrmlang.GetString("reasonforleave")%></label>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control validate" placeholder="Reason for Leave"
                            TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-3">
                        <label for="txtLDate">
                            <%= hrmlang.GetString("selectdate")%></label>
                    </div>
                    <div class="col-xs-3">
                        <label for="ddlSession">
                            <%= hrmlang.GetString("session")%></label>
                    </div>
                    <div class="col-xs-3">
                        <label for="ddlType">
                            <%= hrmlang.GetString("leavetype")%></label>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <uc1:ctlDOB ID="ctlCalendardob" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="5" MinYearCountFromNow="0" />
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control">
                            <asp:ListItem Text="FULL" Value="FULL"></asp:ListItem>
                            <asp:ListItem Text="FN" Value="FN"></asp:ListItem>
                            <asp:ListItem Text="AN" Value="AN"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" DataTextField="LeaveName"
                            DataValueField="LeaveTypeID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success btn-sm newemg" Text="Add"
                            OnClick="btnAdd_Click"></asp:Button>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvLeaves" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            Width="100%" OnRowCommand="gvLeaves_RowCommand" OnRowDataBound="gvLeaves_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Leave Date" DataField="LeaveDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField HeaderText="Leave Session" DataField="LeaveSession" />
                                <asp:BoundField HeaderText="Leave Days" DataField="LeaveDays" />
                                <asp:BoundField HeaderText="Leave Type" DataField="LeaveName" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLtype" runat="server" Visible="false" Text='<%# Eval("LeaveTypeID") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LeaveDetailsID") %>'
                                            CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this entry?')"
                                            CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <!-- /.box-body -->
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Visible="false"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false"
                    Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <br />
            <br />
            <br />
        </div>
            <div class="clearfix"></div>
        <!-- /.box-primary -->
    </section>
</asp:Content>
