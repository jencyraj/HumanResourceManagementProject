using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AllowanceBAL
    {
        public int Save(AllowanceBOL objAllowance)
        {
            AllowanceDAL objDAL = new AllowanceDAL();
            return objDAL.Save(objAllowance);
        }

        public int Delete(int nAllowanceID)
        {
            AllowanceDAL objDAL = new AllowanceDAL();
            return objDAL.Delete(nAllowanceID);
        }

        public DataTable SelectAll(int nAllowanceID)
        {
            AllowanceDAL objDAL = new AllowanceDAL();
            return objDAL.SelectAll(nAllowanceID);
        }
    }
}
