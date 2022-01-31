using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class AppraisalCompetencyBAL
    {
        public int Save(AppraisalCompetencyBOL objCy)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.Save(objCy);
        }

        public int Delete(int nCompetencyID, string CreatedBy)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.Delete(nCompetencyID,CreatedBy);
        }

        public DataTable SelectAll(AppraisalCompetencyBOL objCy)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.SelectAll(objCy);
        }

        public AppraisalCompetencyBOL SelectByID(int ComID)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.SelectByID(ComID);
        }

        public int SaveCompetencyType(AppraisalCompetencyBOL objCy)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.SaveCompetencyType(objCy);
        }

        public int DeleteCompetencyType(int CompetencyTypeID, string CreatedBy)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.DeleteCompetencyType(CompetencyTypeID, CreatedBy);
        }

        public DataTable SelectAllCompetencyTypes(int CompetencyTypeID)
        {
            AppraisalCompetencyDAL objDAL = new AppraisalCompetencyDAL();
            return objDAL.SelectAllCompetencyTypes(CompetencyTypeID);
        }
    }
}
