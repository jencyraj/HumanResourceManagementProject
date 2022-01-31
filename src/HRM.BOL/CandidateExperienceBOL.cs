using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class CandidateExperienceBOL
    {
        public int ExperienceID { get; set; }// Primary Key
        public int CandidateID { get; set; }

        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string FromDate   { get; set; }
        public string ToDate { get; set; }
        public string ReasonforLeaving { get; set; }

    }
}
