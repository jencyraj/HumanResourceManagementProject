using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using HRM.BOL;
using HRM.BAL;

using iTextSharp.text;
using iTextSharp.text.pdf;
using Ionic.Zip;

public partial class SalarySlipGenerator : System.Web.UI.Page
{

    StringBuilder htmlTable = new StringBuilder(); static int EmployeeID = 0; static int Month = 0; static int Year = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string EmplIDs = "" + Session["EMPLIDS"];
            Month = int.Parse(Request.QueryString["Mon"]);
            Year = int.Parse(Request.QueryString["Yr"]);

            string[] EID = EmplIDs.Split(',');

            for (int i = 0; i < EID.Length; i++)
            {
                LeaveBAL objBAL = new LeaveBAL();
                LeaveBOL objBonus = new LeaveBOL();

                int EmployeeID = Util.ToInt(EID[i]);
                objBonus.EmployeeID = EmployeeID;
                objBonus.Month = Month;
                objBonus.Year = Year;


                DataTable dT = objBAL.SalaryslipGenerate(objBonus);

                PayrollMasterBOL objMaster = new PayrollMasterBOL();
                objMaster.EmployeeId = objBonus.EmployeeID;
                objMaster.DesignationId = 0;
                DataSet dSet = new PayrollTemplateBAL().PayrollTemplateforSalarySlip(objMaster, objBonus.Month, objBonus.Year);

                HTML(dT, dSet);
            }
        }

        string sPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["SALARYSLIP"]) + "slipby" + User.Identity.Name + "\\";

        System.IO.DirectoryInfo objDir = new DirectoryInfo(sPath);
        if (System.IO.Directory.Exists(sPath))
        {
            if (objDir.GetFiles().Length > 0)
            {
                //Zip Files

                ZipFile zip = new ZipFile();

                // add this map file into the "images" directory in the zip archive
                DirectoryInfo f = new DirectoryInfo(sPath);
                FileInfo[] a = f.GetFiles();
                for (int i = 0; i < a.Length; i++)
                {
                    zip.AddFile(sPath + a[i].Name, "salaryslip");
                }
                zip.Save(sPath + "salaryslip.zip");

                String FileName = "salaryslip.zip";
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                /// response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(sPath + FileName);
                response.Flush();
                response.End();
            }
        }
        string CloseScript = "<script>window.open('','_self');this.close();</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "onload", CloseScript);
    }


    public void HTML(DataTable DT, DataSet DS)
    {
        DataTable dtAllowances = DS.Tables[1];
        DataTable dtDeductions = DS.Tables[2];
        decimal dOver = GetOverTime();
        decimal dDED = GetDeduction();
        decimal dDedE = GetLeaveTaken();
        htmlTable.Append("<table border='1' ");
        htmlTable.Append("style='border: solid 1px Silver;height: 250%;width: 75%; font-size: x-small;'>");

        int totalcolumns = 4 + ((dtAllowances.Rows.Count > dtDeductions.Rows.Count) ? dtAllowances.Rows.Count : dtDeductions.Rows.Count);

        if (DT.Rows.Count > 0)
        {
            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + "NEXUS SALARY SHEET" + "</td></tr>");

            htmlTable.Append("<tr><td colspan=\"" + totalcolumns.ToString() + "\">" + "" + "</td></tr>");

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
            htmlTable.Append("<td>Other</td><td>Salary Total</td></tr>");

            htmlTable.Append("<tr><td>" + DT.Rows[0]["BasicSalary"] + "</td><td>" + DT.Rows[0]["Bonus"] + "</td>");
            decimal dBS = decimal.Parse(DT.Rows[0]["BasicSalary"].ToString());
            decimal dBO = decimal.Parse(DT.Rows[0]["Bonus"].ToString());
            decimal dAllw = 0;
            for (int i = 0; i < dtAllowances.Rows.Count; i++)
            {

                if (dtAllowances.Rows.Count > i)
                {
                    DataRow dRow = dtAllowances.Rows[i];

                    htmlTable.Append("<td>" + dRow["AlwAmount"] + "</td>");
                    dAllw = Util.ToDecimal("" + dRow["AlwAmount"]);
                }

            }
            decimal totsal = dBS + dBO + dAllw;
            htmlTable.Append("<td>" + dOver + "</td><td>" + totsal + "</td></tr>");

            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count - dtDeductions.Rows.Count + 2).ToString() + "\">" + "Absence" + "</td>");
            for (int i = 0; i < dtDeductions.Rows.Count; i++)
            {

                if (dtDeductions.Rows.Count > i)
                {
                    DataRow dRow = dtDeductions.Rows[i];

                    htmlTable.Append("<td>" + dRow["DedName"] + "</td>");

                }
            }

            htmlTable.Append("<td>" + "Other" + "</td><td>" + "Total Deductions" + "</td></tr>");
            htmlTable.Append("<tr><td colspan=\"" + (dtAllowances.Rows.Count - dtDeductions.Rows.Count + 2).ToString() + "\">" + dDED + "</td>");
            decimal dDed = 0;
            for (int i = 0; i < dtDeductions.Rows.Count; i++)
            {

                if (dtDeductions.Rows.Count > i)
                {
                    DataRow dRow = dtDeductions.Rows[i];


                    htmlTable.Append("<td>" + dRow["DedAmount"] + "</td>");
                    dDed = Util.ToDecimal("" + dRow["DedAmount"]);
                }
            }
            decimal totalD = dDED + dDed + dDedE;
            decimal Netsal = Math.Abs(totsal) - Math.Abs(totalD);
            htmlTable.Append("<td>" + dDedE + "</td><td>" + totalD + "</td></tr>");
            htmlTable.Append("<tr><td>" + "Signature" + "</td><td colspan=\"2\"></td><td bgcolor=\"#AAAAAA\" colspan=\"" + (totalcolumns - 5).ToString() + "\"></td><td align=\"right\">NET PAID</td><td>" + Netsal + "</td></tr>");
            htmlTable.Append("<tr><td colspan=\"3\">&nbsp;</td><td align=\"right\">Date</td><td colspan=\"" + (totalcolumns - 4).ToString() + "\">" + DateTime.Now.ToString() + "</td></tr>");
            htmlTable.Append("</body>");
            htmlTable.Append("</table>");

            //  PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            ltData.Text = htmlTable.ToString();

            string sExtn = ".pdf";
            string sFilePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["SALARYSLIP"]) + "slipby" + User.Identity.Name + "\\";
            if (!System.IO.Directory.Exists(sFilePath))
                System.IO.Directory.CreateDirectory(sFilePath);

            FileStream fs = new FileStream(sFilePath + "salary_" + DT.Rows[0]["EmpCode"] + "_" + DT.Rows[0]["FirstName"] + sExtn, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
            {

                //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                using (var sr = new StringReader(htmlTable.ToString()))
                {

                    //Parse the HTML
                    htmlWorker.Parse(sr);
                }
            }


            doc.Close();
        }
    }

    private static decimal GetOverTime()
    {
        decimal dComm = 0;
        OverTimeWageBAL objBAL = new OverTimeWageBAL();
        OverTimeWageBOL objBOL = new OverTimeWageBOL();
        objBOL.EmployeeId = EmployeeID;
        objBOL.Month = Month;
        objBOL.Year = Year;
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
    private static decimal GetDeduction()
    {
        decimal dDed = 0;

        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = EmployeeID;
        objBOL.Month = Month;
        objBOL.Year = Year;
        DataTable dT = objBAL.SelectAll(objBOL);
        foreach (DataRow row in dT.Rows)
        {
            //if (Util.ToInt(row["WMon"]) == nMonth && Util.ToInt(row["WYer"]) == nYear)
            dDed += Util.ToDecimal(row["Ded"]);

        }

        return dDed;
    }
    private static decimal GetLeaveTaken()
    {
        decimal leavestkn = 0;
        decimal dDedtot = 0;
        LOPDeductionBAL objBAL = new LOPDeductionBAL();
        LOPDeductionBOL objBOL = new LOPDeductionBOL();
        objBOL.EmployeeId = EmployeeID;
        objBOL.Month = Month;
        objBOL.Year = Year;
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

                    }
                    else
                    {
                        if (Util.ToDecimal(MinDT.Rows[0]["MinimumLeaves"].ToString()) > 0)
                        {
                            if (Util.ToDecimal(row["TotLTaken"]) > (Util.ToDecimal(row["MinimumLeaves"])))
                            {

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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.AppendHeader("content-disposition", "attachment;filename=Salaryslip.xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.ms-excel";

        this.EnableViewState = false;

        Response.Write(ltData.Text);

        Response.End();
    }
    //public string toHTML_Table(DataTable dt)
    //{
    //    if (dt.Rows.Count == 0)
    //        return "";

    //    StringBuilder builder = new StringBuilder();
    //    builder.Append("<html>");
    //    builder.Append("<head>");
    //    builder.Append("<title>");
    //    builder.Append("NEXUS");
    //    builder.Append(Guid.NewGuid().ToString());
    //    builder.Append("</title>");
    //    builder.Append("</head>");
    //    builder.Append("<body>");
    //    builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
    //    builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
    //    builder.Append("<tr align='left' valign='top'>");
    //    foreach (DataColumn c in dt.Columns)
    //    {
    //        builder.Append("<td align='left' valign='top'><b>");
    //        builder.Append(c.ColumnName);

    //        builder.Append("</b></td>");
    //    }
    //    builder.Append("</tr>");
    //    foreach (DataRow r in dt.Rows)
    //    {
    //        builder.Append("<tr align='left' valign='top'>");
    //        foreach (DataColumn c in dt.Columns)
    //        {
    //            builder.Append("<td align='left' valign='top'>");
    //            builder.Append(r[c.ColumnName]);
    //            string cellValue = r[c] != null ? r[c].ToString() : "";
    //            builder.Append(cellValue);
    //            builder.Append("</td>");

    //        }
    //        builder.Append("</tr>");
    //    }
    //    builder.Append("</table>");
    //    builder.Append("</body>");
    //    builder.Append("</html>");

    //    return builder.ToString();

    //}
}