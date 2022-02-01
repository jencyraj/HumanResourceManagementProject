using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iCAM70003SDKCLib;
namespace HRM.DAL
{
    public class IrisRestoreDAL
    {
        
        public DataTable SelectAll_User()
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;

                objDA.sqlCmdText = "[hrm_Iris_UserInfo]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectAll_Card()
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;

                objDA.sqlCmdText = "[hrm_Iris_CardInfo]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectTimeGroupOnBackupID(int BackupDevice)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;
                objParam[0] = new SqlParameter("@BackupDevice", BackupDevice);
                objDA.sqlCmdText = "[hrm_Iris_SelectTimeGroupOnBackupID]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable show_Backupondevice(string BackupDevice)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;
                objParam[0] = new SqlParameter("@BackupDevice", BackupDevice);
                objDA.sqlCmdText = "[hrm_Iris_ShowBackuponDevice]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectUserOnBackupID(int BackupID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;
                objParam[0] = new SqlParameter("@BackUpID", BackupID);
                objDA.sqlCmdText = "[hrm_Iris_SelectUserOnBackupID]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectIrisCodeOnBackupID(int BackupID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {

                objDA.sqlParam = objParam;
                objParam[0] = new SqlParameter("@BackUpID", BackupID);
                objDA.sqlCmdText = "[hrm_Iris_SelectIrisCodeonBackupID]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public string Connect_Iris(int BackupID, string ipAddress, string securityId, string userName, string password)
        {
            int Result;

            iCAM70003SDKC _DeviceInstance = IrisConnectivity.ConnectDevice(ipAddress, securityId, userName, password, out Result);
            if (Result == 0)
            {
                Restore_User(BackupID, _DeviceInstance);
                Restore_IRIS_Code(BackupID, _DeviceInstance);
                Restore_IRIS_TimeGroupInfo(BackupID, _DeviceInstance);
                IrisConnectivity.DisconnectDevice(_DeviceInstance);
            }
            return "";

        }
        public string Restore_User(int BackupID, iCAM70003SDKC _DeviceInstance)
        {
            int iRUS;
            USERFILTERINFO FilterInfo = GetIrisDetails.Set_UserFilter();
            DataTable DTUsers = SelectUserOnBackupID(BackupID);
            GetIrisDetails.DeleteAllUsers(FilterInfo, out iRUS, _DeviceInstance);

            if (iRUS == 0)
            {

                for (int i = 0; i < DTUsers.Rows.Count; i++)
                {

                    DataRow row2 = DTUsers.Rows[i];
                    string strUserID = row2["UserID"].ToString();
                    string strFirstName = row2["FirstName"].ToString();
                    string strLastName = row2["LastName"].ToString();
                    string strPIN = row2["PIN"].ToString();
                    byte[] baTimeGroupIDList = Encoding.ASCII.GetBytes("" + row2["TimeGroupIDList"]);

                    int usrkind = 0;
                    if ("" + row2["userKind"] == "" + EUserKind.CommonUser)
                        usrkind = 1;
                    else
                        usrkind = 2;

                    string strStartDate = row2["StartDateTime"].ToString();
                    string strExpireDate = "";
                    string strVisitorStartTime = "";
                    string strVisitorEndTime = "";
                    string CardID = "" + row2["CardID"];
                    string CardNumber = "" + row2["CardNumber"];

                    int CardKind = 0;
                    if ("" + row2["CardKind"] == "" + ECardKind.ProxCard)
                        CardKind = 1;
                    else
                        CardKind = 2;
                    GetIrisDetails.DeleteCard(strUserID, out iRUS, _DeviceInstance);

                    if (iRUS == 0)
                    {
                        CARDINFO objCARDINF = GetIrisDetails.SetCardDetails(CardID, CardNumber, (CardKind == 1) ? ECardKind.ProxCard : ECardKind.SmartCard);
                        GetIrisDetails.AddCard(strUserID, objCARDINF, out iRUS, _DeviceInstance);
                    }
                    USERINFO objUSERINFO = GetIrisDetails.SetUserDetails(strFirstName, strLastName, strUserID, strPIN, (usrkind == 1) ? EUserKind.CommonUser : EUserKind.Visitor, baTimeGroupIDList, strStartDate, strExpireDate, strVisitorStartTime, strVisitorEndTime);
                    GetIrisDetails.AddUser(out iRUS, objUSERINFO, _DeviceInstance);

                }
            }




            return "";
        }
        public int GetSelectedEye(int strSelectedEye)
        {
            int iEyeType = 0;

            if (strSelectedEye == 1)
                iEyeType = (int)EEyeType.RightEye;
            else if (strSelectedEye == 2)
                iEyeType = (int)EEyeType.LeftEye;
            else if (strSelectedEye == 3)
                iEyeType = (int)EEyeType.BothEye;
            else if (strSelectedEye == 4)
                iEyeType = (int)EEyeType.EitherEye;
            return iEyeType;

        }
        public string Restore_IRIS_Code(int BackupID, iCAM70003SDKC _DeviceInstance)
        {
            int iIRC; int iEyeType;
            DataTable DTUsers = SelectIrisCodeOnBackupID(BackupID);
            for (int i = 0; i < DTUsers.Rows.Count; i++)
            {

                DataRow row2 = DTUsers.Rows[i];
                string strUserID = row2["UserID"].ToString();
                iEyeType = Util.ToInt(row2["LeftRight"].ToString());
                iEyeType = GetSelectedEye(iEyeType);
                int EETY;

                if ("" + row2["LeftRight"] == "" + EEyeType.RightEye)
                    EETY =1;
                else if ("" + row2["LeftRight"] == "" + EEyeType.LeftEye)
                    EETY = 2;
                else if ("" + row2["LeftRight"] == "" + EEyeType.BothEye)
                    EETY = 3;
                else if ("" + row2["LeftRight"] == "" + EEyeType.EitherEye)
                    EETY = 4;
                else
                    EETY =0;

                GetIrisDetails.DeleteIris(strUserID, (EEyeType)iEyeType, out iIRC, _DeviceInstance);
                if (iIRC == 0)
                {
                    byte[] baIrisCode = Encoding.ASCII.GetBytes("" + row2["IrisCode"]);
                    byte bIrisCodeType = Convert.ToByte("" + row2["IrisCodeType"]);
                    byte bIrisCodeVersion = Convert.ToByte("" + row2["IrisCodeVersion"]);
                    int bIrisCodeQuality = 70; int Length = 512;
                    IRISINFO stLeftIrisInfo = GetIrisDetails.SetIrisDetails(baIrisCode, bIrisCodeType, bIrisCodeVersion, EEyeType.LeftEye, Length);
                    IRISINFO stRightIrisInfo = GetIrisDetails.SetIrisDetails(baIrisCode, bIrisCodeType, bIrisCodeVersion, EEyeType.RightEye, Length);
                    IRISINFO IrisInfo = GetIrisDetails.SetIrisDetails(baIrisCode, bIrisCodeType, bIrisCodeVersion, (EEyeType)EETY, Length);
                    GetIrisDetails.AddSingleIris(strUserID, (EETY == 1) ? EEyeType.RightEye : EEyeType.LeftEye, IrisInfo, bIrisCodeQuality, out iIRC, _DeviceInstance);
                    GetIrisDetails.AddBothIris(strUserID, stRightIrisInfo, stLeftIrisInfo, bIrisCodeQuality, out iIRC, _DeviceInstance);

                }
            }

            return "";
        }
        public string Restore_IRIS_TimeGroupInfo(int BackupID, iCAM70003SDKC _DeviceInstance)
        {
            int iTGI;
            TIMEGROUPINFO stTimeGroupinfo;
            stTimeGroupinfo.TIMEINFO = new TIMEINFO[Constants.DAY_NUM];
            DataTable DTUsers = SelectTimeGroupOnBackupID(BackupID);
            for (int i = 0; i < DTUsers.Rows.Count; i++)
            {

                DataRow row2 = DTUsers.Rows[i];
                int strTGID = Util.ToInt(row2["TimeGroupId"].ToString());
                string strName = row2["Name"].ToString();
                GetIrisDetails.DeleteTimeGroup(strTGID, out iTGI, _DeviceInstance);
                if (iTGI == 0)
                {
                    TIMEGROUPINFO TIMEGROUPINFO = GetIrisDetails.SetTimeGroupDetails(strName, stTimeGroupinfo.TIMEINFO);
                    GetIrisDetails.AddTimeGroup(strTGID, out iTGI, _DeviceInstance);

                }

            }

            return "";
        }
        
      
       


        
    }
}

