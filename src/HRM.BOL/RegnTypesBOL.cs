﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class RegnTypesBOL
    {

        public int IdentifierID { get; set; } // Primary Key
        public int CompanyID { get; set; }

        public string Description { get; set; }
        public string IdentifierValue { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }

    }
}
