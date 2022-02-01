<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AppraisalPeriod.aspx.cs" Inherits="AppraisalPeriod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("appraisalperiod") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mappraisalperiod") %></li>
        </ol>
    </section>
    <%--
<script src="js/plugins/datepicker/bootstrap-datepicker.js" ></script>
<script type="text/javascript">
    $(function () {
        $('.startdate').datepicker({});
        $('.enddate').datepicker({});
    });
</script>--%>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin" visible="false">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                            Text="Add New" CausesValidation="false" OnClick="btnNew_Click" /></div>
                            <div class="clearfix"></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvApp" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvApp_RowCommand" DataKeyNames="AppPeriodID" EnableViewState="True"
                        AllowPaging="true" PageSize="20" OnPageIndexChanging="gvApp_PageIndexChanging"
                        OnRowDataBound="gvApp_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:d}" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusDesc" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("PeriodStatus") %>' Visible="false"></asp:Label>
                                    <asp:HyperLink ID="lnkTemplate" runat="server" CssClass="fa fa-th" data-toggle="tooltip"
                                          NavigateUrl='<%# String.Concat("~/AppraisalCompetencyTemplate.aspx?appperiodid=", Eval("AppPeriodID")) %>' ></asp:HyperLink>
                                    <asp:HyperLink ID="lnkView" runat="server" CssClass="fa fa-eye" data-toggle="tooltip"
                                          NavigateUrl='<%# String.Concat("~/AppraisalPeriodNew.aspx?id=", Eval("AppPeriodID"),"&view=1") %>' ></asp:HyperLink>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("AppPeriodID") %>'
                                        CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("AppPeriodID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Appraisal Period?')"
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtDesc">
                                    <%= hrmlang.GetString("description") %></label>
                                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtStart">
                                    <%= hrmlang.GetString("startdate") %>
                                </label>
                                
                            </div>
                            <div class="form-group">
                                <label for="txtEnd">
                                    <%= hrmlang.GetString("enddate") %></label>
                                
                            </div>
                            <div class="form-group">
                                <label for="txtEnd">
                                    <%= hrmlang.GetString("status") %></label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false"
                            Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
