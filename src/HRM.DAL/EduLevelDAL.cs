using System;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class EduLevelDAL
    {

        public int Save(string ID, string EduLevelName, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EduLevelID", ID);
                objParam[++i] = new SqlParameter("@EduLevelName", EduLevelName);
                objParam[++i] = new SqlParameter("@Status", 'Y');
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_EduLevel_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(string ID, string EduLevelName,string sSortOrder, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EduLevelID", ID);
                objParam[++i] = new SqlParameter("@EduLevelName", EduLevelName);
                objParam[++i] = new SqlParameter("@SortOrder", sSortOrder);
                objParam[++i] = new SqlParameter("@Status", 'Y');
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_EduLevel_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(string ID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EduLevelID", ID);
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_EduLevel_Delete";
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
                objDA.sqlCmdText = "hrm_EduLevel_Select";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
