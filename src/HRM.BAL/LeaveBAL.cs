using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class LeaveBAL
    {
        public int Save(LeaveBOL objLeave)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.Save(objLeave);
        }

        public int SaveLeaveDates(LeaveBOL objLeave)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.SaveLeaveDates(objLeave);
        }

        public int ApproveLeave(LeaveBOL objLeave)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.ApproveLeave(objLeave);
        }

        public int Delete(int nLeaveID, string sCreatedBy)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.Delete(nLeaveID, sCreatedBy);
        }

        public int DeleteLeaveDetails(int nLeaveDetailID)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.DeleteLeaveDetails(nLeaveDetailID);
        }

        public DataTable SelectAll(LeaveBOL objLeave)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.SelectAll(objLeave);
        }

        public DataSet SelectByID(int nLeaveID)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.SelectByID(nLeaveID);
        }

        public DataTable GetLeaveBalance(int EmpID)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.GetLeaveBalance(EmpID);
        }

        public decimal CheckAvailability(int EmpID, int LeaveTypeID,int LeaveYear)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.CheckAvailability(EmpID, LeaveTypeID,LeaveYear);
        }

        public int UpdateLeaveBalance(LeaveBOL objBOL, int Action)
        {
            LeaveDAL objDAL = new LeaveDAL();
            return objDAL.UpdateLeaveBalance(objBOL, Action);
        }

        public int SetLeaveBalance_NewYear(int EmployeeID)
        {
            return new LeaveDAL().SetLeaveBalance_NewYear(EmployeeID);
        }

        public int SaveLeaveBalance(LeaveBOL objBOL)
        {
            return new LeaveDAL().SaveLeaveBalance(objBOL);
        }
        public DataSet Getlatebytype(LeaveBOL objlate)
        {
            LeaveDAL objLT = new LeaveDAL();
            return objLT.Getlatebyvalue(objlate);
        }
        public DataTable checklatebytime()
        {
            LeaveDAL objLT = new LeaveDAL();
            return objLT.checklatebytime();
        }
        public DataTable SalaryslipGenerate(LeaveBOL objlate)
        {
            LeaveDAL objLT = new LeaveDAL();
            return objLT.SalaryslipGenerate(objlate);
        }
        public DataTable Getallowances(LeaveBOL objlate)
        {
            LeaveDAL objLT = new LeaveDAL();
            return objLT.Getallowances(objlate);
        }

        public int SaveWithLeaveRule(LeaveBOL objLeave)
        {
            return new LeaveDAL().SaveWithLeaveRule(objLeave);
        }

        public DataTable AppliedLeaveRule(int EmployeeID, int nMonth, int nYear)
        {
            return new LeaveDAL().AppliedLeaveRule(EmployeeID, nMonth, nYear);
        }
        public decimal LopLeaveTaken(int EmployeeID, int nYear, int nMonth, decimal minimumleaves)
        {
            return new LeaveDAL().LopLeaveTaken(EmployeeID, nYear, nMonth, minimumleaves);
        }
        
        public int offdayselect(int EmployeeID, string latedate)
        {
            return new LeaveDAL().offdayselect(EmployeeID, latedate);
        }


    }
}
