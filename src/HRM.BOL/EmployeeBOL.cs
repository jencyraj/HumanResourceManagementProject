using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class EmployeeBOL
    {

        public EmployeeBOL()
        {
            Branch = new OrgBranchesBOL();
            Department = new OrgDepartmentBOL();
            Designation = new OrgDesignationBOL();
            Roles = new RoleBOL();
        }

        public int EmployeeID;
        public int BranchID;
        public int DeptId;
        public int SubDeptID;
        public int DesgnID;
        public int RoleID;
        public string CountryID;

        public string UserID;
        public string Password;
        public string EmpStatus;
        public string EmpCode;
        public string IDDesc;
        public string IDNO;

        public string BiometricID;
        public string IRISID;

        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string Gender;
        public string BloodGroup;
        public string SaluteName;
        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public string Country;
        public string ZipCode;
        public string HPhone;
        public string HMobile;
        public string HEmail;
        public string Website;
        public string NationalityCode;
        public string MaritalStatus;
        public string WPhone;
        public string WEmail;
        public string JobType;
        public string BankName;
        public string BankBranch;
        public string OtherBankDetails;
        public string AccountNumber;
        public string Status;
        public string CreatedBy;
        public string ModifiedBy;
        public string PhotoName;

        public string DateOfBirth;
        public string DateOfBirthAR;
        public string JoiningDate;
        public string ProbationStartDate;
        public string ProbationEndDate;
        public DateTime CreatedDate;
        public DateTime ModifiedDate;

        public OrgDesignationBOL Designation;
        public OrgDepartmentBOL Department;
        public OrgBranchesBOL Branch;
        public RoleBOL Roles;

        //Emergency Contacts
        public int EmgContactID;
        public string ContactName;
        public string Relationship;

        //Dependants
        public int DependentID;
        public string DependentName;

        //Education
        public int EducationID;
        public string EduLevel;
        public string University;
        public string College;
        public string Specialization;
        public int PassedYear;
        public string ScorePercentage;
        public string StartDate;
        public string EndDate;

        //Experience
        public int ExpID;
        public string Company;
        public string JobTitle;
        public string FromDate;
        public string ToDate;
        public string Comments;

        //Immigration
        public int ImmigrationId;
        public string DocType;
        public string DocNo;
        public string IssueDate;
        public string ExpiryDate;
        public string EligibleStatus;
        public string IssuedCountryID;
        public string EligibleReviewDate;

        //Language
        public int LangID;
        public string LanguageName;
        public string Fluency;
        public string Competency;

        //Skills
        public int SkillID;
        public string Description;
        public string ExpinYears;

        ///Report To
        public int SuperiorID;
        public string ImmediateSuperior;


        //for rpt
        public int Year;
        public int month;
    }
}
