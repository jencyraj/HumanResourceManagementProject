using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class PermissionBOL
    {

        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public int EmpID { get; set; }

        public string AllowInsert { get; set; }
        public string AllowUpdate { get; set; }
        public string AllowDelete { get; set; }
        public string AllowView { get; set; }
        public string Status { get; set; }

    }
}
