using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class UserBOL
    {

        public int UID { get; set; }
        public int RoleID { get; set; } 
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string BiometricID { get; set; }
        public string IRISID { get; set; }

    }
}
