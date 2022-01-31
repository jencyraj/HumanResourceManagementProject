using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class AttendanceBAL
    {
        public void Save(AttendanceBOL objAttendance)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            objDAL.Save(objAttendance);
        }

        public int IsLeaveApproved(int nEmployeeId)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.IsLeaveApproved(nEmployeeId);
        }

        public int Save_Attendance_Rule(AttendanceRuleBOL objAttendance)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.Save_Attendance_Rule(objAttendance);
        }

        public int IsLeaveApproved(int nEmployeeId, string ATTDATE)
        {
           return new AttendanceDAL().IsLeaveApproved(nEmployeeId, ATTDATE);
        }

        public DataTable SelectAll(AttendanceBOL objAttendance)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.Search(objAttendance);
        }
        public DataTable Get_Active_AttendanceRule()
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.Get_Active_AttendanceRule();
        }
        public DataTable SearchForApproval(AttendanceBOL objAttendance)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.SearchForApproval(objAttendance);
        }

        public AttendanceBOL SearchById(int nAttendanceId)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.SearchById(nAttendanceId);
        }

        public int Delete(int nAttendanceId)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.Delete(nAttendanceId);
        }

        public int DeleteBreak(int nBreakId)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.DeleteBreak(nBreakId);
        }

        public int DeleteOvertime(int nOvertimeId)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            return objDAL.DeleteOvertime(nOvertimeId);
        }

        public void ApproveAttendance(List<int> listAttendanceId, string Approved, string ApprovedBy, string RejectReason)
        {
            AttendanceDAL objDAL = new AttendanceDAL();
            objDAL.ApproveAttendance(listAttendanceId, Approved, ApprovedBy, RejectReason);
        }
    }
}
