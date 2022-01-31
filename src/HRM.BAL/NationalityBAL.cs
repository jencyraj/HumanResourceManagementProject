using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HRM.DAL;

namespace HRM.BAL
{
   public class NationalityBAL
    {
        public DataTable Select(string sLang)
        {
            NationalityDAL objDAL = new NationalityDAL();
            return objDAL.Select(sLang);
        }
        public int UPDATE(string NationalCode, string NationalName, string LangCultureName)
        {
            return new NationalityDAL().UPDATE(NationalCode, NationalName, LangCultureName);
        }
    }
}
