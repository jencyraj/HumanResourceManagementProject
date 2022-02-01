<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ResignationApdRpt.aspx.cs" Inherits="EmployeeTimeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
           Resignation Approval<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
               Manage Resignation Approval</li>
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
                         <label for="ddBranches">
                            Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                          <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Y"></asp:ListItem>
                           <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                             
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                       
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-md-2 hide">
                        <br />
                        <asp:CheckBox ID="chkBreak" runat="server" />
                        <%= hrmlang.GetString("showbreakhour")%>
                    </div>
                    <div class="col-md-3 hide">
                        <br />
                        <asp:CheckBox ID="chkOverTime" runat="server" /><%= hrmlang.GetString("showovertime")%>
                    </div>
                    <div class="col-xs-7">
                        <br />
                        <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            Style="display: inline;">
                            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
                            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" 
                          Text="GO" OnClick="btnSearch_Click" Style="margin-bottom: 14px;
                            margin-left: 8px;" />

                            <asp:Button ID="btnresignatn" runat="server" 
                            CssClass="btn btn-primary btn-sm" Style="margin-bottom: 14px;
                            margin-left: 10px;" Text="Resignation Cancel Report" onclick="btnresignatnclick" />
                            <!--OnClick="btnlate_Click" -->
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
            </div>
        </div>
    </section>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
    <style type="text/css">
        .ui-widget
        {
            font-size: 0.9em !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var BranchId = document.getElementById('MainContent_ddBranches').value;
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
