<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddWorkWeek.aspx.cs" Inherits="WorkWeek" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        <section class="content-header">
    <h1>Work Week<small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
         <li class="active"><%= hrmlang.GetString("addweek")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <div class="box box-primary">
       <%-- <div class="box-header">
            <h3 class="box-title">
                Company Profile</h3>
        </div>--%>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>
           
          <div class="row">
                          <div class="col-md-3">
                            <div class="form-group">
                            <label for="txtbrn">
                                <%= hrmlang.GetString("branch")%></label>
                            <asp:DropDownList ID="ddlbrnch" runat="server" CssClass="form-control" DataTextField="Branch"
                                DataValueField="BranchID" AutoPostBack="True" 
                                    onselectedindexchanged="ddlbrnch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        </div>
                        </div>
             <div class="row">
                          <div class="col-md-3">
                            <div class="form-group">
                            <label for="txtdept">
                                <%= hrmlang.GetString("department")%></label>
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" DataTextField="DEPARTMENTNAME"
                                DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </div>
                        </div>
                        </div>
            <div class="row">
             <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtDesignation">
                                    <%= hrmlang.GetString("designation")%></label>
                                <asp:Label ID="lblDsgnReq" runat="server" CssClass="text-red" />
                                <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                <p class="help-block" style="font-size: 10px; color: red;display:none;">
                                   </p>
                            </div>
                        </div>
                        </div>
                        <div class="row">
                                     <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtEmployee">
                                    <%= hrmlang.GetString("employee")%></label>
                                <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"></asp:TextBox>
                                <p class="help-block" style="font-size: 10px; color: red;display:none;">
                                    </p>
                            </div>
                        </div>
                         </div>
            </div>
            <div class="col-mg-12 rowmargin">
          
              <div class="col-xs-4">
                       
                <asp:GridView ID="gvWork" runat="Server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped dataTable" 
                    onrowdatabound="gvWork_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="Monday">
                            <ItemTemplate>

                             
                                <asp:DropDownList ID="ddlMonday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tuesday">
                            <ItemTemplate>
                          
                                <asp:DropDownList ID="ddlTuesday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Wednesday">
                            <ItemTemplate>
                           
                                <asp:DropDownList ID="ddlWednesday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thursday">
                            <ItemTemplate>
                            
                                <asp:DropDownList ID="ddlThursday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Friday">
                      
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlFriday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saturday">
                            <ItemTemplate>
                           
                                <asp:DropDownList ID="ddlSaturday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sunday">
                            <ItemTemplate>
                         
                                <asp:DropDownList ID="ddlSunday" runat="server">
                                    <asp:ListItem Text="Full Day" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Half Day" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Off Day" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
                </div>
            
            
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
          <div  class="pull-right rowmargin">
            <div class="modal-footer">
           
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"/>
                      <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
                  </div>
                
            
            </div>  
        </div>
         
            
            
    </div>
    </section><!-- /.content -->
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
 <style type="text/css">
    .ui-widget {font-size:0.9em !important}
 </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=txtDesignation.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetDesignations") %>',
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
                    $("#<%=hfDesignationId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });

            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var DesignationId = $("#<%=hfDesignationId.ClientID%>").val();
                    var BranchID = $("#<%=ddlbrnch.ClientID%>").val();
                    var DeptID = $("#<%=ddlDept.ClientID%>").val();                   
                        $.ajax({
                            url: '<%=ResolveUrl("~/ajaxservice.asmx/Employeesearch") %>',
                            data: "{'prefix': '" + request.term + "', 'DesignationId': '" + DesignationId + "','BranchID': '" + BranchID + "','DeptID': '" + DeptID + "'}",
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
     <asp:HiddenField ID="hfDesignationId" runat="server" />
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>
