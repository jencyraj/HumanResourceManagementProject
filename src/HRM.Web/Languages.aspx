<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Languages.aspx.cs" Inherits="Languages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblLangID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("languages") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("manageLanguages") %></li>
        </ol>
    </section>
    <style type="text/css">
      /* div.iradio_minimal
        {
            height: 35px;
            margin-left: -63px;
        }
        
        .iradio_minimal
        {
            background-color: transparent !important;
        }*/
    </style>
    
    <!-- Main content -->
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="pull-right rowmargin">
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                        Text="New Lang" CausesValidation="false" OnClick="btnNew_Click" /></div>
                <div class="col-mg-12 rowmargin">
               
                                <asp:GridView ID="gvLanguages" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                                    DataKeyNames="LanguageID" EnableViewState="True" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="gvLanguages_PageIndexChanging" 
                                    onrowdatabound="gvLanguages_RowDataBound">
                                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next"
                                        PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundField DataField="LangCultureName" HeaderText="Lang. Culture Name" />
                                        <asp:BoundField DataField="LangName" HeaderText="Language" />
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# ("" + Eval("Active") == "Y") ? true : false %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Default Lang">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLang" runat="server">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:Label ID="lblDefault" runat="server" Visible="false" Text='<%# Eval("DefaultLang") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Website Style">
                                            <ItemStyle Width="250px" />
                                            <ItemTemplate>
                                            <asp:Label ID="lblStyle" runat="server" Visible="false" Text='<%# Eval("StyleSheets") %>'></asp:Label>
                                                <asp:RadioButtonList ID="rbtnStyle" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Left to Right&nbsp;" Value="LR"></asp:ListItem>
                                                    <asp:ListItem Text="Right to Left" Value="RL"></asp:ListItem>
                                                </asp:RadioButtonList>
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
                    <div class="row rowmargin hrmhide">
                        <div class="col-xs-4">
                            <label for="txtLang">
                                Lang</label>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtLang" runat="server" CssClass="form-control" placeholder="Enter Lang"
                                Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </section>
</asp:Content>
