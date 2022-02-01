<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddEditHourlyWage.aspx.cs" Inherits="AddEditHourlyWage" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1 id="h1" runat="server">
            <%= hrmlang.GetString("addnewhourlywage")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active" id="LI1" runat="server">
                <%= hrmlang.GetString("addnewhourlywage")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                         <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtEmployee">
                                <%= hrmlang.GetString("designation")%></label>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <label for="txtEmployee">
                            <%= hrmlang.GetString("employee")%></label></div>
                    <div class="col-xs-2 hide">
                        <label for="txtmonth">
                            <%= hrmlang.GetString("startsfrom")%></label></div>
                    <div class="col-xs-2 hide">
                        <label for="txtyear">
                            <%= hrmlang.GetString("year")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation"
                            DataValueField="DesignationID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:Label ID="lblEmployeeReq" runat="server" CssClass="text-red" />
                        <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 hide">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                            DataValueField="MonthID" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2 hide">
                        <asp:TextBox ID="txtyear" runat="server" placeholder="Enter Year" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtRegularHours">
                                <%= hrmlang.GetString("regularhours")%></label>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <label for="txtOverTimeHours">
                            <%= hrmlang.GetString("overtimehours")%></label></div>
                    <div class="col-xs-3">
                        <label for="txtovertimeweeknd">
                            <%= hrmlang.GetString("overtimehourswk")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <asp:Label ID="lblRegHoursReq" runat="server" CssClass="text-red" />
                        <asp:TextBox ID="txtRegularHours" runat="server" placeholder="Enter Wage for Regular Hours"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3">
                        <asp:Label ID="lblOverTimeHoursReq" runat="server" CssClass="text-red" />
                        <asp:TextBox ID="txtOverTimeHours" runat="server" placeholder="Enter Wage for OverTime Hours"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-3">
                        <asp:Label ID="lblovertimeweknd" runat="server" CssClass="text-red" />
                        <asp:TextBox ID="txtovertimeweknd" runat="server" placeholder="Enter Wage for OverTime weekend/offday"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtAddInfo">
                                <%= hrmlang.GetString("additionalinfo")%></label>
                            <asp:TextBox ID="txtAddInfo" CssClass="editor1 form-control" runat="server" TextMode="MultiLine"
                                Style="height: 200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtValue">
                                <%= hrmlang.GetString("active")%></label>
                            <asp:CheckBox ID="chkActive" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="btnSave_Click"  />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                        Text="Cancel" OnClick="btn_Click" />
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="dvConfirm" tabindex="-1" deduction="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H7">
                        <%= hrmlang.GetString("commissionsavehourlywageheader")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7" style="width: 100% !important">
                        <label>
                            <%= hrmlang.GetString("commissionsavehourlywage")%></label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveDialog" runat="server" CssClass="btn btn-primary" OnClick="lnkPostBack_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close")%></button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
        });

        function ConfirmSave() {
            var validate = Validate();
            if (validate == true) {
                var IsHourlyWagePresent = $("#<%=hfIsHourlyWagePresent.ClientID %>").val();
                var IsNew = $("#<%=hfNew.ClientID %>").val();
                var IsActive = $("#<%=hfActive.ClientID %>").val();
                if (IsNew == '1' && IsActive == 'Y'){
                    if (IsHourlyWagePresent != '' && IsHourlyWagePresent != '0' && $('#<%=chkActive.ClientID %>').is(':checked') == true) {
                        $('#dvConfirm').modal();
                    }
                    else {
                        <%= Page.ClientScript.GetPostBackEventReference(lnkPostBack, "")%>
                    }        
                }
                else if (IsHourlyWagePresent == '' && $('#<%=chkActive.ClientID %>').is(':checked') == true && IsActive == 'N') {
                    $('#dvConfirm').modal();
                }
                else {
                    <%= Page.ClientScript.GetPostBackEventReference(lnkPostBack, "")%>
                }
            }
            return false;
        }

        function Save() {`
            <%= Page.ClientScript.GetPostBackEventReference(lnkPostBack, "")%>
            return false;
        }

        function Validate()
        {
            if ($('#<%=txtEmployee.ClientID%>').val() == '' && $('#<%=ddlDesgn.ClientID %> option:selected').val()=='') {
                $('#<%=lblEmployeeReq.ClientID%>').text('Required');
                return false;
            }
            else {
                $('#<%=lblEmployeeReq.ClientID%>').text('');
            }
            if ($('#<%=txtRegularHours.ClientID%>').val() == '') {
                $('#<%=lblRegHoursReq.ClientID%>').text('Required');
                return false;
            }
            else {
                $('#<%=lblRegHoursReq.ClientID%>').text('');
            }
            if ($('#<%=txtOverTimeHours.ClientID%>').val() == '') {
                $('#<%=lblOverTimeHoursReq.ClientID%>').text('Required');
                return false;
            }
            else {
                $('#<%=lblOverTimeHoursReq.ClientID%>').text('');
            }
            return true;
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var DesignationId = $('#<%=ddlDesgn.ClientID %> option:selected').val();
                    if (DesignationId == '') {
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
                    }
                    else {
                        $.ajax({
                            url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByDesignation") %>',
                            data: "{'prefix': '" + request.term + "', 'DesignationId': '" + DesignationId + "'}",
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
                    if ($('#<%=lnkActive.ClientID%>').length > 0) {
                        <%= Page.ClientScript.GetPostBackEventReference(lnkActive, "")%>        
                    }
                }
            });

            $('#<%=txtRegularHours.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $('#<%=txtOverTimeHours.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
            
             $('#<%=txtovertimeweknd.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });

    </script>
    <asp:HiddenField ID="hfNew" runat="server" />
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfActive" runat="server" />
    <asp:HiddenField ID="hfIsHourlyWagePresent" runat="server" />
    <asp:LinkButton ID="lnkPostBack" runat="server" OnClick="lnkPostBack_Click" />
    <asp:LinkButton ID="lnkActive" runat="server" OnClick="lnkActive_Click" />
</asp:Content>
