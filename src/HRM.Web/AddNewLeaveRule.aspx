<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddNewLeaveRule.aspx.cs" Inherits="LeaveRuleSettings" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLeaveTypeID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblindex" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("leavrule")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageleavetypes")%></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
   <div class="box box-primary">
       <!-- /.box-header -->
       <div class="box-body">
           <asp:Panel ID="pnllod" runat="server" Visible="false">
               <div class="col-xs-12" runat="server" id="grd">
               </div>
           </asp:Panel>
           <asp:Panel ID="Pnlnew" runat="server" Visible="false">
               <div class="pull-left dblock rowmargin">
                   <p class="text-red">
                       <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                   <p class="text-green">
                       <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
               </div>
               <div class="clearfix">
               </div>
               <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="txtDescr">
                           <%= hrmlang.GetString("description")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:TextBox ID="txtdescr" runat="server" CssClass="form-control validate" placeholder="Enter Description"></asp:TextBox>
                   </div>
               </div>
               <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="ddlMonth">
                           <%= hrmlang.GetString("startsfrom")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" DataTextField="MonthName"
                           DataValueField="MonthID">
                       </asp:DropDownList>
                   </div>
               </div>
               <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="txtDays">
                           <%= hrmlang.GetString("minleav")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:TextBox ID="txtDays" runat="server" CssClass="form-control validate" placeholder="Enter Minimum Leave"></asp:TextBox>
                   </div>
               </div>
               <div class="row rowmargin">
                   <div class="col-xs-4">
                       <label for="lbyear">
                           <%= hrmlang.GetString("year")%></label></div>
                   <div class="clearfix">
                   </div>
                   <div class="col-xs-4">
                       <asp:TextBox ID="txtYear" runat="server" CssClass="form-control validate" placeholder="Enter Year"></asp:TextBox>
                   </div>
               </div>
       </div>
       <div class="row rowmargin">
           <div class="col-xs-4">
               <label for="lbactive">
                   <%= hrmlang.GetString("active")%></label></div>
           <div class="clearfix">
           </div>
           <div class="col-xs-4">
               <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
           </div>
       </div>
       <div class="col-xs-12">
           <asp:GridView ID="gvAdd" runat="server" AutoGenerateColumns="false" GridLines="None"
               ShowFooter="true" CssClass="table table-bordered table-striped dataTable" DataKeyNames="LDID"
               EmptyDataRowStyle-BorderStyle="None" OnRowCommand="gvAdd_RowCommand" OnRowDataBound="gvAdd_RowDataBound">
               <Columns>
                   <asp:TemplateField HeaderText="Late By">
                       <ItemTemplate>
                           <asp:Label ID="Eddllateby" runat="server" Text='<%# Eval("LateByTypeDesc") %>'></asp:Label>
                           <asp:Label ID="lblLatByCd" runat="server" Visible="false" Text='<%# Eval("LateByType") %>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:DropDownList ID="ddllateby" runat="server" Height="35px" Width="82%">
                               <asp:ListItem Text="Per Month" Value="PM"></asp:ListItem>
                               <asp:ListItem Text="Per Day" Value="PD"></asp:ListItem>
                           </asp:DropDownList>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Late By Time">
                       <ItemTemplate>
                           <asp:Label ID="lblLateTimCd" runat="server" Visible="false" Text='<%# Eval("LateByTime") %>'></asp:Label>
                           <asp:Label ID="EddllatebyTime" runat="server" Text='<%# Eval("LateByTimeDesc") %>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:DropDownList ID="ddllatebyTime" runat="server" Height="35px" Width="82%">
                               <asp:ListItem Text="Hour" Value="H"></asp:ListItem>
                               <asp:ListItem Text="Minute" Value="M"></asp:ListItem>
                           </asp:DropDownList>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Late By Value">
                       <ItemTemplate>
                           <asp:Label ID="Etxtlatevalue" runat="server" Text='<%# Eval("LateByValue") %>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:TextBox ID="txtlatevalue" runat="server" Width="82%" placeholder="Enter Value"
                               CssClass="form-control" Style="display: inline"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtlatevalue"
                               ErrorMessage="*" ForeColor="Red" ValidationGroup="langt"></asp:RequiredFieldValidator>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Loss Type">
                       <ItemTemplate>
                           <asp:Label ID="lblLossTypeCd" runat="server" Visible="false" Text='<%#Eval("LossType") %>'>'></asp:Label>
                           <asp:Label ID="Eddllosstype" runat="server" Text='<%#Eval("LossTypeDesc") %>'>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:DropDownList ID="ddllosstype" runat="server" Height="35px" Width="82%">
                               <asp:ListItem Text="Leave" Value="L"></asp:ListItem>
                               <asp:ListItem Text="Loss of Pay" Value="LOP"></asp:ListItem>
                           </asp:DropDownList>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Loss Time">
                       <ItemTemplate>
                           <asp:Label ID="EddllossTime" runat="server" Text='<%# Eval("LossTimeDesc") %>'></asp:Label>
                           <asp:Label ID="lblLossTimeCd" runat="server" Visible="false" Text='<%#Eval("LossTime") %>'>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:DropDownList ID="ddllossTime" runat="server" Height="35px" Width="98%">
                               <asp:ListItem Text="Day(Applicable for Leave only)" Value="D"></asp:ListItem>
                               <asp:ListItem Text="Hour" Value="H"></asp:ListItem>
                               <asp:ListItem Text="Minute" Value="M"></asp:ListItem>
                           </asp:DropDownList>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Loss Value">
                       <ItemTemplate>
                           <asp:Label ID="Etxtlossvalue" runat="server" Text='<%# Eval("LossValue") %>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:TextBox ID="txtlossvalue" runat="server" Width="82%" placeholder="Enter Loss value"
                               CssClass="form-control" Style="display: inline"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtlossvalue"
                               ErrorMessage="*" ForeColor="Red" ValidationGroup="langt"></asp:RequiredFieldValidator>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Leave Type">
                       <ItemTemplate>
                           <asp:Label ID="Etxtleavtype" runat="server" Visible="false" Text='<%# Eval("LTID") %>'></asp:Label>
                           <asp:Label ID="txtleavname" runat="server" Text='<%# Eval("LeaveName") %>'></asp:Label>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:DropDownList ID="ddlleavtype" runat="server" Height="35px" Width="98%" DataTextField="LeaveName"
                               DataValueField="LeaveTypeID">
                           </asp:DropDownList>
                       </FooterTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("LDID") %>'
                               CommandName="EDITLG" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                           <asp:LinkButton ID="lnkID" runat="server" CommandName="DEL" Text="Action" Visible="false"></asp:LinkButton>
                           <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LDID") %>'
                               CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"
                               data-toggle="tooltip" title="Delete"></asp:LinkButton>
                       </ItemTemplate>
                       <FooterTemplate>
                           <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add"
                               CommandName="ADDKEY" ValidationGroup="langt" />
                       </FooterTemplate>
                   </asp:TemplateField>
               </Columns>
           </asp:GridView>
        <div class="f-right"><small><%= hrmlang.GetString("leaveruletext") %></small></div>
       </div>
       <asp:Label ID="lblIndexE" runat="server" Visible="false"></asp:Label>
       <div class="clearfix">
       </div>
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
</asp:Content>
