using System;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class DBBackUpDAL
    {


        public string DBBACKUP(string sPath)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DBPATH", sPath);
                objDA.sqlCmdText = "hrm_DB_Backup";
                objDA.sqlParam = objParam;
                return "" + objDA.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
