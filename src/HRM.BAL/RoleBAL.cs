using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class RoleBAL
    {
        public int Save(RoleBOL objRole)
        {
            RoleDAL objDAL = new RoleDAL();
            return objDAL.Save(objRole);
        }

        public int Delete(int nRoleID)
        {
            RoleDAL objDAL = new RoleDAL();
            return objDAL.Delete(nRoleID);
        }

        public DataTable SelectAll(int nRoleID)
        {
            RoleDAL objDAL = new RoleDAL();
            return objDAL.SelectAll(nRoleID);
        }
    }
}
