using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class LeaveTypesBOL
    {

        public int LeaveTypeID { get; set; } // Primary Key
        public int BranchID { get; set; } 
       
        public string LeaveName { get; set; }
        public string ShortName { get; set; }
        public int LeaveType { get; set; }
        public int LeaveDays { get; set; }
        public string CarryOver { get; set; }
        public string Deduction { get; set; }
        public string Status { get; set; }

        public string CreatedBy { get; set; }
    }
}
