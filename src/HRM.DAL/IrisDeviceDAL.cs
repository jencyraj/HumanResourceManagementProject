using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class IrisDeviceDAL
    {
        public int Save(int IrisID,string IPAddress, string SecurityId, string UserName, string Password, string Status, int BranchId,string DoorName,string masterdevice)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@IrisID", IrisID));
                objParam.Add(new SqlParameter("@IPAddress", IPAddress));
                objParam.Add(new SqlParameter("@SecurityId", SecurityId));
                objParam.Add(new SqlParameter("@UserName", UserName));
                objParam.Add(new SqlParameter("@Password", Password));
                objParam.Add(new SqlParameter("@Status", Status));
                objParam.Add(new SqlParameter("@BranchId", BranchId));
                objParam.Add(new SqlParameter("@DoorName", DoorName));
                objParam.Add(new SqlParameter("@MasterDevice", masterdevice));
                objDA.sqlCmdText = "hrm_IrisData_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update()
        {
            DataAccess objDA = new DataAccess();
           // SqlParameter[] objParam = new SqlParameter[1];

            try
            {
               // objParam[0] = new SqlParameter("@IrisID", IrisID);

                objDA.sqlCmdText = "hrm_irisData_Update";
               // objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int IrisID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@IrisID", IrisID);
                objParam[1] = new SqlParameter("@Status", "N");
                objDA.sqlCmdText = "hrm_IrisData_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int BranchID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (BranchID > 0)
                {
                    objParam[0] = new SqlParameter("@BranchID", BranchID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_IrisData_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
