<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportingOfficers.aspx.cs" Inherits="ReportingOfficers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <section class="content-header">
    <h1><%= hrmlang.GetString("reportingofficers")%><small> of <asp:Label ID="lblEmp" runat="server"></asp:Label></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("reportto") %></li>
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
                         
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <label for="txtReportTo"><%= hrmlang.GetString("reportto") %></label><asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="txtReportTo" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtReportTo" runat="server" CssClass="form-control" placeholder="Report To" ></asp:TextBox>
                </div>
            </div>   
            <div class="row rowmargin">
                <div class="col-xs-3">
                    <asp:CheckBox ID="chkImmediate" runat="server" Text="Immediate Manager" />
                </div>
                <div class="col-xs-3">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Add" 
                        onclick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            </div>   
            <div class="clearfix"></div>
            <div class="col-mg-12 rowmargin">
                <asp:Label ID="lblSupMsg" runat="server" ForeColor="Green"></asp:Label>
                <asp:GridView ID="gvSuperiors" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                   DataKeyNames="ReportID" EnableViewState="True" 
                    onrowcommand="gvSuperiors_RowCommand" OnRowDataBound="gvSuperiors_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Report To">
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Immediate Manager">
                            <ItemTemplate>
                                <asp:Label ID="lblImmed" runat="server" Text='<%# ("" + Eval("ImmediateSuperior") == "N") ? "No" : "Yes" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("SuperiorID") %>'
                                    CommandName="EDITDT" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("SuperiorID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this record?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <div class="col-mg-12 rowmargin">
                <h4></h4>
                <asp:Label ID="lblSubMsg" runat="server" ForeColor="Green"></asp:Label>
                <asp:GridView ID="gvSub" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                   DataKeyNames="ReportID" EnableViewState="True" OnRowDataBound="gvSub_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Subordinates">
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <div class="clearfix"></div>
            <div class="box-footer">
                
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
 <style type="text/css">
    .ui-widget {font-size:0.9em !important}
 </style>
 <script type="text/javascript">

     $(document).ready(function () {
         $("#<%=txtReportTo.ClientID %>").autocomplete({
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

