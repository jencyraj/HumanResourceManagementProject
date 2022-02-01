<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddResignation.aspx.cs" Inherits="AddResignation" ValidateRequest="false"  %>
    <%@ Register src="~/UserControls/ctlCalendar.ascx" tagname="ctlCalendar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/Parser Error.htm" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblResignationID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("addnewresignation") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("addnewresignation") %></li>
    </ol>
</section>
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
                        <label for="Dtfrom">
                        <%= hrmlang.GetString("NoticeDate") %></label>
                         <uc1:ctlCalendar ID="Dtnotice" runat="server" DefaultCalendarCulture="Grgorian"   />
                    </div>
                </div>
          </div>
          <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="Dtto">
                        <%= hrmlang.GetString("ResgnDate")%></label>   
                       <uc1:ctlCalendar ID="Dtresignation" runat="server" DefaultCalendarCulture="Grgorian"   />
                    </div>
                </div>
          </div>
            <div class="row">
                <div class="col-md-10">
                    <div class="form-group">
                        <label for="txtDesc">
                        <%= hrmlang.GetString("reason") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesc"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="txtDesc" CssClass="editor2 form-control" runat="server" TextMode="MultiLine" style="height:200px; width:600px;"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-10">
                    <div class="form-group">
                        <label for="txtadditionalinfo">
                        <%= hrmlang.GetString("additionalinformation") %></label>
                       
                      <asp:TextBox ID="txtadditionalinfo" CssClass="editor2 form-control" runat="server" TextMode="MultiLine" style="height:200px; width:600px;"></asp:TextBox>
                    </div>
                </div>
          </div>
            <div class="box-footer">
            <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn btn-primary" Text="Save as Draft" OnClick="btnSaveDraft_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
    
</asp:Content>
