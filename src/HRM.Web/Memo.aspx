<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Memo.aspx.cs" Inherits="Memo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("memos")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("memos")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
            <div  class="rowmargin pull-right col-lg-12">      
             <div class="col-xs-3">       
                 <strong><asp:Label ID="lblsub" style="margin-left:20px" runat="server" Text="Subject : "></asp:Label> </strong>
                 <asp:TextBox ID="txtsub" runat="server" CssClass="form-control" placeholder="Search Subject"></asp:TextBox>
                 </div>
                  <div class="col-xs-3">       
                 <strong><asp:Label ID="lblStatus" style="margin-left:20px" runat="server" Text="Status : "></asp:Label> </strong>
                 <asp:DropDownList ID="ddlStatus" runat="server"  AutoPostBack="false"   CssClass="form-control"  ></asp:DropDownList>       
                  </div>
                  <div class="col-xs-3">    
                <strong><asp:Label ID="lblDesc" style="margin-left:20px" runat="server" Text="Description : "></asp:Label> </strong>
                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Text"></asp:TextBox>
                 </div>
                  <div class="col-xs-3" style="padding-top:22px;">    
                
                <asp:Button ID="lnkSearch" runat="server" CssClass="btn btn-primary btn-sm" 
                    onclick="lnkSearch_Click" Text="Search" CausesValidation="False" ></asp:Button>
                <asp:Button ID="btnNew" runat="server" Text="Add New" CssClass="btn btn-success btn-sm" CausesValidation="False" onclick="btnNew_Click" ></asp:Button>
                     </div>
            </div>  
            <div class="clearfix"></div>        
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvMemo" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvMemo_RowCommand" DataKeyNames="MemoID" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvMemo_PageIndexChanging" 
                    onrowdatabound="gvMemo_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>                        
                        <asp:BoundField DataField="EmpName" HeaderText="Memo From" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Memo Date" DataFormatString="{0:dd/mm/yyyy}" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="MemoStatus" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblMemo" runat="server" Text='<%# Eval("MemoID") %>' Visible="false"></asp:Label>
                             <%--   <asp:Label ID="lblType" runat="server" Text='<%# Eval("trainingtype") %>' Visible="false"></asp:Label>--%>
                                
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("MemoID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("MemoID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"></asp:LinkButton>
                                 <asp:LinkButton ID="lnkApproval" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-up" CommandArgument='<%# Eval("MemoID") %>' CommandName="Y" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkRejected" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-down" CommandArgument='<%# Eval("MemoID") %>' CommandName="R" OnClientClick="return confirm('Are you sure to Reject?')" CausesValidation="false"></asp:LinkButton>
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
