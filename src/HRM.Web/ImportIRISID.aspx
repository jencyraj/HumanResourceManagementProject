<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ImportIRISID.aspx.cs" Inherits="ImportIRISID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<section class="content-header">
    <h1><%= hrmlang.GetString("impirisid")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("impirisid")%></li>
    </ol>
</section>
 <section class="content">
  <div class="box box-primary">
    <div class="box-body">
        <!-- Small boxes (Stat box) -->
        <div class="row">
   
     <div class="fr-left" style="margin-left:15px;">
      
       <%-- <asp:DropDownList ID="ddlLang" runat="server" DataTextField="LangName" DataValueField="LangCultureName"
            AutoPostBack="true" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged" CssClass="form-control txtround" >
        </asp:DropDownList>--%>
    </div>
    <div class="clearfix">
    </div>
    <div class="fr-left col-sm-3"  >
        <asp:HyperLink ID="btnDownload" runat="server" Text="Download Import Format" CssClass="btn btn-sm btn-primary" NavigateUrl="~/templates/irisid_import_format.xls" Target="_blank" ></asp:HyperLink>
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
      <asp:Label runat="server" ID="lblupdatecound" Text="No: Of Records Updated"></asp:Label> :
       <asp:Label runat="server" ID="lbldisplycount"  Font-Bold="True"></asp:Label>
    
       <asp:LinkButton runat="server" ID="lbnviewupdated" Text="View List" 
            onclick="linkbuttnclick"></asp:LinkButton>
    <div class="clearfix">
    </div>
    <asp:Label runat="server" ID="lblnullist" Text="No: Of Records Not Updated"></asp:Label> :
       <asp:Label runat="server" ID="lblnullno"  Font-Bold="True"></asp:Label>
    
       <asp:LinkButton runat="server" ID="lbnnullview" Text="View List" 
            onclick="linkbuttclick"></asp:LinkButton>
    <div class="clearfix">
    </div>
    
     <asp:GridView ID="gvirisrecord" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-bordered table-striped dataTable" Visible="false" >
            
      
            <Columns>
              <asp:TemplateField HeaderText="Employee Code">
                    
                <ItemTemplate>
                <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name">
                    
                <ItemTemplate>
                <asp:Label ID="lblempname" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                       <asp:TemplateField HeaderText="IRIS ID">
                    <ItemTemplate>
                     <asp:Label ID="lblempname" runat="server" Text='<%# Eval("IrisId") %>'></asp:Label>
              <%--  <asp:TextBox ID="txtCountry" runat="server" Text='<%# Eval("IrisId") %>' CssClass="form-control" ValidationGroup="errmsg"  Width="250px" style="display:inline"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"  ID="validator" ErrorMessage="*" ControlToValidate="txtCountry" ValidationGroup="errmsg"></asp:RequiredFieldValidator> 
             --%>   </ItemTemplate>
             
                </asp:TemplateField>    
                  
            </Columns>

        </asp:GridView>


         <div class="clearfix">
    </div>
    
     <asp:GridView ID="grdnullview" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-bordered table-striped dataTable" Visible="false" >
            
      
            <Columns>
              
                <asp:TemplateField HeaderText="Employee Name">
                    
                <ItemTemplate>
                <asp:Label ID="lblempname" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                       <asp:TemplateField HeaderText="IRIS ID">
                    <ItemTemplate>
                     <asp:Label ID="lblempname" runat="server" Text='<%# Eval("IrisId") %>'></asp:Label>
              <%--  <asp:TextBox ID="txtCountry" runat="server" Text='<%# Eval("IrisId") %>' CssClass="form-control" ValidationGroup="errmsg"  Width="250px" style="display:inline"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"  ID="validator" ErrorMessage="*" ControlToValidate="txtCountry" ValidationGroup="errmsg"></asp:RequiredFieldValidator> 
             --%>   </ItemTemplate>
             
                </asp:TemplateField>    
                  
            </Columns>

        </asp:GridView>
        </div></div>
 </div>
    </div>
    </section>
</asp:Content>

