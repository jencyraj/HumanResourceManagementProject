<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LoginHistory.aspx.cs" Inherits="LoginHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLeaveTypeID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("loginhistory")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("loginhistory")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12 rowmargin">
                    <div class="col-xs-3">
                        <strong>
                            <%= hrmlang.GetString("branch") %></strong>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control txtround" DataTextField="Branch" DataValueField="BranchID">
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">
                    </div><br />
                    <div class="col-xs-3">
                        <strong>
                            <%= hrmlang.GetString("employee") %></strong>
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control txtround"></asp:TextBox>
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
                            OnClick="btnSearch_Click" />
                    </div>
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
        </div>
            <!-- /.box-body -->
            <div class="box-footer">
            </div>
        </div>
    </section>
    <!-- /.content -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmployee.ClientID %>").autocomplete({
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
                    $("#<%=hfEmployeeId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });
        }); 
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>
