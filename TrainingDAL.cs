using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class TrainingDAL
    {
        #region Training

        public int Save(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[17];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@TrainingID", objCy.TrainingID);
                objParam[++i] = new SqlParameter("@TrainingLID", objCy.TrainingLID);
                objParam[++i] = new SqlParameter("@Emp", objCy.Employee);
                objParam[++i] = new SqlParameter("@trainingtype", objCy.trainingtype);
                objParam[++i] = new SqlParameter("@subject", objCy.subject);
                objParam[++i] = new SqlParameter("@nature", objCy.nature);
                objParam[++i] = new SqlParameter("@title", objCy.title);
                objParam[++i] = new SqlParameter("@trainer", objCy.trainer);
                objParam[++i] = new SqlParameter("@location", objCy.location);
                objParam[++i] = new SqlParameter("@sponseredby", objCy.sponseredby);
                objParam[++i] = new SqlParameter("@organizedby", objCy.organizedby);
                objParam[++i] = new SqlParameter("@fromdt", objCy.fromdt);
                objParam[++i] = new SqlParameter("@todt", objCy.todt);
                objParam[++i] = new SqlParameter("@Description", objCy.Description);
                objParam[++i] = new SqlParameter("@note", objCy.note);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@CreatedBy", objCy.CreatedBy);
              
		
                objDA.sqlCmdText = "hrm_Training_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int savelocation(TrainingBOL objbolt)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@TrainingLID", objbolt.TrainingLID);
                objParam[++i] = new SqlParameter("@TrainingLocationName", objbolt.TrainingLocationName);
                objParam[++i] = new SqlParameter("@Status", objbolt.Status);



                objDA.sqlCmdText = "hrm_Training_Location_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nTrainingID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@TrainingID", nTrainingID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Training_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;

            try
            {
                if (objCy.trainingtype > 0)
                    objParam[++i] = new SqlParameter("@TID", objCy.trainingtype);
                if (""+objCy.Description !="")
                    objParam[++i] = new SqlParameter("@description", objCy.title);
               
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectTrainingSchedule(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;

            try
            {
                if (objCy.TrainingLID > 0)
                    objParam[++i] = new SqlParameter("@TrainingLID", objCy.TrainingLID);
              

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public TrainingBOL SelectByID(int TID)
        {
            TrainingBOL objCy = null;
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@TrId", TID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_SELECT";

                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objCy = new TrainingBOL();
                    objCy.trainingtype = Util.ToInt(dReader["Tid"]);
                    objCy.TrainingLID = Util.ToInt(dReader["TrainingLID"]);
                    objCy.Employee = "" + dReader["employees"];
                    objCy.subject = "" + dReader["subject"];
                    objCy.nature = "" + dReader["nature"];
                    objCy.title = "" + dReader["title"];
                    objCy.trainer = "" + dReader["trainer"];
                    objCy.location = "" + dReader["location"];
                    objCy.sponseredby = "" + dReader["sponseredby"];
                    objCy.organizedby = "" + dReader["organizedby"];
                    objCy.fromdt = "" + dReader["fromdt"];
                    objCy.todt = "" + dReader["todt"];
                    objCy.Description = "" + dReader["Description"];
                    objCy.note = "" + dReader["note"];


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCy;
        }
        public DataTable selecttraining_Loc(int ID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (ID > 0)
                {
                    objParam[0] = new SqlParameter("@TrainingLID", ID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_TrainingLoc_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Deletelocation (int ID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (ID > 0)
                {
                    objParam[0] = new SqlParameter("@TrainingLID", ID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_TrainingLoc_Delete";
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable selecttraining_Location()
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
               
                objDA.sqlCmdText = "hrm_TrainingLoc_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectAllApprovalList(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {

                if ("" + objCy.ApprovalStatus != "")
                    objParam[++i] = new SqlParameter("@approvalstatus", objCy.ApprovalStatus);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_Select_Approval";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectAllEmployeeTrainingEventList(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {

                if ("" + objCy.Employee != "")
                    objParam[++i] = new SqlParameter("@employeeid", objCy.Employee);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_Select_ByEmployee";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectByApprovalStatus(TrainingBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {

                if ("" + objCy.ApprovalStatus != "")
                    objParam[++i] = new SqlParameter("@approvalstatus", objCy.ApprovalStatus);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Training_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int UpdateStatus(int nTrainingID, string CreatedBy, string Approvalstatus)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            try
            {
                objParam[0] = new SqlParameter("@TrainingID", nTrainingID);
                objParam[1] = new SqlParameter("@ApprovedBy", CreatedBy);
                objParam[2] = new SqlParameter("@ApprovedStatus", Approvalstatus);
                objDA.sqlCmdText = "hrm_Training_Status";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       

    }
    public class TrainingEvaluationDAL
    {
        #region TrainingEvaluation
        public int InsertTrainingEvaluation(TrainingEvaluationBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];

            try
            {
                objParam[0] = new SqlParameter("@Evaluateid", objCy.Evaluateid);
                objParam[1] = new SqlParameter("@TrainingID", objCy.TrainingId);
                objParam[2] = new SqlParameter("@Description", objCy.Description);
                objParam[3] = new SqlParameter("@rating", objCy.rating);
                objParam[4] = new SqlParameter("@employeeid", objCy.employeeid);
                objDA.sqlCmdText = "hrm_TrainingEvaluation_Insert_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TrainingEvaluationBOL SelectTrainingEvaluationByID(int evaluateId, int employeeid)
        {
            TrainingEvaluationBOL objCy = null;
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@TrainingId", evaluateId);
                objParam[1] = new SqlParameter("@employeeid", employeeid);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_TrainingEvaluate_SELECT";

                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objCy = new TrainingEvaluationBOL();
                    objCy.Evaluateid = Util.ToInt(dReader["Evaluateid"]);
                    objCy.TrainingId = Util.ToInt("" + dReader["TrainingId"]);
                    objCy.Description = "" + dReader["Description"];
                    objCy.rating = Util.ToInt("" + dReader["rating"]);
                   

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCy;
        }


        public DataTable SelectAllEmployeeTrainingEvaluationList(TrainingEvaluationBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[0];

            try
            {

                if ("" + objCy.TrainingId != "")
                //    objParam[++i] = new SqlParameter("@TrainingId", objCy.TrainingId);
                //objParam[++i] = new SqlParameter("@employeeid", objCy.employeeid);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_TrainingEvaluate_SELECTALL";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Delete(string CreatedBy,int nevaluationID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@evaluationID", nevaluationID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_TrainingEvaluation_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
