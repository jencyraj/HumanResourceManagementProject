using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class BenefitBOL
    {

        public int BenefitTypeID;
        public string BenefitType;
        public string Status;
        public string ActivePack;
        public string CreatedBy;

        public int BenefitID;
        public string BenefitDesc;

        public int EmployeeID;
        public string AvailedBy;
        public decimal EmployeeShare;
        public decimal CompanyShare;
        public string AdditionalInfo;

    }
}
