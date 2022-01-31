using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class OrgDesignationBAL
    {
        public int Save(OrgDesignationBOL objOrg)
        {
            OrgDesignationDAL objDAL = new OrgDesignationDAL();
            return objDAL.Save(objOrg);
        }

        public int Delete(int nDesignationID)
        {
            OrgDesignationDAL objDAL = new OrgDesignationDAL();
            return objDAL.Delete(nDesignationID);
        }

        public DataTable SelectAll(int nCompanyID)
        {
            OrgDesignationDAL objDAL = new OrgDesignationDAL();
            return objDAL.SelectAll(nCompanyID);
        }
        public DataTable Selectdesgn(int nCompanyID)
        {
            OrgDesignationDAL objDAL = new OrgDesignationDAL();
            return objDAL.Selectdesgn(nCompanyID);
        }
        public DataTable SearchByName(string Name)
        {
            OrgDesignationDAL objDAL = new OrgDesignationDAL();
            return objDAL.SearchByName(Name);
        }
    }
}
