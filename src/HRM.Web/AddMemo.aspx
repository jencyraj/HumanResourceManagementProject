<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddMemo.aspx.cs" Inherits="AddMemo" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="CalendarCtrl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
      <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblMemoID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("addnewmemo") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("addnewmemo") %></li>
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
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtsubject">
                                <%= hrmlang.GetString("subject") %></label>
                             <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtsubject"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                          <asp:TextBox ID="txtsubject"  CssClass="form-control validate" runat="server"></asp:TextBox>
                        </div>  
                        
                       
                       
                        <div class="form-group">
                            <label for="txtEmployee">
                                <%= hrmlang.GetString("memoto") %></label><br />                       
                            <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" style="width:150px;display:inline;" placeholder="Employee" Enabled="false" ></asp:TextBox>
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="glyphicon glyphicon-search" onclick="btnSearch_Click" CausesValidation="false" ></asp:LinkButton>
                        </div>
                        <div class="form-group">
                            <label for="txtDesc">
                                <%= hrmlang.GetString("description") %></label>
                           
                            <asp:TextBox ID="txtDesc" CssClass="editor1 form-control" runat="server" TextMode="MultiLine" style="height:200px; width:600px;"></asp:TextBox>
                        </div>                  
                        
                   </div>
                    <div class="col-md-3"></div>  
                    <div class="col-md-5">
                    <asp:label ID="lblTitle" runat="server" Visible="false" Font-Bold="true"></asp:label><br />
                        <asp:GridView ID="gvEmpSelected" runat="server" AutoGenerateColumns="false" 
                        CssClass="table table-bordered table-striped dataTable" 
                        OnRowDataBound="gvEmpSelected_RowDataBound" 
                            onrowcommand="gvEmpSelected_RowCommand"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                    <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                    <asp:Label ID="lblEmpID" runat="server" Text='<%# Eval("EmployeeID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField>
                                <ItemTemplate>
                                      <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EmployeeID") %>'
                                        CommandName="DEL" CausesValidation="false"
                                         data-toggle="tooltip"></asp:LinkButton>           
                                </ItemTemplate>
                            </asp:TemplateField>                          
                        </Columns>
                    </asp:GridView>
                    </div>                 
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false"
                        Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>

    <%--EMPLOYEE SEARCH Popup STARTS--%>
    <div class="modal fade" id="dvEmp" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true" style="z-index: 100000;">
        <div class="modal-dialog" style="width:960px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H8">
                        <%= hrmlang.GetString("search")%></h4>
                </div>
                <div class="col-xs-12">
                <br />
                    <asp:TextBox ID="txtfName" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtmName" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtlName" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="btnSearchAgain" runat="server" CssClass="glyphicon glyphicon-search" CausesValidation="false"
                                onclick="btnSearchAgain_Click" ></asp:LinkButton>
                <br />
                <span style="height:10px">&nbsp;</span>
                   <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" 
                        CssClass="table table-bordered table-striped dataTable" AllowPaging="true" PageSize="25" 
                        OnRowDataBound="gvEmployee_RowDataBound" onpageindexchanging="gvEmployee_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                    <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                    <asp:Label ID="lblEmpID" Visible="false" runat="server" Text='<%# Eval("EmployeeID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" />
                            <asp:BoundField DataField="Branch" />
                            <asp:BoundField DataField="DepartmentName" />
                            <asp:BoundField DataField="Designation" />
                            <asp:TemplateField>
                                <ItemTemplate> 
                                   <asp:CheckBox ID="chkSelect" runat="server" />                         
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>               
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveEmp" runat="server" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnSaveEmp_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
     <%--EMPLOYEE SEARCH Popup ENDS--%>
</asp:Content>
