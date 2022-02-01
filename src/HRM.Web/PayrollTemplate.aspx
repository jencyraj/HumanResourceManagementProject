<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PayrollTemplate.aspx.cs" Inherits="PayrollTemplate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">
        function CalculateSalary() {
           
            var dBasic = $('#<%=txtBasicSalary.ClientID%>').val();

            document.getElementById('<%=lblSalary.ClientID%>').innerHTML = "0.00";
            document.getElementById('<%=lblTotAlw.ClientID%>').innerHTML = "0.00";
            document.getElementById('<%=lblTotDed.ClientID%>').innerHTML = "0.00";
          //  document.getElementById('<%=lblMTax.ClientID%>').innerHTML = "0.00";
          //  document.getElementById('<%=lblFTax.ClientID%>').innerHTML = "0.00";
            document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = "0.00";
            document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = "0.00";

            if (isNaN(dBasic) || dBasic == 0) {
                return;
            }
            else {
                var dtaxable = dBasic;

                var totalw = 0;
                var totded = 0;

                document.getElementById('<%=lblSalary.ClientID%>').innerHTML = parseFloat(dBasic).toFixed(2);
                totalw = 0;
                payrollid = "" + document.getElementById('<%=hfPMId.ClientID %>').value;
              
                if (payrollid == "")
                    payrollid = 0;

                if (payrollid > 0) {
                    CalculateSavedSalary(payrollid);
                }
                else {

                    var gv = document.getElementById("<%=gvAllowances.ClientID %>"); //AlW STARTS

                    for (var i = 1; i < gv.rows.length; i++) {

                        Cell1 = gv.rows[i].cells[1];
                        alwamtctrl = Cell1.children[0];

                        Cell2 = gv.rows[i].cells[2];
                        alwtypectrl = Cell2.children[0];

                      //  Cell3 = gv.rows[i].cells[3];
                     //   taxctrl = Cell3.children[0];

                        var dAmt = alwamtctrl.value;
                        var alwtype = alwtypectrl.value;
                     //   var taxable = taxctrl.value;

                        if (alwtype == "P") {
                            if (isNaN(dAmt) == false) {
                                dAmt = dBasic * (dAmt / 100);
                            }
                        }

                       /* if (taxable == "Y")
                            dtaxable = parseFloat(dtaxable) + parseFloat(dAmt);
*/
                        totalw += parseFloat(dAmt);

                    }

                    document.getElementById('<%=lblTotAlw.ClientID%>').innerHTML = parseFloat(totalw).toFixed(2); //AlW ENDS

                    var gvded = document.getElementById("<%=gvDeductions.ClientID %>"); //DED STARTS

                    for (var i = 1; i < gvded.rows.length; i++) {

                        Cell1 = gvded.rows[i].cells[1];
                        alwamtctrl = Cell1.children[0];

                        Cell2 = gvded.rows[i].cells[2];
                        alwtypectrl = Cell2.children[0];

                      /*  Cell3 = gvded.rows[i].cells[3];
                        taxctrl = Cell3.children[0];*/

                        var dAmt = alwamtctrl.value;
                        var alwtype = alwtypectrl.value;
                       // var taxable = taxctrl.value;

                        if (alwtype == "P") {
                            if (isNaN(dAmt) == false) {
                                dAmt = dBasic * (dAmt / 100);
                            }
                        }

                       // if (taxable == "Y")
                       //     dtaxable = parseFloat(dtaxable) - parseFloat(dAmt);

                        totded += parseFloat(dAmt);

                    }
                    document.getElementById('<%=lblTotDed.ClientID%>').innerHTML = parseFloat(totded).toFixed(2); //DED ENDS

                    var dFTax = 0;
                    var dMTax = 0;
                    ///tax rule starts
                  /*  var gvTax = document.getElementById("<%=gvTaxRules.ClientID %>"); //DED STARTS

                    totearnings = parseFloat(dtaxable) * parseFloat(12);

                    for (var i = 1; i < gvTax.rows.length; i++) {

                        Cell1 = gvTax.rows[i].cells[0];
                        var amtrangefrom = parseFloat(Cell1.children[1].value);

                        Cell2 = gvTax.rows[i].cells[1];
                        var amtrangeto = parseFloat(Cell2.children[0].value);

                        Cell3 = gvTax.rows[i].cells[2];
                        var taxpercent = parseFloat(Cell3.children[0].value);

                        Cell4 = gvTax.rows[i].cells[3];
                        var amtexemption = parseFloat(Cell4.children[0].value);

                        Cell5 = gvTax.rows[i].cells[4];
                        var gender = Cell5.children[1].value;

                        if (totearnings >= amtrangefrom && totearnings <= amtrangeto) {
                            if (gender == "F") {
                                dFTax = (parseFloat(totearnings) - parseFloat(amtexemption));
                                dFTax = (dFTax * (taxpercent / 100));
                                dFTax = dFTax / 12;
                            }

                            if (gender == "M") {
                                dMTax = (parseFloat(totearnings) - parseFloat(amtexemption));
                                dMTax = (dMTax * (taxpercent / 100));
                                dMTax = dMTax / 12;
                            }
                        }
                    }*/
                    ///tax rule ends

                 //   document.getElementById('<%=lblFTax.ClientID%>').innerHTML = dFTax.toFixed(2);
                  //  document.getElementById('<%=lblMTax.ClientID%>').innerHTML = dMTax.toFixed(2);
                   // document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded) - parseFloat(dMTax)).toFixed(2);
                    //document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded) - parseFloat(dFTax)).toFixed(2);

                    document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded)).toFixed(2);
                    document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded)).toFixed(2);
                }
            }
        }

        function CalculateSavedSalary(payrollid) {
            var dBasic = $('#<%=txtBasicSalary.ClientID%>').val();
            var dtaxable = dBasic;
            var totalw = 0;
            var totded = 0;

            document.getElementById('<%=lblSalary.ClientID%>').innerHTML = parseFloat(dBasic).toFixed(2);

            if (payrollid > 0) {

                var gv = document.getElementById("<%=gvPayrollAllowance.ClientID %>"); //AlW STARTS

                for (var i = 1; i < gv.rows.length; i++) {

                    Cell1 = gv.rows[i].cells[1];
                    alwamtctrl = Cell1.children[0];

                    Cell2 = gv.rows[i].cells[2];
                    alwtypectrl = Cell2.children[0];

                 /*   Cell3 = gv.rows[i].cells[3];
                    taxctrl = Cell3.children[0];*/

                    var dAmt = alwamtctrl.value;
                    var alwtype = alwtypectrl.value;
                    //var taxable = taxctrl.value;

                    if (alwtype == "P") {
                        if (isNaN(dAmt) == false) {
                            dAmt = dBasic * (dAmt / 100);
                        }
                    }

                  //  if (taxable == "Y")
                   //     dtaxable = parseFloat(dtaxable) + parseFloat(dAmt);

                    totalw += parseFloat(dAmt);

                }

                document.getElementById('<%=lblTotAlw.ClientID%>').innerHTML = parseFloat(totalw).toFixed(2); //AlW ENDS

                var gvded = document.getElementById("<%=gvPayrollDeductions.ClientID %>"); //DED STARTS

                for (var i = 1; i < gvded.rows.length; i++) {

                    Cell1 = gvded.rows[i].cells[1];
                    alwamtctrl = Cell1.children[0];

                    Cell2 = gvded.rows[i].cells[2];
                    alwtypectrl = Cell2.children[0];

                 //   Cell3 = gvded.rows[i].cells[3];
                 //   taxctrl = Cell3.children[0];

                    var dAmt = alwamtctrl.value;
                    var alwtype = alwtypectrl.value;
                  //  var taxable = taxctrl.value;

                    if (alwtype == "P") {
                        if (isNaN(dAmt) == false) {
                            dAmt = dBasic * (dAmt / 100);
                        }
                    }

                 //   if (taxable == "Y")
                //        dtaxable = parseFloat(dtaxable) - parseFloat(dAmt);

                    totded += parseFloat(dAmt);

                }
                document.getElementById('<%=lblTotDed.ClientID%>').innerHTML = parseFloat(totded).toFixed(2); //DED ENDS

                var dTax = 0;
                ///tax rule starts
               /* var gvTax = document.getElementById("<%=gvPayrollTaxrules.ClientID %>"); //DED STARTS

                totearnings = parseFloat(dtaxable) * parseFloat(12);

                for (var i = 1; i < gvTax.rows.length; i++) {

                    Cell1 = gvTax.rows[i].cells[0];
                    var amtrangefrom = parseFloat(Cell1.children[1].value);

                    Cell2 = gvTax.rows[i].cells[1];
                    var amtrangeto = parseFloat(Cell2.children[0].value);

                    Cell3 = gvTax.rows[i].cells[2];
                    var taxpercent = parseFloat(Cell3.children[0].value);

                    Cell4 = gvTax.rows[i].cells[3];
                    var amtexemption = parseFloat(Cell4.children[0].value);

                    Cell5 = gvTax.rows[i].cells[4];
                    var gender = Cell5.children[1].value;

                    var empgender = document.getElementById('<%=hfGender.ClientID%>').value;

                    var dMTax = 0;
                    var dFTax = 0;

                    if (totearnings >= amtrangefrom && totearnings <= amtrangeto) {
                        if (gender == "M") {
                            dMTax = (parseFloat(totearnings) - parseFloat(amtexemption));
                            dMTax = (dMTax * (taxpercent / 100));
                            dMTax = dMTax / 12;
                        }
                        if (gender == "F") {
                            dFTax = (parseFloat(totearnings) - parseFloat(amtexemption));
                            dFTax = (dFTax * (taxpercent / 100));
                            dFTax = dFTax / 12;
                        }
                    }
                }*/
                ///tax rule ends

               // document.getElementById('<%=lblMTax.ClientID%>').innerHTML = "0.00";
                document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = "0.00";
              //  document.getElementById('<%=lblFTax.ClientID%>').innerHTML = "0.00";
                document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = "0.00";


             /*   if (empgender == "M") {
                    document.getElementById('<%=lblMTax.ClientID%>').innerHTML = dMTax.toFixed(2);

                }
                if (empgender == "F") {
                    document.getElementById('<%=lblFTax.ClientID%>').innerHTML = dFTax.toFixed(2);

                }
                document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded) - parseFloat(dMTax)).toFixed(2);
                document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded) - parseFloat(dFTax)).toFixed(2);*/
                document.getElementById('<%=lblMNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded)).toFixed(2);
                document.getElementById('<%=lblFNetSalary.ClientID%>').innerHTML = parseFloat(parseFloat(dBasic) + parseFloat(totalw) - parseFloat(totded)).toFixed(2);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1><%= hrmlang.GetString("payrollstructure")%><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i><%= hrmlang.GetString("home")%></a></li>
            <li class="active"><%= hrmlang.GetString("mpayrollstructure")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="clearfix">
                </div>
                <div class="pull-left dblock rowmargin">
                    <p class="text-red">
                        <asp:Label ID="lblErr" runat="server"></asp:Label></p>
                    <p class="text-green">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></p>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-3">
                    <div class="box-body">
                        <div class="row">
                            <div class="form-group">
                                <label for="txtDesignation">
                                    <%= hrmlang.GetString("designation")%></label>
                                <asp:Label ID="lblDsgnReq" runat="server" CssClass="text-red" />
                                <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                <p class="help-block" style="font-size: 10px; color: red;display:none;">
                                    *Select designation & Press enter to view saved data</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="txtEmployee">
                                    <%= hrmlang.GetString("employee")%></label>
                                <asp:TextBox ID="txtEmployee" runat="server" placeholder="Enter Employee" CssClass="form-control"></asp:TextBox>
                                <p class="help-block" style="font-size: 10px; color: red;display:none;">
                                    *Select employee & Press enter to view saved data</p>
                            </div>
                        </div>
                <div class="row">
                    
                        <div class="form-group">
                            <label for="ddPP"><%= hrmlang.GetString("payrollperiod")%></label><br />                  
                            <asp:DropDownList ID="ddPP" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    
                </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="txtBasicSalary">
                                    <%= hrmlang.GetString("basicsalary")%></label>
                                <asp:Label ID="lblBscSlrReq" runat="server" CssClass="text-red" />
                                <asp:TextBox ID="txtBasicSalary" runat="server" placeholder="Enter Basic Salary"
                                    CssClass="form-control decimalentry" onchange="CalculateSalary();"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4 dvright">
                    <div class="box box-primary">
                        <div data-original-title="Header tooltip" class="box-header" data-toggle="tooltip"
                            title="">
                            <h3 class="box-title fr-left">
                                <%= hrmlang.GetString("summary")%></h3>
                        </div>
                        <div class="box-body" style="padding-left: 30px;">
                            <div class="form-group">
                                <span class="fr-left spanwd"><%= hrmlang.GetString("basicsalary")%> :</span> <span id="lblSalary" runat="server"
                                    class="col-md-2 spanwd2">0.00</span>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <span class="fr-left spanwd"><%= hrmlang.GetString("tallowances")%> :</span> <span id="lblTotAlw" runat="server"
                                    class="col-md-2 spanwd2">0.00</span>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <span class="fr-left spanwd"><%= hrmlang.GetString("tdeductions")%> :</span> <span id="lblTotDed" runat="server"
                                    class="col-md-2 spanwd2">0.00</span>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group hrmhide">
                                <span class="fr-left spanwd"><%= hrmlang.GetString("tax")%> :</span> <span id="lblFTax" runat="server"
                                    class="col-md-2 spanwd2">0.00</span><span class="spanwd2" style="width: 5px"> /
                                </span><span id="lblMTax" runat="server" class="col-md-2 spanwd2" style="display:none">0.00</span>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <span class="fr-left spanwd"><%= hrmlang.GetString("netsalary")%> :</span> <span id="lblFNetSalary" runat="server"
                                    class="col-md-2 spanwd2">0.00</span><span class="spanwd2" style="width: 5px">
                                </span><span id="lblMNetSalary" runat="server" class="col-md-2 spanwd2" style="display:none">0.00</span>
                            </div>
                        </div>
                        <p class="help-block" style="font-size: 10px; padding-right: 10px; text-align: right;color: red">
                            <%= hrmlang.GetString("bcextra") %></p>
                        <div class="clearfix">
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <div class="clearfix">
                </div>
                <div class="box-body">
                    <div class="col-mg-12 rowmargin">
                        <label id="lblAllowances" runat="server" for="gvAllowances">
                            <%= hrmlang.GetString("allowances") %></label>
                        <asp:GridView ID="gvAllowances" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            DataKeyNames="AlwId" EnableViewState="True" AllowPaging="false" ShowHeaderWhenEmpty="true"
                            OnRowDataBound="gvAllowances_RowDataBound" Width="75%">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfAlwId" runat="server" Value='<%# Eval("AlwId") %>' />
                                        <asp:HiddenField ID="hfAlwCode" runat="server" Value='<%# Eval("AlwCode") %>' />
                                        <asp:Label ID="lblAlwName" runat="server" Text='<%# Eval("AllowanceName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAlwAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("AlwAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlAlwType" runat="server" CssClass="form-control" onchange="CalculateSalary()">                                           
                                            <asp:ListItem Text="Amount" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Percentage" Value="P"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfAlwType" runat="server" Value='<%# Eval("AlwType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfTaxable" runat="server" Value='<%# Eval("Taxable") %>' />
                                        <asp:DropDownList ID="ddlTaxable" runat="server" CssClass="form-control" onchange="CalculateSalary()">                                                                                       
                                            <asp:ListItem Text="Yes" Selected="True" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <label id="lblPayrollAllowance" runat="server" for="gvPayrollAllowance">
                            <%= hrmlang.GetString("allowances") %></label>
                        <asp:GridView ID="gvPayrollAllowance" runat="Server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped dataTable" DataKeyNames="PAId" EnableViewState="True"
                            AllowPaging="false" ShowHeaderWhenEmpty="true" OnRowDataBound="gvPayrollAllowance_RowDataBound"
                            Width="75%">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPRPAId" runat="server" Value='<%# Eval("PAId") %>' />
                                        <asp:HiddenField ID="hfPRPMId" runat="server" Value='<%# Eval("PMId") %>' />
                                        <asp:HiddenField ID="hfPRAlwId" runat="server" Value='<%# Eval("AllowanceId") %>' />
                                        <asp:HiddenField ID="hfPRAlwCode" runat="server" Value='<%# Eval("AlwCode") %>' />
                                        <asp:Label ID="lblPRAlwName" runat="server" Text='<%# Eval("AlwName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRAlwAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("AlwAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPRAlwType" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Amount" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Percentage" Value="P"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfPRAlwType" runat="server" Value='<%# Eval("AlwType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPRTaxable" runat="server" Value='<%# Eval("Taxable") %>' />
                                        <asp:DropDownList ID="ddlPRTaxable" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Yes" Selected="True" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="clearfix"></div>
                        <div class="form-group" style="margin-top:10px;">
                        <label><%= hrmlang.GetString("gosi") %> : </label>
                        <asp:DropDownList ID="ddlGosiAlw" runat="server" CssClass="form-control" Width="150px" style="display:inline" ></asp:DropDownList>
                        <asp:TextBox ID="txtGOSI" runat="server" CssClass="form-control" Width="150px" style="display:inline" ></asp:TextBox>
                        <asp:DropDownList ID="ddlgosi" runat="server" CssClass="form-control" Width="150px" style="display:inline" ></asp:DropDownList>
                        <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtGOSI" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-mg-12 rowmargin">
                        <label id="lblDeductions" runat="server" for="lblDeductions">
                            <%= hrmlang.GetString("deductions")%></label>
                        <asp:GridView ID="gvDeductions" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            DataKeyNames="DedID" EnableViewState="True" AllowPaging="false" ShowHeaderWhenEmpty="true"
                            OnRowDataBound="gvDeductions_RowDataBound" Width="75%">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfDedId" runat="server" Value='<%# Eval("DedId") %>' />
                                        <asp:HiddenField ID="hfDedCode" runat="server" Value='<%# Eval("DedCode") %>' />
                                        <asp:Label ID="lblDedName" runat="server" Text='<%# Eval("DeductionName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDedAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("DedAmount") %>'  onchange="CalculateSalary()"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList Width="200px" ID="ddlDedType" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Amount" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Percentage" Value="P"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfDedType" runat="server" Value='<%# Eval("DedType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Exemption" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTaxExemption" runat="server" onchange="CalculateSalary()" />
                                        <asp:HiddenField ID="hfTaxExemption" runat="server" Value='<%# Eval("TaxExemption") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <label id="lblPayrollDeductions" runat="server" for="gvPayrollDeductions">
                             <%= hrmlang.GetString("deductions")%></label>
                        <asp:GridView ID="gvPayrollDeductions" runat="Server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped dataTable" DataKeyNames="PDId" EnableViewState="True"
                            AllowPaging="false" ShowHeaderWhenEmpty="true" OnRowDataBound="gvPayrollDeductions_RowDataBound"
                            Width="75%">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPRPDId" runat="server" Value='<%# Eval("PDId") %>' />
                                        <asp:HiddenField ID="hfPRPMId" runat="server" Value='<%# Eval("PMId") %>' />
                                        <asp:HiddenField ID="hfPRDedId" runat="server" Value='<%# Eval("DeductionId") %>' />
                                        <asp:HiddenField ID="hfPRDedCode" runat="server" Value='<%# Eval("DedCode") %>' />
                                        <asp:Label ID="lblPRDedName" runat="server" Text='<%# Eval("DedName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRDedAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("DedAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList Width="200px" ID="ddlPRDedType" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Amount" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Percentage" Value="P"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfPRDedType" runat="server" Value='<%# Eval("DedType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Exemption" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPRTaxExemption" runat="server" onchange="CalculateSalary()" />
                                        <asp:HiddenField ID="hfPRTaxExemption" runat="server" Value='<%# Eval("TaxExemption") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="box-body hrmhide">
                    <div class="col-mg-12 rowmargin">
                        <label id="lblTaxRules" runat="server" for="gvTaxRules" Visible="false">
                             <%= hrmlang.GetString("taxrules")%></label>
                        <asp:GridView ID="gvTaxRules" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            DataKeyNames="TaxRuleId" EnableViewState="True" AllowPaging="false" ShowHeaderWhenEmpty="true"
                            OnRowDataBound="gvTaxRules_RowDataBound" Width="75%" Visible="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Salary From">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfTaxRuleId" runat="server" Value='<%# Eval("TaxRuleId") %>' />
                                        <asp:TextBox ID="txtSalaryFrom" CssClass="decimalentry" runat="server" Text='<%# Eval("SalaryFrom") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSalaryTo" CssClass="decimalentry" runat="server" Text='<%# Eval("SalaryTo") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Percentage">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTaxPercentage" CssClass="decimalentry" runat="server" Text='<%# Eval("TaxPercentage") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exempted Tax amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExemptedTaxAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("ExemptedTaxAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Tax Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdditionalTaxAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("AdditionalTaxAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfGender" runat="server" Value='<%# Eval("Gender") %>' />
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <label id="lblPayrollTaxrules" runat="server" for="gvPayrollTaxrules" visible="false">
                             <%= hrmlang.GetString("taxrules")%></label>
                        <asp:GridView ID="gvPayrollTaxrules" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                            DataKeyNames="PTId" EnableViewState="True" AllowPaging="false" ShowHeaderWhenEmpty="true" Visible="false"
                            OnRowDataBound="gvPayrollTaxrules_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Salary From">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPRPTId" runat="server" Value='<%# Eval("PTId") %>' />
                                        <asp:HiddenField ID="hfPRPMId" runat="server" Value='<%# Eval("PMId") %>' />
                                        <asp:HiddenField ID="hfPRTaxRuleId" runat="server" Value='<%# Eval("TaxRuleId") %>' />
                                        <asp:TextBox ID="txtPRSalaryFrom" CssClass="decimalentry" runat="server" Text='<%# Eval("SalaryFrom") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRSalaryTo" CssClass="decimalentry" runat="server" Text='<%# Eval("SalaryTo") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Percentage">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRTaxPercentage" CssClass="decimalentry" runat="server" Text='<%# Eval("TaxPercentage") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Exempted Tax amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRExemptedTaxAmount" CssClass="decimalentry" runat="server" Text='<%# Eval("ExemptedTaxAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Tax Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPRAdditionalTaxAmount" CssClass="decimalentry" runat="server"
                                            Text='<%# Eval("AdditionalTaxAmount") %>' onchange="CalculateSalary()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPRGender" runat="server" Value='<%# Eval("Gender") %>' />
                                        <asp:DropDownList ID="ddlPRGender" runat="server" CssClass="form-control" onchange="CalculateSalary()">
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" OnClientClick="return Validate();" CssClass="btn btn-primary btn-sm"
                        CausesValidation="false" Text="Save" OnCommand="btn_Command" CommandName="SAVE" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                        Text="Cancel" OnCommand="btn_Command" CommandName="CANCEL" />
                    <asp:Button ID="btnSearch" runat="server" Visible="false" CausesValidation="false"
                        OnCommand="btn_Command" CommandName="SEARCH" />
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </section>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=txtDesignation.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/ajaxservice.asmx/GetDesignations") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfDesignationId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });

            $("#<%=txtEmployee.ClientID %>").autocomplete({
                source: function (request, response) {
                    var DesignationId = $("#<%=hfDesignationId.ClientID%>").val();
                    if (DesignationId == '') {
                        $.ajax({
                            url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployees") %>',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }
                    else {
                        $.ajax({
                            url: '<%=ResolveUrl("~/ajaxservice.asmx/GetEmployeesByDesignation") %>',
                            data: "{'prefix': '" + request.term + "', 'DesignationId': '" + DesignationId + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }
                },
                select: function (e, i) {
                    $("#<%=hfEmployeeId.ClientID %>").val(i.item.val);
                },
                minLength: 1
            });

            $('.decimalentry').keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

          /*  $('#<%=txtDesignation.ClientID%>').keydown(function (e) {
                if (e.keyCode == 13) {
                    <%= Page.ClientScript.GetPostBackEventReference(btnSearch, "")%>
                }
            });

            $('#<%=txtEmployee.ClientID%>').keydown(function (e) {
                if (e.keyCode == 13) {
                    <%= Page.ClientScript.GetPostBackEventReference(btnSearch, "")%>
                }
            });
*/
        });

        function Validate()
        {
            if ($('#<%=txtDesignation.ClientID%>').val() == '') {
                $('#<%=lblDsgnReq.ClientID%>').text('Required');
                return false;
            }
            else {
                $('#<%=lblDsgnReq.ClientID%>').text('');
            }
            if ($('#<%=txtBasicSalary.ClientID%>').val() == '') {
                $('#<%=lblBscSlrReq.ClientID%>').text('Required');
                return false;
            }
            else {
                $('#<%=lblBscSlrReq.ClientID%>').text('');
            }
            return true;
        }

    </script>
    <asp:HiddenField ID="hfDesignationId" runat="server" />
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <asp:HiddenField ID="hfPMId" runat="server" />
    <asp:HiddenField ID="hfGender" runat="server" />
</asp:Content>
