using System;
using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class EduLevelBAL
    {
        public int Save(string ID, string EduLevelName, string CreatedBy)
        {
            EduLevelDAL objDAL = new EduLevelDAL();
            return objDAL.Save(ID,EduLevelName,CreatedBy);
        }

        public int Save(string ID, string EduLevelName, string sSortOrder, string CreatedBy)
        {
            EduLevelDAL objDAL = new EduLevelDAL();
            return objDAL.Save(ID, EduLevelName,sSortOrder, CreatedBy);
        }

        public int Delete(string ID, string CreatedBy)
        {
            EduLevelDAL objDAL = new EduLevelDAL();
            return objDAL.Delete(ID, CreatedBy);
        }

        public DataSet Select()
        {
            EduLevelDAL objDAL = new EduLevelDAL();
            return objDAL.Select();
        }
    }
}
