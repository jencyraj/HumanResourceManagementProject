<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Attendance" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlCalendar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("manageattendance")%><small></small></h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
                <li class="active"><%= hrmlang.GetString("manageattendance")%></li>
            </ol>
        </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left;padding-right:5px">
                            <div class="form-group" style="padding-top:10px">
                                <div style="margin:auto;float:left;padding-right:5px"><label for="ddFilterBy"><%= hrmlang.GetString("filterby") %></label></div>
                                <div style="margin:auto;float:right;padding-right:5px">
                                <asp:Label ID="lblJr" runat="server" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddFilterBy" runat="server" CssClass="filtertoggle" onchange="Toggle(true);">
                                        <asp:ListItem Text="No Filter" Value="0" Selected="True" />
                                        <asp:ListItem Text="Date" Value="1" />
                                        <asp:ListItem Text="Month and Year" Value="2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div id="divDate" style="margin:auto;float:left;padding-right:5px">
                            <div class="form-group">
                                <div style="margin:auto;float:right;padding-right:5px">
                                    <uc1:ctlcalendar ID="ctlCalendarAD" runat="server" DefaultCalendarCulture="Grgorian" 
                                        MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                                </div>
                            </div>
                        </div>
                        <div id="divYM" style="margin:auto;float:left;padding-right:5px">
                            <div class="form-group">
                                <div style="margin:auto;float:right;padding-right:5px">
                                    <div style="margin:auto;float:left;padding-right:5px">
                                        <asp:TextBox ID="txtYear" MaxLength="4" placeholder="Year" runat="server" Width="100px" CssClass="form-control" 
                                            Style="border-radius: 10px !important;padding-right:5px"></asp:TextBox>
                                    </div>
                                    <div style="margin:auto;float:right;padding-right:5px;padding-top:10px">
                                        <asp:DropDownList ID="ddMonth" runat="server">
                                            <asp:ListItem Selected="True" Text="All" Value="0" />
                                            <asp:ListItem Text="Januuary" Value="1" />
                                            <asp:ListItem Text="February" Value="2" />
                                            <asp:ListItem Text="March" Value="3" />
                                            <asp:ListItem Text="April" Value="4" />
                                            <asp:ListItem Text="May" Value="5" />
                                            <asp:ListItem Text="June" Value="6" />
                                            <asp:ListItem Text="July" Value="7" />
                                            <asp:ListItem Text="August" Value="8" />
                                            <asp:ListItem Text="September" Value="9" />
                                            <asp:ListItem Text="October" Value="10" />
                                            <asp:ListItem Text="November" Value="11" />
                                            <asp:ListItem Text="December" Value="12" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="margin:auto;float:left;padding-right:5px">
                            <div class="form-group" style="padding-top:10px">
                                <div style="margin:auto;float:left;padding-right:5px"><label for="ddFilterBy"><%= hrmlang.GetString("attendancetype")%></label></div>
                                <div style="margin:auto;float:right;padding-right:5px">
                                    <asp:DropDownList ID="ddAttendanceType" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="margin:auto;float:left;padding-right:10px;">
                            <asp:TextBox ID="txtJEmployee" placeholder="Employee" runat="server" Width="200px" CssClass="form-control" Style="border-radius: 10px !important;"></asp:TextBox>
                            <asp:TextBox ID="txtEmployee" placeholder="Employee" runat="server" Width="200px" CssClass="form-control" Style="border-radius: 10px !important;"></asp:TextBox>
                        </div>
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:5px">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return Validate();" Text="Search" OnClick="btnSearch_Click"/>
                            </div>
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Attendance" CausesValidation="false" OnClick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvAttendance" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="AttendanceId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvAttendance_PageIndexChanging" OnRowCommand="gvAttendance_RowCommand"
                        OnRowDataBound="gvAttendance_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Employee" HeaderText="Employee" />
                            <asp:BoundField DataField="AttendanceType" HeaderText="Attendance Type" />
                            <asp:BoundField DataField="AttendanceDate" HeaderText="Attendance Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="SignInTime" HeaderText="SignIn Time" />
                            <asp:BoundField DataField="SignOutTime" HeaderText="Sign Out Time" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblIRIS" runat="server" Text='<%# ("" + Eval("IsIrisUpdated") == "1") ? "Yes" : "" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit"
                                         NavigateUrl='<%# "~/AddEditAttendance.aspx?id=" + Eval("AttendanceId") + "&isedit=1&fromapp=1" %>' data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("AttendanceId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Attendance?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView> 
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <script type="text/javascript">

        function Toggle(clearcontrols) {
            if(clearcontrols == true) {
                $('#<%=txtYear.ClientID%>').val('');
                $('#<%=lblErr.ClientID%>').text('');
                $('#<%=ddMonth.ClientID%>').get(0).selectedIndex = 0;
                document.getElementById('<%=ctlCalendarAD.FindControl("txtHijri").ClientID %>').value = "";
                document.getElementById('<%=ctlCalendarAD.FindControl("txtGreg").ClientID %>').value = "";
            }
            if (parseInt($(".filtertoggle").val()) == 0) {
                document.getElementById("divDate").style.display = 'none';
                document.getElementById("divYM").style.display = 'none';
            }
            else if (parseInt($(".filtertoggle").val()) == 1) {
                document.getElementById("divDate").style.display = '';
                document.getElementById("divYM").style.display = 'none';
            }
            else if (parseInt($(".filtertoggle").val()) == 2) {
                document.getElementById("divDate").style.display = 'none';
                document.getElementById("divYM").style.display = '';
            }
        }

        function Validate() {
            var ddoption = parseInt($(".filtertoggle").val());
            var messages = $("#<%=hfMessage.ClientID %>").val().split('~');
            if (ddoption == 1) {
                var datefilter = document.getElementById('<%=ctlCalendarAD.FindControl("txtGreg").ClientID %>').value;
                if (datefilter == '') {
                    $('#<%=lblErr.ClientID%>').text(messages[0]);
                    return false;
                }
                else {
                    return true;
                }
            }
            if (ddoption == 2) {
                if ($('#<%=txtYear.ClientID%>').val().length != 4 && $('#<%=txtYear.ClientID%>').val().length != 0) {
                    $('#<%=lblErr.ClientID%>').text(messages[1]);
                    return false;
                }
                else if ($('#<%=txtYear.ClientID%>').val().length == 0) {
                    $('#<%=lblErr.ClientID%>').text(messages[2]);
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        $(document).ready(function () {

            $('#<%=txtYear.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

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

             $("#<%=txtJEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetJuniorEmployees") %>',
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

            $('#<%=txtEmployee.ClientID%>').keydown(function (e) {
                if (e.keyCode == 13) {
                    if ($('#<%=btnSearch.ClientID%>').length > 0) {
                        var val = Validate();
                        if (val == true) {
                            <%= Page.ClientScript.GetPostBackEventReference(btnSearch, "")%>
                        }
                    }
                }
            });

        });
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfMessage" runat="server" />
</asp:Content>

