using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class EmpAppraisalDAL
    {
        public int SaveMaster(EmpAppraisalBOL objAppPeriod)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@AppPeriodID", objAppPeriod.AppPeriodID);
                objParam[++i] = new SqlParameter("@AppTemplateID", objAppPeriod.AppTemplateID);
                objParam[++i] = new SqlParameter("@EmployeeID", objAppPeriod.EmployeeID);
                objParam[++i] = new SqlParameter("@CreatedBy", objAppPeriod.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Employee_AppraisalMaster_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveDetails(EmpAppraisalBOL objAppPeriod)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EAID", objAppPeriod.EAID);
                objParam[++i] = new SqlParameter("@CompetencyID", objAppPeriod.CompetencyID);
                objParam[++i] = new SqlParameter("@RatingID", objAppPeriod.RatingID);
                objParam[++i] = new SqlParameter("@Comments", objAppPeriod.Comments);

                objDA.sqlCmdText = "hrm_Employee_AppraisalDetails_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SubmitAppraisal(int EAID, int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EAID",EAID);
                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);

                objDA.sqlCmdText = "hrm_Employee_AppraisalMaster_Submit";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectAll(int nAppPeriodID, int Employeeid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {

                objParam[0] = new SqlParameter("@AppPeriodID", nAppPeriodID);
                objParam[1] = new SqlParameter("@EmployeeID", Employeeid);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Employee_AppraisalDetails_SELECT";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectLastAppraisal(int Employeeid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objParam[0] =  new SqlParameter("@EmployeeID", Employeeid);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Employee_Last_AppraisalPeriod";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
