<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LeaveApproval.aspx.cs" Inherits="LeaveApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<script type="text/javascript">
    $('body').ajaxStart(function () {
        $('#spinner').show();
    });

    $('body').ajaxComplete(function () {
        $('#spinner').hide();
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnSearch').click(function () {
            //$("#spinner").append('<img id="img-spinner" src="Images/ajax-loader-test.gif" alt="Loading.." style="position: absolute; z-index: 200; left:50%; top:50%; " />');
            $('#spinner').show().fadeIn(20);
        });
    });
</script>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("leaveapprovals")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("leaveapprovals")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <%-- <div class="box-header">
            <h3 class="box-title">
                Company Profile</h3>
        </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control txtround" Width="150px"
                        Style="margin-right: 15px; display:inline;"></asp:TextBox>
                    <strong>
                        <%= hrmlang.GetString("approvalstatus")%></strong>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                        CssClass="form-control txtround" Width="100px" Style="margin-right: 15px;display:inline;">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="P" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="Rejected" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ClientIDMode="Static" /></div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvLeave" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        AllowPaging="true" PageSize="25" OnPageIndexChanging="gvLeave_PageIndexChanging"
                        OnRowCommand="gvLeave_RowCommand" OnRowDataBound="gvLeave_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="fullname" />
                            <asp:BoundField HeaderText="Applied Date" DataField="CreatedDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="No. of Days" DataField="TotalDays" />
                            <asp:BoundField HeaderText="Reason" DataField="Reason" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblLeaveRuleMonth" runat="server" Text='<%# Eval("Lmon") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblLeaveRuleYear" runat="server" Text='<%# Eval("LYR") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblEmpID" runat="server" Text='<%# Eval("EmployeeID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkApprove" runat="server" CssClass="fa fa-thumbs-up" CommandArgument='<%# Eval("LeaveID") %>'
                                        CommandName="APPROVE" OnClientClick="return confirm('Are you sure to approve this Leave Application?')"
                                        CausesValidation="false" data-toggle="tooltip" title="Approve"></asp:LinkButton>
                                    <asp:Label ID="lnkReject" runat="server" CssClass="fa fa-thumbs-down" data-toggle="tooltip"
                                        title="Reject" OnClick='<%# String.Concat("RejectReasonPopup(", Eval("LeaveID"), ")") %>'
                                        Style="cursor: pointer;"></asp:Label>
                                    <asp:HyperLink ID="lnkView" data-toggle="tooltip" title="View" runat="server" CssClass="fa fa-eye"
                                        NavigateUrl='<%# String.Concat("LeaveApplication.aspx?leaveid=",Eval("LeaveID")) %>'
                                        CausesValidation="false"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LeaveID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Leave Application?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
    <script type="text/javascript">
        function RejectReasonPopup(leaveid) {
            var hdpost = document.getElementById('MainContent_hdPostBack');
            hdpost.value = leaveid;
            // alert(hdpost.value);
            $('#dvReason').modal();
        }
    </script>
    <div class="modal fade" id="dvReason" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true" style="z-index: 100000;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H8">
                        <%= hrmlang.GetString("reasonforrejection")%><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ControlToValidate="txtReason" ValidationGroup="edulevel" ErrorMessage="Required"
                            ForeColor="Red"></asp:RequiredFieldValidator></h4>
                </div>
                <br />
                <div class="col-xs-8">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" placeholder="Reason"
                        ValidationGroup="edulevel" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Submit"
                        ValidationGroup="edulevel" OnClick="btnReject_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdPostBack" runat="server" Value="0" />
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
