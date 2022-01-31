using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class LangDataBAL
    {
        public int Save(LangDataBOL objLangData)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.Save(objLangData);
        }

        public int Delete(int nLID)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.Delete(nLID);
        }

        public DataTable Select(LangDataBOL objLangData)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.Select(objLangData);
        }

        public LangDataBOL SearchById(int nLID)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.SearchById(nLID);
        }

        public DataTable SelectLanguage(int nLanguageId)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.SelectLanguage(nLanguageId);
        }

        public int SaveLanguage(int nLID, string sActive, string sDefault,string sStyle)
        {
            LangDataDAL objDAL = new LangDataDAL();
            return objDAL.SaveLanguage(nLID, sActive, sDefault, sStyle);
        }
    }
}
