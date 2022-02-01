using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class UserDAL
    {
        public int Save(UserBOL objUser)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@UID", objUser.UID));
                objParam.Add(new SqlParameter("@RoleID", objUser.RoleID));
                objParam.Add(new SqlParameter("@UserID", objUser.UserID));
                objParam.Add(new SqlParameter("@password", objUser.Password));
                objParam.Add(new SqlParameter("@BiometricID", objUser.BiometricID));
                objParam.Add(new SqlParameter("@IRISID", objUser.IRISID));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objDA.sqlCmdText = "HRM_USER_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int UID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@UID", UID);
                objDA.sqlCmdText = "HRM_USER_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(string UserID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (UserID != "")
                {
                    objParam[0] = new SqlParameter("@UserID", UserID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "HRM_USER_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string SignIn(string sUserID, string sPassword)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            objParam[0] = new SqlParameter("@USERID", sUserID);
            objParam[1] = new SqlParameter("@PASSWD", sPassword);

            objDA.sqlParam = objParam;
            objDA.sqlCmdText = "HRM_USERS_SIGNIN";
            return "" + objDA.ExecuteScalar();
        }

        public string SignOut(string sUserID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@USERID", sUserID);

            objDA.sqlParam = objParam;
            objDA.sqlCmdText = "HRM_USERS_SIGNOUT";
            return "" + objDA.ExecuteScalar();
        }


        public DataTable LoginHistory(string sUserID, bool LastRecordOnly, int sBranchID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            if (LastRecordOnly)
                objParam.Add(new SqlParameter("@LastRecordOnly", 1));

            if ("" + sUserID != "")
                objParam.Add(new SqlParameter("@userid", sUserID));

            if (sBranchID > 0)
                objParam.Add(new SqlParameter("@BranchID", sBranchID));

            objDA.sqlParam = objParam.ToArray();
            objDA.sqlCmdText = "hrm_LoginHistory_Select";
            return objDA.ExecuteDataSet().Tables[0];
        }
    }
}
