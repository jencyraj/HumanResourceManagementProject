using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace HRM.DAL
{
    public class CountryDAL
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
            objDA.sqlCmdText = "HRM_COUNTRY_SELECT";

            return objDA.ExecuteDataSet().Tables[0];
        }

        public int UPDATE(string Code, string CNAME, string LangCultureName)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@CountryCode", Code.Trim()));
            objParam.Add(new SqlParameter("@CountryName", CNAME.Trim()));
            objParam.Add(new SqlParameter("@LangCultureName", LangCultureName.Trim()));

            objDA.sqlCmdText = "hrm_Countrylist_iNSERT_Update";

            objDA.sqlParam = objParam.ToArray();
            return objDA.ExecuteNonQuery();

        }
    }
}
