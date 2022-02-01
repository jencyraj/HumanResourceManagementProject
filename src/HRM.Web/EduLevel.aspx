<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EduLevel.aspx.cs" Inherits="EduLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("educationlevel") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageeducationlevel")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Role" CausesValidation="false" onclick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvEduLevel" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvEduLevel_RowCommand" DataKeyNames="EduLevelID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvEduLevel_PageIndexChanging" OnRowDataBound="gvEduLevel_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="EduLevelName" HeaderText="Education Level" />
                        <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("EduLevelID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EduLevelID") %>'
                                    CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
           <div  class="pull-left dblock rowmargin rowleftmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row hrmhide">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEmployee">
                                        Employee</label>
                                    <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"
                                        ></asp:TextBox> 
                                </div>
                            </div>
                        </div>
            <div class="row rowmargin rowleftmargin">
                <div class="col-xs-4">
                    <label for="txtDesc">
                        <%= hrmlang.GetString("description")%></label>
                    <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                        ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin rowleftmargin">
                <div class="col-xs-4">
                    <label for="txtOrder">
                        <%= hrmlang.GetString("sortorder") %></label>
                    <asp:RequiredFieldValidator ID="rfvOrder" runat="server" ControlToValidate="txtOrder"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator><asp:CompareValidator ID="cmp1" runat="server" ErrorMessage="Invalid Number!" ControlToValidate="txtOrder" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"></asp:CompareValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="form-control" placeholder="Enter Sort Order"></asp:TextBox>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

