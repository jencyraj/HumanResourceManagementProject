<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddEditPayrollPeriod.aspx.cs" Inherits="AddEditPayrollPeriod" %>

<%@ Register src="~/UserControls/ctlCalendar.ascx" tagname="ctlCalendar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("payrollperiod")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> <%= hrmlang.GetString("home") %></a></li>
            <li class="active"><%= hrmlang.GetString("payrollperiod")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div  class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                       <p class="text-green"><asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="txtTitle"><%= hrmlang.GetString("title") %></label> 
                                <asp:Label ID="lblTitleReq" runat="server" CssClass="text-red" />                        
                                <asp:TextBox ID="txtTitle" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>                
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ctlCalendarBD"><%= hrmlang.GetString("startperiod")%></label>     
                            <uc1:ctlCalendar ID="ctlCalendarSP" runat="server" DefaultCalendarCulture="Grgorian" 
                                MaxYearCountFromNow="50" MinYearCountFromNow="-80" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ctlCalendarBD"><%= hrmlang.GetString("endperiod")%></label>  
                            <uc1:ctlCalendar ID="ctlCalendarEP" runat="server" DefaultCalendarCulture="Grgorian" 
                                MaxYearCountFromNow="50" MinYearCountFromNow="-80" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtValue"><%= hrmlang.GetString("active")%></label>                   
                            <asp:CheckBox ID="chkActive" runat="server" />
                        </div>
                    </div>
                </div> 
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClientClick="return Validate();" OnCommand="btn_Command" CommandName ="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        
        function Validate() {
            if ($('#<%=txtTitle.ClientID%>').val() == '') {
                var Message = $("#<%=hfMessage.ClientID %>").val()
                $('#<%=lblTitleReq.ClientID%>').text(Message);
                return false;
            }
            else {
                $('#<%=lblTitleReq.ClientID%>').text('');
            }
            return true;
        }

    </script>
    <asp:HiddenField ID="hfMessage" runat="server" />
</asp:Content>

