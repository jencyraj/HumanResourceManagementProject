using System;
using System.Data;
using System.Data.SqlClient;

using HRM.DAL;

namespace HRM.BAL
{
    public class MonthsBAL
    {
        public DataTable Select(string sLang)
        {
            return new MonthsDAL().Select(sLang);
        }
    }
}
