using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using HRM.BOL;

namespace HRM.DAL
{
    public class WorkPlanDAL
    {
        public int SaveMaster(WorkPlanBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[9];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@WPMID", objWP.WPMID);
                objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                objParam[++i] = new SqlParameter("@WPYear", objWP.WPYear);
                objParam[++i] = new SqlParameter("@WPMonth", objWP.WPMonth);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", objWP.Status);
                objParam[++i] = new SqlParameter("@BranchID", objWP.BranchID);
                objDA.sqlCmdText = "hrm_Employee_WorkPlanMaster_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DeleteMaster(int nWPMID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@WPMID", nWPMID);
                objDA.sqlCmdText = "hrm_Employee_WorkPlanMaster_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectWorkPlanMaster(WorkPlanBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                if(objWP.WPMID>0)
                objParam[++i] = new SqlParameter("@WPMID", objWP.WPMID);
                if (objWP.EmployeeID > 0)
                objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                if (objWP.WPYear > 0)
                    objParam[++i] = new SqlParameter("@WPYear", objWP.WPYear);
                if (objWP.WPMonth > 0)
                    objParam[++i] = new SqlParameter("@WPMonth", objWP.WPMonth);
                if (objWP.WPMonth > 0)
                    objParam[++i] = new SqlParameter("@BranchID", objWP.BranchID);
                objDA.sqlCmdText = "hrm_Employee_WorkPlanMaster_Select";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(WorkPlanBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@WPID", objWP.WPID);
                objParam[++i] = new SqlParameter("@WSID", objWP.WSID);
                objParam[++i] = new SqlParameter("@WPMID", objWP.WPMID);
                objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                objParam[++i] = new SqlParameter("@FromDate", objWP.FromDate);
                objParam[++i] = new SqlParameter("@ToDate", objWP.ToDate);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", objWP.Status);
                objDA.sqlCmdText = "hrm_Employee_WorkPlan_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nWPID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@WPID", nWPID);
                objDA.sqlCmdText = "hrm_Employee_WorkPlan_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(WorkPlanBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objWP.WPMID > 0)
                    objParam.Add(new SqlParameter("@WPMID", objWP.WPMID));

                if (objWP.WPID > 0)
                    objParam.Add( new SqlParameter("@WPID", objWP.WPID));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_WorkPlan_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable SelectWorkSchedule(WorkPlanBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[4];
            int i = -1;

            try
            {
                if (objWP.WPMID > 0)
                    objParam[++i] = new SqlParameter("@WPMID", objWP.WPMID);
                if (objWP.EmployeeID > 0)
                    objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                if (objWP.WPYear > 0)
                    objParam[++i] = new SqlParameter("@WPYear", objWP.WPYear);
                if (objWP.WPMonth > 0)
                    objParam[++i] = new SqlParameter("@WPMonth", objWP.WPMonth);
                if (objWP.BranchID > 0)
                    objParam[++i] = new SqlParameter("@BranchID", objWP.BranchID);

                objDA.sqlCmdText = "hrm_Employee_WorkPSchedule_report";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
    }
}
