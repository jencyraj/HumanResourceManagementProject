using System;
using System.Data;
using System.Data.SqlClient;


namespace HRM.DAL
{
    public class MonthsDAL
    {
        public DataTable Select(string sLang)
        {
            DataAccess objDA = new DataAccess();
            if ("" + sLang != "")
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@langculture", sLang);
                objDA.sqlParam = objParam;
            }
            objDA.sqlCmdText = "HRM_Months_SELECT";

            return objDA.ExecuteDataSet().Tables[0];
        }
    }
}
