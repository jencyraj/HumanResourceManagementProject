<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PayrollPeriod.aspx.cs" Inherits="PayrollPeriod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
        <h1><%= hrmlang.GetString("payrollperiod")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managepayrollperiod")%></li>
        </ol>
    </section>
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left;padding-right:5px">
                            <asp:TextBox ID="txtYear" MaxLength="4" placeholder="Year" runat="server" Width="100px" CssClass="form-control" 
                                 Style="border-radius: 10px !important;padding-right:5px"></asp:TextBox>
                        </div>
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:5px">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClientClick="return Validate();" CausesValidation="false" onclick="btnSearch_Click" ClientIDMode="Static"/>
                            </div>
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Payroll Period" CausesValidation="false" onclick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvPayrollPeriod" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="PPId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvPayrollPeriod_PageIndexChanging" OnRowCommand="gvPayrollPeriod_RowCommand"
                        OnRowDataBound="gvPayrollPeriod_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:TemplateField HeaderText="Start Period">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartPeriod" runat="server" Text='<%# Eval("DayStart") + "/" + Eval("MonthStart") + "/" + Eval("YearStart") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Period">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndPeriod" runat="server" Text='<%# Eval("DayEnd") + "/" + Eval("MonthEnd") + "/" + Eval("YearEnd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit"
                                        NavigateUrl='<%# "~/AddEditPayrollPeriod.aspx?id=" + Eval("PPId") %>' data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("PPId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Payroll period')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView> 
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        
        $(document).ready(function () {

            $('#<%=txtYear.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });

        function Validate() {
            if ($('#<%=txtYear.ClientID%>').val().length != 4 && $('#<%=txtYear.ClientID%>').val().length != 0) {
                $('#<%=lblErr.ClientID%>').text('Not a valid year');
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>

