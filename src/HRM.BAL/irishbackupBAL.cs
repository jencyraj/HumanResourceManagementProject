using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
   public class irishbackupBAL
    {
       public int Save(string ip, string createdby)
       {
         return new irisBackupDAL().Save(ip, createdby);
       }
       public string backup(DataTable dTable, int backupid)
       {
           irisBackupDAL objDAL = new irisBackupDAL();
           return objDAL.backup(dTable, backupid);
       }
       //public string SyncIRISmaster(string IPaddress, string IrisId, string UserName, string Password, string securityId, string Userid)
       //{
       //    irisSyncDAL onjsync = new irisSyncDAL();
       //    return onjsync.SyncIRISmaster(IPaddress, IrisId, UserName, Password, securityId, Userid);

       //}
       //public string SyncIRISDestination(string IPaddress, string IrisId, string UserName, string Password, string securityId, string Userid)
       //{
       //    irisSyncDAL onjsync = new irisSyncDAL();
       //    return onjsync.SyncIRISmaster(IPaddress, IrisId, UserName, Password, securityId, Userid);

       //}
       public DataTable SelectAllMaster(int BranchID)
       {
           irisSyncDAL onjsync = new irisSyncDAL();
           return onjsync.SelectAllMaster(BranchID);
       }
       public void connectMD(DataTable dt, string[] mastrInfo, string[] destinationInfo, string userid,out  string Errmsg)
       {
           irisSyncDAL onjsync = new irisSyncDAL();
           onjsync.connectMD(dt,mastrInfo,destinationInfo, userid,out Errmsg);
       }

    }
}
