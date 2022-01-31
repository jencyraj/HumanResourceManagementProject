using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class OrgDepartmentsBAL
    {
        public int Save(OrgDepartmentBOL objOrg)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.Save(objOrg);
        }

        public int Delete(int nDeptID)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.Delete(nDeptID);
        }

        public DataTable SelectAll(int nCompanyID)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.SelectAll(nCompanyID);
        }

        public DataTable SelectAll(OrgDepartmentBOL objBOL)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.SelectAll(objBOL);
        }

        public DataTable SelectParentDepartments(int nDepartmentID)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.SelectParentDepartments(nDepartmentID);
        }

        public DataTable SelectDepartmentsByBranchID(int BranchID)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.SelectDepartmentsByBranchID(BranchID);
        }

        public DataTable SelectBranchesByDepartmentID(int DepartmentID)
        {
            OrgDepartmentsDAL objDAL = new OrgDepartmentsDAL();
            return objDAL.SelectBranchesByDepartmentID(DepartmentID);
        }
    }
}
