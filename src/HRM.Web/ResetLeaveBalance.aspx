<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ResetLeaveBalance.aspx.cs" Inherits="ResetLeaveBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("resetleavebalance")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("resetleavebalance")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
           <div  class="pull-right rowmargin">
            
           </div>
           <div class="clearfix"></div> 
          
            <div class="row rowmargin">
                <div class="col-xs-12">
                     <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-bordered table-striped dataTable" OnRowDataBound="gvBalance_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Year" DataField="LeaveYear" />
                                <asp:BoundField HeaderText="Leave Type" DataField="LeaveName" />
                                <asp:BoundField HeaderText="No. of Days" DataField="LeaveDays"  />
                                <asp:BoundField HeaderText="Carry Over" DataField="CarryOver" />
                                <asp:TemplateField HeaderText="Leaves Taken">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaken" runat="server" Text='<%# Eval("LeavesTaken") %>'></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("LeaveYear") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("LeaveMonth") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblLTID" runat="server" Text='<%# Eval("LeaveTypeID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carried Over">
                                    <ItemTemplate>
                                    <asp:TextBox ID="txtCarry" runat="server" Text='<%# Eval("PrevYearBalance") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBalance" runat="server" Text='<%# Eval("LeavesBalance") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>     
                </div>
            </div>
            <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
        </div>
        <!-- /.box-body -->
    </div> 
    </section> 
        
</asp:Content>
