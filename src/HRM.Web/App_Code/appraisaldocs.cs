using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

using HRM.BAL;
using HRM.BOL;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;

/// <summary>
/// Summary description for appraisaldocs
/// </summary>
public class appraisaldocs
{
    public appraisaldocs()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static void GenerateAppraisalSummary(string EmployeeID)
    {
        string AppPeriodID = "";
        DataTable dTable = new EmpAppraisalBAL().SelectLastAppraisal(Util.ToInt(EmployeeID));
        if (dTable.Rows.Count > 0)
            AppPeriodID = "" + dTable.Rows[0]["AppPeriodID"];

        if (AppPeriodID != "")
            GenerateAppraisalSummary(EmployeeID, AppPeriodID);
    }

    public static void GenerateAppraisalSummary(string EmployeeID, string AppPeriodID)
    {
        #region GET DATA TO FILL EXCEL

        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.EmployeeID = Util.ToInt(EmployeeID);

        objBOL = new EmployeeBAL().Select(objBOL);

        DataTable dtReviews = new AppraisalReviewBAL().SelectReviews(objBOL.EmployeeID, Util.ToInt(AppPeriodID));
        DataTable dtSelf = new EmpAppraisalBAL().SelectAll(Util.ToInt(AppPeriodID), objBOL.EmployeeID).Tables[1];

        DataTable dtFinal = new DataTable("REVIEW");
        dtFinal.Columns.Add("COMPTYPEID");
        dtFinal.Columns.Add("COMPTYPE");
        dtFinal.Columns.Add("RATING");
        dtFinal.Columns.Add("WEIGHTAGE");

        DataView view = new DataView(dtSelf);
        DataTable distinctValues = view.ToTable(true, "CompetencyTypeID", "CompetencyType");

        foreach (DataRow dR in distinctValues.Rows)
        {
            DataRow drNew = dtFinal.NewRow();
            drNew[0] = dR["COMPETENCYTYPEID"];
            drNew[1] = dR["COMPETENCYTYPE"];

            decimal dTot = 0;
            for (int i = 0; i < dtSelf.Rows.Count; i++)
            {
                DataRow dRow = dtSelf.Rows[i];
                if ("" + dRow["COMPETENCYTYPEID"] == "" + dR["COMPETENCYTYPEID"])
                {
                    dTot += Math.Round((Util.ToDecimal(dRow["ratingid"]) * Util.ToDecimal(dRow["Weightage"])) / 100, 2);
                }
            }
            drNew[2] = dTot;

            int nTot = Util.ToInt(dtSelf.Compute("SUM(Weightage)", "CompetencyTypeID=" + dR["COMPETENCYTYPEID"]));
            drNew[3] = nTot;

            dtFinal.Rows.Add(drNew);
        }


        DataTable dtReviewers = new DataTable("REVIEWER");
        dtReviewers.Columns.Add("REVIEWERID");
        dtReviewers.Columns.Add("REVIEWER");
        dtReviewers.Columns.Add("COMPTYPEID");
        dtReviewers.Columns.Add("COMPTYPE");
        dtReviewers.Columns.Add("RATING");


        foreach (DataRow dRow in dtReviews.Rows)
        {

            DataTable dtDetails = new AppraisalReviewBAL().Select(Util.ToInt(dRow["ReviewID"])).Tables[1];
            foreach (DataRow dR in distinctValues.Rows)
            {
                DataRow dRW = dtReviewers.NewRow();
                dRW[0] = "" + dRow["ReviewerID"];
                dRW[1] = "" + dRow["Reviewer_Fullname"];
                decimal dTot = 0;
                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    DataRow row = dtDetails.Rows[i];
                    if ("" + row["COMPETENCYTYPEID"] == "" + dR["COMPETENCYTYPEID"])
                    {
                        dTot += Math.Round((Util.ToDecimal(row["ratingid"]) * Util.ToDecimal(row["Weightage"])) / 100, 2);
                    }
                }

                dRW[2] = dR["COMPETENCYTYPEID"];
                dRW[3] = dR["COMPETENCYTYPE"];
                dRW[4] = dTot;
                dtReviewers.Rows.Add(dRW);
            }

        }

        if (dtReviewers.Rows.Count > 0)
        {
            DataView dview = new DataView(dtReviewers);
            DataTable dtValues = dview.ToTable(true, "REVIEWERID", "REVIEWER");

            foreach (DataRow dR in dtValues.Rows)
            {
                string sColName = dR["REVIEWERID"] + "_" + dR["REVIEWER"];
                dtFinal.Columns.Add(sColName);
                for (int i = 0; i < dtReviewers.Rows.Count; i++)
                {
                    DataRow[] dRV = dtReviewers.Select("ReviewerID = " + dR["ReviewerID"]);
                    for (int j = 0; j < dtFinal.Rows.Count; j++)
                    {
                        for (int k = 0; k < dRV.Length; k++)
                        {
                            if ("" + dtFinal.Rows[j]["COMPTYPEID"] == "" + dRV[k]["COMPTYPEID"])
                            {
                                dtFinal.Rows[j][sColName] = dRV[k]["RATING"];
                            }
                        }
                    }
                }
            }
        }

        string LastAppPeriod = "";
        string LastAppScore = "";

        new AppraisalReviewBAL().GetLastAppraisalScore(objBOL.EmployeeID, Util.ToInt(AppPeriodID), out LastAppPeriod, out LastAppScore);
        #endregion

        #region CREATE EXCEL FILE

        int rownum = -1;
        string sText = "EMPLOYEE APPRAISAL DOCUMENT";

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook();
        HSSFSheet xlsSheet = xlsWorkbook.CreateSheet("APPRAISAL SUMMARY");



        HSSFRow xlsfRow = xlsSheet.CreateRow(++rownum);
        xlsfRow.HeightInPoints = 100f;

        HSSFCell xlsfCell = xlsfRow.CreateCell(2);
        xlsfCell.SetCellValue(sText);

        HSSFCellStyle cellfStyle = xlsWorkbook.CreateCellStyle();
        HSSFFont fontH1 = xlsWorkbook.CreateFont();
        fontH1.FontName = HSSFFont.FONT_ARIAL;
        fontH1.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        fontH1.FontHeight = 20 * 15;
        cellfStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellfStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellfStyle.FillForegroundColor = HSSFColor.BROWN.index;
        cellfStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellfStyle.SetFont(fontH1);
        cellfStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsfCell.CellStyle = cellfStyle;

        CellRangeAddress cellRange = new CellRangeAddress(rownum, rownum, 2, 5);
        xlsSheet.AddMergedRegion(cellRange);

        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderRight(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);

        xlsSheet.SetColumnWidth(2, (int)((35 + 0.72) * 256));//C
        xlsSheet.SetColumnWidth(3, (int)((23 + 0.72) * 256));//D
        xlsSheet.SetColumnWidth(4, (int)((23 + 0.72) * 256));//E
        xlsSheet.SetColumnWidth(5, (int)((35 + 0.72) * 256));//F

        /*SECOND ROW FONT*/
        HSSFFont fontH2 = xlsWorkbook.CreateFont();
        fontH2.FontName = HSSFFont.FONT_ARIAL;
        fontH2.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;

        /*Color Font*/
        HSSFFont brownFont = xlsWorkbook.CreateFont();
        brownFont.FontName = HSSFFont.FONT_ARIAL;
        brownFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownFont.Color = HSSFColor.BROWN.index;

        HSSFRow xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        HSSFCellStyle cellStyle = xlsWorkbook.CreateCellStyle();
        cellStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_TOP;
        cellStyle.SetFont(fontH2);
        cellStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellStyle.WrapText = true;

        HSSFCell xlsCell = xlsRow.CreateCell(2);
        sText = "Employee Name : ";
        HSSFRichTextString richString = new HSSFRichTextString(sText + ((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim());
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Current Appraisal Period : ";
        DataTable dtAppPeriod = new AppraisalPeriodBAL().SelectAll(Util.ToInt(AppPeriodID)).Tables[0];
        richString = new HSSFRichTextString(sText + dtAppPeriod.Rows[0]["Description"]);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        xlsSheet.AddMergedRegion(cellRange);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Date : ";
        richString = new HSSFRichTextString(sText + LastAppPeriod);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        xlsCell = xlsRow.CreateCell(2);
        sText = "Position Title : ";
        richString = new HSSFRichTextString(sText + objBOL.Designation.Designation);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Department : ";
        richString = new HSSFRichTextString(sText + objBOL.Department.DepartmentName);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Score : ";
        richString = new HSSFRichTextString(sText + LastAppScore);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        /*Brown Red Color Font*/
        HSSFFont brownredFont = xlsWorkbook.CreateFont();
        brownredFont.FontName = HSSFFont.FONT_ARIAL;
        brownredFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownredFont.Color = HSSFColor.BROWN.RED.index;

        HSSFCellStyle cellcStyle = xlsWorkbook.CreateCellStyle();
        cellcStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellcStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellcStyle.WrapText = true;
        cellcStyle.FillForegroundColor = HSSFColor.ORANGE.index;
        cellcStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellcStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellcStyle.SetFont(brownredFont);

        HSSFCellStyle celllStyle = xlsWorkbook.CreateCellStyle();
        celllStyle.CloneStyleFrom(cellcStyle);
        celllStyle.FillForegroundColor = HSSFColor.WHITE.index;
        celllStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celllStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;

        HSSFCellStyle cellrStyle = xlsWorkbook.CreateCellStyle();
        cellrStyle.CloneStyleFrom(cellcStyle);
        cellrStyle.FillForegroundColor = HSSFColor.WHITE.index;
        cellrStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellrStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;

        for (int j = 3; j < dtFinal.Columns.Count - 1; j++)
        {

            string sRvwr = "";

            if (dtFinal.Columns.Count >= j + 1)
                sRvwr = dtFinal.Columns[j + 1].ColumnName.Split('_')[1];

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 80f;
            xlsCell = xlsRow.CreateCell(2);
            sText = "Reviewer : ";
            richString = new HSSFRichTextString(sText + sRvwr);
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow.CreateCell(3).SetCellValue(" ");
            cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
            NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
            xlsSheet.AddMergedRegion(cellRange);

            xlsCell = xlsRow.CreateCell(5);
            sText = "Joining Date : ";
            richString = new HSSFRichTextString(sText + DateTime.Parse(objBOL.JoiningDate).ToString("dd/MMM/yyyy"));
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 60f;

            xlsCell = xlsRow.CreateCell(2);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("AREA");

            xlsCell = xlsRow.CreateCell(3);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("SELF \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(4);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("ACTUAL \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(5);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("WEIGHTAGE");

            foreach (DataRow dRow in dtFinal.Rows)
            {
                xlsRow = xlsSheet.CreateRow(++rownum);
                xlsRow.HeightInPoints = 40f;

                xlsCell = xlsRow.CreateCell(2);
                xlsCell.CellStyle = celllStyle;
                xlsCell.SetCellValue("" + dRow["COMPTYPE"]);

                xlsCell = xlsRow.CreateCell(3);
                xlsCell.CellStyle = cellrStyle;

                if (sRvwr != "")
                    xlsCell.SetCellValue("" + dRow["RATING"]);
                else
                    xlsCell.SetCellValue("");

                xlsCell = xlsRow.CreateCell(4);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue("" + dRow[j + 1]);

                xlsCell = xlsRow.CreateCell(5);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue(dRow["Weightage"] + "%");
            }
        }

        /*Bold Font*/
        HSSFFont bFont = xlsWorkbook.CreateFont();
        bFont.FontName = HSSFFont.FONT_ARIAL;
        bFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        bFont.Color = HSSFColor.BLACK.index;


        /*White Color Font*/
        HSSFFont whiteFont = xlsWorkbook.CreateFont();
        whiteFont.FontName = HSSFFont.FONT_ARIAL;
        whiteFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        whiteFont.Color = HSSFColor.WHITE.index;
        whiteFont.FontHeight = 20 * 12;

        HSSFCellStyle celloStyle = xlsWorkbook.CreateCellStyle();
        celloStyle.CloneStyleFrom(cellcStyle);
        celloStyle.FillForegroundColor = HSSFColor.BROWN.index;
        celloStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celloStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        celloStyle.SetFont(whiteFont);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 35f;

        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Overall Rating Explanation");

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        xlsSheet.AddMergedRegion(cellRange);

        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Rating Scale");


        HSSFCellStyle cellexStyle1 = xlsWorkbook.CreateCellStyle();
        cellexStyle1.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellexStyle1.WrapText = true;
        cellexStyle1.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderBottom = HSSFCellStyle.BORDER_THICK;

        HSSFCellStyle cellexStyle2 = xlsWorkbook.CreateCellStyle();
        cellexStyle2.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellexStyle2.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellexStyle2.WrapText = true;
        cellexStyle2.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;

        richString = new HSSFRichTextString("Outstanding: This rating is for that rare performance that clearly and consistently exceeds expectations of what is required to effectively achieve performance objectives.The employee demonstrates a consistent level of excellence both as an individual contributor and a team member.The employee requires only high-level direction from manager.The employee makes an extraordinary contribution to the team and organisation.The employee consistently masters and expands ongoing responsibilities of the current postion.");
        richString.ApplyFont(0, ("Outstanding:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("4.26 - 5.0");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Exceeds Requirements: This rating is for highly effective and successful performance that consistently meets and frequently exceeds expectations of what is required to effectively achieve performance objectives.The employee contributes high-quality work in a timely manner and requires little guidance from the manager.The employee makes a very significant contribution to unit goals,solidly fulfilling and exceeding the core perofrmance requirements of the position.");
        richString.ApplyFont(0, ("Exceeds Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("3.51 - 4.25");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Meets All Requirements: This rating is for successful performance that consistently meets all expectations of what is required to effectively achieve performance objectives.Employees performance is fully satisfactory.The employee consistently contributes to team goals with good quality work in a timely manner,requiring periodic guidance from the manager.");
        richString.ApplyFont(0, ("Meets All Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("2.5 - 3.5");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Meets some Requirements: This rating is for performance that does not consistently meet expectations of what is required to effectively achieve performance objectives.  Some expectations are met but not consistently enough to result in fully successful job performance.  This rating indicates a performance problem and requires an action plan.Though the employee may make some contribution to goals,contribution is inconsistent,improvement is needed to raise achievement to agreed upon goals.The employee ususally requires considerable guidance.");
        richString.ApplyFont(0, ("Meets some Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.75 - 2.49");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Unsatisfactory: This rating is for performance that clearly falls short of meeting expectations of what is required to effectively achieve performance objectives. This rating indicates a severe performance problem and requires an action plan.The employees work may contain repeated errors or poor results and may not be completed in a timely manner.The employee reqires detailed guidance and more than an appropriate amount of supervision to complete work..The employee does not amke a contribution to team goals.");
        richString.ApplyFont(0, ("Unsatisfactory:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderBottom(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.0 - 1.74");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        //string sFile = Server.MapPath(ConfigurationManager.AppSettings["appraisaldocs"]) + "\\" + objBOL.EmpCode + ".xls";
        /* using (System.IO.FileStream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         {
             xlsWorkbook.Write(stream);
         }*/

        cellRange = new CellRangeAddress(0, rownum, 0, 1);
        xlsSheet.AddMergedRegion(cellRange);
        cellRange = new CellRangeAddress(0, rownum, 6, 10);
        xlsSheet.AddMergedRegion(cellRange);
        #endregion

        string sTime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
        string sFile = (((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim()).Trim().Replace(" ", "_") + "_" + objBOL.EmpCode + "_" + sTime + ".xls";

        System.IO.MemoryStream file = new System.IO.MemoryStream();
        xlsWorkbook.Write(file);
        xlsWorkbook = null;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", sFile));
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        HttpContext.Current.Response.End();
    }

    public static void GenerateAppraisalReview(string EmployeeID, string AppraisalPeriodID)
    {
        int Empid = Util.ToInt(EmployeeID);
        int AppPeriodID = Util.ToInt(AppraisalPeriodID);

        #region GET DATA TO FILL EXCEL

        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.EmployeeID = Util.ToInt(EmployeeID);

        objBOL = new EmployeeBAL().Select(objBOL);

        DataTable dtReviews = new AppraisalReviewBAL().SelectReviews(objBOL.EmployeeID, Util.ToInt(AppPeriodID));
        DataTable dtSelf = new EmpAppraisalBAL().SelectAll(Util.ToInt(AppPeriodID), objBOL.EmployeeID).Tables[1];

        DataTable dtFinal = new DataTable("REVIEW");
        dtFinal.Columns.Add("COMPTYPEID");
        dtFinal.Columns.Add("COMPTYPE");
        dtFinal.Columns.Add("RATING");
        dtFinal.Columns.Add("WEIGHTAGE");

        DataView view = new DataView(dtSelf);
        DataTable distinctValues = view.ToTable(true, "CompetencyTypeID", "CompetencyType");

        foreach (DataRow dR in distinctValues.Rows)
        {
            DataRow drNew = dtFinal.NewRow();
            drNew[0] = dR["COMPETENCYTYPEID"];
            drNew[1] = dR["COMPETENCYTYPE"];

            decimal dTot = 0;
            for (int i = 0; i < dtSelf.Rows.Count; i++)
            {
                DataRow dRow = dtSelf.Rows[i];
                if ("" + dRow["COMPETENCYTYPEID"] == "" + dR["COMPETENCYTYPEID"])
                {
                    dTot += Math.Round((Util.ToDecimal(dRow["ratingid"]) * Util.ToDecimal(dRow["Weightage"])) / 100, 2);
                }
            }
            drNew[2] = dTot;

            int nTot = Util.ToInt(dtSelf.Compute("SUM(Weightage)", "CompetencyTypeID=" + dR["COMPETENCYTYPEID"]));
            drNew[3] = nTot;

            dtFinal.Rows.Add(drNew);
        }


        DataTable dtReviewers = new DataTable("REVIEWER");
        dtReviewers.Columns.Add("REVIEWERID");
        dtReviewers.Columns.Add("REVIEWER");
        dtReviewers.Columns.Add("COMPTYPEID");
        dtReviewers.Columns.Add("COMPTYPE");
        dtReviewers.Columns.Add("RATING");


        foreach (DataRow dRow in dtReviews.Rows)
        {

            DataTable dtDetails = new AppraisalReviewBAL().Select(Util.ToInt(dRow["ReviewID"])).Tables[1];
            foreach (DataRow dR in distinctValues.Rows)
            {
                DataRow dRW = dtReviewers.NewRow();
                dRW[0] = "" + dRow["ReviewerID"];
                dRW[1] = "" + dRow["Reviewer_Fullname"];
                decimal dTot = 0;
                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    DataRow row = dtDetails.Rows[i];
                    if ("" + row["COMPETENCYTYPEID"] == "" + dR["COMPETENCYTYPEID"])
                    {
                        dTot += Math.Round((Util.ToDecimal(row["ratingid"]) * Util.ToDecimal(row["Weightage"])) / 100, 2);
                    }
                }

                dRW[2] = dR["COMPETENCYTYPEID"];
                dRW[3] = dR["COMPETENCYTYPE"];
                dRW[4] = dTot;
                dtReviewers.Rows.Add(dRW);
            }

        }

        if (dtReviewers.Rows.Count > 0)
        {
            DataView dview = new DataView(dtReviewers);
            DataTable dtValues = dview.ToTable(true, "REVIEWERID", "REVIEWER");

            foreach (DataRow dR in dtValues.Rows)
            {
                string sColName = dR["REVIEWERID"] + "_" + dR["REVIEWER"];
                dtFinal.Columns.Add(sColName);
                for (int i = 0; i < dtReviewers.Rows.Count; i++)
                {
                    DataRow[] dRV = dtReviewers.Select("ReviewerID = " + dR["ReviewerID"]);
                    for (int j = 0; j < dtFinal.Rows.Count; j++)
                    {
                        for (int k = 0; k < dRV.Length; k++)
                        {
                            if ("" + dtFinal.Rows[j]["COMPTYPEID"] == "" + dRV[k]["COMPTYPEID"])
                            {
                                dtFinal.Rows[j][sColName] = dRV[k]["RATING"];
                            }
                        }
                    }
                }
            }
        }

        string LastAppPeriod = "";
        string LastAppScore = "";

        new AppraisalReviewBAL().GetLastAppraisalScore(objBOL.EmployeeID, Util.ToInt(AppPeriodID), out LastAppPeriod, out LastAppScore);
        #endregion

        #region CREATE EXCEL FILE

        int rownum = -1;
        string sText = "EMPLOYEE APPRAISAL DOCUMENT";

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook();
        HSSFSheet xlsSheet = xlsWorkbook.CreateSheet("APPRAISAL SUMMARY");



        HSSFRow xlsfRow = xlsSheet.CreateRow(++rownum);
        xlsfRow.HeightInPoints = 100f;

        HSSFCell xlsfCell = xlsfRow.CreateCell(2);
        xlsfCell.SetCellValue(sText);

        HSSFCellStyle cellfStyle = xlsWorkbook.CreateCellStyle();
        HSSFFont fontH1 = xlsWorkbook.CreateFont();
        fontH1.FontName = HSSFFont.FONT_ARIAL;
        fontH1.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        fontH1.FontHeight = 20 * 15;
        cellfStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellfStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellfStyle.FillForegroundColor = HSSFColor.BROWN.index;
        cellfStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellfStyle.SetFont(fontH1);
        cellfStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsfCell.CellStyle = cellfStyle;

        CellRangeAddress cellRange = new CellRangeAddress(rownum, rownum, 2, 5);
        xlsSheet.AddMergedRegion(cellRange);

        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderRight(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);

        xlsSheet.SetColumnWidth(2, (int)((35 + 0.72) * 256));//C
        xlsSheet.SetColumnWidth(3, (int)((23 + 0.72) * 256));//D
        xlsSheet.SetColumnWidth(4, (int)((23 + 0.72) * 256));//E
        xlsSheet.SetColumnWidth(5, (int)((35 + 0.72) * 256));//F

        /*SECOND ROW FONT*/
        HSSFFont fontH2 = xlsWorkbook.CreateFont();
        fontH2.FontName = HSSFFont.FONT_ARIAL;
        fontH2.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;

        /*Color Font*/
        HSSFFont brownFont = xlsWorkbook.CreateFont();
        brownFont.FontName = HSSFFont.FONT_ARIAL;
        brownFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownFont.Color = HSSFColor.BROWN.index;

        HSSFRow xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        HSSFCellStyle cellStyle = xlsWorkbook.CreateCellStyle();
        cellStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_TOP;
        cellStyle.SetFont(fontH2);
        cellStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellStyle.WrapText = true;

        HSSFCell xlsCell = xlsRow.CreateCell(2);
        sText = "Employee Name : ";
        HSSFRichTextString richString = new HSSFRichTextString(sText + ((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim());
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Current Appraisal Period : ";
        DataTable dtAppPeriod = new AppraisalPeriodBAL().SelectAll(Util.ToInt(AppPeriodID)).Tables[0];
        richString = new HSSFRichTextString(sText + dtAppPeriod.Rows[0]["Description"]);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        xlsSheet.AddMergedRegion(cellRange);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Date : ";
        richString = new HSSFRichTextString(sText + LastAppPeriod);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        xlsCell = xlsRow.CreateCell(2);
        sText = "Position Title : ";
        richString = new HSSFRichTextString(sText + objBOL.Designation.Designation);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Department : ";
        richString = new HSSFRichTextString(sText + objBOL.Department.DepartmentName);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Score : ";
        richString = new HSSFRichTextString(sText + LastAppScore);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        /*Brown Red Color Font*/
        HSSFFont brownredFont = xlsWorkbook.CreateFont();
        brownredFont.FontName = HSSFFont.FONT_ARIAL;
        brownredFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownredFont.Color = HSSFColor.BROWN.RED.index;

        HSSFCellStyle cellcStyle = xlsWorkbook.CreateCellStyle();
        cellcStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellcStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellcStyle.WrapText = true;
        cellcStyle.FillForegroundColor = HSSFColor.ORANGE.index;
        cellcStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellcStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellcStyle.SetFont(brownredFont);

        HSSFCellStyle celllStyle = xlsWorkbook.CreateCellStyle();
        celllStyle.CloneStyleFrom(cellcStyle);
        celllStyle.FillForegroundColor = HSSFColor.WHITE.index;
        celllStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celllStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;

        HSSFCellStyle cellrStyle = xlsWorkbook.CreateCellStyle();
        cellrStyle.CloneStyleFrom(cellcStyle);
        cellrStyle.FillForegroundColor = HSSFColor.WHITE.index;
        cellrStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellrStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;

        for (int j = 3; j < dtFinal.Columns.Count - 1; j++)
        {

            string sRvwr = "";

            if (dtFinal.Columns.Count >= j + 1)
                sRvwr = dtFinal.Columns[j + 1].ColumnName.Split('_')[1];

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 80f;
            xlsCell = xlsRow.CreateCell(2);
            sText = "Reviewer : ";
            richString = new HSSFRichTextString(sText + sRvwr);
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow.CreateCell(3).SetCellValue(" ");
            cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
            NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
            xlsSheet.AddMergedRegion(cellRange);

            xlsCell = xlsRow.CreateCell(5);
            sText = "Joining Date : ";
            richString = new HSSFRichTextString(sText + DateTime.Parse(objBOL.JoiningDate).ToString("dd/MMM/yyyy"));
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 60f;

            xlsCell = xlsRow.CreateCell(2);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("AREA");

            xlsCell = xlsRow.CreateCell(3);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("SELF \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(4);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("ACTUAL \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(5);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("WEIGHTAGE");

            foreach (DataRow dRow in dtFinal.Rows)
            {
                xlsRow = xlsSheet.CreateRow(++rownum);
                xlsRow.HeightInPoints = 40f;

                xlsCell = xlsRow.CreateCell(2);
                xlsCell.CellStyle = celllStyle;
                xlsCell.SetCellValue("" + dRow["COMPTYPE"]);

                xlsCell = xlsRow.CreateCell(3);
                xlsCell.CellStyle = cellrStyle;

                if (sRvwr != "")
                    xlsCell.SetCellValue("" + dRow["RATING"]);
                else
                    xlsCell.SetCellValue("");

                xlsCell = xlsRow.CreateCell(4);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue("" + dRow[j + 1]);

                xlsCell = xlsRow.CreateCell(5);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue(dRow["Weightage"] + "%");
            }
        }

        /*Bold Font*/
        HSSFFont bFont = xlsWorkbook.CreateFont();
        bFont.FontName = HSSFFont.FONT_ARIAL;
        bFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        bFont.Color = HSSFColor.BLACK.index;


        /*White Color Font*/
        HSSFFont whiteFont = xlsWorkbook.CreateFont();
        whiteFont.FontName = HSSFFont.FONT_ARIAL;
        whiteFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        whiteFont.Color = HSSFColor.WHITE.index;
        whiteFont.FontHeight = 20 * 12;

        HSSFCellStyle celloStyle = xlsWorkbook.CreateCellStyle();
        celloStyle.CloneStyleFrom(cellcStyle);
        celloStyle.FillForegroundColor = HSSFColor.BROWN.index;
        celloStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celloStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        celloStyle.SetFont(whiteFont);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 35f;

        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Overall Rating Explanation");

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        xlsSheet.AddMergedRegion(cellRange);

        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Rating Scale");


        HSSFCellStyle cellexStyle1 = xlsWorkbook.CreateCellStyle();
        cellexStyle1.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellexStyle1.WrapText = true;
        cellexStyle1.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderBottom = HSSFCellStyle.BORDER_THICK;

        HSSFCellStyle cellexStyle2 = xlsWorkbook.CreateCellStyle();
        cellexStyle2.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellexStyle2.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellexStyle2.WrapText = true;
        cellexStyle2.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;

        richString = new HSSFRichTextString("Outstanding: This rating is for that rare performance that clearly and consistently exceeds expectations of what is required to effectively achieve performance objectives.The employee demonstrates a consistent level of excellence both as an individual contributor and a team member.The employee requires only high-level direction from manager.The employee makes an extraordinary contribution to the team and organisation.The employee consistently masters and expands ongoing responsibilities of the current postion.");
        richString.ApplyFont(0, ("Outstanding:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("4.26 - 5.0");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Exceeds Requirements: This rating is for highly effective and successful performance that consistently meets and frequently exceeds expectations of what is required to effectively achieve performance objectives.The employee contributes high-quality work in a timely manner and requires little guidance from the manager.The employee makes a very significant contribution to unit goals,solidly fulfilling and exceeding the core perofrmance requirements of the position.");
        richString.ApplyFont(0, ("Exceeds Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("3.51 - 4.25");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Meets All Requirements: This rating is for successful performance that consistently meets all expectations of what is required to effectively achieve performance objectives.Employees performance is fully satisfactory.The employee consistently contributes to team goals with good quality work in a timely manner,requiring periodic guidance from the manager.");
        richString.ApplyFont(0, ("Meets All Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("2.5 - 3.5");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Meets some Requirements: This rating is for performance that does not consistently meet expectations of what is required to effectively achieve performance objectives.  Some expectations are met but not consistently enough to result in fully successful job performance.  This rating indicates a performance problem and requires an action plan.Though the employee may make some contribution to goals,contribution is inconsistent,improvement is needed to raise achievement to agreed upon goals.The employee ususally requires considerable guidance.");
        richString.ApplyFont(0, ("Meets some Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.75 - 2.49");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Unsatisfactory: This rating is for performance that clearly falls short of meeting expectations of what is required to effectively achieve performance objectives. This rating indicates a severe performance problem and requires an action plan.The employees work may contain repeated errors or poor results and may not be completed in a timely manner.The employee reqires detailed guidance and more than an appropriate amount of supervision to complete work..The employee does not amke a contribution to team goals.");
        richString.ApplyFont(0, ("Unsatisfactory:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderBottom(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.0 - 1.74");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        //string sFile = Server.MapPath(ConfigurationManager.AppSettings["appraisaldocs"]) + "\\" + objBOL.EmpCode + ".xls";
        /* using (System.IO.FileStream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         {
             xlsWorkbook.Write(stream);
         }*/

        cellRange = new CellRangeAddress(0, rownum, 0, 1);
        xlsSheet.AddMergedRegion(cellRange);
        cellRange = new CellRangeAddress(0, rownum, 6, 10);
        xlsSheet.AddMergedRegion(cellRange);
        #endregion

        #region CREATE SECOND SHEET

        foreach (DataRow dRow in dtReviews.Rows)
        {
            dtSelf.Columns.Add(("" + dRow["reviewer_fullname"]).ToString().Replace(" ", "_"));
            dtSelf.Columns.Add(("" + dRow["reviewer_fullname"]).ToString().Replace(" ", "_") + "_rating");
        }

        foreach (DataRow dRow in dtReviews.Rows)
        {
            DataSet dSet = new AppraisalReviewBAL().Select(Util.ToInt(dRow["reviewid"]));

            if (dSet.Tables[0].Rows.Count > 0)
            {
                string sColName = ("" + dSet.Tables[0].Rows[0]["reviewer_fullname"]).ToString().Replace(" ", "_");
                for (int i = 0; i < dSet.Tables[1].Rows.Count; i++)
                {
                    DataRow dRV = dSet.Tables[1].Rows[i];
                    for (int j = 0; j < dtSelf.Rows.Count; j++)
                    {
                        if ("" + dRV["CompetencyID"] == "" + dtSelf.Rows[j]["CompetencyID"])
                        {
                            dtSelf.Rows[j][sColName] = dRV["Comments"];
                            dtSelf.Rows[j][sColName + "_rating"] = dRV["RatingID"];
                        }
                    }
                }
            }

        }

        int startindex = 0;
        for (int i = 0; i < dtSelf.Columns.Count; i++)
        {
            if (dtSelf.Columns[i].ColumnName.IndexOf("_rating") > -1)
                startindex = i - 1;
        }

        rownum = -1;

        xlsSheet = xlsWorkbook.CreateSheet("APPRAISAL REVIEW DETAILS");

        int dcol = -1;

        HSSFFont whitetFont = xlsWorkbook.CreateFont();
        whitetFont.FontName = HSSFFont.FONT_ARIAL;
        whitetFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        whitetFont.Color = HSSFColor.WHITE.index;
        whitetFont.FontHeight = 18 * 12;

        HSSFCellStyle hStyle = xlsWorkbook.CreateCellStyle();
        hStyle.CloneStyleFrom(cellcStyle);
        hStyle.FillForegroundColor = HSSFColor.BROWN.index;
        hStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        hStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        hStyle.SetFont(whitetFont);
        hStyle.WrapText = true;

        HSSFCellStyle iStyle = xlsWorkbook.CreateCellStyle();
        iStyle.WrapText = true;

        HSSFCellStyle icStyle = xlsWorkbook.CreateCellStyle();
        icStyle.WrapText = true;
        icStyle.Alignment =HSSFCellStyle.ALIGN_CENTER ;
        
        HSSFRow xlssRow = xlsSheet.CreateRow(++rownum);
        xlssRow.HeightInPoints = 20f;
       

        int nPos = -1;

        for (int i = 0; i < 9; i++)
        {
            HSSFCell xlCell = xlssRow.CreateCell(++dcol);
            xlsSheet.SetColumnWidth(++nPos, (int)((35 + 0.72) * 256));
            xlCell.CellStyle = hStyle;
            if (i == 1)
                xlCell.SetCellValue("RATING DESCRIPTION");
        }

        CellRangeAddress cellrange = new CellRangeAddress(0, 0, 1, 5);
        xlsSheet.AddMergedRegion(cellrange);

        xlsSheet.SetColumnWidth(6, (int)((25 + 0.72) * 256));
        xlsSheet.SetColumnWidth(7, (int)((25 + 0.72) * 256));

        for (int i = startindex; i < dtSelf.Columns.Count; i++)
        {
            HSSFCell xlCell = xlssRow.CreateCell(++dcol);
            xlsSheet.SetColumnWidth(++nPos, (int)((35 + 0.72) * 256));
            xlCell.SetCellValue("Reviewer : " + dtSelf.Columns[i].ColumnName.Replace("_", " "));

              cellrange = new CellRangeAddress(0, 0, dcol, dcol + 1);
            xlsSheet.AddMergedRegion(cellrange);
            xlCell.CellStyle = hStyle;
            ++i;
        }

        xlssRow = xlsSheet.CreateRow(++rownum);
        xlssRow.HeightInPoints = 20f;

        dcol = -1;

        HSSFCell xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("COMPETENCY");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("1");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("2");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("3");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("4");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("5");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("WEIGHTAGE");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("EMPLOYEE RATING");
        xlssCell.CellStyle = hStyle;
        xlssCell = xlssRow.CreateCell(++dcol);
        xlssCell.SetCellValue("EMPLOYEE COMMENTS");
        xlssCell.CellStyle = hStyle;

        nPos = 8;
        for (int i = startindex; i < dtSelf.Columns.Count; i++)
        {
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("COMMENTS");
            xlsSheet.SetColumnWidth(++nPos, (int)((35 + 0.72) * 256));
            xlssCell.CellStyle = hStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("RATING");
            xlsSheet.SetColumnWidth(++nPos, (int)((25 + 0.72) * 256));
            xlssCell.CellStyle = hStyle;
            ++i;
        }

        for (int i = 0; i < dtSelf.Rows.Count; i++)
        {
            dcol = -1;
            xlssRow = xlsSheet.CreateRow(++rownum);
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["CompetencyName"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingDesc1"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingDesc2"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingDesc3"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingDesc4"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingDesc5"]);
            xlssCell.CellStyle = iStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue(dtSelf.Rows[i]["Weightage"] + "%");
            xlssCell.CellStyle = icStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["RatingID"]);
            xlssCell.CellStyle = icStyle;
            xlssCell = xlssRow.CreateCell(++dcol);
            xlssCell.SetCellValue("" + dtSelf.Rows[i]["Comments"]);
            xlssCell.CellStyle = iStyle;

            for (int j = startindex; j < dtSelf.Columns.Count; j++)
            {
                xlssCell = xlssRow.CreateCell(++dcol);
                xlssCell.SetCellValue("" + dtSelf.Rows[i][j]);
                xlssCell.CellStyle = iStyle;
                xlssCell = xlssRow.CreateCell(++dcol);
                xlssCell.SetCellValue("" + dtSelf.Rows[i][++j]);
                xlssCell.CellStyle = icStyle;
            }
        }
        #endregion

        string sTime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
        string sFile = (((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim()).Trim().Replace(" ", "_") + "_" + objBOL.EmpCode + "_" + sTime + ".xls";

        System.IO.MemoryStream file = new System.IO.MemoryStream();
        xlsWorkbook.Write(file);
        xlsWorkbook = null;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", sFile));
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        HttpContext.Current.Response.End();
    }
    public static void GenerateAppraisalReview1(string EmployeeID, string AppraisalPeriodID)
    {
        int Empid = Util.ToInt(EmployeeID);
        int AppPeriodID = Util.ToInt(AppraisalPeriodID);

        DataTable dT = new EmpAppraisalBAL().SelectAll(AppPeriodID, Empid).Tables[1]; //Employee Appraisal Details
        DataTable dtReview = new AppraisalReviewBAL().SelectReviews(Empid, AppPeriodID); // All Review MAster Details

        DataTable dtFinal = new DataTable("REVIEW");
        dtFinal.Columns.Add("COMPTYPEID");
        dtFinal.Columns.Add("COMPTYPE");
        dtFinal.Columns.Add("RATING");
        dtFinal.Columns.Add("WEIGHTAGE");

        DataView view = new DataView(dT);
        DataTable distinctValues = view.ToTable(true, "CompetencyTypeID", "CompetencyType");

        foreach (DataRow dR in distinctValues.Rows)
        {
            DataRow drNew = dtFinal.NewRow();
            drNew[0] = dR["COMPETENCYTYPEID"];
            drNew[1] = dR["COMPETENCYTYPE"];

            decimal dTot = 0;
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                DataRow dRow = dT.Rows[i];
                if ("" + dRow["COMPETENCYTYPEID"] == "" + dR["COMPETENCYTYPEID"])
                {
                    dTot += Math.Round((Util.ToDecimal(dRow["ratingid"]) * Util.ToDecimal(dRow["Weightage"])) / 100, 2);
                }
            }
            drNew[2] = dTot;

            int nTot = Util.ToInt(dT.Compute("SUM(Weightage)", "CompetencyTypeID=" + dR["COMPETENCYTYPEID"]));
            drNew[3] = nTot;

            dtFinal.Rows.Add(drNew);
        }

        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.EmployeeID = Util.ToInt(EmployeeID);
        new EmployeeBAL().Select(objBOL);

        foreach (DataRow dRow in dtReview.Rows)
        {
            dT.Columns.Add(("" + dRow["reviewer_fullname"]).ToString().Replace(" ", "_"));
            dT.Columns.Add(("" + dRow["reviewer_fullname"]).ToString().Replace(" ", "_") + "_rating");
        }

        foreach (DataRow dRow in dtReview.Rows)
        {
            DataSet dSet = new AppraisalReviewBAL().Select(Util.ToInt(dRow["reviewid"]));

            if (dSet.Tables[0].Rows.Count > 0)
            {
                string sColName = ("" + dSet.Tables[0].Rows[0]["reviewer_fullname"]).ToString().Replace(" ", "_");
                for (int i = 0; i < dSet.Tables[1].Rows.Count; i++)
                {
                    DataRow dRV = dSet.Tables[1].Rows[i];
                    for (int j = 0; j < dT.Rows.Count; j++)
                    {
                        if ("" + dRV["CompetencyID"] == "" + dT.Rows[j]["CompetencyID"])
                        {
                            dT.Rows[j][sColName] = dRV["Comments"];
                            dT.Rows[j][sColName + "_rating"] = dRV["RatingID"];
                        }
                    }
                }
            }

        }

        string LastAppPeriod = "";
        string LastAppScore = "";

        new AppraisalReviewBAL().GetLastAppraisalScore(objBOL.EmployeeID, Util.ToInt(AppPeriodID), out LastAppPeriod, out LastAppScore);

        #region CREATE EXCEL FILE

        int rownum = -1;
        string sText = "EMPLOYEE APPRAISAL DOCUMENT";

        HSSFWorkbook xlsWorkbook = new HSSFWorkbook();
        HSSFSheet xlsSheet = xlsWorkbook.CreateSheet("APPRAISAL SUMMARY");



        HSSFRow xlsfRow = xlsSheet.CreateRow(++rownum);
        xlsfRow.HeightInPoints = 100f;

        HSSFCell xlsfCell = xlsfRow.CreateCell(2);
        xlsfCell.SetCellValue(sText);

        HSSFCellStyle cellfStyle = xlsWorkbook.CreateCellStyle();
        HSSFFont fontH1 = xlsWorkbook.CreateFont();
        fontH1.FontName = HSSFFont.FONT_ARIAL;
        fontH1.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        fontH1.FontHeight = 20 * 15;
        cellfStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellfStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellfStyle.FillForegroundColor = HSSFColor.BROWN.index;
        cellfStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellfStyle.SetFont(fontH1);
        cellfStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellfStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsfCell.CellStyle = cellfStyle;

        CellRangeAddress cellRange = new CellRangeAddress(rownum, rownum, 2, 5);
        xlsSheet.AddMergedRegion(cellRange);

        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderRight(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);

        xlsSheet.SetColumnWidth(2, (int)((35 + 0.72) * 256));//C
        xlsSheet.SetColumnWidth(3, (int)((23 + 0.72) * 256));//D
        xlsSheet.SetColumnWidth(4, (int)((23 + 0.72) * 256));//E
        xlsSheet.SetColumnWidth(5, (int)((35 + 0.72) * 256));//F

        /*SECOND ROW FONT*/
        HSSFFont fontH2 = xlsWorkbook.CreateFont();
        fontH2.FontName = HSSFFont.FONT_ARIAL;
        fontH2.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;

        /*Color Font*/
        HSSFFont brownFont = xlsWorkbook.CreateFont();
        brownFont.FontName = HSSFFont.FONT_ARIAL;
        brownFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownFont.Color = HSSFColor.BROWN.index;

        HSSFRow xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        HSSFCellStyle cellStyle = xlsWorkbook.CreateCellStyle();
        cellStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_TOP;
        cellStyle.SetFont(fontH2);
        cellStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellStyle.WrapText = true;

        HSSFCell xlsCell = xlsRow.CreateCell(2);
        sText = "Employee Name : ";
        HSSFRichTextString richString = new HSSFRichTextString(sText + ((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim());
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Current Appraisal Period : ";
        DataTable dtAppPeriod = new AppraisalPeriodBAL().SelectAll(Util.ToInt(AppPeriodID)).Tables[0];
        richString = new HSSFRichTextString(sText + dtAppPeriod.Rows[0]["Description"]);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        xlsSheet.AddMergedRegion(cellRange);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Date : ";
        richString = new HSSFRichTextString(sText + LastAppPeriod);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 80f;

        xlsCell = xlsRow.CreateCell(2);
        sText = "Position Title : ";
        richString = new HSSFRichTextString(sText + objBOL.Designation.Designation);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(3);
        sText = "Department : ";
        richString = new HSSFRichTextString(sText + objBOL.Department.DepartmentName);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        xlsCell.CellStyle = cellStyle;

        xlsCell = xlsRow.CreateCell(5);
        sText = "Last Appraisal Score : ";
        richString = new HSSFRichTextString(sText + LastAppScore);
        richString.ApplyFont(sText.Length, richString.Length, brownFont);
        xlsCell.SetCellValue(richString);
        xlsCell.CellStyle = cellStyle;


        /*Brown Red Color Font*/
        HSSFFont brownredFont = xlsWorkbook.CreateFont();
        brownredFont.FontName = HSSFFont.FONT_ARIAL;
        brownredFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        brownredFont.Color = HSSFColor.BROWN.RED.index;

        HSSFCellStyle cellcStyle = xlsWorkbook.CreateCellStyle();
        cellcStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellcStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellcStyle.WrapText = true;
        cellcStyle.FillForegroundColor = HSSFColor.ORANGE.index;
        cellcStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellcStyle.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellcStyle.BorderBottom = HSSFCellStyle.BORDER_THICK;
        cellcStyle.SetFont(brownredFont);

        HSSFCellStyle celllStyle = xlsWorkbook.CreateCellStyle();
        celllStyle.CloneStyleFrom(cellcStyle);
        celllStyle.FillForegroundColor = HSSFColor.WHITE.index;
        celllStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celllStyle.Alignment = HSSFCellStyle.ALIGN_LEFT;

        HSSFCellStyle cellrStyle = xlsWorkbook.CreateCellStyle();
        cellrStyle.CloneStyleFrom(cellcStyle);
        cellrStyle.FillForegroundColor = HSSFColor.WHITE.index;
        cellrStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        cellrStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;

        for (int j = 3; j < dT.Columns.Count - 1; j++)
        {

            string sRvwr = "";

            if (dtFinal.Columns.Count >= j + 1)
                sRvwr = dT.Columns[j + 1].ColumnName.Split('_')[1];

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 80f;
            xlsCell = xlsRow.CreateCell(2);
            sText = "Reviewer : ";
            richString = new HSSFRichTextString(sText + sRvwr);
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow.CreateCell(3).SetCellValue(" ");
            cellRange = new CellRangeAddress(rownum, rownum, 3, 4);
            NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
            xlsSheet.AddMergedRegion(cellRange);

            xlsCell = xlsRow.CreateCell(5);
            sText = "Joining Date : ";
            richString = new HSSFRichTextString(sText + DateTime.Parse(objBOL.JoiningDate).ToString("dd/MMM/yyyy"));
            richString.ApplyFont(sText.Length, richString.Length, brownFont);
            xlsCell.SetCellValue(richString);
            xlsCell.CellStyle = cellStyle;

            xlsRow = xlsSheet.CreateRow(++rownum);
            xlsRow.HeightInPoints = 60f;

            xlsCell = xlsRow.CreateCell(2);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("AREA");

            xlsCell = xlsRow.CreateCell(3);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("SELF \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(4);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("ACTUAL \nPERFORMANCE \nRATING");

            xlsCell = xlsRow.CreateCell(5);
            xlsCell.CellStyle = cellcStyle;
            xlsCell.SetCellValue("WEIGHTAGE");

            foreach (DataRow dRow in dT.Rows)
            {
                xlsRow = xlsSheet.CreateRow(++rownum);
                xlsRow.HeightInPoints = 40f;

                xlsCell = xlsRow.CreateCell(2);
                xlsCell.CellStyle = celllStyle;
                xlsCell.SetCellValue("" + dRow["COMPTYPE"]);

                xlsCell = xlsRow.CreateCell(3);
                xlsCell.CellStyle = cellrStyle;

                if (sRvwr != "")
                    xlsCell.SetCellValue("" + dRow["RATING"]);
                else
                    xlsCell.SetCellValue("");

                xlsCell = xlsRow.CreateCell(4);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue("" + dRow[j + 1]);

                xlsCell = xlsRow.CreateCell(5);
                xlsCell.CellStyle = cellrStyle;
                xlsCell.SetCellValue(dRow["Weightage"] + "%");
            }
        }

        /*Bold Font*/
        HSSFFont bFont = xlsWorkbook.CreateFont();
        bFont.FontName = HSSFFont.FONT_ARIAL;
        bFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        bFont.Color = HSSFColor.BLACK.index;


        /*White Color Font*/
        HSSFFont whiteFont = xlsWorkbook.CreateFont();
        whiteFont.FontName = HSSFFont.FONT_ARIAL;
        whiteFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
        whiteFont.Color = HSSFColor.WHITE.index;
        whiteFont.FontHeight = 20 * 12;

        HSSFCellStyle celloStyle = xlsWorkbook.CreateCellStyle();
        celloStyle.CloneStyleFrom(cellcStyle);
        celloStyle.FillForegroundColor = HSSFColor.BROWN.index;
        celloStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
        celloStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
        celloStyle.SetFont(whiteFont);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 35f;

        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Overall Rating Explanation");

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        xlsSheet.AddMergedRegion(cellRange);

        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = celloStyle;
        xlsCell.SetCellValue("Rating Scale");


        HSSFCellStyle cellexStyle1 = xlsWorkbook.CreateCellStyle();
        cellexStyle1.Alignment = HSSFCellStyle.ALIGN_LEFT;
        cellexStyle1.WrapText = true;
        cellexStyle1.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle1.BorderBottom = HSSFCellStyle.BORDER_THICK;

        HSSFCellStyle cellexStyle2 = xlsWorkbook.CreateCellStyle();
        cellexStyle2.Alignment = HSSFCellStyle.ALIGN_CENTER;
        cellexStyle2.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
        cellexStyle2.WrapText = true;
        cellexStyle2.BorderLeft = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderTop = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderRight = HSSFCellStyle.BORDER_THICK;
        cellexStyle2.BorderBottom = HSSFCellStyle.BORDER_THICK;

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;

        richString = new HSSFRichTextString("Outstanding: This rating is for that rare performance that clearly and consistently exceeds expectations of what is required to effectively achieve performance objectives.The employee demonstrates a consistent level of excellence both as an individual contributor and a team member.The employee requires only high-level direction from manager.The employee makes an extraordinary contribution to the team and organisation.The employee consistently masters and expands ongoing responsibilities of the current postion.");
        richString.ApplyFont(0, ("Outstanding:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("4.26 - 5.0");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Exceeds Requirements: This rating is for highly effective and successful performance that consistently meets and frequently exceeds expectations of what is required to effectively achieve performance objectives.The employee contributes high-quality work in a timely manner and requires little guidance from the manager.The employee makes a very significant contribution to unit goals,solidly fulfilling and exceeding the core perofrmance requirements of the position.");
        richString.ApplyFont(0, ("Exceeds Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);

        richString = new HSSFRichTextString("3.51 - 4.25");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 75f;
        richString = new HSSFRichTextString("Meets All Requirements: This rating is for successful performance that consistently meets all expectations of what is required to effectively achieve performance objectives.Employees performance is fully satisfactory.The employee consistently contributes to team goals with good quality work in a timely manner,requiring periodic guidance from the manager.");
        richString.ApplyFont(0, ("Meets All Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("2.5 - 3.5");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Meets some Requirements: This rating is for performance that does not consistently meet expectations of what is required to effectively achieve performance objectives.  Some expectations are met but not consistently enough to result in fully successful job performance.  This rating indicates a performance problem and requires an action plan.Though the employee may make some contribution to goals,contribution is inconsistent,improvement is needed to raise achievement to agreed upon goals.The employee ususally requires considerable guidance.");
        richString.ApplyFont(0, ("Meets some Requirements:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.75 - 2.49");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        xlsRow = xlsSheet.CreateRow(++rownum);
        xlsRow.HeightInPoints = 85f;
        richString = new HSSFRichTextString("Unsatisfactory: This rating is for performance that clearly falls short of meeting expectations of what is required to effectively achieve performance objectives. This rating indicates a severe performance problem and requires an action plan.The employees work may contain repeated errors or poor results and may not be completed in a timely manner.The employee reqires detailed guidance and more than an appropriate amount of supervision to complete work..The employee does not amke a contribution to team goals.");
        richString.ApplyFont(0, ("Unsatisfactory:").Length, bFont);
        xlsCell = xlsRow.CreateCell(2);
        xlsCell.CellStyle = cellexStyle1;
        xlsCell.SetCellValue(richString);

        cellRange = new CellRangeAddress(rownum, rownum, 2, 4);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderTop(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        NPOI.HSSF.UserModel.Contrib.HSSFRegionUtil.SetBorderBottom(HSSFCellStyle.BORDER_THICK, cellRange, xlsSheet, xlsWorkbook);
        xlsSheet.AddMergedRegion(cellRange);
        richString = new HSSFRichTextString("1.0 - 1.74");
        richString.ApplyFont(bFont);
        xlsCell = xlsRow.CreateCell(5);
        xlsCell.CellStyle = cellexStyle2;
        xlsCell.SetCellValue(richString);

        //string sFile = Server.MapPath(ConfigurationManager.AppSettings["appraisaldocs"]) + "\\" + objBOL.EmpCode + ".xls";
        /* using (System.IO.FileStream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create, System.IO.FileAccess.Write))
         {
             xlsWorkbook.Write(stream);
         }*/

        cellRange = new CellRangeAddress(0, rownum, 0, 1);
        xlsSheet.AddMergedRegion(cellRange);
        cellRange = new CellRangeAddress(0, rownum, 6, 10);
        xlsSheet.AddMergedRegion(cellRange);
        #endregion

        string sTime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
        string sFile = (((objBOL.FirstName + " " + objBOL.MiddleName).Trim() + " " + objBOL.LastName).Trim()).Trim().Replace(" ", "_") + "_" + objBOL.EmpCode + "_" + sTime + ".xls";

        System.IO.MemoryStream file = new System.IO.MemoryStream();
        xlsWorkbook.Write(file);
        xlsWorkbook = null;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", sFile));
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        HttpContext.Current.Response.End();
    }
}