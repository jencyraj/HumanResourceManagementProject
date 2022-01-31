using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class TrainingBOL
    {

        public int TrainingID { get; set; } // Primary Key
        public int trainingtype { get; set; }
        public string subject { get; set; }
        public string nature { get; set; }
        public string title { get; set; }
        public string trainer { get; set; }
        public string location { get; set; }
        public string sponseredby { get; set; }
        public string organizedby { get; set; }
        public string fromdt { get; set; }
        public string todt { get; set; }
        public string Description { get; set; }
        public string note { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string Employee { get; set; }
        public string ApprovalStatus { get; set; }
        public string Approvedby { get; set; }
        public int TrainingLID { get; set; }
        public string TrainingLocationName { get; set; }
    }

    public class TrainingEvaluationBOL
    {
        public int Evaluateid { get; set; } // Primary Key
        public int TrainingId { get; set; }
        public string Description { get; set; }
        public int rating { get; set; }
        public int employeeid { get; set; }
    }
   
}
