using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class ResignationBAL
    {
        public int Save(ResignationBOL objResgn)
        {
           ResignationDAL objDAL = new ResignationDAL ();
           return objDAL.Save(objResgn);
        }

        public int Approve(ResignationBOL objResgn)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.Approve(objResgn);
        }
        public int Update_Interview_Closed(ResignationBOL objResgn)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.Update_Interview_Closed(objResgn);
        }
        public int Delete(int nResgnID)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.Delete(nResgnID);
        }

        public DataTable SelectAll(ResignationBOL objResgn)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.SelectAll(objResgn);
        }
        public DataTable Resignation_SelectById(ResignationBOL objResgn)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.Resignation_SelectById(objResgn);
        }
        public DataTable SelectAll(int EmployeeId,int BranchId, string Status)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.SelectAll(EmployeeId,BranchId,Status);
        }
        public ResignationBOL SelectByID(int TID)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.SelectByID(TID);
        }
        public int cancel(int nResgnID, string status)
        {
              ResignationDAL objDAL = new ResignationDAL();
              return objDAL.cancel(nResgnID,status);

        }
        public DataTable SelectAPDresignation(ResignationBOL objBOL)
        {
            ResignationDAL objDAL = new ResignationDAL();
            return objDAL.SelectAPDresignation(objBOL);
        }
    }
}
