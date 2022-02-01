<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AttendanceApproval.aspx.cs" Inherits="AttendanceApproval" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("attendanceapproval")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("attendanceapproval") %></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12 rowmargin">
                    <div id="divBranch" runat="server" class="col-md-2">
                        <label for="ddBranches">
                            <%= hrmlang.GetString("branches") %></label>
                        <asp:DropDownList ID="ddBranches" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label for="txtEmployee">
                            <%= hrmlang.GetString("employee")%></label>
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label for="ddApprovalStatus">
                            <%= hrmlang.GetString("status") %></label>
                        <asp:DropDownList ID="ddApprovalStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="" Selected="True" />
                            <asp:ListItem Text="Pending Approval" Value="P" />
                            <asp:ListItem Text="Approved" Value="Y" />
                            <asp:ListItem Text="Rejected" Value="N" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="ddFilterBy">
                            <%= hrmlang.GetString("type") %></label><br />
                        <asp:DropDownList ID="ddAttendanceType" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="ddFilterBy">
                            <%= hrmlang.GetString("additionalfilters")%></label>
                        <asp:DropDownList ID="ddFilterBy" runat="server" CssClass="form-control filtertoggle"
                            onchange="Toggle(true);">
                            <asp:ListItem Text="Today" Value="0" Selected="True" />
                            <asp:ListItem Text="Date" Value="1" />
                            <asp:ListItem Text="Month and Year" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div id="divDate" class="col-md-3">
                        <br />
                        <uc1:ctlCalendar ID="ctlCalendarAAD" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                    </div>
                    <div id="divYM" class="col-md-4">
                        <br />
                        <asp:TextBox ID="txtYear" MaxLength="4" runat="server" CssClass="form-control" Style="width: 150px;
                            margin-right: 20px; display: inline;"></asp:TextBox>
                        <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control" Style="width: 145px;
                            display: inline;" DataTextField="MonthName" DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1" style="margin-top: 25px;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return Validate();"
                            Text="GO" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            DataKeyNames="AttendanceId" OnRowDataBound="gvAttendance_RowDataBound" 
                            AllowPaging="true" PageSize="40" 
                            onpageindexchanging="gvAttendance_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfAttendanceId" runat="server" Value='<%# Eval("AttendanceId") %>' />
                                        <asp:Label ID="lblEmployee" runat="server" Text='<%# Eval("Employee") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AttendanceDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="SignInTime" />
                                <asp:BoundField DataField="SignOutTime" />
                                <asp:BoundField DataField="BreakHours" />
                                <asp:BoundField DataField="OverTime" />
                                <asp:BoundField DataField="AdditionalInfo" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%# ("" + Eval("Isirisupdated") == "1") ? "Yes" : "No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHSelect" CssClass="hcadd" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" CssClass="cadd" runat="server" Style="font-weight: normal !important;
                                            font-family: sans-serif; font-size: 12px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkView" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddEditAttendance.aspx?id=" + Eval("AttendanceId") + "&isedit=0" %>'
                                            data-toggle="tooltip"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary btn-sm" OnCommand="btn_Command"
                        CommandName="APPROVE" />
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary btn-sm" OnCommand="btn_Command"
                        CommandName="REJECT" />
                    <div class="fr-right">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Attendance"
                            CausesValidation="false" onclick="btnNew_Click" /></div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="divConfirmReject" tabindex="-1" deduction="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H1">
                        <%= hrmlang.GetString("confirmreject") %></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7" style="width: 100% !important">
                        <label>
                            <%= hrmlang.GetString("rejectreason") %></label>
                        <asp:Label ID="lblTitleRejectReason" runat="server" CssClass="text-red" />
                        <asp:TextBox ID="txtRejectReason" CssClass="form-control" runat="server" TextMode="MultiLine"
                            Style="height: 200px"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDeleteOvertime" runat="server" CssClass="btn btn-primary" OnClientClick="return Reject();" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("cancel") %></button>
                </div>
            </div>
        </div>
    </div>
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
    <script type="text/javascript">

        function Toggle(clearcontrols) {
            if(clearcontrols == true) {
                $('#<%=txtYear.ClientID%>').val('');
                $('#<%=lblErr.ClientID%>').text('');
                $('#<%=ddMonth.ClientID%>').get(0).selectedIndex = 0;
                document.getElementById('<%=ctlCalendarAAD.FindControl("txtHijri").ClientID %>').value = "";
                document.getElementById('<%=ctlCalendarAAD.FindControl("txtGreg").ClientID %>').value = "";
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
                var datefilter = document.getElementById('<%=ctlCalendarAAD.FindControl("txtGreg").ClientID %>').value;
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
                    var RoleId = $("#<%=hfRoleId.ClientID%>").val();
                    if (RoleId == '') {
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
                    }
                    else {
                        var BranchId = parseInt($(".branchcss").val());
                        $.ajax({
                            url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByBranchID") %>',
                            data: "{'prefix': '" + request.term + "', 'BranchID': '" + BranchId + "'}",
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
                    }
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

        function ConfirmReject() {
            $('#divConfirmReject').modal();
        }

        function Reject() {
            if ($('#<%=txtRejectReason.ClientID%>').val() == '') {
                $('#<%=lblTitleRejectReason.ClientID%>').text($("#<%=hfMessage.ClientID %>").val().split('~')[3]);
            }
            else {
                <%= Page.ClientScript.GetPostBackEventReference(lnkPostBack, "")%>    
            }
            return false;
        }
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfRoleId" runat="server" />
    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:LinkButton ID="lnkPostBack" runat="server" OnClick="lnkPostBack_Click" />
</asp:Content>
