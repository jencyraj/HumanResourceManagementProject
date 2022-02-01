<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RecruitmentReport.aspx.cs" Inherits="RecruitmentReport" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label ID="lblLeaveTypeID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("recruitmentreport")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("recruitmentreport")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12 rowmargin">
                     <div class="col-xs-3">
                        <strong>
                            <%= hrmlang.GetString("jobtitle")%></strong>
                        <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="form-control txtround" DataTextField="JobTitle" DataValueField="JobTitle">
                        </asp:DropDownList>
                        </div>
                         <div class="col-xs-3">
                        <strong>
                            <%= hrmlang.GetString("status")%></strong>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control txtround" >
                        </asp:DropDownList>

                    </div>
                    
                    <div class="clearfix">
                    </div>
                    <div class="clearfix">
                    </div>
                   <div class="col-md-12 rowmargin">
                    
                    <div class="col-xs-3">
                        <label for="txtLDate">
                            <%= hrmlang.GetString("fromdate")%></label>
                    </div>
                    <div class="col-xs-3">
                        <label for="ddlSession">
                            <%= hrmlang.GetString("todate")%></label>
                    </div>
                   
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <uc1:ctlDOB ID="ctlCalendardob" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="5" MinYearCountFromNow="0" />
                    </div>
                    <div class="col-xs-3">
                         <uc1:ctlDOB ID="CtlDOB1" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="5" MinYearCountFromNow="0" />
                    </div>
                   
                    <div class="col-xs-1">
                      <%--  <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success btn-sm newemg" Text="Add"
                            OnClick="btnAdd_Click"></asp:Button>--%>
                    </div>
                </div>
                    <div class="clearfix">
                    </div><br />
                    <div class="col-xs-4">
                        <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="150px" style="display:inline; margin-right:25px;">
                            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
                            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
                        </asp:RadioButtonList> 
                        <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" style=" margin-top:-25px;"
                           OnClick="btnSearch_Click"  />
                    </div>
            </div>
            <div class="clearfix">
            </div>
             <div class="clearfix">
                    </div>
                     <div class="clearfix">
                    </div>
                     <div class="clearfix">
                    </div>
                     <div class="clearfix">
                    </div>
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix">
            </div>
             <div class="clearfix">
            </div>
        </div>
            <!-- /.box-body -->
            <div class="box-footer">
            </div>
             <div class="box-footer">
            </div>
        </div>
    </section>
    <!-- /.content -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
   
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>

