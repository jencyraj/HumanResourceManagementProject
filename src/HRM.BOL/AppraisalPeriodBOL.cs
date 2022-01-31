using System;

namespace HRM.BOL
{
    public class AppraisalPeriodBOL
    {
        public AppraisalPeriodBOL()
        {
            Branch = new OrgBranchesBOL();
            Department = new OrgDepartmentBOL();
            SubDepartment = new OrgDepartmentBOL();
            Designation = new OrgDesignationBOL();
        }

        public int AppPeriodID;
        public string Description;
        public DateTime StartDate;
        public DateTime EndDate;
        public string Status;
        public string CreatedBy;
        public string PeriodStatus;

        public OrgDesignationBOL Designation;
        public OrgDepartmentBOL Department;
        public OrgDepartmentBOL SubDepartment;
        public OrgBranchesBOL Branch;
    }
}
