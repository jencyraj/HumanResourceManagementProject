using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class RegnTypesBAL
    {
        public int Save(RegnTypesBOL objRegn)
        {
            RegnTypesDAL objDAL = new RegnTypesDAL();
            return objDAL.Save(objRegn);
        }

        public int Delete(int nIdentifierID)
        {
            RegnTypesDAL objDAL = new RegnTypesDAL();
            return objDAL.Delete(nIdentifierID);
        }

        public DataTable SelectAll(int nCompanyID)
        {
            RegnTypesDAL objDAL = new RegnTypesDAL();
            return objDAL.SelectAll(nCompanyID);
        }
    }
}
