using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class AttendanceDAL
    {

        public int IsLeaveApproved(int nEmployeeId)
        {
            int LeaveApproved = 0;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@EmployeeId", nEmployeeId));
                objDA.sqlCmdText = "hrm_Attendance_LeaveApproved";
                objDA.sqlParam = objParam.ToArray();
                LeaveApproved = Util.ToInt(objDA.ExecuteScalar().ToString());
            }
            catch
            {
                LeaveApproved = 0;
            }
            return LeaveApproved;
        }

        public int IsLeaveApproved(int nEmployeeId, string ATTDATE)
        {
            int LeaveApproved = 0;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@EmployeeId", nEmployeeId));
                objParam.Add(new SqlParameter("@ATTDATE", ATTDATE));
                objDA.sqlCmdText = "hrm_Attendance_LeaveApproved";
                objDA.sqlParam = objParam.ToArray();
                LeaveApproved = Util.ToInt(objDA.ExecuteScalar().ToString());
            }
            catch
            {
                LeaveApproved = 0;
            }
            return LeaveApproved;
        }


        public void Save(AttendanceBOL objAttendance)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                int AttendanceId = 0;
                objParam.Add(new SqlParameter("@AttendanceId", objAttendance.AttendanceId));
                objParam.Add(new SqlParameter("@AttendanceTypeId", objAttendance.AttendanceTypeId));
                objParam.Add(new SqlParameter("@EmployeeId", objAttendance.EmployeeId));
                objParam.Add(new SqlParameter("@AttendanceDate", objAttendance.AttendanceDate));
                objParam.Add(new SqlParameter("@SignInTime", objAttendance.SignInTime));
                objParam.Add(new SqlParameter("@SignOutTime", objAttendance.SignOutTime));
                objParam.Add(new SqlParameter("@AdditionalInfo", objAttendance.AdditionalInfo));
                objParam.Add(new SqlParameter("@Status", objAttendance.Status));
                objParam.Add(new SqlParameter("@Shift", objAttendance.ShiftType));
                objParam.Add(new SqlParameter("@CreatedBy", objAttendance.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objAttendance.ModifiedBy));
                objParam.Add(new SqlParameter("@Approved", objAttendance.Approved));
                objDA.sqlCmdText = "hrm_Attendance_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                AttendanceId = Util.ToInt(objDA.ExecuteScalar().ToString());
                SaveAttendanceBreaks(objAttendance.Breaks, AttendanceId);
                SaveAttendanceOvertimes(objAttendance.Overtimes, AttendanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Save_Attendance_Rule(AttendanceRuleBOL objAttendance)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {	
               
                objParam.Add(new SqlParameter("@RuleID", objAttendance.RuleID));
                objParam.Add(new SqlParameter("@UseValue", objAttendance.UseValue));
                objParam.Add(new SqlParameter("@UseRule", objAttendance.UseRule));
                objParam.Add(new SqlParameter("@ZeroAttendanceTo", objAttendance.ZeroAttendanceTo));
                objParam.Add(new SqlParameter("@ZeroAttendanceFrom", objAttendance.ZeroAttendanceFrom));
                objParam.Add(new SqlParameter("@FullAttendanceFrom", objAttendance.FullAttendanceFrom));
                objParam.Add(new SqlParameter("@FullAttendanceTo", objAttendance.FullAttendanceTo));
                objParam.Add(new SqlParameter("@HalfAttendanceFrom", objAttendance.HalfAttendanceFrom));
                objParam.Add(new SqlParameter("@HalfAttendanceTo", objAttendance.HalfAttendanceTo));
                objParam.Add(new SqlParameter("@CreatedBy", objAttendance.CreatedBy));

                objDA.sqlCmdText = "[hrm_Attendance_Rules_Insert_Update]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveAttendanceBreaks(List<AttendanceBreakBOL> Breaks, int AttendanceId)
        {
            foreach (AttendanceBreakBOL Break in Breaks)
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                try
                {
                    objParam.Add(new SqlParameter("@BreakId", Break.BreakId));
                    objParam.Add(new SqlParameter("@AttendanceId", AttendanceId));
                    objParam.Add(new SqlParameter("@StartTime", Break.StartTime));
                    objParam.Add(new SqlParameter("@EndTime", Break.EndTime));
                    objParam.Add(new SqlParameter("@Description", Break.Description));
                    objParam.Add(new SqlParameter("@Status", Break.Status));
                    objParam.Add(new SqlParameter("@CreatedBy", Break.CreatedBy));
                    objParam.Add(new SqlParameter("@ModifiedBy", Break.ModifiedBy));
                    objDA.sqlCmdText = "hrm_Attendance_Break_INSERT_UPDATE";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void SaveAttendanceOvertimes(List<AttendanceOvertimeBOL> Overtimes, int AttendanceId)
        {
            foreach (AttendanceOvertimeBOL Overtime in Overtimes)
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                try
                {
                    objParam.Add(new SqlParameter("@OvertimeId", Overtime.OvertimeId));
                    objParam.Add(new SqlParameter("@AttendanceId", AttendanceId));
                    objParam.Add(new SqlParameter("@StartTime", Overtime.StartTime));
                    objParam.Add(new SqlParameter("@EndTime", Overtime.EndTime));
                    objParam.Add(new SqlParameter("@Description", Overtime.Description));
                    objParam.Add(new SqlParameter("@Status", Overtime.Status));
                    objParam.Add(new SqlParameter("@CreatedBy", Overtime.CreatedBy));
                    objParam.Add(new SqlParameter("@ModifiedBy", Overtime.ModifiedBy));
                    objDA.sqlCmdText = "hrm_Attendance_Overtime_INSERT_UPDATE";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable Search(AttendanceBOL objBOL)
        {
            DataTable dtAttendance = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.AttendanceId > 0)
                        objParam.Add(new SqlParameter("@AttendanceId", objBOL.AttendanceId));
                    if (objBOL.AttendanceTypeId > 0)
                        objParam.Add(new SqlParameter("@AttendanceTypeId", objBOL.AttendanceTypeId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    if (objBOL.AttendanceDate != null)
                        objParam.Add(new SqlParameter("@AttendanceDate", objBOL.AttendanceDate));
                    if (objBOL.Year != null)
                        objParam.Add(new SqlParameter("@Year", objBOL.Year));
                    if (objBOL.Month != null)
                        objParam.Add(new SqlParameter("@Month", objBOL.Month));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Attendance_SelectAll";
                dtAttendance = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtAttendance = new DataTable();
            }
            return dtAttendance;
        }
        public DataTable Get_Active_AttendanceRule()
        {
            DataTable dtAttendance = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "[hrm_GetActiveAttendanceRule]";
                dtAttendance = objDA.ExecuteDataSet().Tables[0];

            }
            catch
            {
                dtAttendance = new DataTable();
            }
            return dtAttendance;
        }
        public DataTable SearchForApproval(AttendanceBOL objBOL)
        {
            DataTable dtAttendance = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.AttendanceId > 0)
                        objParam.Add(new SqlParameter("@AttendanceId", objBOL.AttendanceId));
                    if (objBOL.AttendanceTypeId > 0)
                        objParam.Add(new SqlParameter("@AttendanceTypeId", objBOL.AttendanceTypeId));
                    if (objBOL.BranchId > 0)
                        objParam.Add(new SqlParameter("@BranchId", objBOL.BranchId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    if (objBOL.AttendanceDate != null)
                        objParam.Add(new SqlParameter("@AttendanceDate", objBOL.AttendanceDate));
                    if (objBOL.Year != null)
                        objParam.Add(new SqlParameter("@Year", objBOL.Year));
                    if (objBOL.Month != null)
                        objParam.Add(new SqlParameter("@Month", objBOL.Month));
                    if (objBOL.Approved != null)
                        objParam.Add(new SqlParameter("@Approved", objBOL.Approved));
                    objParam.Add(new SqlParameter("@RoleId", objBOL.RoleId));
                    objParam.Add(new SqlParameter("@LoggedInEmployeeId", objBOL.LoggedInEmployeeId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Attendance_SelectAll_Approval";
                dtAttendance = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtAttendance = new DataTable();
            }
            return dtAttendance;
        }

        public AttendanceBOL SearchById(int nAttendanceId)
        {
            AttendanceBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nAttendanceId > 0)
                {
                    objBOL = new AttendanceBOL();
                    objBOL.AttendanceId = nAttendanceId;
                    objBOL.AttendanceTypeId = 0;
                    objBOL.EmployeeId = 0;
                    objBOL.AttendanceDate = null;
                    objBOL.Year = null;
                    objBOL.Month = null;
                    objBOL.Approved = null;
                    DataTable dt = Search(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.AttendanceId = nAttendanceId;
                        objBOL.AttendanceTypeId = Util.ToInt("" + dt.Rows[0]["AttendanceTypeId"]);
                        objBOL.AttendanceType = "" + dt.Rows[0]["AttendanceType"];
                        objBOL.EmployeeId = Util.ToInt("" + dt.Rows[0]["EmployeeId"]);
                        objBOL.AttendanceDate = "" + dt.Rows[0]["AttendanceDate"];
                        objBOL.SignInTime = "" + dt.Rows[0]["SignInTime"];
                        objBOL.SignOutTime = "" + dt.Rows[0]["SignOutTime"];
                        objBOL.AdditionalInfo = "" + dt.Rows[0]["AdditionalInfo"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
                        objBOL.CreatedDate = Util.ToDateTime("" + dt.Rows[0]["CreatedDate"]);
                        objBOL.ModifiedBy = "" + dt.Rows[0]["ModifiedBy"];
                        objBOL.ModifiedDate = Util.ToDateTime("" + dt.Rows[0]["ModifiedDate"]);
                        objBOL.Approved = "" + dt.Rows[0]["Approved"];
                        objBOL.ApprovedBy = "" + dt.Rows[0]["ApprovedBy"];
                        objBOL.ApprovedDate = Util.ToDateTime("" + dt.Rows[0]["ApprovedDate"]);
                        objBOL.RejectReason = "" + dt.Rows[0]["RejectReason"];
                        objBOL.Breaks = GetBreaks(nAttendanceId);
                        objBOL.Overtimes = GetOvertimes(nAttendanceId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }

        public List<AttendanceBreakBOL> GetBreaks(int nAttendanceId)
        {
            DataTable dtBreaks = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (nAttendanceId != 0)
                {
                    objParam.Add(new SqlParameter("@AttendanceId", nAttendanceId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Attendance_Break_SelectAll";
                dtBreaks = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtBreaks = new DataTable();
            }
            List<AttendanceBreakBOL> Breaks = new List<AttendanceBreakBOL>();
            foreach (DataRow dr in dtBreaks.Rows)
            {
                AttendanceBreakBOL Break = new AttendanceBreakBOL();
                Break.BreakId = Util.ToInt("" + dr["BreakId"]);
                Break.AttendanceId = nAttendanceId;
                Break.StartTime = "" + dr["StartTime"];
                Break.EndTime = "" + dr["EndTime"];
                Break.Description = "" + dr["Description"];
                Break.Status = "" + dr["Status"];
                Break.CreatedBy = "" + dr["CreatedBy"];
                Break.CreatedDate = Util.ToDateTime("" + dr["CreatedDate"]);
                Break.ModifiedBy = "" + dr["ModifiedBy"];
                Break.ModifiedDate = Util.ToDateTime("" + dr["ModifiedDate"]);
                Breaks.Add(Break);
            }
            return Breaks;
        }

        public List<AttendanceOvertimeBOL> GetOvertimes(int nAttendanceId)
        {
            DataTable dtOvertimes = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (nAttendanceId != 0)
                {
                    objParam.Add(new SqlParameter("@AttendanceId", nAttendanceId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Attendance_Overtime_SelectAll";
                dtOvertimes = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtOvertimes = new DataTable();
            }
            List<AttendanceOvertimeBOL> Overtimes = new List<AttendanceOvertimeBOL>();
            foreach (DataRow dr in dtOvertimes.Rows)
            {
                AttendanceOvertimeBOL Overtime = new AttendanceOvertimeBOL();
                Overtime.OvertimeId = Util.ToInt("" + dr["OvertimeId"]);
                Overtime.AttendanceId = nAttendanceId;
                Overtime.StartTime = "" + dr["StartTime"];
                Overtime.EndTime = "" + dr["EndTime"];
                Overtime.Description = "" + dr["Description"];
                Overtime.Status = "" + dr["Status"];
                Overtime.CreatedBy = "" + dr["CreatedBy"];
                Overtime.CreatedDate = Util.ToDateTime("" + dr["CreatedDate"]);
                Overtime.ModifiedBy = "" + dr["ModifiedBy"];
                Overtime.ModifiedDate = Util.ToDateTime("" + dr["ModifiedDate"]);
                Overtimes.Add(Overtime);
            }
            return Overtimes;
        }

        public int Delete(int nAttendanceId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@AttendanceId", nAttendanceId));
                objDA.sqlCmdText = "hrm_Attendance_Delete";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteBreak(int nBreakId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@BreakId", nBreakId));
                objDA.sqlCmdText = "hrm_Attendance_Break_Delete";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteOvertime(int nOvertimeId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@OvertimeId", nOvertimeId));
                objDA.sqlCmdText = "hrm_Attendance_Overtime_Delete";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApproveAttendance(List<int> listAttendanceId, string Approved, string ApprovedBy, string RejectReason)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = null;
            foreach (int AttendanceId in listAttendanceId)
            {
                try
                {
                    objParam = new List<SqlParameter>();
                    objParam.Add(new SqlParameter("@AttendanceId", AttendanceId));
                    objParam.Add(new SqlParameter("@Approved", Approved));
                    objParam.Add(new SqlParameter("@ApprovedBy", ApprovedBy));
                    objParam.Add(new SqlParameter("@RejectReason", RejectReason));
                    objDA.sqlCmdText = "hrm_ApproveAttendance";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteNonQuery();
                }
                catch
                {
                    throw; 
                }
            }
        }
    }
}
