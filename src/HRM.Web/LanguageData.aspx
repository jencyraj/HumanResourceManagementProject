<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LanguageData.aspx.cs" Inherits="LanguageData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("languagedata")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("managelanguagedata")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <div style="margin: auto; float: left">
                        <div style="margin: auto; float: left">
                            <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 10px">
                                    <div>
                                        <label for="ddLanguagesSearch">
                                            <%= hrmlang.GetString("languages") %></label><br />
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddLanguagesSearch" runat="server" Height="35px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 10px">
                                    <div>
                                        <label for="txtLangKeySearch">
                                            <%= hrmlang.GetString("languagekey")%></label><br />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtLangKeySearch" runat="server" placeholder="Enter Language Key"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 10px">
                                    <div>
                                        <label for="txtLangData">
                                            <%= hrmlang.GetString("languagetext")%></label><br />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtLangTextSearch" runat="server" Width="250px" placeholder="Enter Language Text"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 15px">
                                    <div>
                                        <label for="btnSearch">
                                        </label>
                                        <br />
                                    </div>
                                    <div>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm" Text="Search"
                                            CausesValidation="false" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 15px">
                                    <div>
                                        <label for="btnNew">
                                        </label>
                                        <br />
                                    </div>
                                    <div>
                                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Text="Add New"
                                            CausesValidation="false" OnClick="btnNew_Click" />
                                    </div>
                                </div>
                            </div>
                             <div style="margin: auto; float: left; padding-right: 15px">
                                <div class="form-group" style="padding-top: 15px">
                                    <div>
                                        <label for="btnupdate">
                                        </label>
                                        <br />
                                    </div>
                                    <div>
                                        <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" Text="Update Resources"
                                            CausesValidation="false" OnClick="btnupdate_Click"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-md-12 rowmargin">
                    <asp:GridView ID="gvLanguageData" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="LID" EnableViewState="True" AllowPaging="true" PageSize="15" ShowHeaderWhenEmpty="true"
                        OnPageIndexChanging="gvLanguageData_PageIndexChanging" OnRowCommand="gvLanguageData_RowCommand">
                        <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                            PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="LangName" HeaderText="Language" />
                            <asp:BoundField DataField="LangKey" HeaderText="Language Key" />
                            <asp:BoundField DataField="LangText" HeaderText="Language Text" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("LangKey") %>'
                                        CommandName="EDITLG" CausesValidation="false" data-toggle="tooltip" title="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("LID") %>'
                                        CommandName="DEL" OnClientClick="return confirm('Are you sure to delete?')" CausesValidation="false"
                                        data-toggle="tooltip" title="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-md-12 rowmargin" id="addnew" style="border: 1px solid rgb(236, 236, 236);
                    width: 97%; margin-left: 15px; padding: 0px 0px 10px;">
                    <div class="col-xs-7">
                        <label for="txtLangKey">
                            Language Key</label>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtLangKey"
                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txtLangKey" runat="server" placeholder="Enter Language Key" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">
                    </div>
                    <br />
                    <div class="col-xs-7">
                        <asp:GridView ID="gvAdd" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAdd_RowDataBound" GridLines="None"
                            ShowFooter="true" CssClass="table table-bordered table-striped dataTable" OnRowCommand="gvAdd_RowCommand" EmptyDataRowStyle-BorderStyle="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Language">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCulture" runat="server" Text='<%# Eval("LangCultureName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblLang" runat="server" Text='<%# Eval("LangName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddLanguages" runat="server" Height="35px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language Text">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtText" runat="server" Text='<%# Eval("LangText") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtLangText" runat="server" Width="82%" placeholder="Enter Language Text"
                                            CssClass="form-control" Style="display: inline"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtLangText"
                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="langt"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Text="Delete" Checked='<%# ("" + Eval("todelete") == "Y") ? true :false %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add"
                                            CommandName="ADDKEY" ValidationGroup="langt" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save"
                            OnCommand="btn_Command" CommandName="SAVE" CausesValidation="true" />
                        <asp:Button ID="btnCancel" class="btn btn-default" Text="Cancel" CausesValidation="false"
                            runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfLID" runat="server" />
    </section>
</asp:Content>
