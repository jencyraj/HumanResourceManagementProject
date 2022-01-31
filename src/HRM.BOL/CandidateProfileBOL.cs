using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class CandidateProfileBOL
    {
        public int CandidateID { get; set; }// Primary Key
        public int JID { get; set; }
        public string SaluteName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }

        public string NationalityCode { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }
        public string Skills { get; set; }
        public string Interests { get; set; }
        public string Achievements { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime AppliedDate { get; set; }
        public string AppliedStatus { get; set; }
        public string JobTitle { get; set; }
        public string Status  { get; set; }
        public int HistoryID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modifieddate { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Nationality { get; set; }
        public string EmailContent { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
    }
}
