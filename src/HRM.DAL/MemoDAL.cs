using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class MemoDAL
    {
        public int Save(MemoBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@MemoID", objEmp.MemoID));
                objParam.Add(new SqlParameter("@MemoFrom", objEmp.MemoFrom));
                objParam.Add(new SqlParameter("@MemoTo", objEmp.MemoTo));
                objParam.Add(new SqlParameter("@Subject", objEmp.Subject));
                objParam.Add(new SqlParameter("@Description", objEmp.Description));
                objParam.Add(new SqlParameter("@Status", objEmp.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objEmp.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Memo_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangeStatus(int nMemoID, string status, int modifiedby)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            try
            {
                objParam[0] = new SqlParameter("@MemoID", nMemoID);
                objParam[1] = new SqlParameter("@ModifiedBy", modifiedby);
                objParam[2] = new SqlParameter("@Status", status);
                objDA.sqlCmdText = "hrm_Employee_Memo_Approval";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int nMemoID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@MemoID", nMemoID);
                objDA.sqlCmdText = "hrm_Employee_Memo_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(MemoBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.MemoID > 0)
                    objParam.Add(new SqlParameter("@MemoID", objBOL.MemoID));
                if (objBOL.MemoFrom > 0)
                    objParam.Add(new SqlParameter("@MemoFrom", objBOL.MemoFrom));
                if ("" + objBOL.Subject != "")
                    objParam.Add(new SqlParameter("@Subject", objBOL.Subject));
                if ("" + objBOL.Description != "")
                    objParam.Add(new SqlParameter("@Description", objBOL.Description));
                if ("" + objBOL.Status != "")
                    objParam.Add(new SqlParameter("@Status", objBOL.Status));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Memo_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectEmployeeByMemoID(int MemoID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (MemoID > 0)
                    objParam.Add(new SqlParameter("@MemoID", MemoID));               

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Memo_SELECTEMPLOYEESBY_MEMOID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
