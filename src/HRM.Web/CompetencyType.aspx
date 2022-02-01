<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CompetencyType.aspx.cs" Inherits="CompetencyType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyTypeID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("competencytypes") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mcompetencytypes") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                        Text="New CompetencyType" CausesValidation="false" OnClick="btnNew_Click" /></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvCompetencyType" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvCompetencyType_RowCommand" DataKeyNames="CompetencyTypeID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvCompetencyType_PageIndexChanging"
                        OnRowDataBound="gvCompetencyType_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="CompetencyType" HeaderText="Competency Type" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("CompetencyTypeID") %>'
                                        CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("CompetencyTypeID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Competency Type?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
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
                            <label for="txtCompetencyType">
                                <%= hrmlang.GetString("competencytype") %></label>
                            <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtCompetencyType"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtCompetencyType" runat="server" CssClass="form-control" placeholder="Enter CompetencyType"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
