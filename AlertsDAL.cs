using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class AlertsDAL
    {
        public int Save(int EmployeeID, string sMsg, string sType, string LangCulture)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeId", EmployeeID));
                if ("" + sMsg != "")
                    objParam.Add(new SqlParameter("@AlertDescription", sMsg));
                if ("" + LangCulture != "")
                   objParam.Add(new SqlParameter("@LangCulture", LangCulture));
                objDA.sqlCmdText = "hrm_Alerts_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (EmployeeID > 0)
                {
                    objParam[0] = new SqlParameter("@EmployeeID", EmployeeID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_Alerts_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Delete(int AlertID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (AlertID > 0)
                    objParam.Add(new SqlParameter("@AlertID", AlertID));
                
                objDA.sqlCmdText = "hrm_Alerts_Delete";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
