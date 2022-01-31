using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
   public class OverTimeBOL
    {
      
        public int RId;

        public decimal MinimumHr;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public string Ruleapplicable;
        public DateTime ApplicableSum;
        public List<OvertimeviewBOL> Overtimes = new List<OvertimeviewBOL>();
    }
   public class OvertimeviewBOL
   {

       public int RId;

       public decimal MinimumHr;
       public string Status;
       public string CreatedBy;
       public DateTime CreatedDate;
       public string ModifiedBy;
       public DateTime ModifiedDate;
       public string Ruleapplicable;
       public DateTime ApplicableSum;
   }
}
