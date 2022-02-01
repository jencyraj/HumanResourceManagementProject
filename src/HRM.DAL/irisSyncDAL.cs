using iCAM70003SDKCLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HRM.BOL;

namespace HRM.DAL
{
   public class irisSyncDAL
    {

       


       public DataTable SelectAllMaster(int BranchID)
       {

           DataAccess objDA = new DataAccess();
           SqlParameter[] objParam = new SqlParameter[1];
           try
           {
               if (BranchID > 0)
               {
                   objParam[0] = new SqlParameter("@BranchID", BranchID);
                   objDA.sqlParam = objParam;
               }

               objDA.sqlCmdText = "hrm_IrisDataMaster_Select";
               return objDA.ExecuteDataSet().Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

        private USERFILTERINFO SetFilter(USERFILTERINFO FilterInfo)
        {
            FilterInfo.BlockSize = Constants.MAXBLOCK_SIZE;

            FilterInfo.FilterType = (int)ETransactionFilter.NO_FILTER;

           

            return FilterInfo;
        }
        public string SyncIRISmaster(string[] Credentials,out  USERINFO objUSERINFO,out CARDINFO objCARDINFO ,out IRISINFO objRIGHTEYE,out IRISINFO objLEFTEYE, string Userid)
        {
          objUSERINFO = new USERINFO();
          objCARDINFO = new CARDINFO();
          objRIGHTEYE = new IRISINFO();
          objLEFTEYE = new IRISINFO();
            int result;
            iCAM70003SDKC _DeviceInstance = IrisConnectivity.connectDevice(Credentials[0], Credentials[1], Credentials[2], Credentials[3], out result);
            if (_DeviceInstance.IsConnected() != 0)
            {
              objUSERINFO=  GetIrisDetails.GetUser(Userid,out  result, _DeviceInstance);
              objCARDINFO=  GetIrisDetails.GetCard(Userid, out result, _DeviceInstance);
              objRIGHTEYE = GetIrisDetails.GetRightIris(Userid, out result, _DeviceInstance);
              objLEFTEYE = GetIrisDetails.GetLeftIris(Userid, out result, _DeviceInstance);
                IrisConnectivity.Disconnect(_DeviceInstance);
                
            }
        
            return result.ToString();

            
        }
        public string SyncIRISDestination(string[] Credentials, USERINFO objUSERINFO, CARDINFO objCARDINFO, IRISINFO objRIGHTEYE, IRISINFO objLEFTEYE, string Userid)
        {
            objUSERINFO = new USERINFO();
            objCARDINFO = new CARDINFO();
            objRIGHTEYE = new IRISINFO();
            objLEFTEYE = new IRISINFO();
            int result;
            iCAM70003SDKC _DeviceInstance = IrisConnectivity.connectDevice(Credentials[0], Credentials[1], Credentials[2], Credentials[3],  out result);
            if (_DeviceInstance.IsConnected() != 0)
                    
            {

                GetIrisDetails.AddUser(out result, objUSERINFO, _DeviceInstance);
                GetIrisDetails.AddCard(Userid, objCARDINFO, out result, _DeviceInstance);
                
                IrisConnectivity.Disconnect(_DeviceInstance);

            }
          

            return result.ToString();


        }
        public void connectMD(DataTable dt,string[] mastrInfo,string [] destinationInfo,string userid,out string Errmsg)
        {
            Errmsg = "";

            try
            {
                USERINFO objUSERINFO = new USERINFO();
                CARDINFO objCARDINFO = new CARDINFO();
                IRISINFO objRIGHTEYE = new IRISINFO();
                IRISINFO objLEFTEYE = new IRISINFO();


                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    string irisId = dt.Rows[i]["IrisId"].ToString();
                    string ipAddress = dt.Rows[i]["IPAddresss"].ToString();
                    string securityId = dt.Rows[i]["SecurityId"].ToString();
                    string userName = dt.Rows[i]["UserName"].ToString();
                    string password = dt.Rows[i]["Password"].ToString();
                    SyncIRISmaster(mastrInfo,out objUSERINFO,out objCARDINFO,out objRIGHTEYE,out objLEFTEYE, userid);
                    SyncIRISDestination(destinationInfo, objUSERINFO, objCARDINFO, objRIGHTEYE, objLEFTEYE, userid);
                }

            }
            catch(Exception ex)
            {
                Errmsg = ex.Message;
            }
            
       
        }
     

    }
}
