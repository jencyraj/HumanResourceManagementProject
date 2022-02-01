using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
    public  class NationalityDAL
    {
        public DataTable Select(string sLang)
        {
            DataAccess objDA = new DataAccess();
            if ("" + sLang != "")
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@lang", sLang);
                objDA.sqlParam = objParam;
            }
            objDA.sqlCmdText = "hrm_Nationality_Select";

            return objDA.ExecuteDataSet().Tables[0];
        }
        public int UPDATE(string Code, string Nname, string LangCultureName)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@NationalityCode", Code.Trim()));
            objParam.Add(new SqlParameter("@Nationality", Nname.Trim()));
            objParam.Add(new SqlParameter("@LangCultureName", LangCultureName.Trim()));

            objDA.sqlCmdText = "hrm_Nationalitylist_iNSERT_Update";

            objDA.sqlParam = objParam.ToArray();
            return objDA.ExecuteNonQuery();

        }
    }
}
