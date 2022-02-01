using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AppraisalTemplateDAL
    {
        #region APPRAISAL TEMPLATE

        public int Save(AppraisalCompetencyBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@CompetencyID", objCy.CompetencyID));
                objParam.Add(new SqlParameter("@AppPeriodID", objCy.AppraisalPeriodID));
                objParam.Add(new SqlParameter("@CompetencyTypeID", objCy.CompetencyTypeID));
                objParam.Add(new SqlParameter("@CompetencyName", objCy.CompetencyName));
                objParam.Add(new SqlParameter("@RatingDesc1", objCy.RatingDesc1));
                objParam.Add(new SqlParameter("@RatingDesc2", objCy.RatingDesc2));
                objParam.Add(new SqlParameter("@RatingDesc3", objCy.RatingDesc3));
                objParam.Add(new SqlParameter("@RatingDesc4", objCy.RatingDesc4));
                objParam.Add(new SqlParameter("@RatingDesc5", objCy.RatingDesc5));
                objParam.Add(new SqlParameter("@Weightage", objCy.Weightage));
                objParam.Add(new SqlParameter("@CreatedBy", objCy.CreatedBy));

                objDA.sqlCmdText = "hrm_Appraisal_Template_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAppraisalTemplate(int AppPeriodID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@AppPeriodID", AppPeriodID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_AppraisalCompetency_SelectBy_AppraisalPeriod";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
