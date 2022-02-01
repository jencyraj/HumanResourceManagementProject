using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class RegnTypesDAL
    {
        public int Save(RegnTypesBOL objRegn)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@IdentifierID", objRegn.IdentifierID);
                objParam[++i] = new SqlParameter("@CompanyID", objRegn.CompanyID);
                objParam[++i] = new SqlParameter("@Description", objRegn.Description);
                objParam[++i] = new SqlParameter("@IdentifierValue", objRegn.IdentifierValue);
                objParam[++i] = new SqlParameter("@CreatedBy", objRegn.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Identifiers_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nIdentifierID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@IdentifierID", nIdentifierID);
                objDA.sqlCmdText = "hrm_Identifiers_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int nCompanyID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CompanyID", nCompanyID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Identifiers_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
