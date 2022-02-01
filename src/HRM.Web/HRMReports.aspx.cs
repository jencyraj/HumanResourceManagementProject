using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using HRM.BOL;
using HRM.BAL;

public partial class Reports_HRMReports : System.Web.UI.Page
{
    string sp1 = "";
    string sp2 = "";
    string sp3 = "";
    string sp4 = "";
    string sp5 = "";
    string sp6 = "";
    string sp7 = "";
    string sp8 = "";
    string sp9 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sp1 = "" + Request.QueryString["sp1"];
        sp2 = "" + Request.QueryString["sp2"];
        sp3 = "" + Request.QueryString["sp3"];
        sp4 = "" + Request.QueryString["sp4"];
        sp5 = "" + Request.QueryString["sp5"];
        sp6 = "" + Request.QueryString["sp6"];
        sp7 = "" + Request.QueryString["sp7"];
        sp8 = "" + Request.QueryString["sp8"];
        sp9 = "" + Request.QueryString["sp9"];

        ReportDocument rptDoc = new ReportDocument();

        // Your .rpt file path will be below
        // rptDoc.Load(Server.MapPath("../Reports/" + Request.QueryString["rptname"] + ".rpt"));

        if ("" + Session["STYLESHEET"] == "")
            Session["STYLESHEET"] = "LR";

        string rptname = ("" + Session["STYLESHEET"] == "LR") ? Request.QueryString["rptname"] + ".rpt" : Request.QueryString["rptname"] + "Ar" + ".rpt";

        rptDoc.Load(Server.MapPath(rptname));


        bool bLoaded = false;

        ////set dataset to the report viewer.
        if ("" + Request.QueryString["rptcase"] != "")
        {
            bLoaded = GetReportData(rptDoc);
        }
        else
        {
            DataTable dTable = (DataTable)Session["rptdata"];
            dTable = SetStaticData(dTable);
            if (dTable.Rows.Count > 0)
                rptDoc.SetDataSource(dTable);
            else
                bLoaded = false;
        }

        if (!bLoaded) // No data FOUND
        {
            ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('No data found');this.close();</script>");
            return;
        }

        string _printType = Request.QueryString["printtype"];

        if (_printType == "CR")
        {
            CrViewer.SeparatePages = false;
            CrViewer.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;

        }
        else if (_printType == "PDF")
        {
            try
            {
                MemoryStream oStream; // using System.IO
                oStream = (MemoryStream)
                 rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());

                Response.End();
            }
            catch (Exception ex)
            {
              string var=  ex.Message;
            }
        }
        else if (_printType == "MX")
        {
            MemoryStream oStream; // using System.IO
            oStream = (MemoryStream)
            rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }
        else if (_printType == "MW")
        {
            MemoryStream oStream; // using System.IO
            oStream = (MemoryStream)
            rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-word";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }

        CrViewer.ReportSource = rptDoc;

        /* MemoryStream oStream; // using System.IO
         oStream = (MemoryStream)
         rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
         Response.Clear();
         Response.Buffer = true;
         Response.ContentType = "application/pdf";
         Response.BinaryWrite(oStream.ToArray());
         Response.End();*/
    }

    private bool GetReportData(ReportDocument rptDoc)
    {
        bool bLoaded = true;

        DataSet dSet = null;
        DataTable dTable = null;

        switch ("" + Request.QueryString["rptcase"])
        {
            case "TS": // TIMESHEET
                TimeSheetBAL objTime = new TimeSheetBAL();
                dSet = objTime.GetTimeSheet(Util.ToInt(sp1), Util.ToInt(sp2), Util.ToInt(sp3), Util.ToInt(sp4));

                dSet.Tables[0].TableName = "TimeSheet";

                dTable = dSet.Tables[0].Copy();
                dTable.TableName = "TimeSheet";
                dTable = SetStaticData(dTable);
                dTable.Columns.Add("BreakStart");
                dTable.Columns.Add("BreakEnd");
                dTable.Columns.Add("BreakDetails");
                dTable.Columns.Add("OTStart");
                dTable.Columns.Add("OTEnd");
                dTable.Columns.Add("OTDetails");

                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    string sBreak = "";
                    string sBreakStart = "";
                    string sBreakEnd = "";

                    string sOT = "";
                    string sOTStart = "";
                    string sOTEnd = "";

                    DataRow dRow = dTable.Rows[i];

                    DataRow[] dRows = dSet.Tables[1].Select("AttendanceID=" + dRow["AttendanceID"]);

                    if (dRows.Length > 0)
                    {

                        for (int j = 0; j < dRows.Length; j++)
                        {
                            sBreakStart += "<p>" + dRows[j]["StartTime"] + "</p>";
                            sBreakEnd += "<p>" + dRows[j]["EndTime"] + "</p>";
                            sBreak += "<p>" + dRows[j]["Description"] + "</p>";
                        }

                    }

                    dTable.Rows[i]["BreakStart"] = sBreakStart;
                    dTable.Rows[i]["BreakEnd"] = sBreakEnd;
                    dTable.Rows[i]["BreakDetails"] = sBreak;

                    DataRow[] dR = dSet.Tables[2].Select("AttendanceID=" + dRow["AttendanceID"]);

                    if (dR.Length > 0)
                    {

                        for (int j = 0; j < dR.Length; j++)
                        {
                            sOTStart += "<p>" + dR[j]["StartTime"] + "</p>";
                            sOTEnd += "<p>" + dR[j]["EndTime"] + "</p>";
                            sOT += "<p>" + dR[j]["Description"] + "</p>";
                        }
                    }

                    dTable.Rows[i]["OTStart"] = sOTStart;
                    dTable.Rows[i]["OTEnd"] = sOTEnd;
                    dTable.Rows[i]["OTDetails"] = sOT;
                }

                //set dataset to the report viewer.
             //   dTable = SetStaticData(dTable);
              //  if(dTable.Rows.Count>0)
                rptDoc.SetDataSource(dTable);
              //    else
                  //  bLoaded = false;
                break;
              //  break;
            case "LOGINHISTORY":
                DataTable dtHistory = new UserBAL().LoginHistory(sp1, false, Util.ToInt(sp2));
                dtHistory = SetStaticData(dtHistory);
                if (dtHistory.Rows.Count > 0)
                    rptDoc.SetDataSource(dtHistory);
                else
                    bLoaded = false;
                break;
            case "LEAVEAPPROVAL":
                LeaveBAL objBAL = new LeaveBAL();

                DataTable dT = objBAL.SelectAll(new LeaveBOL());
                DataView dView = dT.DefaultView;
                if (sp1 != "")
                    dView.RowFilter = "ApprovalStatus='" + sp1 + "'";
                dTable = dView.ToTable();
                dTable = SetStaticData(dTable);
                if (dTable.Rows.Count > 0)
                    rptDoc.SetDataSource(dTable);
                else
                    bLoaded = false;
                break;
            case "HOLIDAY":
                HolidayBAL obj = new HolidayBAL();
                dTable = obj.SelectAll(Util.ToInt(sp1));
                dTable = SetStaticData(dTable);
                if (dTable.Rows.Count > 0)
                    rptDoc.SetDataSource(dTable);
                else
                    bLoaded = false;
                break;
            case "EMPL":
                EmployeeBAL objEmp = new EmployeeBAL();
                EmployeeBOL objBOL = new EmployeeBOL();

                if (Util.ToInt(sp1) > 0)
                    objBOL.BranchID = int.Parse(sp1);

                if (Util.ToInt(sp2) > 0)
                    objBOL.DeptId = int.Parse(sp2);

                if ("" + sp3 != "")
                    objBOL.EmpStatus = sp3;

                if (Util.ToInt(sp4) > 0)
                    objBOL.RoleID = int.Parse(sp4);

                if (Util.ToInt(sp5) > 0)
                    objBOL.DesgnID = int.Parse(sp5);

                objBOL.FirstName = sp6;
                objBOL.MiddleName = sp7;
                objBOL.LastName = sp8;
                objBOL.EmpCode = sp9;

                DataTable dt = objEmp.Search(objBOL);
                dt = SetStaticData(dt);
                if (dt.Rows.Count > 0)
                    rptDoc.SetDataSource(dt);
                else
                    bLoaded = false;
                break;

            case "TRAININGRPT":

                TrainingBAL objTraining = new TrainingBAL();
                TrainingBOL objCy = new TrainingBOL();

                objCy.trainingtype = Util.ToInt(sp1);

                objCy.title = sp2;
                DataTable dt1 = objTraining.SelectAll(objCy);
                dt1 = SetStaticData(dt1);
                if (dt1.Rows.Count > 0)
                    rptDoc.SetDataSource(dt1);
                else
                    bLoaded = false;
                break;

            case "RECRUITMENT":

                CandidateProfileBAL objBALR = new CandidateProfileBAL();
                CandidateProfileBOL objBOLR = new CandidateProfileBOL();
                if (sp1 != "")
                    objBOLR.JobTitle = sp1;
                if (sp2 != "")
                    objBOLR.AppliedStatus = sp2;
                if (sp3 != "")
                    objBOLR.fromdate = Util.ToDateTime(sp3);
                if (sp4 != "")
                    objBOLR.todate = Util.ToDateTime(sp4);
                DataTable dTABLE = objBALR.Selectcandidatereport(objBOLR);
                dTABLE = SetStaticData(dTABLE);

                if (dTABLE.Rows.Count > 0)
                {
                    string sNew = hrmlang.GetString("new");
                    string rwp = hrmlang.GetString("rwprogress");
                    string rwc = hrmlang.GetString("rwcompleted");
                    string shi = hrmlang.GetString("slforinterview");
                    string ivw = hrmlang.GetString("ivsheduled");
                    string sel = hrmlang.GetString("selected");
                    string jnd = hrmlang.GetString("joined");
                    for (int i = 0; i < dTABLE.Rows.Count; i++)
                    {
                        switch ("" + dTABLE.Rows[i]["ApplicationStatus"])
                        {
                            case "NEW":
                                dTABLE.Rows[i]["ApplicationStatus"] = sNew;
                                break;
                            case "RWP":
                                dTABLE.Rows[i]["ApplicationStatus"] = rwp;
                                break;
                            case "RWC":
                                dTABLE.Rows[i]["ApplicationStatus"] = rwc;
                                break;
                            case "SHI":
                                dTABLE.Rows[i]["ApplicationStatus"] = shi;
                                break;
                            case "IVW":
                                dTABLE.Rows[i]["ApplicationStatus"] = ivw;
                                break;
                            case "SEL":
                                dTABLE.Rows[i]["ApplicationStatus"] = sel;
                                break;
                            case "JND":
                                dTABLE.Rows[i]["ApplicationStatus"] = jnd;
                                break;
                        }
                    }
                }

                if (dTABLE.Rows.Count > 0)
                    rptDoc.SetDataSource(dTABLE);
                else
                    bLoaded = false;
                break;
            case "EMPBRPT":
                EmployeeBAL objBALE = new EmployeeBAL();
                EmployeeBOL objBOLE = new EmployeeBOL();

                if (Util.ToInt(sp1) > 0)
                    objBOLE.BranchID = int.Parse(sp1);

                if (Util.ToInt(sp2) > 0)
                    objBOLE.DeptId = int.Parse(sp2);

                if (Util.ToInt(sp3) > 0)
                    objBOLE.EmpStatus = sp3.ToString();

                if (Util.ToInt(sp4) > 0)
                    objBOLE.RoleID = int.Parse(sp4);

                if (Util.ToInt(sp5) > 0)
                    objBOLE.DesgnID = int.Parse(sp5);

                objBOLE.FirstName = sp6.Trim();
                objBOLE.MiddleName = sp7.Trim();
                objBOLE.LastName = sp8.Trim();
                objBOLE.EmpCode = sp9.Trim();

                DataTable dtE = objBALE.employeeBenefit(objBOLE);
                dtE = SetStaticData(dtE);
                if (dtE.Rows.Count > 0)
                    rptDoc.SetDataSource(dtE);
                else
                    bLoaded = false;
                break;
            case "WORKSCHE":
                WorkPlanBOL objBOLW = new WorkPlanBOL();

                objBOLW.EmployeeID = Util.ToInt(sp1);
                objBOLW.WPMonth = Util.ToInt(sp2);
                objBOLW.WPYear = Util.ToInt(sp3);
                objBOLW.BranchID = Util.ToInt(sp4);
                DataTable dTableW = new WorkPlanBAL().SelectWorkSchedule(objBOLW);


                //return dtPlan;

                dTableW = SetStaticData(dTableW);

                if (dTableW.Rows.Count > 0)
                    rptDoc.SetDataSource(dTableW);


                else
                    bLoaded = false;
                break;
            case "WORKShft":
                WorkPlanBOL objBOLsh = new WorkPlanBOL();

                objBOLsh.EmployeeID = Util.ToInt(sp1);
                objBOLsh.WPMonth = Util.ToInt(sp2);
                objBOLsh.WPYear = Util.ToInt(sp3);
                objBOLsh.BranchID = Util.ToInt(sp4);
                objBOLsh.WSID = Util.ToInt(sp5);
                DataTable dTableWsh = new WorkPlanBAL().SelectWorkSchedule(objBOLsh);


                //return dtPlan;

                dTableW = SetStaticData(dTableWsh);

                if (dTableW.Rows.Count > 0)
                    rptDoc.SetDataSource(dTableW);


                else
                    bLoaded = false;
                break;

            case "LVBAL":
                EmployeeBAL objBALL = new EmployeeBAL();
                EmployeeBOL objBOLL = new EmployeeBOL();
                if (Util.ToInt(sp1) > 0)
                    objBOLL.BranchID = int.Parse(sp1);
                if (Util.ToInt(sp2) > 0)
                    objBOLL.DeptId = int.Parse(sp2);

                objBOLL.FirstName = sp3.Trim();
                objBOLL.MiddleName = sp4.Trim();
                objBOLL.LastName = sp5.Trim();
                objBOLL.EmpCode = sp6.Trim();
                objBOLL.Year = Util.ToInt(sp7);
                objBOLL.month = Util.ToInt(sp8);
                DataTable dTableL = objBALL.SearchRpt(objBOLL);

                dTableL = SetStaticData(dTableL);

                if (dTableL.Rows.Count > 0)
                    rptDoc.SetDataSource(dTableL);
                else
                    bLoaded = false;

                break;


            case "IRISRPT":
                IrisDataBAL objbal = new IrisDataBAL();
                IrisDataBOL objbol = new IrisDataBOL();
                objbol.BranchId = 0;
                objbol.FromDate = "";
                objbol.todate = "";
                objbol.empname = "";
                objbol.EmpCode = "";
                if (Util.ToInt(sp1) > 0)
                    objbol.BranchId = int.Parse(sp1);
                if (sp2 != "")
                    objbol.FromDate = sp2;
                if (sp3 != "")
                    objbol.todate = sp3;
                if (sp4 != "")
                    objbol.empname = sp4;
                if (sp5 != "")
                    objbol.EmpCode = sp5;

                DataTable dTableI = objbal.SelectIrisDetails(objbol);
                dTableI = SetStaticData(dTableI);

                if (dTableI.Rows.Count > 0)
                    rptDoc.SetDataSource(dTableI);
                else
                    bLoaded = false;
                break;

            case "LateTS": // TIMESHEET
                TimeSheetBAL objLatets = new TimeSheetBAL();

                int branchid = 0;
                int empid = 0;
                int year = 0;
                int month = 0;
                if (Util.ToInt(sp1) > 0)
                    branchid = int.Parse(sp1);
                if (Util.ToInt(sp2) > 0)
                    empid = int.Parse(sp2);
                if (Util.ToInt(sp3) > 0)
                    year = int.Parse(sp3);
                if (Util.ToInt(sp4) > 0)
                    month = int.Parse(sp4);
                DataTable dtlatesh = objLatets.laterpt(branchid, empid, year, month);
                dtlatesh = SetStaticData(dtlatesh);

                if (dtlatesh.Rows.Count > 0)
                    rptDoc.SetDataSource(dtlatesh);
                else
                    bLoaded = false;
                break;
            



            case "Resg": // Resignation rpt
                ResignationBAL objresgBAL = new ResignationBAL();
                ResignationBOL objresgBOL=new ResignationBOL();
                if (Util.ToInt(sp1) > 0)
                    objresgBOL.EmployeeID = int.Parse(sp1);
                    objresgBOL.Approved = ""+(sp2);
                    DataTable DTRESG = objresgBAL.SelectAPDresignation(objresgBOL);
                    DTRESG = SetStaticData(DTRESG);
                    if (DTRESG.Rows.Count > 0)
                    rptDoc.SetDataSource(DTRESG);
                else
                    bLoaded = false;
                break;
        }

        return bLoaded;
    }

    private DataTable SetStaticData(DataTable dTable)
    {
        if (dTable.Columns.IndexOf("WebStyle") == -1)
            dTable.Columns.Add("WebStyle");
        if (dTable.Columns.IndexOf("CompanyName") == -1)
            dTable.Columns.Add("CompanyName");
        if (dTable.Columns.IndexOf("CompanyAddress") == -1)
            dTable.Columns.Add("CompanyAddress");


        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();

        if (objBOL != null)
        {
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                dTable.Rows[i]["WebStyle"] = "" + Session["STYLESHEET"];
                dTable.Rows[i]["CompanyName"] = objBOL.CompanyName;
                dTable.Rows[i]["CompanyAddress"] = objBOL.Address + ", " + objBOL.City + ", " + objBOL.State + " ZIP : " + objBOL.ZipCode;
            }
        }

        return dTable;
    }
}