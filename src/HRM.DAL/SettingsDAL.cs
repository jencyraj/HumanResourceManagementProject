using System;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class SettingsDAL
    {
        public int Save(string ConfigCode, string ConfigValue)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            
            try
            {
                objParam[0] = new SqlParameter("@ConfigCode", ConfigCode);
                objParam[1] = new SqlParameter("@ConfigValue", ConfigValue);
                objDA.sqlCmdText = "hrm_Config_Settings_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll()
        {
            DataAccess objDA = new DataAccess();

            try
            {
                objDA.sqlCmdText = "hrm_Config_Settings_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
