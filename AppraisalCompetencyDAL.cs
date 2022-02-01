using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AppraisalCompetencyDAL
    {
        #region COMPETENCY

        public int Save(AppraisalCompetencyBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@CompetencyID", objCy.CompetencyID));
                //objParam[++i] = new SqlParameter("@AppPeriodID", objCy.AppraisalPeriodID);
                objParam.Add(new SqlParameter("@AppPeriodID", objCy.AppraisalPeriod));
                objParam.Add(new SqlParameter("@RoleID", objCy.RoleID));
                objParam.Add(new SqlParameter("@CompetencyTypeID", objCy.CompetencyTypeID));
                objParam.Add(new SqlParameter("@CompetencyName", objCy.CompetencyName));
                objParam.Add(new SqlParameter("@RatingDesc1", objCy.RatingDesc1));
                objParam.Add(new SqlParameter("@RatingDesc2", objCy.RatingDesc2));
                objParam.Add(new SqlParameter("@RatingDesc3", objCy.RatingDesc3));
                objParam.Add(new SqlParameter("@RatingDesc4", objCy.RatingDesc4));
                objParam.Add(new SqlParameter("@RatingDesc5", objCy.RatingDesc5));
                objParam.Add(new SqlParameter("@Weightage", objCy.Weightage));
                objParam.Add(new SqlParameter("@CreatedBy", objCy.CreatedBy));
                objParam.Add(new SqlParameter("@Status", "Y"));

                objDA.sqlCmdText = "hrm_AppraisalCompetency_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nCompetencyID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@CompetencyID", nCompetencyID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_AppraisalCompetency_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(AppraisalCompetencyBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[4];
            int i = -1;

            try
            {
                if (objCy.AppraisalPeriodID > 0)
                    objParam[++i] = new SqlParameter("@AppPeriodID", objCy.AppraisalPeriodID);
                if (objCy.CompetencyTypeID > 0)
                    objParam[++i] = new SqlParameter("@CompetencyTypeID", objCy.CompetencyTypeID);
                if (objCy.RoleID > 0)
                    objParam[++i] = new SqlParameter("@RoleID", objCy.RoleID);
                if (objCy.CompetencyID > 0)
                    objParam[++i] = new SqlParameter("@CompetencyID", objCy.CompetencyID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_AppraisalCompetency_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public AppraisalCompetencyBOL SelectByID(int ComID)
        {
            AppraisalCompetencyBOL objCy = null;
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CompetencyID", ComID);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_AppraisalCompetency_SELECT";

                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objCy = new AppraisalCompetencyBOL();
                    //objCy.AppraisalPeriodID = Util.ToInt(dReader["AppPeriodID"]);
                    objCy.AppraisalPeriod = "" + dReader["AppPeriodID"];
                    objCy.RoleID = Util.ToInt(dReader["RoleID"]);
                    objCy.CompetencyTypeID = Util.ToInt(dReader["CompetencyTypeID"]);
                    objCy.CompetencyID = Util.ToInt(dReader["CompetencyID"]);
                    objCy.CompetencyName = "" + dReader["CompetencyName"];
                    objCy.Weightage = Util.ToInt("" + dReader["Weightage"]);
                    objCy.RatingDesc1 = "" + dReader["RatingDesc1"];
                    objCy.RatingDesc2 = "" + dReader["RatingDesc2"];
                    objCy.RatingDesc3 = "" + dReader["RatingDesc3"];
                    objCy.RatingDesc4 = "" + dReader["RatingDesc4"];
                    objCy.RatingDesc5 = "" + dReader["RatingDesc5"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCy;
        }
        #endregion

        #region COMPETENCY TYPE

        public int SaveCompetencyType(AppraisalCompetencyBOL objCy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[7];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@CompetencyTypeID", objCy.CompetencyTypeID);
                objParam[++i] = new SqlParameter("@CompetencyType", objCy.CompetencyType);
                objParam[++i] = new SqlParameter("@CreatedBy", objCy.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_CompetencyType_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteCompetencyType(int CompetencyTypeID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@CompetencyTypeID", CompetencyTypeID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_CompetencyType_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAllCompetencyTypes(int CompetencyTypeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (CompetencyTypeID > 0)
                {
                    objParam[0] = new SqlParameter("@CompetencyTypeID", CompetencyTypeID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_CompetencyType_SELECT";
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
