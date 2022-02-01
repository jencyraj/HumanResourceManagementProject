<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddContracts.aspx.cs" Inherits="ManageContracts" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>Contracts<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">Manage Contracts</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="pull-left dblock rowmargin">     
                    <p class="text-red"><asp:Label ID="lblErr" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix"></div>
                 <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="ddlType">Contract Type</label> <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlType"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" DataTextField="ContractTypeName" DataValueField="CTID"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtTitle">Title</label> <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="Required" CssClass="text-red"></asp:RequiredFieldValidator>                        
                            <asp:TextBox ID="txtTitle" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                       
                            <label for="txtDescription">Description</label>                        
                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" style="height:100px"   onblur="textCounter(this,this.form.counter,250);" onkeyup="textCounter(this,this.form.counter,250);"></asp:TextBox>
                        <input  onblur="textCounter(this.form.recipients,this,250);" disabled  onfocus="this.blur();" tabindex="999" maxlength="3" size="3" value="250" name="counter"><span style="color: #808080"  >characters left</span></input>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label for="fpDoc">Select File</label>                        
                           <asp:FileUpload ID="fpDoc" runat="server" /><br />
                           <asp:HyperLink ID="lnkDoc" runat="server" Target="_blank"></asp:HyperLink>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
   </section>
    <script type="text/javascript">
        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit) {
                field.value = field.value.substring(0, maxlimit);
                field.blur();
                field.focus();
                return false;
            } else {
                countfield.value = maxlimit - field.value.length;
            }
        }
    </script>
    
    <asp:HiddenField ID="hfContractId" runat="server" />
</asp:Content>
