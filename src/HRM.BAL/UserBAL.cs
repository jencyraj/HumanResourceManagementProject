using System;
using System.Data;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class UserBAL
    {
        public int Save(UserBOL objUser)
        {
            UserDAL objDAL = new UserDAL();
            return objDAL.Save(objUser);
        }

        public int Delete(int UID)
        {
            UserDAL objDAL = new UserDAL();
            return objDAL.Delete(UID);
        }

        public DataTable SelectAll(string UserID)
        {
            UserDAL objDAL = new UserDAL();
            return objDAL.SelectAll(UserID);
        }

        public string SignIn(string sUserID, string sPassword)
        {
            UserDAL objDAL = new UserDAL();
            return objDAL.SignIn(sUserID, sPassword);
        }

        public string SignOut(string sUserID)
        {
            UserDAL objDAL = new UserDAL();
            return objDAL.SignOut(sUserID);
        }

        public DataTable LoginHistory(string sUserID, bool LastRecordOnly,int BranchID)
        {
            return new UserDAL().LoginHistory(sUserID, LastRecordOnly,BranchID);
        }
    }
}
