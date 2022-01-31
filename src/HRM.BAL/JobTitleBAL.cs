using HRM.BOL;
using HRM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HRM.BAL
{
    public class JobTitleBAL
    {
        public int Save(JobTitleBOL objOrg)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.Save(objOrg);
        }

        public int Delete(int nDeptID)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.Delete(nDeptID);
        }

        public int UpdatePublishedType(JobTitleBOL objOrg)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.UpdatePublishedType(objOrg);
        }

        //public DataTable SelectAll(int nCompanyID)
        //{
        //    JobTitleDAL objDAL = new JobTitleDAL();
        //    return objDAL.SelectAll(nCompanyID);
        //}

        public DataTable SelectAll()
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.SelectAll();
        }

        public DataTable SelectJobTitleByJID(int JID)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.SelectJobTitleByJID(JID);
        }

        public DataTable SelectJobRequestBranchsByJID(int JID)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.SelectJobRequestBranchsByJID(JID);
        }
        public DataTable SelectJobCandidatesByJID(int id, string st)
        {
            JobTitleDAL objDAL = new JobTitleDAL();
            return objDAL.SelectJobCandidatesByJID(id,st);
        }
        public int DeleteCandidateprofile(int ID)
        {
            return new JobTitleDAL().DeleteCandidateprofile(ID);
        }
        
    }
}
