<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmployeeReports.aspx.cs" Inherits="Reports_EmployeeReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
    <h1><%= hrmlang.GetString("empreports") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="../Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("mempreports") %></li>
    </ol>
</section>
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
    <div onclick="ShowFilter()" style="display: none;">
        <a href="#" class="ancfilter fa fa-angle-down"><%= hrmlang.GetString("advancedfilter") %></a></div>
    <div class="advfilter" id="dvFilter" runat="server">
        <div class="row rowmargin">
            <div class="col-xs-2">
                <label for="ddlBranch">
                    <%= hrmlang.GetString("branch") %></label>
                <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch" Operator="NotEqual"
                    Type="String" ValueToCompare="" ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></div>
            <div class="col-xs-2">
                <label for="ddlDept">
                   <%= hrmlang.GetString("maindepartment") %></label></div>
            <div class="col-xs-2">
                <label for="ddlSubDept"><%= hrmlang.GetString("department") %></label></div>
            <div class="col-xs-2">
                <label for="ddlStatus"><%= hrmlang.GetString("status") %></label>
            </div>
            <div class="col-xs-2">
                <label for="ddlRole"><%= hrmlang.GetString("role") %></label>
            </div>
            <div class="col-xs-2">
                <label for="ddlDesgn"><%= hrmlang.GetString("designation") %></label>
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
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Text="Current" Value="C"></asp:ListItem>
                    <asp:ListItem Text="Previous" Value="P"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName"
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
    
        <div class="pull-left dblock rowmargin">
            <p class="text-red">
                <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            <p class="text-green">
                <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
        </div>
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
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate" placeholder="Employee Code"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate" placeholder="First Name"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtmName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate" placeholder="Last Name"></asp:TextBox>
            </div>
            </div>
             <div class="row rowmargin">
            <div class="col-xs-2">
             <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
        </asp:RadioButtonList>
</div>
            <div class="col-xs-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />
            </div>
            <div class="col-xs-2">
                <asp:Button ID="btnView" runat="server" Text="View Benefit Packages" CssClass="btn btn-primary"
                  OnClick="btnView_Report"  />
            </div>
               <div class="col-xs-2">
                <asp:Button ID="btnbldgrp" runat="server" Text="Blood Group Report" CssClass="btn btn-primary"
                  OnClick="btnbloodgrp_Report"  />
            </div>
        </div>
       
    <input type="hidden" name="hid1" id="hid1" value="0" />
    </div>
    </div>
    </section>
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
