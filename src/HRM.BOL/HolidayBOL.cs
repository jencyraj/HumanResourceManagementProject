using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class HolidayBOL
    {

        public int HolidayID { get; set; } // Primary Key
        public int BranchID { get; set; } 
       
        public string Description { get; set; }
        public DateTime Holiday { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
