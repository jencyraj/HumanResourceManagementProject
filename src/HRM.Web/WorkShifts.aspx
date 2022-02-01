<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkShifts.aspx.cs" Inherits="WorkShifts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="css/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <script src="js/plugins/timepicker/bootstrap-timepicker.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblWSID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("workshifts")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("manageworkshifts")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                        Text="New WorkShift" CausesValidation="false" OnClick="btnNew_Click" /></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvWorkShift" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvWorkShift_RowCommand" DataKeyNames="WSID" EnableViewState="True"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvWorkShift_PageIndexChanging"
                        OnRowDataBound="gvWorkShift_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="WorkShiftName" HeaderText="Shift Name" />
                            <asp:BoundField DataField="WorkingHours" HeaderText="Shift Name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("StartTime") %>'></asp:Label> - 
                                    <asp:Label ID="lblEndTime" runat="server" Text='<%# Eval("EndTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break Hour #1">
                                <ItemTemplate>
                                    <asp:Label ID="lblStart1" runat="server" Text='<%# Eval("BreakHour1Start") %>'></asp:Label> - 
                                    <asp:Label ID="lblEnd1" runat="server" Text='<%# Eval("BreakHour1End") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break Hour #2">
                                <ItemTemplate>
                                    <asp:Label ID="lblStart2" runat="server" Text='<%# Eval("BreakHour2Start") %>'></asp:Label> - 
                                    <asp:Label ID="lblEnd2" runat="server" Text='<%# Eval("BreakHour2End") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break Hour #3">
                                <ItemTemplate>
                                    <asp:Label ID="lblStart3" runat="server" Text='<%# Eval("BreakHour3Start") %>'></asp:Label> - 
                                    <asp:Label ID="lblEnd3" runat="server" Text='<%# Eval("BreakHour3End") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("WSID") %>'
                                        CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("WSID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this WorkShift?')"
                                        CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlNew" runat="server" Visible="false">
                    <div class="pull-left dblock rowmargin">
                        <p class="text-red">
                            <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                        <p class="text-green">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtName">
                                    <%= hrmlang.GetString("workshiftname")%></label>
                                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Work Shift"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <label for="txtStart">
                                    <%= hrmlang.GetString("starttime")%></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStart" runat="server" CssClass="timepicker form-control" placeholder="Enter Start Time"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <label for="txtEnd">
                                    <%= hrmlang.GetString("endtime") %></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEnd" runat="server" CssClass="timepicker form-control" placeholder="Enter Work Shift"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtworkhours">
                                    <%= hrmlang.GetString("workhours")%></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtworkhours"
                                    ErrorMessage="Only Numbers allowed" ValidationExpression="\d+" CssClass="text-red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtworkhours" runat="server" CssClass="form-control" placeholder="Enter Working Hours"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                <%= hrmlang.GetString("breakhour")%></label>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="row">
                        <div class="col-md-1" style="width: 25px">
                            <label>
                                #1</label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak1Start" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak1End" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1" style="width: 25px">
                            <label>
                                #2</label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak2Start" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak2End" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1" style="width: 25px">
                            <label>
                                #3</label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak3Start" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bootstrap-timepicker">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBreak3End" runat="server" CssClass="timepicker form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <script type="text/javascript">
        $(function () {
            $(".timepicker").timepicker({
                showInputs: false,
                minuteStep: 5,
                defaultTime: false
            });
        });
    </script>
</asp:Content>
