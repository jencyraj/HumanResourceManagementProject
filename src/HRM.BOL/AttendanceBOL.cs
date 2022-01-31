using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace HRM.BOL
{
    public class AttendanceBOL
    {
        public int AttendanceId;
        public int AttendanceTypeId;
        public int BranchId;
        public int EmployeeId;
        public string AttendanceDate;
        public string AttendanceType;
        public string SignInTime;
        public string SignOutTime;
        public string AdditionalInfo;
        public string Status;
        public string ShiftType;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public string Approved;
        public string ApprovedBy;
        public DateTime ApprovedDate;
        public string RejectReason;
        public string Year;
        public string Month;
        public int RoleId;
        public int LoggedInEmployeeId;
        public List<AttendanceBreakBOL> Breaks = new List<AttendanceBreakBOL>();
        public List<AttendanceOvertimeBOL> Overtimes = new List<AttendanceOvertimeBOL>();
    }

    public class AttendanceBreakBOL
    {
        public int BreakId;
        public int AttendanceId;
        public string StartTime;
        public string EndTime;
        public string Description;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
    }

    public class AttendanceOvertimeBOL
    {
        public int OvertimeId;
        public int AttendanceId;
        public string StartTime;
        public string EndTime;
        public string Description;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
    }
    public class AttendanceRuleBOL
    {
        public int RuleID;
        public string UseValue;
        public string UseRule;
        public Decimal ZeroAttendanceTo;
        public Decimal ZeroAttendanceFrom;
        public Decimal FullAttendanceFrom;
        public Decimal FullAttendanceTo;
        public Decimal HalfAttendanceFrom;
        public Decimal HalfAttendanceTo;
        public string Active;
        public string CreatedBy;
        public string ModifiedBy;
        public DateTime ModifiedDate;
    }
}
