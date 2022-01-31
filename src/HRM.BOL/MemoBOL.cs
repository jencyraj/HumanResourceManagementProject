using System;

namespace HRM.BOL
{
    public class MemoBOL
    {
        public int MemoID { get; set; }
        public int MemoFrom { get; set; }
        public string MemoTo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
    }
}
