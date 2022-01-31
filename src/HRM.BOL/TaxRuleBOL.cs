using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class TaxRuleBOL
    {
        public int TaxRuleId;
        public decimal SalaryFrom;
        public decimal SalaryTo;
        public decimal TaxPercentage;
        public decimal ExemptedTaxAmount;
        public decimal AdditionalTaxAmount;
        public string Gender;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
    }
}
