using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AppraisalPeriodDAL
    {
        public int Save(AppraisalPeriodBOL objAppPeriod)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@AppPeriodID", objAppPeriod.AppPeriodID));
                objParam.Add(new SqlParameter("@DepartmentID", objAppPeriod.Department.DeptID));
                objParam.Add(new SqlParameter("@SubDepartmentID", objAppPeriod.SubDepartment.DeptID));
                objParam.Add(new SqlParameter("@DesignationID", objAppPeriod.Designation.DesignationID));
                objParam.Add(new SqlParameter("@Description", objAppPeriod.Description));
                objParam.Add(new SqlParameter("@StartDate", objAppPeriod.StartDate));
                objParam.Add(new SqlParameter("@EndDate", objAppPeriod.EndDate));
                objParam.Add(new SqlParameter("@CreatedBy", objAppPeriod.CreatedBy));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@PeriodStatus", objAppPeriod.PeriodStatus));

                objDA.sqlCmdText = "hrm_AppraisalPeriod_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveBranch(int AppPeriodID ,string sBranch,string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@AppPeriodID", AppPeriodID));
                objParam.Add(new SqlParameter("@Branch", sBranch));
                objParam.Add(new SqlParameter("@CreatedBy", CreatedBy));

                objDA.sqlCmdText = "hrm_AppraisalPeriod_Branch_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEmployees(int AppPeriodID, string sEmployee, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@AppPeriodID", AppPeriodID));
                objParam.Add(new SqlParameter("@Employees", sEmployee));
                objParam.Add(new SqlParameter("@CreatedBy", CreatedBy));

                objDA.sqlCmdText = "hrm_AppraisalPeriod_Employees_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nAppPeriodID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@AppPeriodID", nAppPeriodID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_AppraisalPeriod_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectAll(int nAppPeriodID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nAppPeriodID > 0)
                {
                    objParam[0] = new SqlParameter("@AppPeriodID", nAppPeriodID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_AppraisalPeriod_SELECT";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetAppraisalPeriods(int EmpID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@EmployeeID", EmpID);
                objDA.sqlCmdText = "hrm_AppraisalPeriod_Select_Fill";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
