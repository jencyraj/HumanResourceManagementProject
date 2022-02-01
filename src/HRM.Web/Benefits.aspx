<%@ Page Title="Employee Benefits" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Benefits.aspx.cs" Inherits="Benefits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("ebenefits") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mebenefits") %></li>
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
                    <div class="col-xs-4">
                        <label for="txtEmp">
                            <%= hrmlang.GetString("employee")%></label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtEmp" ErrorMessage="Required"
                            CssClass="text-red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control txtround"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        <asp:Button ID="btnShow" runat="server" Text="Search" 
                            CssClass="btn btn-primary" onclick="btnShow_Click" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvPackage" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvPackage_RowCommand" EnableViewState="True" OnRowDataBound="gvPackage_RowDataBound" Width="70%" >
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblBFTID" runat="server" Text='<%# Eval("BFTID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblBFType" runat="server" Text='<%# Eval("BFType") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlBfType" runat="server">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                     <asp:Label ID="lblAvail" runat="server" Text='<%# ("" + Eval("AvailedBy") == "M") ? hrmlang.GetString("permonth") : ( ("" + Eval("AvailedBy") == "Y") ? hrmlang.GetString("peryear") : "") %>' ></asp:Label>
                                    <asp:Label ID="lblAvailed" runat="server" Text='<%# Eval("AvailedBy") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpShare" runat="server" Text='<%# Eval("EmployeeAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEmpShare" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgShare" runat="server" Text='<%# Eval("Org_Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOrgShare" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("BFTID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("BFTID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')"
                                        CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" CommandName="ADDNEW" />
                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CommandName="RESET" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-mg-12 rowmargin" id="dvRemarks" runat="server" visible="false">
                    <label for="txtRemarks">
                        <%= hrmlang.GetString("additionalinfo")%></label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Width="70%"></asp:TextBox>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"  visible="false"/>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" visible="false"
                        Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <asp:Label ID="lblRowID" runat="server" Visible="false"></asp:Label>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
    <style type="text/css">
        .ui-widget
        {
            font-size: 0.9em !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtEmp.ClientID %>").autocomplete({
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
