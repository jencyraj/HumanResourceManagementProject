<%@ Page Language="C#" MasterPageFile="~/PortalSite.master" AutoEventWireup="true"
    CodeFile="careers.aspx.cs" Inherits="careers" ValidateRequest="false" Title="APPLY NOW" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script src="js/plugins/datepicker/bootstrap-datepicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--    <script type="text/javascript">
         $(function() {
             $('.dtFromDate').datepicker({});
             $('.dtToDate').datepicker({});
             $('.dtYear').datepicker({});
         });

    </script>--%>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(".dtFromDate").datepicker({});
            $('.dtToDate').datepicker({});
            $('.dtYear').datepicker({});
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
            $(".editor2").wysihtml5();
            $(".editor3").wysihtml5();
            $(".editor4").wysihtml5();
        });
    </script>
    <asp:Label ID="lblJID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
<%--  <section class="content-header">
    <h1>APPLY NOW<small></small></h1>     
</section>--%>
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
                <div class="perheader">
                    <h4>
                        <%= hrmlang.GetString("candidateinformation")%></h4>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtJobTitle">
                            <%= hrmlang.GetString("jobtitle") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="form-control validate"
                            DataTextField="JobTitle" DataValueField="JID">
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="ddlsalute">
                            <%= hrmlang.GetString("salutename")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList ID="ddlsalute" runat="server" CssClass="form-control">
                        <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                        <asp:ListItem Text="Mrs." Value="mrs"></asp:ListItem>
                        <asp:ListItem Text="Mr." Value="mr"></asp:ListItem>
                        <asp:ListItem Text="Miss." Value="mis"></asp:ListItem>
                       
                    </asp:DropDownList>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtmName">
                            <%= hrmlang.GetString("fname") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate" placeholder="First Name"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtmName">
                            <%= hrmlang.GetString("mname") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtmName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtmName">
                            <%= hrmlang.GetString("lname") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate" placeholder="Last Name"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtDOB">
                            <%= hrmlang.GetString("dob") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <uc1:ctlDOB ID="ctlCalendardob" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                        <asp:TextBox ID="txtDOB" CssClass="form-control dttxtbox" runat="server" Style="display: none;"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtGender">
                            <%= hrmlang.GetString("gender") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:RadioButtonList ID="rbtnGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtIDNo">
                            <%= hrmlang.GetString("nationality")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control" DataTextField="Nationality"
                            DataValueField="NationalityCode">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="perheader">
                    <h4>
                        <%= hrmlang.GetString("contactinformation")%></h4>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtAdd1">
                        Address</div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control validate" placeholder="Address"></asp:TextBox>
                    </div>
                </div>
                <%--                    <div class="row rowmargin">
                        <div class="col-xs-4">
                        <label for="txtAdd2">
                            <%= hrmlang.GetString("addressline2")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                          <asp:TextBox ID="txtAdd2" runat="server" CssClass="form-control" placeholder="Address Line 2"></asp:TextBox>
                        </div>
                    </div>--%>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtCity">
                            <%= hrmlang.GetString("city") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control validate" placeholder="City"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtState">
                            <%= hrmlang.GetString("state") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control validate"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtZipCode">
                            <%= hrmlang.GetString("zipcode") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control validate" placeholder="Zip Code"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtmName">
                            <%= hrmlang.GetString("country") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="CountryName"
                            DataValueField="CountryID">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtEmail">
                            <%= hrmlang.GetString("emailaddress")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control validate" placeholder="Other Email"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtHPhone">
                            <%= hrmlang.GetString("phoneno") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtHPhone" runat="server" CssClass="form-control" placeholder="enterphoneno"></asp:TextBox>
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-4">
                        <label for="txtMobile">
                            <%= hrmlang.GetString("mobileno") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control validate" placeholder="entermobileno"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="perheader">
                    <h4>
                        <%-- <%= hrmlang.GetString("candidatequalification")%></h4>--%>
                        <%= hrmlang.GetString("education")%>
                </div>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:UpdatePanel ID="updpnlCompanies" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvqualification" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                                OnRowCommand="gvQualification_RowCommand" OnRowDataBound="gvQualification_RowDataBound"
                                ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Education Level">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblEducationLevel" runat="server"></asp:Label>--%>
                                            <%# Eval("EducationLevel")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlEducationLevel" runat="server" CssClass="form-control" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblCollege" runat="server"></asp:Label>--%>
                                            <%# Eval("College")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCollege" runat="server" CssClass="form-control" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="University">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblUniversity" runat="server"></asp:Label>--%>
                                            <%# Eval("University")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUniversity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specialization">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSpecialization" runat="server"></asp:Label>--%>
                                            <%# Eval("Specialization")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year Of Passing">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblYear" runat="server"></asp:Label>--%>
                                            <%# Eval("Year")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtYear" runat="server" CssClass="dtYear form-control" ></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Score(%)">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblScore" runat="server"></asp:Label>--%>
                                            <%# Eval("Score")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" Visible="true" runat="server" CssClass="fa fa-trash-o"
                                                CommandArgument='<%# Eval("RowNumber") %>' data-toggle="tooltip" title="Delete"
                                                CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="AddQualificationDetails"
                                                CssClass="btn btn-primary" CommandArgument='ADD' CommandName="Footer" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="perheader">
                    <h4>
                        <%= hrmlang.GetString("experience")%></h4>
                </div>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvExperience" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                                OnRowCommand="gvExperience_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblEducationLevel" runat="server"></asp:Label>--%>
                                            <%# Eval("Company")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JobTitle">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblCollege" runat="server"></asp:Label>--%>
                                            <%# Eval("JobTitle")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromDate">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblUniversity" runat="server"></asp:Label>--%>
                                            <%# Eval("FromDate")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFromDate" CssClass="dtFromDate form-control" runat="server" ></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToDate">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSpecialization" runat="server"></asp:Label>--%>
                                            <%# Eval("ToDate")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtToDate" CssClass="dtToDate form-control" runat="server" ></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason for Leaving">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblYear" runat="server"></asp:Label>--%>
                                            <%# Eval("Reason")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkExpDelete" Visible="true" runat="server" CssClass="fa fa-trash-o"
                                                CommandArgument='<%# Eval("RowNumber") %>' data-toggle="tooltip" title="Delete"
                                                CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnExpAdd" runat="server" Text="Add" OnClick="AddExperienceDetails"
                                                CssClass="btn btn-primary" CommandArgument='ADD' CommandName="Footer" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        

        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtSkills">
                                <%= hrmlang.GetString("skills")%></label>
                            <asp:TextBox ID="txtSkills" CssClass="editor4 form-control" runat="server" TextMode="MultiLine"
                                Style="height: 200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtinterests">
                                <%= hrmlang.GetString("interests")%></label>
                            <asp:TextBox ID="txtinterests" CssClass="editor1 form-control" runat="server" TextMode="MultiLine"
                                Style="height: 200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtExperience">
                                <%= hrmlang.GetString("achievements")%></label>
                            <asp:TextBox ID="txtachievements" CssClass="editor1 form-control" runat="server"
                                TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
        
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="perheader">
                    <h4>
                        <%= hrmlang.GetString("language") %></h4>
                </div>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvLanguage" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                                OnRowCommand="gvLanguage_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Language">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblEducationLevel" runat="server"></asp:Label>--%>
                                            <%# Eval("LanguageName")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLanguageName" runat="server" CssClass="form-control" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fluency">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblCollege" runat="server"></asp:Label>--%>
                                            <%# Eval("Fluency")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFluency" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Read,Write" Value="RW"></asp:ListItem>
                                                <asp:ListItem Text="Read,Write,Speak" Value="RWS"></asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Competency">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblUniversity" runat="server"></asp:Label>--%>
                                            <%# Eval("Competency")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCompetency" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Poor" Value="P"></asp:ListItem>
                                                <asp:ListItem Text="Basic" Value="B"></asp:ListItem>
                                                <asp:ListItem Text="Good" Value="G"></asp:ListItem>
                                                <asp:ListItem Text="Excellent" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Mother Tongue" Value="M"></asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSpecialization" runat="server"></asp:Label>--%>
                                            <%# Eval("Comments")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtComments" runat="server" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLanDelete" Visible="true" runat="server" CssClass="fa fa-trash-o"
                                                CommandArgument='<%# Eval("RowNumber") %>' data-toggle="tooltip" title="Delete"
                                                CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnLinAdd" runat="server" Text="Add" OnClick="AddLanguageDetails"
                                                CssClass="btn btn-primary" CommandArgument='ADD' CommandName="Footer" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
                
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="perheader">
                    <h4>
                        References</h4>
                </div>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvReference" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable">
                                <Columns>
                                    <asp:TemplateField HeaderText="#1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRefName1" runat="server" CssClass="form-control" placeholder="Reference Name"></asp:TextBox>
                                            <asp:TextBox ID="txtOrg1" runat="server" CssClass="form-control" placeholder="Organisation"></asp:TextBox> 
                                            <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                            <asp:TextBox ID="txtPhone1" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#2">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRefName2" runat="server" CssClass="form-control" placeholder="Reference Name"></asp:TextBox>
                                            <asp:TextBox ID="txtOrg2" runat="server" CssClass="form-control" placeholder="Organisation"></asp:TextBox>
                                            <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                            <asp:TextBox ID="txtPhone2" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRefName3" runat="server" CssClass="form-control" placeholder="Reference Name"></asp:TextBox>
                                            <asp:TextBox ID="txtOrg3" runat="server" CssClass="form-control" placeholder="Organisation"></asp:TextBox>
                                            <asp:TextBox ID="txtEmail3" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                            <asp:TextBox ID="txtPhone3" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>

                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtDescription">
                                <%= hrmlang.GetString("additionalinformation")%></label>
                            <asp:TextBox ID="txtaddInfo" CssClass="editor2 form-control" runat="server" TextMode="MultiLine"
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
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
