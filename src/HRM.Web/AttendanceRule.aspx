<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AttendanceRule.aspx.cs" Inherits="AttendanceRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1 id="h1" runat="server">
            <%= hrmlang.GetString("attendancerule")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active" id="LI1" runat="server">
                <%= hrmlang.GetString("attendancerule")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                          <p class="text-green">
                         <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                   <div class="row rowmargin">
                    <div class="col-md-3">
                  <asp:DropDownList ID="ddlrule" runat="server" CssClass="form-control" 
                            AutoPostBack="True" onselectedindexchanged="ddlrule_SelectedIndexChanged">
                      <asp:ListItem Text="Use Value" ></asp:ListItem>
                      <asp:ListItem Text="Use Rule" >Use Rule</asp:ListItem>
                                
                            </asp:DropDownList>
                    </div>
                   
                   </div>
               
                 <asp:Panel ID="pnlRule" runat="server" Visible="false">
                <div class="rowmargin">
                 
                <div class="f-left">
               <asp:TextBox ID="txtZerofrom"  runat="server"  style="width: 70px; font-weight: bold; float: left;" 
               CssClass="form-control"></asp:TextBox>
               <span style="font-weight: bold; font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  -  &nbsp;&nbsp;</span>
               </div>
                  
                <div class="f-left">
                <asp:TextBox ID="txtZeroto" runat="server"  style="width: 70px; font-weight: bold;  float: left;"  
                CssClass="form-control"></asp:TextBox>
                <span id="Span1" style="font-weight: bold;  font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  =  &nbsp;&nbsp;</span>
                </div>            
                 <div class="f-left" style="margin-top:8px">  
                 <span style="font-weight: bold;  font-size:18px;"><%= hrmlang.GetString("zeroattendance")%></span>
                 </div>      
                 </div>      
                   <div class="clearfix"></div>
                      <div class="rowmargin">

                 <div class="f-left">
               <asp:TextBox ID="txthaffrom"  runat="server"  style="width: 70px; font-weight: bold; float: left;" 
               CssClass="form-control"></asp:TextBox>
               <span style="font-weight: bold; font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  -  &nbsp;&nbsp;</span>
               </div>
                  <div class="f-left">
                <asp:TextBox ID="txthafto" runat="server"  style="width: 70px; font-weight: bold;  float: left;"  
                CssClass="form-control"></asp:TextBox>
                <span id="Span2" style="font-weight: bold;  font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  =  &nbsp;&nbsp;</span>
                </div>  
                     <div class="f-left" style="margin-top:8px">  
                 <span style="font-weight: bold;  font-size:18px;"><%= hrmlang.GetString("hafday")%></span>
                 </div>      
                  
                 </div>        
                    
                   <div class="clearfix"></div>
                
                  <div class="rowmargin">
                  <div class="f-left">
                <asp:TextBox ID="txtfullfrom" runat="server"  style="width: 70px; font-weight: bold;  float: left;"  
                CssClass="form-control"></asp:TextBox>
                <span id="Span3" style="font-weight: bold;  font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  =  &nbsp;&nbsp;</span>
                </div>  
               
                   <div class="f-left">
                <asp:TextBox ID="txtfullto" runat="server"  style="width: 70px; font-weight: bold;  float: left;"  
                CssClass="form-control"></asp:TextBox>
                <span id="Span4" style="font-weight: bold;  font-size:18px; display: inline-block; padding: 9px 0px 1px 2px;">%&nbsp;&nbsp;  =  &nbsp;&nbsp;</span>
                </div>  
                         
                 <div class="f-left" style="margin-top:8px">  
                 <span style="font-weight: bold;  font-size:18px;"><%= hrmlang.GetString("1day")%></span>
                 </div>    
                     
                 </div>      
                </asp:Panel>
            <br />
            <br />
            <div class="clearfix">
           </div>
                <div class="row rowmargin">
           
           <div class="clearfix">
           </div>
           <div class="col-xs-4">
           <label for="lbactive">
                   <%= hrmlang.GetString("active")%></label>
               <asp:CheckBox ID="chkActive" runat="server"></asp:CheckBox>
           </div>
       </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="btnSave_Click"  />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                        Text="Cancel" OnClick="btn_Click" />
                </div>
                <div class="clearfix">
                </div>
            </div>
       
    </section>
</asp:Content>
