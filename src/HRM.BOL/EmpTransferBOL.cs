using System;

namespace HRM.BOL
{
    public class EmpTransferBOL
    {
        public int TransferID { get; set; }
        public int EmployeeID { get; set; }
        public int ForwardTo { get; set; }
        public DateTime TransferDate { get; set; }
        public int BranchFrom { get; set; }
        public int BranchTo { get; set; }
        public int DeptFrom { get; set; }
        public int DeptTo { get; set; }
        public int SubDeptFrom { get; set; }
        public int SubDeptTo { get; set; }
        public int ReportTo { get; set; }
        public string ApprovedBy { get; set; }
        public string Approve_Old_Branch { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_New_Branch { get; set; }
        public DateTime Approve_New_Date { get; set; }
        public string Approved_New_By { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
