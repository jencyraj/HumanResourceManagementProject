<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OverTimeRule.aspx.cs" Inherits="overtimeview" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<section class="content-header">
    <h1><%= hrmlang.GetString("overtimeview")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("overtimeview")%></li>
    </ol>
</section>
  <section class="content">
   <div class="box box-primary">
       <!-- /.box-header -->
       <div class="box-body">
           <asp:Panel ID="pnllod" runat="server" Visible="false">
               <div class="col-xs-12" runat="server" id="grd">
               </div>
           </asp:Panel>
           <asp:Panel ID="Pnlnew" runat="server" Visible="true">
               <div class="pull-left dblock rowmargin">
                   <p class="text-red">
                       <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                   <p class="text-green">
                       <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
               </div>
               <div class="clearfix">
               </div>
                 
               <div class="row rowmargin">
                   
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">

                    <div class="form-group">
                            <label for="txtValue"><%= hrmlang.GetString("minhr")%></label><asp:CompareValidator ID="CMP1" runat="server" ControlToValidate="txtminhr"
                                Operator="DataTypeCheck" Type="Double" ErrorMessage="Invalid Number" CssClass="text-red"></asp:CompareValidator>                   
                            <asp:TextBox ID="txtminhr" runat="server" CssClass="form-control" placeholder="Enter Min Hr:"></asp:TextBox>
                        </div>
                           </div>
               </div>
                  <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="lbyear">
                           <%= hrmlang.GetString("applicablesum")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4"  id="ctlCalDepDoeeb">
                      
                        <uc2:ctlJoin ID="ctlCalDepDob" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder=" Scheduled time"></asp:TextBox>
                     </div>
               </div>
               
                <div class="row rowmargin">
           <div class="col-xs-4">
               <label for="lbactive">
                   <%= hrmlang.GetString("ruleapplicable")%></label></div>
           <div class="clearfix">
           </div>
           <div class="col-xs-4">
               <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
           </div>
       </div>    
               </div>
            
           
       </div>
       
    
       <asp:Label ID="lblIndexE" runat="server" Visible="false"></asp:Label>
       <div class="clearfix">
       </div>
       <div class="box-footer">
          <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"  OnClick="btnSave_Click" />

       <%--   OnClick="btnSave_Click"
               OnClientClick="return validatectrl();"--%>
           <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"  OnClick="btnCancel_Click"
                />
               <%-- OnClick="btnCancel_Click"--%>
       </div>
       
         </asp:Panel>
         
   </div>
        <!-- /.box-body -->
    </div>
    </section>
   
</asp:Content>

