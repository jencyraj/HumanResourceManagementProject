<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AppraisalForm.aspx.cs" Inherits="AppraisalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblTemplateID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEAID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("fillappraisalform") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mappraisal") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvAppraisal" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvAppraisal_RowCommand" DataKeyNames="AppPeriodID" EnableViewState="True"
                        AllowPaging="true" PageSize="5" OnPageIndexChanging="gvAppraisal_PageIndexChanging"
                        OnRowDataBound="gvAppraisal_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:d}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="glyphicon glyphicon-pencil"
                                        CommandArgument='<%# Eval("AppPeriodID") %>' CommandName="FILL" CausesValidation="false"
                                        data-toggle="tooltip" title="Fill Appraisal Form"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <div class="modal fade" id="dvForm" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog" style="width: 1200px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H5">
                        <%= hrmlang.GetString("markratings") %></h4>
                </div>
                <div class="modal-header rowmargin" style="height: 400px; overflow-y: scroll;">
                    <div class="col-xs-12">
                        <div class="pull-left dblock rowmargin">
                            <p class="text-green">
                                <asp:Label ID="lblSubMsg" runat="server"></asp:Label></p>
                        </div>
                        <asp:Repeater ID="rptrForm" runat="server" OnItemDataBound="rptrForm_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" width="100%">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblComHeader" runat="server" Text='<%# Eval("CompetencyType") %>'
                                            Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblCTID" runat="server" Visible="false" Text='<%# Eval("CompetencyTypeID") %>'></asp:Label>
                                        <asp:GridView ID="gvForm" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                                            OnRowDataBound="gvForm_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("CompetencyName")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <strong>1.</strong>
                                                        <%# Eval("RatingDesc1")%><br />
                                                        <strong>2.</strong>
                                                        <%# Eval("RatingDesc2")%><br />
                                                        <strong>3.</strong>
                                                        <%# Eval("RatingDesc3")%><br />
                                                        <strong>4.</strong>
                                                        <%# Eval("RatingDesc4")%><br />
                                                        <strong>5.</strong>
                                                        <%# Eval("RatingDesc5")%><br />
                                                        <br />
                                                        <strong>Weightage :  </strong><%# Eval("Weightage")%>%
                                                        <asp:TextBox ID="txtWeightage" runat="server" placeholder="Rate Yourself (1.0 to 5.0)"></asp:TextBox>
                                                         <asp:TextBox ID="txtComments" runat="server" placeholder="Comments" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                                        <asp:Label ID="lblCID" runat="server" Text='<%# Eval("CompetencyID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                 
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit"
                        Visible="false" OnClick="btnSubmit_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                    <p class="help-block" style="text-align: left;" id="pNote" runat="server">
                        <%= hrmlang.GetString("saveratings") %><br />
                        <%= hrmlang.GetString("submitratings") %></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
