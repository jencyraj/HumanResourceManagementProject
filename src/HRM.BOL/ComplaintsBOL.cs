using System;

namespace HRM.BOL
{
    public class ComplaintsBOL
    {
        public int ComplaintID { get; set; }
        public string EmployeeID { get; set; }
        public int ComplaintBy { get; set; }
        public string ComplaintTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
