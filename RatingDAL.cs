using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class RatingDAL
    {
        public int Save(RatingBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@RatingID", objOrg.RatingID);
                objParam[++i] = new SqlParameter("@RatingDesc", objOrg.RatingDesc);
                objParam[++i] = new SqlParameter("@MaxScore", objOrg.MaxScore);
                objParam[++i] = new SqlParameter("@CreatedBy", objOrg.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "HRM_Rating_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nRatingID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@RatingID", nRatingID);
                objDA.sqlCmdText = "HRM_Rating_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int nRatingID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nRatingID > 0)
                {
                    objParam[0] = new SqlParameter("@RatingID", nRatingID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "HRM_Rating_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
