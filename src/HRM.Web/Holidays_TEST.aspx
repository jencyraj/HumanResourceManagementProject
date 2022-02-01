<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Holidays_TEST.aspx.cs" Inherits="Holidays_TEST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:Label ID="lblHolidayID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1>Holidays<small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Manage Holidays</li>
    </ol>
 </section>
 <script src="js/plugins/datepicker/bootstrap-datepicker.js" ></script>
<script type="text/javascript">
    $(function () {
        $('.dttxtbox').datepicker({});
    });
</script>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Branch" CausesValidation="false" onclick="btnNew_Click" />
                <strong><asp:Label ID="lblBranch" runat="server" Text="Branch : " Visible="false"></asp:Label> </strong><asp:DropDownList ID="ddlBr" runat="server" 
                    CssClass="form-control" DataTextField="Branch" DataValueField="BranchID" Visible="false" 
                    AutoPostBack="True" onselectedindexchanged="ddlBr_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvHolidays" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvHolidays_RowCommand" DataKeyNames="HolidayID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvHolidays_PageIndexChanging" 
                    onrowdatabound="gvHolidays_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="Branch" HeaderText="Branch" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Holiday" HeaderText="Holiday" DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblBr" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblComm" runat="server" Text='<%# Eval("Comments") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lComm" runat="server" Text='<%# (Eval("Comments").ToString().Length>30) ? Eval("Comments").ToString().Substring(0,30)+"..." : Eval("Comments") %>'></asp:Label> <a href="#" data-toggle="tooltip" title='<%# Eval("Comments") %>' id="lnkCom" runat="server" visible='<%# (Eval("Comments").ToString().Length==0 || Eval("Comments").ToString().Length < 30) ? false : true %>'>more...</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("HolidayID") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("HolidayID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Holiday?')" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
                    <div class="clearfix"></div>
            <asp:Panel ID="pnlNew" runat="server" >
    <!-- Calendar STARTS -->
            <div  class="pull-left dblock rowmargin">
              <div class="col-md-4">
                <asp:Calendar ID="dtJan" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" 
                      onselectionchanged="dtJan_SelectionChanged" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div> 
              <div class="col-md-4">
                <asp:Calendar ID="dtFeb" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div> 
              <div class="col-md-4">
                <asp:Calendar ID="dtMarch" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div>            
    </div>
     <div  class="pull-left dblock rowmargin">
          <div class="col-md-4">
                <asp:Calendar ID="dtApril" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div> 
            <div class="col-md-4">
                <asp:Calendar ID="dtMay" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div> 
           <div class="col-md-4">
                <asp:Calendar ID="dtJune" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div>
     </div>
      <div  class="pull-left dblock rowmargin">
              
               
              <div class="col-md-4">
                <asp:Calendar ID="dtJuly" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div> 
            <div class="col-md-4">
                <asp:Calendar ID="dtAug" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div> 
             <div class="col-md-4">
                <asp:Calendar ID="dtSep" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div> 
    </div>
     <div  class="pull-left dblock rowmargin">
             
              <div class="col-md-4">
                <asp:Calendar ID="dtOct" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
           </div> 
              <div class="col-md-4">
                <asp:Calendar ID="dtNov" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div> 
            <div class="col-md-4">
                <asp:Calendar ID="dtDec" runat="server" BackColor="#FFFFCC" 
                    BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                    ShowGridLines="True" Width="220px" ShowNextPrevMonth="False" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            </div> 
    </div>
    <!-- Calendar ENDS -->
           <div  class="pull-left dblock rowmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="ddlBranches">
                        Branch</label>
                </div>
                 <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlBranches" runat="server" CssClass="form-control" DataTextField="Branch" DataValueField="BranchID"></asp:DropDownList>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDesc">
                        Description</label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDate">
                        Holiday</label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control validate dttxtbox" placeholder="Select Date"></asp:TextBox>
                </div>
            </div>
            <div class="row rowmargin">
                <div class="col-xs-4">
                    <label for="txtDays">
                        Comments</label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtComments" runat="server" CssClass="form-control validate" placeholder="Enter Comments"></asp:TextBox>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validatectrl();" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
            </asp:Panel>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>

