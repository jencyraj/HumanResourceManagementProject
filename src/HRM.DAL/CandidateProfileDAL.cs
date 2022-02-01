using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
    public class CandidateProfileDAL
    {
        public int Save(CandidateProfileBOL objOrg, DataTable dtQualification, DataTable dtExperience, DataTable dtLanguages)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[22];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@CandidateID", objOrg.CandidateID);
                objParam[++i] = new SqlParameter("@JID", objOrg.JID);
                objParam[++i] = new SqlParameter("@SaluteName", objOrg.SaluteName);
                objParam[++i] = new SqlParameter("@FirstName", objOrg.FirstName);
                objParam[++i] = new SqlParameter("@MiddleName", objOrg.MiddleName);
                objParam[++i] = new SqlParameter("@LastName", objOrg.LastName);
                objParam[++i] = new SqlParameter("@DateOfBirth", objOrg.DateOfBirth);
                objParam[++i] = new SqlParameter("@Gender", objOrg.Gender);
                objParam[++i] = new SqlParameter("@NationalityCode", objOrg.NationalityCode);
                objParam[++i] = new SqlParameter("@AddressLine", objOrg.AddressLine);
                objParam[++i] = new SqlParameter("@City", objOrg.City);
                objParam[++i] = new SqlParameter("@State", objOrg.State);
                objParam[++i] = new SqlParameter("@ZipCode", objOrg.ZipCode);
                objParam[++i] = new SqlParameter("@Country", objOrg.Country);
                objParam[++i] = new SqlParameter("@EmailAddress", objOrg.EmailAddress);
                objParam[++i] = new SqlParameter("@PhoneNumber", objOrg.PhoneNumber);
                objParam[++i] = new SqlParameter("@MobileNumber", objOrg.MobileNumber);
                objParam[++i] = new SqlParameter("@Skills", objOrg.Skills);
                objParam[++i] = new SqlParameter("@Interests", objOrg.Interests);
                objParam[++i] = new SqlParameter("@Achievements", objOrg.Achievements);
                objParam[++i] = new SqlParameter("@AdditionalInfo", objOrg.AdditionalInfo);
                //objParam[++i] = new SqlParameter("@AppliedDate", objOrg.AppliedDate);

                objDA.sqlCmdText = "hrm_CandidateProfile_Insert_Update";
                objDA.sqlParam = objParam;
                int candidateId = int.Parse(objDA.ExecuteScalar().ToString());

                foreach (DataRow row in dtQualification.Rows)
                {
                    try
                    {
                        if (row["EduLevel"] != DBNull.Value)
                        {
                            DataAccess objDA1 = new DataAccess();
                            SqlParameter[] objParam1 = new SqlParameter[8];
                            int j = -1;

                            objParam1[++j] = new SqlParameter("@EducationID", "");
                            objParam1[++j] = new SqlParameter("@CandidateID", candidateId);
                            objParam1[++j] = new SqlParameter("@EduLevel", row.Field<int>("EduLevel"));
                            objParam1[++j] = new SqlParameter("@University", row.Field<string>("University"));
                            objParam1[++j] = new SqlParameter("@College", row.Field<string>("College"));
                            objParam1[++j] = new SqlParameter("@Specialization", row.Field<string>("Specialization"));
                            objParam1[++j] = new SqlParameter("@PassedYear", row.Field<string>("Year"));
                            objParam1[++j] = new SqlParameter("@ScorePercentage", row.Field<string>("Score"));

                            objDA1.sqlCmdText = "hrm_Candidate_Education_Insert_Update";
                            objDA1.sqlParam = objParam1;
                            int.Parse(objDA1.ExecuteScalar().ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                foreach (DataRow row in dtExperience.Rows)
                {
                    try
                    {
                        if (row["Company"] != DBNull.Value)
                        {
                            DataAccess objDA2 = new DataAccess();
                            SqlParameter[] objParam2 = new SqlParameter[7];
                            int j = -1;

                            objParam2[++j] = new SqlParameter("@ExperienceID", "");
                            objParam2[++j] = new SqlParameter("@CandidateID", candidateId);
                            objParam2[++j] = new SqlParameter("@Company", row.Field<string>("Company"));
                            objParam2[++j] = new SqlParameter("@JobTitle", row.Field<string>("JobTitle"));
                            objParam2[++j] = new SqlParameter("@FromDate", row.Field<string>("FromDate"));
                            objParam2[++j] = new SqlParameter("@ToDate", row.Field<string>("ToDate"));
                            objParam2[++j] = new SqlParameter("@ReasonforLeaving", row.Field<string>("Reason"));

                            objDA2.sqlCmdText = "hrm_Candidate_Experience_Insert_Update";
                            objDA2.sqlParam = objParam2;
                            int.Parse(objDA2.ExecuteScalar().ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                foreach (DataRow row in dtLanguages.Rows)
                {
                    try
                    {
                        if (row["LanguageName"] != DBNull.Value)
                        {
                            DataAccess objDA3 = new DataAccess();
                            SqlParameter[] objParam3 = new SqlParameter[6];
                            int j = -1;

                            objParam3[++j] = new SqlParameter("@LangID", "");
                            objParam3[++j] = new SqlParameter("@CandidateID", candidateId);
                            objParam3[++j] = new SqlParameter("@LanguageName", row.Field<string>("LanguageName"));
                            objParam3[++j] = new SqlParameter("@Fluency", row.Field<string>("Fluency"));
                            objParam3[++j] = new SqlParameter("@Competency", row.Field<string>("Competency"));
                            objParam3[++j] = new SqlParameter("@Comments", row.Field<string>("Comments"));

                            objDA3.sqlCmdText = "hrm_Candidates_Language_Insert_Update";
                            objDA3.sqlParam = objParam3;
                            int.Parse(objDA3.ExecuteScalar().ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return candidateId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveReference(int candidateid, DataTable dtRef)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[6];
            int j = -1;

            objParam[++j] = new SqlParameter("@CandidateID", candidateid);
            objParam[++j] = new SqlParameter("@refName", dtRef.Rows[0]["REFNAME"]);
            objParam[++j] = new SqlParameter("@organisation", dtRef.Rows[0]["ORG"]);
            objParam[++j] = new SqlParameter("@email", dtRef.Rows[0]["EMAIL"]);
            objParam[++j] = new SqlParameter("@phone", dtRef.Rows[0]["PHONE"]);

            objDA.sqlCmdText = "hrm_Candidates_Reference_Insert_Update";
            objDA.sqlParam = objParam;
            return objDA.ExecuteNonQuery();
        }

        public int Delete(int candidateid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CandidateID", candidateid);
                objDA.sqlCmdText = "hrm_CandidateProfile_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Recycle(int candidateid)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CandidateID", candidateid);
                objDA.sqlCmdText = "hrm_CandidateProfile_Delete_Full";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
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
                objDA.sqlCmdText = "hrm_CandidateProfile_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectCandidateProfileById(int JID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            int i = -1;

            try
            {
                if (JID > 0)
                    objParam[++i] = new SqlParameter("@CandidateID", JID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_CandidateProfile_SelectByID";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CandidateProfileBOL SelectcandidateProfileForlist(int id)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            CandidateProfileBOL objBOL = new CandidateProfileBOL();


            try
            {

                objDA.sqlCmdText = "hrm_CandidateProfile_SelectByID";

                if (id > 0)
                {
                    objParam[0] = new SqlParameter("@candidateid", id);
                    objDA.sqlParam = objParam;
                }

                SqlDataReader dReader = objDA.ExecuteDataReader();

                while (dReader.Read())
                {
                    objBOL.CandidateID = id;

                    objBOL.AppliedDate = Util.ToDateTime("" + dReader["AppliedDate"]);
                    objBOL.AppliedStatus = "" + dReader["ApplicationStatus"];
                    objBOL.SaluteName = "" + dReader["SaluteName"];
                    objBOL.FirstName = "" + dReader["FirstName"];
                    objBOL.MiddleName = "" + dReader["MiddleName"];
                    objBOL.LastName = "" + dReader["LastName"];
                    objBOL.DateOfBirth = "" + dReader["DateOfBirth"];
                    objBOL.Gender = "" + dReader["Gender"];
                    objBOL.NationalityCode = "" + dReader["NationalityCode"];
                    objBOL.AddressLine = "" + dReader["AddressLine"];
                    objBOL.City = "" + dReader["City"];
                    objBOL.State = "" + dReader["State"];
                    objBOL.ZipCode = "" + dReader["ZipCode"];
                    objBOL.Country = "" + dReader["Country"];
                    objBOL.EmailAddress = "" + dReader["EmailAddress"];
                    objBOL.PhoneNumber = "" + dReader["PhoneNumber"];
                    objBOL.MobileNumber = "" + dReader["MobileNumber"];
                    objBOL.Skills = "" + dReader["Skills"];
                    objBOL.Interests = "" + dReader["Interests"];
                    objBOL.Achievements = "" + dReader["Achievements"];
                    objBOL.AdditionalInfo = "" + dReader["AdditionalInfo"];
                    objBOL.JobTitle = "" + dReader["JobTitle"];
                    objBOL.Nationality = "" + dReader["Nationality"];


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;


        }

        public DataSet GetCandidateData(int CandidateId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@CandidateId", CandidateId));

            objDA.sqlCmdText = "hrm_CandidateData";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet();
        }
        public int Savestatus(CandidateProfileBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@CandidateID", objBOL.CandidateID));
                objParam.Add(new SqlParameter("@Status", objBOL.Status));
                objParam.Add(new SqlParameter("@ApplicationStatus", objBOL.ApplicationStatus));
                if (objBOL.ApplicationStatus == "IVW")
                {
                    objParam.Add(new SqlParameter("@InterviewDate", objBOL.InterviewDate));
                }
                objDA.sqlCmdText = "hrm_CandidateStatus_Update_Insert";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Selectemail()
        {
            DataAccess objDA = new DataAccess();
            try
            {
                objDA.sqlCmdText = "hrm_Email_template_select";
                SqlDataReader dReader = objDA.ExecuteDataReader();
                return objDA.ExecuteDataSet().Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Selectcandidatereport(CandidateProfileBOL objBOL)
        {
            // CandidateProfileBOL objBOL = new CandidateProfileBOL();
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[4];

            try
            {
                if ("" + objBOL.AppliedStatus != "")
                    objParam[0] = new SqlParameter("@ApplicationStatus", objBOL.AppliedStatus);
                if ("" + objBOL.JobTitle != "")
                    objParam[1] = new SqlParameter("@JobTitle", objBOL.JobTitle);
                if (objBOL.fromdate != DateTime.MinValue)
                    objParam[2] = new SqlParameter("@StartDate", objBOL.fromdate);
                if (objBOL.todate != DateTime.MinValue)
                    objParam[3] = new SqlParameter("@EndDate", objBOL.todate);
                objDA.sqlParam = objParam;


                objDA.sqlCmdText = "hrm_SelectCandidateReport";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
