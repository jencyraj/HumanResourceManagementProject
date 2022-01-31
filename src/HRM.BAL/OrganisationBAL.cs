using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class OrganisationBAL
    {
        public int Save(OrganisationBOL objOrg)
        {
            OrganisationDAL objDAL = new OrganisationDAL();
            return objDAL.Save(objOrg);
        }

        public int Delete(int nCompanyID)
        {
            OrganisationDAL objDAL = new OrganisationDAL();
            return objDAL.Delete(nCompanyID);
        }

        public DataTable Search(OrganisationBOL objOrg)
        {
            OrganisationDAL objDAL = new OrganisationDAL();
            return objDAL.Search(objOrg);
        }

        public OrganisationBOL Select()
        {
            OrganisationDAL objDAL = new OrganisationDAL();
            return objDAL.Select();
        }
    }
}
