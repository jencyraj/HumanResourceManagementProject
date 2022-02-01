<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddTraining.aspx.cs" Inherits="AddTraining" ValidateRequest="false"  %>
    <%@ Register src="~/UserControls/ctlCalendar.ascx" tagname="ctlCalendar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
 <link href="css/cupertino/jquery-ui-1.7.3.custom.css" rel="stylesheet" type="text/css" />
   <link href="css/fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
   <link href="css/fullcalendar/fullcalendar.print.css" rel="stylesheet" type="text/css" media='print' />

    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>


  <%--  <script src="jquery/jquery-ui-timepicker-addon-0.6.2.min.js" type="text/javascript"></script>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<style type="text/css">
.box .box-body .fc-widget-header{background:none;}
</style>
    <asp:Label ID="lblTrainingID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("addnewtraining") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("addnewtraining") %></li>
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
                    
              <div class="row hrmhide">
                <div class="col-md-3">
                    <div class="form-group">
                        <h3>Employees</h3>                        
                      
                    </div>
                </div>
            </div>
            <div class="col-md-12">
            <div class="col-md-4" style="margin-left:-20px">
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="ddlRole"><%= hrmlang.GetString("trainingemployees") %></label>                        
                        <asp:ListBox ID="ddlEmployees" runat="server" CssClass="form-control" SelectionMode="Multiple" DataTextField="Fullname" Height="100px" DataValueField="EmployeeID"></asp:ListBox>
                    </div>
                </div>
            </div>
            
            <div class="row hrmhide">
                <div class="col-md-8">
                    <div class="form-group">
                        <h3>Training Information</h3>                        
                      
                    </div>
                </div>
            </div>
             <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="ddlRole"><%= hrmlang.GetString("trainingtype") %></label>                        
                        <asp:DropDownList ID="ddltrainingtype" runat="server" CssClass="form-control" DataTextField="Description" DataValueField="Tid"></asp:DropDownList>
                    </div>
                </div></div>
           
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtsubject">
                        <%= hrmlang.GetString("trainingsubject") %></label>
                        
                        <asp:TextBox ID="txtsubject" runat="server"  CssClass="form-control"
                         Width="250px"
                         placeholder="Enter Subject"></asp:TextBox>
                    </div>
                </div>
          </div>
              <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="ddlnatureoftraining">
                        <%= hrmlang.GetString("trainingnature") %></label>                        
                        <asp:DropDownList ID="ddlnatureoftraining" runat="server" 
                            CssClass="form-control" 
                            onselectedindexchanged="ddlnatureoftraining_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                 
            </div>   
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtlocation">
                        <%= hrmlang.GetString("traininglocation") %></label>
                      
                        <asp:TextBox ID="txtlocation" runat="server"  CssClass="form-control"
                         Width="250px"
                         placeholder="Enter Location" Visible="false"></asp:TextBox>
                         <asp:DropDownList ID="ddltrainingloc" runat="server" CssClass="form-control trainingloc" 
                            DataValueField="TrainingLID" DataTextField="TrainingLocationName" 
                            AutoPostBack="true" onselectedindexchanged="ddltrainingloc_SelectedIndexChanged"  
                           ></asp:DropDownList>
                          <%--  onselectedindexchanged="ddltrainingloc_SelectedIndexChanged"--%>
                    </div>
                </div>
          </div>         
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txttitle">
                        <%= hrmlang.GetString("trainingtitle") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttitle"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txttitle" runat="server"  CssClass="form-control validate"
                         Width="250px"
                         placeholder="Enter Title"></asp:TextBox>
                    </div>
                </div>
          </div>
              <div class="row hrmhide">
                <div class="col-md-8">
                    <div class="form-group">
                        <h3>Training Details</h3>                        
                      
                    </div>
                </div>
            </div>
              <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txttrainer">
                        <%= hrmlang.GetString("trainer") %></label>
                      
                        <asp:TextBox ID="txttrainer" runat="server"  CssClass="form-control"
                         Width="250px"
                         placeholder="Enter Trainer"></asp:TextBox>
                    </div>
                </div>
          </div>
           
             <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtsponsoredby">
                        <%= hrmlang.GetString("trainingsponsored") %></label>
                      
                        <asp:TextBox ID="txtsponsoredby" runat="server"  CssClass="form-control"
                         Width="250px"
                         placeholder="Enter Sponsored By" ></asp:TextBox>
                    

                    </div>
                </div>
                
                  
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtorganizedby">
                        <%= hrmlang.GetString("trainingorganized") %></label>                      
                        <asp:TextBox ID="txtorganizedby" runat="server"  CssClass="form-control"
                         Width="250px"
                         placeholder="Enter Organized By"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="Dtfrom">
                        <%= hrmlang.GetString("trainingfrom") %></label>
                                           
                         <uc1:ctlCalendar ID="Dtfrom" runat="server" DefaultCalendarCulture="Grgorian"   />
                    </div>
                </div>
          </div>
          <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="Dtto">
                        <%= hrmlang.GetString("trainingto") %></label>    
                                       
                       <uc1:ctlCalendar ID="Dtto" runat="server" DefaultCalendarCulture="Grgorian"   />
                    </div>
                </div>
          </div>
           </div>
            <div class="col-md-8">
                        <%--<div id="trainingcalendar">
                        </div>OnItemDataBound="dlCalendar_ItemDataBound"--%>
                        <asp:DataList ID="dlCalendar" runat="server" RepeatColumns="7" 
                            BorderStyle="None" RepeatDirection="Horizontal" 
                            onitemdatabound="dlCalendar_ItemDataBound" >

<HeaderTemplate>
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color:rgba(60, 141, 188, 0.76);height:20px"><tr><td width="33%"><asp:DropDownList runat="server" ID="cboPrev" AutoPostBack="true" Width="70px" OnSelectedIndexChanged="RfreshData"></asp:DropDownList>
<asp:Label runat="server" ID="lblLeft" ></asp:Label>
</td><td width="33%" align="center"><asp:Label runat="server" ID="lblMiddle" >
</asp:Label></td><td width="33%" align="right">
<asp:Label runat="server" ID="lblRight" ></asp:Label>
<asp:DropDownList runat="server" ID="cboNext" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="RfreshData"></asp:DropDownList></td></tr>
</table>
</HeaderTemplate>

<ItemTemplate><table border="0" style="background-color:Silver;height:100px" width="100px" cellpadding="0" cellspacing="0" ><tr><td align="left" valign="top"">
<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Data") %>' Width="100%"></asp:Label>
</td></tr></table>
</ItemTemplate>

</asp:DataList>
            </div>
            <div class="row">
                <div class="col-md-10">
                    <div class="form-group">
                        <label for="txtDesc">
                        <%= hrmlang.GetString("description") %></label>
                       
                      <asp:TextBox ID="txtDesc" CssClass="editor1 form-control" runat="server" TextMode="MultiLine" style="height:200px; width:600px;"></asp:TextBox>
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
         
            </div>
            <div class="clearfix"></div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
      <div class="modal fade" id="emgcontact" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H1">
                        <%= hrmlang.GetString("addemergencycontact")%></h4>
                </div>
                <div class="modal-header">
              <label><%= hrmlang.GetString("trainingtitle") %></label>
                     <div class="eventtitle"></div>
                      <div class="clearfix">
                    </div>
                <label><%= hrmlang.GetString("description")%></label>
                     <div class="eventdata"></div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript">
         $(function () {
             $(".editor1").wysihtml5();
             $(".editor2").wysihtml5();
         });

         function showdata(sender) {
             //alert($(sender).find('.ptext').text());
             $('.eventtitle').html($(sender).find('.ptitle').text());
             $('.eventdata').html($(sender).find('.ptext').text());
            $('#emgcontact').modal();
     }
    </script>
   
   
</asp:Content>
