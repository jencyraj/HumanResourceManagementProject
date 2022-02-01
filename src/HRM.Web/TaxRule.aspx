<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TaxRule.aspx.cs" Inherits="TaxRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("taxrule") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managetaxrule")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Tax rule" CausesValidation="false" onclick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">     
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvTaxRule" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="TaxRuleId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvTaxRule_PageIndexChanging" OnRowCommand="gvTaxRule_RowCommand"
                        OnRowDataBound="gvTaxRule_RowDataBound" OnRowEditing="gvTaxRule_RowEditing">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="SalaryFrom" HeaderText="Salary From" />
                            <asp:BoundField DataField="SalaryTo" HeaderText="Salary To" />
                            <asp:BoundField DataField="TaxPercentage" HeaderText="Tax Percentage" />
                            <asp:BoundField DataField="ExemptedTaxAmount" HeaderText="Exempted Tax Amount" />
                            <asp:BoundField DataField="AdditionalTaxAmount" HeaderText="Additional Tax Amount" />
                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("TaxRuleId") %>'
                                        CommandName="EDIT" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TaxRuleId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Bonus?')" CausesValidation="false" 
                                        data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView> 
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtSalaryFrom"><%= hrmlang.GetString("salfrom") %></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSalaryFrom"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtSalaryFrom" runat="server" placeholder="Enter Salary From" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtSalaryTo"><%= hrmlang.GetString("salto") %></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSalaryTo"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtSalaryTo" runat="server" placeholder="Enter Salary To" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtTaxPercentage"><%= hrmlang.GetString("ptax") %></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTaxPercentage"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtTaxPercentage" runat="server" placeholder="Enter Tax Percentage" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtExemptedTaxAmount"><%= hrmlang.GetString("extaxamount")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExemptedTaxAmount"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtExemptedTaxAmount" runat="server" placeholder="Enter Exempted Tax Amount" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtAdditionalTaxAmount"><%= hrmlang.GetString("addtaxamount") %></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAdditionalTaxAmount"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtAdditionalTaxAmount" runat="server" placeholder="Enter Additional Tax Amount" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlGender"><%= hrmlang.GetString("gender") %></label>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=txtSalaryFrom.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $('#<%=txtSalaryTo.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $('#<%=txtTaxPercentage.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $('#<%=txtExemptedTaxAmount.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $('#<%=txtAdditionalTaxAmount.ClientID%>').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

        });
    </script>
    <asp:HiddenField ID="hfTaxRuleId" runat="server" />
</asp:Content>

