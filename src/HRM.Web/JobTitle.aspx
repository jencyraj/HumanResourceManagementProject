<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobTitle.aspx.cs" Inherits="JobTitle" ValidateRequest="false" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlClosingDate" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
            $(".editor2").wysihtml5();
            $(".editor3").wysihtml5();
        });
    </script>
    <asp:Label ID="lblJID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("newjobrequest") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managejobtitles")%></li>
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
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("department") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control validate"
                                DataTextField="DepartmentName" DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("jobtitle")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control validate" placeholder="Enter Job Title"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("jobtype") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control" DataTextField="Description"
                                DataValueField="EmplStatusID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtPositions">
                                <%= hrmlang.GetString("numberofpositions")%></label>
                            <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="txtPositions" Operator="DataTypeCheck"
                                Type="Integer" ErrorMessage="InValid" CssClass="text-red"></asp:CompareValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtPositions" runat="server" CssClass="form-control validate" placeholder="Enter Number Of Positions">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="ddlRangeFrom">
                                <%= hrmlang.GetString("candidateagerangefrom") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlRangeFrom" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="ddlRangeTo">
                                <%= hrmlang.GetString("candidateagerangeto") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlRangeTo" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%--            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtCode">
                        <%= hrmlang.GetString("joblocation") %></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control" DataTextField="City" DataValueField="BranchID" ></asp:DropDownList>
                </div>
            </div>--%>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("branch") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:ListBox ID="lstBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                                DataValueField="BranchID" SelectionMode="Multiple" Height="175px"  >
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtSalFrom">
                                <%= hrmlang.GetString("salaryrangefrom")%></label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtSalFrom"
                                Operator="DataTypeCheck" Type="Integer" ErrorMessage="InValid" CssClass="text-red"></asp:CompareValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtSalFrom" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtSalTo">
                                <%= hrmlang.GetString("salaryrangeto")%></label>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtSalTo"
                                Operator="DataTypeCheck" Type="Integer" ErrorMessage="InValid" CssClass="text-red"></asp:CompareValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtSalTo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtClosingDate">
                                <%= hrmlang.GetString("closingdate")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                       <uc1:ctlClosingDate ID="ctlClosingDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                        <asp:TextBox ID="txtClosingDate" runat="server" CssClass="form-control dtjoin hrmhide"
                            placeholder="Closing Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtQualification">
                                    <%= hrmlang.GetString("candidatequalification")%></label>
                                <asp:TextBox ID="txtQualification" CssClass="editor1 form-control" runat="server"
                                    TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtExperience">
                                    <%= hrmlang.GetString("candidateexperience")%></label>
                                <asp:TextBox ID="txtExperience" CssClass="editor1 form-control" runat="server" TextMode="MultiLine"
                                    Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtDescription">
                                    <%= hrmlang.GetString("jobpostdescription")%></label>
                                <asp:TextBox ID="txtDescription" CssClass="editor2 form-control" runat="server" TextMode="MultiLine"
                                    Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtAddInfo">
                                    <%= hrmlang.GetString("additionalinfo")%></label>
                                <asp:TextBox ID="txtAddInfo" CssClass="editor3 form-control" runat="server" TextMode="MultiLine"
                                    Style="height: 200px"></asp:TextBox>
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
