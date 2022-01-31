using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
   public class ReqTypeBAL
    {
        public int Save(ReqTypeBOL objChek)
        {
            ReqTypeDAL objDAL = new ReqTypeDAL();
            return objDAL.Save(objChek);
        }

        public int Delete(int nChekID)
        {
            ReqTypeDAL objDAL = new ReqTypeDAL();
            return objDAL.Delete(nChekID);
        }

        public DataTable SelectAll(int nChekID)
        {
            ReqTypeDAL objDAL = new ReqTypeDAL();
            return objDAL.SelectAll(nChekID);
        }
    }
}
