using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class TerminationBAL
    {
        public int Save(TerminationBOL objTerm)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.Save(objTerm);
        }

        public int Approve(TerminationBOL objTerm)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.Approve(objTerm);
        }

        public int Delete(int nTID)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.Delete(nTID);
        }
        public int Update_Interview_Closed(TerminationBOL objTerm)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.Update_Interview_Closed( objTerm);
        }
        public DataTable SelectAll(TerminationBOL objTerm)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.SelectAll(objTerm);
        }

        public TerminationBOL SelectByID(int TID)
        {
            TerminationDAL objDAL = new TerminationDAL();
            return objDAL.SelectByID(TID);
        }

    }
}
