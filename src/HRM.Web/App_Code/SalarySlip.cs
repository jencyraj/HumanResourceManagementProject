using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

using System.Net;
using System.Net.Mail;

using HRM.BAL;
using HRM.BOL;

using iTextSharp.text;
using iTextSharp.text.pdf;

using Ionic.Zip;
using System.Web.SessionState;
/// <summary>
/// Summary description for SalarySlip
/// </summary>
public class SalarySlip
{
    StringBuilder htmlTable = new StringBuilder(); int EmpID = 0; static int sMonth = 0; static int sYear = 0; static decimal minLeaveCount = 0; static decimal reghourrate = 0;
    decimal dMissedDays = 0;
    decimal dMissedDaysAmount = 0;
    DataTable DTTemp = new DataTable();

    public SalarySlip()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable SalarySlipData(string EmplIDs, int nMonth, int nYear)
    {
        DataTable dTSalary = new DataTable();
        dTSalary.Columns.Add("EmployeeID");
        dTSalary.Columns.Add("BASIC");
        dTSalary.Columns.Add("Allowance");
        dTSalary.Columns.Add("Deduction");
        dTSalary.Columns.Add("Bonus");
        dTSalary.Columns.Add("Commission");
        dTSalary.Columns.Add("OverTime");
        dTSalary.Columns.Add("LOP");

        sMonth = nMonth;
        sYear = nYear;

        string[] EID = EmplIDs.Split(',');

        for (int ix = 0; ix < EID.Length; ix++)
        {
            int EmployeeID = Util.ToInt(EID[ix]);

            DataRow dRow = dTSalary.NewRow();
            dRow["EmployeeID"] = EID[ix];
            PayrollSalaryslipBOL objM = new PayrollSalaryslipBOL();
            EmployeeBOL objBOL = new EmployeeBOL();
            objBOL.EmployeeID = EmployeeID;
            objM.EmployeeId = EmployeeID;
            objM.Month = sMonth;
            objM.Year = sYear;
            objBOL = new EmployeeBAL().Select(objBOL);//GET EMPLOYEE DETAILS
            
            PayrollMasterBOL objMaster = new PayrollMasterBOL();
            objMaster.EmployeeId = objBOL.EmployeeID;
            objMaster.DesignationId = objBOL.DesgnID;
           // HttpSessionState Session = HttpContext.Current.Session;
            ajaxservice cs = new ajaxservice();
            objMaster.BranchID = cs.GetBranchID();
           // objMaster.BranchID = Util.ToInt(System.Web.HttpContext.Current.Session("BRANCHID"));
            DataSet dSet = new PayrollTemplateBAL().PayrollTemplateforSalarySlip(objMaster, sMonth, sYear);
           if(dSet.Tables.Count>0)
            if(dSet.Tables[0].Rows.Count>0)
            objMaster.PMId=Util.ToInt(dSet.Tables[0].Rows[0]["PMID"].ToString());
            LeaveBAL objBAL = new LeaveBAL();
            LeaveBOL objBonus = new LeaveBOL();
            objBonus.EmployeeID = EmpID = EmployeeID;
            objBonus.Month = sMonth;
            objBonus.Year = sYear;

            dRow["Allowance"] = 0;
            dRow["Deduction"] = 0;
            dRow["Commission"] = 0;
            dRow["Bonus"] = 0;
            dRow["OverTime"] = 0;
            dRow["LOP"] = 0;

            if (dSet != null)
            {
                if (dSet.Tables.Count > 0)
                {
                    DataTable dT = objBAL.SalaryslipGenerate(objBonus);
                    if (dT.Rows.Count > 0)
                    {
                        DataTable DTTemp = Check_SalarySlip_Exist(objM);
                        DataTable dtAllowances = new DataTable();
                        DataTable dtDeductions = new DataTable();
                        decimal dBasic = 0;
                        if (DTTemp.Rows.Count == 0)
                        {
                            dtAllowances = dSet.Tables[1];
                            dtDeductions = dSet.Tables[2];
                            dBasic = Util.ToDecimal(dT.Rows[0]["BasicSalary"]);
                        }
                        else
                        {
                            DataSet DS = new PayrollTemplateBAL().PayrollSalarySlip(objMaster,sMonth, sYear);
                            dtAllowances = DS.Tables[1];
                            dtDeductions = DS.Tables[2];
                            dBasic = Util.ToDecimal(DTTemp.Rows[0]["BasicSalary"].ToString());
                        }
                        decimal LP_leaveRule = GetLateValue();
                        decimal dOver = GetOverTime();
                        decimal dDED = GetDeduction(); //Leave type marked as deductible
                        decimal dDedE = GetLeaveTakenAsLop_Absence();
                        decimal dAllw = 0;

                        int gosialwtype = Util.ToInt(dT.Rows[0]["gosi_alw_type"]);
                        decimal gosialwtypeamount = 0;

                        for (int i = 0; i < dtAllowances.Rows.Count; i++)
                        {
                            decimal dAmt = 0;

                            DataRow dR = dtAllowances.Rows[i];
                            dAmt = Util.ToDecimal(dR["AlwAmount"]);
                            if ("" + dR["AlwType"] != "A")
                                dAmt = (dBasic * dAmt) / 100;

                            dAllw += Math.Round(dAmt, 2);

                            if (gosialwtype > 0 && Util.ToInt(dR["AllowanceID"]) == gosialwtype)
                                gosialwtypeamount = dAmt;
                        }

                        if (gosialwtypeamount > 0)
                        {
                            if ("" + dT.Rows[0]["gosiType"] != "A")
                            {
                                gosialwtypeamount = ((dBasic + gosialwtypeamount) * Util.ToDecimal(dT.Rows[0]["gosiamount"])) / 100;
                            }
                            else
                                gosialwtypeamount = Util.ToDecimal(dT.Rows[0]["gosiamount"]);

                            dAllw = dAllw + Math.Round(gosialwtypeamount, 2);
                        }

                        dRow["BASIC"] = dBasic;
                        dRow["Allowance"] = dAllw;
                        dRow["Commission"] = "" + dT.Rows[0]["Commission"];
                        dRow["Bonus"] = "" + dT.Rows[0]["Bonus"];
                        dRow["OverTime"] = dOver;

                        decimal dDed = 0;
                        for (int i = 0; i < dtDeductions.Rows.Count; i++)
                        {

                            if (dtDeductions.Rows.Count > i)
                            {
                                DataRow dR = dtDeductions.Rows[i];

                                decimal dDedAmt = Util.ToDecimal("" + dR["DedAmount"]);
                                if ("" + dR["DedType"] != "A")
                                    dDedAmt = (dBasic * dDedAmt) / 100;

                                dDed += Math.Round(dDedAmt, 2);
                            }
                        }
                        dRow["Deduction"] = dDed;
                        dRow["LOP"] = LP_leaveRule + dDED + dDedE;
                    }
                }
            }
            dTSalary.Rows.Add(dRow);
        }

        return dTSalary;
    }

    public string CreateSlip(string sPath, string EmplIDs, int nMonth, int nYear, bool SendMail, out string sEmp)
    {
        sEmp = "";
        sMonth = nMonth;
        sYear = nYear;

        try
        {
            string[] EID = EmplIDs.Split(',');

            for (int i = 0; i < EID.Length; i++)
            {

                LeaveBAL objBAL = new LeaveBAL();
                LeaveBOL objBonus = new LeaveBOL();

                int EmployeeID = Util.ToInt(EID[i]);
                objBonus.EmployeeID = EmployeeID;
                objBonus.Month = sMonth;
                objBonus.Year = sYear;

                EmployeeBOL objBOL = new EmployeeBOL();
                objBOL.EmployeeID = EmployeeID;
                objBOL = new EmployeeBAL().Select(objBOL);//GET EMPLOYEE DETAILS

                OrganisationBOL objOrg = new OrganisationBAL().Select();
                if (objBOL.EmployeeID > 0)
                {
                    PayrollMasterBOL objMaster = new PayrollMasterBOL();
                    objMaster.EmployeeId = objBOL.EmployeeID;
                    objMaster.DesignationId = objBOL.DesgnID;
                    DataSet dSet = new PayrollTemplateBAL().PayrollTemplateforSalarySlip(objMaster, sMonth, sYear);

                    string sName = objBOL.FirstName + ((objBOL.MiddleName.Trim() != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName.Trim() != "") ? " " + objBOL.LastName : "");

                    if (dSet == null || dSet.Tables.Count == 0)
                    {
                        sEmp = (sEmp == "") ? sName : "," + sName;
                        continue;
                    }

                    if (dSet.Tables[0].Rows.Count == 0)
                    {
                        sEmp = (sEmp == "") ? sName : "," + sName;
                        continue;
                    }
                }

                DataTable dT = objBAL.SalaryslipGenerate(objBonus);

                PayrollMasterBOL objM = new PayrollMasterBOL();
                objM.EmployeeId = objBonus.EmployeeID;
                objM.DesignationId = objBOL.DesgnID;
                PayrollSalaryslipBOL objP = new PayrollSalaryslipBOL();
                objP.EmployeeId = EmployeeID;
                objP.Month = sMonth;
                objP.Year = sYear;
                DataSet dtSet = new PayrollTemplateBAL().PayrollTemplateforSalarySlip(objM, objBonus.Month, objBonus.Year);
                DataTable DTTemp = Check_SalarySlip_Exist(objP);

                //if (DTTemp.Rows.Count == 0)
                //   // HTML(sPath, dT, dtSet, SendMail, objBOL);
                //else
                    TestHTML(sPath, dT, dtSet, SendMail, objBOL);

            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "SUCCESS";
    }
    private DataTable Check_SalarySlip_Exist(PayrollSalaryslipBOL objM)
    {
        return new PayrollTemplateBAL().Check_Salaryslip(objM);
    }


//    public void HTML(string sFilePath, DataTable DT, DataSet DS, bool SendMail, EmployeeBOL objBOL)
//    {
//        EmpID = Util.ToInt(DT.Rows[0]["EmployeeID"]);
//        string sText = hrmlang.GetString("salarysheet");
//        // DataTable LOP = GetLateValue(); 
//        PayrollSalaryslipBOL objM = new PayrollSalaryslipBOL();
//        objM.EmployeeId = EmpID;
//        decimal LP_leaveRule = GetLateValue();// LEAVE RULES

//        /*if (LOP.Rows.Count > 0)
//            LP_leaveRule = Util.ToDecimal(LOP.Rows[0]["LOP"].ToString());*/

//        DataTable dtAllowances = DS.Tables[1];
//        DataTable dtDeductions = DS.Tables[2];
//        DataTable dtTax = DS.Tables[3];
//        DataTable dtmon = DS.Tables[4];
//        DataTable dtyr = DS.Tables[5];
//        string M = dtmon.Rows[0]["MonthName"].ToString();
//        string date = M + " " + dtyr.Rows[0]["Nyear"].ToString() + " " + sText;
//        int Month = Util.ToInt(DS.Tables[6].Rows[0]["MonthNO"].ToString());
//        objM.Month = Month;
//        objM.Year = Util.ToInt(dtyr.Rows[0]["Nyear"].ToString());
//        DataTable dtyear = DS.Tables[5];
//        OrganisationBOL objOrg = new OrganisationBAL().Select();
//        string Cname = objOrg.CompanyName;
//        decimal dOver = GetOverTime();
//        decimal dDED = GetDeduction(); //Leave type marked as deductible
//        decimal dDedE = GetLeaveTakenAsLop_Absence();
//        decimal LOP = Get_LOP_WrkngHour();
//        // dDedE = LOP + dDedE;
//        dDedE = LOP + dDedE;
//        htmlTable.Append("<table border=\"1\" style=\"border: solid 1px Silver;font-size: 10px;\">");

//        int ncount = dtAllowances.Rows.Count + 1;

//        int totalcolumns = 4 + ((ncount > dtDeductions.Rows.Count) ? ncount : dtDeductions.Rows.Count);

//        if (DT.Rows.Count > 0)
//        {
//            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + Cname + " " + sText + "</td></tr>");

//            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + date + "</td></tr>");

//            htmlTable.Append("<tr><td>Staff Name</td><td colspan=\"" + (totalcolumns - 1).ToString() + "\">" + DT.Rows[0]["FULLNAME"] + "</td></tr>");


//            htmlTable.Append("<tr><td>Staff Acc NO</td><td colspan=\"2\">" + DT.Rows[0]["AccountNumber"] + "</td><td>Dept Name</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DT.Rows[0]["DepartmentName"] + "</td></tr>");
//            htmlTable.Append("<tr><td>Position</td><td colspan=\"2\">" + DT.Rows[0]["Designation"] + "</td><td>Bank Name</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DT.Rows[0]["BankName"] + "</td></tr>");
//            htmlTable.Append("<tr><td>Basic Salary</td><td>Bonus</td>");


//            for (int i = 0; i < dtAllowances.Rows.Count; i++)
//            {

//                if (dtAllowances.Rows.Count > i)
//                {
//                    DataRow dRow = dtAllowances.Rows[i];

//                    htmlTable.Append("<td>" + dRow["AlwName"] + "</td>");
//                }
//            }
//            htmlTable.Append("<td>GOSI</td><td>Other</td><td>Salary Total</td></tr>");

//            htmlTable.Append("<tr><td>" + DT.Rows[0]["BasicSalary"] + "</td><td>" + DT.Rows[0]["Bonus"] + "</td>");
//            decimal dBS = Util.ToDecimal(DT.Rows[0]["BasicSalary"]);

//            decimal dBO = Util.ToDecimal(DT.Rows[0]["Bonus"]);
//            decimal dAllw = 0;
//            decimal taxamount = 0;
//            decimal Netsal = 0;
//            decimal totalD = 0;
//            decimal dBasic = Util.ToDecimal(DT.Rows[0]["BasicSalary"]);
//            objM.BasicSalary = dBasic;
//            decimal dTaxable = dBasic;
//            int gosialwtype = Util.ToInt(DT.Rows[0]["gosi_alw_type"]);
//            decimal gosialwtypeamount = 0;

//            for (int i = 0; i < dtAllowances.Rows.Count; i++)
//            {
//                decimal dAmt = 0;

//                DataRow dRow = dtAllowances.Rows[i];
//                dAmt = Util.ToDecimal(dRow["AlwAmount"]);
//                if ("" + dRow["AlwType"] != "A")
//                    dAmt = (dBasic * dAmt) / 100;
//                if ("" + dRow["Taxable"] == "Y")
//                    dTaxable = dTaxable + dAmt;
//                htmlTable.Append("<td>" + Math.Round(dAmt, 2).ToString() + "</td>");
//                dAllw += Math.Round(dAmt, 2);

//                if (gosialwtype > 0 && Util.ToInt(dRow["AllowanceID"]) == gosialwtype)
//                    gosialwtypeamount = dAmt;
//            }
//            objM.Allowance = dAllw;
//            if (gosialwtypeamount > 0)
//            {
//                if ("" + DT.Rows[0]["gosiType"] != "A")
//                {
//                    gosialwtypeamount = ((dBasic + gosialwtypeamount) * Util.ToDecimal(DT.Rows[0]["gosiamount"])) / 100;
//                }
//                else
//                    gosialwtypeamount = Util.ToDecimal(DT.Rows[0]["gosiamount"]);

//                dAllw = dAllw + Math.Round(gosialwtypeamount, 2);
//            }

//            dOver += Util.ToDecimal(DT.Rows[0]["Commission"]);
//            decimal totsal = dBS + dBO + dAllw + Math.Round(dOver, 2);
//            htmlTable.Append("<td>" + Math.Round(gosialwtypeamount, 2) + "</td><td>" + Math.Round(dOver, 2) + "</td><td>" + totsal + "</td></tr>");

//            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count + 1 - dtDeductions.Rows.Count + 2).ToString() + "\">" + "Absence" + "</td>");
//            for (int i = 0; i < dtDeductions.Rows.Count; i++)
//            {
//                if (dtDeductions.Rows.Count > i)
//                {
//                    DataRow dRow = dtDeductions.Rows[i];

//                    htmlTable.Append("<td>" + dRow["DedName"] + "</td>");

//                }
//            }

//            htmlTable.Append("<td>" + "Other" + "</td><td>" + "Total Deductions" + "</td></tr>");
//            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count + 1 - dtDeductions.Rows.Count + 2).ToString() + "\">" + (dDED + dDedE + dMissedDaysAmount).ToString() + "</td>");
//            decimal dDed = 0;
//            for (int i = 0; i < dtDeductions.Rows.Count; i++)
//            {

//                DataRow dRow = dtDeductions.Rows[i];

//                decimal dDedAmt = Util.ToDecimal("" + dRow["DedAmount"]);
//                if ("" + dRow["DedType"] != "A")
//                    dDedAmt = (dBasic * dDedAmt) / 100;
//                if ("" + dRow["TaxExemption"] == "Y")
//                    dTaxable = dTaxable - dDedAmt;
//                htmlTable.Append("<td>" + Math.Round(dDedAmt, 2).ToString() + "</td>");
//                dDed += Math.Round(dDedAmt, 2);

//            }
//            objM.Deduction = dDed;
//            totalD = dDED + dDed + dDedE + LP_leaveRule;
//            if ("" + System.Web.HttpContext.Current.Session["USETAX"] != "")
//            {
//                taxamount = CalculateTaxAmount(dtTax, dTaxable * 12, objBOL.Gender);
//                Netsal = Math.Abs(totsal) - (Math.Abs(totalD) + Math.Abs(taxamount));
//                objM.Tax = taxamount;
//            }
//            else
//            {

//                Netsal = Math.Abs(totsal) - Math.Abs(totalD);
//            }
//            htmlTable.Append("<td>" + LP_leaveRule + "</td><td>" + totalD + "</td></tr>");
//            htmlTable.Append("<tr><td>" + "Signature" + "</td><td colspan=\"2\"></td><td bgcolor=\"#AAAAAA\" colspan=\"" + (totalcolumns - 5).ToString() + "\"></td><td align=\"right\">NET PAID</td><td>" + Netsal + "</td></tr>");
//            htmlTable.Append("<tr><td colspan=\"4\">&nbsp;</td><td align=\"right\">Date</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DateTime.Now.ToString() + "</td></tr>");
//            htmlTable.Append("</body>");
//            htmlTable.Append("</table>");

//            //  PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
//            // ltData.Text = htmlTable.ToString();

//            string sExtn = ".pdf";

//            if (!System.IO.Directory.Exists(sFilePath))
//                System.IO.Directory.CreateDirectory(sFilePath);

//            FileStream fs = new FileStream(sFilePath + "salary_" + DT.Rows[0]["EmpCode"] + "_" + DT.Rows[0]["FirstName"] + "_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + sExtn, FileMode.Create, FileAccess.Write, FileShare.None);
//            Document doc = new Document();
//            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
//            doc.Open();

//            string sHtml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
//                 <!DOCTYPE html 
//                     PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
//                    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
//                 <html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
//                    <head>
//                        <title></title>
//                    </head>
//                  <body>
//                    " + htmlTable.ToString() + "</body></html>";

//            /*  using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
//              {

//                  //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
//                  using (var sr = new StringReader(htmlTable.ToString()))
//                  {
//                      //Parse the HTML
//                      htmlWorker.Parse(sr);
//                  }
//              }*/

//            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(sHtml), null);
//            for (int k = 0; k < htmlarraylist.Count; k++)
//            {
//                doc.Add((IElement)htmlarraylist[k]);
//            }


//            doc.Close();
//            fs.Close();
//            if (SendMail)
//            {
//                EmailBAL objBAL = new EmailBAL();
//                EmailBOL objEmail = new EmailBOL();

//                objEmail = objBAL.Select();
//                SendEmailToEmployee(objEmail, objBOL, sFilePath);
//            }
//        }
//    }
    private void BindList(DataTable DT,int SID, string Type)
    {
        int CVString;
        PayrollSalaryslipBOL objM = new PayrollSalaryslipBOL();
        objM.Id = SID;
        if (Type == "A")
        {
            for (int j = 0; j < DT.Rows.Count; j++)
            {

                objM.AllowanceID = Util.ToInt(DT.Rows[j]["AllowanceId"].ToString());
                objM.ATax = DT.Rows[j]["Taxable"].ToString();
                objM.AllowanceType = DT.Rows[j]["AlwType"].ToString();
                objM.AllowanceAmount = Util.ToDecimal(DT.Rows[j]["AlwAmount"].ToString());
                new PayrollTemplateBAL().SavePayrollPayslipAllowance(objM);
            }
        }
        if (Type == "D")
        {
            for (int j = 0; j < DT.Rows.Count; j++)
            {
                objM.DeductionID = Util.ToInt(DT.Rows[j]["DeductionId"].ToString());
                objM.DTax = DT.Rows[j]["TaxExemption"].ToString();
                objM.DeductionType = DT.Rows[j]["DedType"].ToString();
                objM.DeductionAmount = Util.ToDecimal(DT.Rows[j]["DedAmount"].ToString());
                new PayrollTemplateBAL().SavePayrollPayslipDeduction(objM);
            }
        }
       
    }
    private DataTable GetTableSlip()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ID");
        dt.Columns.Add("PMID");
        dt.Columns.Add("EmployeeID");
        dt.Columns.Add("BasicSalary");
        dt.Columns.Add("Allowance");
        dt.Columns.Add("Deduction");
        dt.Columns.Add("Tax");
        dt.Columns.Add("Month");
        dt.Columns.Add("Year");
        return dt;
    }
    public void TestHTML(string sFilePath, DataTable DT, DataSet DS, bool SendMail, EmployeeBOL objBOL)
    {
        DataRow dRows = GetTableSlip().NewRow();

        EmpID = Util.ToInt(DT.Rows[0]["EmployeeID"]);
        dRows["EmployeeID"] = EmpID;
        string sText = hrmlang.GetString("salarysheet");
        // DataTable LOP = GetLateValue(); 
        PayrollSalaryslipBOL objM = new PayrollSalaryslipBOL();
        objM.EmployeeId = EmpID;
        decimal LP_leaveRule = GetLateValue();// LEAVE RULES

        /*if (LOP.Rows.Count > 0)
            LP_leaveRule = Util.ToDecimal(LOP.Rows[0]["LOP"].ToString());*/
      
       
        objM.BasicSalary =Util.ToDecimal(DT.Rows[0]["BasicSalary"]);
        DataTable dtAllowances = DS.Tables[1];

        DataTable dtDeductions = DS.Tables[2];
        DataTable dtTax = DS.Tables[3];
        DataTable dtmon = DS.Tables[4];
        DataTable dtyr = DS.Tables[5];
        string M = dtmon.Rows[0]["MonthName"].ToString();
        string date = M + " " + dtyr.Rows[0]["Nyear"].ToString() + " " + sText;
        int Month = Util.ToInt(DS.Tables[6].Rows[0]["MonthNO"].ToString());
        objM.Month = Month;
        objM.Year = Util.ToInt(dtyr.Rows[0]["Nyear"].ToString());
        DataTable dtyear = DS.Tables[5];
        OrganisationBOL objOrg = new OrganisationBAL().Select();
        string Cname = objOrg.CompanyName;
        decimal dOver = GetOverTime();
        decimal dDED = GetDeduction(); //Leave type marked as deductible
        decimal dDedE = GetLeaveTakenAsLop_Absence();
        decimal LOP = Get_LOP_WrkngHour();
        int PMID = Util.ToInt(dtAllowances.Rows[0]["PMID"].ToString());
        int SID = 0;
        objM.PMID = PMID;
        DataTable DTTemp = Check_SalarySlip_Exist(objM);
        if(DTTemp.Rows.Count==0)
        SID = new PayrollTemplateBAL().PayslipSave(objM);//15906
        // dDedE = LOP + dDedE;
        dDedE = LOP + dDedE;
        htmlTable.Append("<table border=\"1\" style=\"border: solid 1px Silver;font-size: 10px;\">");

        int ncount = dtAllowances.Rows.Count + 1;

        int totalcolumns = 4 + ((ncount > dtDeductions.Rows.Count) ? ncount : dtDeductions.Rows.Count);

        if (DT.Rows.Count > 0)
        {
            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + Cname + " " + sText + "</td></tr>");

            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + date + "</td></tr>");

            htmlTable.Append("<tr><td>Staff Name</td><td colspan=\"" + (totalcolumns - 1).ToString() + "\">" + DT.Rows[0]["FULLNAME"] + "</td></tr>");


            htmlTable.Append("<tr><td>Staff Acc NO</td><td colspan=\"2\">" + DT.Rows[0]["AccountNumber"] + "</td><td>Dept Name</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DT.Rows[0]["DepartmentName"] + "</td></tr>");
            htmlTable.Append("<tr><td>Position</td><td colspan=\"2\">" + DT.Rows[0]["Designation"] + "</td><td>Bank Name</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DT.Rows[0]["BankName"] + "</td></tr>");
            htmlTable.Append("<tr><td>Basic Salary</td><td>Bonus</td>");
            for (int i = 0; i < dtAllowances.Rows.Count; i++)
            {

                if (dtAllowances.Rows.Count > i)
                {
                    DataRow dRow = dtAllowances.Rows[i];

                    htmlTable.Append("<td>" + dRow["AlwName"] + "</td>");
                }
            }
            string CvAllwn = string.Empty;
            string CvDed = string.Empty;
            if (dtAllowances.Rows.Count > 0)
            {   
                if(SID!=0)
                BindList(dtAllowances,SID, "A");
                dRows["PMID"] = dtAllowances.Rows[0]["PMID"].ToString();
            }

            dRows["Allowance"] = CvAllwn;
          
            htmlTable.Append("<td>GOSI</td><td>Other</td><td>Salary Total</td></tr>");

            htmlTable.Append("<tr><td>" + DT.Rows[0]["BasicSalary"] + "</td><td>" + DT.Rows[0]["Bonus"] + "</td>");
            decimal dBS = Util.ToDecimal(DT.Rows[0]["BasicSalary"]);

            decimal dBO = Util.ToDecimal(DT.Rows[0]["Bonus"]);
            decimal dAllw = 0;
            decimal taxamount = 0;
            decimal Netsal = 0;
            decimal totalD = 0;
            decimal dBasic = Util.ToDecimal(DT.Rows[0]["BasicSalary"]);
            
           

            decimal dTaxable = dBasic;
            int gosialwtype = Util.ToInt(DT.Rows[0]["gosi_alw_type"]);
            decimal gosialwtypeamount = 0;

            for (int i = 0; i < dtAllowances.Rows.Count; i++)
            {
                decimal dAmt = 0;

                DataRow dRow = dtAllowances.Rows[i];
                dAmt = Util.ToDecimal(dRow["AlwAmount"]);
                if ("" + dRow["AlwType"] != "A")
                    dAmt = (dBasic * dAmt) / 100;
                if ("" + dRow["Taxable"] == "Y")
                    dTaxable = dTaxable + dAmt;
                htmlTable.Append("<td>" + Math.Round(dAmt, 2).ToString() + "</td>");
                dAllw += Math.Round(dAmt, 2);

                if (gosialwtype > 0 && Util.ToInt(dRow["AllowanceID"]) == gosialwtype)
                    gosialwtypeamount = dAmt;
            }
          //  objM.Allowance = "" + CvAllwn;
            if (gosialwtypeamount > 0)
            {
                if ("" + DT.Rows[0]["gosiType"] != "A")
                {
                    gosialwtypeamount = ((dBasic + gosialwtypeamount) * Util.ToDecimal(DT.Rows[0]["gosiamount"])) / 100;
                }
                else
                    gosialwtypeamount = Util.ToDecimal(DT.Rows[0]["gosiamount"]);

                dAllw = dAllw + Math.Round(gosialwtypeamount, 2);
            }

            dOver += Util.ToDecimal(DT.Rows[0]["Commission"]);
            decimal totsal = dBS + dBO + dAllw + Math.Round(dOver, 2);
            htmlTable.Append("<td>" + Math.Round(gosialwtypeamount, 2) + "</td><td>" + Math.Round(dOver, 2) + "</td><td>" + totsal + "</td></tr>");

            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count + 1 - dtDeductions.Rows.Count + 2).ToString() + "\">" + "Absence" + "</td>");
            for (int i = 0; i < dtDeductions.Rows.Count; i++)
            {
                if (dtDeductions.Rows.Count > i)
                {
                    DataRow dRow = dtDeductions.Rows[i];

                    htmlTable.Append("<td>" + dRow["DedName"] + "</td>");

                }
            }
            if (dtDeductions.Rows.Count > 0)
                if(SID!=0)
                 BindList(dtDeductions,SID,"D");
            dRows["Deduction"] = CvDed;
          //  objM.Deduction = CvDed;
            //GetTableSlip().Rows.Add(dRows);
           


            htmlTable.Append("<td>" + "Other" + "</td><td>" + "Total Deductions" + "</td></tr>");
            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count + 1 - dtDeductions.Rows.Count + 2).ToString() + "\">" + (dDED + dDedE + dMissedDaysAmount).ToString() + "</td>");
            decimal dDed = 0;
            for (int i = 0; i < dtDeductions.Rows.Count; i++)
            {

                DataRow dRow = dtDeductions.Rows[i];

                decimal dDedAmt = Util.ToDecimal("" + dRow["DedAmount"]);
                if ("" + dRow["DedType"] != "A")
                    dDedAmt = (dBasic * dDedAmt) / 100;
                if ("" + dRow["TaxExemption"] == "Y")
                    dTaxable = dTaxable - dDedAmt;
                htmlTable.Append("<td>" + Math.Round(dDedAmt, 2).ToString() + "</td>");
                dDed += Math.Round(dDedAmt, 2);

            }
           // objM.Deduction = "" + CvAllwn;
            totalD = dDED + dDed + dDedE + LP_leaveRule;
            if ("" + System.Web.HttpContext.Current.Session["USETAX"] != "")
            {
                taxamount = CalculateTaxAmount(dtTax, dTaxable * 12, objBOL.Gender);
                Netsal = Math.Abs(totsal) - (Math.Abs(totalD) + Math.Abs(taxamount));
                objM.Tax = ""+taxamount;
            }
            else
            {

                Netsal = Math.Abs(totsal) - Math.Abs(totalD);
            }
            htmlTable.Append("<td>" + LP_leaveRule + "</td><td>" + totalD + "</td></tr>");
            htmlTable.Append("<tr><td>" + "Signature" + "</td><td colspan=\"2\"></td><td bgcolor=\"#AAAAAA\" colspan=\"" + (totalcolumns - 5).ToString() + "\"></td><td align=\"right\">NET PAID</td><td>" + Netsal + "</td></tr>");
            htmlTable.Append("<tr><td colspan=\"4\">&nbsp;</td><td align=\"right\">Date</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DateTime.Now.ToString() + "</td></tr>");
            htmlTable.Append("</body>");
            htmlTable.Append("</table>");

            //  PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            // ltData.Text = htmlTable.ToString();
          
         
            string sExtn = ".pdf";

            if (!System.IO.Directory.Exists(sFilePath))
                System.IO.Directory.CreateDirectory(sFilePath);

            FileStream fs = new FileStream(sFilePath + "salary_" + DT.Rows[0]["EmpCode"] + "_" + DT.Rows[0]["FirstName"] + "_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + sExtn, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            string sHtml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                 <!DOCTYPE html 
                     PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
                    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                 <html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
                    <head>
                        <title></title>
                    </head>
                  <body>
                    " + htmlTable.ToString() + "</body></html>";

            /*  using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
              {

                  //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                  using (var sr = new StringReader(htmlTable.ToString()))
                  {
                      //Parse the HTML
                      htmlWorker.Parse(sr);
                  }
              }*/

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(sHtml), null);
            for (int k = 0; k < htmlarraylist.Count; k++)
            {
                doc.Add((IElement)htmlarraylist[k]);
            }


            doc.Close();
            fs.Close();
            if (SendMail)
            {
                EmailBAL objBAL = new EmailBAL();
                EmailBOL objEmail = new EmailBOL();

                objEmail = objBAL.Select();
                SendEmailToEmployee(objEmail, objBOL, sFilePath);
            }
        }
    }
    private static decimal CalculateTaxAmount(DataTable dtTax, decimal dtTaxable, string gender)
    {
        decimal taxamount = 0;
        DataRow taxrow = null;
        foreach (DataRow row in dtTax.Rows)
        {
            if (Util.ToDecimal(row["SalaryFrom"]) <= dtTaxable && dtTaxable <= Util.ToDecimal(row["SalaryTo"]))
            {
                if ("" + row["Gender"] == gender)
                {
                    taxrow = row;
                    break;
                }
            }
        }
        if (taxrow != null)
        {
            taxamount = dtTaxable - Util.ToDecimal(taxrow["ExemptedTaxAmount"]);
            taxamount = (taxamount * (int)(Util.ToDecimal(taxrow["TaxPercentage"]))) / 100;
            taxamount = (int)taxamount / 12;
        }
        return taxamount;
    }
    private static void SendEmailToEmployee(EmailBOL objEmail, EmployeeBOL objEmp, string sFilePath)
    {
        try
        {
            MailAddress from = new MailAddress(objEmail.FromEmail);
            MailAddress to = new MailAddress(objEmp.WEmail);
            MailMessage msg = new MailMessage(from, to);
            string subject = "Salary Payslip";
            msg.Subject = subject;
            string body = "Dear " + objEmp.FirstName + ",<br/>";
            body += string.Format("Please find attached the Salary slip for the month of {0} {1}.<br/>", DateTime.Now.ToString("MMMM"), DateTime.Today.Year.ToString());
            body += "Regards<br/>";
            body += objEmail.FromName + "<br/>";
            msg.Body = body;
            string file = string.Format("{0}salary_{1}_{2}.pdf", sFilePath, objEmp.EmpCode, objEmp.FirstName);
            if (File.Exists(file))
            {
                msg.Attachments.Add(new Attachment(file));
            }
            SmtpClient smtp = new SmtpClient(objEmail.SmtpHost, objEmail.SmtpPort);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(objEmail.SmtpUserName, objEmail.SmtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
        catch
        {
        }
    }
    private decimal GetMin_Overtime()
    {
        decimal minhr = 0;
        DataTable dt_OvTime = new AttRegularBAL().GetPayable_Overtime();
        if (dt_OvTime.Rows.Count > 0)
        {
            minhr = Util.ToDecimal(dt_OvTime.Rows[0]["MinimumHr"].ToString());
            minhr = Util.Get_Hours(minhr);
            //if (minhr > 0)
            //{
            //    string[] sVal = minhr.ToString().Split('.');
            //    if (Util.ToInt(sVal[1]) > 59)
            //    {
            //        sVal[0] = Convert.ToString((Util.ToInt(sVal[0])) + Util.ToInt(sVal[1]) / 60);
            //        sVal[1] = Convert.ToString((Util.ToInt(sVal[1]) % 60) / 100);

            //        minhr = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);
            //    }

            //}
        }

        return minhr;
    }

    private DataTable Dtovertime()
    {
        OverTimeWageBAL objBAL = new OverTimeWageBAL();
        OverTimeWageBOL objBOL = new OverTimeWageBOL();
        objBOL.EmployeeId = EmpID;
        objBOL.Month = sMonth;
        objBOL.Year = sYear;
        DataTable dT = objBAL.SelectAll(objBOL);
        return dT;
    }
    private decimal GetOverTime()
    {
        decimal dComm = 0;

        DataTable dT = Dtovertime();
        decimal Pay_Over = Get_PayableOverTime();
        if (Pay_Over > 0)
        {
            Pay_Over = Util.Get_Hours(Pay_Over);
            //string[] sVal = Pay_Over.ToString().Split('.');
            //if (Util.ToInt(sVal[1]) > 59)
            //{
            //    sVal[0] = Convert.ToString((Util.ToInt(sVal[0])) + Util.ToInt(sVal[1]) / 60);
            //    sVal[1] = Convert.ToString((Util.ToInt(sVal[1]) % 60) / 100);

            //    Pay_Over = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);
            //}

        }
        foreach (DataRow row in dT.Rows)
        {
            //  if (Util.ToInt(row["OMonth"]) == nMonth && Util.ToInt(row["OYear"]) == nYear)
            decimal day = 0; decimal end = 0;
            if (row["WeekDay"].ToString() != "")
            {
                day = Decimal.Parse(row["WeekDay"].ToString());
            }
            if (row["WeekEnd"].ToString() != "")
            {
                end = Decimal.Parse(row["WeekEnd"].ToString());
            }

            dComm += Pay_Over;

        }

        return dComm;
    }
    private decimal Get_PayableOverTime()
    {
        DateTime start = DateTime.MinValue;
        DateTime End = DateTime.MinValue;
        decimal Pay_over = 0; decimal minhr = 0; decimal overtime = 0;
        DataTable dTwage = Dtovertime();
        DataTable dT = new AttRegularBAL().showAttendance(EmpID, sMonth, sYear);
        minhr = GetMin_Overtime();
        for (int i = 0; i < dT.Rows.Count; i++)
        {

            decimal OT = Util.ToDecimal(dT.Rows[i]["OverTime"].ToString());

            if (OT == 0 && dT.Rows[i]["STARTTIME"].ToString() != "" && dT.Rows[i]["ENDTIME"].ToString() != "")
            {

                decimal Hours = Util.ToDecimal(dT.Rows[i]["WorkedHours"].ToString());
                decimal WH = Util.ToDecimal(dT.Rows[i]["WORKNGHOUR"].ToString());
                decimal BH = Util.ToDecimal(dT.Rows[i]["BreakHours"].ToString());
                overtime = Math.Abs(Util.Get_Hours_MinusFromHundred((BH + WH), Hours));

                if (overtime > 0)
                {
                    overtime = Util.Get_Hours(overtime);

                }
                if (Util.Get_Hours_MinusFromHundred(Hours, BH) > WH)
                {

                    if (overtime > minhr)
                    {
                        if (dT.Rows[i]["offday"].ToString() == "N")
                        {
                            Pay_over += Util.Get_Regular_wage(Util.Get_Hours_MinusFromHundred(overtime, minhr), Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString()), Util.ToDecimal(dTwage.Rows[0]["overwkday"].ToString()));

                        }
                        else if (dT.Rows[i]["offday"].ToString() == "Y")
                        {
                            Pay_over += Util.Get_Regular_wage(Util.Get_Hours_MinusFromHundred(overtime, minhr), Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString()), Util.ToDecimal(dTwage.Rows[0]["overwkend"].ToString()));

                        }

                    }
                }

            }
            else if (OT != 0)
            {
                if (OT > minhr)
                {
                    if (dT.Rows[i]["offday"].ToString() == "N")
                    {
                        Pay_over += Util.Get_Regular_wage(Util.Get_Hours_MinusFromHundred(OT, minhr), Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString()), Util.ToDecimal(dTwage.Rows[0]["overwkday"].ToString()));

                    }
                    else if (dT.Rows[i]["offday"].ToString() == "Y")
                    {
                        Pay_over += Util.Get_Regular_wage(Util.Get_Hours_MinusFromHundred(OT, minhr), Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString()), Util.ToDecimal(dTwage.Rows[0]["overwkend"].ToString()));

                    }

                }
            }

        }

        return Pay_over;
    }
    private decimal GetDeduction()
    {
        decimal dDed = 0;

        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = EmpID;
        objBOL.Month = sMonth;
        objBOL.Year = sYear;
        DataTable dT = objBAL.SelectAll(objBOL);
        foreach (DataRow row in dT.Rows)
        {
            //if (Util.ToInt(row["WMon"]) == nMonth && Util.ToInt(row["WYer"]) == nYear)
            dDed += Util.ToDecimal(row["Ded"]);

        }

        return dDed;
    }
    private DataTable Checkyear_timesheet()
    {
        return new LeaveRuleBAL().Checkyear_timesheet();
    }

    private decimal Get_LOP_WrkngHour()
    {
        decimal dDed = 0; decimal ZeroAttendanceFrom = 0; decimal ZeroAttendanceTo = 0; decimal HalfAttendanceFrom = 0;
        decimal HalfAttendanceTo = 0;
        bool bLeaveRuleApplied = true;

        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = EmpID;
        objBOL.Month = sMonth;
        objBOL.Year = sYear;
        DataTable dT = objBAL.Get_LOP_WrkngHour(objBOL);
        DataTable dTwage = Dtovertime();
        DataTable DTRule = new AttendanceBAL().Get_Active_AttendanceRule();
        ZeroAttendanceFrom = Util.ToDecimal(DTRule.Rows[0]["ZeroAttendanceFrom"].ToString());
        ZeroAttendanceTo = Util.ToDecimal(DTRule.Rows[0]["ZeroAttendanceTo"].ToString());
        HalfAttendanceFrom = Util.ToDecimal(DTRule.Rows[0]["HalfAttendanceFrom"].ToString());
        HalfAttendanceTo = Util.ToDecimal(DTRule.Rows[0]["HalfAttendanceTo"].ToString());
        DataTable DTLrule = Checkyear_timesheet();
        if (DTLrule.Rows.Count > 0)
            bLeaveRuleApplied = true;
        if (DTRule.Rows[0]["UseValue"].ToString() == "Y")
        {
            foreach (DataRow row in dT.Rows)
            {
                //if (Util.ToInt(row["WMon"]) == nMonth && Util.ToInt(row["WYer"]) == nYear)
                if (Util.ToDecimal(row["lophr"]) > 0)
                {
                    if (bLeaveRuleApplied)
                    {
                        if (Util.ToDecimal(row["lophr"]) >= Util.ToDecimal(row["latehour"]))
                        {
                            dDed += Util.Get_Hours_MinusFromHundred(Util.ToDecimal(row["lophr"]), Util.ToDecimal(row["latehour"])) * Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString());
                        }
                    }
                    else
                    {
                        dDed += Util.ToDecimal(row["lophr"]) * Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString());
                    }
                }
            }

            //calculate hr:minutes correctly <here>
        }

        if (DTRule.Rows[0]["UseRule"].ToString() == "Y")
        {

            foreach (DataRow row in dT.Rows)
            {
                decimal WH = Util.ToDecimal(row["WorkShiftHours"]);
                decimal WKDHR = Util.ToDecimal(row["WorkedHours"]);
                decimal BH = Util.ToDecimal(row["BreakHours"]);
                decimal LH = Util.ToDecimal(row["latehour"]);
                decimal dVal = 0;

                if (bLeaveRuleApplied)
                    dVal = (Util.Get_Hours_MinusFromHundred(WKDHR, BH + LH) / WH) * 100;
                else
                    dVal = (Util.Get_Hours_MinusFromHundred(WKDHR, BH) / WH) * 100;

                if (ZeroAttendanceFrom <= dVal && ZeroAttendanceTo >= dVal)
                {
                    dDed += WH * Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString());
                }
                else if (HalfAttendanceFrom <= dVal && HalfAttendanceTo >= dVal)
                {
                    dDed += (WH / 2) * Util.ToDecimal(dTwage.Rows[0]["regularhour"].ToString());
                }
            }
        }


        return dDed;
    }

    private decimal GetLeaveTakenAsLop_Absence()
    {
        return new LeaveBAL().LopLeaveTaken(EmpID, sYear, sMonth, minLeaveCount);
    }

    private DataTable GetTable()
    {
        DataTable dDedDT = new DataTable();
        dDedDT.Columns.Add("EmployeeId");
        dDedDT.Columns.Add("LOP");
        dDedDT.Columns.Add("diff");
        return dDedDT;
    }
    private decimal GetLateValue()
    {
        decimal dDed = 0;
        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();

        objBOL.EmployeeID = EmpID;
        objBOL.Month = sMonth;
        objBOL.Year = sYear;
        objBOL.Reason = "Leave Rules => LateBy Leaves";
        DataTable dtRules = objBAL.checklatebytime(); // GET CURRENTLY ACTIVE LEAVE RULE DETAILS
        reghourrate = 0;
        bool bApplied = false;


        DataTable dtApplied = objBAL.AppliedLeaveRule(EmpID, sMonth, sYear);
        if (dtApplied.Rows.Count > 0)
            bApplied = true;
        if (dtRules.Rows.Count > 0)
        {
            DataSet dSetDiff = objBAL.Getlatebytype(objBOL);



            if (dSetDiff.Tables[1].Rows.Count > 0)
            {
                reghourrate = Util.ToDecimal(dSetDiff.Tables[1].Rows[0]["Regularhours"]);
                dMissedDays = Util.ToDecimal(dSetDiff.Tables[1].Rows[0]["MissedDays"]);
                dMissedDaysAmount = Util.ToDecimal(dSetDiff.Tables[1].Rows[0]["MissedDaysAmount"]);
            }

            if (dSetDiff.Tables[0].Rows.Count > 0)
            {
                minLeaveCount = Util.ToDecimal(dtRules.Rows[0]["MinimumLeaves"]);
                DataTable dTdiff = dSetDiff.Tables[0];

                for (int i = 0; i < dtRules.Rows.Count; i++)
                {
                    DataRow dR = dtRules.Rows[i];
                    if ("" + dR["LateByType"] == "PM") // Per Month
                    {
                        decimal diffVal = Util.Get_Hours(Util.ToDecimal(dTdiff.Compute("Sum(diff)", "")));
                        string[] sVal = diffVal.ToString().Split('.');


                        switch ("" + dR["LateByTime"])
                        {
                            case "H"://Hour
                                if (sVal.Length > 1)
                                {
                                    int nVal = Util.ToInt(sVal[1]) / 60;
                                    int rem = Util.ToInt(sVal[1]) - (nVal * 60);
                                    diffVal = Util.ToDecimal(sVal[0]) + Util.ToDecimal(nVal) + Util.ToDecimal(Util.ToDecimal(rem) / 100);
                                }
                                break;
                            case "M"://Minutes
                                if (sVal.Length > 1)
                                {
                                    diffVal = (Util.ToDecimal(sVal[0]) * 60) + Util.ToDecimal(sVal[1]);
                                }
                                break;
                        }


                        decimal latetime = Util.ToDecimal(dR["LateByValue"]);
                        if (diffVal > latetime)
                        {
                            string sLossvalue = ("" + dR["LossValue"]).ToUpper();
                            if ("" + dR["LossType"] == "L")
                            {
                                if (!bApplied)
                                {
                                    LeaveBOL objLeave = new LeaveBOL();
                                    objLeave.LeaveTypeID = Util.ToInt(dR["LTID"]);
                                    objLeave.EmployeeID = EmpID;
                                    objLeave.LeaveMonth = sMonth;
                                    objLeave.LeaveYear = sYear;
                                    objLeave.CreatedBy = "" + HttpContext.Current.Session["EMPID"];
                                    objLeave.Status = "Y";
                                    objLeave.LeaveDays = (sLossvalue == "X") ? diffVal.ToString() : sLossvalue;
                                    if (Util.ToDecimal(objLeave.LeaveDays) < 1)
                                        objLeave.LeaveSession = "FN";
                                    else
                                        objLeave.LeaveSession = "FULL";
                                    objLeave.Reason = "Leave Rule applied";
                                    objLeave.LeaveID = objBAL.SaveWithLeaveRule(objLeave);
                                    objBAL.SaveLeaveDates(objLeave);
                                }
                            }
                            else if ("" + dR["LossType"] == "LOP")
                            {
                                if ("" + dR["LossTime"] == "H")
                                {
                                    if (sLossvalue == "X")
                                        dDed += Util.Get_Regular_wage(diffVal, reghourrate, 1);
                                    else
                                        dDed += Util.Get_Regular_wage(Util.ToDecimal(sLossvalue), reghourrate, 1);
                                }
                                else if ("" + dR["LossTime"] == "M")
                                {
                                    if (sLossvalue == "X")
                                        dDed += Util.Get_Regular_wage(diffVal, reghourrate / 60, 1);
                                    else
                                        dDed += Util.Get_Regular_wage(Util.ToDecimal(sLossvalue), reghourrate / 60, 1);
                                }
                            }
                        }
                    }
                    else if ("" + dR["LateByType"] == "PD") // Per DAY
                    {
                        for (int k = 0; k < dTdiff.Rows.Count; k++)
                        {
                            decimal diffVal = Util.ToDecimal(dTdiff.Rows[k]["diff"]);
                            string[] sVal = diffVal.ToString().Split('.');


                            switch ("" + dR["LateByTime"])
                            {
                                case "H"://Hour
                                    if (sVal.Length > 1)
                                    {
                                        int nVal = Util.ToInt(sVal[1]) / 60;
                                        int rem = Util.ToInt(sVal[1]) - (nVal * 60);
                                        diffVal = Util.ToDecimal(sVal[0]) + Util.ToDecimal(nVal) + Util.ToDecimal(Util.ToDecimal(rem) / 100);
                                        /* int nVal = Util.ToInt(sVal[1]) / 60;
                                         int rem = Util.ToInt(sVal[1]) - nVal * 60;
                                         diffVal = Util.ToDecimal(sVal[0]) + nVal + Util.ToDecimal(rem / 100);*/
                                    }
                                    break;
                                case "M"://Minutes
                                    if (sVal.Length > 1)
                                    {
                                        diffVal = (Util.ToDecimal(sVal[0]) * 60) + Util.ToDecimal(sVal[1]);
                                    }
                                    break;
                            }


                            decimal latetime = Util.ToDecimal(dR["LateByValue"]);
                            if (diffVal > latetime)
                            {
                                string sLossvalue = ("" + dR["LossValue"]).ToUpper();
                                if ("" + dR["LossType"] == "L")
                                {
                                    if (!bApplied)
                                    {
                                        LeaveBOL objLeave = new LeaveBOL();
                                        objLeave.LeaveTypeID = Util.ToInt(dR["LTID"]);
                                        objLeave.EmployeeID = EmpID;
                                        objLeave.LeaveMonth = sMonth;
                                        objLeave.LeaveYear = sYear;
                                        objLeave.CreatedBy = "" + HttpContext.Current.Session["EMPID"];
                                        objLeave.Status = "Y";
                                        objLeave.LeaveDays = (sLossvalue == "X") ? diffVal.ToString() : sLossvalue;
                                        if (Util.ToDecimal(objLeave.LeaveDays) < 1)
                                            objLeave.LeaveSession = "FN";
                                        else
                                            objLeave.LeaveSession = "FULL";
                                        objLeave.Reason = "Leave Rule applied";
                                        objLeave.LeaveID = objBAL.SaveWithLeaveRule(objLeave);
                                        objBAL.SaveLeaveDates(objLeave);
                                    }
                                }
                                else if ("" + dR["LossType"] == "LOP")
                                {
                                    if ("" + dR["LossTime"] == "H")
                                    {
                                        if (sLossvalue == "X")
                                            dDed += Util.Get_Regular_wage(diffVal, reghourrate, 1);
                                        else
                                            dDed += Util.Get_Regular_wage(Util.ToDecimal(sLossvalue), reghourrate, 1);
                                    }
                                    else if ("" + dR["LossTime"] == "M")
                                    {
                                        if (sLossvalue == "X")
                                            dDed += Util.Get_Regular_wage(diffVal, reghourrate / 60, 1);
                                        else
                                            dDed += Util.Get_Regular_wage(Util.ToDecimal(sLossvalue), reghourrate / 60, 1);

                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        return dDed;
    }
}