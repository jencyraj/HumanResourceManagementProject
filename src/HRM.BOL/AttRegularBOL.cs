using System;
using System.Text;
using System.Data;

namespace HRM.BOL
{
    public class AttRegularBOL
    {
        public int ReqID;
        public int ReqDetailID;
        public int EmployeeID;
        public int ReqMonth;
        public int ReqYear;
        public string ReqReason;
        public string Status;
        public string CreatedBy;

        public DataTable DTAttendance;
        public DateTime AttendanceDate;
        public int AttendanceDay;
        public int AttendanceMon;
        public int Attendanceyear;
        public string Dstatus;
        public string ApprovedBy;
        public string ApprovedStatus;
        public string RejectReason;
        public string ApprovedDate;


        public int AttendanceId;
        public int WorkShiftId;

        public string StartTime;
        public string EndTime;
      
    }
}
