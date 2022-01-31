using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class PermissionBAL
    {
        public int Save(PermissionBOL objBAL)
        {
            PermissionDAL objDAL = new PermissionDAL();
            return objDAL.Save(objBAL);
        }

        public DataSet SelectAll(int nRoleID,int EmployeeID, string Langculturename)
        {
            PermissionDAL objDAL = new PermissionDAL();
            return objDAL.SelectAll(nRoleID,EmployeeID, Langculturename);
        }

        public DataSet GetPermissions(int nRoleID, int EmployeeID, string Langculturename)
        {
            return new PermissionDAL().GetPermissions(nRoleID, EmployeeID, Langculturename);
        }
    }
}
