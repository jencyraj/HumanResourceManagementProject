using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class CommissionBAL
    {
        public int Save(CommissionBOL objCommission)
        {
            CommissionDAL objDAL = new CommissionDAL();
            return objDAL.Save(objCommission);
        }

        public int Delete(int nCommissionID)
        {
            CommissionDAL objDAL = new CommissionDAL();
            return objDAL.Delete(nCommissionID);
        }

        public DataTable SelectAll(CommissionBOL objCommission)
        {
            CommissionDAL objDAL = new CommissionDAL();
            return objDAL.SelectAll(objCommission);
        }

        public CommissionBOL SearchById(int nCommissionID)
        {
            CommissionDAL objDAL = new CommissionDAL();
            return objDAL.SearchById(nCommissionID);
        }
    }
}
