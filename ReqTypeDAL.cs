using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HRM.BOL;
namespace HRM.DAL
{
   public class ReqTypeDAL
    {
        public int Save(ReqTypeBOL objOrg)
        {

            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ReqID", objOrg.ReqID);
                objParam[++i] = new SqlParameter("@ReqType", objOrg.ReqType);
                objParam[++i] = new SqlParameter("@ReqDesc", objOrg.ReqDesc);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_RequestType_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int ReqID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@ReqID", ReqID);
                objDA.sqlCmdText = "[hrm_ReqType_DELETE]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int ReqID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                //objParam[0] = new SqlParameter("@ReqID", ReqID);
                objDA.sqlCmdText = "[hrm_ReqType_Select]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
