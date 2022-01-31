using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class WorkPlanBAL
    {
        public int Save(WorkPlanBOL objWP)
        {
            WorkPlanDAL objDAL = new WorkPlanDAL();
            return objDAL.Save(objWP);
        }

        public int Delete(int ID)
        {
            WorkPlanDAL objDAL = new WorkPlanDAL();
            return objDAL.Delete(ID);
        }

        public DataTable SelectAll(WorkPlanBOL objWP)
        {
            WorkPlanDAL objDAL = new WorkPlanDAL();
            return objDAL.SelectAll(objWP);
        }
       

         public int SaveMaster(WorkPlanBOL objWP)
        {
            WorkPlanDAL objDAL = new WorkPlanDAL();
            return objDAL.SaveMaster(objWP);
        }

         public DataTable SelectWorkPlanMaster(WorkPlanBOL objWP)
         {
             WorkPlanDAL objDAL = new WorkPlanDAL();
             return objDAL.SelectWorkPlanMaster(objWP);
         }

         public int DeleteMaster(int nWPMID)
         {
             WorkPlanDAL objDAL = new WorkPlanDAL();
             return objDAL.DeleteMaster(nWPMID);
         }
         public DataTable SelectWorkSchedule(WorkPlanBOL objWP)
         {
             return new WorkPlanDAL().SelectWorkSchedule(objWP);
         }
    }
}
