using System;

namespace HRM.BOL
{
    public class TerminationBOL
    {
        public int TID { get; set; }
        public int EmployeeID { get; set; }
        public int ForwardedTo { get; set; }
        public string Reason { get; set; }
        public string Approved { get; set; }
        public int ApprovedBy { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string EmployeeName { get; set; }
        public string ForwardedToName { get; set; }
        public string ApprovalReason { get; set; }
        public bool IsExitInterview { get; set; }
        public int InterviewerId { get; set; }
        public string InterviewDate { get; set; }
        public string InterviewRemarks { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
