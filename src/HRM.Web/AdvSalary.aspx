<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AdvSalary.aspx.cs" Inherits="AdvSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("advancedsalary")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("advancedsalary")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <div style="margin: auto; float: left">
                        <div style="margin: auto; float: left; padding-right: 5px">
                            <asp:TextBox ID="txtEmployee" runat="server" Width="200px" CssClass="form-control"
                                Style="border-radius: 10px !important;"></asp:TextBox>
                        </div>
                        <div style="margin: auto; float: left">
                            <div style="margin: auto; float: left; padding-right: 5px">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search"
                                    CausesValidation="false" OnClick="btnSearch_Click" />
                            </div>
                            <div style="margin: auto; float: left; padding-right: 14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="Add New"
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
                    <asp:GridView ID="gvAdvSalary" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="AdvSalaryId" EnableViewState="True" AllowPaging="true" PageSize="15"
                        ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvAdvSalary_PageIndexChanging"
                        OnRowCommand="gvAdvSalary_RowCommand" OnRowDataBound="gvAdvSalary_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("FirstName") %><%# Eval("MiddleName") %><%# Eval("LastName") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="SalaryDate" HeaderText="Salary Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddEditAdvSalary.aspx?id=" + Eval("AdvSalaryId") %>'
                                        data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("AdvSalaryId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Advanced salary?')"
                                        CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
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
