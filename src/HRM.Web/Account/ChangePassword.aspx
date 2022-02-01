<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <section class="content-header">
    <h1>Change Password<small></small></h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Change Password</li>
    </ol>
</section>

<!-- Main content -->
<section class="content">
   <div class="box box-primary">
        <!-- /.box-header -->
        <div class="box-body">
            <div class="accountInfo">
                <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="ChangeUserPasswordValidationGroup"/>
                    <div class="col-md-3">
                    <div class="form-group">
                        <label for="CurrentPassword">Old Password:<asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                            ValidationGroup="ChangeUserPasswordValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            
                    </div>
                    <div class="form-group">
                        <label for="NewPassword">New Password:<asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                                CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                                ValidationGroup="ChangeUserPasswordValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ConfirmNewPassword">Confirm New Password: <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                                ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                ValidationGroup="ChangeUserPasswordValidationGroup" ForeColor="Red">*</asp:CompareValidator></label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                           
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="box-footer">
                    <asp:Button ID="CancelPushButton" runat="server" CssClass="btn btn-primary" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" 
                        onclick="CancelPushButton_Click"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" 
                        CommandName="ChangePassword" Text="Change Password" 
                         ValidationGroup="ChangeUserPasswordValidationGroup" 
                        CssClass="btn btn-primary" onclick="ChangePasswordPushButton_Click"/>
                </div>
                <div class="clearfix"></div>
            </div>
     </div>
        <!-- /.box-body -->
    </div>
    </section>
</asp:Content>