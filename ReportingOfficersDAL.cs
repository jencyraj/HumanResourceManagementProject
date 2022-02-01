using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class ReportingOfficersDAL
    {
        public int Save(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@SuperiorID", objEmp.SuperiorID);
                objParam[++i] = new SqlParameter("@ImmediateSuperior", objEmp.ImmediateSuperior);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Employees_Superiors_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@SuperiorID", objEmp.SuperiorID);
                objDA.sqlCmdText = "hrm_Employees_Superiors_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectSuperiors(int nEmployeeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@EmployeeID", nEmployeeID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Employees_Superiors_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectSubordinates(int nSuperiorID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@SuperiorID", nSuperiorID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Employees_Superiors_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}
