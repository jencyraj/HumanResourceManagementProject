<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmployeeIrisReport.aspx.cs" Inherits="EmployeeIrisReport" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>
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
            <div class="col-xs-3">
               <label for="ddlBranch">
                    <%= hrmlang.GetString("fromdate")%></label>
                </div>
            <div class="col-xs-2">
                <label for="ddlBranch">
                    <%= hrmlang.GetString("todate")%></label>
            </div>
            <div class="clearfix">
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                    DataValueField="BranchID"  >
                </asp:DropDownList>
            </div>

            <div class="col-xs-3">
             <uc2:ctlJoin ID="ctlCalDepDob" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="txtstatus" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder="Tnterview Scheduled time"></asp:TextBox>
               
            </div>
            <div class="col-xs-3">
             <uc2:ctlJoin ID="CtlJoin1" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder="Tnterview Scheduled time"></asp:TextBox>
               
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
                    <%= hrmlang.GetString("employeename")%></label></div>
          
            <div class="clearfix">
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate" placeholder="Employee Code"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txteName" runat="server" CssClass="form-control validate" placeholder="First Name"></asp:TextBox>
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
        </div>
       
    <input type="hidden" name="hid1" id="hid1" value="0" />
    </div>
    </div>
    </section>
</asp:Content>
