<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigSettings.aspx.cs" Inherits="ConfigSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="bg-black">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>HRM | Settings</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- font Awesome -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
</head>
<body class="bg-black">
    <div class="form-box" id="login-box">
        <div class="header">
            Settings</div>
        <form id="form1" runat="server">
        <div class="body bg-gray">
            <div class="form-group">
                <asp:CheckBoxList ID="chkSettings" runat="server" RepeatColumns="1">
                    <asp:ListItem Text="&nbsp;Show Language Selection" Value="LANG"></asp:ListItem>
                    <asp:ListItem Text="&nbsp;Include Tax in Payroll" Value="TAX"></asp:ListItem>
                     <asp:ListItem Text="&nbsp;Enable DownloadPayslip" Value="DOWNLOADSLIP"></asp:ListItem>
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="footer">
            <asp:Button ID="btnSave" runat="server" CssClass="btn bg-olive btn-block" Text="Save"
                OnClick="btnSave_Click" />
                 <asp:Button ID="btnsignin" runat="server" CssClass="btn bg-olive btn-block" Text="SignIn" OnClick="btnsignin_Click" />
        </div>
      
           
                 <!--OnClick="btnsignin_Click"-->
     
        </form>
    </div>
    <!-- jQuery 2.0.2 -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
