<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlDOB" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="lblImg" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("manageprofile")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageprofile")%></li>
    </ol>
</section>
<script src="js/plugins/datepicker/bootstrap-datepicker.js" ></script>
<script type="text/javascript">
   $(function() {
       $('.dttxtbox').datepicker({});
       $('.dtjoin').datepicker({});
       $('.dtprobstart').datepicker({});
       $('.dtprobend').datepicker({});
       $('.dtdepdob').datepicker({});
       $('.dtedustart').datepicker({});
       $('.dteduend').datepicker({});
       $('.dtexpfrom').datepicker({});
       $('.dtexpto').datepicker({});
       $('.dtissue').datepicker({});
       $('.dtexpiry').datepicker({});
       $('.dtreview').datepicker({});
  });
</script>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
             <div  class="pull-left dblock rowmargin">  
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlBranch">
                                <%= hrmlang.GetString("branch") %></label><asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch"
                             Operator="NotEqual" Type="String" ValueToCompare="0"
                                ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator>
                               <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" 
                            DataTextField="Branch" DataValueField="BranchID" AutoPostBack="true" 
                            onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="ddlStatus">
                                <%= hrmlang.GetString("status") %></label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Active" Value="C"></asp:ListItem>
                            <asp:ListItem Text="InActive" Value="P"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtEmpCode">
                            <%= hrmlang.GetString("employeecode")%></label></label>
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate" placeholder="Employee Code"></asp:TextBox> 
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlDept">
                                <%= hrmlang.GetString("maindepartment")%></label><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlDept"
                         Operator="NotEqual" Type="String" ValueToCompare="0"
                            ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator>
                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" 
                            DataTextField="DepartmentName" DataValueField="DepartmentID" 
                            AutoPostBack="True" onselectedindexchanged="ddlDept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="ddlAssetType">
                            <%= hrmlang.GetString("role") %><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlRole"
                         Operator="NotEqual" Type="String" ValueToCompare="0"
                            ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></label>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName" DataValueField="RoleID">
                    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtUserID">
                            <%= hrmlang.GetString("userid") %></label>
                        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control validate"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlDept">
                                <%= hrmlang.GetString("department")%></label>
                        <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" DataTextField="DepartmentName" DataValueField="DepartmentID">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="ddlDesgn">
                            <%= hrmlang.GetString("designation") %><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlDesgn"
                         Operator="NotEqual" Type="String" ValueToCompare="0"
                            ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></label>
                         <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation" DataValueField="DesignationID">
                         </asp:DropDownList>                          
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">
                            <%= hrmlang.GetString("password") %></label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control validate"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2 f-right">
                    <div class="form-group">
                        <asp:Image ID="imgPhoto" runat="server" Height="135px" /><br />
                        <asp:FileUpload ID="fpPhoto" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
         </div> <!-- /.box-body -->
        </div> <!-- /.box-primary -->
       
        <div class="box box-primary">
        <!-- /.box-header -->
         <div class="perheader"><h4><%= hrmlang.GetString("personaldetails")%></h4></div>
        <div class="clearfix">
            </div>
        <div class="box-body personaldt">
            <div class="row rowmargin">
            <div class="col-xs-3" style="width:100px">
                    <label for="ddlsalute">
                        <%= hrmlang.GetString("salutename")%></label></div>
                <div class="col-xs-3" style="width:200px">
                    <label for="txtfName" >
                        <%= hrmlang.GetString("fname") %></label></div>
                <div class="col-xs-3">
                    <label for="txtmName">
                        <%= hrmlang.GetString("mname") %></label></div>
                <div class="col-xs-3">
                    <label for="txtlName">
                        <%= hrmlang.GetString("lname") %></label></div>
                <div class="clearfix">
                </div>
                 <div class="col-xs-3" style="width:100px">
                 <asp:DropDownList ID="ddlsalute" runat="server" CssClass="form-control">
                        <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                        <asp:ListItem Text="Mrs." Value="mrs"></asp:ListItem>
                        <asp:ListItem Text="Mr." Value="mr"></asp:ListItem>
                        <asp:ListItem Text="Miss." Value="mis"></asp:ListItem>
                       
                    </asp:DropDownList>
                 </div>
                <div class="col-xs-3" style="width:200px">
                    <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate" placeholder="First Name" ></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtmName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate" placeholder="Last Name"></asp:TextBox>
                            <asp:TextBox ID="txtBiometricId" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtIrisId" runat="server" Visible="false"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtEmpCode">
                        <%= hrmlang.GetString("gender") %></label></div>
                <div class="col-xs-3">
                    <label for="txtDOB">
                        <%= hrmlang.GetString("dob")%></label></div>
                <div class="col-xs-3">
                    <label for="txtIDDesc">
                        <%= hrmlang.GetString("maritalstatus")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:RadioButtonList ID="rbtnGender" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-xs-3">
                <uc1:ctlDOB ID="ctlCalendardob" runat="server" DefaultCalendarCulture="Grgorian" 
                        MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                    <asp:TextBox ID="txtDOB" CssClass="form-control dttxtbox" runat="server" style="display:none;"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlMarital" runat="server" CssClass="form-control">
                        <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                        <asp:ListItem Text="Single" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Married" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Widowed" Value="W"></asp:ListItem>
                        <asp:ListItem Text="Divorced" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>        
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtIDNo">
                        <%= hrmlang.GetString("nationality")%></label></div>            
                <div class="col-xs-3">
                    <label for="txtIDDesc">
                        <%= hrmlang.GetString("iddescription")%></label></div>
                <div class="col-xs-3">
                    <label for="txtIDNo">
                        <%= hrmlang.GetString("idno") %></label></div>
                <div class="clearfix">
                </div>

                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control" DataTextField="Nationality" DataValueField="NationalityCode">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtIDDesc" runat="server" CssClass="form-control" placeholder="ID Description"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" placeholder="ID No"></asp:TextBox>
                </div>
            </div>   
            <div class="row rowmargin">
                    <div class="col-xs-3">
                        <label for="txtIDNo">
                            <%= hrmlang.GetString("bloodgrp")%></label></div>
                             <div class="clearfix">
                    </div>
                              <div class="col-xs-3">
                        <asp:TextBox ID="txtbloodgrp" runat="server" CssClass="form-control" placeholder="Blood Group"></asp:TextBox>
                    </div>
                            </div> 
          </div> <!-- /.box-body -->
        </div> <!-- /.box-primary -->
                
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header jobdet">
         <div class="perheader"><h4><%= hrmlang.GetString("jobdetails")%></h4></div>
        </div>
        <div class="box-body jobdetdt">
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="ddlJobType"><%= hrmlang.GetString("employmentstatus")%><asp:Label ID="lblNewStatus" runat="server" CssClass="badge pull-right bg-green newstatus" ></asp:Label>
                    </label>
                </div>
                <div class="col-xs-3">
                    <label for="txtJoinDate">
                        <%= hrmlang.GetString("joiningdate")%></label></div>
                <div class="col-xs-3">
                    <label for="txtProbStart">
                        <%= hrmlang.GetString("probationstartdate")%></label></div>
                <div class="col-xs-3">
                    <label for="txtProbEnd">
                         <%= hrmlang.GetString("probationenddate")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control" DataTextField="Description" DataValueField="EmplStatusID"></asp:DropDownList>
                </div>
                <div class="col-xs-3">
                    <uc2:ctlJoin ID="ctlCalJoin" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                </div>
                <div class="col-xs-3">
                    <uc2:ctlJoin ID="ctlCalProbStart" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" /> 
                </div>
                <div class="col-xs-3"> 
                    <uc2:ctlJoin ID="ctlCalProbEnd" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="5"
                            MinYearCountFromNow="-5" /> 
                </div>
            </div> 
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->

        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header bankdet">
         <div class="perheader"><h4><%= hrmlang.GetString("bankdetails")%></h4></div>
        </div>
        <div class="clearfix">
            </div>
        <div class="box-body bankdetdt">   
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtBank">
                        <%= hrmlang.GetString("bankname")%></label></div>            
                <div class="col-xs-3">
                    <label for="txtBranch">
                        <%= hrmlang.GetString("bankbranch")%></label></div>
                <div class="col-xs-3">
                    <label for="txtAccNo">
                        <%= hrmlang.GetString("accountno")%></label></div>
                <div class="col-xs-3">
                    <label for="txtOther">
                        <%= hrmlang.GetString("otherdetails")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtBank" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtOther" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div> <!-- /.box-body -->
        </div> <!-- /.box-primary -->

        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header contact">
         <div class="perheader"><h4>Contact Details</h4></div>
        </div>
        <div class="box-body contactdt">
        <div class="row rowmargin">
            <div class="col-xs-3">
                <label for="txtAdd1">
                    <%= hrmlang.GetString("addressline1")%></label></div>
            <div class="col-xs-3">
                <label for="txtAdd2">
                    <%= hrmlang.GetString("addressline2")%></label></div>
            <div class="col-xs-3">
                <label for="txtCity">
                    <%= hrmlang.GetString("city") %></label></div>
            <div class="clearfix">
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control validate"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtAdd2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control validate"></asp:TextBox>
            </div>
        </div>
        <div class="row rowmargin">
            <div class="col-xs-3">
                <label for="txtState">
                    <%= hrmlang.GetString("state") %></label></div>
            <div class="col-xs-3">
                <label for="txtZipCode">
                    <%= hrmlang.GetString("zipcode") %></label></div>
            <div class="col-xs-3">
                <label for="txtmName">
                    <%= hrmlang.GetString("country") %></label></div>           
            <div class="clearfix">
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtState" runat="server" CssClass="form-control validate" placeholder=" State"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control validate" placeholder="Zip Code"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="CountryName" DataValueField="CountryID"></asp:DropDownList>
            </div>
        </div>
        <div class="row rowmargin">
            <div class="col-xs-3">
                <label for="txtHPhone">
                    <%= hrmlang.GetString("homephone") %></label></div>
            <div class="col-xs-3">
                <label for="txtMobile">
                    <%= hrmlang.GetString("mobile") %></label></div>
            <div class="col-xs-3">
                <label for="txtWPhone">
                    <%= hrmlang.GetString("workphone") %></label></div>
            <div class="clearfix">
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtHPhone" runat="server" CssClass="form-control" placeholder="Home Phone"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control validate" placeholder="Mobile"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtWPhone" runat="server" CssClass="form-control" placeholder="Work Phone"></asp:TextBox>
            </div>
        </div>        
        <div class="row rowmargin">
            <div class="col-xs-3">
                <label for="txtWEmail">
                    <%= hrmlang.GetString("workemail")%></label></div>
            <div class="col-xs-3">
                <label for="txtEmail">
                    <%= hrmlang.GetString("otheremail")%></label></div>
            <div class="col-xs-3">
                <label for="txtWebsite">
                    <%= hrmlang.GetString("website") %></label></div>
            <div class="clearfix">
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtWEmail" runat="server" CssClass="form-control" placeholder="Work Email"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control validate" placeholder="Other Email"></asp:TextBox>
            </div>
            <div class="col-xs-3">
                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Website"></asp:TextBox>
            </div>
        </div>
        </div> <!-- /.box-body -->
        </div> <!-- /.box-primary -->
       
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header emgcy">
         <div class="perheader"><h4><%= hrmlang.GetString("emergencycontactdetails")%></h4></div>
        </div>
        <div class="box-body emgcydt">
            <div class="row rowmargin">
                <div class="f-right col-xs-1">
                    <asp:Label ID="lblAddEmg" runat="server" CssClass="btn btn-success btn-sm newemg" Text="Add" ></asp:Label>
                </div>
                <div class="clearfix"></div>
                <div class="col-xs-12">
                     <asp:GridView ID="gvEmg" runat="server" AutoGenerateColumns="false" 
                        OnRowCommand="gvEmg_RowCommand" OnRowDataBound="gvEmg_RowDataBound" CssClass="table table-bordered table-striped dataTable" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="Contact Name" DataField="ContactName" />
                            <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                            <asp:BoundField HeaderText="Home Phone" DataField="HPhone" />
                            <asp:BoundField HeaderText="Work Phone" DataField="WPhone" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblEID" runat="server" Text='<%# Eval("EmgContactID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("EmgContactID") %>'
                                        CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EmgContactID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Contact?')" CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </div>
            </div>                          
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->        

          <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header depend">
         <div class="perheader"><h4><%= hrmlang.GetString("dependents")%></h4></div>
        </div>
        <div class="box-body dependdt">
            <div class="row rowmargin">
                <div class="f-right col-xs-1">
                    <asp:Label ID="lblAddDep" runat="server" CssClass="btn btn-success btn-sm newdependent"></asp:Label>
                </div>
                <div class="col-xs-12">
                     <asp:GridView ID="gvDependent" runat="server" AutoGenerateColumns="false" 
                         CssClass="table table-bordered table-striped dataTable" Width="100%" 
                         OnRowCommand="gvDependent_RowCommand" 
                         OnRowDataBound="gvDependent_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Dependent Name" DataField="DependentName" />
                            <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                            <asp:TemplateField HeaderText="DateOfBirth" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblDID" runat="server" Text='<%# Eval("DependentID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("DependentID") %>'
                                        CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("DependentID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Dependent?')" CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </div>
            </div>   
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header education">
         <div class="perheader"><h4><%= hrmlang.GetString("education")%></h4></div>
        </div>
        <div class="box-body educationdt">
            <div class="row rowmargin">
                <div class="f-right col-xs-3">
                    <asp:Label ID="Label1" runat="server" CssClass="btn btn-success btn-sm newedulevel" ><%= hrmlang.GetString("addeducationlevel")%></asp:Label>&nbsp;<asp:Label ID="lblAdAlwu" runat="server" CssClass="btn btn-success btn-sm newedu"></asp:Label>
                </div>
               <div class="clearfix"></div>
            <div class="col-xs-12">
               <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped dataTable" 
                    onrowcommand="gvEducation_RowCommand" OnRowDataBound="gvEducation_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Level" DataField="EduLevelName" />
                        <asp:BoundField HeaderText="University" DataField="University" />
                        <asp:BoundField HeaderText="College" DataField="College" />
                        <asp:BoundField HeaderText="Specialization" DataField="Specialization" />
                        <asp:BoundField HeaderText="Passed Year" DataField="PassedYear" />
                        <asp:BoundField HeaderText="Score (%)" DataField="ScorePercentage" />
                        <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0:d}" />
                        <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="EduLevel" runat="server" Text='<%# Eval("EduLevel") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblEdID" runat="server" Text='<%# Eval("EducationID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("EducationID") %>'
                                        CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("EducationID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Education Details?')" CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
              </asp:GridView>
            </div>              
          </div>              
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header expnce">
         <div class="perheader"><h4><%= hrmlang.GetString("experience")%></h4></div>
        </div>        
        <div class="box-body expncedt">
            <div class="row rowmargin">
            <div class="f-right col-xs-1">
                <asp:Label ID="lblAddExp" runat="server" CssClass="btn btn-success btn-sm newexp"></asp:Label>
                        </div>
                        <div class="clearfix"></div>
            <div class="col-xs-12">
                <asp:GridView ID="gvExp" runat="server" onrowcommand="gvExp_RowCommand" OnRowDataBound="gvExp_RowDataBound"
                    AutoGenerateColumns="false" 
                    CssClass="table table-bordered table-striped dataTable">
                    <Columns>
                        <asp:BoundField DataField="Company" />
                        <asp:BoundField DataField="JobTitle" />
                        <asp:BoundField DataField="FromDate" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ToDate" DataFormatString="{0:d}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label> 
                                <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server" visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'><%= hrmlang.GetString("more")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblExID" runat="server" Text='<%# Eval("ExpID") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("ExpID") %>'
                                    CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("ExpID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Experience Details?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>              
            </div>             
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header skills">
         <div class="perheader"><h4><%= hrmlang.GetString("skills") %></h4></div>
        </div>
        <div class="box-body skillsdt">
            <div class="row rowmargin">
            <div class="f-right col-xs-1">
                <asp:Label ID="lblAddSkill" runat="server" CssClass="btn btn-success btn-sm newskill"></asp:Label>
                        </div>
                        <div class="clearfix"></div>
            <div class="col-xs-12">
                <asp:GridView ID="gvSkill" runat="server" AutoGenerateColumns="false" 
                    CssClass="table table-bordered table-striped dataTable" 
                    onrowcommand="gvSkill_RowCommand" OnRowDataBound="gvSkill_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Skill" DataField="Description" />
                        <asp:BoundField HeaderText="Exp. in Years" DataField="ExpinYears" />
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lComm" runat="server" Visible='<%# (Eval("Comments").ToString().Length==0) ? false : true %>' Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label> 
                                <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server" visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'><%= hrmlang.GetString("more") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblSID" runat="server" Text='<%# Eval("SkillID") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("SkillID") %>'
                                    CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("SkillID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Skill?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>              
            </div>              
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header langs">
         <div class="perheader"><h4><%= hrmlang.GetString("language") %></h4></div>
        </div>
        <div class="box-body langsdt">
            <div class="row rowmargin">
            <div class="f-right col-xs-1">
                <asp:Label ID="lblAddLanguage" runat="server" CssClass="btn btn-success btn-sm newlang"></asp:Label>
                        </div>
                        <div class="clearfix"></div>
                <div class="col-xs-12">
                    <asp:GridView ID="gvLang" runat="server" CssClass="table table-bordered table-striped dataTable" 
                    AutoGenerateColumns="False" OnRowCommand="gvLang_RowCommand" 
                    OnrRowDataBound="gvLang_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Language" DataField="LanguageName" />
                        <asp:TemplateField HeaderText="Fluency">
                            <ItemTemplate>
                                <asp:Label ID="lblFy" runat="server" Text='<%# Eval("FluencyCode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblFluency" runat="server" Text='<%# Eval("Fluency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Competency" >
                           <ItemTemplate>
                                <asp:Label ID="lblCy" runat="server" Text='<%# Eval("CompetencyCode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblCompetency" runat="server" Text='<%# Eval("Competency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label> 
                                <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server" visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'><%= hrmlang.GetString("more") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblLID" runat="server" Text='<%# Eval("LangID") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("LangID") %>'
                                    CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LangID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Language?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>              
            </div>              
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-header immg">
         <div class="perheader"><h4><%= hrmlang.GetString("immigration")%></h4></div>
        </div>
        <div class="box-body immgdt">
            <div class="row rowmargin">
            <div class="f-right col-xs-1">
                <asp:Label ID="lblAddImmg" runat="server" CssClass="btn btn-success btn-sm newimm"></asp:Label>
                        </div>
                        <div class="clearfix"></div>
                <asp:GridView ID="gvImmigration" runat="server" 
                    CssClass="table table-bordered table-striped dataTable" 
                    AutoGenerateColumns="False" onrowcommand="gvImmigration_RowCommand" OnRowDataBound="gvImmigration_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Doc. Type" DataField="DocType" />
                        <asp:BoundField HeaderText="Doc. No" DataField="DocNo" />
                        <asp:BoundField HeaderText="Issue Date" DataField="IssueDate" DataFormatString="{0:d}"  />
                        <asp:BoundField HeaderText="Expiry Date" DataField="ExpiryDate" DataFormatString="{0:d}"  />
                        <asp:BoundField HeaderText="Eligible Status" DataField="EligibleStatus" />
                        <asp:BoundField HeaderText="Eligible Review Date" DataField="EligibleReviewDate" DataFormatString="{0:d}"  />
                        <asp:BoundField HeaderText="Comments" DataField="Comments" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCnID" runat="server" Text='<%# Eval("IssuedCountryID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblIID" runat="server" Text='<%# Eval("ImmigrationId") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("ImmigrationId") %>'
                                    CommandName="EDITDT" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("ImmigrationId") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Immigration Detail?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
               </asp:GridView>
            </div>              
        </div> <!-- /.box-body -->
        </div><!-- /.box-primary -->
        <div class="box-footer">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateempctrl();" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" Text="Cancel" onclick="btnCancel_Click" />
        </div>
 </section>
 <script type="text/javascript">

     $(function () {


         var hdpost = document.getElementById('MainContent_hdPostBack');

         $('.bankdetdt').slideUp();
         //   $('.jobdetdt').slideUp();
         //    $('.contactdt').slideUp();
         $('.emgcydt').slideUp();
         $('.dependdt').slideUp();
         $('.educationdt').slideUp();
         $('.expncedt').slideUp();
         $('.skillsdt').slideUp();
         $('.langsdt').slideUp();
         $('.immgdt').slideUp();
         if (hdpost.value != "0") {

             var str_array = hdpost.value.split('.');

             for (var i = 0; i < str_array.length; i++) {
                 // Trim the excess whitespace.
                 str_array[i] = str_array[i].replace(/^\s*/, "").replace(/\s*$/, "");
                 // Add additional code here, such as:
                // alert('.' + str_array[i]);
                 if(str_array[i] != "" )
                    $('.' + str_array[i]).slideToggle();
             }
         }
         $('.bankdet').click(function () {
             hdpost.value += '.bankdetdt';
             $('.bankdetdt').slideToggle();
         });

         $('.jobdet').click(function () {
             hdpost.value += '.jobdetdt';
             $('.jobdetdt').slideToggle();
         });

         $('.contact').click(function () {
             hdpost.value += '.contactdt';
             $('.contactdt').slideToggle();
         });

         $('.emgcy').click(function () {
             hdpost.value += '.emgcydt';
             $('.emgcydt').slideToggle();
         });

         $('.depend').click(function () {
             hdpost.value += '.dependdt';
             $('.dependdt').slideToggle();
         });

         $('.education').click(function () {
             hdpost.value += '.educationdt';
             $('.educationdt').slideToggle();
         });

         $('.expnce').click(function () {
             hdpost.value += '.expncedt';
             $('.expncedt').slideToggle();
         });

         $('.skills').click(function () {
             hdpost.value += '.skillsdt';
             $('.skillsdt').slideToggle();
         });

         $('.langs').click(function () {
             hdpost.value += '.langsdt';
             $('.langsdt').slideToggle();
         });

         $('.immg').click(function () {
             hdpost.value += '.immgdt';
             $('.immgdt').slideToggle();
         });

         $('.newstatus').click(function () {
             $('#emplstatus').modal();
         });

         $('.newemg').click(function () {
             $('#emgcontact').modal();
         });

         $('.newdependent').click(function () {
             $('#dvdependent').modal();
         });

         $('.newedu').click(function () {
             $('#dvEducation').modal();
         });

         $('.newexp').click(function () {
             $('#dvExperience').modal();
         });

         $('.newskill').click(function () {
             $('#dvSkills').modal();
         });

         $('.newlang').click(function () {
             $('#dvLanguage').modal();
         });

         $('.newimm').click(function () {
             $('#dvImmigration').modal();
         });

         $('.newedulevel').click(function () {
             $('#dvEduLevel').modal();
         });

     });
        </script>
   
   <%--Employment / Job Type Popup STARTS--%>
   <div class="modal fade" id="emplstatus" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><%= hrmlang.GetString("employmentstatus")%></h4>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("description")%>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="txtType" ValidationGroup="emplstatus" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtType" runat="server" CssClass="form-control" placeholder="Employment Status" ValidationGroup="emplstatus"></asp:TextBox>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveEmplStatus" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="emplstatus" onclick="btnSaveEmplStatus_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Employment / Job Type Popup STARTS--%>

   <%--Education Level Popup STARTS--%>
   <div class="modal fade" id="dvEduLevel" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true" style="z-index:100000;">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H8"><%= hrmlang.GetString("educationlevel")%></h4>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("description")%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLevel" ValidationGroup="edulevel" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtLevel" runat="server" CssClass="form-control" placeholder="Education Level" ValidationGroup="edulevel"></asp:TextBox>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveLevel" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="edulevel" onclick="btnSaveLevel_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Employment / Job Type Popup STARTS--%>

   <%--Emergency Contact Popup STARTS--%>
   <div class="modal fade" id="emgcontact" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1"><%= hrmlang.GetString("addemergencycontact")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="txtEmgName"><%= hrmlang.GetString("name") %><asp:RequiredFieldValidator ID="rfvEmgName" runat="server" ControlToValidate="txtEmgName" ValidationGroup="emgcn" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtEmgName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <label for="txtRelation">
                            <%= hrmlang.GetString("relationship")%></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtRelation" runat="server" CssClass="form-control" placeholder="Relationship"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <label for="txtEmgHPhone">
                            <%= hrmlang.GetString("homephone") %><asp:RequiredFieldValidator ID="rfvEmgHPhone" runat="server" ControlToValidate="txtEmgHPhone" ValidationGroup="emgcn" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtEmgHPhone" runat="server" CssClass="form-control" placeholder="Home Phone"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <label for="txtEmgWPhone">
                            <%= hrmlang.GetString("workphone") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtEmgWPhone" runat="server" CssClass="form-control" placeholder="Work Phone"></asp:TextBox>
                    </div>                
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveEmg" runat="server" CssClass="btn btn-primary" 
                        Text="Add" ValidationGroup="emgcn" onclick="btnSaveEmg_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Emergency Contact Popup ENDS--%>

   <%--Dependents Popup STARTS--%>
   <div class="modal fade" id="dvdependent" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H2"><%= hrmlang.GetString("adddependent")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-3">
                        <label for="txtDepName"><%= hrmlang.GetString("name") %><asp:RequiredFieldValidator ID="rfvDepName" runat="server" ControlToValidate="txtDepName" ValidationGroup="depgroup" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtDepName" runat="server" CssClass="form-control" placeholder="Dependent Name" ValidationGroup="depgroup"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtDepRelation">
                            <%= hrmlang.GetString("relationship") %><asp:RequiredFieldValidator ID="rfvRel" runat="server" ControlToValidate="txtDepRelation" ValidationGroup="depgroup" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtDepRelation" runat="server" CssClass="form-control" placeholder="Relationship"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtDepDob">
                            <%= hrmlang.GetString("dob") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalDepDob" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                    </div>             
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveDep" runat="server" CssClass="btn btn-primary" 
                        Text="Add" ValidationGroup="depgroup" onclick="btnSaveDep_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Dependents Popup ENDS--%>

   <%--Education Popup STARTS--%>
   <div class="modal fade" id="dvEducation" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H3"><%= hrmlang.GetString("addeducationdetails")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="ddlEduLevel"><%= hrmlang.GetString("educationlevel") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlEduLevel" runat="server" CssClass="form-control" DataTextField="EduLevelName" DataValueField="EduLevelID"></asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtUniversity">
                            <%= hrmlang.GetString("university")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtUniversity" runat="server" CssClass="form-control" placeholder="University"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtCollege">
                            <%= hrmlang.GetString("college") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtCollege" runat="server" CssClass="form-control" placeholder="College"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSpec">
                            <%= hrmlang.GetString("specialization")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtSpec" runat="server" CssClass="form-control" placeholder="Specialization"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtStartDate">
                            <%= hrmlang.GetString("startdate") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalStartDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-20" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtEndDate">
                            <%= hrmlang.GetString("enddate") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalEndDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="5"
                            MinYearCountFromNow="-20" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtYear">
                            <%= hrmlang.GetString("yearofpassing")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" placeholder="Year of Passing"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtScore">
                            <%= hrmlang.GetString("scoreperc")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtScore" runat="server" CssClass="form-control" placeholder="Score"></asp:TextBox>
                    </div>             
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveEdu" runat="server" CssClass="btn btn-primary" 
                        Text="Add" onclick="btnSaveEdu_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Education Popup ENDS--%>
       
   <%--Experience Popup STARTS--%>
   <div class="modal fade" id="dvExperience" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H4"><%= hrmlang.GetString("addexperiencedetails")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="txtCompany">Company</label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" placeholder="Company"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtJobTitle">
                            <%= hrmlang.GetString("jobtitle") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" placeholder="University"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtFromDate">
                            <%= hrmlang.GetString("fromdate")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalFromDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-25" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtToDate">
                            <%= hrmlang.GetString("todate")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalToDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-25" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtComments">
                            <%= hrmlang.GetString("comments")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comments"></asp:TextBox>
                    </div>          
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveExp" runat="server" CssClass="btn btn-primary" 
                        Text="Add" onclick="btnSaveExp_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Experience Popup ENDS--%>
   
   <%--Skills Popup STARTS--%>
   <div class="modal fade" id="dvSkills" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H5"><%= hrmlang.GetString("addskill") %></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                       <label for="txtSkill">Description</label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtSkill" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSkillYear">
                            <%= hrmlang.GetString("experienceinyears")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtSkillYear" runat="server" CssClass="form-control" placeholder="Experience in Years"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtSkillComments">
                            <%= hrmlang.GetString("comments") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtSkillComments" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comments"></asp:TextBox>
                    </div>          
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveSkill" runat="server" CssClass="btn btn-primary" 
                        Text="Add" onclick="btnSaveSkill_Click"   />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
   <%--Skills Popup ENDS--%>

   <%--Language Popup STARTS--%>
   <div class="modal fade" id="dvLanguage" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H6"><%= hrmlang.GetString("addlanguage")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                       <label for="txtLang">Language</label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtLang" runat="server" CssClass="form-control" placeholder="Language"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlFluency">
                            <%= hrmlang.GetString("fluency")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlFluency" runat="server" CssClass="form-control">
                            <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                            <asp:ListItem Text="Read,Write" Value="RW"></asp:ListItem>
                            <asp:ListItem Text="Read,Write,Speak" Value="RWS"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlCompetency">
                            <%= hrmlang.GetString("competency")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlCompetency" runat="server" CssClass="form-control">
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
                    <div class="clearfix"></div>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtLangComments" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comments"></asp:TextBox>
                    </div>          
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveLang" runat="server" CssClass="btn btn-primary" 
                        Text="Add" onclick="btnSaveLang_Click"   />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
         </div>
    </div>
   <%--Language Popup ENDS--%>

   <%--Immigration Popup STARTS--%>
   <div class="modal fade" id="dvImmigration" tabindex="-1" AssetType="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H7"><%= hrmlang.GetString("addimmigrationdetails")%></h4>
                </div>
                  <div class="modal-header rowmargin">
                    <div class="col-xs-7">
                        <label for="rbtnDocType"><%= hrmlang.GetString("documenttype")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:RadioButtonList ID="rbtnDocType" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                            <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                            <asp:ListItem Text="Visa" Value="Visa"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtDocNo">
                            <%= hrmlang.GetString("documentno")%></label><asp:RequiredFieldValidator ID="rfvPassport" runat="server" 
                            ControlToValidate="txtDocNo" ErrorMessage="*" ForeColor="Red" ValidationGroup="immggroup"></asp:RequiredFieldValidator></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" placeholder="Doc No"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtIssueDate">
                            <%= hrmlang.GetString("issuedate")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalIssueDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-25" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtExpDate">
                            <%= hrmlang.GetString("expirydate")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalExpDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="20"
                            MinYearCountFromNow="-25" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtElgStatus">
                            <%= hrmlang.GetString("eligiblestatus")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:TextBox ID="txtElgStatus" runat="server" CssClass="form-control" placeholder="Eligible Status"></asp:TextBox>
                    </div>
                    <div class="col-xs-7">
                        <label for="ddlCountry">
                            <%= hrmlang.GetString("issuedby")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <asp:DropDownList ID="ddlIssueCountry" runat="server" CssClass="form-control" DataTextField="CountryName" DataValueField="CountryID"></asp:DropDownList>
                    </div>
                    <div class="col-xs-7">
                        <label for="txtReviewdate">
                            <%= hrmlang.GetString("eligiblereviewdate")%></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-7">
                        <uc2:ctlJoin ID="ctlCalReviewDate" runat="server" DefaultCalendarCulture="Grgorian"
                            MaxYearCountFromNow="20" MinYearCountFromNow="-25" />
                    </div>
                    <div class="col-xs-7">
                        <label for="txtImmgComments">
                            <%= hrmlang.GetString("comments") %></label></div>
                    <div class="clearfix"></div>
                    <div class="col-xs-9">
                        <asp:TextBox ID="txtImmgComments" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comments"></asp:TextBox>
                    </div>          
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveImmg" runat="server" CssClass="btn btn-primary" ValidationGroup="immggroup" 
                        Text="Add" onclick="btnSaveImmg_Click"   />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
       </div>
   </div>
   <%--Immigration Popup ENDS--%>
   <asp:HiddenField ID="hdPostBack" runat="server" Value="0" />
</asp:Content>

