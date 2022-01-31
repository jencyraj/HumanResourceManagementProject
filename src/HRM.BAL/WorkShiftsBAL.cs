using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class WorkShiftsBAL
    {
        public int Save(WorkShiftsBOL objWorkShifts)
        {
            WorkShiftsDAL objDAL = new WorkShiftsDAL();
            return objDAL.Save(objWorkShifts);
        }

        public int Delete(int ID)
        {
            WorkShiftsDAL objDAL = new WorkShiftsDAL();
            return objDAL.Delete(ID);
        }

        public DataTable SelectAll(int ID)
        {
            WorkShiftsDAL objDAL = new WorkShiftsDAL();
            return objDAL.SelectAll(ID);
        }
        public DataTable Selectshift()
        {
            WorkShiftsDAL objDAL = new WorkShiftsDAL();
            return objDAL.Selectshift();
        }
    }
}
