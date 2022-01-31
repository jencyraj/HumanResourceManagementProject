using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class AppraisalPeriodBAL
    {
        public int Save(AppraisalPeriodBOL objAppPeriod)
        {
            AppraisalPeriodDAL objDAL = new AppraisalPeriodDAL();
            return objDAL.Save(objAppPeriod);
        }

        public int Delete(int nAppPeriodID, string CreatedBy)
        {
            AppraisalPeriodDAL objDAL = new AppraisalPeriodDAL();
            return objDAL.Delete(nAppPeriodID,CreatedBy);
        }

        public DataSet SelectAll(int nAppPeriodID)
        {
            AppraisalPeriodDAL objDAL = new AppraisalPeriodDAL();
            return objDAL.SelectAll(nAppPeriodID);
        }

        public int SaveBranch(int AppPeriodID, string sBranch, string CreatedBy)
        {
            return new AppraisalPeriodDAL().SaveBranch(AppPeriodID, sBranch, CreatedBy);
        }

        public int SaveEmployees(int AppPeriodID, string sEmployee, string CreatedBy)
        {
            return new AppraisalPeriodDAL().SaveEmployees(AppPeriodID, sEmployee, CreatedBy);
        }

        public DataSet GetAppraisalPeriods(int EmpID)
        {
            return new AppraisalPeriodDAL().GetAppraisalPeriods(EmpID);
        }
    }
}
