<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkPlan.aspx.cs" Inherits="WorkPlan" %>

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
    <asp:Label ID="lblWPID" runat="server" Visible="false"></asp:Label>
    <!-- content-header -->
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("workschedules")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("manageworkschedules")%></li>
        </ol>
    </section>
    <!-- content-header -->
    <!-- Main content -->
    <section class="content">
     <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
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
                <div class="pull-right rowmargin" style="height:30px">
                
                </div>
                 <div class="col-lg-2" style="padding-left:17px; padding-right:0px">
                      <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control branchcss" 
                            DataTextField="Branch" DataValueField="BranchID"  
                        >
                        </asp:DropDownList></div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:TextBox ID="txtEmployee" CssClass="form-control" runat="server" placeholder="Select an Employee" onchange="cleartxt()"></asp:TextBox></div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                            DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:TextBox ID="txtYear" runat="server" placeholder="Enter Year" CssClass="form-control" MaxLength="4" ></asp:TextBox>
                        <asp:RangeValidator ID="rgYear" runat="server" MinimumValue="2014" MaximumValue="2100" ControlToValidate="txtYear" 
                             ErrorMessage="Year should be in between 2014 and 2100!" ForeColor="Red"></asp:RangeValidator>
                    </div>
                    <div class="col-lg-4" style="padding-left:7px; padding-right:0px">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search"
                            CausesValidation="false" onclick="btnSearch_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="Add Work Schedule"
                            CausesValidation="false" OnClick="btnNew_Click" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable" 
                        onrowdatabound="gvPlan_RowDataBound" onrowcommand="gvPlan_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Employee" DataField="fullname" />
                            <asp:BoundField HeaderText="Year" DataField="WPYear"/>
                            <asp:TemplateField HeaderText="Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("WPMonth") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />                            
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "~/AddWorkPlan.aspx?id=" + Eval("WPMID") %>'
                                    data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("WPMID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
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
