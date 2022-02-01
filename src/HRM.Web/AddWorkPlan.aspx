<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddWorkPlan.aspx.cs" Inherits="AddWorkPlan" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="CalendarCtrl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblWPMID" runat="server" Visible="false"></asp:Label>
    <!-- content-header -->
    <section class="content-header">
        <h1>
            Work Schedules<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Manage Work Schedules</li>
        </ol>
    </section>
    <!-- content-header -->
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <!-- /.box-body -->
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                 <div class="col-md-2">
                        <label for="ddlBranch">
                            Branch</label><%--&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                             ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-md-4">
                        <label for="txtEmployee">
                            Employee</label>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvemp" runat="server" ControlToValidate="txtEmployee"
                             ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="txtYear">
                            Year</label>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvyear" runat="server" ControlToValidate="txtYear"
                             ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><asp:CompareValidator ID="cmpYear" runat="server" ControlToValidate="txtYear"
                                Type="Integer" Operator="GreaterThanEqual" ValueToCompare="2014" ErrorMessage="Invalid!"
                                ForeColor="Red"></asp:CompareValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="txtYear">
                            Month</label>
                    </div>
                   
                    <div class="clearfix">
                    </div>
                    <div class="col-md-2">
                      <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control branchcss" 
                            DataTextField="Branch" DataValueField="BranchID" 
                            >
                        </asp:DropDownList></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmployee" runat="server" placeholder="Select an Employee" CssClass="form-control"></asp:TextBox></div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtYear" runat="server" placeholder="Enter Year" CssClass="form-control" MaxLength="4"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                            DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
                     <!--onselectedindexchanged="ddlBranch_SelectedIndexChanged"-->
                </div>
                <div class="clearfix">
                </div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPlan_RowDataBound"
                        OnRowCommand="gvPlan_RowCommand" Width="75%" CssClass="table table-bordered table-striped dataTable">
                        <Columns>
                            <asp:TemplateField HeaderText="Work Shift Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblWSType" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrom" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTo" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblWPID" runat="server" Text='<%# Eval("WPID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblWSID" runat="server" Text='<%# Eval("WSID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("WPMID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="row rowmargin">
                        <div class="col-md-3">
                            <label for="txtEmployee">
                                Work Shift Type</label>
                        </div>
                        <div class="col-md-3">
                            <label for="txtYear">
                                From Date</label>
                        </div>
                        <div class="col-md-3">
                            <label for="txtYear">
                                To Date</label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-md-3">
                             <asp:DropDownList ID="ddlType" runat="server" DataTextField="WorkShiftName" DataValueField="WSID"
                                        CssClass="form-control">
                                    </asp:DropDownList></div>
                        <div class="col-md-3">
                            <uc1:CalendarCtrl ID="txtFrom" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <uc1:CalendarCtrl ID="txtTo" runat="server" />
                        </div>
                         <div class="col-md-2">
                         <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-sm btn-primary" 
                                 onclick="btnAdd_Click" />
                         </div>
                    </div>
                    <span style="float: right; margin-right: 275px">
                        <asp:LinkButton Visible="false" ID="lnkAdd" runat="server" OnClick="lnkAdd_Click">Add New Row</asp:LinkButton></span>
                    <br />
                    <br />
                    <br />
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" CausesValidation="true" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" CausesValidation="false"
                        OnClick="btnCancel_Click" />
                         <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" 
                         CausesValidation="false" onclick="btnNew_Click"/>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box-primary -->
    </section>
    <!-- Main content -->
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var BranchId = parseInt($(".branchcss").val());
                    $.ajax({

                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByBranchID") %>',
                        data: "{ 'prefix': '" + request.term + "', 'BranchID': '" + BranchId + "'}",
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

        function cleartxt() {
            var txtemp = $("#<%=txtEmployee.ClientID %>").val();
            if ("" + txtemp == "")
                $("#<%=hfEmployeeId.ClientID %>").val("");
        }
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>
