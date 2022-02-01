<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ComplaintsNew.aspx.cs" Inherits="ComplaintsNew" ValidateRequest="false" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlComplaintDate" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
        });
    </script>
    <asp:Label ID="lblComplaintId" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("addnewcomplaint") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managecomplaints")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("complaintfrom") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlComplaintFrom" runat="server" CssClass="form-control validate"
                                DataTextField="Fullname" Height="100px" DataValueField="EmployeeID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("complainttitle")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtComplaintTitle" runat="server" CssClass="form-control validate" placeholder="Enter Complaint Title"></asp:TextBox>
                        </div>
                    </div>                
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtComplaintDate">
                                <%= hrmlang.GetString("complaintdate")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                       <uc1:ctlComplaintDate ID="ctlClosingDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                        <asp:TextBox ID="txtComplaintDate" runat="server" CssClass="form-control dtjoin hrmhide"
                            placeholder="Complaint Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtQualification">
                                    <%= hrmlang.GetString("complaintdescription")%></label>
                                <asp:TextBox ID="txtDescription" CssClass="editor1 form-control" runat="server"
                                    TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return validatectrl();" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
