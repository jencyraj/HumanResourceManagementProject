using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class OrgBranchesBAL
    {
        public int Save(OrgBranchesBOL objOrg)
        {
            OrgBranchesDAL objDAL = new OrgBranchesDAL();
            return objDAL.Save(objOrg);
        }

        public int Delete(int nBranchID)
        {
            OrgBranchesDAL objDAL = new OrgBranchesDAL();
            return objDAL.Delete(nBranchID);
        }

        public DataTable SelectAll(int nCompanyID)
        {
            OrgBranchesDAL objDAL = new OrgBranchesDAL();
            return objDAL.SelectAll(nCompanyID);
        }
    }
}
