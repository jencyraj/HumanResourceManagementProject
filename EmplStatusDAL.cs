using System;
using System.Data;
using System.Data.SqlClient;


namespace HRM.DAL
{
    public class EmplStatusDAL
    {

        public int Save(string ID, string Description, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmplStatusID", ID);
                objParam[++i] = new SqlParameter("@Description", Description);
                objParam[++i] = new SqlParameter("@Status", 'Y');
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_EmplStatus_Insert_Update";
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
                objParam[++i] = new SqlParameter("@EmplStatusID", ID);
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_EmplStatus_Delete";
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
                objDA.sqlCmdText = "hrm_EmplStatus_Select";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
