using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class BonusBAL
    {
        public int Save(BonusBOL objBonus)
        {
            BonusDAL objDAL = new BonusDAL();
            return objDAL.Save(objBonus);
        }

        public int Delete(int nBonusID)
        {
            BonusDAL objDAL = new BonusDAL();
            return objDAL.Delete(nBonusID);
        }

        public DataTable SelectAll(BonusBOL objBonus)
        {
            BonusDAL objDAL = new BonusDAL();
            return objDAL.SelectAll(objBonus);
        }

        public BonusBOL SearchById(int nBonusID)
        {
            BonusDAL objDAL = new BonusDAL();
            return objDAL.SearchById(nBonusID);
        }
    }
}
