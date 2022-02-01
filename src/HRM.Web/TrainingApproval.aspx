<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrainingApproval.aspx.cs" Inherits="TrainingApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("trainingapproval")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("trainingapproval")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
            <div  class="pull-right rowmargin">             
                <strong><asp:Label ID="lblTrainingApprovalStatus" runat="server" Text="Approval Status : "></asp:Label> </strong>
                <asp:DropDownList ID="ddlApprovalStatus" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlApprovalStatus_SelectedIndexChanged"></asp:DropDownList>             
          
            </div>  
                   
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvTraining" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvTraining_RowCommand" DataKeyNames="TrainingID" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvTraining_PageIndexChanging" 
                    onrowdatabound="gvTraining_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="TrainingType" HeaderText="Training Types" />
                        <asp:BoundField DataField="title" HeaderText="Title" />
                        <asp:BoundField DataField="trainer" HeaderText="Trainer" />
                        <asp:BoundField DataField="location" HeaderText="Location" />
                        <asp:BoundField DataField="TrainingDt"  HeaderText="Training Date" />
                         <asp:BoundField DataField="ApprovalStatus"  HeaderText="Status" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTraining" runat="server" Text='<%# Eval("TrainingID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("trainingtype") %>' Visible="false"></asp:Label>
                                 <asp:LinkButton ID="lnkApproval" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-up" CommandArgument='<%# Eval("TrainingID") %>' CommandName="Y" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkRejected" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-down" CommandArgument='<%# Eval("TrainingID") %>' CommandName="N" OnClientClick="return confirm('Are you sure to Reject?')" CausesValidation="false"></asp:LinkButton>
                                  <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("TrainingID") %>' CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TrainingID") %>' CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"></asp:LinkButton>
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
</asp:Content>
