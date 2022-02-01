<%@ Page Title="View Candidate Profile" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CandidateProfile.aspx.cs" Inherits="CandidateProfile" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblJID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
    <h1><%= hrmlang.GetString("candidateprofile")%></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("managecandidateprofile")%></li>
    </ol>
</section>
    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
            <section class="col-lg-5 connectedSortable">
                <!-- quick email widget -->
                <div class="box box-info">
                    <div class="box-header">
                       <h3 class="box-title">
                            <%= hrmlang.GetString("basicinfo")%></h3>
                      
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <label class="w100">
                                <%= hrmlang.GetString("jobtitle")%></label>
                            :
                            <asp:Label ID="lbljobtitle" runat="server"></asp:Label><br />
                              <label class="w100">
                                <%= hrmlang.GetString("applieddate")%></label>
                            :
                            <asp:Label ID="lblapplieddate" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("status")%></label>
                            :
                            <asp:Label ID="lblappstatus" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("name")%></label>
                            :
                     <asp:Label ID="lblsalutename" runat="server"></asp:Label>         <asp:Label ID="lblfname" runat="server"></asp:Label><asp:Label ID="lblmname" runat="server"></asp:Label>
                            <asp:Label ID="lblname" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("dob")%></label>
                            :
                            <asp:Label ID="lbldob" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("gender")%></label>
                            :
                            <asp:Label ID="lblgen" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("nationality")%></label>
                            :
                            <asp:Label ID="lblnatn" runat="server"></asp:Label><br />
                     
                           <a href="#" class="fr-right changestatus" ><%= hrmlang.GetString("changestatus")%></a>
                        </div>
                    </div>
                </div>
            </section><!-- /.Left col -->
               <section class="col-lg-7 connectedSortable">
                <!-- quick email widget -->
                <div class="box box-info">
              <%--  <asp:LinkButton ></asp:LinkButton>--%>
            
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("contactinformation")%></h3>
                    </div>
                    <div class="box-body">
                        

                        <div class="form-group">
                            <label class="w100">
                                <%= hrmlang.GetString("address")%></label>
                            :
                            <asp:Label ID="lbladd" runat="server"></asp:Label><br />
                              <label class="w100">
                                <%= hrmlang.GetString("city")%></label>
                            :
                            <asp:Label ID="lblcity" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("state")%></label>
                            :
                            <asp:Label ID="lblstate" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("zipcode")%></label>
                            :
                            <asp:Label ID="lblzip" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("country")%></label>
                            :
                            <asp:Label ID="lblcountry" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("emailaddress")%></label>
                            :
                            <asp:Label ID="lblemail" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("phoneno")%></label>
                            :
                            <asp:Label ID="lblphnno" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("mobileno")%></label>
                            :
                               <asp:Label ID="lblmobn" runat="server"></asp:Label><br />
                          <div>
                          </div>
                               <%-- <asp:LinkButton runat="server"  class="fr-right" 
                                ID="linkbtn" CommandArgument="lblcommand" CommandName="FILL" ><%= hrmlang.GetString("changeapplicationstatus")%>
                          </asp:LinkButton>--%>
           
                    
                        </div>
                           
                    </div>

                   
                <div class="clearfix">
                </div>
           
                </div>
            </section>



        </div>
                <div class="row"> 
                  <section  class="content">
               
               <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("education")%></h3>
                    </div>
                   
                   <asp:GridView ID="gvqualification" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable" >
                                <Columns>
                                    <asp:BoundField DataField="RowNumber">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Education Level">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEducationLevel" runat="server"></asp:Label>
                                            <%# Eval("EducationLevel")%>
                                        </ItemTemplate>
                                       </asp:TemplateField> 
                                      <asp:TemplateField HeaderText="College">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollege" runat="server"></asp:Label>
                                            <%# Eval("College")%>
                                        </ItemTemplate>
                                       </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="University">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUniversity" runat="server"></asp:Label>
                                            <%# Eval("University")%>
                                        </ItemTemplate>
                                         </asp:TemplateField> 
                                           <asp:TemplateField HeaderText="Specialization">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecialization" runat="server"></asp:Label>
                                            <%# Eval("Specialization")%>
                                        </ItemTemplate>
                                           </asp:TemplateField> 
                                             <asp:TemplateField HeaderText="Year Of Passing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server"></asp:Label>
                                            <%# Eval("Year")%>
                                        </ItemTemplate>

                                    </asp:TemplateField> 

                                      <asp:TemplateField HeaderText="Score(%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblScore" runat="server"></asp:Label>
                                            <%# Eval("Score")%>
                                        </ItemTemplate>
                                     </asp:TemplateField> 

                                </Columns>
                            </asp:GridView>
                            </div></section>
                            
                             <section  class="content">
               
               <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("experience")%></h3>
                    </div>
                             <asp:GridView ID="gvExperience" runat="Server" AutoGenerateColumns="False" 
                             CssClass="table table-bordered table-striped dataTable">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <%# Eval("Company")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JobTitle">
                                        <ItemTemplate>
                                            <%# Eval("JobTitle")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromDate">
                                        <ItemTemplate >
                                            <%# Eval("FromDate","{0:dd/MMM/yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToDate">
                                        <ItemTemplate>
                                         <%-- <asp:Label ID="lblComm"  runat="server" Text='<%# String.Format("{0:dd/MMM/yyyy}" ,Eval("ToDate"))%>' ></asp:Label>
                                         --%>
                                          <%# Eval("ToDate", "{0:D}")%>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason for Leaving">
                                        <ItemTemplate>
                                        <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Reason") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Reason").ToString().Length>30) ? Eval("Reason").ToString().Substring(0,30)+"..." : Eval("Reason") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Reason") %>' id="lnkCom" runat="server"
                                            visible='<%# (Eval("Reason").ToString().Length==0 || Eval("Reason").ToString().Length < 30) ? false : true %>'>
                                            <%= hrmlang.GetString("more")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                            </asp:GridView>
                            </div></section> 
                     <section  class="content">
               <div class="box box-info">
            <!-- /.box-header -->
            <div class="box-body">
               <div class="box-header">
                        <h3 class="box-title" style="padding-left:0px;">
                            <%= hrmlang.GetString("skills")%> / <%= hrmlang.GetString("interests")%> / <%= hrmlang.GetString("achievements")%></h3>
                    </div>
              <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                                <label for="txtSkills">
                                <%= hrmlang.GetString("skills")%></label> 
                                :
                            <asp:Label ID="lblSkills" runat="server"></asp:Label>
                            </div>
                            </div>
                            </div>
                              <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                              <label for="txtinterests">
                                <%= hrmlang.GetString("interests")%></label>
                                :
                            <asp:Label ID="lblInterests" runat="server"></asp:Label>
                            </div>
                            </div>
                            </div>
                              <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                                <label for="txtExperience">
                                <%= hrmlang.GetString("achievements")%></label>
                           :
                            <asp:Label ID="lblAchievements" runat="server"></asp:Label>
                            </div></div></div>
                            </div></div></section>
                             <section  class="content">
                               <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("language")%></h3>
                    </div>
                              <asp:GridView ID="gvLanguage" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable" >
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
                                            <%# Eval("Fluency")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Competency">
                                        <ItemTemplate>
                                            <%# Eval("Competency")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server"
                                            visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'>
                                            <%= hrmlang.GetString("more")%></a>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div></section>
                             <section  class="content">
                               <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("references")%></h3>
                    </div>
                                <asp:GridView ID="gvReference" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable">
                                <Columns>
                                    <asp:TemplateField HeaderText="#1">
                                        <ItemTemplate>
                                             <asp:Label ID="lblEducationLevel" runat="server" ></asp:Label>
                                            <%# Eval("RefName")%><br />

                                           <asp:Label ID="lblorgan" runat="server" ></asp:Label>
                                            <%# Eval("Organisation")%><br />
                                             <asp:Label ID="lblemail" runat="server"></asp:Label>
                                            <%# Eval("Email")%><br />
                                           <asp:Label ID="lblphn" runat="server"></asp:Label>
                                            <%# Eval("Phone")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#2">
                                        <ItemTemplate>
                                         <asp:Label ID="txtRefName2" runat="server" ></asp:Label>
                                            <%# Eval("RefName")%><br />

                                           <asp:Label ID="txtOrg2" runat="server" ></asp:Label>
                                            <%# Eval("Organisation")%><br />
                                             <asp:Label ID="txtEmail2" runat="server"></asp:Label>
                                            <%# Eval("Email")%><br />
                                           <asp:Label ID="txtPhone2" runat="server"></asp:Label>
                                            <%# Eval("Phone")%>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#3">
                                        <ItemTemplate>
                                          <asp:Label ID="txtRefName3" runat="server" ></asp:Label>
                                            <%# Eval("RefName")%><br />

                                           <asp:Label ID="txtOrg3" runat="server" ></asp:Label>
                                            <%# Eval("Organisation")%><br />
                                             <asp:Label ID="txtEmail3" runat="server"></asp:Label>
                                            <%# Eval("Email")%><br />
                                           <asp:Label ID="txtPhone3" runat="server"></asp:Label>
                                            <%# Eval("Phone")%>

                                        </ItemTemplate>
                                      
                                    </asp:TemplateField> 


                                </Columns>
                            </asp:GridView>

                        

                    
                    </div>
                    </section>
            <section  class="content">
                   <div class="box box-info">
                      <div class="box-body">
                          <div class="box-header">
                               <h3 class="box-title" style="padding-left:0px;">
                                    <%= hrmlang.GetString("additionalinformation")%>
                               </h3>
                          </div>
                       <div class="row">
                          <div class="col-md-10">
                             <div class="form-group">
                                    <asp:Label ID="lblAdditional" runat="server"></asp:Label>
                             </div>
                          </div>
                     </div>
                   </div>
                 </div>
           </section>
                                
             
    </div>
      <script type="text/javascript">

        $(function () {
            $('.changestatus').click(function () {
                $('#dvStatus').modal();
            });
        });

         $().ready(function () {
            $('.form-control').change(function () {
                if ($(this).val() == "IVW") {

                    $("#ctlCalDepDoeeb").show();
                }
                else {
                    $("#ctlCalDepDoeeb").hide();
                }

            });
        });


            </script>

     

     <%--Dependents Popup STARTS--%>
    <div class="modal fade" id="dvStatus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H2">
                        <%= hrmlang.GetString("changeapplicationstatus")%></h4>
                </div>
                <div class="modal-header rowmargin"   >
                    <div class="form-group">
                            <label for="ddlDesgn">
                                <%= hrmlang.GetString("statuschangeto")%><asp:CompareValidator ID="CompareValidator3"
                                    runat="server" ControlToValidate="ddlStatus" Operator="NotEqual" Type="String"
                                    ValueToCompare="0" ValidationGroup="maingrp" ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"  
                                ValidationGroup="maingrp"   >
                          
                            </asp:DropDownList>
                        </div>
                    <div class="clearfix">
                    </div>
                   
                  
                    <div class="clearfix">
                    </div>
                   
                    <div class="col-xs-7">
                        <%--<label for="txtstatus"  id="lbldate"  >
                            <%= hrmlang.GetString("selectdate")%></label>--%></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7" style="display:none;" id="ctlCalDepDoeeb">
                        <uc2:ctlJoin ID="ctlCalDepDob" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="txtstatus" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder="Tnterview Scheduled time"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveDep" runat="server" CssClass="btn btn-primary" ValidationGroup="depgroup" Text="Save" OnClick="btnSaveCandidateStatus_Click"  />
                    <asp:Button ID="btnSendEmail" runat="server" CssClass="btn btn-primary" ValidationGroup="depgroup" Text="Send Email To Candidate" OnClick="btnsendEmail"   />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <%--Dependents Popup ENDS--%>

    </section>
  </asp:Content>
