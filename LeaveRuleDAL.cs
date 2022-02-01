using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using HRM.BOL;
using System.Data;
namespace HRM.DAL
{
  public  class LeaveRuleDAL
    {
        public int Save(LeaveRuleBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LRID",objWP.LRID);
                objParam[++i] = new SqlParameter("@StartMonth", objWP.Month);
                objParam[++i] = new SqlParameter("@Minleav", objWP.minleav);
                objParam[++i] = new SqlParameter("@Description", objWP.Description);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Active", objWP.Active);
                objParam[++i] = new SqlParameter("@LRYear", objWP.Year);
                objParam[++i] = new SqlParameter("@LeaveRule", objWP.DTLeaveRule);
                objDA.sqlParam = objParam;
               objDA.sqlCmdText = "hrm_LeaveRule_INSERT";
               
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(LeaveRuleBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LRID", objWP.LRID);
                objParam[++i] = new SqlParameter("@StartMonth", objWP.Month);
                objParam[++i] = new SqlParameter("@Minleav", objWP.minleav);
                objParam[++i] = new SqlParameter("@Description", objWP.Description);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Active", objWP.Active);
                objParam[++i] = new SqlParameter("@LRYear", objWP.Year);
                objParam[++i] = new SqlParameter("@LeaveRule", objWP.DTLeaveRule);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_LeaveRule_UPDATE]";

                
               
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectDetail(int LRID)
        {
            DataAccess objDA = new DataAccess();
              SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@LRID", LRID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_LeaveRuleDetail_Select";
                
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            DataAccess objDA = new DataAccess();
       
            try
            {

                objDA.sqlCmdText = "hrm_LeaveRule_AllSelect";

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable rowselect(int LDID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@LDID", LDID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_LeaveRuleDetail_rowselect]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable CheckyearA()
        {
            DataAccess objDA = new DataAccess();
           
            try
            {

                objDA.sqlCmdText = "[hrm_CheckActiveYear]";

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable Checkyear_timesheet()
        {
            DataAccess objDA = new DataAccess();

            try
            {

                objDA.sqlCmdText = "[hrm_CheckActiveYear_Timesheet]";

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int Delete(int LRID, string sCreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@LRID", LRID);
                objParam[1] = new SqlParameter("@CreatedBy", sCreatedBy);
                objDA.sqlCmdText = "hrm_LeaveRule_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteRow(int LDID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@LDID", LDID);

                objDA.sqlCmdText = "[hrm_LeaveRuleRow_DELETE]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectAll(int LRID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@LRID", LRID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_LeaveRule_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable bindleavtype()
        {
            DataAccess objDA = new DataAccess();
            
            try
            {
                
               
                objDA.sqlCmdText = "[hrm_All_LeaveTypes]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
