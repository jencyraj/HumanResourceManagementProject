<%@ Page Title="HRM :: Employee Transfer" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="AddTransfer.aspx.cs" Inherits="AddTransfer" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblTransferID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1 id="h1" runat="server">
            <%= hrmlang.GetString("empltransfer") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active" id="LI1" runat="server"><%= hrmlang.GetString("empltransfer") %></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
                    <div class="box-body">
                        <div class="clearfix">
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEmployee">
                                        <%= hrmlang.GetString("employee") %></label>
                                    <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"
                                        AutoPostBack="true" OnTextChanged="txtEmployee_TextChanged"></asp:TextBox> 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label for="ddlForward">
                                            <%= hrmlang.GetString("forwardto") %></label>
                                        <asp:DropDownList ID="ddlForward" runat="server" CssClass="form-control" DataTextField="FullName"
                                            DataValueField="SuperiorID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ctlCalendarBD">
                                        <%= hrmlang.GetString("requestdate") %></label>
                                    <uc1:ctlDOB ID="ctlCalendarBD" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                                        MinYearCountFromNow="-1" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlBranch">
                                        <%= hrmlang.GetString("branchto") %></label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                                        DataValueField="BranchID" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlDept">
                                        <%= hrmlang.GetString("deptto") %></label>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                        DataValueField="DepartmentID" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlSubDept">
                                        <%= hrmlang.GetString("subdeptto") %></label>
                                    <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                        DataValueField="DepartmentID">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtReport">
                                        <%= hrmlang.GetString("reportto") %></label>
                                   <asp:TextBox ID="txtReportTo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                                Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                    <asp:HiddenField ID="hfReportId" runat="server" />
 
        </div>
    </section>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
   
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployees") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfEmployeeId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });

            //REPORT TO STARTS
            $("#<%=txtReportTo.ClientID %>").autocomplete({
                source: function (request, response) {
                    var BranchID = $("#<%=ddlBranch.ClientID%>").val();
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByBranchID") %>',
                        data: "{ 'prefix': '" + request.term + "', 'BranchID': '" + BranchID + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfReportId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });
            //REPORT TO ENDS
        });
    </script>
</asp:Content>
