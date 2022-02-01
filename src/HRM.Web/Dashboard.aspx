<%@ Page Title="HRM :: Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" ValidateRequest="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>
            <asp:Literal ID="ltDash" runat="server" Mode="PassThrough"></asp:Literal><small><asp:Literal
                ID="ltHome" runat="server" Mode="PassThrough"></asp:Literal></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <asp:Literal ID="ltbHome" runat="server" Mode="PassThrough"></asp:Literal></a></li>
            <li class="active">
                <asp:Literal ID="ltbDash" runat="server" Mode="PassThrough"></asp:Literal></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
         <div class="container">
  
  <div class="marquee">
  
    <ul class="marquee-content-items">
     <asp:Literal ID="ltNotify" runat="server" Mode="PassThrough"></asp:Literal>  
    </ul>
  </div>
</div>
            <section class="col-lg-5 connectedSortable">
                <!-- quick email widget -->
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Literal ID="ltProfile" runat="server" Mode="PassThrough"></asp:Literal></h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <label class="w100">
                                <%= hrmlang.GetString("name") %></label>
                            :
                            <asp:Label ID="lblName" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("userid") %></label>
                            :
                            <asp:Label ID="lblUserID" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("role") %></label>
                            :
                            <asp:Label ID="lblRole" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("branch") %></label>
                            :
                            <asp:Label ID="lblBranch" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("department") %></label>
                            :
                            <asp:Label ID="lblDept" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("designation") %></label>
                            :
                            <asp:Label ID="lblDesgn" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("joiningdate") %></label>
                            :
                            <asp:Label ID="lblJoin" runat="server"></asp:Label><br />
                            <label class="w100">
                                <%= hrmlang.GetString("lastlogintime") %></label>
                            :
                            <asp:Label ID="lblLogin" runat="server"></asp:Label><br />
                            <a href="EditProfile.aspx" class="fr-right" ><%= hrmlang.GetString("edit") %></a>
                        </div>
                    </div>
                </div>
            </section><!-- /.Left col -->
             <section class="col-lg-7 connectedSortable">
                <!-- quick email widget -->
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <%= hrmlang.GetString("leavesummary")%></h3>
                    </div>
                    <div class="box-body">
                        <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped dataTable"
                            OnRowDataBound="gvBalance_RowDataBound" >
                            <Columns>
                                <asp:BoundField HeaderText="Leave Type" DataField="LeaveName" />
                                <asp:BoundField HeaderText="No. of Days" DataField="LeaveDays" />
                                <asp:BoundField HeaderText="Carry Over" DataField="CarryOver" Visible="false" />
                                <asp:TemplateField HeaderText="Leaves Taken">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaken" runat="server" Text='<%# Eval("LeavesTaken") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Carry Overed" DataField="PrevYearBalance" />
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Util.ToDecimal(Eval("PrevYearBalance")) +  Util.ToDecimal(Eval("LeavesBalance")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <p class="text-green">
                            <asp:Label ID="lblSubMsg" runat="server"></asp:Label></p>
                    </div>
                </div>
            </section>
        </div>
        <!-- Main row -->
        <div class="row">
            <!-- Left col -->
            <section class="col-lg-7 connectedSortable">
                <!-- quick email widget -->
                <div class="box box-info">
                    <div class="box-header">
                        <i class="fa fa-envelope"></i>
                        <h3 class="box-title">
                            <%= hrmlang.GetString("quickemail")%></h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtTo" runat="server" placeholder="Email to"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtSubject" runat="server" placeholder="Subject"></asp:TextBox>
                        </div>
                        <div>
                            <asp:TextBox ID="txtMsg" runat="server" CssClass="textarea" placeholder="Message"
                                Style="width: 100%; height: 125px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd;
                                padding: 10px;" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-footer clearfix">
                        <asp:LinkButton class="pull-right btn btn-default" ID="btnSendEmail" runat="server"
                            Text="Send" OnClick="btnSendEmail_Click"></asp:LinkButton>
                    </div>
                </div>
            </section><!-- /.Left col -->
          
            <section class="col-lg-5 connectedSortable">
                
                <div class="box box-solid bg-green-gradient">
                    <div class="box-header">
                        <i class="fa fa-calendar"></i>
                        <h3 class="box-title">
                            <%= hrmlang.GetString("calendar")%></h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <!--The calendar -->
                        <div id="calendar" style="width: 100%">
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </section>
        </div>
        <!-- /.row (main row) -->
        
        <div class="row">          
            
            <section class="col-lg-5 connectedSortable">
                <!-- Calendar -->
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Literal ID="ltChartTitle" runat="server" Mode="PassThrough"></asp:Literal></h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <asp:Chart ID="chPie" runat="server" Palette="EarthTones" BackColor="#ECECEC" BackGradientStyle="VerticalCenter"
                            Width="400px" Height="250px" EnableViewState="true" OnCustomize="chPie_Customize"
                            RightToLeft="Inherit" Style="margin: 0px 20px 5px 20px;">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Pie" Color="#9966ff" Font="Open Sans, 12px">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="Beige" BorderColor="Blue">
                                    <AxisX IntervalAutoMode="VariableCount" Interval="1">
                                        <MajorGrid Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto"
                                            LineDashStyle="Dash" />
                                    </AxisX>
                                    <Position Height="100" Width="100" />
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </section><!-- right col -->
            
              <!-- right col (We are only adding the ID to make the widgets sortable)-->
            <section class="col-lg-4 connectedSortable">
              
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title"><%= hrmlang.GetString("holidays") %> :: <asp:Literal ID="ltMonth" runat="server" Mode="PassThrough"></asp:Literal></h3>
                    </div>
                    <div class="box-body">
                    <asp:GridView ID="gvHolidays" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable" EnableViewState="True" 
                    onrowdatabound="gvHolidays_RowDataBound" ShowHeader="false">
                    <Columns>
                        <asp:BoundField DataField="Holiday" HeaderText="Holiday" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                    </Columns>
                </asp:GridView>
                 <p class="text-green">
                            <asp:Label ID="lblHoliday" runat="server"></asp:Label></p> 
                    </div>
                </div>
                <!-- /.box -->
            </section><!-- right col -->
            
        </div>
        <!-- /.row -->
    </section>
    <!-- /.content -->
    </a>
</asp:Content>
