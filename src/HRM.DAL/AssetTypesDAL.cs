using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AssetTypesDAL
    {
        public int Save(AssetTypesBOL objAlw)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[4];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@AssetTypeID", objAlw.AssetTypeID);
                objParam[++i] = new SqlParameter("@AssetType", objAlw.AssetType);
                objParam[++i] = new SqlParameter("@CreatedBy", objAlw.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_AssetTypes_INSERT_UPDATE";
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
                objParam[0] = new SqlParameter("@AssetTypeID", nAlwID);
                objDA.sqlCmdText = "hrm_AssetTypes_DELETE";
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
                    objParam[0] = new SqlParameter("@AssetTypeID", nAlwID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_AssetTypes_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
