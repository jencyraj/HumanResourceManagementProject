<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UploadDocuments.aspx.cs" Inherits="UploadDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <section class="content-header">
    <h1><%= hrmlang.GetString("uploaddocuments")%><small> of <asp:Label ID="lblEmp" runat="server"></asp:Label></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("uploaddocuments")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">      
            
           <div  class="pull-left dblock rowmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
                         
            <div class="row rowmargin">
                 <div class="col-xs-3">
                    <label for="txtDesc"><%= hrmlang.GetString("description") %></label>
                </div>
                 <div class="clearfix"></div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtDesc" runat="server" ></asp:TextBox>
                </div>
                    <div class="clearfix"></div><br />
                <div class="col-xs-3">
                    <label for="fpDoc"><%= hrmlang.GetString("selectfile") %> </label>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:FileUpload ID="fpDoc" runat="server" />
                </div>
                    <div class="clearfix"></div><br />
                <div class="col-xs-3">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Add" 
                        onclick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            </div>   
            <div class="clearfix"></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvDocs" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                   DataKeyNames="DocID" EnableViewState="True" onrowcommand="gvDocs_RowCommand" OnRowDataBound="gvDocs_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Description" DataField="Description" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a target="_blank" href='<%# String.Concat("images/Employee/DOCS/",Eval("EmployeeID"),String.Concat("/",Eval("DocName"))) %>' >Download</a>
                                <asp:Label Visible="false" ID="lblFile" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("DocID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this document?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>           
            <div class="clearfix"></div>
            <div class="box-footer">
                
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

