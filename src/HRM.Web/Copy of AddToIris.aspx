<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Copy of AddToIris.aspx.cs" Inherits="AddToIris" ValidateRequest="false"%>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlStartDate" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlEndDate" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("irisdevice")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageirisdevicedata")%></li>
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
                      <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtStartDate">
                                <%= hrmlang.GetString("startdate")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                    <div class="col-xs-4">
                       <uc1:ctlStartDate ID="ctlStartDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control dtjoin hrmhide"
                            placeholder="Start Date"></asp:TextBox>
                        </div>
                    </div>
                    </td>
                    <td>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtEndDate">
                                <%= hrmlang.GetString("enddate")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                       <uc2:ctlEndDate ID="ctlEndDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control dtjoin hrmhide"
                            placeholder="End Date"></asp:TextBox>
                        </div>
                    </div>
                    </td>
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
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate> 
                                   <asp:CheckBox ID="chkSelect" runat="server" />                         
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>

                                    <div class="box-footer">
                        <asp:Button ID="btnHoliday" runat="server" CssClass="btn btn-primary" Text="Add Holidays" OnClick="btnHolidays_Click"
                             />
                        <asp:Button ID="btnTransactions" runat="server" CssClass="btn btn-primary" Text="Add Transactions"
                            OnClick="btnTransactions_Click" />
                    </div>

            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>


