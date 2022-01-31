using System;
using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class EmplStatusBAL
    {
        public int Save(string ID, string Description, string CreatedBy)
        {
            EmplStatusDAL objDAL = new EmplStatusDAL();
            return objDAL.Save(ID,Description,CreatedBy);
        }

        public int Delete(string ID, string CreatedBy)
        {
            EmplStatusDAL objDAL = new EmplStatusDAL();
            return objDAL.Delete(ID, CreatedBy);
        }

        public DataSet Select()
        {
            EmplStatusDAL objDAL = new EmplStatusDAL();
            return objDAL.Select();
        }
    }
}
