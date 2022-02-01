<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrainingTypes.aspx.cs" Inherits="TrainingTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblTID" runat="server" Visible="false"></asp:Label>    
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("trainingtypes") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("managetrainingtypes") %></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Training Type" CausesValidation="false" onclick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvTrainingTypes" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvTrainingTypes_RowCommand" DataKeyNames="Tid" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    OnPageIndexChanging="gvTrainingTypes_PageIndexChanging" 
                    OnRowDataBound="gvTrainingTypes_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Training Type" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("Tid") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("Tid") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Role?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtRole">
                            <%= hrmlang.GetString("description")%></label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDescription"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
