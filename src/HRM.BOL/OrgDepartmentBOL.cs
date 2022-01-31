using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class OrgDepartmentBOL
    {

        public int DeptID { get; set; } // Primary Key
        public int CompanyID { get; set; }
        public int ParentDeptID { get; set; }
        public int BranchID { get; set; }

        public string DeptCode { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string Branches { get; set; }

    }
}
