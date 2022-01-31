using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class OrgBranchesBOL
    {

        public int BranchID { get; set; } // Primary Key
        public int CompanyID { get; set; }

        public string Branch { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public int CountryID { get; set; }
    }
}
