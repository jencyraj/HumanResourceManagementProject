using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class RatingBOL
    {

        public int RatingID { get; set; } // Primary Key

        public string RatingDesc { get; set; }
        public int MaxScore { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }

    }
}
