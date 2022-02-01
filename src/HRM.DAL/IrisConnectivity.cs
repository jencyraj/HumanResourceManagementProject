using iCAM70003SDKCLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HRM.DAL
{
   public class IrisConnectivity
    {
        public static iCAM70003SDKC connectDevice(string IPAddresss, string securityId, string userName, string password, out int Result)
        {
            return ConnectDevice(IPAddresss, securityId, userName, password, out   Result);
        }
        public static iCAM70003SDKC ConnectDevice(string IPAddresss, string securityId, string userName, string password, out int Result)
        {

            iCAM70003SDKC _DeviceInstance = new iCAM70003SDKC();
            CONNECTION_OPTIONS _ConnectionOptions;
            int _ConnectionID = 0;
            _DeviceInstance = new iCAM70003SDKC();
            _ConnectionOptions = new CONNECTION_OPTIONS();
            _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
            _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

            
            Result = _DeviceInstance.Connect(IPAddresss, securityId, userName, password, ref _ConnectionOptions, out _ConnectionID);
           
            return _DeviceInstance;
        }
        public static void DisconnectDevice(iCAM70003SDKC _DeviceInstance)
        {
            _DeviceInstance.DisConnect();
        }
        public static void Disconnect(iCAM70003SDKC _DeviceInstance)
        {
            _DeviceInstance.DisConnect();
        }
    }
}
