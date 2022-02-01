using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;



namespace HRM.DAL
{
    public class LeaveDAL
    {
        public int Save(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@LeaveID", objLeave.LeaveID);
                objParam[++i] = new SqlParameter("@EmployeeID", objLeave.EmployeeID);
                objParam[++i] = new SqlParameter("@Reason", objLeave.Reason);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@CreatedBy", objLeave.CreatedBy);

                objDA.sqlCmdText = "hrm_Leave_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveWithLeaveRule(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@LeaveID", objLeave.LeaveID);
                objParam[++i] = new SqlParameter("@EmployeeID", objLeave.EmployeeID);
                objParam[++i] = new SqlParameter("@Reason", objLeave.Reason);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@CreatedBy", objLeave.CreatedBy);
                objParam[++i] = new SqlParameter("@leaverule", "Y");
                objParam[++i] = new SqlParameter("@Lyr", objLeave.LeaveYear);
                objParam[++i] = new SqlParameter("@LMon", objLeave.LeaveMonth);


                objDA.sqlCmdText = "hrm_Leave_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Getlatebyvalue(LeaveBOL objBOL)
        {

            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.EmployeeID > 0)
                        objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
                    objParam.Add(new SqlParameter("@NMONTH", objBOL.Month));
                    objParam.Add(new SqlParameter("@NYEAR", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();

                }
                objDA.sqlCmdText = "hrm_late_Byvalue";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable checklatebytime()
        {
            DataTable dtded = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objDA.sqlCmdText = "[hrm_CheckActiveYear]";
                dtded = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtded = new DataTable();
            }
            return dtded;
        }
        public DataTable SalaryslipGenerate(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;
            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objLeave.EmployeeID);
                objParam[++i] = new SqlParameter("@NMONTH", objLeave.Month);
                objParam[++i] = new SqlParameter("@NYEAR", objLeave.Year);
                objDA.sqlCmdText = "[hrm_Salaryslip_Excel]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable Getallowances(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;
            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objLeave.EmployeeID);
                objParam[++i] = new SqlParameter("@NMONTH", objLeave.Month);
                objParam[++i] = new SqlParameter("@NYEAR", objLeave.Year);
                objDA.sqlCmdText = "[hrm_ViewDetailSalaryByEmployeeID]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int SaveLeaveDates(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[6];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LeaveDetailsID", objLeave.LeaveDetailsID);
                objParam[++i] = new SqlParameter("@LeaveTypeID", objLeave.LeaveTypeID);
                objParam[++i] = new SqlParameter("@LeaveID", objLeave.LeaveID);
                if (objLeave.LeaveDate > DateTime.MinValue)
                    objParam[++i] = new SqlParameter("@LeaveDate", objLeave.LeaveDate);
                objParam[++i] = new SqlParameter("@LeaveSession", objLeave.LeaveSession);
                objParam[++i] = new SqlParameter("@LeaveDays", objLeave.LeaveDays);

                objDA.sqlCmdText = "hrm_LeaveDetails_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApproveLeave(LeaveBOL objLeave)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LeaveID", objLeave.LeaveID);
                objParam[++i] = new SqlParameter("@ApprovalStatus", objLeave.ApprovalStatus);
                objParam[++i] = new SqlParameter("@RejectReason", objLeave.RejectReason);
                objParam[++i] = new SqlParameter("@ApprovedBy", objLeave.ApprovedBy);

                objDA.sqlCmdText = "hrm_Leave_Approve";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nLeaveID, string sCreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@LeaveID", nLeaveID);
                objParam[1] = new SqlParameter("@CreatedBy", sCreatedBy);
                objDA.sqlCmdText = "hrm_Leave_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteLeaveDetails(int nLeaveDetailID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@LeaveDetailID", nLeaveDetailID);
                objDA.sqlCmdText = "hrm_LeaveDetail_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(LeaveBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            try
            {
                if (objBOL.LeaveID > 0)
                    objParam[0] = new SqlParameter("@LeaveID", objBOL.LeaveID);
                if (objBOL.EmployeeID > 0)
                    objParam[1] = new SqlParameter("@EmployeeID", objBOL.EmployeeID);
                if ("" + objBOL.ApprovalStatus != "")
                    objParam[2] = new SqlParameter("@APPROVALSTATUS", objBOL.ApprovalStatus);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Leave_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectByID(int nLeaveID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objParam[0] = new SqlParameter("@LeaveID", nLeaveID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Leave_Select";
                DataSet dSet = objDA.ExecuteDataSet();
                return dSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLeaveBalance(int EmpID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", EmpID);

                objDA.sqlCmdText = "hrm_LeaveBalance_Select";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal CheckAvailability(int EmpID, int LeaveTypeID, int LeaveYear)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", EmpID);
                objParam[++i] = new SqlParameter("@LeaveTypeID", LeaveTypeID);
                objParam[++i] = new SqlParameter("@LeaveYear", LeaveYear);

                objDA.sqlCmdText = "hrm_Leave_Availablility";
                objDA.sqlParam = objParam;
                return Util.ToDecimal(objDA.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateLeaveBalance(LeaveBOL objBOL, int Action)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[6];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objBOL.EmployeeID);
                objParam[++i] = new SqlParameter("@LeaveTypeID", objBOL.LeaveTypeID);
                objParam[++i] = new SqlParameter("@TotalLeaves", objBOL.TotalLeaves);
                objParam[++i] = new SqlParameter("@LeavesTaken", objBOL.LeavesTaken);
                objParam[++i] = new SqlParameter("@LeaveYear", objBOL.LeaveDate.Year);
                objParam[++i] = new SqlParameter("@Action", Action);

                objDA.sqlCmdText = "hrm_LeaveBalance_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int SetLeaveBalance_NewYear(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);

                objDA.sqlCmdText = "hrm_Set_LeaveBalance";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveLeaveBalance(LeaveBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objBOL.EmployeeID);
                objParam[++i] = new SqlParameter("@LeaveTypeID", objBOL.LeaveTypeID);
                objParam[++i] = new SqlParameter("@TotalLeaves", objBOL.TotalLeaves);
                objParam[++i] = new SqlParameter("@LeavesTaken", objBOL.LeavesTaken);
                objParam[++i] = new SqlParameter("@LeaveYear", objBOL.LeaveYear);
                objParam[++i] = new SqlParameter("@LeaveMonth", objBOL.LeaveMonth);
                objParam[++i] = new SqlParameter("@PrevYearBalance", objBOL.PrevYearBalance);
                objParam[++i] = new SqlParameter("@LeavesBalance", objBOL.LeavesBalance);

                objParam[2].SqlDbType = SqlDbType.Decimal;
                objParam[3].SqlDbType = SqlDbType.Decimal;
                objParam[6].SqlDbType = SqlDbType.Decimal;
                objParam[7].SqlDbType = SqlDbType.Decimal;

                objDA.sqlCmdText = "hrm_LeaveBalance_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable AppliedLeaveRule(int EmployeeID, int nMonth, int nYear)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@leavemonth", nMonth);
                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);
                objParam[++i] = new SqlParameter("@leaveyear", nYear);

                objDA.sqlCmdText = "hrm_LeaveRule_Applied";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal LopLeaveTaken(int EmployeeID, int nYear, int nMonth, decimal minimumleaves)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@salmonth", nMonth);
                objParam[++i] = new SqlParameter("@EmpID", EmployeeID);
                objParam[++i] = new SqlParameter("@salyear", nYear);
                objParam[++i] = new SqlParameter("@minleaves", minimumleaves);

                objDA.sqlCmdText = "hrm_LeaveTaken_Attendance_Missed";
                objDA.sqlParam = objParam;
                return Util.ToDecimal(objDA.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     

        public int offdayselect(int EmployeeID, string latedate)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;

            try
            {


                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);

                objParam[++i] = new SqlParameter("@leavedate", latedate);

                objDA.sqlCmdText = "hrm_getoffdayrecord";
                objDA.sqlParam = objParam;
                return Util.ToInt(objDA.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
