using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class ComplaintsBAL
    {
        public int Save(ComplaintsBOL objCom)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.Save(objCom);
        }

        public int Delete(int nComplaintID, string sDeletedBy)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.Delete(nComplaintID,sDeletedBy);
        }

        public int UpdateStatus(int nComplaintID, string sModifiedBy)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.UpdateStatus(nComplaintID, sModifiedBy);
        }

        public DataTable SelectAll(ComplaintsBOL objCom)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.SelectAll(objCom);
        }

        public ComplaintsBOL SelectByID(int complaintId)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.SelectByID(complaintId);
        }

        public DataTable Search(string complaintBy, string complaintAgainst)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.Search(complaintBy, complaintAgainst);
        }
        public DataTable SelectEmployeeByComplaintID(int complaintId)
        {
            ComplaintsDAL objDAL = new ComplaintsDAL();
            return objDAL.SelectEmployeeByComplaintID(complaintId);
        }
    }
}
