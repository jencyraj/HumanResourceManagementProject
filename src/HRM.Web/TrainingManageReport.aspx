<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TrainingManageReport.aspx.cs" Inherits="TrainingManagementReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("managetraingrpt")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("managetraingrpt")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
            <div  class="rowmargin pull-left">             
                <strong><asp:Label ID="lblTrainingType" runat="server" Text="Training Type : "></asp:Label> </strong>
                <asp:DropDownList ID="ddlStype" runat="server" 
                    DataTextField="DESCRIPTION" DataValueField="Tid" ></asp:DropDownList>  
                
                <strong><asp:Label ID="lblDesc" style="margin-left:20px" runat="server" Text="Description : "></asp:Label> </strong>
                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control1" placeholder="Search Text"></asp:TextBox>
                
                
              <%--  <asp:Button ID="lnkSearch" runat="server" CssClass="btn btn-primary" 
                    onclick="lnkSearch_Click" Text="GO" CausesValidation="False" ></asp:Button>--%>
              
            </div> 
            
            <div class="row  rowmargin" style="">
                    
                 
                    <div class="col-xs-3">
                        <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            <asp:ListItem Text="PDF" Value="PDF" Selected="True" ></asp:ListItem>
                            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
                            <asp:ListItem Text="RPT" Value="CR" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="lnkSearch" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="lnkSearch_Click" />
                    </div>
                </div> 
            <div class="clearfix"></div>        
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
               <%-- <asp:GridView ID="gvTraining" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
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
                        <asp:BoundField DataField="fromdt"  HeaderText="From" />
                        <asp:BoundField DataField="todt"  HeaderText="To" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTraining" runat="server" Text='<%# Eval("TrainingID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("trainingtype") %>' Visible="false"></asp:Label>
                                
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("TrainingID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TrainingID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> --%>
            </div>
           
            <div class="box-footer">
               
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

