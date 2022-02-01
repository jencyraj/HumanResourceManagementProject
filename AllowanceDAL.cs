using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AllowanceDAL
    {
        public int Save(AllowanceBOL objAlw)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@AlwID", objAlw.AlwID);
                objParam[++i] = new SqlParameter("@AlwCode", objAlw.AlwCode);
                objParam[++i] = new SqlParameter("@AllowanceName", objAlw.AllowanceName);
                objParam[++i] = new SqlParameter("@AlwAmount", objAlw.AlwAmount);
                objParam[++i] = new SqlParameter("@AlwType", objAlw.AlwType);
                objParam[++i] = new SqlParameter("@CreatedBy", objAlw.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@Taxable", objAlw.Taxable);
                objDA.sqlCmdText = "hrm_Allowances_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nAlwID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@AlwID", nAlwID);
                objDA.sqlCmdText = "hrm_Allowances_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int nAlwID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nAlwID > 0)
                {
                    objParam[0] = new SqlParameter("@AlwID", nAlwID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_Allowances_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
