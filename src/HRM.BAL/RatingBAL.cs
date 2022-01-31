using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class RatingBAL
    {
        public int Save(RatingBOL objRating)
        {
            RatingDAL objDAL = new RatingDAL();
            return objDAL.Save(objRating);
        }

        public int Delete(int nRatingID)
        {
            RatingDAL objDAL = new RatingDAL();
            return objDAL.Delete(nRatingID);
        }

        public DataTable SelectAll(int nRatingID)
        {
            RatingDAL objDAL = new RatingDAL();
            return objDAL.SelectAll(nRatingID);
        }
    }
}
