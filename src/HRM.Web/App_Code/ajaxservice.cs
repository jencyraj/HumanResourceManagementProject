using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Data;

using HRM.BAL;
using HRM.BOL;

/// <summary>
/// Summary description for ajaxservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ajaxservice : System.Web.Services.WebService
{

    public ajaxservice()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession=true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetEmployees(string prefix)
    {
        List<string> Employees = new List<string>();
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
        objBOL.BranchID = Util.ToInt(Session["BRANCHID"]);
        DataTable dtEmp = objBAL.Search(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }


//*  new content*-//


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetSuperiors(string prefix,string id)
    {

        List<string> Employees = new List<string>();
        ReportingOfficersBAL objBAL = new ReportingOfficersBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
        DataTable dtEmp = objBAL.SelectSuperiors(int.Parse(id)); 
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetEmployeesByDesignation(string prefix, string DesignationId)
   {
        List<string> Employees = new List<string>();
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
     //   objBOL.BranchID =37;
      objBOL.BranchID = Util.ToInt(Session["BRANCHID"]);
        objBOL.DesgnID = Util.ToInt(DesignationId);
        DataTable dtEmp = objBAL.Search(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetDesignations(string prefix)
    {
        List<string> Designations = new List<string>();
        OrgDesignationBAL objBAL = new OrgDesignationBAL();
        OrgDesignationBOL objBOL = new OrgDesignationBOL();
        DataTable dtEmp = objBAL.SearchByName(prefix);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                Designations.Add(String.Format("{0}-{1}", dRow["Designation"], dRow["DesignationID"]));
            }
        }
        return Designations.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetEmployeesByBranchID(string prefix, string BranchID)
    {
        List<string> Employees = new List<string>();
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
        objBOL.BranchID = Util.ToInt(BranchID);
        DataTable dtEmp = objBAL.Search(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetJuniorEmployees(string prefix)
    {
        List<string> Employees = new List<string>();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.EmployeeID = Util.ToInt(Session["EMPID"]);
        objBOL.FirstName = prefix.Trim();
        DataTable dtEmp = new EmployeeBAL().GetJuniorEmployees(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = "" + dRow["Employee"];
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"].ToString()));
            }
        }
        return Employees.ToArray();
    }

    [WebMethod(EnableSession=true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetEmployeesbyRoleID(string prefix,string RoleID)
    {
        List<string> Employees = new List<string>();
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
        objBOL.RoleID = Util.ToInt(RoleID);
        DataTable dtEmp = objBAL.Search(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] Employeesearch(string prefix, string DesignationId, string BranchID, string DeptID)
    {
        List<string> Employees = new List<string>();
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.FirstName = prefix.Trim();
        objBOL.DesgnID = Util.ToInt(DesignationId);
        objBOL.BranchID = Util.ToInt(BranchID);
        objBOL.DeptId = Util.ToInt(DeptID);
        DataTable dtEmp = objBAL.Search(objBOL);
        if (dtEmp.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                string sName = dRow["FirstName"] + " " + dRow["MiddleName"] + " " + dRow["LastName"];
                sName = sName.Trim().Replace("  ", " ");
                Employees.Add(String.Format("{0}-{1}", sName, dRow["EmployeeID"]));
            }
        }
        return Employees.ToArray();
    }
    public int GetBranchID()
    {
        int BranchID = 0;
         BranchID = Util.ToInt(Session["BRANCHID"]);
        return BranchID;
    }
}
