using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class EmpTransferBAL
    {
        public int Save(EmpTransferBOL objEmp)
        {
            return new EmpTransferDAL().Save(objEmp); 
        }

        public int Delete(int nTransferID)
        {
            return new EmpTransferDAL().Delete(nTransferID); 
        }

        public DataTable SelectAll(EmpTransferBOL objBOL)
        {
            return new EmpTransferDAL().SelectAll(objBOL);
        }

        public int ApprovalRejectByCurrentBranch(EmpTransferBOL objBOL)
        {
            return new EmpTransferDAL().ApprovalRejectByCurrentBranch(objBOL);
        }

        public int ApprovalRejectByNewBranch(EmpTransferBOL objBOL)
        {
            return new EmpTransferDAL().ApprovalRejectByNewBranch(objBOL);
        }
    }
}
