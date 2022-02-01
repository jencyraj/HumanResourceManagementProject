<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrainingEvents.aspx.cs" Inherits="TrainingEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script src="js/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <link href="rating/rating_simple.css" rel="stylesheet" type="text/css" />
    
    <script src="rating/rating_simple.js" type="text/javascript"></script>

     <script language="javascript" type="text/javascript">
         function getratingvalue(value) {
             $("#MainContent_txtratingvalue").val(value);
            // alert( $("#MainContent_txtratingvalue").val());
         }
      

        
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("trainingevents")%><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("trainingevents")%></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
          
                   
            <div class="pull-left dblock rowmargin">
                <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
            </div>
            <div class="clearfix"></div>        
            <div class="col-mg-12 rowmargin">
                <asp:GridView ID="gvTraining" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                    OnRowCommand="gvTraining_RowCommand" DataKeyNames="TrainingID" 
                    EnableViewState="True" AllowPaging="true" PageSize="20" 
                    onpageindexchanging="gvTraining_PageIndexChanging" 
                    onrowdatabound="gvTraining_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" PreviousPageText="Prev" NextPageText="Next" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField DataField="TrainingType" HeaderText="Training Types" />
                        <asp:BoundField DataField="title" HeaderText="Title" />
                        <asp:BoundField DataField="trainer" HeaderText="Trainer" />
                        <asp:BoundField DataField="location" HeaderText="Location" />
                        <asp:BoundField DataField="TrainingDt"  HeaderText="Training Date" />
                         
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTraining" runat="server" Text='<%# Eval("TrainingID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("trainingtype") %>' Visible="false"></asp:Label>
                                 <asp:LinkButton ID="lnkEvaluate" runat="server"   data-toggle="tooltip" CssClass="fa fa-reply" CommandArgument='<%# Eval("TrainingID") %>' CommandName="Evaluate" CausesValidation="false"></asp:LinkButton>
                               <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash-o" CommandArgument='<%# Eval("TrainingID") %>'
                                    CommandName="DEL" OnClientClick="return confirm('Are you sure to delete this Record?')" CausesValidation="false" data-toggle="tooltip" title="Delete Comment"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </div>

            <!--Evaluate Popup starts -->
           <div class="modal fade" id="dvevaluate" tabindex="-1" role="dialog" aria-labelledby="basicModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="H2">
                        <%= hrmlang.GetString("trainingevaluate")%></h4>
                </div>
                <div class="modal-header rowmargin">
                    <div class="col-xs-7"> 
                        <label for="txtTrainingTypes">
                            <%= hrmlang.GetString("trainingtypes")%></label> : <asp:Label ID="txttrainingtype" runat="server"  ></asp:Label><asp:HiddenField ID="hdntrainingId" runat="server"></asp:HiddenField><asp:HiddenField ID="hdnevaluateid" runat="server"></asp:HiddenField></div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                        <label for="txtTitle">
                            <%= hrmlang.GetString("title") %></label> : <asp:Label ID="txtTitle" runat="server"  ></asp:Label>
                    </div>
                    <div class="col-xs-7">
                     <label for="txttrainer">
                            <%= hrmlang.GetString("trainer")%></label> :  <asp:Label ID="txttrainer" runat="server"  ></asp:Label>
                       </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                    <label for="txtlocation">
                          <%=  hrmlang.GetString("location")%></label> :  <asp:Label ID="txtlocation" runat="server"  ></asp:Label>
                    </div>
                    <div class="col-xs-7">
                    <label for="txtdate">
                       <%=  hrmlang.GetString("date")%></label> :  <asp:Label ID="txtdate" runat="server"  ></asp:Label>
                       </div>
                    <div class="clearfix">
                    </div>
                    <div class="col-xs-7">
                     <label class="fr-left" for="rating_simple2">
                        <%= hrmlang.GetString("rating") %> : </label> 
                   <asp:HiddenField ID="txtratingvalue" runat="server" />
                     <input name="my_input"  class="fr-left" id="rating_simple1" type="hidden"  />
                    </div>
                    <div class="col-xs-7">
                     <label for="txtComment">
                        <%= hrmlang.GetString("description") %></label>
                    </div>
                    <div class="col-xs-7">
                       <asp:TextBox ID="txtComment" Placeholder="Enter Text" CssClass="editor1 form-control" runat="server" TextMode="MultiLine" style="height:200px; width:450px;"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtComment"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEvaluate" runat="server" CssClass="btn btn-primary"  ValidationGroup="depgroup"
                        OnClick="btnEvaluate_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        <%= hrmlang.GetString("close") %></button>
                </div>
            </div>
        </div>
    </div><!--Evaluate Popup Ends -->
            <div class="box-footer">
               
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
