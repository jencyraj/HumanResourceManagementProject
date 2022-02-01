<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmpCodeSett.aspx.cs" Inherits="EmpCodeSett" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblprefix" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("empcodesett")%><small></small>

    </h1>
   
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("manageeducationlevel")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Role" CausesValidation="false" onclick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                 <asp:GridView ID="gvEduLevel" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvEduLevel_RowCommand" DataKeyNames="ECode" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvEduLevel_PageIndexChanging" OnRowDataBound="gvEduLevel_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                     
                        <asp:BoundField DataField="CodeItem" HeaderText="CodeItem" />
                        <asp:BoundField DataField="SettOrder" HeaderText="Order" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("ECode") %>'
                                    CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("CodeItem") %>'
                                    CommandName="DEL" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
           <div  class="pull-left dblock rowmargin rowleftmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row hrmhide">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEmployee">
                                        Employee</label>
                                    <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"
                                        ></asp:TextBox> 
                                </div>
                            </div>
                        </div>
                     
            <div class="row rowmargin rowleftmargin">
                <div class="col-xs-4">
                    <label for="ddlItems">
                        <%= hrmlang.GetString("coditem")%></label></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlItems" runat="server" CssClass="form-control" 
                        OnSelectedIndexChanged="ddlItems_SelectedIndexChanged" AutoPostBack="True" >  
                        <asp:ListItem Text="Month" Value="Month"></asp:ListItem>
                        <asp:ListItem Text="Year" Value="Year"></asp:ListItem>
                        <asp:ListItem Text="Department" Value="Department"></asp:ListItem>
                        <asp:ListItem Text="Branch" Value="Branch"></asp:ListItem>
                         <asp:ListItem Text="Designation" Value="Designation"></asp:ListItem>
                        <asp:ListItem Text="SerialNo" Value="SerialNo"></asp:ListItem>
                         <asp:ListItem Text="OtherText" Value="OtherText"></asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
            </div>
           
            <div class="row rowmargin rowleftmargin" runat="server" id="ival">
                <div class="col-xs-4">
                    <label for="txtvalue">
                        <%= hrmlang.GetString("value") %></label>
                  </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtvalue" runat="server" CssClass="form-control" placeholder="Enter Value" ></asp:TextBox>
                </div>
            </div>
             <div class="row rowmargin rowleftmargin">
                <div class="col-xs-4">
                    <label for="txtOrder">
                        <%= hrmlang.GetString("order") %></label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrder"
                        ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Number!" ControlToValidate="txtOrder" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"></asp:CompareValidator></div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="form-control" placeholder="Enter Order"></asp:TextBox>
                </div>
            </div>
           <div class="row rowmargin rowleftmargin" runat="server" id="ipfx">
                <div class="col-xs-4">
                    <label for="txtprefix">
                        <%= hrmlang.GetString("prefix")%></label>
                    </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtprefix" runat="server" CssClass="form-control" placeholder="Enter Prefix"  ></asp:TextBox>
                </div>
            </div>
             <div class="row rowmargin rowleftmargin" runat="server" id="istrt">
                <div class="col-xs-4">
                    <label for="txtstart">
                        <%= hrmlang.GetString("start") %></label>
                   </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtstart" runat="server" CssClass="form-control" placeholder="Enter Order" ></asp:TextBox>
                </div>
            </div>
              <div class="row rowmargin rowleftmargin" runat="server" id="ilen">
                <div class="col-xs-4">
                    <label for="txtlength">
                        <%= hrmlang.GetString("length") %></label>
                   </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtlength" runat="server" CssClass="form-control" placeholder="Enter Value" ></asp:TextBox>
                </div>
            </div>
             <div class="row rowmargin rowleftmargin">
                
                <div class="clearfix">
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtEmpCodeTotalLength" runat="server" CssClass="form-control validate" Visible="False"></asp:TextBox>
                    <p class="help-block"><asp:Label ID="lblEx" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> 
                </div>
            </div>
        
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
    </label>
</asp:Content>

