<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AppraisalCompetency.aspx.cs" Inherits="AppraisalCompetency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("appraisalcompetency") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mappraisalcompetency") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="rowmargin pull-right">
                    <strong>
                        <asp:Label ID="lblBranch" runat="server" Text="Appraisal Period : "></asp:Label>
                    </strong>
                    <asp:DropDownList ID="ddlSApp" runat="server" DataTextField="DESCRIPTION" DataValueField="AppPeriodID">
                    </asp:DropDownList>
                    <strong>
                        <asp:Label ID="lblRole" Style="margin-left: 20px" runat="server" Text="Role : "></asp:Label>
                    </strong>
                    <asp:DropDownList ID="ddlSRole" runat="server" DataTextField="RoleName" DataValueField="RoleID">
                    </asp:DropDownList>
                    <strong>
                        <asp:Label ID="lblType" Style="margin-left: 20px" runat="server" Text="Competency Type : "></asp:Label>
                    </strong>
                    <asp:DropDownList ID="ddlSType" runat="server" DataTextField="CompetencyType" DataValueField="CompetencyTypeID">
                    </asp:DropDownList>
                    <asp:Button ID="lnkSearch" runat="server" CssClass="btn btn-primary" OnClick="lnkSearch_Click"
                        Text="GO" CausesValidation="False"></asp:Button>
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary" Text="Add New"
                        CausesValidation="False" OnClick="btnNew_Click"></asp:Button>
                </div>
                <div class="clearfix">
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvApp" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvApp_RowCommand" DataKeyNames="CompetencyID" EnableViewState="True"
                        AllowPaging="true" PageSize="20" OnPageIndexChanging="gvApp_PageIndexChanging"
                        OnRowDataBound="gvApp_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="Appraisal Period">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="VIEWAPPPERIOD"
                                        CommandArgument='<%# Eval("AppPeriodID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompetencyType" HeaderText="Competency Type" />
                            <asp:BoundField DataField="CompetencyName" HeaderText="Description" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblAppPeriod" runat="server" Text='<%# Eval("AppPeriodID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("CompetencyTypeID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("CompetencyID") %>'
                                        CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("CompetencyID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Appraisal Competency?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="box-footer">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <div class="modal fade" id="dvAppPeriod" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><%= hrmlang.GetString("appraisalperiod") %></h4>
                </div><br />
                <div class="col-xs-12">
                    <asp:TextBox ID="txtAppPeriod" runat="server" TextMode="MultiLine" Height="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
</asp:Content>
