<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AppraisalReview.aspx.cs" Inherits="AppraisalReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("appraisalreview")%><small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home")%></a></li>
            <li class="active">
                <%= hrmlang.GetString("appraisalreview")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvAppraisals" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        PageSize="20" CssClass="table table-bordered table-striped dataTable" OnRowDataBound="gvAppraisals_RowDataBound"
                        OnRowCommand="gvAppraisals_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <%# Eval("FirstName") %>
                                    <%# Eval("MiddleName") %>
                                    <%# Eval("LastName") %>
                                    <asp:Label ID="lblEmpID" runat="server" Text='<%# Eval("EmployeeID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRID" runat="server" Text='<%# Eval("ReviewID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblEAppID" runat="server" Text='<%# Eval("AppPeriodID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="Appraisal Period" />
                            <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted Date" />
                            <asp:BoundField DataField="ReviewedDate" HeaderText="Review Date" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReview" runat="server" CssClass="fa fa-comments" CommandArgument='<%# Eval("EAID") %>'
                                        CommandName="REVIEW" CausesValidation="false" data-toggle="tooltip" title="Review"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkSummary" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("EAID") %>'
                                        CommandName="REVIEWSUMMARY" CausesValidation="false" data-toggle="tooltip"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="pull-left dblock rowmargin">
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblReviewID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblEAID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
        <div class="modal fade" id="dvForm" tabindex="-1" role="dialog" aria-labelledby="basicModal"
            aria-hidden="true">
            <div class="modal-dialog" style="width: 1200px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="H5">
                            <%= hrmlang.GetString("markratings") %></h4>
                    </div>
                    <div class="modal-header rowmargin" style="height: 400px; overflow-y: scroll;">
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
                                            OnRowCommand="gvForm_RowCommand" OnRowDataBound="gvForm_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("CompetencyName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
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
                                                        <strong>Employee Ratings/Comments : </strong>
                                                        <asp:Label ID="lblRating" runat="server"></asp:Label>
                                                        /
                                                        <asp:Label ID="lblComments" runat="server"></asp:Label><br />
                                                        <strong>Weightage : </strong>
                                                        <%# Eval("Weightage")%>%
                                                        <asp:TextBox ID="txtWeightage" runat="server" placeholder="Rate Yourself (1.0 to 5.0)"></asp:TextBox>
                                                        <asp:TextBox ID="txtComments" runat="server" placeholder="Comments" TextMode="MultiLine"
                                                            Width="70%"></asp:TextBox>
                                                        <asp:LinkButton ID="lnkReviews" runat="server" CssClass="fa fa-comments" CommandArgument='<%# Eval("CompetencyID") %>'
                                                            CommandName="OTHERREVIEWS"></asp:LinkButton>
                                                        <asp:Label ID="lblCID" runat="server" Text='<%# Eval("CompetencyID") %>' Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtAllReviews" runat="server" TextMode="MultiLine" Visible="false"
                                                            CssClass="form-control" Style="margin-top: 5px" Width="85%"></asp:TextBox>
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
                        <br />
                        <div class="form-group hrmhide">
                            <label for="txtComments">
                                <%= hrmlang.GetString("comments") %></label>
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                            <%= hrmlang.GetString("submitreviewratings") %></p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>
