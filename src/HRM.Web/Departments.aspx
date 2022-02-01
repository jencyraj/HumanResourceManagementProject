<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Departments.aspx.cs" Inherits="Departments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblDeptID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("company") %><small><%= hrmlang.GetString("departments") %></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managedepartments")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-right rowmargin" visible="false">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                            Text="New Department" CausesValidation="false" OnClick="btnNew_Click" /></div>
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div style="float: left; width: 450px;">
                        <div class="row rowmargin">
                            <div class="col-xs-6">
                                <label for="txtCode">
                                    <%= hrmlang.GetString("maindepartment") %></label></div>
                            <div class="clearfix">
                            </div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control txtround" DataTextField="DepartmentName"
                                    DataValueField="DepartmentID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row rowmargin">
                            <div class="col-xs-6">
                                <label for="txtCode">
                                    <%= hrmlang.GetString("departmentcode")%></label></div>
                            <div class="clearfix">
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control txtround validate" placeholder="Enter Department Code"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row rowmargin">
                            <div class="col-xs-6">
                                <label for="txtDepartment">
                                    <%= hrmlang.GetString("department")%></label></div>
                            <div class="clearfix">
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control txtround validate" placeholder="Enter Department"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 450px;">
                        <div class="row rowmargin">
                            <div class="col-xs-4">
                                <label for="txtCode">
                                    <%= hrmlang.GetString("branch") %></label></div>
                            <div class="clearfix">
                            </div>
                            <div class="col-xs-4">
                                <asp:ListBox ID="lstBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                                    DataValueField="BranchID" SelectionMode="Multiple" Height="190px" Width="250px">
                                </asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return validatectrl();" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:GridView ID="gvDepts" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvDepts_RowCommand" DataKeyNames="DepartmentID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDepts_PageIndexChanging"
                        OnRowDataBound="gvDepts_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewBranch" runat="server" Text="View" CommandName="VIEWBRANCH" CommandArgument='<%# Eval("DepartmentID") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DeptCode" HeaderText="Department Code" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                            <asp:BoundField DataField="ParentDeptName" HeaderText="Parent Dept" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchID" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblParentDeptID" runat="server" Text='<%# Eval("ParentDepartmentID") %>'
                                        Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("DepartmentID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("DepartmentID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Department?')"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>

     <div class="modal fade" id="dvBranches" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><%= hrmlang.GetString("branches") %></h4>
                </div><br />
                <div class="col-xs-12">
                    <asp:TextBox ID="txtBranches" runat="server" TextMode="MultiLine" Height="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
</asp:Content>
