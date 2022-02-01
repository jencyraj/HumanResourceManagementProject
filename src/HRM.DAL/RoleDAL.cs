using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class RoleDAL
    {
        public int Save(RoleBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@RoleID", objOrg.RoleID);
                objParam[++i] = new SqlParameter("@RoleName", objOrg.RoleName);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "HRM_USER_ROLE_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nRoleID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@RoleID", nRoleID);
                objDA.sqlCmdText = "HRM_USER_ROLE_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int nRoleID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nRoleID > 0)
                {
                    objParam[0] = new SqlParameter("@RoleID", nRoleID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "HRM_USER_ROLE_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
