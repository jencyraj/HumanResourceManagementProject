<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageContracts.aspx.cs" Inherits="ManageContracts" ValidateRequest="false" %>

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
               <%= hrmlang.GetString("contracts")%> <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managecontracts") %></li>
        </ol>
    </section>
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin" style="width: 50% ! important;">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Text"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" DataTextField="ContractTypeName"
                            DataValueField="CTID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4" style="margin: auto; float: left; padding-right: 14px">
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnSearch_Click"  ClientIDMode="Static"/>
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Contract"
                            CausesValidation="false" OnClick="btnNew_Click" />
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvContracts" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="ContractId" EnableViewState="True" AllowPaging="true" PageSize="15"
                        OnPageIndexChanging="gvContracts_PageIndexChanging" OnRowCommand="gvContracts_RowCommand"
                        OnRowDataBound="gvContracts_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="ContractTypeName" HeaderText="Contract Type" />
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Wrap="true"  />
                            <asp:TemplateField HeaderText="Document">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkDocName" runat="server" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <ItemTemplate >
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" NavigateUrl='<%# "AddContracts.aspx?id=" + Eval("ContractId") %>'
                                        data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("ContractId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Document?')"
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
    </script>
    <asp:HiddenField ID="hfContractId" runat="server" />
</asp:Content>
