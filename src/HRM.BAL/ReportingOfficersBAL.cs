using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class ReportingOfficersBAL
    {
        public int Save(EmployeeBOL objEmp)
        {
            ReportingOfficersDAL objDAL = new ReportingOfficersDAL();
            return objDAL.Save(objEmp);
        }

        public int Delete(EmployeeBOL objEmp)
        {
            ReportingOfficersDAL objDAL = new ReportingOfficersDAL();
            return objDAL.Delete(objEmp);
        }

        public DataTable SelectSuperiors(int nEmployeeID)
        {
            ReportingOfficersDAL objDAL = new ReportingOfficersDAL();
            return objDAL.SelectSuperiors(nEmployeeID);
        }

        public DataTable SelectSubordinates(int nSuperiorID)
        {
            ReportingOfficersDAL objDAL = new ReportingOfficersDAL();
            return objDAL.SelectSubordinates(nSuperiorID);
        }
    }
}
