using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class WorkPlanBOL
    {
        public int WPID;
        public int WSID;
        public int WPMID;
        public int EmployeeID;
        public int WPYear;
        public int WPMonth;
        public DateTime FromDate;
        public DateTime ToDate;
        public string Status;
        public string CreatedBy;
        public int BranchID;
    }
}
