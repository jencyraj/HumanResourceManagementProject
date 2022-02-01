<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Notification" %>
<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlJoin" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
<section class="content-header">
    <h1><%= hrmlang.GetString("notifications")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("notifications")%></li>
    </ol>
</section>
 <section class="content">
   <div class="box box-primary">
       <!-- /.box-header -->
       <div class="box-body">

       <div class="clearfix">
               </div>
                 
               <div class="row rowmargin">
                   
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">

                    <div class="form-group">
                                 
                            <asp:TextBox ID="txtnotify" runat="server" CssClass="form-control" placeholder="Enter Notification:" TextMode="MultiLine"></asp:TextBox>
                        </div>
                           </div>
               </div>
                <div class="clearfix">
               </div>
                 
               <div class="row rowmargin">
                   
                   
                     
                   <div class="col-xs-4">
                   <label for="txtValue"><%= hrmlang.GetString("fromdate")%></label>
                     <div class="clearfix">
                   </div>
                    <div class="form-group">
                                
                           <div class="col-xs-7"  id="ctlCalDepDoeeb">
                        <uc2:ctlJoin ID="ctlCalDepDob" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="txtstatus" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder="Tnterview Scheduled time"></asp:TextBox>
                    </div>     
                                 
                                 </div>
                           </div>
               </div>


                <div class="clearfix">
               </div>
                 
               <div class="row rowmargin">
                   
                   
                     
                   <div class="col-xs-4">
                   <label for="txtValue"><%= hrmlang.GetString("todate")%></label>
                     <div class="clearfix">
                   </div>
                    <div class="form-group">
                                
                           <div class="col-xs-7"  id="CtlJoin1todate">
                        <uc2:ctlJoin ID="CtlJoin1" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-80" />
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control dtdepdob hrmhide"
                            placeholder="Tnterview Scheduled time"></asp:TextBox>
                    </div>     
                                 
                                 </div>
                           </div>
               </div>
                 <div class="clearfix">
       </div>
       <div class="box-footer">
          <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />

       <%--   OnClick="btnSave_Click"
               OnClientClick="return validatectrl();"--%>
           <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click"
                />
               <%-- OnClick="btnCancel_Click"--%>
       </div>
       </div>
       </div>
       </section>
</asp:Content>

