using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class OrgDesignationDAL
    {
        public int Save(OrgDesignationBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@DesignationID", objOrg.DesignationID);
                objParam[++i] = new SqlParameter("@CompanyID", objOrg.CompanyID);
                objParam[++i] = new SqlParameter("@ParentID", objOrg.ParentID);
                objParam[++i] = new SqlParameter("@DesgnCode", objOrg.DesgnCode);
                objParam[++i] = new SqlParameter("@Designation", objOrg.Designation);
                objParam[++i] = new SqlParameter("@CreatedBy", objOrg.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "HRM_COM_DESIGNATIONS_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nDesignationID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DesignationID", nDesignationID);
                objDA.sqlCmdText = "HRM_COM_DESIGNATIONS_DELETE";
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
                objDA.sqlCmdText = "HRM_COM_DESIGNATIONS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
      
        public DataTable Selectdesgn(int nCompanyID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DesignationID", nCompanyID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "HRM_DESIGNATIONS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SearchByName(string Name)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@Name", Name);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Com_Designations_SearchByName";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
