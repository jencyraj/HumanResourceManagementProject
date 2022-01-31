using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class WarningsBAL
    {
        public int Save(WarningsBOL objWarn)
        {
            WarningsDAL objDAL = new WarningsDAL();
            return objDAL.Save(objWarn);
        }

        public int Delete(int nWarningID)
        {
            WarningsDAL objDAL = new WarningsDAL();
            return objDAL.Delete(nWarningID);
        }

        public DataTable SelectAll(WarningsBOL objWarn)
        {
            WarningsDAL objDAL = new WarningsDAL();
            return objDAL.SelectAll(objWarn);
        }

    }
}
