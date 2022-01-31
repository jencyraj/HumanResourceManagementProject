using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class TrainingTypeBOL
    {

        public int TID { get; set; } // Primary Key

        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
