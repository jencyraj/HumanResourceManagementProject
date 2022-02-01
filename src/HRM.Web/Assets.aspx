<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Assets.aspx.cs" Inherits="Assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblAssetID" runat="server" Visible="false"></asp:Label>   
    <asp:Label ID="lblEmp" runat="server" Visible="false"></asp:Label>    
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("manageassets")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageassets")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">            
             <strong><%= hrmlang.GetString("branch")%>:</asp:Label> </strong><asp:DropDownList ID="ddlBranch" runat="server" 
                  DataTextField="Branch" DataValueField="BranchID" ></asp:DropDownList>&nbsp;
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" 
                    Text="Search" CausesValidation="false" onclick="btnSearch_Click"/>&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" 
                    Text="New Asset" CausesValidation="false" onclick="btnNew_Click" />
            </div>
            <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvAsset" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvAsset_RowCommand" DataKeyNames="AssetID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvAsset_PageIndexChanging" 
                    onrowdatabound="gvAsset_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Branch" HeaderText="Branch" />
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Type" />
                        <asp:BoundField DataField="AssetCode" HeaderText="Asset Code" />
                        <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />
                        <asp:TemplateField HeaderText="Assigned To">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server"></asp:Label> 
                                <asp:Label ID="lblEID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkTransfer" runat="server" CssClass="glyphicon glyphicon-transfer" data-toggle="tooltip" title="Transfer"
                                 CommandArgument='<%# Eval("AssetID") + "|" + Eval("BranchID") %>' CommandName="TRANSFER" ></asp:LinkButton>
                                <asp:LinkButton ID="lnkAssign" runat="server" CssClass="glyphicon glyphicon-user" data-toggle="tooltip" title="Assign" 
                                 CommandArgument='<%# Eval("AssetID") + "|" + Eval("BranchID") %>' CommandName="ASSIGN"></asp:LinkButton>
                                   <asp:LinkButton ID="lnkreturn" runat="server" CssClass="glyphicon glyphicon-check" data-toggle="tooltip" title="Return"
                                 CommandArgument='<%# Eval("AssetID") + "|" + Eval("BranchID") %>' CommandName="Return" OnClientClick="return confirm('Are you sure to return this Asset?')" ></asp:LinkButton>
                                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="fa fa-edit"
                                 NavigateUrl='<%# "~/AddAsset.aspx?id=" + Eval("AssetID") %>' data-toggle="tooltip" title="Edit"></asp:HyperLink>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("AssetID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Asset?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>       
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>

   <%--Asset Transfer Popup STARTS--%>
   <div class="modal fade" id="dvTransfer" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><%= hrmlang.GetString("assettransfer")%></h4>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("assetname")%> : </label>&nbsp; 
                    <asp:Label ID="lblAsset" runat="server" ></asp:Label>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("branch")%><span style="margin-left:30px">: </span></label><asp:Label ID="lblCurrBranch" runat="server"  style="margin-left:8px" ></asp:Label>
                    <asp:Label ID="lblCurrBranchID" runat="server" style="display:none"  ></asp:Label>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("transferto")%></label>
                </div>
                <div class="col-xs-8">
                    <asp:DropDownList ID="ddlNewBranch" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID"></asp:DropDownList>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-primary" 
                        ValidationGroup="emplstatus" onclick="btnTransfer_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= hrmlang.GetString("close")%></button>
                </div>
            </div>
       </div>
   </div>
   <!--Asset Transfer Popup STARTS-->

   <!--Asset Assignment Popup STARTS-->
   <div class="modal fade" id="dvAssign" tabindex="-1" Deduction="dialog" aria-labelledby="basicModal" aria-hidden="true">
       <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1"><%= hrmlang.GetString("assignasset")%></h4>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("assetname")%> : </label>&nbsp; 
                    <asp:Label ID="lblAssignAsset" runat="server" ></asp:Label>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("branch")%><span style="margin-left:30px">: </span></label><asp:Label ID="lblCurBranch" runat="server"  style="margin-left:8px" ></asp:Label>
                 <asp:Label ID="lblCurrBrnchID" runat="server" style="display:none"></asp:Label>
                </div>
                <div class="col-xs-8" id="dvAssigned" runat="server" visible="false">
                    <label><%= hrmlang.GetString("alreadyassignedto")%></label><asp:Label ID="lblAssignTo" runat="server"  style="margin-left:8px" ></asp:Label>
                </div>
                <div class="col-xs-8">
                    <label><%= hrmlang.GetString("assignto")%></label>
                </div>
                <div class="col-xs-8">                    
                    <asp:TextBox ID="txtAssignTo" runat="server" CssClass="form-control assignto"></asp:TextBox><br />
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-primary" 
                        OnClick="btnAssign_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= hrmlang.GetString("close")%></button>
                </div>
            </div>
       </div>
   </div>
   <!--Asset Assignment Popup STARTS-->
   <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
 <style type="text/css">
    .ui-widget {font-size:0.9em !important}
    .ui-autocomplete {z-index:10000 !important}
 </style>
 <%--   <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtAssignTo.ClientID %>").autocomplete({
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
</script>--%>
<script type="text/javascript">

    $(document).ready(function () {

        $("#<%=txtAssignTo.ClientID %>").autocomplete({
            source: function (request, response) {

                var BranchId = document.getElementById('<%= lblCurrBrnchID.ClientID %>').innerHTML;
               0
                $.ajax({

                    url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByBranchID") %>',
                    data: "{ 'prefix': '" + request.term + "', 'BranchID': '" + BranchId + "'}",
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

    function cleartxt() {
        var txtemp = $("#<%=txtAssignTo.ClientID %>").val();
        if ("" + txtemp == "")
            $("#<%=hfEmployeeId.ClientID %>").val("");
    }
    </script>
<asp:HiddenField ID="hfEmployeeId" runat="server" />
</asp:Content>
