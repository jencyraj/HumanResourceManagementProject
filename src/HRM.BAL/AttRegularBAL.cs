using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AttRegularBAL
    {
        public int Save(AttRegularBOL objBOL)
        {
            return new AttRegularDAL().Save(objBOL);
        }

        public int Approve(int ReqID, string Approved_Rejected_By, string ApprovalStatus, string RejectReason)
        {
            return new AttRegularDAL().Approve(ReqID,Approved_Rejected_By,ApprovalStatus,RejectReason);
        }

        public int Delete(int ReqID, string DeletedBy)
        {
            return new AttRegularDAL().Delete(ReqID,DeletedBy);
        }

        public DataTable Select(int EmployeeID)
        {
            return new AttRegularDAL().Select(EmployeeID);
        }
            public DataTable GetPayable_Overtime()
        {
            return new AttRegularDAL().GetPayable_Overtime();
        }
        public int Detail_Save(AttRegularBOL objBOL)
        {
            try
            {
                return new AttRegularDAL().Detail_Save(objBOL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
}
        public int ReqDetail_Save(AttRegularBOL objWP)
        {
            return new AttRegularDAL().ReqDetail_Save(objWP);
        }
        public int Approve(int reqID, int attndncID, int empID, DateTime Date, int signin, int signout, string status, DataTable DT)
        {
            return new AttRegularDAL().Approve(reqID,attndncID,empID,Date,signin,signout,status,DT);
        }
        public DataTable showAttendance(int EmployeeID,int mon,int year)
        {
            return new AttRegularDAL().ShowAttendance(EmployeeID,mon,year);
        }
        public DataSet TimeSheet_CheckAttendance(int EmployeeID, int mon, int year)
        {
            return new AttRegularDAL().TimeSheet_CheckAttendance(EmployeeID, mon, year);
        }
        public DataTable SelectBy(int ID)
        {
            return new AttRegularDAL().SelectBy(ID);
        }
        public AttRegularBOL SelectByID(int ReqID)
        {
            return new AttRegularDAL().SelectByID(ReqID);
        }

        public void ClearRequestDetail(int ReqID)
        {
            new AttRegularDAL().ClearRequestDetail(ReqID);
        }

        public void ApproveRequest(AttRegularBOL objBOL)
        {
            new AttRegularDAL().ApproveRequest(objBOL);
        }

        public void CloseRequest(int ReqID)
        {
            new AttRegularDAL().CloseRequest(ReqID);
        }
    }
}
