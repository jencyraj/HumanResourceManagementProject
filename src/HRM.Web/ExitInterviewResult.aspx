<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ExitInterviewResult.aspx.cs" Inherits="ExitInterviewResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="lblEid" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="lblexit" runat="server" Visible="false"></asp:Label>

 <section class="content-header">
        <h1>
            <%= hrmlang.GetString("mexitinterviewresult")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mexitinterviewresult")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
             <div class="pull-left dblock rowmargin">
                   <p class="text-red">
                       <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                   <p class="text-green">
                       <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
               </div>
              <div class="row rowmargin" id="term" runat="server">
                    <div class="col-xs-2 ">
                        <div class="form-group">
                            <label for="txtEmployee">
                                <%= hrmlang.GetString("employee")%></label>
                        </div>
                    </div>
                    <div class="col-xs-2 ">
                     <asp:Label ID="lblemp" runat="server"  />
                        </div>
                 
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-2 ">
                    <label for="txtforwrd" >
                            <%= hrmlang.GetString("forwardto")%></label>
                      
                    </div>
                    <div class="col-xs-2 ">
                        <asp:Label ID="lblforwardto" runat="server" />
                      
                    </div>
                   
                    
                </div>
                    <div class="row rowmargin" id="term1" runat="server">
                    
                   
                    <div class="col-xs-2 ">
                    <div class="form-group">
                        <label for="txtintdt">
                            <%= hrmlang.GetString("interviewdate")%></label></div>
                            </div>                   
                        <div class="col-xs-2 ">
                             <asp:Label ID="lblIdate" runat="server"  /></div>
                    <div class="clearfix">
                    </div>
                  
                    <div class="col-xs-2 ">
                        <label for="txtapp">
                            <%= hrmlang.GetString("approved")%></label>
                    </div>
                    <div class="col-xs-2 ">
                       <asp:Label ID="lblapp" runat="server"  />
                    </div>
                   
                </div>
                 <div class="row rowmargin" id="reg" runat="server">
                    <div class="col-xs-2 ">
                        <div class="form-group">
                            <label for="txtEmployee">
                                <%= hrmlang.GetString("employee")%></label>
                        </div>
                    </div>
                    <div class="col-xs-2 ">
                     <asp:Label ID="lblemp1" runat="server"  />
                        </div>
                 
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-2 ">
                    <label for="txtforwrd" >
                            <%= hrmlang.GetString("noticedate")%></label>
                      
                    </div>
                    <div class="col-xs-2 ">
                        <asp:Label ID="lblforwardto1" runat="server" />
                      
                    </div>
                   
                    
                </div>
                    <div class="row rowmargin" id="reg1" runat="server">
                    
           
                    <div class="col-xs-2 ">
                     <div class="form-group">
                        <label for="txtintdt">
                            <%= hrmlang.GetString("approved")%></label></div>
                            </div>
                    <div class="col-xs-2 ">
                       
                             <asp:Label ID="lblIdate1" runat="server"  /></div>
                    <div class="clearfix">
                    </div>
                  
                    <div class="col-xs-2 ">
                        <label for="txtapp">
                            <%= hrmlang.GetString("resgndate")%></label>
                    </div>
                    <div class="col-xs-2 ">
                       <asp:Label ID="lblapp1" runat="server"  />
                    </div>
                   
                </div>
               
                 <div class="row rowmargin">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtAddInfo">
                                <%= hrmlang.GetString("remarks")%></label>
                                 </div>
                                  
                                    <div class="form-group">
                            <asp:TextBox ID="txtremark" CssClass="form-control" runat="server" TextMode="MultiLine" Width=325px
                                Style="height: 55px "  ></asp:TextBox>

                                
                           <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtremark"
                               ErrorMessage="*" ForeColor="Red" ValidationGroup="langt"></asp:RequiredFieldValidator>
                               </div>
                       
                    </div>
                </div>
                <div class="col-mg-12 rowmargin">
                    <label for="txtasst">
                            <%= hrmlang.GetString("assets")%></label>
                        
                    <asp:GridView ID="gvasset" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        DataKeyNames="AssetID" EnableViewState="True">
                        
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                         <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfAssetId" runat="server" Value='<%# Eval("AEID") %>' Visible="false" />
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="AssetName" HeaderText="AssetName" />
                             
                              <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHSelect" CssClass="hcadd" runat="server" Enabled="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" CssClass="cadd" runat="server" Style="font-weight: normal !important;
                                            font-family: sans-serif; font-size: 12px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="box-footer">
                    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" OnCommand="btn_Command"  OnClientClick="return validatectrl();" 
                        CommandName="APPROVE" Text="Approve" />
                         
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-primary" Text="Close" OnCommand="btn_Command" OnClientClick="return confirm('Are you sure to Close this Assets?')"
                        CommandName="Close"  data-toggle="tooltip" />
                         
                   <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnCommand="btn_Command"
                        CommandName="Cancel" />
                </div>
        </div>
         
    </section>
</asp:Content>

