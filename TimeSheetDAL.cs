using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HRM.DAL
{
    public class TimeSheetDAL
    {
        public DataSet GetTimeSheet(int BranchID, int EmployeeID, int nYear, int nMonth)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objDA.sqlCmdText = "usp_Timesheet";

                if (BranchID > 0)
                    objParam.Add(new SqlParameter("@BranchID", BranchID));

                if (EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));

                if (nYear > 0)
                    objParam.Add(new SqlParameter("@Year", nYear));

                if (nMonth > 0)
                    objParam.Add(new SqlParameter("@Month", nMonth));

                objDA.sqlParam = objParam.ToArray();

                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable laterpt(int BranchID, int EmployeeID, int nYear, int nMonth)
        {

            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objDA.sqlCmdText = "hrm_latetiming_select";

                if (BranchID > 0)
                    objParam.Add(new SqlParameter("@BranchID", BranchID));

                if (EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));

                if (nYear > 0)
                    objParam.Add(new SqlParameter("@Year", nYear));

                if (nMonth > 0)
                    objParam.Add(new SqlParameter("@Month", nMonth));

                objDA.sqlParam = objParam.ToArray();

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
