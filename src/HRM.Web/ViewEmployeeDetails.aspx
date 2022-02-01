<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewEmployeeDetails.aspx.cs" Inherits="ViewEmployeeDetails" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<style type="text/css">
.box-info .box-header h4 { margin-left:10px;}
</style>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblImg" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("employeeprofile") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("employeeprofile")%></li>
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
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="lblBranch" class="w100">
                                <%= hrmlang.GetString("branch") %></label>
                            :
                            <asp:Label ID="lblBranch" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblDept" class="w100">
                                <%= hrmlang.GetString("maindepartment")%></label>
                            :
                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblSubDepartment" class="w100">
                                <%= hrmlang.GetString("subdepartment")%></label>
                            :
                            <asp:Label ID="lblSubDepartment" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblDesgn" class="w100">
                                <%= hrmlang.GetString("designation")%></label>
                            :
                            <asp:Label ID="lblDesgn" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblRole" class="w100">
                                <%= hrmlang.GetString("role") %></label>
                            :
                            <asp:Label ID="lblRole" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtUserID" class="w100">
                                <%= hrmlang.GetString("userid") %></label>
                            :
                            <asp:Label ID="txtUserID" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtBiometricId" class="w100">
                                <%= hrmlang.GetString("biometricid")%></label>
                            :
                            <asp:Label ID="txtBiometricId" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtIrisId" class="w100">
                                <%= hrmlang.GetString("irisid") %></label>
                            :
                            <asp:Label ID="txtIrisId" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="ddlStatus" class="w100">
                                <%= hrmlang.GetString("status") %></label>
                            :
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtEmpCode" class="w100">
                                <%= hrmlang.GetString("employeecode")%></label>
                            :
                            <asp:Label ID="txtEmpCode" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtfName" class="w100">
                                <%= hrmlang.GetString("name") %></label>
                            :
                             <asp:Label ID="txtsalute" runat="server" placeholder="Salute Name"></asp:Label>
                            <asp:Label ID="txtfName" runat="server" placeholder="First Name"></asp:Label>
                            <asp:Label ID="txtmName" runat="server" placeholder="Middle Name"></asp:Label>
                            <asp:Label ID="txtlName" runat="server" placeholder="Last Name"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblGender" class="w100">
                                <%= hrmlang.GetString("gender") %></label>
                            :
                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtDOB" class="w100">
                                <%= hrmlang.GetString("dob") %></label>
                            :
                            <asp:Label ID="txtDOB" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtIDDesc" class="w100">
                                <%= hrmlang.GetString("maritalstatus")%></label>
                            :
                            <asp:Label ID="lblMarried" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="lblNationality" class="w100">
                                <%= hrmlang.GetString("nationality")%></label>
                            :
                            <asp:Label ID="lblNationality" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtIDDesc" class="w100">
                                <%= hrmlang.GetString("iddescription")%></label>
                            :
                            <asp:Label ID="txtIDDesc" runat="server"></asp:Label>
                            <div class="clearfix">
                            </div>
                            <label for="txtIDNo" class="w100">
                                <%= hrmlang.GetString("idno")%></label>
                            :
                            <asp:Label ID="txtIDNo" runat="server" placeholder="ID No"></asp:Label>
                              <div class="clearfix">
                            </div>
                            <label for="txtbldgrp" class="w100">
                                <%= hrmlang.GetString("bloodgrp")%></label>
                            :
                            <asp:Label ID="txtbldgrp" runat="server" placeholder="ID No"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Image ID="imgPhoto" runat="server" Height="135px" />
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box-primary -->
        <div class="row">
            <section class="col-lg-5">
                <div class="box box-info">
                    <!-- /.box-header -->
                    <div class="box-header bankdet">
                        <h4>
                            <%= hrmlang.GetString("jobdetails")%></h4>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-body bankdetdt">
                        <div class="row rowmargin">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="ddlJobType" class="w100" style="width: 130px">
                                        <%= hrmlang.GetString("employmentstatus")%>
                                    </label>
                                    :
                                    <asp:Label ID="lblEmplStatus" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtJoinDate" class="w100" style="width: 130px">
                                        <%= hrmlang.GetString("joiningdate")%></label>
                                    :
                                    <asp:Label ID="txtJoinDate" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtProbStart" class="w100" style="width: 130px">
                                        <%= hrmlang.GetString("probationstartdate")%></label>
                                    :
                                    <asp:Label ID="txtProbStart" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtProbEnd" class="w100" style="width: 130px">
                                        <%= hrmlang.GetString("probationenddate")%></label>
                                    :
                                    <asp:Label ID="txtProbEnd" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-header bankdet" style="margin-top: -35px">
                        <h4>
                            <%= hrmlang.GetString("bankdetails")%></h4>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-body bankdetdt">
                        <div class="row rowmargin">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="txtBank" class="w100">
                                        <%= hrmlang.GetString("bankname") %></label>
                                    :
                                    <asp:Label ID="txtBank" runat="server" placeholder="Bank Name"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtBranch" class="w100">
                                        <%= hrmlang.GetString("bankbranch") %></label>
                                    :
                                    <asp:Label ID="txtBranch" runat="server" placeholder="Bank Branch"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtAccNo" class="w100">
                                        <%= hrmlang.GetString("accountno") %></label>
                                    :
                                    <asp:Label ID="txtAccNo" runat="server" placeholder="Account No"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtOther" class="w100">
                                        <%= hrmlang.GetString("otherdetails")%></label>
                                    :
                                    <asp:Label ID="txtOther" runat="server" placeholder="Other Details"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </section>
            <section class="col-lg-7">
                <div class="box box-info">
                    <!-- /.box-header -->
                    <div class="box-header bankdet">
                        <h4>
                            <%= hrmlang.GetString("contactdetails")%></h4>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-body bankdetdt">
                        <div class="row rowmargin">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="txtAdd1" class="w100">
                                        <%= hrmlang.GetString("address")%></label>
                                    :
                                    <asp:Label ID="txtAdd1" runat="server"></asp:Label>
                                    <asp:Label ID="txtAdd2" runat="server"></asp:Label>
                                    <asp:Label ID="txtCity" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtAdd1" class="w100">
                                        <%= hrmlang.GetString("zipcode")%></label>
                                    :
                                    <asp:Label ID="txtZipCode" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtAdd1" class="w100">
                                        <%= hrmlang.GetString("state")%></label>
                                    :
                                    <asp:Label ID="txtState" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtAdd1" class="w100">
                                        <%= hrmlang.GetString("country")%></label>
                                    :
                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtHPhone" class="w100">
                                        <%= hrmlang.GetString("homephone") %></label>
                                    :
                                    <asp:Label ID="txtHPhone" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtMobile" class="w100">
                                        <%= hrmlang.GetString("mobile") %></label>
                                    :
                                    <asp:Label ID="txtMobile" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtWPhone" class="w100">
                                        <%= hrmlang.GetString("workphone") %></label>
                                    :
                                    <asp:Label ID="txtWPhone" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtWEmail" class="w100">
                                        <%= hrmlang.GetString("workemail")%></label>
                                    :
                                    <asp:Label ID="txtWEmail" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtEmail" class="w100">
                                        <%= hrmlang.GetString("otheremail")%></label>
                                    :
                                    <asp:Label ID="txtEmail" runat="server"></asp:Label>
                                    <div class="clearfix">
                                    </div>
                                    <label for="txtWebsite" class="w100">
                                        <%= hrmlang.GetString("website") %></label>
                                    :
                                    <asp:Label ID="txtWebsite" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <div class="row">
            <section class="col-lg-6">
                <div class="box box-info">
                    <!-- /.box-header -->
                    <div class="box-header bankdet">
                        <h4>
                            <%= hrmlang.GetString("emergencycontactdetails")%></h4>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-body bankdetdt">
                        <div class="row rowmargin">
                            <div class="col-md-12">
                                <asp:GridView ID="gvEmg" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvEmg_RowDataBound"
                                    CssClass="table table-bordered table-striped dataTable" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Contact Name" DataField="ContactName" />
                                        <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                                        <asp:BoundField HeaderText="Home Phone" DataField="HPhone" />
                                        <asp:BoundField HeaderText="Work Phone" DataField="WPhone" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section class="col-lg-6 connectedSortable">
                <!-- /.box-primary -->
                <div class="box box-info">
                    <!-- /.box-header -->
                    <div class="box-header bankdet">
                        <h4>
                            <%= hrmlang.GetString("dependents")%></h4>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="box-body bankdetdt">
                        <div class="row rowmargin">
                            <div class="col-md-12">
                                <asp:GridView ID="gvDependent" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                                    Width="100%" OnRowDataBound="gvDependent_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Dependent Name" DataField="DependentName" />
                                        <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                                        <asp:TemplateField HeaderText="DateOfBirth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.box-primary -->
            </section>
        </div>
        <!-- /.box-primary -->
        <!-- /.box-primary -->
        <div class="box box-info">
            <!-- /.box-header -->
            <div class="box-header education">
                <h4>
                    <%= hrmlang.GetString("education")%></h4>
            </div>
            <div class="box-body
        educationdt">
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered
        table-striped dataTable" OnRowDataBound="gvEducation_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Level" DataField="EduLevelName" />
                                <asp:BoundField HeaderText="University" DataField="University" />
                                <asp:BoundField HeaderText="College" DataField="College" />
                                <asp:BoundField HeaderText="Specialization" DataField="Specialization" />
                                <asp:BoundField HeaderText="Passed Year" DataField="PassedYear" />
                                <asp:BoundField HeaderText="Score
        (%)" DataField="ScorePercentage" />
                                <asp:BoundField HeaderText="Start Date" DataField="StartDate" />
                                <asp:BoundField HeaderText="End Date" DataField="EndDate" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="EduLevel" runat="server" Text='<%#
        Eval("EduLevel") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblEdID" runat="server" Text='<%# Eval("EducationID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!--
        /.box-primary -->
        <div class="box box-info">
            <!-- /.box-header -->
            <div class="box-header expnce">
                    <h4>
                        <%= hrmlang.GetString("experience")%></h4>
            </div>
            <div class="box-body expncedt">
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvExp" runat="server" OnRowDataBound="gvExp_RowDataBound" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped dataTable">
                            <Columns>
                                <asp:BoundField HeaderText="Company" DataField="Company" />
                                <asp:BoundField HeaderText="Job Title" DataField="JobTitle" />
                                <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30)
        ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server"
                                            visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length
        < 30) ? false : true %>'>
                                            <%= hrmlang.GetString("more")%></a>
                                        <asp:Label ID="lblExID" runat="server" Text='<%# Eval("ExpID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body
        -->
        </div>
        <!-- /.box-primary -->
        <div class="box box-info">
            <!-- /.box-header     -->
            <div class="box-header skills">
                    <h4>
                        <%= hrmlang.GetString("skills") %></h4>
            </div>
            <div class="box-body skillsdt">
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvSkill" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            OnRowDataBound="gvSkill_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Skill" DataField="Description" />
                                <asp:BoundField HeaderText="Exp. in Years" DataField="ExpinYears" />
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lComm" runat="server" Visible='<%# (Eval("Comments").ToString().Length==0) ? false : true %>' Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server" visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'><%= hrmlang.GetString("more") %></a>
                                        <asp:Label ID="lblSID" runat="server" Text='<%# Eval("SkillID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body
        -->
        </div>
        <!-- /.box-primary -->
        <div class="box box-info">
            <!-- /.box-header
        -->
            <div class="box-header langs">
                    <h4><%= hrmlang.GetString("language") %></h4>
            </div>
            <div class="box-body langsdt">
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvLang" runat="server" CssClass="table table-bordered table-striped dataTable" AutoGenerateColumns="False" OnrRowDataBound="gvLang_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Language" DataField="LanguageName" />
                                <asp:TemplateField HeaderText="Fluency">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFy" runat="server" Text='<%# Eval("FluencyCode") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFluency" runat="server" Text='<%# Eval("Fluency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Competency">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCy" runat="server" Text='<%# Eval("CompetencyCode") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCompetency" runat="server" Text='<%# Eval("Competency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server"
                                            visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length  < 30) ? false : true %>'><%= hrmlang.GetString("more") %></a>
                                        <asp:Label ID="lblLID" runat="server" Text='<%# Eval("LangID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body
        -->
        </div>
        <!-- /.box-primary -->
        <div class="box box-info">
            <!-- /.box-header
        -->
            <div class="box-header immg">
                    <h4>
                        <%= hrmlang.GetString("immigration")%></h4>
            </div>
            <div class="box-body immgdt">
                <div class="row rowmargin">
                    <asp:GridView ID="gvImmigration" runat="server" CssClass="table table-bordered table-striped dataTable"
                        AutoGenerateColumns="False" OnRowDataBound="gvImmigration_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Doc. Type" DataField="DocType" />
                            <asp:BoundField HeaderText="Doc. No" DataField="DocNo" />
                            <asp:BoundField HeaderText="Issue Date" DataField="IssueDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Expiry Date" DataField="ExpiryDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Eligible Status" DataField="EligibleStatus" />
                            <asp:BoundField HeaderText="Eligible Review Date" DataField="EligibleReviewDate"
                                DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Comments" DataField="Comments" />
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCnID" runat="server" Text='<%# Eval("IssuedCountryID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblIID" runat="server" Text='<%# Eval("ImmigrationId") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!--
        /.box-body -->
        </div>
        <!-- /.box-primary -->
        <div class="box-footer">
        </div>
    </section>
    <%--Education Popup STARTS--%>
    <div class="modal fade" id="dvEducation" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H3">
                        <%= hrmlang.GetString("addeducationdetails")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="ddlEduLevel">
                            <%= hrmlang.GetString("educationlevel") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlEduLevel" runat="server" DataTextField="EduLevelName" DataValueField="EduLevelID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtUniversity">
                            <%= hrmlang.GetString("university")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtUniversity" runat="server" placeholder="University"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtCollege">
                            <%= hrmlang.GetString("college") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtCollege" runat="server" placeholder="College"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSpec">
                            <%= hrmlang.GetString("specialization")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtSpec" runat="server" placeholder="Specialization"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtStartDate">
                            <%= hrmlang.GetString("startdate") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtStartDate" runat="server" CssClass="form-control dtedustart " placeholder="Start Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtEndDate">
                            <%= hrmlang.GetString("enddate") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtEndDate" runat="server" CssClass="form-control dteduend" placeholder="End Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtYear">
                            <%= hrmlang.GetString("yearofpassing")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtYear" runat="server" placeholder="Year of Passing"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtScore">
                            <%= hrmlang.GetString("scoreperc")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtScore" runat="server" placeholder="Score"></asp:Label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <%--Education Popup ENDS--%>
    <%--Experience Popup STARTS--%>
    <div class="modal fade" id="dvExperience" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H4">
                        <%= hrmlang.GetString("addexperiencedetails")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="txtCompany">
                            <%= hrmlang.GetString("company") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtCompany" runat="server" placeholder="Company"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtJobTitle">
                            <%= hrmlang.GetString("jobtitle") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtJobTitle" runat="server" placeholder="University"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtFromDate">
                            <%= hrmlang.GetString("fromdate")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtFromDate" runat="server" CssClass="form-control dtexpfrom" placeholder="From Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtToDate">
                            <%= hrmlang.GetString("todate")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtToDate" runat="server" CssClass="form-control dtexpto" placeholder="To Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtComments">
                            <%= hrmlang.GetString("comments")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="txtComments" runat="server" TextMode="MultiLine" placeholder="Comments"></asp:Label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close")%></button>
                </div>
            </div>
        </div>
    </div>
    <%--Experience Popup ENDS--%>
    <%--Skills Popup STARTS--%>
    <div class="modal fade" id="dvSkills" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H5">
                        <%= hrmlang.GetString("addskill") %></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="txtSkill">
                            <%= hrmlang.GetString("description") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtSkill" runat="server" placeholder="Description"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSkillYear">
                            <%= hrmlang.GetString("experienceinyears")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtSkillYear" runat="server" placeholder="Experience in Years"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSkillComments">
                            <%= hrmlang.GetString("comments") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="txtSkillComments" runat="server" TextMode="MultiLine" placeholder="Comments"></asp:Label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <%--Skills Popup ENDS--%>
    <%--Language Popup STARTS--%>
    <div class="modal fade" id="dvLanguage" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H6">
                        <%= hrmlang.GetString("addlanguage")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="txtLang">
                            <%= hrmlang.GetString("language") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtLang" runat="server" placeholder="Language"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlFluency">
                            <%= hrmlang.GetString("fluency")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlFluency" runat="server">
                            <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                            <asp:ListItem Text="Read,Write" Value="RW"></asp:ListItem>
                            <asp:ListItem Text="Read,Write,Speak" Value="RWS"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlCompetency">
                            <%= hrmlang.GetString("competency")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlCompetency" runat="server">
                            <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                            <asp:ListItem Text="Poor" Value="P"></asp:ListItem>
                            <asp:ListItem Text="Basic" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Good" Value="G"></asp:ListItem>
                            <asp:ListItem Text="Excellent" Value="E"></asp:ListItem>
                            <asp:ListItem Text="Mother Tongue" Value="M"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtLangComments">
                            <%= hrmlang.GetString("comments") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="txtLangComments" runat="server" TextMode="MultiLine" placeholder="Comments"></asp:Label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <%--Language Popup ENDS--%>
    <%--Immigration Popup STARTS--%>
    <div class="modal fade" id="dvImmigration" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H7">
                        <%= hrmlang.GetString("addimmigrationdetails")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="rbtnDocType">
                            <%= hrmlang.GetString("documenttype")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:RadioButtonList ID="rbtnDocType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                            <asp:ListItem Text="Visa" Value="Visa"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtDocNo">
                            <%= hrmlang.GetString("documentno")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtDocNo" runat="server" placeholder="Doc No"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtIssueDate">
                            <%= hrmlang.GetString("issuedate")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtIssueDate" runat="server" CssClass="form-control dtissue" placeholder="Issue Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtExpDate">
                            <%= hrmlang.GetString("expirydate")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtExpDate" runat="server" CssClass="form-control dtexpiry" placeholder="Expiry Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtElgStatus">
                            <%= hrmlang.GetString("eligiblestatus")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtElgStatus" runat="server" placeholder="Eligible Status"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlCountry">
                            <%= hrmlang.GetString("issuedby")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlIssueCountry" runat="server" DataTextField="CountryName"
                            DataValueField="CountryID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtReviewdate">
                            <%= hrmlang.GetString("eligiblereviewdate")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:Label ID="txtReviewdate" runat="server" CssClass="form-control dtreview" placeholder="Eligible Review Date"></asp:Label>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtImmgComments">
                            <%= hrmlang.GetString("comments") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="txtImmgComments" runat="server" TextMode="MultiLine" placeholder="Comments"></asp:Label>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <%--Immigration Popup ENDS--%>
    <asp:HiddenField ID="hdPostBack" runat="server" Value="0" />
</asp:Content>
