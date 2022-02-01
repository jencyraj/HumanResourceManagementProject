using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using HRM.BOL;

namespace HRM.DAL
{
    public class EmployeeDAL
    {
        public string GetNextEmployeeCode()
        {
            DataAccess objDA = new DataAccess();

            objDA.sqlCmdText = "hrm_EmpCode_GetNew";
            return objDA.ExecuteScalar().ToString();
        }

        public int UpdateEmpCodeSettings(string EmpCodePrefix, string EmpCodeCtrPrefix, int EmpCodeCtrStart, int EmpCodeTotalLength, string ModifiedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            objParam[++i] = new SqlParameter("@EmpCodePrefix", EmpCodePrefix);
            objParam[++i] = new SqlParameter("@EmpCodeCtrPrefix", EmpCodeCtrPrefix);
            objParam[++i] = new SqlParameter("@EmpCodeCtrStart", EmpCodeCtrStart);
            objParam[++i] = new SqlParameter("@EmpCodeTotalLength", EmpCodeTotalLength);
            objParam[++i] = new SqlParameter("@ModifiedBy", ModifiedBy);

            objDA.sqlCmdText = "hrm_EmpCode_Update";
            objDA.sqlParam = objParam;
            return objDA.ExecuteNonQuery();
        }
        public DataTable GetCount()
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objDA.sqlCmdText = "[hrm_Employee_GetCount]";

                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }
        public int UpdateECodeSettings(string EmpCodeCtrPrefix, int EmpCodeCtrStart, int EmpCodeTotalLength, string ModifiedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;


            objParam[++i] = new SqlParameter("@EmpCodeCtrPrefix", EmpCodeCtrPrefix);
            objParam[++i] = new SqlParameter("@EmpCodeCtrStart", EmpCodeCtrStart);
            objParam[++i] = new SqlParameter("@EmpCodeTotalLength", EmpCodeTotalLength);
            objParam[++i] = new SqlParameter("@ModifiedBy", ModifiedBy);

            objDA.sqlCmdText = "hrm_EmpCodeSett_Update";
            objDA.sqlParam = objParam;
            return objDA.ExecuteNonQuery();
        }

        public DataTable SelectEmpCodeSettings()
        {
            DataAccess objDA = new DataAccess();

            objDA.sqlCmdText = "hrm_EmpCode_Select";
            return objDA.ExecuteDataSet().Tables[0];
        }

        public DataTable SelectNationality(string sLang)
        {
            DataAccess objDA = new DataAccess();

            objDA.sqlCmdText = "HRM_NATIONALITY_SELECT";

            if ("" + sLang != "")
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@lang", sLang);
                objDA.sqlParam = objParam;
            }
            return objDA.ExecuteDataSet().Tables[0];
        }

        public DataTable GetJuniorEmployees(EmployeeBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objDA.sqlCmdText = "hrm_Employees_GetJuniorEmployees";
                if (objBOL.EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeID));
                if ("" + objBOL.FirstName != "")
                    objParam.Add(new SqlParameter("@FirstName", objBOL.FirstName));
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable Search(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[15];
            int i = -1;

            try
            {

                objDA.sqlCmdText = "hrm_Employees_Search";

                if (objEmp.BranchID > 0)
                    objParam[++i] = new SqlParameter("@BranchID", objEmp.BranchID);

                if (objEmp.DeptId > 0)
                    objParam[++i] = new SqlParameter("@DeptId", objEmp.DeptId);

                if ("" + objEmp.EmpStatus != "")
                    objParam[++i] = new SqlParameter("@EmpStatus", objEmp.EmpStatus);

                if (objEmp.RoleID > 0)
                    objParam[++i] = new SqlParameter("@RoleID", objEmp.RoleID);

                if (objEmp.DesgnID > 0)
                    objParam[++i] = new SqlParameter("@DesgnID", objEmp.DesgnID);

                if ("" + objEmp.FirstName != "")
                    objParam[++i] = new SqlParameter("@FirstName", objEmp.FirstName);
                if ("" + objEmp.MiddleName != "")
                    objParam[++i] = new SqlParameter("@MiddleName", objEmp.MiddleName);
                if ("" + objEmp.LastName != "")
                    objParam[++i] = new SqlParameter("@LastName", objEmp.LastName);
                if ("" + objEmp.EmpCode != "")
                    objParam[++i] = new SqlParameter("@EmpCode", objEmp.EmpCode);

                objDA.sqlParam = objParam;

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SearchRpt(EmployeeBOL objEmp)
        {

            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[15];
            int i = -1;

            try
            {

                objDA.sqlCmdText = "hrm_Employees_Search_Rpt";

                if (objEmp.BranchID > 0)
                    objParam[++i] = new SqlParameter("@BranchID", objEmp.BranchID);

                if (objEmp.DeptId > 0)
                    objParam[++i] = new SqlParameter("@DeptId", objEmp.DeptId);

                if ("" + objEmp.EmpStatus != "")
                    objParam[++i] = new SqlParameter("@EmpStatus", objEmp.EmpStatus);

                if (objEmp.RoleID > 0)
                    objParam[++i] = new SqlParameter("@RoleID", objEmp.RoleID);

                if (objEmp.DesgnID > 0)
                    objParam[++i] = new SqlParameter("@DesgnID", objEmp.DesgnID);

                if ("" + objEmp.FirstName != "")
                    objParam[++i] = new SqlParameter("@FirstName", objEmp.FirstName);
                if ("" + objEmp.MiddleName != "")
                    objParam[++i] = new SqlParameter("@MiddleName", objEmp.MiddleName);
                if ("" + objEmp.LastName != "")
                    objParam[++i] = new SqlParameter("@LastName", objEmp.LastName);
                if ("" + objEmp.EmpCode != "")
                    objParam[++i] = new SqlParameter("@EmpCode", objEmp.EmpCode);
                if ("" + objEmp.Year != "")
                    objParam[++i] = new SqlParameter("@Year", objEmp.Year);
                if ("" + objEmp.month != "")
                    objParam[++i] = new SqlParameter("@Month", objEmp.month);
                objDA.sqlParam = objParam;

                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmployeeBOL Select(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[15];
            int i = -1;

            try
            {

                objDA.sqlCmdText = "hrm_Employees_Select";
                if ("" + objEmp.UserID != "")
                    objParam[++i] = new SqlParameter("@USERID", objEmp.UserID);
                if (objEmp.EmployeeID > 0)
                    objParam[++i] = new SqlParameter("@EmpID", objEmp.EmployeeID);
                objDA.sqlParam = objParam;

                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objEmp.EmployeeID = Util.ToInt("" + dReader["EmployeeID"]);
                    objEmp.BranchID = Util.ToInt("" + dReader["BranchID"]);
                    objEmp.DeptId = Util.ToInt("" + dReader["DeptId"]);
                    objEmp.SubDeptID = Util.ToInt("" + dReader["SubDeptID"]);
                    objEmp.DesgnID = Util.ToInt("" + dReader["DesgnID"]);
                    objEmp.CountryID = "" + dReader["CountryID"];
                    objEmp.RoleID = Util.ToInt("" + dReader["RoleID"]);

                    objEmp.BiometricID = "" + dReader["BiometricID"];
                    objEmp.IRISID = "" + dReader["IRISID"];

                    objEmp.UserID = "" + dReader["USERID"];
                    objEmp.Password = "" + dReader["Password"];
                    objEmp.EmpStatus = "" + dReader["EmpStatus"];
                    objEmp.EmpCode = "" + dReader["EmpCode"];
                    objEmp.IDDesc = "" + dReader["IDDesc"];
                    objEmp.IDNO = "" + dReader["IDNO"];
                    objEmp.FirstName = "" + dReader["FirstName"];
                    objEmp.MiddleName = "" + dReader["MiddleName"];
                    objEmp.LastName = "" + dReader["LastName"];
                    objEmp.Gender = "" + dReader["Gender"];
                    objEmp.BloodGroup = "" + dReader["BloodGroup"];
                    objEmp.SaluteName = "" + dReader["SaluteName"];
                    objEmp.Address1 = "" + dReader["Address1"];
                    objEmp.Address2 = "" + dReader["Address2"];
                    objEmp.City = "" + dReader["City"];
                    objEmp.State = "" + dReader["State"];
                    objEmp.ZipCode = "" + dReader["ZipCode"];
                    objEmp.HPhone = "" + dReader["HPhone"];
                    objEmp.HMobile = "" + dReader["HMobile"];
                    objEmp.HEmail = "" + dReader["HEmail"];
                    objEmp.WPhone = "" + dReader["WPhone"];
                    objEmp.WEmail = "" + dReader["WEmail"];
                    objEmp.Website = "" + dReader["Website"];
                    objEmp.NationalityCode = "" + dReader["NationalityCode"];
                    objEmp.MaritalStatus = "" + dReader["MaritalStatus"];
                    objEmp.BankName = "" + dReader["BankName"];
                    objEmp.BankBranch = "" + dReader["BankBranch"];
                    objEmp.OtherBankDetails = "" + dReader["OtherBankDetails"];
                    objEmp.AccountNumber = "" + dReader["AccountNumber"];
                    objEmp.Status = "" + dReader["Status"];
                    objEmp.CreatedBy = "" + dReader["CreatedBy"];
                    objEmp.ModifiedBy = "" + dReader["ModifiedBy"];
                    objEmp.JobType = "" + dReader["JobType"];
                    objEmp.PhotoName = "" + dReader["PhotoName"];

                    objEmp.DateOfBirth = "" + dReader["DateOfBirth"];// Util.ToDateTime("" + dReader["DateOfBirth"]);
                    objEmp.JoiningDate = "" + dReader["JoiningDate"];
                    objEmp.ProbationStartDate = "" + dReader["ProbationStartDate"];
                    objEmp.ProbationEndDate = "" + dReader["ProbationEndDate"];
                    objEmp.CreatedDate = Util.ToDateTime("" + dReader["CreatedDate"]);
                    objEmp.ModifiedDate = Util.ToDateTime("" + dReader["ModifiedDate"]);

                    objEmp.Designation.Designation = "" + dReader["Designation"];
                    objEmp.Designation.DesgnCode = "" + dReader["DesgnCode"];

                    objEmp.Department.DeptCode = "" + dReader["DeptCode"];
                    objEmp.Department.DepartmentName = "" + dReader["DepartmentName"];

                    objEmp.Branch.Branch = "" + dReader["Branch"];

                    objEmp.Roles.RoleID = Util.ToInt("" + dReader["RoleID"]);
                    objEmp.Roles.RoleName = "" + dReader["RoleName"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEmp;
        }

        public int Save(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[50];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@EmpStatus", objEmp.EmpStatus);
                objParam[++i] = new SqlParameter("@BranchID", objEmp.BranchID);
                objParam[++i] = new SqlParameter("@DeptId", objEmp.DeptId);
                objParam[++i] = new SqlParameter("@SubDeptID", objEmp.SubDeptID);
                objParam[++i] = new SqlParameter("@DesgnID", objEmp.DesgnID);
                objParam[++i] = new SqlParameter("@RoleID", objEmp.RoleID);
                objParam[++i] = new SqlParameter("@UserID", objEmp.UserID);
                objParam[++i] = new SqlParameter("@Password", objEmp.Password);
                objParam[++i] = new SqlParameter("@BiometricID", objEmp.BiometricID);
                objParam[++i] = new SqlParameter("@IRISID", objEmp.IRISID);
                objParam[++i] = new SqlParameter("@EmpCode", objEmp.EmpCode);
                objParam[++i] = new SqlParameter("@IDDesc", objEmp.IDDesc);
                objParam[++i] = new SqlParameter("@IDNO", objEmp.IDNO);
                objParam[++i] = new SqlParameter("@SaluteName", objEmp.SaluteName);
                objParam[++i] = new SqlParameter("@FirstName", objEmp.FirstName);
                objParam[++i] = new SqlParameter("@MiddleName", objEmp.MiddleName);
                objParam[++i] = new SqlParameter("@LastName", objEmp.LastName);
                objParam[++i] = new SqlParameter("@Gender", objEmp.Gender);
                objParam[++i] = new SqlParameter("@BloodGroup", objEmp.BloodGroup);
             
                objParam[++i] = new SqlParameter("@MaritalStatus", objEmp.MaritalStatus);
                objParam[++i] = new SqlParameter("@NationalityCode", objEmp.NationalityCode);
                if (objEmp.DateOfBirth != "" + DateTime.MinValue)
                    objParam[++i] = new SqlParameter("@DateOfBirth", objEmp.DateOfBirth);
                objParam[++i] = new SqlParameter("@DateOfBirthAR", objEmp.DateOfBirthAR);
                objParam[++i] = new SqlParameter("@Address1", objEmp.Address1);
                objParam[++i] = new SqlParameter("@Address2", objEmp.Address2);
                objParam[++i] = new SqlParameter("@City", objEmp.City);
                objParam[++i] = new SqlParameter("@State", objEmp.State);
                objParam[++i] = new SqlParameter("@CountryID", objEmp.CountryID);
                objParam[++i] = new SqlParameter("@ZipCode", objEmp.ZipCode);
                objParam[++i] = new SqlParameter("@HPhone", objEmp.HPhone);
                objParam[++i] = new SqlParameter("@HMobile", objEmp.HMobile);
                objParam[++i] = new SqlParameter("@HEmail", objEmp.HEmail);
                objParam[++i] = new SqlParameter("@WPhone", objEmp.WPhone);
                objParam[++i] = new SqlParameter("@WEmail", objEmp.WEmail);
                objParam[++i] = new SqlParameter("@Website", objEmp.Website);
                objParam[++i] = new SqlParameter("@JobType", objEmp.JobType);
                if (objEmp.JoiningDate != "") //DateTime.MinValue)
                    objParam[++i] = new SqlParameter("@JoiningDate", objEmp.JoiningDate);
                if (objEmp.ProbationStartDate != "") //DateTime.MinValue)
                    objParam[++i] = new SqlParameter("@ProbationStartDate", objEmp.ProbationStartDate);
                if (objEmp.ProbationEndDate != "") //DateTime.MinValue)
                    objParam[++i] = new SqlParameter("@ProbationEndDate", objEmp.ProbationEndDate);
                objParam[++i] = new SqlParameter("@BankName", objEmp.BankName);
                objParam[++i] = new SqlParameter("@BankBranch", objEmp.BankBranch);
                objParam[++i] = new SqlParameter("@OtherBankDetails", objEmp.OtherBankDetails);
                objParam[++i] = new SqlParameter("@AccountNumber", objEmp.AccountNumber);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEmergencyContacts(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmgContactID", objEmp.EmgContactID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@ContactName", objEmp.ContactName);
                objParam[++i] = new SqlParameter("@Relationship", objEmp.Relationship);
                objParam[++i] = new SqlParameter("@HPhone", objEmp.HPhone);
                objParam[++i] = new SqlParameter("@WPhone", objEmp.WPhone);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_EmgcyContacts_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveDependents(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@DependentID", objEmp.DependentID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@DependentName", objEmp.DependentName);
                objParam[++i] = new SqlParameter("@Relationship", objEmp.Relationship);
                objParam[++i] = new SqlParameter("@DateOfBirth", objEmp.DateOfBirth);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Dependents_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEducation(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[15];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EducationID", objEmp.EducationID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@EduLevel", objEmp.EduLevel);
                objParam[++i] = new SqlParameter("@University", objEmp.University);
                objParam[++i] = new SqlParameter("@College", objEmp.College);
                objParam[++i] = new SqlParameter("@Specialization", objEmp.Specialization);
                objParam[++i] = new SqlParameter("@PassedYear", objEmp.PassedYear);
                objParam[++i] = new SqlParameter("@ScorePercentage", objEmp.ScorePercentage);
                objParam[++i] = new SqlParameter("@StartDate", objEmp.StartDate);
                objParam[++i] = new SqlParameter("@EndDate", objEmp.EndDate);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Education_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveExperience(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ExpID", objEmp.ExpID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@Company", objEmp.Company);
                objParam[++i] = new SqlParameter("@JobTitle", objEmp.JobTitle);
                // if (objEmp.FromDate != DateTime.MinValue)
                objParam[++i] = new SqlParameter("@FromDate", objEmp.FromDate);
                // if (objEmp.ToDate != DateTime.MinValue)
                objParam[++i] = new SqlParameter("@ToDate", objEmp.ToDate);
                objParam[++i] = new SqlParameter("@Comments", objEmp.Comments);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Experience_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveImmigration(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[15];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ImmigrationId", objEmp.ImmigrationId);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@DocType", objEmp.DocType);
                objParam[++i] = new SqlParameter("@DocNo", objEmp.DocNo);
                //   if (objEmp.IssueDate != DateTime.MinValue)
                objParam[++i] = new SqlParameter("@IssueDate", objEmp.IssueDate);
                // if (objEmp.ExpiryDate != DateTime.MinValue)
                objParam[++i] = new SqlParameter("@ExpiryDate", objEmp.ExpiryDate);
                objParam[++i] = new SqlParameter("@EligibleStatus", objEmp.EligibleStatus);
                objParam[++i] = new SqlParameter("@IssuedCountryID", objEmp.IssuedCountryID);
                //  if (objEmp.EligibleReviewDate != DateTime.MinValue)
                objParam[++i] = new SqlParameter("@EligibleReviewDate", objEmp.EligibleReviewDate);
                objParam[++i] = new SqlParameter("@Comments", objEmp.Comments);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Immigration_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveLanguage(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@LangId", objEmp.LangID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@LanguageName", objEmp.LanguageName);
                objParam[++i] = new SqlParameter("@Fluency", objEmp.Fluency);
                objParam[++i] = new SqlParameter("@Competency", objEmp.Competency);
                objParam[++i] = new SqlParameter("@Comments", objEmp.Comments);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Language_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveSkills(EmployeeBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@SkillId", objEmp.SkillID);
                objParam[++i] = new SqlParameter("@EmployeeID", objEmp.EmployeeID);
                objParam[++i] = new SqlParameter("@Description", objEmp.Description);
                objParam[++i] = new SqlParameter("@ExpinYears", objEmp.ExpinYears);
                objParam[++i] = new SqlParameter("@Comments", objEmp.Comments);
                objParam[++i] = new SqlParameter("@Status", objEmp.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objEmp.CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Skills_Insert_Update";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int EID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EmpID", EID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetOtherDetails(int EID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@EID", EID);
                objDA.sqlCmdText = "hrm_Employees_OtherDetails";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region DELETE

        public int DeleteEmergencyContact(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EmgID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Emergency_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteDependent(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@DEPID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Dependent_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteEducation(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EDUID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Education_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteExperience(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EXPID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Experience_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteLanguage(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@langID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Language_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteSkill(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@SkillID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Skills_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteImmigration(int nID, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@ImmgID", nID);
                objParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                objDA.sqlCmdText = "hrm_Employees_Immigration_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public int UpdatePhoto(int EmpID, string sPhotoName)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EmployeeID", EmpID);
                objParam[1] = new SqlParameter("@PhotoName", sPhotoName);
                objDA.sqlCmdText = "hrm_Employees_Photo_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region DOCUMENTS

        public int UploadDocument(int EmployeeID, string Description, string sFileName, string CreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);
                objParam[++i] = new SqlParameter("@Description", Description);
                objParam[++i] = new SqlParameter("@DocName", sFileName);
                objParam[++i] = new SqlParameter("@CreatedBy", CreatedBy);

                objDA.sqlCmdText = "hrm_Employees_Documents_Update";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteDocument(int DocumentID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@DocID", DocumentID);

                objDA.sqlCmdText = "hrm_Employees_Documents_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAllDocuments(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@EmployeeID", EmployeeID);

                objDA.sqlCmdText = "hrm_Employees_Documents_Select";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int ChangePassword(EmployeeBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@EmployeeID", objBOL.EmployeeID);
                objParam[1] = new SqlParameter("@Password", objBOL.Password);
                objDA.sqlCmdText = "hrm_Change_Password";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EmployeeStatistics(int BranchID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@BranchID", BranchID);
                objDA.sqlCmdText = "hrm_Employee_Statistics";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet viewsalaryDetail(int Month, int year)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;
            try
            {

                objParam[++i] = new SqlParameter("@NMONTH", Month);
                objParam[++i] = new SqlParameter("@NYEAR", year);
                objDA.sqlCmdText = "[hrm_ViewDetailSalary]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable employeeBenefit(EmployeeBOL objbol)
        {
            DataAccess objDA = new DataAccess();

            List<SqlParameter> objParam = new List<SqlParameter>();
            int i = -1;

            try
            {


                if (objbol.BranchID > 0)
                    objParam.Add(new SqlParameter("@BranchID", objbol.BranchID));

                if (objbol.DeptId > 0)
                    objParam.Add(new SqlParameter("@DeptId", objbol.DeptId));

                if ("" + objbol.EmpStatus != "")
                    objParam.Add(new SqlParameter("@EmpStatus", objbol.EmpStatus));

                if (objbol.RoleID > 0)
                    objParam.Add(new SqlParameter("@RoleID", objbol.RoleID));

                if (objbol.DesgnID > 0)
                    objParam.Add(new SqlParameter("@DesgnID", objbol.DesgnID));

                if ("" + objbol.FirstName != "")
                    objParam.Add(new SqlParameter("@FirstName", objbol.FirstName));
                if ("" + objbol.MiddleName != "")
                    objParam.Add(new SqlParameter("@MiddleName", objbol.MiddleName));
                if ("" + objbol.LastName != "")
                    objParam.Add(new SqlParameter("@LastName", objbol.LastName));
                if ("" + objbol.EmpCode != "")
                    objParam.Add(new SqlParameter("@EmpCode", objbol.EmpCode));

                objDA.sqlCmdText = "hrm_employeebenefit_report";

                objDA.sqlParam = objParam.ToArray();


                return objDA.ExecuteDataSet().Tables[0];
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
