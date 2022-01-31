using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class CandidateEducationBOL
    {
        public int EducationID { get; set; }// Primary Key
        public int CandidateID { get; set; }
        public int EduLevel { get; set; }
        public string University { get; set; }
        public string College { get; set; }
        public string Specialization { get; set; }
        public string PassedYear { get; set; }
        public string ScorePercentage { get; set; }
    }
}
