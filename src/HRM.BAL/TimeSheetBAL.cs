using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using HRM.DAL;

namespace HRM.BAL
{
    public class TimeSheetBAL
    {
        public DataSet GetTimeSheet(int BranchID, int EmployeeID, int nYear, int nMonth)
        {
            return new TimeSheetDAL().GetTimeSheet(BranchID, EmployeeID, nYear, nMonth);
        }
        public DataTable laterpt(int BranchID, int EmployeeID, int nYear, int nMonth)
        {
            return new TimeSheetDAL().laterpt(BranchID, EmployeeID, nYear, nMonth);
        }
    }
}
