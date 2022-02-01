using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
namespace HRM.DAL
{
  public  class LOPDeductionDAL
    {
        public DataTable SelectAll(LOPDeductionBOL objBOL)
        {
            DataTable dtded = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objParam.Add(new SqlParameter("@NMONTH", objBOL.Month));
                    objParam.Add(new SqlParameter("@NYEAR", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Salaryslip_Deduction";
                dtded = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtded = new DataTable();
            }
            return dtded;
        }

        public DataTable SelectLeaveType(LOPDeductionBOL objBOL)
        {
            DataTable dtded = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objParam.Add(new SqlParameter("@NMONTH", objBOL.Month));
                    objParam.Add(new SqlParameter("@NYEAR", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "[hrm_Salaryslip_LeavesTaken]";
                dtded = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtded = new DataTable();
            }
            return dtded;
        }
      
        public DataTable SelectMinLeav(LOPDeductionBOL objBOL)
        {
            DataTable dtded = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.EmployeeId > 0)
                        
                    objParam.Add(new SqlParameter("@NMONTH", objBOL.Month));
                    objParam.Add(new SqlParameter("@NYEAR", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "[hrm_Salaryslip_MinimumLeaves]";
                dtded = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtded = new DataTable();
            }
            return dtded;
        }
        public DataTable Get_LOP_WrkngHour(LOPDeductionBOL objBOL)
        {
            DataTable dtded = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {

                    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeId));
                      objParam.Add(new SqlParameter("@MONTH", objBOL.Month));
                    objParam.Add(new SqlParameter("@YEAR", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "[usp_Timesheet_LOP]";
                dtded = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtded = new DataTable();
            }
            return dtded;
        }
    }
}
