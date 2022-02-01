<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AppraisalPeriodNew.aspx.cs" Inherits="AppraisalPeriodNew" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="CalendarCtrl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("appraisalperiod") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mappraisalperiod") %></li>
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
                            <label for="ddlDept">
                                <%= hrmlang.GetString("department") %></label>
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" 
                                DataTextField="DepartmentName" DataValueField="DepartmentID"
                            AutoPostBack="true" onselectedindexchanged="ddlDept_SelectedIndexChanged" ></asp:DropDownList>
                        </div>                        
                        <div class="form-group">
                            <label for="ddlSubDept">
                                <%= hrmlang.GetString("subdepartment") %></label>
                            <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" 
                                DataTextField="DepartmentName" DataValueField="DepartmentID" 
                                onselectedindexchanged="ddlSubDept_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="lstBranch">
                                <%= hrmlang.GetString("branch") %></label>
                            <asp:ListBox ID="lstBranch" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID" SelectionMode="Multiple"></asp:ListBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlDesgn">
                                <%= hrmlang.GetString("designation") %></label>
                            <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation" DataValueField="DesignationID"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtEmployee">
                                <%= hrmlang.GetString("employee") %></label><br />                       
                            <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" style="width:150px;display:inline;" placeholder="Employee" Enabled="false" ></asp:TextBox>
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="glyphicon glyphicon-search" onclick="btnSearch_Click" CausesValidation="false" ></asp:LinkButton>
                        </div>
                        <div class="form-group">
                            <label for="txtDesc">
                                <%= hrmlang.GetString("description") %></label>
                            <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtStart">
                                <%= hrmlang.GetString("startdate") %>
                            </label>
                            <uc1:CalendarCtrl ID="txtStart" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="txtEnd">
                                <%= hrmlang.GetString("enddate") %></label>
                            <uc1:CalendarCtrl ID="txtEnd" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="txtEnd">
                                <%= hrmlang.GetString("status") %></label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            </asp:DropDownList>
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
