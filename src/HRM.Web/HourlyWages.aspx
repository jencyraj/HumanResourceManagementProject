<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HourlyWages.aspx.cs" Inherits="HourlyWages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
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
            <%= hrmlang.GetString("hourlywages")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managehourlywages")%></li>
        </ol>
    </section>
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <div style="margin: auto; float: left">
                        <div style="width:250px;float: left;padding-right: 10px">
                            <b style="float: left;padding-right: 5px; margin-top:5px;"><%= hrmlang.GetString("designation") %></b>
                            <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" style="width:150px" DataTextField="Designation"
                                DataValueField="DesignationID">
                            </asp:DropDownList>
                        </div>
                    <div style="margin: auto; float: left; padding-right: 10px">
                        <asp:TextBox ID="txtEmployee" placeholder="Employee" runat="server" Width="200px"
                            CssClass="form-control" Style="border-radius: 10px !important;"></asp:TextBox>
                    </div>
                    <div style="margin: auto; float: left; padding-right: 5px">
                        <div class="form-group">
                            <div style="margin: auto; float: left; padding-right: 5px">
                                <label for="chkActive">
                                    <%= hrmlang.GetString("active") %></label></div>
                            <div style="margin: auto; float: right; padding-right: 5px">
                                <asp:CheckBox ID="chkActive" runat="server" /></div>
                        </div>
                    </div>
                    <div style="margin: auto; float: left">
                        <div style="margin: auto; float: left; padding-right: 5px">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search"
                                CausesValidation="false" OnClick="btnSearch_Click" ClientIDMode="Static"/>
                        </div>
                        <div style="margin: auto; float: left; padding-right: 14px">
                            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Hourly Wage"
                                CausesValidation="false" OnClick="btnNew_Click" />
                        </div>
                    </div>
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
            <div class="col-md-12 rowmargin">
                <asp:GridView ID="gvHourlyWages" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    DataKeyNames="HourlyWagesId" EnableViewState="True" AllowPaging="true" PageSize="15"
                    ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvHourlyWages_PageIndexChanging"
                    OnRowCommand="gvHourlyWages_RowCommand" OnRowDataBound="gvHourlyWages_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblEmployee" runat="server" Text='<%# Eval("FULLNAME") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblWageFor" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RegularHours" HeaderText="Regular Hours" />
                        <asp:BoundField DataField="OverTimeHours" HeaderText="Overtime Hours" />
                         <asp:BoundField DataField="OverTimeWekend" HeaderText="Overtime Weekend" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddEditHourlyWage.aspx?id=" + Eval("HourlyWagesId")+ "&isactive=" + Eval("ActiveWage")  %>'
                                    data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("HourlyWagesId") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"
                                    data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="clearfix">
            </div>
        </div>
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

            $('#<%=txtEmployee.ClientID%>').keydown(function (e) {
                if (e.keyCode == 13) {
                    if ($('#<%=btnSearch.ClientID%>').length > 0) {
                        <%= Page.ClientScript.GetPostBackEventReference(btnSearch, "")%>
                    }
                }
            });

        }); 
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>
