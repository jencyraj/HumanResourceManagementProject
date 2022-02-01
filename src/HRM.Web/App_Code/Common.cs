using System;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Collections.Generic;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Net;
using System.Net.Mail;

using HRM.BAL;
using HRM.BOL;

using Ionic.Zip;

using iTextSharp.text;
using iTextSharp.text.pdf;

public class PDFGenerator
{
    static iTextSharp.text.Font normalFont = FontFactory.GetFont("ARIAL", 8);
    static iTextSharp.text.Font normalFont_bold = FontFactory.GetFont("ARIAL", 8, Font.BOLD);
    static iTextSharp.text.Font normalFont_Underline = FontFactory.GetFont("ARIAL", 8, Font.UNDERLINE);
    static iTextSharp.text.Font normalFont_UnderlineBold = FontFactory.GetFont("ARIAL", 8, Font.UNDERLINE | Font.BOLD);
    static iTextSharp.text.Font bigFont = FontFactory.GetFont("ARIAL", 14);
    static iTextSharp.text.Font bigFont_bold = FontFactory.GetFont("ARIAL", 14, Font.BOLD);
    static iTextSharp.text.Font bigFont_Underline = FontFactory.GetFont("ARIAL", 14, Font.UNDERLINE);
    static iTextSharp.text.Font bigFont_UnderlineBold = FontFactory.GetFont("ARIAL", 14, Font.UNDERLINE | Font.BOLD);
    static decimal leavesded = 0; static bool flag = false;
	public static string ErrorLog(string sErrMsg)
    {
        string txt = "1";
        string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
        string Errordir = ConfigurationManager.AppSettings["ErrorPath"];
        if (!System.IO.Directory.Exists(Errordir))
        {
            System.IO.Directory.CreateDirectory(Errordir);
        }
        DateTime dt = DateTime.Now;
        string dtval = dt.ToString("dd-MM-yyyy");
        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["ErrorPath"] + "\\" + dtval + "Score_error.txt", true);
        sw.WriteLine(DateTime.Now.ToShortDateString() + " : \n" + sLogFormat + sErrMsg + "\n----------------------------------");
        sw.Flush();
        sw.Close();
        return txt;
    }
    public static string CreatePDF(string sFilePath, string EmployeeeIds, int nMonth, int nYear, bool SendEmail, out string sEmp)
    {
        string retMessage = "";
        sEmp = "";
        string sExtn = ".pdf";
        if (!System.IO.Directory.Exists(sFilePath))
            System.IO.Directory.CreateDirectory(sFilePath);


        string[] EmpIDs = EmployeeeIds.Split(',');

        foreach (string EmpID in EmpIDs)
        {
            if (EmpID.Trim() != "")
            {
                EmployeeBOL objBOL = new EmployeeBOL();
                objBOL.EmployeeID = Util.ToInt(EmpID.Trim());
                objBOL = new EmployeeBAL().Select(objBOL);//GET EMPLOYEE DETAILS

                OrganisationBOL objOrg = new OrganisationBAL().Select(); //GET COMPANY DETAILS

                PayrollMasterBOL objMaster = new PayrollMasterBOL();
                objMaster.EmployeeId = objBOL.EmployeeID;
                objMaster.DesignationId = objBOL.DesgnID;
                DataSet dSet = new PayrollTemplateBAL().PayrollTemplateforSalarySlip(objMaster, nMonth, nYear);

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

                DataRow drMaster = dSet.Tables[0].Rows[0];

                decimal dBasic = Util.ToDecimal(drMaster["BasicSalary"]); //GET BASIC SALARY
                decimal dTaxable = dBasic;
                decimal dDeduction = 0;
                DataTable dtAllowances = dSet.Tables[1];
                DataTable dtDeductions = dSet.Tables[2];

                DataTable dtTax = dSet.Tables[3];

                FileStream fs = new FileStream(sFilePath + "salary_" + objBOL.EmpCode + "_" + objBOL.FirstName + sExtn, FileMode.Create, FileAccess.Write, FileShare.None);
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                PdfPTable headertable = new PdfPTable(3);
                headertable.WidthPercentage = 98;
                float[] widths = new float[] { 25f, 50f, 20f };
                headertable.SetWidths(widths);
                PdfPCell[] jHdrCell = new PdfPCell[3];
                for (int i = 0; i < 2; i++)
                {
                    jHdrCell[i] = new PdfPCell();
                    jHdrCell[i].Border = 0;
                }
                jHdrCell[0].PaddingTop = 50f;
                jHdrCell[0].HorizontalAlignment = Element.ALIGN_LEFT;
                jHdrCell[0].VerticalAlignment = Element.ALIGN_MIDDLE;
                jHdrCell[0].Border = 0;
                var phrase1 = new Phrase();
                phrase1.Add(new Chunk("Employee: ", normalFont_bold));
                sName = objBOL.FirstName + ((objBOL.MiddleName.Trim() != "") ? " " + objBOL.MiddleName : "") + ((objBOL.LastName.Trim() != "") ? " " + objBOL.LastName : "");
                phrase1.Add(new Chunk(sName, normalFont));
                jHdrCell[0].AddElement(phrase1);
                var phrase2 = new Phrase();
                phrase2.Add(new Chunk("Designation: ", normalFont_bold));
                phrase2.Add(new Chunk(objBOL.Designation.Designation + "", normalFont));
                jHdrCell[0].AddElement(phrase2);
                var phrase3 = new Phrase();
                phrase3.Add(new Chunk("Month: ", normalFont_bold));
                phrase3.Add(new Chunk(new DateTime(nYear, nMonth, 1).ToString("MMMM"), normalFont));
                phrase3.Add(new Chunk("    Year: ", normalFont_bold));
                phrase3.Add(new Chunk(nYear.ToString(), normalFont));
                jHdrCell[0].AddElement(phrase3);
                Paragraph pg1 = new Paragraph(objOrg.CompanyName, bigFont_bold);
                pg1.Alignment = Element.ALIGN_CENTER;
                pg1.Leading = 0;
                pg1.MultipliedLeading = 1.0f;
                pg1.SpacingBefore = 10f;
                pg1.SpacingAfter = 10f;
                jHdrCell[1].AddElement(pg1);
                jHdrCell[1].HorizontalAlignment = Element.ALIGN_MIDDLE;
                Paragraph pg2 = new Paragraph(objOrg.Address + ", " + objOrg.City + ", " + objOrg.State + " ZIP : " + objOrg.ZipCode, normalFont);
                pg2.Alignment = Element.ALIGN_CENTER;
                pg2.Leading = 0;
                pg2.MultipliedLeading = 1.0f;
                pg2.SpacingAfter = 5f;
                jHdrCell[1].AddElement(pg2);
                Paragraph pg3 = new Paragraph("Salary Slip", normalFont);
                pg3.Alignment = Element.ALIGN_CENTER;
                pg3.Leading = 0;
                pg3.MultipliedLeading = 1.0f;
                pg3.SpacingAfter = 5f;
                jHdrCell[1].AddElement(pg3);
                jHdrCell[2] = new PdfPCell();
                jHdrCell[2].PaddingTop = 10f;
                jHdrCell[2].HorizontalAlignment = Element.ALIGN_RIGHT;
                jHdrCell[2].VerticalAlignment = Element.ALIGN_BOTTOM;
                jHdrCell[2].Border = 0;
                PdfPRow pHdrRow = new PdfPRow(jHdrCell);
                headertable.Rows.Add(pHdrRow);
                doc.Add(headertable);

                PdfPTable table1 = new PdfPTable(1);
                table1.WidthPercentage = 98;
                table1.SpacingBefore = 15f;

                PdfPCell[] pCell = new PdfPCell[1];

                PdfPTable tbEarnings = new PdfPTable(4);
                tbEarnings.WidthPercentage = 98;
                PdfPCell[] pECell = new PdfPCell[4];

                pECell[0] = new PdfPCell(new Phrase("Earnings", normalFont_bold));
                pECell[0].BorderWidthRight = 0.0f;
                pECell[1] = new PdfPCell(new Phrase("", normalFont));
                pECell[1].BorderWidthLeft = 0.0f;
                pECell[2] = new PdfPCell(new Phrase("Deductions", normalFont_bold));
                pECell[2].BorderWidthRight = 0.0f;
                pECell[3] = new PdfPCell(new Phrase("", normalFont));
                pECell[3].BorderWidthLeft = 0.0f;

                PdfPRow pETRow = new PdfPRow(pECell);
                tbEarnings.Rows.Add(pETRow);

                pECell = new PdfPCell[4];

                pECell[0] = new PdfPCell(new Phrase("Basic Salary", normalFont));
                pECell[1] = new PdfPCell(new Phrase(dBasic.ToString(), normalFont));
                pECell[2] = new PdfPCell();
                pECell[3] = new PdfPCell();

                pETRow = new PdfPRow(pECell);
                tbEarnings.Rows.Add(pETRow);


                decimal dTotAllowance = 0;
                decimal dTotDeductions = 0;
               
                decimal dBonus = GetBonus(objBOL.EmployeeID, nMonth, nYear);
                if (dBonus > 0)
                {
                    DataRow drAlw = dtAllowances.NewRow();
                    drAlw["AlwName"] = "Bonus";
                    drAlw["AlwType"] = "A";
                    drAlw["Taxable"] = "N";
                    drAlw["AlwAmount"] = dBonus.ToString();
                    dtAllowances.Rows.Add(drAlw);
                }

                decimal dComm = GetCommission(objBOL.EmployeeID, nMonth, nYear);
                if (dComm > 0)
                {
                    DataRow drAlw = dtAllowances.NewRow();
                    drAlw["AlwName"] = "Commission";
                    drAlw["AlwType"] = "A";
                    drAlw["Taxable"] = "N";
                    drAlw["AlwAmount"] = dComm.ToString();
                    dtAllowances.Rows.Add(drAlw);
                }
                decimal dDed = GetDeduction(objBOL.EmployeeID, nMonth, nYear);
                if (dDed > 0)
                {
                    DataRow drAlw = dtAllowances.NewRow();
                    
                    dtAllowances.Rows.Add(dDed);
                }
                decimal dDedAd =GetLeaveTaken(objBOL.EmployeeID, nMonth, nYear);
                if (dDedAd > 0)
                {
                    DataRow drAlw = dtAllowances.NewRow();

                    dtAllowances.Rows.Add(dDedAd);
                }
                decimal  dOver = GetOverTime(objBOL.EmployeeID, nMonth, nYear);
                if (dOver > 0)
                {
                    DataRow drAlw = dtAllowances.NewRow();
                   
                    dtAllowances.Rows.Add(dOver);
                }
                dDeduction = dOver - (dDed + dDedAd);
                int nTotRows = (dtAllowances.Rows.Count >= dtDeductions.Rows.Count) ? dtAllowances.Rows.Count : dtDeductions.Rows.Count;

                for (int i = 0; i < nTotRows; i++)
                {
                    pECell = new PdfPCell[4];
                    if (dtAllowances.Rows.Count > i)
                    {
                        DataRow dRow = dtAllowances.Rows[i];
                        decimal dAmt = Util.ToDecimal("" + dRow["AlwAmount"]);
                        pECell[0] = new PdfPCell(new Phrase("" + dRow["AlwName"], normalFont));
                        if ("" + dRow["AlwType"] != "A")
                            dAmt = (dBasic * dAmt) / 100;
                        pECell[1] = new PdfPCell(new Phrase("" + dAmt, normalFont));

                        if ("" + dRow["Taxable"] == "Y")
                            dTaxable = dTaxable + dAmt;

                        dTotAllowance = dTotAllowance + dAmt;
                    }
                    else
                    {
                        pECell[0] = new PdfPCell(new Phrase("", normalFont));
                        pECell[1] = new PdfPCell(new Phrase("", normalFont));
                    }
                    if (dtDeductions.Rows.Count > i)
                    {
                        DataRow dRow = dtDeductions.Rows[i];
                        decimal dAmt = Util.ToDecimal("" + dRow["DedAmount"]);
                        pECell[2] = new PdfPCell(new Phrase("" + dRow["DedName"], normalFont));
                        if ("" + dRow["DedType"] != "A")
                            dAmt = dBasic * (dAmt / 100);
                        pECell[3] = new PdfPCell(new Phrase("" + dAmt, normalFont));

                        if ("" + dRow["TaxExemption"] == "Y")
                            dTaxable = dTaxable - dAmt;

                        dTotDeductions = dTotDeductions + dAmt;
                    }
                    else
                    {
                        pECell[2] = new PdfPCell(new Phrase("Loss Of Pay(Leaves)", normalFont));
                        pECell[3] = new PdfPCell(new Phrase(dDed.ToString(), normalFont));
                    }
                    PdfPRow pERow = new PdfPRow(pECell);
                    tbEarnings.Rows.Add(pERow);
                }
                //pECell = new PdfPCell[4];
                //pECell[0] = new PdfPCell(new Phrase("", normalFont));
                //pECell[1] = new PdfPCell(new Phrase("", normalFont));
                //pECell[2] = new PdfPCell(new Phrase("Loss Of Pay(Leaves)", normalFont));
                //pECell[3] = new PdfPCell(new Phrase(dDed.ToString(), normalFont));

                //pETRow = new PdfPRow(pECell);
                //tbEarnings.Rows.Add(pETRow);
                pECell = new PdfPCell[4];
                pECell[0] = new PdfPCell(new Phrase("OverTime Wage", normalFont));
                pECell[1] = new PdfPCell(new Phrase(dOver.ToString(), normalFont));
                pECell[2] = new PdfPCell(new Phrase("Loss Of Pay(Additional Leaves)", normalFont));
                pECell[3] = new PdfPCell(new Phrase(dDedAd.ToString(), normalFont));

                pETRow = new PdfPRow(pECell);
                tbEarnings.Rows.Add(pETRow);

                decimal dNetSal = (dBasic + dTotAllowance) - (dTotDeductions);// + taxamount);
                decimal dNetSal1 = dNetSal - Math.Abs(dDeduction);
                if ("" + System.Web.HttpContext.Current.Session["USETAX"] != "")
                {

                    pECell = new PdfPCell[4];
                    pECell[0] = new PdfPCell();
                    pECell[1] = new PdfPCell();
                    pECell[2] = new PdfPCell(new Phrase("Taxable Amount", normalFont));
                    decimal taxamount = CalculateTaxAmount(dtTax, dTaxable * 12, objBOL.Gender);
                    pECell[3] = new PdfPCell(new Phrase(taxamount.ToString(), normalFont));

                    pETRow = new PdfPRow(pECell);
                    tbEarnings.Rows.Add(pETRow);

                    dNetSal = (dNetSal - taxamount);
                }

               
         
             
                pECell = new PdfPCell[4];
                pECell[0] = new PdfPCell();
                pECell[1] = new PdfPCell();
                pECell[2] = new PdfPCell(new Phrase("NET SALARY", normalFont_bold));
                pECell[3] = new PdfPCell(new Phrase(dNetSal1.ToString(), normalFont_bold));

                pETRow = new PdfPRow(pECell);
                tbEarnings.Rows.Add(pETRow);

                
                pCell = new PdfPCell[1];
                pCell[0] = new PdfPCell(tbEarnings);
                PdfPRow pRow1 = new PdfPRow(pCell);
                table1.Rows.Add(pRow1);

                doc.Add(table1);
                writer.PageEvent = new Footer();
                doc.Close();

                if (SendEmail)
                {
                    EmailBAL objBAL = new EmailBAL();
                    EmailBOL objEmail = new EmailBOL();

                    objEmail = objBAL.Select();
                    SendEmailToEmployee(objEmail, objBOL, sFilePath);
                }
            }
        }
        return retMessage;
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

    private static decimal GetCommission(int empid, int nMonth, int nYear)
    {
        decimal dComm = 0;

        CommissionBAL objBAL = new CommissionBAL();
        CommissionBOL objCommission = new CommissionBOL();
        objCommission.EmployeeId = empid;
        DataTable dT = objBAL.SelectAll(objCommission);
        foreach (DataRow row in dT.Rows)
        {
            if (Util.ToInt(row["commonth"]) == nMonth && Util.ToInt(row["comyear"]) == nYear)
                dComm += Util.ToDecimal(row["Amount"]);
        }

        return dComm;
    }

    private static decimal GetBonus(int empid, int nMonth, int nYear)
    {
        decimal dComm = 0;

        BonusBAL objBAL = new BonusBAL();
        BonusBOL objBonus = new BonusBOL();
        objBonus.EmployeeId = empid;
        DataTable dT = objBAL.SelectAll(objBonus);
        foreach (DataRow row in dT.Rows)
        {
            if (Util.ToInt(row["BonusMonth"]) == nMonth && Util.ToInt(row["BonusYear"]) == nYear)
                dComm += Util.ToDecimal(row["Amount"]);
        }

        return dComm;
    }
    private static decimal GetOverTime(int empid, int nMonth, int nYear)
    {
        decimal dComm = 0;
        OverTimeWageBAL objBAL = new OverTimeWageBAL();
        OverTimeWageBOL objBOL = new OverTimeWageBOL();
        objBOL.EmployeeId = empid;
        objBOL.Month = nMonth;
        objBOL.Year = nYear;
        DataTable dT = objBAL.SelectAll(objBOL);
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
           dComm += day + end;

        }

        return dComm;
    }
    private static decimal GetDeduction(int empid, int nMonth, int nYear)
    {
        decimal dDed = 0;

        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = empid;
        objBOL.Month = nMonth;
        objBOL.Year = nYear;
        DataTable dT = objBAL.SelectAll(objBOL);
        foreach (DataRow row in dT.Rows)
        {
            //if (Util.ToInt(row["WMon"]) == nMonth && Util.ToInt(row["WYer"]) == nYear)
            dDed += Util.ToDecimal(row["Ded"]);
            
        }

        return dDed;
    }
  
    private static decimal GetLateValue1(int empid, int nMonth, int nYear)
    {
        decimal dDed = 0;
        LeaveBAL objBAL = new LeaveBAL();
        LeaveBOL objBOL = new LeaveBOL();
        objBOL.EmployeeID = empid;
        objBOL.Month = nMonth;
        objBOL.Year = nYear;
        objBOL.Reason = "LateByLeave";
        DataTable dT1 = objBAL.checklatebytime();
        if (dT1.Rows.Count > 0)
        {
            DataSet dSgetlbT = objBAL.Getlatebytype(objBOL);
            DataTable dTdiff = dSgetlbT.Tables[0];
            DataTable DTregH = dSgetlbT.Tables[1];
            DataRow[] dr = DTregH.Select("RegularHours");
            decimal RH = Util.ToDecimal(dr[0]["RegularHours"].ToString());
            object tothr = dTdiff.Compute("Sum(diff)", "");
            decimal toth = Util.ToDecimal(tothr.ToString());
            decimal totm = Util.ToDecimal(tothr.ToString()) * 60;
            decimal LV = decimal.Parse(dT1.Rows[0]["LossValue"].ToString());
            if (dT1.Rows[0]["LateByType"].ToString() == "PM")
            {
                if (dT1.Rows[0]["LossType"].ToString() == "L")
                {
                   
                    if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                    {
                        if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                        {
                            objBOL.Reason = "LateByLeave";
                            objBAL.Save(objBOL);
                        }
                    }
                }
                else if (dT1.Rows[0]["LossType"].ToString() == "LOP")
                {
                   
                    if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                    {
                        if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                        {
                           dDed=toth*RH*LV;
                        }
                    }

                }
            }
            else
            {
                if (dT1.Rows[0]["LossType"].ToString() == "L")
                {

                    if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                    {
                        if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                        {
                            objBOL.Reason = "LateByLeave";
                            objBAL.Save(objBOL);
                        }
                    }
                }
                else if (dT1.Rows[0]["LossType"].ToString() == "LOP")
                {
                    decimal dComm1 = 0;
                    if (dT1.Rows[0]["LateByTime"].ToString() == "H" || dT1.Rows[0]["LateByTime"].ToString() == "M")
                    {
                        if (toth > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()) || totm > decimal.Parse(dT1.Rows[0]["LateByValue"].ToString()))
                        {
                            foreach (DataRow row in dTdiff.Rows)
                            {
                                dComm1 = Util.ToDecimal(row["diff"]);
                                dDed  += dComm1 * RH * LV;
                            }

                           
                        }
                    }

                }

            }
        }

        return dDed;
    }
    private static decimal GetLeaveTaken(int empid, int nMonth, int nYear)
    {
        decimal leavestkn = 0;
        decimal dDedtot = 0;
        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = empid;
        objBOL.Month = nMonth;
        objBOL.Year = nYear;
        DataTable dTable = objBAL.SelectLeaveType(objBOL);
        DataTable MinDT = objBAL.SelectMinLeav(objBOL);
        foreach (DataRow row in dTable.Rows)
        {
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if (Util.ToDecimal(row["LeavesTaken"]) != 0)
                {

                    if (Util.ToDecimal(row["LeavesTaken"]) > (Util.ToDecimal(row["LeavesBalance"])))
                    {
                        leavestkn = Util.ToDecimal(row["LeavesTaken"]) - Util.ToDecimal(row["LeavesBalance"]);
                        dDedtot += leavestkn * Util.ToDecimal(row["RegularWage"]);
                        flag = true;
                    }
                    else
                    {
                        if (Util.ToDecimal(MinDT.Rows[0]["MinimumLeaves"].ToString()) > 0)
                        {
                            if (Util.ToDecimal(row["TotLTaken"]) > (Util.ToDecimal(row["MinimumLeaves"])))
                            {
                                flag = false;
                                leavestkn = Util.ToDecimal(row["TotLTaken"]) - Util.ToDecimal(row["MinimumLeaves"]);
                                dDedtot += leavestkn * Util.ToDecimal(row["RegularWage"]);
                            }
                        }
                    }
                                            
                }
               
            }
        }

        return dDedtot;
    }
}
