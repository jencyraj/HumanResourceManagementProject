<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Complaints.aspx.cs" Inherits="Complaints" %>

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
            Manage Complaints<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Manage Complaints</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <label for="txtEmployee">
                        Complaint By</label>
                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" placeholder="Complaint By" style="display:inline;margin-right:15px;" width="250px"></asp:TextBox>
                    <label for="txtAgainst">
                        Against</label>
                    <asp:TextBox ID="txtAgainst" runat="server" CssClass="form-control" placeholder="Complaint Against" style="display:inline;" width="250px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm"
                        OnClick="btnSearch_Click"  ClientIDMode="Static"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        OnClick="btnCancel_Click" />
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Complaint"
                        CausesValidation="false" OnClick="btnNew_Click" />
                </div>
                <div class="clearfix">
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvComplaint" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            OnRowCommand="gvComplaint_RowCommand" OnRowDataBound="gvComplaint_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Employee Code" DataField="EmpCode" />
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                        <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Complaint Title" DataField="ComplaintTitle" />
                               <asp:BoundField HeaderText="Created Date" DataField="CreatedDate" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("ComplaintID") %>'
                                            CommandName="EDITDT" NavigateUrl='<%# String.Concat("~/AddComplaint.aspx?ComplaintID=", Eval("ComplaintID")) %>'
                                            data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("ComplaintID") %>'
                                            CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Complaint?')"
                                            CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
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

            $("#<%=txtAgainst.ClientID %>").autocomplete({
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
                    $("#<%=hfAgainst.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });
        }); 
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfAgainst" runat="server" />
</asp:Content>
