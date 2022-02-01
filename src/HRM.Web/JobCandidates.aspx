<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobCandidates.aspx.cs" Inherits="JobCandidates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<script type="text/javascript">
    $('body').ajaxStart(function () {
        $('#spinner').show();
    });

    $('body').ajaxComplete(function () {
        $('#spinner').hide();
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnsearch').click(function () {
            //$("#spinner").append('<img id="img-spinner" src="Images/ajax-loader-test.gif" alt="Loading.." style="position: absolute; z-index: 200; left:50%; top:50%; " />');
            $('#spinner').show().fadeIn(20);
        });
    });
</script>
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
    <section class="content-header">
    <h1><%= hrmlang.GetString("jobcandidates")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("jobcandidates")%></li>
    </ol>
</section>
    <section class="content">
    <div id="spinner" class="spinner" style="display: none; width: 100%; height: 100%; position: absolute; z-index: 100; background-color: rgba(100, 100, 100, 0.4); left: 0; top: 0; bottom: 0; right: 0">
     <img id="img-spinner" src="images/loading.gif" alt="Loading.."  style="position: absolute; z-index: 100; left: 50%; top: 50%;" />
     </div>
   <div class="box box-primary">
        <div class="box-body"> 
        <div  class="pull-right rowmargin" style="padding:18px;"> 

        <asp:Button ID="btnsearch"  runat="server" Text="Search" OnClick="btnSearchStatus_Click" CssClass="btn btn-primary" ClientIDMode="Static"></asp:Button>
        </div>
           <div  class="pull-right rowmargin" style="padding-right:15px;" >             
                <strong><asp:Label ID="lblStatus" runat="server" Text="Status : "  style="padding-right:15px;"></asp:Label> </strong>
                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" DataTextField="Status" DataValueField="ApplicationStatus" style="left:12px"  ></asp:DropDownList>             
          
            </div>   
           <div  class="pull-right rowmargin" style="padding-right:15px;">             
                <strong><asp:Label ID="lblJobtitle" runat="server" Text="JobTitle : " style="padding-right:15px;"></asp:Label> </strong>
                <asp:DropDownList ID="ddlJobTitle" runat="server" DataTextField="JobTitle" DataValueField="JID"  CssClass="form-control"
                   ></asp:DropDownList>             
          
            </div>  
          <%--    <div  class="pull-right rowmargin"> 

                 <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary"  Text="Search" OnClick="btnSearchStatus_Click"  />
              </div>
           --%>
                   
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server" Visible="false"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvCandidates" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvCandidates_RowCommand" DataKeyNames="CandidateID" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvCandidates_PageIndexChanging" 
                    onrowdatabound="gvCandidates_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>                     
                        <asp:TemplateField HeaderText="Candidate Name">
                        <ItemTemplate>
                  <%# Eval("SaluteName")%><%# Eval("FirstName") %> <%# Eval("LastName")%>
                        </ItemTemplate>
                        </asp:TemplateField> 
                            <asp:BoundField DataField="EmailAddress"  HeaderText="EmailAddress" />
                        <asp:BoundField DataField="JobTitle" HeaderText="JobTitle" />
                        <asp:BoundField DataField="AppliedDate" HeaderText="AppliedDate"/>

                        <%--   <asp:BoundField DataField="ApplicationStatus" HeaderText="Status" />--%>
                            <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <%# Statusdef(Eval("ApplicationStatus").ToString())%> 
                        </ItemTemplate>
                        </asp:TemplateField> 
                          <asp:TemplateField>
                            <ItemTemplate>
                             <asp:HyperLink ID="hlnkProfile" runat="server" 
                NavigateUrl='<%# GetUrl(Eval("CandidateID"))%>' 
                text="View Profile" Font-Underline="True"></asp:HyperLink>
                 <asp:HyperLink ID="hlnkChange" runat="server" 
                NavigateUrl='<%# GetUrl(Eval("CandidateID"))%>' 
                text="Change Status" Font-Underline="True"></asp:HyperLink>

                  <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("CandidateID") %>'
                                    CommandName="DEL" CausesValidation="false" OnClientClick="return confirm('Are you sure to delete?')"></asp:LinkButton>
                   
                      
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                      
                    </Columns>
                </asp:GridView> 
            </div>

            <div class="box-footer">
               
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
