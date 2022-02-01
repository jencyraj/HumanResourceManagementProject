using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
    public class JobTitleDAL
    {
        public int Save(JobTitleBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[19];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@JID", objOrg.JID);
                objParam[++i] = new SqlParameter("@DepartmentID", objOrg.DepartmentID);
                objParam[++i] = new SqlParameter("@EmplStatusID", objOrg.EmplStatusID);
                objParam[++i] = new SqlParameter("@VacancyNos", objOrg.VacancyNos);
                objParam[++i] = new SqlParameter("@AgeFrom", objOrg.AgeFrom);
                objParam[++i] = new SqlParameter("@AgeTo", objOrg.AgeTo);
                objParam[++i] = new SqlParameter("@SalaryFrom", objOrg.SalaryFrom);
                objParam[++i] = new SqlParameter("@SalaryTo", objOrg.SalaryTo);
                objParam[++i] = new SqlParameter("@JobTitle", objOrg.JobTitle);
                objParam[++i] = new SqlParameter("@Experience", objOrg.Experience);
                objParam[++i] = new SqlParameter("@Qualification", objOrg.Qualification);
                objParam[++i] = new SqlParameter("@JobPostDescription", objOrg.JobPostDescription);
                objParam[++i] = new SqlParameter("@AdditionalInfo", objOrg.AdditionalInfo);
                objParam[++i] = new SqlParameter("@Published", objOrg.Published);
                objParam[++i] = new SqlParameter("@CreatedBy", objOrg.CreatedBy);
                objParam[++i] = new SqlParameter("@ModifiedBy", objOrg.ModifiedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@Branches", objOrg.Branches);
                objParam[++i] = new SqlParameter("@ClosingDate", objOrg.ClosingDate);

                objDA.sqlCmdText = "hrm_JobRequests_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int JID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@JID", JID);
                objDA.sqlCmdText = "hrm_JobRequests_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdatePublishedType(JobTitleBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;
            try
            {
                objParam[++i] = new SqlParameter("@JID", objOrg.JID);
                objParam[++i] = new SqlParameter("@Published", objOrg.Published);
                objParam[++i] = new SqlParameter("@PublishedBy", objOrg.PublishedBy);
                objDA.sqlCmdText = "hrm_JobRequests_Update_PublishedType";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(OrgDepartmentBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            int i = -1;
            try
            {
                if (objBOL.CompanyID > 0)
                    objParam[++i] = new SqlParameter("@CompanyID", objBOL.CompanyID);

                if (objBOL.DeptID > 0)
                    objParam[++i] = new SqlParameter("@DepartmentID", objBOL.DeptID);

                if (objBOL.ParentDeptID > 0)
                    objParam[++i] = new SqlParameter("@ParentDepartmentID", objBOL.ParentDeptID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "HRM_COM_DEPARTMENTS_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectJobTitleByJID(int JID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            int i = -1;

            try
            {
                if (JID > 0)
                    objParam[++i] = new SqlParameter("@JID", JID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_JobRequests_SelectByJID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectAll()
        {
            DataAccess objDA = new DataAccess();
            try
            {
                objDA.sqlCmdText = "hrm_JobRequests_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectJobRequestBranchsByJID(int JID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            int i = -1;

            try
            {
                if (JID > 0)
                    objParam[++i] = new SqlParameter("@JID", JID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_JobRequests_Branch_SelectByJID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectJobCandidatesByJID(int id, string st)
        {
   
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            int i = -1;

            try
            {
                if (id > 0)
                {
                    objParam[++i] = new SqlParameter("@JID", id);
                }
                if (st!="")
                {
                    objParam[++i] = new SqlParameter("@ApplicationStatus", st);

                }
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_JobCandidate_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int DeleteCandidateprofile(int ID)
        {

            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CandidateID", ID);
                objDA.sqlCmdText = "hrm_CandidateProfile_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
