using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class HourlyWageBAL
    {
        public int Save(HourlyWageBOL objHourlyWage)
        {
            HourlyWageDAL objDAL = new HourlyWageDAL();
            return objDAL.Save(objHourlyWage);
        }

        public int Delete(int nHourlyWageId)
        {
            HourlyWageDAL objDAL = new HourlyWageDAL();
            return objDAL.Delete(nHourlyWageId);
        }

        public DataTable SelectAll(HourlyWageBOL objHourlyWage)
        {
            HourlyWageDAL objDAL = new HourlyWageDAL();
            return objDAL.SelectAll(objHourlyWage);
        }

        public HourlyWageBOL SearchById(int nHourlyWageId)
        {
            HourlyWageDAL objDAL = new HourlyWageDAL();
            return objDAL.SearchById(nHourlyWageId);
        }

        public int ActiveHourWagePresent(int nHourlyWageId)
        {
            HourlyWageDAL objDAL = new HourlyWageDAL();
            return objDAL.ActiveHourWagePresent(nHourlyWageId);
        }
    }
}
