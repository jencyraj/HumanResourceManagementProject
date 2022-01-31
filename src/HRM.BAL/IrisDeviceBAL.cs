using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using HRM.DAL;

namespace HRM.BAL
{
    public class IrisDeviceBAL
    {
        public int Save(int IrisID, string IPAddress, string SecurityId, string UserName, string Password, string Status, int BranchId,string DoorName,string masterdevice)
        {
           return new IrisDeviceDAL().Save(IrisID, IPAddress, SecurityId, UserName, Password, Status, BranchId,DoorName,masterdevice);
        }

        public int Delete(int IrisID)
        {
            return new IrisDeviceDAL().Delete(IrisID);
        }
        public int Update()
        {
            return new IrisDeviceDAL().Update();
        }

        public DataTable SelectAll()
        {
            return new IrisDeviceDAL().SelectAll(0);
        }


        public DataTable SelectByBranch(int BranchID)
        {
            return new IrisDeviceDAL().SelectAll(BranchID);
        }
    }
}
