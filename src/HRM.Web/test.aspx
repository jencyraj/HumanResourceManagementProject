<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type='text/javascript' 
src='https://ajax.googleapis.com/ajax/libs/prototype/1/prototype.js'></script>
<script type='text/javascript' 
src='https://ajax.googleapis.com/ajax/libs/scriptaculous/1/scriptaculous.js'></script>
<script type='text/javascript' src="js/starbox.js"></script>
<link rel="stylesheet" type="text/css" href="css/starbox.css" />
 

<div id="example_1"><!-- starbox will be inserted here --></div>
<script type='text/javascript'>
    new Starbox('example_1', 5);
</script>
<asp:Calendar ID="calHijriCalendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
 
<SelectedDayStyle BackColor="#333399" ForeColor="White" />
 
<TodayDayStyle BackColor="#CCCCCC" />
 
<OtherMonthDayStyle ForeColor="#999999" />
 
<NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
 
<DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
 
<TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"      Font-Size="12pt" ForeColor="#333399" />
 
</asp:Calendar>
</asp:Content>

