using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class TrainingTypeBAL
    {
        public int Save(TrainingTypeBOL objTrainingType)
        {
            TrainingTypeDAL objDAL = new TrainingTypeDAL();
            return objDAL.Save(objTrainingType);
        }

        public int Delete(int nTID)
        {
            TrainingTypeDAL objDAL = new TrainingTypeDAL();
            return objDAL.Delete(nTID);
        }

        public DataTable SelectAll(int nTID)
        {
            TrainingTypeDAL objDAL = new TrainingTypeDAL();
            return objDAL.SelectAll(nTID);
        }
    }
}
