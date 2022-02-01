<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LeaveBalance.aspx.cs" Inherits="LeaveBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
    $('body').ajaxStart(function () {
        $('#spinner').show();
    });

    $('body').ajaxComplete(function () {
        $('#spinner').hide();
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnSearch').click(function () {
            //$("#spinner").append('<img id="img-spinner" src="Images/ajax-loader-test.gif" alt="Loading.." style="position: absolute; z-index: 200; left:50%; top:50%; " />');
            $('#spinner').show().fadeIn(20);
        });
    });
</script>
 <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("leavebalance")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("leavebalance")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
<div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
           <div class="clearfix"></div>
   <asp:Panel ID="pnlAll" runat="server">
                     
           <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtfName">
                        <%= hrmlang.GetString("branch") %></label>
                    <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch"
                     Operator="NotEqual" Type="String" ValueToCompare=""
                        ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></div>
                <div class="col-xs-3">
                    <label for="txtmName">
                        <%= hrmlang.GetString("department") %></label></div>
                <div class="col-xs-3">
                    <label for="txtEmpCode">
                        <%= hrmlang.GetString("employeecode") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" DataTextField="DepartmentName" DataValueField="DepartmentID">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                     <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate" placeholder="Employee Code"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtfName">
                        <%= hrmlang.GetString("fname") %></label></div>
                <div class="col-xs-3">
                    <label for="txtmName">
                        <%= hrmlang.GetString("mname") %></label></div>
                <div class="col-xs-3">
                    <label for="txtlName">
                        <%= hrmlang.GetString("lname") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate" placeholder="First Name"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtmName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate" placeholder="Last Name"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" onclick="btnSearch_Click" ClientIDMode="Static"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" onclick="btnCancel_Click" />
                </div>
            </div>            
           <div  class="pull-left dblock rowmargin">     
                <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
           </div>
   </asp:Panel>

            <div class="row rowmargin">
                <div class="col-xs-12">
                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" 
                        CssClass="table table-bordered table-striped dataTable" 
                        onrowcommand="gvEmployee_RowCommand" OnRowDataBound="gvEmployee_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                    <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Employee Code" DataField="EmpCode" />
                            <asp:BoundField HeaderText="Branch" DataField="Branch" />
                            <asp:BoundField HeaderText="Department" DataField="DepartmentName" />
                            <asp:BoundField HeaderText="Designation" DataField="Designation" />
                            <asp:TemplateField>
                                <ItemTemplate>  
                                     <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("EmployeeID") %>' CommandName="VIEWBAL"
                                      Text="Balance"></asp:LinkButton>                                      
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <!-- /.box-body -->
    </div> 
    </section>   
     <div class="modal fade" id="dvLeave" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H5"><%= hrmlang.GetString("leavebalanceof")%><asp:Label ID="lblEmp" runat="server" Font-Size="Small"></asp:Label></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-12">
                         <div  class="pull-left dblock rowmargin">     
                            <p class="text-green"><asp:Label ID="lblSubMsg" runat="server"></asp:Label></p>
                        </div>
                        <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-bordered table-striped dataTable" OnRowDataBound="gvBalance_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Leave Type" DataField="LeaveName" />
                                <asp:BoundField HeaderText="No. of Days" DataField="LeaveDays"  />
                                <asp:BoundField HeaderText="Carry Over" DataField="CarryOver" />
                                <asp:TemplateField HeaderText="Leaves Taken">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaken" runat="server" Text='<%# Eval("LeavesTaken") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Carry Overed" DataField="PrevYearBalance" />
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Util.ToDecimal(Eval("PrevYearBalance")) +  Util.ToDecimal(Eval("LeavesBalance")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>     
                    </div>
                </div> 
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

