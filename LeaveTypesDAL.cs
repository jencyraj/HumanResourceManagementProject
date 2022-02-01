using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class LeaveTypesDAL
    {
        public int Save(LeaveTypesBOL objLType)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LeaveTypeID", objLType.LeaveTypeID);
                objParam[++i] = new SqlParameter("@LeaveName", objLType.LeaveName);
                objParam[++i] = new SqlParameter("@ShortName", objLType.ShortName);
                objParam[++i] = new SqlParameter("@LeaveType", objLType.LeaveType);
                objParam[++i] = new SqlParameter("@LeaveDays", objLType.LeaveDays);
                objParam[++i] = new SqlParameter("@CarryOver", objLType.CarryOver);
                objParam[++i] = new SqlParameter("@Deduction", objLType.Deduction);
                objParam[++i] = new SqlParameter("@Status", objLType.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objLType.CreatedBy);

                objDA.sqlCmdText = "hrm_LeaveTypes_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nLeaveTypeID, string sCreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@LeaveTypeID", nLeaveTypeID);
                objParam[1] = new SqlParameter("@CreatedBy", sCreatedBy);
                objDA.sqlCmdText = "hrm_LeaveTypes_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll()
        {
            DataAccess objDA = new DataAccess();

            try
            {

                objDA.sqlCmdText = "hrm_LeaveTypes_Select";
                return objDA.ExecuteDataSet().Tables[0];              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveTypesBOL SelectByID(int nLeaveTypeID)
        {
            DataAccess objDA = new DataAccess();
            LeaveTypesBOL objLType = null;

            try
            {

                objDA.sqlCmdText = "hrm_LeaveTypes_Select";

                if (nLeaveTypeID > 0)
                {
                    SqlParameter[] objParam = new SqlParameter[1];
                    objParam[0] = new SqlParameter("@LeaveTypeID", nLeaveTypeID);
                    objDA.sqlParam = objParam;
                }

                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objLType = new LeaveTypesBOL();
                    objLType.LeaveTypeID = Util.ToInt("" + dReader["LeaveTypeID"]);
                    objLType.LeaveName = "" + dReader["LeaveName"];
                    objLType.ShortName = "" + dReader["ShortName"];
                    objLType.LeaveType = Util.ToInt("" + dReader["LeaveType"]);
                    objLType.LeaveDays = Util.ToInt("" + dReader["LeaveDays"]);
                    objLType.CarryOver = "" + dReader["CarryOver"];
                    objLType.Status = "" + dReader["Status"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objLType;
        }
    }
}
