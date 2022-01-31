using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class HourlyWageBOL
    {
        public int HourlyWageId;
        public int EmployeeId;
        public int DesignationId;
        public decimal RegularHours;
        public decimal OverTimeHours;
        public decimal OverTimewekend;
        public string AdditionalInfo;
        public int HMonth;
        public int HYear;
        public string ActiveWage;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public int HourlyWagePresent;
    }
}
