using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class OrgDesignationBOL
    {

        public int DesignationID { get; set; } // Primary Key
        public int CompanyID { get; set; }
        public int ParentID { get; set; }

        public string DesgnCode { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }

    }
}
