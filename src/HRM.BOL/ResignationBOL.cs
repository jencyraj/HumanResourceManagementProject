using System;

namespace HRM.BOL
{
    public class ResignationBOL
    {
        public int ResgnID { get; set; }
        public int EmployeeID { get; set; }
        public int ForwardedTo { get; set; }
        public DateTime NoticeDate { get; set; }
        public DateTime ResgnDate { get; set; }
        public string Reason { get; set; }
        public string AdditionalInfo { get; set; }
        public string Approved { get; set; }
        public string ApprovedBy { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public bool IsExitInterview { get; set; }
        public int InterviewerId { get; set; }
        public string InterviewDate { get; set; }
         public string InterviewRemarks { get; set; }
        public string ApprovalReason { get; set; }
    }
}
