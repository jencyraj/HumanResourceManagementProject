using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class AlertsBAL
    {
        public int Save(int EmployeeID, string sMsg, string sType, string LangCulture)
        {
            return new AlertsDAL().Save(EmployeeID, sMsg, sType, LangCulture);
        }

        public DataTable Select(int EmployeeID)
        {
            return new AlertsDAL().Select(EmployeeID);
        }

        public int Delete(int AlertID)
        {
            return new AlertsDAL().Delete(AlertID);
        }
    }
}
