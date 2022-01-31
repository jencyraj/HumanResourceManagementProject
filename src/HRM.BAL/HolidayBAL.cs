using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class HolidayBAL
    {
        public int Save(HolidayBOL objHo)
        {
            HolidayDAL objDAL = new HolidayDAL();
            return objDAL.Save(objHo);
        }

        public int Delete(int nHolidayID, string sCreatedBy)
        {
            HolidayDAL objDAL = new HolidayDAL();
            return objDAL.Delete(nHolidayID, sCreatedBy);
        }

        public DataTable SelectAll(int BranchID)
        {
            HolidayDAL objDAL = new HolidayDAL();
            return objDAL.SelectAll(BranchID);
        }

        public DataTable GetHolidays(int branch, int month, int year)
        {
            return new HolidayDAL().GetHolidays(branch, month, year);
        }
    }
}
