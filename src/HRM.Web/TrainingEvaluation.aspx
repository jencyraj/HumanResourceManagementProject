<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrainingEvaluation.aspx.cs" Inherits="TrainingEvaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("trainingevaluate")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("trainingevaluate")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
          
                   
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvTraining" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvTraining_RowCommand" DataKeyNames="Evaluateid" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvTraining_PageIndexChanging" 
                    onrowdatabound="gvTraining_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                      
                        <asp:BoundField DataField="title" HeaderText="Title" />
                        <asp:BoundField DataField="Name" HeaderText="Employee Name"/>
                            <asp:BoundField DataField="PostedOn"  HeaderText="Posted Date" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                          <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblEvaluateId" runat="server" Text='<%# Eval("Evaluateid") %>' Visible="false"></asp:Label>
                               
                                   <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("Evaluateid") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Record?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
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
