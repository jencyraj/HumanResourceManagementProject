using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HRM.BOL
{
  public  class LeaveRuleBOL
    {

       
        public int LDID;
        public string LBType;
        public decimal LBValue;
        public string LossType;
        public decimal LossValue;
        public string LossTime;
        public string LRStatus;
        public int LTID;
        public DataTable DTLeaveRule;
        
        public int LRID;
        public int Month;
        public decimal minleav;
        public string LDStatus;
        public string Description;
        public Byte Active;
       
        public int Year;
        public string CreatedBy;
        public DateTime CreatedDate;
       
    }
}
