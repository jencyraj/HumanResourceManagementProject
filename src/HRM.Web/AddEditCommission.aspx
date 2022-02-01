<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
CodeFile="AddEditCommission.aspx.cs" Inherits="AddEditCommission" ValidateRequest="false" %>

<%@ Register src="~/UserControls/ctlCalendar.ascx" tagname="ctlCalendar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1 id="h1" runat="server"><%= hrmlang.GetString("addnewcommission")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> ><%= hrmlang.GetString("home")%></a></li>
            <li class="active" id="LI1" runat="server">><%= hrmlang.GetString("addnewcommission")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                      <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtEmployee"><%= hrmlang.GetString("employee")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmployee"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="txtTitle"><%= hrmlang.GetString("title")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                                <asp:TextBox ID="txtTitle" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAmount"><%= hrmlang.GetString("amount")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmount"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ctlCalendarCD"><%= hrmlang.GetString("commissiondate")%></label>                      
                            <uc1:ctlCalendar ID="ctlCalendarCD" runat="server" DefaultCalendarCulture="Grgorian" 
                                MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtDescription"><%= hrmlang.GetString("description")%></label>                        
                            <asp:TextBox ID="txtDescription" CssClass="editor1 form-control" runat="server" TextMode="MultiLine" style="height:200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10"> 
                        <div class="form-group">
                            <label for="txtAddInfo"><%= hrmlang.GetString("additionalinfo")%></label>                        
                            <asp:TextBox ID="txtAddInfo" CssClass="editor2 form-control" runat="server" TextMode="MultiLine" style="height:200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
            $(".editor2").wysihtml5();
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#<%=txtAmount.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
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

        });

    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>

