<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ContractType.aspx.cs" Inherits="ContractType" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("managecontracttype")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managecontracttype")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">            
                    <div style="margin:auto;float:left">
                        <div style="margin:auto;float:left">
                            <div style="margin:auto;float:left;padding-right:14px">
                                <asp:Button ID="btnNew" Visible="false" runat="server" CssClass="btn btn-success btn-sm" Text="New Contract Type" CausesValidation="false" onclick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">     
                    <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvContractType" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="CTId" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true" 
                        OnPageIndexChanging="gvContractType_PageIndexChanging" OnRowCommand="gvContractType_RowCommand" OnRowEditing="gvContractType_RowEditing"
                        OnRowDataBound="gvContractType_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="ContractTypeName" HeaderText="Contract Type Name" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("CTId") %>'
                                        CommandName="EDIT" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("CTId") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Contract type?')" CausesValidation="false" 
                                        data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView> 
                </div>
                <div class="clearfix"></div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtContractTypeName"><%= hrmlang.GetString("contracttypename")%></label> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContractTypeName"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtContractTypeName" runat="server" placeholder="Enter Contract Type Name" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-left:15px">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="txtDescription"><%= hrmlang.GetString("description")%></label>                        
                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" style="height:200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-footer" style="padding-left:15px">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
   </section>
    <script type="text/javascript">
    </script>
    <asp:HiddenField ID="hfCTId" runat="server" />
</asp:Content>
