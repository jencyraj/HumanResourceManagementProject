using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class OrgBranchesDAL
    {
        public int Save(OrgBranchesBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@BranchID", objOrg.BranchID);
                objParam[++i] = new SqlParameter("@CompanyID", objOrg.CompanyID);
                objParam[++i] = new SqlParameter("@Branch", objOrg.Branch);
                objParam[++i] = new SqlParameter("@Address", objOrg.Address);
                objParam[++i] = new SqlParameter("@City", objOrg.City);
                objParam[++i] = new SqlParameter("@State", objOrg.State);
                objParam[++i] = new SqlParameter("@CountryID", objOrg.CountryID);
                objParam[++i] = new SqlParameter("@Phone1", objOrg.Phone1);
                objParam[++i] = new SqlParameter("@Phone2", objOrg.Phone2);
                objParam[++i] = new SqlParameter("@Email", objOrg.Email);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "HRM_COM_LOCATIONS_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nBranchID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@BranchID", nBranchID);
                objDA.sqlCmdText = "HRM_COM_LOCATIONS_DELETE";
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
                objDA.sqlCmdText = "HRM_COM_LOCATIONS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
