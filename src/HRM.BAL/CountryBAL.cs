using System;
using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class CountryBAL
    {
        public DataTable Select(string sLang)
        {
            CountryDAL objDAL = new CountryDAL();
            return objDAL.Select(sLang);
        }
        public int UPDATE(string CountryCode, string CountryName,string LangCultureName)
        {
            return new CountryDAL().UPDATE(CountryCode, CountryName,LangCultureName);
        }

    }
}
