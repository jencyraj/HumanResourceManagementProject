<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Deductions.aspx.cs" Inherits="Deductions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblDedID" runat="server" Visible="false"></asp:Label>    
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("managedeductions")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("managedeductions")%></li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div  class="pull-right rowmargin">
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false" 
                    Text="New Deduction" CausesValidation="false" onclick="btnNew_Click" /></div>
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvDeduction" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvDeduction_RowCommand" DataKeyNames="DedID" 
                    EnableViewState="True" AllowPaging="true" PageSize="15" 
                    onpageindexchanging="gvDeduction_PageIndexChanging" 
                    onrowdatabound="gvDeduction_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="DedCode" HeaderText="Deduction Code" />
                        <asp:BoundField DataField="DeductionName" HeaderText="Deduction Name" />
                         <asp:TemplateField HeaderText="Deduction Type">
                            <ItemTemplate>
                                <asp:Label ID="lblDedType" runat="server" Text='<%# ("" + Eval("DedType") == "P" ) ? "Percentage" : "Amount" %>'></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("DedType") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DedAmount" HeaderText="Deduction Value" />
                        <asp:TemplateField HeaderText="Use for Tax Exemption">
                            <ItemTemplate>
                                <asp:Label ID="lblTaxEx" runat="server" Text='<%# ("" + Eval("TaxExemption") == "Y" ) ? "Yes" : "No" %>'></asp:Label>
                                <asp:Label ID="lblTaxType" runat="server" Text='<%# Eval("TaxExemption") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("DedID") %>'
                                    CommandName="EDITBR" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("DedID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Deduction?')" CausesValidation="false" data-toggle="tooltip" title="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>
            <asp:Panel ID="pnlNew" runat="server" Visible="false">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtCode"><%= hrmlang.GetString("deductioncode")%></label> <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Enter Deduction Code"></asp:TextBox>
                        </div>
                    </div>
                </div>                  
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtDeduction"><%= hrmlang.GetString("deductionname")%></label> <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDeduction"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtDeduction" runat="server" CssClass="form-control" placeholder="Enter Deduction"></asp:TextBox>
                        </div>
                    </div>
                </div>                  
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlType"><%= hrmlang.GetString("deductiontype")%></label>     
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="Amount" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Percentage" Value="P"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>                  
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtValue"><%= hrmlang.GetString("deductionvalue")%></label><asp:CompareValidator ID="CMP1" runat="server" ControlToValidate="txtValue"
                                Operator="DataTypeCheck" Type="Double" ErrorMessage="Invalid Number" CssClass="text-red"></asp:CompareValidator>                   
                            <asp:TextBox ID="txtValue" runat="server" CssClass="form-control" placeholder="Enter Deduction"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" id="dvTax" runat="server">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtValue"><%= hrmlang.GetString("usefortaxexemption")%></label>                   
                            <asp:CheckBox ID="chkTax" runat="server" />
                        </div>
                    </div>
                </div>   
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <div class="clearfix"></div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
