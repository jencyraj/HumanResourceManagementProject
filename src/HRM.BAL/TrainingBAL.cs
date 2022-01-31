using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class TrainingBAL
    {
        public int Save(TrainingBOL objCy)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.Save(objCy);
        }

        public int Delete(int nCompetencyID, string CreatedBy)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.Delete(nCompetencyID,CreatedBy);
        }

        public DataTable SelectAll(TrainingBOL objCy)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.SelectAll(objCy);
        }

        public TrainingBOL SelectByID(int ComID)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.SelectByID(ComID);
        }

        public int UpdateStatus(int nTrainingID, string CreatedBy, string ApprovalStatus)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.UpdateStatus(nTrainingID, CreatedBy, ApprovalStatus);
        }
        public DataTable SelectAllTrainingApprovalList(TrainingBOL objCy)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.SelectAllApprovalList(objCy);
        }
        public DataTable SelectAllEmployeeTrainingEventList(TrainingBOL objCy)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.SelectAllEmployeeTrainingEventList(objCy);
        }
        public int savelocation(TrainingBOL objbolt)
        {
             TrainingDAL objDAL = new TrainingDAL();
             return objDAL.savelocation(objbolt); 
        }
        public DataTable selecttraining_Loc(int ID)
        {
             TrainingDAL objDAL = new TrainingDAL();
             return objDAL.selecttraining_Loc(ID);
        }
        public DataTable selecttraining_Location()
        {
             TrainingDAL objDAL = new TrainingDAL();
             return objDAL.selecttraining_Location();
        }
        public int  Deletelocation(int ID)
        {
            TrainingDAL objDAL = new TrainingDAL();
            return objDAL.Deletelocation(ID);
        }
        public DataTable SelectTrainingSchedule(TrainingBOL objCy)
        { TrainingDAL objDAL = new TrainingDAL();
        return objDAL.SelectTrainingSchedule(objCy);

        }
    }
    public class TrainingEvaluationBAL
    {
        public int InsertTrainingEvaluation(TrainingEvaluationBOL objCy)
        {
            TrainingEvaluationDAL objDAL = new TrainingEvaluationDAL();
            return objDAL.InsertTrainingEvaluation(objCy);
        }
        public TrainingEvaluationBOL SelectTrainingEvaluationByID(int evaluateId, int Employee)
        {
            TrainingEvaluationDAL objDAL = new TrainingEvaluationDAL();
            return objDAL.SelectTrainingEvaluationByID(evaluateId, Employee);
        }
        public DataTable SelectAllEmployeeTrainingEvaluationList(TrainingEvaluationBOL objCy)
        {
            TrainingEvaluationDAL objDAL = new TrainingEvaluationDAL();
            return objDAL.SelectAllEmployeeTrainingEvaluationList(objCy);
        }
        public int Delete(string CreatedBy,int evaluationid)
        {
            TrainingEvaluationDAL objDAL = new TrainingEvaluationDAL();
            return objDAL.Delete(CreatedBy, evaluationid);
        }
    }
   
}
