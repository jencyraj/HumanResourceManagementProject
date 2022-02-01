<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AppraisalCompetencyTemplate.aspx.cs" Inherits="AppraisalCompetencyTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("appraisaltemplate") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mappraisaltemplate") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    <div class="fr-right">
                    <label><%= hrmlang.GetString("competencytype") %></label>
                        <asp:DropDownList ID="ddlType" runat="server" 
                    DataTextField="CompetencyType" DataValueField="CompetencyTypeID" 
                            AutoPostBack="true" onselectedindexchanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="clearfix"></div>
                    <asp:GridView ID="gvAppraisal" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                      DataKeyNames="AppPeriodID" EnableViewState="True"
                        AllowPaging="true" PageSize="20" OnRowDataBound="gvAppraisal_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("CompetencyNAME") %>'></asp:Label><br />
                            <center><asp:TextBox ID="txtWeightage" runat="server" Text='<%# Eval("Weightage") %>' Width="40px" style="text-align:center" ></asp:TextBox>%</center>
                            </ItemTemplate>                            
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                   
                                    <asp:Label ID="lblCompetencyID" runat="server" Text='<%# Eval("CompetencyID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCompetencyTypeID" runat="server" Text='<%# Eval("CompetencyTypeID") %>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtRating1" runat="server" Text='<%# Eval("RatingDesc1") %>'
                                     TextMode="MultiLine" CssClass="form-control" Height="130px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtRating1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                   
                                    <asp:TextBox ID="txtRating2" runat="server" CssClass="form-control" Text='<%# Eval("RatingDesc2") %>'  TextMode="MultiLine" Height="130px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtRating2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                   
                                    <asp:TextBox ID="txtRating3" runat="server" CssClass="form-control" Text='<%# Eval("RatingDesc3") %>'  TextMode="MultiLine" Height="130px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtRating3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                   
                                    <asp:TextBox ID="txtRating4" runat="server" CssClass="form-control" Text='<%# Eval("RatingDesc4") %>'  TextMode="MultiLine" Height="130px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtRating4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                   
                                    <asp:TextBox ID="txtRating5" runat="server" CssClass="form-control" Text='<%# Eval("RatingDesc5") %>'  TextMode="MultiLine" Height="130px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtRating5" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>

