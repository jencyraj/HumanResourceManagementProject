<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ApproveTermination.aspx.cs" Inherits="ApproveTermination" ValidateRequest="false"%>

<%@ Register Src="~/UserControls/ctlCalendar.ascx" TagName="ctlInterviewDate" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <script type="text/javascript">
        $(function () {
            $(".editor1").wysihtml5();
        });
    </script>

    <asp:Label ID="lblTID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("employeetermination")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageemployeetermination")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <panel class="box-body">
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                </asp:Panel>
                <div class="col-mg-12 rowmargin rowpadleft">
                    <asp:GridView ID="gvTermination" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvTermination_RowCommand" DataKeyNames="TID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvTermination_PageIndexChanging"
                        OnRowDataBound="gvTermination_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField  HeaderText="Employee Name">
                                <ItemTemplate>
                                    <div><%#Eval("EFName")%> <%#Eval("EMName")%> <%#Eval("ELName")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Forwarded To">
                                <ItemTemplate>
                                    <div><%#Eval("FFName")%> <%#Eval("FMName")%> <%#Eval("FLName")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                            <asp:BoundField DataField="Approved" HeaderText="Approved Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApprove" runat="server" CssClass="fa fa-thumbs-up" CommandArgument='<%# Eval("TID") %>'
                                        data-toggle="tooltip" title="Approve" CommandName="APPROVE"
                                        CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkReject" runat="server" CssClass="fa fa-thumbs-down" CommandArgument='<%# Eval("TID") %>'
                                        data-toggle="tooltip" title="Deny" CommandName="DENY"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>

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
                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" placeholder="" style="display:inline;margin-right:15px;" width="250px"></asp:TextBox>

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

