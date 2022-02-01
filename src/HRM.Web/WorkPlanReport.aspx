<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WorkPlanReport.aspx.cs" Inherits="WorkPlanReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:Label ID="lblWPID" runat="server" Visible="false"></asp:Label>
    <!-- content-header -->
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("workshedulerpt")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("workshedulerpt")%></li>
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
                <div class="pull-left rowmargin" style="height:30px">
                    <div class="col-lg-4" style="padding-right:7px; padding-right:0px">
                        <asp:TextBox ID="txtEmployee" CssClass="form-control" runat="server" placeholder="Select an Employee" onchange="cleartxt()"></asp:TextBox></div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                            DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                     
                        <asp:DropDownList ID="ddBranches" runat="server" CssClass="form-control" DataTextField="Select Branch">
                        </asp:DropDownList>
                              </div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <asp:TextBox ID="txtYear" runat="server" placeholder="Enter Year" CssClass="form-control" MaxLength="4" ></asp:TextBox>
                        <asp:RangeValidator ID="rgYear" runat="server" MinimumValue="2014" MaximumValue="2100" ControlToValidate="txtYear" 
                             ErrorMessage="Year should be in between 2014 and 2100!" ForeColor="Red"></asp:RangeValidator>
                    </div>
                    <div class="col-lg-2" style="padding-left:7px; padding-right:0px">
                        <%--<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Show"
                            CausesValidation="false" onclick="btnSearch_Click" />--%>
                       
                    </div>
                </div>
                  <div class="col-mg-12 rowmargin">
            <div class="col-xs-2">
             <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
        </asp:RadioButtonList>
        </div>
            <div class="col-xs-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />
            </div>
           
        </div>
                <div class="clearfix"></div>
                <div class="col-mg-12 rowmargin">
                   <%-- <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable" 
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
                    </asp:GridView>--%>
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

        function cleartxt() {
            var txtemp = $("#<%=txtEmployee.ClientID %>").val();
            if ("" + txtemp == "")
                $("#<%=hfEmployeeId.ClientID %>").val("");
        }
    </script>
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>

