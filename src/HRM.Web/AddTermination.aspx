<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddTermination.aspx.cs" Inherits="AddTermination" ValidateRequest="false" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlClosingDate" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblTID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("newemployeetermination") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageemployeetermination")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-right rowmargin" visible="false">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                            Text="New Department" CausesValidation="false" OnClick="btnNew_Click" /></div>
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
                            <label for="txtCode">
                                <%= hrmlang.GetString("employeename") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" placeholder="" style="display:inline;margin-right:15px;" width="250px"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("forwardto")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-7">
                         <asp:TextBox ID="txtForwardTo" runat="server" CssClass="form-control" placeholder="" style="display:inline;" width="250px"></asp:TextBox>
                         <input type="checkbox" id="chkSelect" /><%=  hrmlang.GetString("reportingofficers") %>
                        </div>
                        </div>
                           
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtQualification">
                                    <%= hrmlang.GetString("description")%></label>
                                <asp:TextBox ID="txtReason" CssClass="editor1 form-control" runat="server"
                                    TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return validatectrl();" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>

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

                $("#<%=txtForwardTo.ClientID %>").autocomplete({
                    source: function (request, response) {
                        if ($("#chkSelect").is(':checked')) {
                            var empid = $("#<%=hfEmployeeId.ClientID %>").val();
                            $.ajax({
                                url: '<%=ResolveUrl("~/ajaxservice.asmx/GetSuperiors") %>',
                                data: "{ 'prefix': '" + request.term + "','id':'" + empid + "'}",
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
                    },
                    select: function (e, i) {
                        $("#<%=hfForwardTo.ClientID %>").val(i.item.val);
                    },
                    minLength: 1
                });

            });

            function validatectrl()
            {
                var empid = $("#<%=hfEmployeeId.ClientID %>").val();
                var fwid = $("#<%=hfForwardTo.ClientID %>").val();
                if (empid == fwid) {
                    alert('Employee Name and Forward To should not be same');
                   //("#<%=hfForwardTo.ClientID %>").val('');
//                    $("#<%=hfEmployeeId.ClientID %>").val('');
                    return false;
                }

                return true;
            }

    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfForwardTo" runat="server" />

</asp:Content>
