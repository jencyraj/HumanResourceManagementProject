using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class MemoBAL
    {
        public int Save(MemoBOL objMemo)
        {
            MemoDAL objDAL = new MemoDAL();
            return objDAL.Save(objMemo);
        }

        public int Delete(int nMemoID)
        {
            MemoDAL objDAL = new MemoDAL();
            return objDAL.Delete(nMemoID);
        }
        public int ChangeStatus(int nMemoID, string status, int modifiedby)
        {
            MemoDAL objDAL = new MemoDAL();
            return objDAL.ChangeStatus(nMemoID, status, modifiedby);
        }
        public DataTable SelectAll(MemoBOL objMemo)
        {
            MemoDAL objDAL = new MemoDAL();
            return objDAL.SelectAll(objMemo);
        }
        public DataTable SelectEmployeeByMemoID(int MemoID)
        {
            MemoDAL objDAL = new MemoDAL();
            return objDAL. SelectEmployeeByMemoID(MemoID);
        }
    }
}
