using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using HRM.BOL;

namespace HRM.DAL
{
    public class PermissionDAL
    {
        public int Save(PermissionBOL objPerm)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                if (objPerm.ModuleID <= 0) return 0;
                objParam[++i] = new SqlParameter("@RoleID", objPerm.RoleID);
                objParam[++i] = new SqlParameter("@ModuleID", objPerm.ModuleID);
                if (objPerm.EmpID > 0)
                    objParam[++i] = new SqlParameter("@EmployeeID", objPerm.EmpID);
                objParam[++i] = new SqlParameter("@AllowInsert", objPerm.AllowInsert);
                objParam[++i] = new SqlParameter("@AllowUpdate", objPerm.AllowUpdate);
                objParam[++i] = new SqlParameter("@AllowDelete", objPerm.AllowDelete);
                objParam[++i] = new SqlParameter("@AllowView", objPerm.AllowView);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "HRM_PERMISSIONS_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectAll(int nRoleID, int EmployeeID, string Langculturename)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@RoleID", nRoleID));
                if (EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));
                objParam.Add(new SqlParameter("@Langculturename", Langculturename));
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "HRM_MODULES_SELECT";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetPermissions(int nRoleID, int EmployeeID, string Langculturename)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@RoleID", nRoleID));
                if (EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));
                objParam.Add(new SqlParameter("@Langculturename", Langculturename));
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Module_Permissions";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
