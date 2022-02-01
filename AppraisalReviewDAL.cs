using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AppraisalReviewDAL
    {
        public int SaveMaster(AppraisalReviewBOL objBL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@ReviewID", 0));
                objParam.Add(new SqlParameter("@EAID", objBL.EAID));
                objParam.Add(new SqlParameter("@ReviewerID", objBL.ReviewerID));
                objParam.Add(new SqlParameter("@Comments", objBL.Comments));
                objParam.Add(new SqlParameter("@CreatedBy", objBL.CreatedBy));
                objParam.Add(new SqlParameter("@Status", "Y"));

                objDA.sqlCmdText = "hrm_Employee_ReviewMaster_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int SaveDetails(AppraisalReviewBOL objBL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@ReviewID", objBL.ReviewID));
                objParam.Add(new SqlParameter("@CompetencyID", objBL.CompetencyID));
                objParam.Add(new SqlParameter("@RatingID", objBL.RatingID));
                objParam.Add(new SqlParameter("@Comments", objBL.Comments));

                objDA.sqlCmdText = "hrm_Employee_ReviewDetails_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SubmitReview(AppraisalReviewBOL objBL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@ReviewID", objBL.ReviewID));
                objParam.Add(new SqlParameter("@Comments", objBL.Comments));
                objParam.Add(new SqlParameter("@CreatedBy", objBL.CreatedBy));
                objParam.Add(new SqlParameter("@Submitted", "Y"));

                objDA.sqlCmdText = "hrm_Employee_ReviewMaster_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectAppraisalsForReview(int nReviewerID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@ReviewerID", nReviewerID));

                objDA.sqlCmdText = "hrm_Employee_SelectAppraisalsForReview";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet Select(int nReviewID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@ReviewID", nReviewID));

                objDA.sqlCmdText = "hrm_Employee_Review_Select";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable SelectReviews(int EmployeeID, int AppraisalPeriodID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));
                objParam.Add(new SqlParameter("@AppPeriodID", AppraisalPeriodID));

                objDA.sqlCmdText = "hrm_Employee_SelectReview";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet SelectReviewsByCompetencyID(int EAID, int CompetencyID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {

                objParam[0] = new SqlParameter("@EAID", EAID);
                objParam[1] = new SqlParameter("@CompetencyID", CompetencyID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Employee_Review_Select_CompetencyID";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetLastAppraisalScore(int EmployeeID, int AppPeriodID, out string AppPeriod, out string AppScore)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                AppPeriod = "";
                AppScore = "";

                objParam[0] = new SqlParameter("@EmployeeID", EmployeeID);
                if (AppPeriodID > 0)
                    objParam[1] = new SqlParameter("@AppPeriodID", AppPeriodID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Last_Apprisal_Score";
                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    AppPeriod = "" + dReader["LASTAPPDATE"];
                    AppScore = "" + dReader["LASTAPPSCORE"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
