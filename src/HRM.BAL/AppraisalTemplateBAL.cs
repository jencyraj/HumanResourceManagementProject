using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AppraisalTemplateBAL
    {
        public int Save(AppraisalCompetencyBOL objCy)
        {
            return new AppraisalTemplateDAL().Save(objCy);
        }

        public DataTable GetAppraisalTemplate(int AppPeriodID)
        {
            return new AppraisalTemplateDAL().GetAppraisalTemplate(AppPeriodID);
        }
    }
}
