using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class PayrollPeriodBOL
    {
        public int PPId;
	    public string Title;
	    public int DayStart;
	    public int MonthStart;
	    public int YearStart;
	    public int DayEnd;
        public int MonthEnd;
	    public int YearEnd;
	    public string Active;
	    public string Status;
	    public string CreatedBy;
        public DateTime CreatedDate;
	    public string ModifiedBy;
        public DateTime ModifiedDate;
        public string Year;
    }
}
