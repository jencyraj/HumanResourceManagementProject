using System;

namespace HRM.BOL
{
    public class WarningsBOL
    {
        public int WarningID { get; set; }
        public int WarningFrom { get; set; }
        public int WarningTo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
    }
}
