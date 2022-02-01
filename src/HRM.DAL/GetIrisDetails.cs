using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iCAM70003SDKCLib;
namespace HRM.DAL
{
  public  class GetIrisDetails
    {   iCAM70003SDKC _DeviceInstance = null;
        public static USERINFO SetUserDetails(string strFirstName, string strLastName, string strUserID, string strPIN, EUserKind eUserKind, byte[] baTimeGroupIDList, string strStartDate, string strExpireDate, string strVisitorStartTime, string strVisitorEndTime)
        {
            USERINFO stUserInfo = new USERINFO();
            if (!string.IsNullOrEmpty(strFirstName))
            {
                stUserInfo.FirstName = new byte[HRM.DAL.Constants.FIRSTNAME_LENGTH];
                IrisUtility.ConvertStringToByteArr(strFirstName, stUserInfo.FirstName);
            }
            if (!string.IsNullOrEmpty(strLastName))
            {
                stUserInfo.LastName = new byte[HRM.DAL.Constants.LASTNAME_LENGTH];
                IrisUtility.ConvertStringToByteArr(strLastName, stUserInfo.LastName);
            }
            if (!string.IsNullOrEmpty(strUserID))
            {
                stUserInfo.UserID = new byte[HRM.DAL.Constants.USERID_LENGTH];
                IrisUtility.ConvertStringToByteArr(strUserID, stUserInfo.UserID);
            }
            if (baTimeGroupIDList != null)
            {
                stUserInfo.TimeGroupIDList = new byte[HRM.DAL.Constants.TIMEGROUP_ID_LIST_LENGTH];
                stUserInfo.TimeGroupIDList = baTimeGroupIDList;
            }
            else
            {
                stUserInfo.TimeGroupIDList = null;
            }
            if (strStartDate != null)
            {
                stUserInfo.StartDate = new byte[HRM.DAL.Constants.DATE_SIZE];
                IrisUtility.ConvertStringToByteArr(strStartDate, stUserInfo.StartDate);
            }
            if (strExpireDate != null)
            {
                stUserInfo.ExpireDate = new byte[HRM.DAL.Constants.DATE_SIZE];
                IrisUtility.ConvertStringToByteArr(strExpireDate, stUserInfo.ExpireDate);
            }
            if (strVisitorStartTime != null)
            {
                stUserInfo.VisitorStartTime = new byte[HRM.DAL.Constants.TIME_SIZE];
                IrisUtility.ConvertStringToByteArr(strVisitorStartTime, stUserInfo.VisitorStartTime);
            }
            if (strVisitorEndTime != null)
            {
                stUserInfo.VisitorEndTime = new byte[HRM.DAL.Constants.TIME_SIZE];
                IrisUtility.ConvertStringToByteArr(strVisitorEndTime, stUserInfo.VisitorEndTime);
            }
            if (!string.IsNullOrEmpty((strPIN)))
                stUserInfo.Pin = Convert.ToInt32(strPIN);

            stUserInfo.EUserKind = eUserKind;

            return stUserInfo;
        }

        /// <summary>
        /// Description : SetCardDetails , Forms the CardInfo Structure
        /// Return type	: static CARDINFO
        /// Arguments	: string strCardID, string strCardNumber, int iCardKind
        /// </summary>
        public static CARDINFO SetCardDetails(string strCardID, string strCardNumber, ECardKind eCardKind)
        {
            CARDINFO stCardInfo = new CARDINFO();

            if (!string.IsNullOrEmpty(strCardID))
            {
                stCardInfo.CardID = new byte[HRM.DAL.Constants.CARDID_LENGTH];
                IrisUtility.ConvertStringToByteArr(strCardID, stCardInfo.CardID);
            }
            if (!string.IsNullOrEmpty(strCardNumber))
            {
                stCardInfo.CardNumber = new byte[HRM.DAL.Constants.CARDNUMBER_LENGTH];
                IrisUtility.ConvertStringToByteArr(strCardNumber, stCardInfo.CardNumber);
            }

            stCardInfo.ECardKind = eCardKind;

            return stCardInfo;

        }

        /// <summary>
        /// Description : SetIrisDetails , Forms the IrisInfo Structure
        /// Return type	: static IRISINFO
        /// Arguments	: byte[] baIrisCode, byte bIrisCodeType, byte bIrisCodeVersion,byte bEyeType
        /// </summary>
        public static IRISINFO SetIrisDetails(byte[] baIrisCode, byte bIrisCodeType, byte bIrisCodeVersion, EEyeType eEyeType, int iLength)
        {
            IRISINFO stIrisInfo = new IRISINFO();
            if (baIrisCode == null)
            {
                stIrisInfo.IrisCode = null;
            }
            else
            {
                stIrisInfo.IrisCode = new byte[HRM.DAL.Constants.IRISCODE_LENGTH];
                stIrisInfo.IrisCode = baIrisCode;
            }
            stIrisInfo.IrisCodeVersion = bIrisCodeVersion;
            stIrisInfo.LeftRight = eEyeType;
            return stIrisInfo;
        }


        /// <summary>
        /// Description : SetTimeGroupDetails , Forms the TIMEGROUPINFO Structure
        /// Return type	: static TIMEGROUPINFO
        /// Arguments	: string strName, string strDescription, byte[] baTimeInfo
        /// </summary>
        public static TIMEGROUPINFO SetTimeGroupDetails(string strName, TIMEINFO[] baTimeInfo)
        {
            TIMEGROUPINFO stTimeGroupInfo = new TIMEGROUPINFO();
            if (!string.IsNullOrEmpty(strName))
            {
                stTimeGroupInfo.Name = new byte[HRM.DAL.Constants.NAME_SIZE];
                IrisUtility.ConvertStringToByteArr(strName, stTimeGroupInfo.Name);
            }
            if (baTimeInfo == null)
            {
                stTimeGroupInfo.TIMEINFO = null;
            }
            else
            {
                stTimeGroupInfo.TIMEINFO = new TIMEINFO[HRM.DAL.Constants.DAY_NUM];
                stTimeGroupInfo.TIMEINFO = baTimeInfo;
            }

            return stTimeGroupInfo;
        }
        public static USERFILTERINFO Set_UserFilter()
        {
            USERFILTERINFO FilterInfo = new USERFILTERINFO();
            FilterInfo.BlockSize = Constants.MAXBLOCK_SIZE;
            FilterInfo.FilterType = (int)ETransactionFilter.NO_FILTER;
            return FilterInfo;

        }
        public static object GetUserFirst(out int Result, iCAM70003SDKC _DeviceInstance)  
      {
            USERFILTERINFO FILTERINFO = new USERFILTERINFO();
            object USERINFOS=null;
            int Number=0;
            Result = _DeviceInstance.GetUsersFirst(FILTERINFO, out USERINFOS, out Number);

            return USERINFOS;
            
      }
       
        public static USERINFO GetUser(string UserId, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            USERINFO USERINFO=new USERINFO();
            Result = _DeviceInstance.GetUser(UserId,out USERINFO);

            return USERINFO;

        }
        public static USERFILTERINFO DeleteAllUsers( USERFILTERINFO FILTERINFO,out int Result, iCAM70003SDKC _DeviceInstance)
        {
            Result = _DeviceInstance.DeleteAllUsers(FILTERINFO);

            return FILTERINFO;

        }
        public static CARDINFO DeleteCard(string UserId,out int Result, iCAM70003SDKC _DeviceInstance)
        {

            CARDINFO CARDINFO = new CARDINFO();

            Result = _DeviceInstance.DeleteCard(UserId);

            return CARDINFO;

        }
        public static USERINFO AddUser(out int Result,USERINFO USERINFO, iCAM70003SDKC _DeviceInstance)
        {
            Result = _DeviceInstance.AddUser(USERINFO);

            return USERINFO;

        }
        public static CARDINFO AddCard(string UserId,CARDINFO CARDINFO , out int Result, iCAM70003SDKC _DeviceInstance)
        {
            
            Result = _DeviceInstance.AddCard(UserId, CARDINFO);

            return CARDINFO;

        }
        public static CARDINFO GetCard(string UserId, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            CARDINFO CARDINFO = new CARDINFO();
            Result = _DeviceInstance.GetCard(UserId,out CARDINFO);

            return CARDINFO;

        }
        public static IRISINFO AddSingleIris(string UserId, EEyeType eeyeType, IRISINFO IRISINFO, int bIrisCodeQuality, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            
            Result = _DeviceInstance.AddSingleIris(UserId, eeyeType, IRISINFO, bIrisCodeQuality);

            return IRISINFO;

        }
        public static IRISINFO AddBothIris(string UserId, IRISINFO stRightIRISINFO,IRISINFO stLeftIRISINFO, int bIrisCodeQuality, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            IRISINFO objIRISINFO = new IRISINFO();
            Result = _DeviceInstance.AddBothIris(UserId, stRightIRISINFO, stLeftIRISINFO, bIrisCodeQuality);

            return objIRISINFO;

        }
        public static IRISINFO DeleteIris(string UserId, EEyeType eeyeType, out int Result, iCAM70003SDKC _DeviceInstance)
        {
           
            IRISINFO IRISINFO = new IRISINFO();
            Result = _DeviceInstance.DeleteIris(UserId, eeyeType);
            return IRISINFO;
          
        }
        public static TIMEGROUPINFO DeleteTimeGroup(int TGID, out int Result, iCAM70003SDKC _DeviceInstance)
        {
           
            TIMEGROUPINFO TIMEGROUPINFO = new TIMEGROUPINFO();
            Result = _DeviceInstance.DeleteTimeGroup(TGID);

            return TIMEGROUPINFO;

        }
        public static TIMEGROUPINFO AddTimeGroup(int TGID, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            TIMEGROUPINFO TIMEGROUPINFO = new TIMEGROUPINFO();
            Result = _DeviceInstance.AddTimeGroup(TIMEGROUPINFO, out TGID);

            return TIMEGROUPINFO;

        }
        public static TIMEGROUPINFO GetTimeGroup(int TGID, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            TIMEGROUPINFO TIMEGROUPINFO = new TIMEGROUPINFO();
            Result = _DeviceInstance.GetTimeGroup(TGID,out TIMEGROUPINFO);

            return TIMEGROUPINFO;

        }

        public static object GetAllTimeGroup(out int Result, iCAM70003SDKC _DeviceInstance)
        {
          
            object tIMEGROUPINFO = null;
            int GroupCount = 0;
            Result = _DeviceInstance.GetAllTimeGroups(out tIMEGROUPINFO, out GroupCount);
            return tIMEGROUPINFO;

        }
        public static IRISINFO GetRightIris(string UserId, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            IRISINFO LeftIrisInfo = new IRISINFO();
            IRISINFO RightIrisInfo = new IRISINFO();
            Result = _DeviceInstance.GetIris(UserId, out RightIrisInfo, out LeftIrisInfo);

            return RightIrisInfo;

        }
        public static IRISINFO GetLeftIris(string UserId, out int Result, iCAM70003SDKC _DeviceInstance)
        {
            IRISINFO RightIrisInfoS = new IRISINFO();
            IRISINFO LeftIrisInfoS = new IRISINFO();
            Result = _DeviceInstance.GetIris(UserId, out RightIrisInfoS, out LeftIrisInfoS);

            return LeftIrisInfoS;

        }
      ///////////////////////
        public TRANSACTIONFILTERINFO SetFilter(TRANSACTIONFILTERINFO FilterInfo, string strDate, string endDate)
        {
            string strStartDate = null;
            string strEndDate = null;




            FilterInfo.BlockSize = Constants.MAXBLOCK_SIZE;

            FilterInfo.FilterType = (int)ETransactionFilter.NO_FILTER;

            strStartDate = HRM.DAL.IrisUtility.FormatDateTime(strDate, Convert.ToInt32("0"), Convert.ToInt32("0"), Convert.ToInt32("0"));
            strEndDate = HRM.DAL.IrisUtility.FormatDateTime(endDate, Convert.ToInt32("0"), Convert.ToInt32("0"), Convert.ToInt32("0"));
            FilterInfo.bStartDate = new byte[Constants.DATE_SIZE];
            HRM.DAL.IrisUtility.ConvertStringToByteArr(strStartDate, FilterInfo.bStartDate);
            FilterInfo.bEndDate = new byte[Constants.DATE_SIZE];
            HRM.DAL.IrisUtility.ConvertStringToByteArr(strEndDate, FilterInfo.bEndDate);



            return FilterInfo;
        }
        public static object GetTransactionCount(out int translogcount, iCAM70003SDKC _DeviceInstance)
        {
            // int translogcount = 0;
            TRANSACTIONFILTERINFO transfiltrinfo = new TRANSACTIONFILTERINFO();
            translogcount = _DeviceInstance.GetTransactionsCount(transfiltrinfo, out translogcount);
            return translogcount;

        }
        public static object GetTransactionFirst(out int result, iCAM70003SDKC _DeviceInstance)
        {

            int translogcount;
            object translist;
            TRANSACTIONFILTERINFO transfiltrinfo = new TRANSACTIONFILTERINFO();
            result = _DeviceInstance.GetTransactionsFirst(transfiltrinfo, out translist, out translogcount);
            return result;


        }
        public static object GetSystemlogFirst(out int result, iCAM70003SDKC _DeviceInstance)
        {

            int Syslogcount = 0;
            object sysinfo;
            string strStartDate = null;
            string strEndDate = null;

            result = _DeviceInstance.GetSystemLogFirst(Constants.MAXBLOCK_SIZE, strStartDate, strEndDate, out sysinfo, out Syslogcount);
            return result;


        }
    }
}
