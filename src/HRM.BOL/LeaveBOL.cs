using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class LeaveBOL
    {
        /// <summary>
        /// Leave Master
        /// </summary>
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public string Reason { get; set; }
        public string ApprovalStatus { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string RejectedBy { get; set; }
        public DateTime RejectedDate { get; set; }
        public string RejectReason { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        ///Leave Details
        public int LeaveDetailsID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime LeaveDate { get; set; }
        public string LeaveSession { get; set; }
        public string LeaveDays { get; set; }


        ///Leave Balance
        public decimal TotalLeaves { get; set; }
        public decimal LeavesTaken { get; set; }
        public int LeaveYear { get; set; }
        public int LeaveMonth { get; set; }
        public decimal PrevYearBalance { get; set; }
        public decimal LeavesBalance { get; set; }
    }
}
