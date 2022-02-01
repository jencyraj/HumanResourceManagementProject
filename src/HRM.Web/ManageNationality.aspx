<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageNationality.aspx.cs" Inherits="ManageNationality" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <section class="content-header">
        <h1><%= hrmlang.GetString("managenationality")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("managenationality")%></li>
        </ol>
    </section>
    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
   
     <div class="fr-left" style="margin-left:15px;">
        <label for="ddlLang">
        </label>
        <asp:DropDownList ID="ddlLang" runat="server" DataTextField="LangName" DataValueField="LangCultureName"
            AutoPostBack="true" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged" CssClass="form-control txtround" >
        </asp:DropDownList>
    </div>

    <div class="fr-right col-sm-3"  style="margin-top:-15px;">
        <asp:HyperLink ID="btnDownload" runat="server" Text="Download Import Format" CssClass="btn btn-sm btn-primary" NavigateUrl="~/templates/nationality_import_format.xls" Target="_blank" ></asp:HyperLink>
        <asp:Button ID="btnImport" runat="server" Text="Import" 
            CssClass="btn btn-sm btn-primary" style="margin-right:15px;" 
            CausesValidation="False" onclick="btnImport_Click" />
        <br />
        <asp:FileUpload ID="flUpload" runat="server" Width="175px" style="display:inline" Visible="false" />
        <asp:Button ID="btnUpload" runat="server" Visible="false" Text="Upload" 
            CssClass="btn success btn-sm" style="display:inline" onclick="uploadexel" />
    </div>
    <div class="clearfix">
    </div>
    <br />
    <div class="col-md-5">
        <asp:GridView ID="gvNationality" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-bordered table-striped dataTable"  >
        <RowStyle HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
            <Columns>
             
                <asp:TemplateField HeaderText="National Code">
                    
                <ItemTemplate>
                <asp:Label ID="lblnational" runat="server" Text='<%# Eval("NationalityCode") %>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="National Name">
                    <ItemTemplate>
                       <asp:TextBox ID="txtNational" runat="server" Text='<%# Eval("Nationality") %>' CssClass="form-control" ValidationGroup="errmsg"  Width="250px" style="display:inline"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"  ID="validator" ErrorMessage="*" ControlToValidate="txtNational" ValidationGroup="errmsg"></asp:RequiredFieldValidator> 
                    </ItemTemplate>
             
                </asp:TemplateField>
                  
            </Columns>

        </asp:GridView>
          <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" 
                        ValidationGroup="depgroup" Text="Save" onclick="btnSave_Click"  />
              
                     <asp:Button ID="btncancel" runat="server" class="btn btn-default" Text="Cancel" CausesValidation="False" OnClick="cancelclick" />
                </div>
    </div>
    </div>
    </section>
</asp:Content>

