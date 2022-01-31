using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class AppraisalReviewBAL
    {
        public int SaveMaster(AppraisalReviewBOL objBL)
        {
            return new AppraisalReviewDAL().SaveMaster(objBL);
        }

        public int SaveDetails(AppraisalReviewBOL objBL)
        {
            return new AppraisalReviewDAL().SaveDetails(objBL);
        }


        public void SubmitReview(AppraisalReviewBOL objBL)
        {
            new AppraisalReviewDAL().SubmitReview(objBL);
        }

        public DataTable SelectAppraisalsForReview(int nReviewerID)
        {
            return new AppraisalReviewDAL().SelectAppraisalsForReview(nReviewerID);
        }


        public DataSet Select(int nReviewID)
        {
            return new AppraisalReviewDAL().Select(nReviewID);
        }

        public DataTable SelectReviews(int EmployeeID, int AppraisalPeriodID)
        {
            return new AppraisalReviewDAL().SelectReviews(EmployeeID, AppraisalPeriodID);
        }

        public DataSet SelectReviewsByCompetencyID(int EAID, int CompetencyID)
        {
            return new AppraisalReviewDAL().SelectReviewsByCompetencyID(EAID, CompetencyID);
        }

        public void GetLastAppraisalScore(int EmployeeID, int AppraisalPeriodID, out string AppPeriod, out string AppScore)
        {
            new AppraisalReviewDAL().GetLastAppraisalScore(EmployeeID, AppraisalPeriodID, out AppPeriod, out AppScore);
        }
    }
}
