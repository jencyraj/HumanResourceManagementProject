using HRM.BOL;
using HRM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HRM.BAL
{
    public class CandidateProfileBAL
    {
        public int Save(CandidateProfileBOL objOrg, DataTable dtQualification, DataTable dtExperience, DataTable dtLanguages)
        {
            CandidateProfileDAL objDAL = new CandidateProfileDAL();
            return objDAL.Save(objOrg, dtQualification, dtExperience, dtLanguages);
        }

        public int SaveReference(int candidateid, DataTable dtRef)
        {
            return new CandidateProfileDAL().SaveReference(candidateid, dtRef);
        }

        public int Delete(int candidateid)
        {
            CandidateProfileDAL objDAL = new CandidateProfileDAL();
            return objDAL.Delete(candidateid);
        }

        public int Recycle(int candidateid)
        {
            return new CandidateProfileDAL().Recycle(candidateid);
        }

        public DataTable SelectAll()
        {
            CandidateProfileDAL objDAL = new CandidateProfileDAL();
            return objDAL.SelectAll();
        }

        public DataTable SelectCandidateProfileById(int JID)
        {
            CandidateProfileDAL objDAL = new CandidateProfileDAL();
            return objDAL.SelectCandidateProfileById(JID);
        }
        public CandidateProfileBOL selectprofile(int id)
        {
            CandidateProfileDAL objDAL = new CandidateProfileDAL();
            return objDAL.SelectcandidateProfileForlist(id);
        }

        public DataSet GetCandidateData(int CandidateId)
        {
            return new CandidateProfileDAL().GetCandidateData(CandidateId);
        }
        public int Savestatus(CandidateProfileBOL objBOL)
        {
            return new  CandidateProfileDAL().Savestatus(objBOL);
        }

        public DataTable Selectemail()
        {

            return new CandidateProfileDAL().Selectemail();
        }
        public DataTable Selectcandidatereport(CandidateProfileBOL objBOL)
        {
            return new CandidateProfileDAL().Selectcandidatereport(objBOL);
        }
    }
}
