using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class OrgDepartmentsDAL
    {
        public int Save(OrgDepartmentBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@DeptID", objOrg.DeptID);
                objParam[++i] = new SqlParameter("@CompanyID", objOrg.CompanyID);
                objParam[++i] = new SqlParameter("@ParentDepartmentID", objOrg.ParentDeptID);
                objParam[++i] = new SqlParameter("@BranchID", objOrg.BranchID);
                objParam[++i] = new SqlParameter("@DeptCode", objOrg.DeptCode);
                objParam[++i] = new SqlParameter("@DepartmentName", objOrg.DepartmentName);
                objParam[++i] = new SqlParameter("@CreatedBy", objOrg.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@Branches", objOrg.Branches);

                objDA.sqlCmdText = "HRM_COM_DEPARTMENTS_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nDeptID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DeptID", nDeptID);
                objDA.sqlCmdText = "HRM_COM_DEPARTMENTS_DELETE";
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
                objDA.sqlCmdText = "HRM_COM_DEPARTMENTS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable SelectAll(OrgDepartmentBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            int i = -1;
            try
            {
                if (objBOL.CompanyID > 0)
                    objParam[++i] = new SqlParameter("@CompanyID", objBOL.CompanyID);

                if (objBOL.DeptID > 0)
                    objParam[++i] = new SqlParameter("@DepartmentID", objBOL.DeptID);

                if (objBOL.ParentDeptID > 0)
                    objParam[++i] = new SqlParameter("@ParentDepartmentID", objBOL.ParentDeptID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "HRM_COM_DEPARTMENTS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectParentDepartments(int nDepartmentID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DepartmentID", nDepartmentID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Com_Departments_SelectParent";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectDepartmentsByBranchID(int BranchID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            int i = -1;

            try
            {
                if (BranchID > 0)
                    objParam[++i] = new SqlParameter("@BranchID", BranchID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Com_Departments_SelectByBranchID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable SelectBranchesByDepartmentID(int DepartmentID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            int i = -1;

            try
            {
                if (DepartmentID > 0)
                    objParam[++i] = new SqlParameter("@DepartmentID", DepartmentID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Com_Departments_SelectByBranchID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
