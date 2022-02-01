using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class WorkShiftsDAL
    {
        public int Save(WorkShiftsBOL objWS)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@WSID", objWS.WSID));
                objParam.Add(new SqlParameter("@WorkShiftName", objWS.WorkShiftName));
                objParam.Add(new SqlParameter("@StartTime", objWS.StartTime));
                objParam.Add(new SqlParameter("@EndTime", objWS.EndTime));
                objParam.Add(new SqlParameter("@BreakHour1Start", objWS.BreakHour1Start));
                objParam.Add(new SqlParameter("@BreakHour1End", objWS.BreakHour1End));
                objParam.Add(new SqlParameter("@BreakHour2Start", objWS.BreakHour2Start));
                objParam.Add(new SqlParameter("@BreakHour2End", objWS.BreakHour2End));
                objParam.Add(new SqlParameter("@BreakHour3Start", objWS.BreakHour3Start));
                objParam.Add(new SqlParameter("@BreakHour3End", objWS.BreakHour3End));
                objParam.Add(new SqlParameter("@WorkingHours", objWS.WorkingHours));
                objParam.Add(new SqlParameter("@CreatedBy", objWS.CreatedBy));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objDA.sqlCmdText = "hrm_WorkShifts_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nWSID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@WSID", nWSID);
                objDA.sqlCmdText = "hrm_WorkShifts_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Selectshift()
        {
            DataAccess objDA = new DataAccess();

            try
            {

                objDA.sqlCmdText = "hrm_WorkShifts_All";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectAll(int nWSID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nWSID > 0)
                {
                    objParam[0] = new SqlParameter("@WSID", nWSID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_WorkShifts_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
