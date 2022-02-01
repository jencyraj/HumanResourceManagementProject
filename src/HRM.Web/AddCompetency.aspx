<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddCompetency.aspx.cs" Inherits="AddCompetency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblCompetencyID" runat="server" Visible="false"></asp:Label>
 <section class="content-header">
    <h1><%= hrmlang.GetString("addnewcompetency") %><small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
        <li class="active"><%= hrmlang.GetString("mappraisalcompetency") %></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">  
           <div  class="pull-left dblock rowmargin">     
           <p class="text-red">
                    <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                <p class="text-green">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div> 
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlRole"><%= hrmlang.GetString("appraisalperiod") %></label>                        
                        <asp:ListBox ID="ddlAppPeriod" runat="server" CssClass="form-control" DataTextField="DESCRIPTION" DataValueField="AppPeriodID" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="row hrmhide">
                <div class="col-md-3 hrmhide">
                    <div class="form-group hrmhide">
                        <label for="ddlRole"><%= hrmlang.GetString("role") %></label>                        
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName" DataValueField="RoleID"></asp:DropDownList>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlCompetencyType">
                        <%= hrmlang.GetString("competencytype") %></label>                        
                        <asp:DropDownList ID="ddlCompetencyType" runat="server" CssClass="form-control" DataTextField="CompetencyType" DataValueField="CompetencyTypeID"></asp:DropDownList>
                    </div>
                </div>
            </div>          
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtDesc">
                        <%= hrmlang.GetString("description") %></label>
                        <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtDesc"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtRatingDesc1">
                        <%= hrmlang.GetString("evaldesc1") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRatingDesc1"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRatingDesc1" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtRatingDesc2">
                        <%= hrmlang.GetString("evaldesc2") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRatingDesc2"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRatingDesc2" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtRatingDesc3">
                        <%= hrmlang.GetString("evaldesc3") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRatingDesc3"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRatingDesc3" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtRatingDesc4">
                        <%= hrmlang.GetString("evaldesc4") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRatingDesc4"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRatingDesc4" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
           <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="txtRatingDesc5">
                        <%= hrmlang.GetString("evaldesc5") %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRatingDesc5"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRatingDesc5" runat="server" TextMode="MultiLine" CssClass="form-control validate" Height="50px"
                         placeholder="Enter Description"></asp:TextBox>
                    </div>
                </div>
          </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtWeightage">
                        <%= hrmlang.GetString("weightage") %> (%)</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWeightage"
                            ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpWt" runat="server" ControlToValidate="txtWeightage" ErrorMessage="Invalid" CssClass="text-red" Type="Integer"
                             Operator="DataTypeCheck"></asp:CompareValidator>
                        <asp:TextBox ID="txtWeightage" runat="server" CssClass="form-control validate" placeholder="Enter Weightage"></asp:TextBox>
                    </div>
                </div>
          </div>      
            <div class="box-footer">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" CausesValidation="false" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>
