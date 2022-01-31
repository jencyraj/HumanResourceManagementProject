using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class EmailBAL
    {
        public int Save(EmailBOL objBOL)
        {
            EmailDAL objDAL = new EmailDAL();
            return objDAL.Save(objBOL);
        }

        public EmailBOL Select()
        {
            EmailDAL objDAL = new EmailDAL();
            return objDAL.Select();
        }
    }
}
