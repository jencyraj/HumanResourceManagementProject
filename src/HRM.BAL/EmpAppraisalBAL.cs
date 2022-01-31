using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class EmpAppraisalBAL
    {
        public int SaveMaster(EmpAppraisalBOL objAppPeriod)
        {
            return new EmpAppraisalDAL().SaveMaster(objAppPeriod);
        }

        public int SaveDetails(EmpAppraisalBOL objAppPeriod)
        {
            return new EmpAppraisalDAL().SaveDetails(objAppPeriod);
        }

        public DataSet SelectAll(int nAppPeriodID, int EmployeeID)
        {
            return new EmpAppraisalDAL().SelectAll(nAppPeriodID, EmployeeID);
        }

        public int SubmitAppraisal(int EAID, int EmployeeID)
        {
            return new EmpAppraisalDAL().SubmitAppraisal(EAID, EmployeeID);
        }
        public DataTable SelectLastAppraisal(int Employeeid)
        {
            return new EmpAppraisalDAL().SelectLastAppraisal(Employeeid);
        }
    }
}
