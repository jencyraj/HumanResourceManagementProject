using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class AppraisalReviewBOL
    {
        //Master

        public int ReviewID;
        public int EAID;
        public int ReviewerID;
        public string Submitted;
        public string Comments;
        public string Status;
        public string CreatedBy;

        //Detail
        public int ReviewDetailID;
        public int CompetencyID;
        public decimal RatingID;
    }
}
