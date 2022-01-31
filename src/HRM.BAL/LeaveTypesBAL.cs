using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

    namespace HRM.BAL
{
     public class LeaveTypesBAL
    {
         public int Save(LeaveTypesBOL objLType)
        {
           LeaveTypesDAL objLT=new LeaveTypesDAL();
             return objLT.Save(objLType);
        }

        public int Delete(int nLeaveTypeID, string sCreatedBy)
        {
            LeaveTypesDAL objLT=new LeaveTypesDAL();
             return objLT.Delete(nLeaveTypeID,sCreatedBy);
        }

        public DataTable SelectAll()
        {
            LeaveTypesDAL objLT = new LeaveTypesDAL();
            return objLT.SelectAll();
        }

        public LeaveTypesBOL SelectByID(int nLeaveTypeID)
        {
            LeaveTypesDAL objLT = new LeaveTypesDAL();
            return objLT.SelectByID(nLeaveTypeID);
        }
    }
}
