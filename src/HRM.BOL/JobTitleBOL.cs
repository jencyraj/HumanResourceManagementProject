using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class JobTitleBOL
    {
        public int JID { get; set; }// Primary Key
        public int DepartmentID { get; set; } 
        public int EmplStatusID { get; set; }
        public int VacancyNos { get; set; }
        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }

        public string JobTitle { get; set; }
        public string Experience { get; set; }
        public string Qualification { get; set; }
        public string JobPostDescription { get; set; }
        public string AdditionalInfo { get; set; }

        public string Status { get; set; }
        public string Published { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public string ClosingDate { get; set; }
        public string PublishedDate { get; set; }
        public string PublishedBy { get; set; }
        public string ApplicationStatus { get; set; }
        public string Branches { get; set; }
    }
}
