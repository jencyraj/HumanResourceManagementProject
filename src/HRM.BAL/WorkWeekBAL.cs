using System;
using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class WorkWeekBAL
    {
        public int Save(string[] sDays, string CreatedBy)
        {
            WorkWeekDAL objDAL = new WorkWeekDAL();
            return objDAL.Save(sDays, CreatedBy);
        }
        public int Delete(int Eid)
        {
            WorkWeekDAL objDAL = new WorkWeekDAL();
            return objDAL.Delete(Eid);
        }
        public DataSet Select()
        {
            WorkWeekDAL objDAL = new WorkWeekDAL();
            return objDAL.Select();
        }
        public DataTable Workweek_edit(int Eid)
        {
            WorkWeekDAL objDAL = new WorkWeekDAL();
            return objDAL.Workweek_edit(Eid);
        }
        public DataTable Workweek_select()
        {
            WorkWeekDAL objDAL = new WorkWeekDAL();
            return objDAL.Workweek_select();
        }
    }
}
