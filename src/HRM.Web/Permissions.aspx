<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Permissions.aspx.cs" Inherits="Permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        $(function () {

            "use strict";

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });

            //When unchecking the checkbox
            $(".hcadd").on('ifUnchecked', function (event) {
                //Uncheck all checkboxes
                $(".cadd", ".dataTable").iCheck("uncheck");
            });
            //When checking the checkbox
            $(".hcadd").on('ifChecked', function (event) {
                //Check all checkboxes
                $(".cadd", ".dataTable").iCheck("check");
            });

            //When unchecking the checkbox
            $(".hcedit").on('ifUnchecked', function (event) {
                //Uncheck all checkboxes
                $(".cedit", ".dataTable").iCheck("uncheck");
            });
            //When checking the checkbox
            $(".hcedit").on('ifChecked', function (event) {
                //Check all checkboxes
                $(".cedit", ".dataTable").iCheck("check");
            });

            //When unchecking the checkbox
            $(".hcdelete").on('ifUnchecked', function (event) {
                //Uncheck all checkboxes
                $(".cdelete", ".dataTable").iCheck("uncheck");
            });
            //When checking the checkbox
            $(".hcdelete").on('ifChecked', function (event) {
                //Check all checkboxes
                $(".cdelete", ".dataTable").iCheck("check");
            });

            //When unchecking the checkbox
            $(".hcview").on('ifUnchecked', function (event) {
                //Uncheck all checkboxes
                $(".cview", ".dataTable").iCheck("uncheck");
            });
            //When checking the checkbox
            $(".hcview").on('ifChecked', function (event) {
                //Check all checkboxes
                $(".cview", ".dataTable").iCheck("check");
            });

        });
        </script>
    <asp:Label ID="lblRoleID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("userrolepermissions") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">
                <%= hrmlang.GetString("managepermissions") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
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
                    <div class="col-xs-3">
                        <label for="ddlRole">
                            <%= hrmlang.GetString("role") %></label></div>
                    <div class="col-xs-3">
                        <label for="ddlLang">
                            <%= hrmlang.GetString("language") %></label></div>
                    <div class="col-xs-3">
                        <label for="txtEmployee">
                            <%= hrmlang.GetString("employee") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control txtround" DataTextField="RoleName"
                            DataValueField="RoleID" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlLang" runat="server" CssClass="form-control txtround" DataTextField="LangName"
                            DataValueField="LangCultureName" AutoPostBack="true" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control txtround" placeholder="Employee"
                            Width="250px"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" 
                            CssClass="btn btn-primary" onclick="btnSearch_Click" />
                    </div>
                </div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvPermissions" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        EnableViewState="True" AllowPaging="true" PageSize="30" OnPageIndexChanging="gvPermissions_PageIndexChanging"
                        OnRowDataBound="gvPermissions_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="Module">
                                <ItemTemplate>
                                    <asp:Label ID="lblParentModName" runat="server" Text='<%# Eval("PARENTNAME") %>'
                                        Visible='<%# (Eval("ModuleID").ToString() == Eval("ParentModuleID").ToString()) ? false : true %>'></asp:Label>
                                    <span class="text-green" style='<%# (Eval("ModuleID").ToString() == Eval("ParentModuleID").ToString()) ? "display:none": "" %>'>
                                        =></span>
                                    <asp:Label ID="lblModName" runat="server" Text='<%# Eval("ModuleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHAdd" CssClass="hcadd" runat="server" Text="Insert" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" CssClass="cadd" runat="server" Style="font-weight: normal !important;
                                        font-family: sans-serif; font-size: 12px;" Checked='<%# ("" + Eval("AllowInsert") == "Y") ? true : false %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHEdit" CssClass="hcedit" runat="server" Text="Edit" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEdit" CssClass="cedit" runat="server" Style="margin-left: 25px;
                                        font-weight: normal !important; font-family: sans-serif; font-size: 12px;" Checked='<%# ("" + Eval("AllowUpdate") == "Y") ? true : false %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHDelete" CssClass="hcdelete" runat="server" Text="Delete" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDelete" CssClass="cdelete" runat="server" Style="margin-left: 5px;
                                        font-weight: normal !important; font-family: sans-serif; font-size: 12px;" Checked='<%# ("" + Eval("AllowDelete") == "Y") ? true : false %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHView" CssClass="hcview" runat="server" Text="View" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkView" CssClass="cview" runat="server" Style="margin-left: 25px;
                                        font-weight: normal !important; font-family: sans-serif; font-size: 12px;" Checked='<%# ("" + Eval("AllowView") == "Y") ? true : false %>' />
                                    <asp:Label ID="lblModuleID" runat="server" Visible="false" Text='<%# Eval("ModuleID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                        Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Visible="false"
                        Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var RoleID = $("#<%=ddlRole.ClientID%>").val();
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesbyRoleID") %>',
                        data: "{ 'prefix': '" + request.term + "', 'RoleID': '" + RoleID + "'}",
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
