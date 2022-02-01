<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Branches.aspx.cs" Inherits="Branches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblBranchID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("company")%><small><%= hrmlang.GetString("branches") %></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managebranches") %></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="New Branch"
                        CausesValidation="false" OnClick="btnNew_Click" Visible="false" /></div>
                <div class="col-mg-12 rowmargin">
                    <asp:GridView ID="gvBranches" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvBranches_RowCommand" DataKeyNames="BranchID" EnableViewState="True"
                        AllowPaging="true" PageSize="5" OnPageIndexChanging="gvBranches_PageIndexChanging"
                        OnRowDataBound="gvBranches_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                    <%# ("" + Eval("City") == "") ? "" : "<br />" %>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="State" HeaderText="State" />
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:TemplateField HeaderText="Contact #">
                                <ItemTemplate>
                                    <strong>
                                        <%= hrmlang.GetString("telephone") %># : </strong>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone1") %>'></asp:Label><br />
                                    <strong>
                                        <%= hrmlang.GetString("mobile") %># : </strong>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Phone2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CountryID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("BranchID") %>'
                                        data-toggle="tooltip" title="Edit" CommandName="EDITBR" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("BranchID") %>'
                                        data-toggle="tooltip" title="Delete" CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Branch?')"
                                        CausesValidation="false"></asp:LinkButton>
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
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtBranch">
                                <%= hrmlang.GetString("branch") %></label>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="*" ControlToValidate="txtBranch" ValidationGroup="alpha" ForeColor="Red"></asp:RequiredFieldValidator>
                                
                                </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control txtround validate" placeholder="Enter Branch"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtAddress">
                                <%= hrmlang.GetString("address") %></label>
                                <asp:RequiredFieldValidator ID="reqadd" runat="server" ErrorMessage="*" ControlToValidate="txtAddress" ValidationGroup="alpha" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control txtround validate" placeholder="Enter Address"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtCity">
                                <%= hrmlang.GetString("city") %></label>
                                <asp:RequiredFieldValidator ID="Redcity" runat="server" ErrorMessage="*" ControlToValidate="txtCity" ValidationGroup="alpha" ForeColor="Red"></asp:RequiredFieldValidator>
                              
                                </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control txtround validate" placeholder="Enter City"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtState">
                                <%= hrmlang.GetString("state") %></label>
                                <asp:RequiredFieldValidator ID="Reqstate" runat="server" ErrorMessage="*" ControlToValidate="txtState" ValidationGroup="alpha" ForeColor="Red"></asp:RequiredFieldValidator>
                              </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control txtround validate" placeholder="Enter State"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="ddlCountry">
                                <%= hrmlang.GetString("country") %></label>
                                <asp:RequiredFieldValidator ID="Reqcountry" runat="server" ErrorMessage="*" ControlToValidate="ddlCountry" ValidationGroup="alpha" ForeColor="Red"></asp:RequiredFieldValidator>
                              </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control txtround" DataTextField="CountryName"
                                DataValueField="CountryID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtPhone">
                                <%= hrmlang.GetString("telephone") %></label>&nbsp;<asp:RegularExpressionValidator ID="rgxPhone"
                                    runat="server" ControlToValidate="txtPhone" ValidationExpression="^[-()@_.+ 0-9]{4,}$" ForeColor="Red"
                                    ErrorMessage="Invalid!" ValidationGroup="alpha"></asp:RegularExpressionValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control txtround validate" placeholder="Enter Telephone" ValidationGroup="albha"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtMobile">
                                <%= hrmlang.GetString("mobile") %></label>&nbsp;<asp:RegularExpressionValidator ID="rgxmobile"
                                    runat="server" ControlToValidate="txtMobile" ValidationExpression="^[-()@_.+ 0-9]{4,}$" ForeColor="Red"
                                    ErrorMessage="Invalid!" ValidationGroup="alpha"></asp:RegularExpressionValidator></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control txtround validate" placeholder="Enter Mobile" ValidationGroup="alpha" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row rowmargin">
                        <div class="col-xs-4">
                            <label for="txtEmail">
                                <%= hrmlang.GetString("emailaddress") %></label></div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txtround validate" placeholder="Enter Email"
                                type="email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"
                            ValidationGroup="alpha" CausesValidation="true"/>
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
