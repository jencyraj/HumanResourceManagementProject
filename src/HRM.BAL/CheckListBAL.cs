using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class CheckListBAL
    {
        
        public int Save(CheckListBOL objChek)
        {
            CheckListDAL objDAL = new CheckListDAL();
            return objDAL.Save(objChek);
        }

        public int Delete(int nChekID)
        {
            CheckListDAL objDAL = new CheckListDAL();
            return objDAL.Delete(nChekID);
        }

        public DataTable SelectAll(int nChekID)
        {
            CheckListDAL objDAL = new CheckListDAL();
            return objDAL.SelectAll(nChekID);
        }
    }
}
