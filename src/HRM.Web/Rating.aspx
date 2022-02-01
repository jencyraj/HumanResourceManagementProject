<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Rating.aspx.cs" Inherits="Rating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblRatingID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("ratingmaster") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mratings") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                        Text="New Rating" CausesValidation="false" OnClick="btnNew_Click" /></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvRating" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvRating_RowCommand" DataKeyNames="RatingID" EnableViewState="True"
                        AllowPaging="true" PageSize="20" OnPageIndexChanging="gvRating_PageIndexChanging"
                        OnRowDataBound="gvRating_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="RatingDesc" HeaderText="Rating" />
                            <asp:BoundField DataField="MaxScore" HeaderText="Max. Score" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("RatingID") %>'
                                        CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("RatingID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Rating?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlNew" runat="server">
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtRating">
                                    <%= hrmlang.GetString("description") %></label>
                                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtRating"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtRating" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtScore">
                                    <%= hrmlang.GetString("maxscore") %></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScore"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtScore" Operator="DataTypeCheck"
                                    Type="Integer" ErrorMessage="Invalid Number" CssClass="text-red"></asp:CompareValidator>
                                <asp:TextBox ID="txtScore" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                            </div>
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
