<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LeaveBalancerpt.aspx.cs" Inherits="LeaveBalancerpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("leaveblcrpt")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("leaveblcrpt")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
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
                
            </div>
            <div class="row rowmargin">
            <div class="col-md-2">
                        <label for="txtYear">
                            <%= hrmlang.GetString("year")%></label>&nbsp;<asp:CompareValidator ID="cmp" runat="server"
                                ControlToValidate="txtYear" ErrorMessage="Invalid!" Operator="DataTypeCheck"
                                Type="Integer" ForeColor="Red"></asp:CompareValidator>
                        <asp:TextBox ID="txtYear" MaxLength="4" runat="server" CssClass="form-control" Style="width: 150px;
                            margin-right: 20px; display: inline;"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label for="ddMonth">
                            <%= hrmlang.GetString("month")%></label>
                        <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control" Style="width: 145px;"
                            DataTextField="MonthName" DataValueField="MonthID">
                        </asp:DropDownList>
                    </div>
            </div>
             <div class="col-xs-2">
             <asp:RadioButtonList ID="rbtnPrint" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
            <asp:ListItem Text="PDF" Value="PDF" Selected="True"></asp:ListItem>
            <asp:ListItem Text="MS-Excel" Value="MX"></asp:ListItem>
            <asp:ListItem Text="RPT" Value="CR"></asp:ListItem>
        </asp:RadioButtonList>
</div>  
<div class="col-xs-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" onclick="btnSearch_Click" />
                        </div>          
           <div  class="pull-left dblock rowmargin">     
                <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
           </div>
   </asp:Panel>

            <div class="row rowmargin">
                <div class="col-xs-12">
                  
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

