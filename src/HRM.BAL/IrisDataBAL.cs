using HRM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using HRM.BOL;

namespace HRM.BAL
{
    public class IrisDataBAL
    {
        public DataTable SelectAll(int BranchID)
        {
            IrisDataDAL objDAL = new IrisDataDAL();
            return objDAL.SelectAll(BranchID);
        }

        public string SaveHolidays(DataTable dTable, int BranchID)
        {
            IrisDataDAL objDAL = new IrisDataDAL();
            return objDAL.SaveHolidays(dTable, BranchID);
        }

        public string SaveTransactions(DataTable dTable, string StartDate, string EndDate)
        {
            IrisDataDAL objDAL = new IrisDataDAL();
            return objDAL.SaveTransactions(dTable, StartDate, EndDate);
        }


        public DataTable SelectIrisDetails(IrisDataBOL objBOL)
        {
            IrisDataDAL objDAL = new IrisDataDAL();
            return objDAL.SelectIrisDetails(objBOL);

        }
        
    }
}
