using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class WarningsDAL
    {
        public int Save(WarningsBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@WarningID", objEmp.WarningID));
                objParam.Add(new SqlParameter("@WarningFrom", objEmp.WarningFrom));
                objParam.Add(new SqlParameter("@WarningTo", objEmp.WarningTo));
                objParam.Add(new SqlParameter("@Subject", objEmp.Subject));
                objParam.Add(new SqlParameter("@Description", objEmp.Description));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@CreatedBy", objEmp.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Warnings_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nWarningID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@WarningID", nWarningID);
                objDA.sqlCmdText = "hrm_Employee_Warnings_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(WarningsBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.WarningID > 0)
                    objParam.Add(new SqlParameter("@WarningID", objBOL.WarningID));
                if (objBOL.WarningFrom > 0)
                    objParam[0] = new SqlParameter("@WarningFrom", objBOL.WarningFrom);
                if ("" + objBOL.Subject != "")
                    objParam[0] = new SqlParameter("@Subject", objBOL.Subject);
                if ("" + objBOL.Description != "")
                    objParam[0] = new SqlParameter("@Description", objBOL.Description);

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Warnings_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
