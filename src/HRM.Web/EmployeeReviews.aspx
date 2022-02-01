<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmployeeReviews.aspx.cs" Inherits="EmployeeReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
<style type="text/css">
.listreview{ height:420px; overflow-y:scroll;}
</style>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("employeereviews")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("employeereviews")%></li>
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
                <div onclick="ShowFilter()">
                    <a href="#" class="ancfilter  fa fa-angle-down">
                        <%= hrmlang.GetString("advancedfilter")%></a></div>
                <div class="advfilter" style="display: none;" id="dvFilter" runat="server">
                    <div class="row rowmargin">
                        <div class="col-xs-2">
                            <label for="txtfName">
                                <%= hrmlang.GetString("branch") %></label>
                            <asp:CompareValidator ID="cmp0" runat="server" ControlToValidate="ddlBranch" Operator="NotEqual"
                                Type="String" ValueToCompare="" ErrorMessage="Required" CssClass="text-red"></asp:CompareValidator></div>
                        <div class="col-xs-2">
                            <label for="txtmName">
                                <%= hrmlang.GetString("maindepartment") %></label></div>
                        <div class="col-xs-2">
                            <label for="txtmName">
                                <%= hrmlang.GetString("department") %></label></div>
                        <div class="col-xs-2">
                            <label for="ddlStatus">
                                <%= hrmlang.GetString("status") %></label>
                        </div>
                        <div class="col-xs-2">
                            <label for="txtlName">
                                <%= hrmlang.GetString("role") %></label>
                        </div>
                        <div class="col-xs-2">
                            <label for="txtlName">
                                <%= hrmlang.GetString("designation") %></label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" DataTextField="Branch"
                                DataValueField="BranchID" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                DataValueField="DepartmentID" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="form-control" DataTextField="DepartmentName"
                                DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="[SELECT]" Value=""></asp:ListItem>
                                <asp:ListItem Text="Current" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Previous" Value="P"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName"
                                DataValueField="RoleID">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="ddlDesgn" runat="server" CssClass="form-control" DataTextField="Designation"
                                DataValueField="DesignationID">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-2">
                        <label for="txtEmpCode">
                            <%= hrmlang.GetString("employeecode") %></label></div>
                    <div class="col-xs-2">
                        <label for="txtfName">
                            <%= hrmlang.GetString("fname") %></label></div>
                    <div class="col-xs-2">
                        <label for="txtmName">
                            <%= hrmlang.GetString("mname") %></label></div>
                    <div class="col-xs-2">
                        <label for="txtlName">
                            <%= hrmlang.GetString("lname") %></label></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control validate"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtfName" runat="server" CssClass="form-control validate"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtmName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtlName" runat="server" CssClass="form-control validate"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"  ClientIDMode="Static" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div class="row rowmargin">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            OnRowCommand="gvEmployee_RowCommand" OnRowDataBound="gvEmployee_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        <asp:Label ID="lblName2" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                        <asp:Label ID="lblName3" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpCode" />
                                <asp:BoundField DataField="Branch" />
                                <asp:BoundField DataField="DepartmentName" />
                                <asp:BoundField DataField="Designation" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReview" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("EmployeeID") %>'
                                            CommandName="REVIEWS" data-toggle="tooltip">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
    <input type="hidden" name="hid1" id="hid1" value="0" />
    <asp:Label ID="lblEmpID" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript">
        function ShowFilter() {
            //alert($('#hid1').val());
            if ($('#hid1').val() == "0") {
                $('.ancfilter').removeClass('fa-angle-down');
                $('.ancfilter').addClass('fa-angle-up');
                document.getElementById('hid1').value = "1";
                $('.advfilter').css("display", "");
            }
            else {
                $('.ancfilter').removeClass('fa-angle-up');
                $('.ancfilter').addClass('fa-angle-down');
                document.getElementById('hid1').value = "0";
                $('.advfilter').css("display", "none");
            }
        }


        function OpenPanel() {
            $('.btnClose').css("display", "none");
            $('.btnBack').css("display", "");
            $('.reviewtitle').css("display", "none");
            $('.appperiodhdr').css("display", "none");
            $('.reviewhdr').css("display", "");
        }

        function ClosePanel() {
            document.getElementById('<%=pnlList.ClientID%>').style.display = '';
            document.getElementById('<%=pnlList.ClientID%>').style.display = '';
            $('.btnClose').css("display", "");
            $('.reviewtitle').css("display", "");
            $('.appperiodhdr').css("display", "");
            document.getElementById('<%=pnlDetails.ClientID%>').style.display = 'none';
            $('.btnBack').css("display", "none");
            $('.reviewhdr').css("display", "none");
        }
    </script>
    <div class="modal fade" id="dvForm" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog" style="width: 750px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H5">
                        <span class="appperiodhdr">
                            <%= hrmlang.GetString("appraisalperiod") %></span><span class="reviewhdr" style="display:none"
                                visible="false"><%= hrmlang.GetString("reviewdetails") %><asp:Literal ID="ltEmp" runat="server"></asp:Literal></span></h4>
                </div>
                <asp:Label ID="lblEmpName" runat="server" Visible="false"></asp:Label>
                <div class="modal-header rowmargin">
                    <asp:Panel ID="pnlList" runat="server">
                        <asp:GridView ID="gvAppraisal" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            OnRowCommand="gvAppraisal_RowCommand" DataKeyNames="AppPeriodID" EnableViewState="True"
                            AllowPaging="true" PageSize="5" OnRowDataBound="gvAppraisal_RowDataBound" OnPageIndexChanging="gvAppraisal_PageIndexChanging">
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                            <PagerStyle HorizontalAlign="Right" />
                            <Columns>
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:d}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("AppPeriodID") %>'
                                            CommandName="FILL" CausesValidation="false" data-toggle="tooltip" title="View Reviews"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblAppPeriodID" runat="server" Visible="false"></asp:Label>
                        <h4 class="modal-title reviewtitle" id="H1" style="display:none">
                            <%= hrmlang.GetString("reviews") %></h4>
                        <asp:GridView ID="gvReviews" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            EnableViewState="True" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvReviews_PageIndexChanging"
                            OnRowDataBound="gvReviews_RowDataBound" OnRowCommand="gvReviews_RowCommand">
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                            <PagerStyle HorizontalAlign="Right" />
                            <Columns>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfname" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        <asp:Label ID="lblmname" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                        <asp:Label ID="lbllname" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ReviewedDate" HeaderText="Reviewed Date" />
                                <asp:BoundField DataField="AvgRating" HeaderText="Avg. Rating" />
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label>
                                        <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server"
                                            visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'>
                                            <%= hrmlang.GetString("more") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("ReviewID") %>'
                                            CommandName="VIEWDETAILS" CausesValidation="false" data-toggle="tooltip" title="Fill Appraisal Form"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="pull-left dblock rowmargin">
                            <p class="text-red">
                                <asp:Label ID="lblErrMsg" runat="server"></asp:Label></p>
                            <p class="text-green">
                                <asp:Label ID="lblSMsg" runat="server"></asp:Label></p>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlDetails" CssClass="listreview" runat="server" style="display:none;">
                        <label><%= hrmlang.GetString("reviewer") %> : </label>
                        <asp:Label ID="lblFname" runat="server"></asp:Label><asp:Label ID="lblMname" runat="server"></asp:Label><asp:Label ID="lblLname" runat="server"></asp:Label><br />
                        <asp:Repeater ID="rptrForm" runat="server" OnItemDataBound="rptrForm_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" width="100%">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblComHeader" runat="server" Text='<%# Eval("CompetencyType") %>'
                                            Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblCTID" runat="server" Visible="false" Text='<%# Eval("CompetencyTypeID") %>'></asp:Label>
                                        <asp:GridView ID="gvForm" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                                            OnRowDataBound="gvForm_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Description" DataField="CompetencyName" />
                                                <asp:TemplateField HeaderText="Employee Rating">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("RatingID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reviewer Rating">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCID" runat="server" Text='<%# Eval("CompetencyID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRvwRating" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <br />
                        <div class="form-group">
                            <label for="txtComments">
                                <%= hrmlang.GetString("comments") %></label>
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control"
                                ReadOnly="true"></asp:TextBox>
                        </div>
                    </asp:Panel>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btnBack"style="display:none" onclick="ClosePanel()"><%= hrmlang.GetString("back") %></button>
                    <button type="button" class="btn btn-default btnClose" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div>
    <asp:DropDownList ID="ddlRating" runat="server" DataTextField="RatingDesc" DataValueField="RatingID" Visible="false">
    </asp:DropDownList>
</asp:Content>
