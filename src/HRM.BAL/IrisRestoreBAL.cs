using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.DAL;

namespace HRM.BAL
{
   public class IrisRestoreBAL
    {
       public DataTable Select_AllUser()
       {
           IrisRestoreDAL objDAL = new IrisRestoreDAL();
           return objDAL.SelectAll_User();
       }
       public DataTable Select_AllCard()
       {
           IrisRestoreDAL objDAL = new IrisRestoreDAL();
           return objDAL.SelectAll_Card();
       }
       public string Connect_Iris(int BackupID, string ipAddress, string securityId, string userName, string password)
       {
           IrisRestoreDAL objDAL = new IrisRestoreDAL();
           return objDAL.Connect_Iris(BackupID,ipAddress,securityId,userName,password);
       }
     
       public DataTable SelectUserOnBackupID(int BackupID)
       {
           IrisRestoreDAL objDAL = new IrisRestoreDAL();
           return objDAL.SelectUserOnBackupID(BackupID);
       }
       public DataTable show_Backupondevice(string BackupDevice)
       {
           IrisRestoreDAL objDAL = new IrisRestoreDAL();
           return objDAL.show_Backupondevice(BackupDevice);
       }
    }
}
