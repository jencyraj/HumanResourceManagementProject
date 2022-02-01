<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddEditAttendance.aspx.cs" Inherits="AddEditAttendance" %>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="css/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <script src="js/plugins/timepicker/bootstrap-timepicker.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1><%= hrmlang.GetString("addnewattendance")%><small></small></h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home")%></a></li>
                <li class="active"><%= hrmlang.GetString("addnewattendance")%></li>
            </ol>
        </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="clearfix"></div>
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                
                <div class="row" style="padding-left:15px" id="dvEmp" runat="server">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtEmployee"><%= hrmlang.GetString("employee")%></label><br />
                            <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control txtround"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddAttendanceType"><%= hrmlang.GetString("attendancetype")%></label><br />
                            <asp:DropDownList ID="ddAttendanceType" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ctlCalendarAD"><%= hrmlang.GetString("attendancedate")%></label>                      
                            <uc1:ctlcalendar ID="ctlCalendarAD" runat="server" DefaultCalendarCulture="Grgorian" 
                                MaxYearCountFromNow="0" MinYearCountFromNow="-80" />
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <label><%= hrmlang.GetString("signintime")%></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSignInTime" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <label><%= hrmlang.GetString("signouttime")%></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSignOutTime" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                            <div class="form-group">
                                 <label for="ddlBranch">
                                <%= hrmlang.GetString("workshift")%></label><asp:CompareValidator ID="cmp0" runat="server"
                                    ControlToValidate="ddlshift" Operator="NotEqual" Type="String" ValueToCompare="0"
                                    ValidationGroup="maingrp" ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator>
                            <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" ValidationGroup="maingrp"
                                DataTextField="WorkShiftName" DataValueField="WSID" AutoPostBack="true" >
                            </asp:DropDownList>
			                </div>
		                
	                </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label for="txtAdditionalInfo"><%= hrmlang.GetString("additionalinfo")%></label>                        
                            <asp:TextBox ID="txtAdditionalInfo" CssClass="form-control" runat="server" TextMode="MultiLine" style="height:200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="colwidth">
                        <div class="form-group">
                            <div class="col-md-12 rowmargin">
                        <label id="lblBreaks" runat="server" for="gvAllowances"><%= hrmlang.GetString("breaks")%></label>  
                            <asp:GridView ID="gvBreak" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        ShowHeaderWhenEmpty="true" ShowFooter="true" DataKeyNames="BreakId"
                        OnRowEditing="gvBreak_RowEditing" OnRowCancelingEdit="gvBreak_RowCancelingEdit" OnRowUpdating="gvBreak_RowUpdating" 
                        OnRowDeleting="gvBreak_RowDeleting" OnRowDataBound="gvBreak_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfBreakId" runat="server" Value='<%# Eval("BreakId") %>' />
                                    <asp:Label ID="lblBStartTime" runat="server" Text='<%# Eval("StartTime") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hfEBreakId" runat="server" Value='<%# Eval("BreakId") %>' />
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtBStartTime" runat="server" CssClass="form-control timepicker" Text='<%# Eval("StartTime") %>'></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtBNewStartTime" placeholder="Enter Start Time" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblBEndTime" runat="server" Text='<%# Eval("EndTime") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtBEndTime" runat="server" CssClass="form-control timepicker" Text='<%# Eval("EndTime") %>'></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtBNewEndTime" placeholder="Enter End Time" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblBDescription" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBDescription" runat="server" CssClass="form-control" Text='<%# Eval("Description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div style="margin:auto;float:left">
                                        <asp:TextBox ID="txtBNewDescription" placeholder="Enter Description" Width="400px" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div style="margin:auto;float:right">
                                        <asp:LinkButton ID="btnAddNewBreak" runat="server" Text="Add Break" OnClick="btnAddNewBreak_Click" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>   
                </div>
                        </div>
                    </div>
                 </div>
                    
                        
                <div class="row">
                    <div class="colwidth">
                        <div class="form-group">
                            <div class="col-md-12 rowmargin">
                            <label id="lblOvertimes" runat="server" for="gvAllowances"><%= hrmlang.GetString("overtimes")%></label>  
                            <asp:GridView ID="gvOvertime" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        ShowHeaderWhenEmpty="true" ShowFooter="true" DataKeyNames="OvertimeId"
                        OnRowEditing="gvOvertime_RowEditing" OnRowCancelingEdit="gvOvertime_RowCancelingEdit" OnRowUpdating="gvOvertime_RowUpdating" 
                        OnRowDeleting="gvOvertime_RowDeleting" OnRowDataBound="gvOvertime_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfOvertimeId" runat="server" Value='<%# Eval("OvertimeId") %>' />
                                    <asp:Label ID="lblOStartTime" runat="server" Text='<%# Eval("StartTime") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hfEOvertimeId" runat="server" Value='<%# Eval("OvertimeId") %>' />
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtOStartTime" runat="server" CssClass="form-control timepicker" Text='<%# Eval("StartTime") %>'></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtONewStartTime" placeholder="Enter Start Time" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblOEndTime" runat="server" Text='<%# Eval("EndTime") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtOEndTime" runat="server" CssClass="form-control timepicker" Text='<%# Eval("EndTime") %>'></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div class="bootstrap-timepicker">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtONewEndTime" placeholder="Enter End Time" CssClass="timepicker form-control" runat="server"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
			                            </div>
		                            </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblODescription" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtODescription" runat="server" CssClass="form-control" Text='<%# Eval("Description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <div style="margin:auto;float:left">
                                        <asp:TextBox ID="txtONewDescription" placeholder="Enter Description" Width="400px" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div style="margin:auto;float:right">
                                        <asp:LinkButton ID="btnAddNewOvertime" runat="server" Text="Add Overtime" OnClick="btnAddNewOvertime_Click" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>   
                </div>            
                        </div>
                    </div>
                </div>
                    
                        
                
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="dvConfirmBreak" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H7"><%= hrmlang.GetString("deletebreak")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7" style="width:100% !important">
                        <label><%= hrmlang.GetString("confirmdeletebreak")%></label>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnDeleteBreak" runat="server" CssClass="btn btn-primary" OnClientClick="return Delete();" />
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= hrmlang.GetString("cancel") %></button>
                </div>
            </div>
       </div>
   </div>
   <div class="modal fade" id="divConfirmOvertime" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1"><%= hrmlang.GetString("deleteovertime")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7" style="width:100% !important">
                        <label><%= hrmlang.GetString("confirmdeleteovertime")%></label>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnDeleteOvertime" runat="server" CssClass="btn btn-primary" OnClientClick="return Delete();" />
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= hrmlang.GetString("cancel") %></button>
                </div>
            </div>
       </div>
   </div>
    <script type="text/javascript">
        $(function () {
            $(".timepicker").timepicker({
                showInputs: false
            });
        });

        function ConfirmBreak() {
            $('#dvConfirmBreak').modal();
        }

        function ConfirmOvertime() {
            $('#divConfirmOvertime').modal();
        }

        function Delete() {
            <%= Page.ClientScript.GetPostBackEventReference(lnkPostBack, "")%>
            return false;
        }
    </script>
    <asp:HiddenField ID="hfBreaks" runat="server" />
    <asp:HiddenField ID="hfOvertimes" runat="server" />
    <asp:HiddenField ID="hdnDelete" runat="server" />
    <asp:LinkButton ID="lnkPostBack" runat="server" OnClick="lnkPostBack_Click" />
    
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
 <style type="text/css">
    .ui-widget {font-size:0.9em !important}
 </style>
 <script type="text/javascript">

     $(document).ready(function () {
         $("#<%=txtEmployee.ClientID %>").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployees") %>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[0],
                                 val: item.split('-')[1]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("#<%=hfEmployeeId.ClientID %>").val(i.item.val);
             },
             minLength: 1
         });
     }); 
</script>
<asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>

