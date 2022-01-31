using System;

using HRM.DAL;

namespace HRM.BAL
{
    public class DBBackUpBAL
    {
        public string DBBACKUP(string sPath)
        {
            DBBackUpDAL objDAL = new DBBackUpDAL();
            return objDAL.DBBACKUP(sPath);
        }
    }
}
