<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SalaryslipView.aspx.cs" Inherits="SalaryslipView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        <h1>
            <%= hrmlang.GetString("salarypayslip") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managesalarypayslip") %></li>
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
                <div class="clearfix">
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div onclick="ShowFilter()">
                    <a href="#" class="ancfilter fa fa-angle-down">
                        <%= hrmlang.GetString("advancedfilter") %></a></div>
                <div class="advfilter" style="display: none;" id="dvFilter" runat="server">
                    <div class="row rowmargin">
                        <div class="col-xs-2">
                            <label for="txtfName">
                                <%= hrmlang.GetString("branch") %></label>
                            <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch" Operator="NotEqual"
                                Type="String" ValueToCompare="" ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></div>
                        <div class="col-xs-2">
                            <label for="txtmName">
                                <%= hrmlang.GetString("maindepartment") %></label></div>
                        <div class="col-xs-2">
                            <label for="txtmName">
                                <%= hrmlang.GetString("department") %></label></div>
                        <div class="col-xs-2 hrmhide">
                            <label for="ddlStatus">
                                <%= hrmlang.GetString("status") %></label>
                        </div>
                        <div class="col-xs-2 hrmhide">
                            <label for="txtlName">
                                <%= hrmlang.GetString("role") %></label>
                        </div>
                        <div class="col-xs-2">
                            <label for="txtlName">
                                <%= hrmlang.GetString("designation")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                                DataValueField="BranchID" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                DataValueField="DepartmentID" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2 hrmhide">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control hrmhide">
                                <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                                <asp:ListItem Text="Current" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Previous" Value="P"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2 hrmhide">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control hrmhide" DataTextField="RoleName"
                                DataValueField="RoleID">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation"
                                DataValueField="DesignationID">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-2">
                        <label for="txtEmpCode">
                            <%= hrmlang.GetString("employeecode") %></label></div>
                    <div class="col-xs-3">
                        <label for="txtfName">
                            <%= hrmlang.GetString("fname") %></label></div>
                    <div class="col-xs-3">
                        <label for="txtmName">
                            <%= hrmlang.GetString("lname") %></label></div>
                    <div class="col-xs-3">
                        <label for="txtlName">
                            <%= hrmlang.GetString("lname") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate"></asp:TextBox>
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
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4" style="width:300px">
                        <label for="txtEmpCode">
                            <%= hrmlang.GetString("salaryforthemonthandyearof")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4" style="width:300px">
                        <asp:DropDownList ID="ddlMonth" runat="server" Visible="true" CssClass="form-control"
                            Width="150px" Style="display: inline">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" Visible="true" CssClass="form-control"
                            Width="100px" Style="display: inline">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                            OnClick="btnSearch_Click" ClientIDMode="Static"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                            OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            OnRowDataBound="gvEmployee_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Employee Name" DataField="FullC" />
                                <asp:BoundField HeaderText="Basic Salary" />
                                <asp:BoundField HeaderText="Allowance" />
                                <asp:BoundField HeaderText="OverTime" />
                                <asp:BoundField HeaderText="Bonus" />
                                <asp:BoundField HeaderText="Commission" />
                                <asp:BoundField HeaderText="Deduction" />
                                <asp:BoundField HeaderText="Loss Of Pay" />
                                <asp:BoundField HeaderText="Total" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer" style="text-align: right;" id="dvGen" runat="server" visible="false">
                    <div class="row hide">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:CheckBox ID="chkSendEmail" runat="server" /><label for="txtValue"><%= hrmlang.GetString("sendpayslipasemail")%></label>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnGenerate" runat="server" Visible="false" CssClass="btn btn-primary" Text="Export"
                        OnClick="btnGenerate_Click" />
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
    <script type="text/javascript">
        $(function () {

            "use strict";

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });

            //When unchecking the checkbox
            $(".hcadd").on('ifUnchecked', function (event) {
                //Uncheck all checkboxes
                $(".cadd", ".dataTable").iCheck("uncheck");
            });
            //When checking the checkbox
            $(".hcadd").on('ifChecked', function (event) {
                //Check all checkboxes
                $(".cadd", ".dataTable").iCheck("check");
            });
        });
    </script>
</asp:Content>
