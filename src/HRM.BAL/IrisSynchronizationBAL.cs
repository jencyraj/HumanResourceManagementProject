using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.DAL;
using System.Data;
using HRM.BOL;
namespace HRM.BAL
{
   public class IrisSynchronizationBAL
    {
        public int DeleteIrisUser_Synch(string DeviceIp, int User)
        {
            IrisSynchronizationDAL objDAL = new IrisSynchronizationDAL();
            return objDAL.DeleteIrisUser_Synch(DeviceIp, User);
        }
       
        public int Iris_User_SynchSave(IrisSynchronizationBOL objBOL)
        {
            IrisSynchronizationDAL objDAL = new IrisSynchronizationDAL();
            return objDAL.Iris_User_SynchSave(objBOL);
        }
        public DataSet GetCredentials(string MasterIP, string DestinationIP, out int Result)
        {
            IrisSynchronizationDAL objDAL = new IrisSynchronizationDAL();
            return objDAL.GetCredentials(MasterIP, DestinationIP,out Result);
        }
        public void BulkSynch(DataTable dTable, string[] MasterDeviceInfo, string[] DestinationDeviceInfo, out string ErrMsg)
        {
            IrisSynchronizationDAL objDAL = new IrisSynchronizationDAL();
            objDAL.BulkSync(dTable, MasterDeviceInfo, DestinationDeviceInfo, out ErrMsg);
        }
        public DataTable GetDevice_DT(string ipAddress, string securityId, string userName, string password)
        {
            IrisSynchronizationDAL objDAL = new IrisSynchronizationDAL();
            return objDAL.GetDevice_DT(ipAddress, securityId, userName, password);
        }
    }
}
