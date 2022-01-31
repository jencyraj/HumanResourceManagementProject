using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class DeductionBAL
    {
        public int Save(DeductionBOL objDeduction)
        {
            DeductionDAL objDAL = new DeductionDAL();
            return objDAL.Save(objDeduction);
        }

        public int Delete(int nDeductionID)
        {
            DeductionDAL objDAL = new DeductionDAL();
            return objDAL.Delete(nDeductionID);
        }

        public DataTable SelectAll(int nDeductionID)
        {
            DeductionDAL objDAL = new DeductionDAL();
            return objDAL.SelectAll(nDeductionID);
        }
    }
}
