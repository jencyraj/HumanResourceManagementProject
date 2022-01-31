using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class EmployeeBAL
    {
        public string GetNextEmployeeCode()
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetNextEmployeeCode();
        }

        public int UpdateEmpCodeSettings(string EmpCodePrefix, string EmpCodeCtrPrefix, int EmpCodeCtrStart, int EmpCodeTotalLength, string ModifiedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.UpdateEmpCodeSettings(EmpCodePrefix, EmpCodeCtrPrefix, EmpCodeCtrStart, EmpCodeTotalLength, ModifiedBy);
        }
        public int UpdateECodeSettings(string EmpCodeCtrPrefix, int EmpCodeCtrStart, int EmpCodeTotalLength, string ModifiedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.UpdateECodeSettings(EmpCodeCtrPrefix, EmpCodeCtrStart, EmpCodeTotalLength, ModifiedBy);
        }
        public DataTable SelectEmpCodeSettings()
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SelectEmpCodeSettings();
        }

        public DataTable SelectNationality(string sLang)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SelectNationality(sLang);
        }
        public DataTable GetCount()
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetCount();
        }
        public DataTable Search(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.Search(objEmp);
        }
        public DataTable SearchRpt(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SearchRpt(objEmp);
        }
        public EmployeeBOL Select(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.Select(objEmp);
        }

        public int Save(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.Save(objEmp);
        }

        public int SaveEmergencyContacts(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveEmergencyContacts(objEmp);
        }

        public int SaveDependents(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveDependents(objEmp);
        }

        public int SaveEducation(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveEducation(objEmp);
        }

        public int SaveExperience(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveExperience(objEmp);
        }

        public int SaveImmigration(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveImmigration(objEmp);
        }

        public int SaveLanguage(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveLanguage(objEmp);
        }

        public int SaveSkills(EmployeeBOL objEmp)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SaveSkills(objEmp);
        }

        public int Delete(int EID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.Delete(EID, CreatedBy);
        }

        public DataSet GetOtherDetails(int EID)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetOtherDetails(EID);
        }
        public DataSet viewsalaryDetail( int month, int year)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.viewsalaryDetail( month, year);
        }
        public DataTable GetJuniorEmployees(EmployeeBOL objBOL)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetJuniorEmployees(objBOL);
        }


        #region DELETE

        public int DeleteEmergencyContact(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteEmergencyContact(nID, CreatedBy);
        }

        public int DeleteDependent(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteDependent(nID, CreatedBy);
        }

        public int DeleteEducation(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteEducation(nID, CreatedBy);
        }

        public int DeleteExperience(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteExperience(nID, CreatedBy);
        }

        public int DeleteLanguage(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteLanguage(nID, CreatedBy);
        }

        public int DeleteSkill(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteSkill(nID, CreatedBy);
        }

        public int DeleteImmigration(int nID, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteImmigration(nID, CreatedBy);
        }
        #endregion

        public int UpdatePhoto(int EmpID, string sPhotoName)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.UpdatePhoto(EmpID, sPhotoName);
        }

        #region DOCUMENTS

        public int UploadDocument(int EmployeeID, string Description, string sFileName, string CreatedBy)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.UploadDocument(EmployeeID, Description,sFileName,CreatedBy);
        }

        public int DeleteDocument(int DocumentID)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.DeleteDocument(DocumentID);
        }

        public DataTable SelectAllDocuments(int EmployeeID)
        {

            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.SelectAllDocuments(EmployeeID);
        }

        #endregion

        public int ChangePassword(EmployeeBOL objBOL)
        {
            EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.ChangePassword(objBOL);
        }

        public DataTable EmployeeStatistics(int BranchID)
        {
            return new EmployeeDAL().EmployeeStatistics(BranchID);
        }
        public DataTable employeeBenefit(EmployeeBOL objbol)
        {
            return new EmployeeDAL().employeeBenefit(objbol);
        }
    }
}
