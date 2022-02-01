using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iCAM70003SDKCLib;
using System.Runtime.InteropServices;
 

using HRM.BOL;

namespace HRM.DAL
{
    public class IrisSynchronizationDAL
    {

        public int DeleteIrisUser_Synch(string DeviceIP, int user)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@DeviceIP", DeviceIP));
                objParam.Add(new SqlParameter("@CreatedBy", user));
                objDA.sqlCmdText = "hrm_Iris_DeleteIrisUser_Synch";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCredentials(string MasterIP, string DestinationIP, out int Result)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            int i = -1;

            try
            {
                objParam.Add(new SqlParameter("@MasterIP", MasterIP));
                objParam.Add(new SqlParameter("@DestinationIP", DestinationIP));
                objParam.Add(new SqlParameter("@Result", SqlDbType.Int));
                objParam[2].Direction = ParameterDirection.Output;
                objDA.sqlCmdText = "[hrm_Iris_GetCredentials]";
                objDA.sqlParam = objParam.ToArray();
                Result = int.Parse("" + objParam[2].Value);
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int Iris_User_SynchSave(IrisSynchronizationBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            
            try
            {
                objParam.Add(new SqlParameter("@UserID", objBOL.UserId));
                objParam.Add(new SqlParameter("@FirstName", objBOL.First));
                objParam.Add(new SqlParameter("@LastName", objBOL.Last));
                objParam.Add(new SqlParameter("@StartDateTime", objBOL.Start));
                objParam.Add(new SqlParameter("@EndDateTime", objBOL.End));
                objParam.Add(new SqlParameter("@UserKind", objBOL.Userkind));
                objParam.Add(new SqlParameter("@TimeGroupIDList", ""));
                objParam.Add(new SqlParameter("@WarningEye", objBOL.Userkind));
                objParam.Add(new SqlParameter("@PIN", objBOL.Eye));
                objParam.Add(new SqlParameter("@DeviceIP", objBOL.Eye));
                objParam.Add(new SqlParameter("@CreatedBy", objBOL.Userkind));

                objDA.sqlCmdText = "[hrm_iris_UserSynch_Insert_Update]";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable getMaster(USERINFO[] USERIFOS)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("UserID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("PIN");
            dt.Columns.Add("TimeGroupIDList");
            dt.Columns.Add("EUserKind");
            dt.Columns.Add("EEyeType ");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("ExpireDate");
            dt.Columns.Add("VisitorStartTime ");
            dt.Columns.Add("VisitorEndTime ");

            for (int i = 0; i < USERIFOS.Length; i++)
            {
                dt.Rows.Add(USERIFOS[i].UserID, USERIFOS[i].FirstName, USERIFOS[i].LastName, USERIFOS[i].Pin, USERIFOS[i].TimeGroupIDList, USERIFOS[i].EUserKind, USERIFOS[i].WhichEye, USERIFOS[i].StartDate, USERIFOS[i].ExpireDate, USERIFOS[i].VisitorStartTime, USERIFOS[i].VisitorEndTime);
            }

            return dt;
        }
        public DataTable IrisSynchronize_UserMaster(iCAM70003SDKC _DeviceInstance)
        {
            USERFILTERINFO USERFILTERINFO = GetIrisDetails.Set_UserFilter();
            int Result;
            object objUSERINFO = GetIrisDetails.GetUserFirst(out Result, _DeviceInstance);
            DataTable dtResult = getMaster((USERINFO[])objUSERINFO);
            return dtResult;
           
        }
        public static iCAM70003SDKC ConnectDevice(string ipAddress, string securityId, string userName, string password)
        {
            int Result;
            iCAM70003SDKC _DeviceInstance = IrisConnectivity.ConnectDevice(ipAddress, securityId, userName, password, out Result);
            return _DeviceInstance;
        }
        public void DisConnectDevice(iCAM70003SDKC _DeviceInstance)
        {
            IrisConnectivity.DisconnectDevice(_DeviceInstance);
        }
        public void BulkSync(DataTable dTable, string[] MasterDeviceInfo, string[] DestinationDeviceInfo, out string ErrMsg)
        {
            ErrMsg = "";

            try
            {
                USERINFO objUser = new USERINFO();
                CARDINFO objcard = new CARDINFO();
                IRISINFO objRIGHTIRISINFO = new IRISINFO();
                IRISINFO objLEFTIRISINFO = new IRISINFO();

                for (int k = 0; k < dTable.Rows.Count; k++)
                {
                    string UserId = "" + dTable.Rows[k]["UserId"];

                    Get_Synch_Master(MasterDeviceInfo, UserId, out objUser, out objcard, out objRIGHTIRISINFO, out objLEFTIRISINFO);
                    Get_Synch_Destination(DestinationDeviceInfo,UserId, objUser, objcard, objRIGHTIRISINFO, objLEFTIRISINFO);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
        }
        public DataTable GetDevice_DT(string ipAddress, string securityId, string userName, string password)
        {
            DataTable dtMasterData = null;
            try
            {
               
                int Result;
                iCAM70003SDKC _DeviceInstance = ConnectDevice(ipAddress, securityId, userName, password);
                if (_DeviceInstance.IsConnected() == 0)
                {

                    dtMasterData = IrisSynchronize_UserMaster(_DeviceInstance);
                }

                DisConnectDevice(_DeviceInstance);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dtMasterData;
        }
        public void Get_Synch_Master(string[] Credentials, string UserId, out USERINFO USERINFO, out  CARDINFO CARDINFO, out  IRISINFO RIGHTIRISINFO, out  IRISINFO LEFTIRISINFO)
        {
            int Result;

            USERINFO = new USERINFO();
            CARDINFO = new CARDINFO();
            RIGHTIRISINFO = new IRISINFO();
            LEFTIRISINFO = new IRISINFO();

            iCAM70003SDKC _DeviceInstance = ConnectDevice(Credentials[0], Credentials[1], Credentials[2], Credentials[3]);
            if (_DeviceInstance.IsConnected() == 0)
            {

                USERINFO = GetIrisDetails.GetUser(UserId, out Result, _DeviceInstance);
                CARDINFO = GetIrisDetails.GetCard(UserId, out Result, _DeviceInstance);
                RIGHTIRISINFO = GetIrisDetails.GetRightIris(UserId, out Result, _DeviceInstance);
                LEFTIRISINFO = GetIrisDetails.GetLeftIris(UserId, out Result, _DeviceInstance);
                DisConnectDevice(_DeviceInstance);
            }

        }
        public void Get_Synch_Destination(string[] Credentials, string UserId, USERINFO USERINFO, CARDINFO CARDINFO, IRISINFO RIGHTIRISINFO, IRISINFO LEFTIRISINFO)
        {
            int iIRC;
            int bIrisCodeQuality = 70;
            iCAM70003SDKC _DeviceInstance = ConnectDevice(Credentials[0], Credentials[1], Credentials[2], Credentials[3]);
            if (_DeviceInstance.IsConnected() == 0)
            {
                GetIrisDetails.AddUser(out iIRC, USERINFO, _DeviceInstance);
                GetIrisDetails.AddCard(UserId, CARDINFO, out iIRC, _DeviceInstance);

                GetIrisDetails.AddSingleIris(UserId, EEyeType.RightEye, RIGHTIRISINFO, bIrisCodeQuality, out iIRC, _DeviceInstance);
                GetIrisDetails.AddSingleIris(UserId, EEyeType.LeftEye, LEFTIRISINFO, bIrisCodeQuality, out iIRC, _DeviceInstance);
                GetIrisDetails.AddBothIris(UserId, RIGHTIRISINFO, LEFTIRISINFO, bIrisCodeQuality, out iIRC, _DeviceInstance);

                DisConnectDevice(_DeviceInstance);
            }

        }

    }
}
