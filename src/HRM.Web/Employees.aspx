<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


<script type="text/javascript">
$('body').ajaxStart(function() {
  $('#spinner').show();
});

$('body').ajaxComplete(function() {
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
    <h1><%= hrmlang.GetString("manageemployees")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageemployees")%></li>
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
           <div  class="pull-right rowmargin">
            
           </div>
           <div class="clearfix"></div> 
            <div onclick="ShowFilter()"><a href="#" class="ancfilter fa fa-angle-down"><%= hrmlang.GetString("advancedfilter")%></a></div>
            <div class="advfilter" style="display:none;" id="dvFilter" runat="server">           
               <div class="row rowmargin">
                    <div class="col-xs-2">
                        <label for="txtfName">
                            <%= hrmlang.GetString("branch") %></label>
                        <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch"
                         Operator="NotEqual" Type="String" ValueToCompare=""
                            ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></div>
                    <div class="col-xs-2">
                        <label for="txtmName">
                            <%= hrmlang.GetString("maindepartment") %></label></div>
                    <div class="col-xs-2">
                        <label for="txtmName">
                            <%= hrmlang.GetString("department") %></label></div>  
                <div class="col-xs-2">
                    <label for="ddlStatus">
                        <%= hrmlang.GetString("status") %></label>
                </div>        
                <div class="col-xs-2">
                    <label for="txtlName">
                        <%= hrmlang.GetString("role") %></label>
                </div>
                <div class="col-xs-2">
                    <label for="txtlName">
                        <%= hrmlang.GetString("designation") %></label>
                </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" 
                            DataTextField="Branch" DataValueField="BranchID" AutoPostBack="True" 
                            onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" 
                            DataTextField="DepartmentName" DataValueField="DepartmentID" 
                            AutoPostBack="True" onselectedindexchanged="ddlDept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" DataTextField="DepartmentName" DataValueField="DepartmentID">
                        </asp:DropDownList>
                    </div>
                
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                        <asp:ListItem Text="Current" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Previous" Value="P"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName" DataValueField="RoleID">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation" DataValueField="DesignationID">
                    </asp:DropDownList>
                </div>
            </div>   
            </div>       
           <div  class="pull-left dblock rowmargin">     
                <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
           </div>
           <div class="clearfix"></div>
            <div class="row rowmargin">
                <div class="col-xs-2">
                    <label for="txtEmpCode">
                        <%= hrmlang.GetString("employeecode") %></label></div>
                <div class="col-xs-2">
                    <label for="txtfName">
                        <%= hrmlang.GetString("fname") %></label></div>
                <div class="col-xs-2">
                    <label for="txtmName">
                        <%= hrmlang.GetString("mname") %></label></div>
                <div class="col-xs-2">
                    <label for="txtlName">
                        <%= hrmlang.GetString("lname") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtmName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate"></asp:TextBox>
                </div>
                <div class="col-xs-3">
               
                      <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click"  ClientIDMode="Static" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" OnClick="btnNew_Click"/>
                </div>
            </div> 
            <div class="row rowmargin">
                <div class="col-xs-12">
                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" 
                        CssClass="table table-bordered table-striped dataTable" AllowPaging="true" PageSize="25" 
                        OnRowCommand="gvEmployee_RowCommand" OnRowDataBound="gvEmployee_RowDataBound" onpageindexchanging="gvEmployee_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                
                                    <asp:Label ID="lblsalute" runat="server" Text='<%# Eval("SaluteName")%>'></asp:Label>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                    <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" />
                            <asp:BoundField DataField="Branch" />
                            <asp:BoundField DataField="DepartmentName" />
                            <asp:BoundField DataField="Designation" />
                            <asp:TemplateField>
                                <ItemTemplate> 
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="glyphicon glyphicon-user"
                                        NavigateUrl='<%# String.Concat("~/ReportingOfficers.aspx?empid=", Eval("EmployeeID")) %>' data-toggle="tooltip">
                                   </asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="glyphicon glyphicon-upload"
                                        NavigateUrl='<%# String.Concat("~/UploadDocuments.aspx?empid=", Eval("EmployeeID")) %>' data-toggle="tooltip">
                                    </asp:HyperLink> 
                                      <asp:HyperLink ID="lnkReset" runat="server" CssClass="fa fa-calendar-o" data-toggle="tooltip" Visible="false" 
                                          NavigateUrl='<%# String.Concat("~/ResetLeaveBalance.aspx?empid=", Eval("EmployeeID")) %>' ></asp:HyperLink>
                                     <asp:HyperLink ID="lnkView" runat="server" CssClass="fa fa-eye" data-toggle="tooltip"
                                          NavigateUrl='<%# String.Concat("~/ViewEmployeeDetails.aspx?empid=", Eval("EmployeeID")) %>' ></asp:HyperLink>
                                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("EmployeeID") %>'
                                        CommandName="EDITDT" NavigateUrl='<%# String.Concat("~/ManageEmployee.aspx?empid=", Eval("EmployeeID")) %>'
                                        data-toggle="tooltip"></asp:HyperLink>
                                        <asp:HyperLink ID="lnksync" runat="server" CssClass="fa fa-retweet" CommandArgument='<%# Eval("EmployeeID") %>'
                                        CommandName="sync" NavigateUrl='<%# String.Concat("~/syncEmployee.aspx?empid=", Eval("EmployeeID")) %>'
                                        data-toggle="tooltip"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EmployeeID") %>'
                                        CommandName="DEL" CausesValidation="false"
                                         data-toggle="tooltip"></asp:LinkButton>                                  
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
    <input type="hidden" name="hid1" id="hid1" value="0" />
    <script type="text/javascript">
        function ShowFilter() {
            //alert($('#hid1').val());
            if ($('#hid1').val() == "0") {
                $('.ancfilter').removeClass('fa-angle-down');
                $('.ancfilter').addClass('fa-angle-up');
                document.getElementById('hid1').value = "1";
                $('.advfilter').css("display", "");
            }
            else {
                $('.ancfilter').removeClass('fa-angle-up');
                $('.ancfilter').addClass('fa-angle-down');
                document.getElementById('hid1').value = "0";
                $('.advfilter').css("display", "none");
            }
        }
    </script>    
</asp:Content>
