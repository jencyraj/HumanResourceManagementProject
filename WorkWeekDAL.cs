using System;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class WorkWeekDAL
    {
        public int Save(string[] sDays, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[13];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@BranchID", sDays[0]);
                objParam[++i] = new SqlParameter("@DeptID", sDays[1]);
                objParam[++i] = new SqlParameter("@DesgnID", sDays[2]);
                objParam[++i] = new SqlParameter("@EmpID", sDays[3]);
                objParam[++i] = new SqlParameter("@Sunday", sDays[4]);
                objParam[++i] = new SqlParameter("@Monday", sDays[5]);
                objParam[++i] = new SqlParameter("@Tuesday", sDays[6]);
                objParam[++i] = new SqlParameter("@Wednesday", sDays[7]);
                objParam[++i] = new SqlParameter("@Thursday", sDays[8]);
                objParam[++i] = new SqlParameter("@Friday", sDays[9]);
                objParam[++i] = new SqlParameter("@Saturday", sDays[10]);
                objParam[++i] = new SqlParameter("@ModifiedBy", CreatedBy);
                objParam[++i] = new SqlParameter("@WID", sDays[11]);
                objDA.sqlCmdText = "hrm_WorkWeek_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Workweek_select()
        {
            DataAccess objDA = new DataAccess();

            try
            {

                objDA.sqlCmdText = "[hrm_WorkWeek_Display]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Workweek_edit(int Eid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@WID", Eid);
                objDA.sqlCmdText = "[hrm_WorkWeek_Select]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int Eid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@WID", Eid);
                objDA.sqlCmdText = "[hrm_WorkWeek_Delete]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Select()
        {
            DataAccess objDA = new DataAccess();

            try
            {
                objDA.sqlCmdText = "hrm_WorkWeek_Select";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
