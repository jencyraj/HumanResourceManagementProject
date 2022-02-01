<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewAllResignation.aspx.cs" Inherits="ViewAllResignation" %>
        <%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlInterviewDate" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<script type="text/javascript">
    $('body').ajaxStart(function () {
        $('#spinner').show();
    });

    $('body').ajaxComplete(function () {
        $('#spinner').hide();
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnSearch').click(function () {
            //$("#spinner").append('<img id="img-spinner" src="Images/ajax-loader-test.gif" alt="Loading.." style="position: absolute; z-index: 200; left:50%; top:50%; " />');
            $('#spinner').show().fadeIn(20);
        });
    });
</script>
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("resignationapproval")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("resignation")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
<div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="col-md-12 rowmargin">
            
             <div class="col-md-4">
                        <strong> <%= hrmlang.GetString("employee")%></strong>
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-4">                     
                           <strong> <%= hrmlang.GetString("branches") %></strong>
                          <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" 
                            DataTextField="Branch" DataValueField="BranchID" AutoPostBack="True" >
                        </asp:DropDownList>
                    </div>
                     
                    <div class="col-xs-3">
                        <strong><%= hrmlang.GetString("approvalstatus") %></strong>
                        <asp:DropDownList ID="ddlStatus" runat="server"  AutoPostBack="true"  CssClass="form-control" >
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="P" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div class="col-xs-1">
                    <strong></strong>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                            OnClick="btnSearch_Click"  ClientIDMode="Static"/>
                    </div>
           
           
                     
          
            </div>  
                   <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
            
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin"> 
                <asp:GridView ID="gvResignation" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvResignation_RowCommand" DataKeyNames="Resgnid" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvResignation_PageIndexChanging" 
                    onrowdatabound="gvResignation_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName"/>
                        <asp:BoundField DataField="NoticeDate" HeaderText="Notice Date"  DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ResgnDate" HeaderText="Resignation Date"  DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Reason" HeaderText="Reason"/>
                        <asp:TemplateField>
                        <HeaderTemplate>Status</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Approved") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblResignation" runat="server" Text='<%# Eval("Resgnid") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Eval("EmployeeID") %>' Visible="false"></asp:Label>
                                  <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("Resgnid") %>' CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("Resgnid") %>' CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"></asp:LinkButton>
                                 <asp:LinkButton ID="lnkApproval" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-up" CommandArgument='<%# Eval("Resgnid") %>' CommandName="Y" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkRejected" runat="server"  data-toggle="tooltip" CssClass="fa fa-thumbs-down" CommandArgument='<%# Eval("Resgnid") %>' CommandName="N" OnClientClick="return confirm('Are you sure to Reject?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>

             <%--Resignation Popup STARTS--%>
  <%--  <div class="modal fade" id="dvApprove" tabindex="-1" role="dialog" aria-labelledby="basicModal"  aria-hidden="true" style="z-index: 100000;">
        <div class="modal-dialog" style="width:960px">
            <div class="modal-content">--%>
               <%-- <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H8">
                         <label for="rbtnAuth">
                                <%= hrmlang.GetString("exitinterview")%></label></h4>
                </div>--%>
                 <div class="clearfix"></div>
                       <asp:Panel ID="pnlApprove" runat="server" Visible="false">
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="rbtnAuth">
                                <%= hrmlang.GetString("exitinterview")%></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="rbtnIntrv" runat="server" CssClass="form-control validate" AutoPostBack="true" OnSelectedIndexChanged ="rbtnIntrv_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Panel ID="pnlIntrv" runat="server" Visible="false">
                        <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCode">
                                <%= hrmlang.GetString("employeename") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                        <asp:TextBox ID="txtexitEmployee" runat="server" CssClass="form-control" placeholder="" style="display:inline;margin-right:15px;" width="250px"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtInterviewDate">
                                <%= hrmlang.GetString("interviewdate")%></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                       <uc1:ctlInterviewDate ID="ctlInterviewDate" runat="server" DefaultCalendarCulture="Grgorian" MaxYearCountFromNow="0"
                            MinYearCountFromNow="-50" />
                        <asp:TextBox ID="txtInterviewDate" runat="server" CssClass="form-control dtjoin hrmhide"
                            placeholder="Interview Date"></asp:TextBox>
                        </div>
                    </div>
                    </asp:Panel>                    
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtReason">
                                    <%= hrmlang.GetString("description")%></label>
                                <asp:TextBox ID="txtApprReason" CssClass="editor1 form-control" runat="server"
                                    TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </asp:Panel>
                     <asp:Panel ID="pnlDeny" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtReason">
                                    <%= hrmlang.GetString("description")%></label>
                                <asp:TextBox ID="txtDnyReason" CssClass="editor1 form-control" runat="server"
                                    TextMode="MultiLine" Style="height: 200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </asp:Panel>
                 <asp:Panel ID="pnlButtons" runat="server" Visible="false">
                   <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return validatectrl();" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                 </asp:Panel>
            <asp:HiddenField ID="hfEmployeeId" runat="server" />
            <asp:HiddenField ID="hfexitEmployeeId" runat="server" />
                <asp:Label ID="lblTID" runat="server" Visible="false"></asp:Label>
            <div class="box-footer">
               
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
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





            //Exit Interviewer

            $("#<%=txtexitEmployee.ClientID %>").autocomplete({
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
                    alert(i.item.val);
                    $("#<%=hfexitEmployeeId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });

        });

           
    </script>
</asp:Content>
