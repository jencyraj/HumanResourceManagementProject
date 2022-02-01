using iCAM70003SDKCLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
    public enum iCAMStatus
    {
        NotConnected = 0,
        Connected
    }

    public partial class iCAMManagerAPI
    {
        public iCAMStatus Status { get; set; }
        public string IPAddress { get; set; }
        public string SecurityID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDefault
        {
            get
            {
                return _IsDefault;
            }

            set
            {
                _IsDefault = value;
            }
        }
        public bool IsConnected
        {
            get
            {
                if (Status == iCAMStatus.NotConnected)
                    return false;
                else
                    return true;
            }

            set
            {
                if (value == false)
                {
                    Status = iCAMStatus.NotConnected;
                }
            }
        }

        private string _ImagesDir;
        private string _DeviceName;        

        private string ConvertByteArrToString(byte[] byData)
        {
            string strData = string.Empty;
            if (byData != null)
            {
                strData = Encoding.Default.GetString(byData);
                if (strData.Contains("\0"))
                {
                    string[] strList = strData.Split('\0');
                    strData = strList[0];
                }
            }
            return strData;
        }

        public iCAMManagerAPI()
        {
            Status = iCAMStatus.NotConnected;
            _ConnectionID = 0;
            _ConnectionOptions = new CONNECTION_OPTIONS();
            _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
            _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;
        }

        public void SetDefault(bool bDefault)
        {
            IsDefault = bDefault;
        }

        public int Connect(out int iConnectionID)
        {
            iConnectionID = 0;
            _DeviceInstance = new iCAM70003SDKC();

            int iResult = _DeviceInstance.Connect(IPAddress, SecurityID, UserName, Password, ref _ConnectionOptions, out _ConnectionID);
            if (iResult != 0)
            {
                this.Status = iCAMStatus.NotConnected;
                this._DeviceInstance = null;
                this.IsDefault = false;

                return iResult;
            }

            iConnectionID = _ConnectionID;
            //_DeviceInstance.GetDeviceSoftWareInfo(out _DeviceInfo);
            _DeviceInstance.GetDeviceName(out _DeviceName);
            _ImagesDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Iris ID\\iCAM_Manager_Sample\\IrisData\\" + IPAddress;
            Directory.CreateDirectory(_ImagesDir);

            Status = iCAMStatus.Connected;
            return ErrorConstants.ICAMSDK_ERROR_NONE;
        }
    
        public int Disconnect()
        {
            if (_DeviceInstance != null)
            {
                int iResult = _DeviceInstance.DisConnect();

                if (iResult == 0)
                {
                    Status = iCAMStatus.NotConnected;
                    _DeviceInstance = null;
                    IsDefault = false;
                }

                return iResult;
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int CancelEnroll()
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.CancelEnrollIris();
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }      
      
        public int CheckDuplicateSingleIrisByImage(EEyeType eye_type, byte[] baIrisData, out int iMatched, out string strUserID, out EEyeType matched_eye, out float fHD)
        {
            iMatched = 0;
            strUserID = "";
            matched_eye = EEyeType.None;
            fHD = 1.0f;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.CheckDuplicateSingleIrisByImage(Constants.FIRST_MATCH,
                                                                       eye_type,
                                                                       baIrisData,
                                                                       out iMatched,
                                                                       out strUserID,
                                                                       out matched_eye,
                                                                       out fHD);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int CheckDuplicateBothIrisByImage(byte[] baLeftEyeData, byte[] baRightEyeData, out int iMatched, out string strLeftIrisUserID, out string strRightIrisUserID, out EEyeType left_iris_matched_eye, out EEyeType right_iris_matched_eye, out float fLeftIrisHD, out float fRightIrisHD)
        {
            iMatched = 0;
            strRightIrisUserID = "";
            strLeftIrisUserID = "";
            left_iris_matched_eye = EEyeType.None;
            right_iris_matched_eye = EEyeType.None;
            fLeftIrisHD = 1.0f;
            fRightIrisHD = 1.0f;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.CheckDuplicateBothIrisByImage(Constants.FIRST_MATCH,
                                                                     baRightEyeData,
                                                                     baLeftEyeData,
                                                                     out iMatched,
                                                                     out strRightIrisUserID,
                                                                     out right_iris_matched_eye,
                                                                     out fRightIrisHD,
                                                                     out strLeftIrisUserID,
                                                                     out left_iris_matched_eye,
                                                                     out fLeftIrisHD);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetUserDataFirst(int iBlockSize, out object objUserList, out int iCount)
        {
            objUserList = null;
            iCount = 0;

            if (_DeviceInstance != null)
            {
                USERFILTERINFO objFilterInfo = new USERFILTERINFO();
                objFilterInfo.BlockSize = iBlockSize;

                return _DeviceInstance.GetUsersFirst(ref objFilterInfo, out objUserList, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetUserDataNext(out object objUserList, out int iCount)
        {
            objUserList = null;
            iCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetUsersNext(out objUserList, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetOperatorLogFirst(int iBlockSize, out object objOperatorLogs, out int iCount)
        {
            objOperatorLogs = null;
            iCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetOperationLogFirst(iBlockSize, null, null, out objOperatorLogs, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetOperatorLogNext(out object objOperatorLogs, out int iCount)
        {
            objOperatorLogs = null;
            iCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetOperationLogNext(out objOperatorLogs, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetIrisData(string strUserID, out IRISINFO right_iris_data, out IRISINFO left_iris_data)
        {
            right_iris_data = new IRISINFO();
            left_iris_data = new IRISINFO();

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetIris(strUserID, out right_iris_data, out left_iris_data);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetIrisCode(string strUserID, out byte[] right_iris_data, out byte[] left_iris_data)
        {
            int iResult;
            IRISINFO right_iris;
            IRISINFO left_iris;

            right_iris_data = null;
            left_iris_data = null;

            if (_DeviceInstance != null)
            {
                iResult = _DeviceInstance.GetIris(strUserID, out right_iris, out left_iris);
                if (iResult == ErrorConstants.ICAMSDK_ERROR_NONE)
                {
                    if (right_iris.LeftRight == EEyeType.RightEye)
                    {
                        byte[] temp_iris = right_iris.IrisCode as byte[];
                        right_iris_data = new byte[temp_iris.Length];
                        temp_iris.CopyTo(right_iris_data, 0);
                    }

                    if (left_iris.LeftRight == EEyeType.LeftEye)
                    {
                        byte[] temp_iris = left_iris.IrisCode as byte[];
                        left_iris_data = new byte[temp_iris.Length];
                        temp_iris.CopyTo(left_iris_data, 0);
                    }
                }

                return iResult;
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetUserData(string strUserID, out USERINFO user_info)
        {
            user_info = new USERINFO();

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetUser(strUserID, out user_info);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetUserCount(out int iUserCount)
        {
            int iResult = 0;
            USERFILTERINFO stUserFilterInfo = default(USERFILTERINFO);

            if (_DeviceInstance != null)
                iResult = _DeviceInstance.GetUserCount(ref stUserFilterInfo, out iUserCount);
            else
            {
                iUserCount = 0;
                iResult = ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
            }

            return iResult;
        }

        public int GetCardData(string strUserID, out CARDINFO objCardData)
        {
            objCardData = new CARDINFO();

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetCard(strUserID, out objCardData);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddUser(USERINFO info)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AddUser(ref info);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AssignDefaultTimeGroup(string strUserID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AssignTimeGroup(strUserID, 0);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddIris(string strUserID, EEyeType eye_type, int iMinQuality, byte[] baRightEyeData, byte[] baLeftEyeData)
        {
            IRISINFO left_iris_info = new IRISINFO();
            IRISINFO right_iris_info = new IRISINFO();

            if (eye_type == EEyeType.LeftEye || eye_type == EEyeType.BothEye)
            {
                left_iris_info.LeftRight = EEyeType.LeftEye;
                left_iris_info.IrisCode = baLeftEyeData;
            }

            if (eye_type == EEyeType.RightEye || eye_type == EEyeType.BothEye)
            {
                right_iris_info.LeftRight = EEyeType.RightEye;
                right_iris_info.IrisCode = baRightEyeData;
            }

            if (_DeviceInstance != null)
            {
                if (eye_type == EEyeType.RightEye)
                    return _DeviceInstance.AddSingleIris(strUserID, eye_type, ref right_iris_info, iMinQuality);
                else if (eye_type == EEyeType.LeftEye)
                    return _DeviceInstance.AddSingleIris(strUserID, eye_type, ref left_iris_info, iMinQuality);
                else
                    return _DeviceInstance.AddBothIris(strUserID, ref right_iris_info, ref left_iris_info, iMinQuality);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddIrisByImage(string strUserID, EEyeType eye_type, EQualityType eQuality, int iMinQuality, byte[] baRightEyeData, byte[] baLeftEyeData)
        {
            if (_DeviceInstance != null)
            {
                if (eye_type == EEyeType.RightEye)
                    return _DeviceInstance.AddSingleIrisByImage(strUserID, eye_type, (object)baRightEyeData, eQuality, iMinQuality);
                else if (eye_type == EEyeType.LeftEye)
                    return _DeviceInstance.AddSingleIrisByImage(strUserID, eye_type, (object)baLeftEyeData, eQuality, iMinQuality);
                else
                    return _DeviceInstance.AddBothIrisByImage(strUserID, (object)baRightEyeData, (object)baLeftEyeData, eQuality, iMinQuality);

            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateIris(string strUserID, EEyeType eye_type, int iMinQuality, byte[] baRightEyeData, byte[] baLeftEyeData)
        {
            IRISINFO left_iris_info = new IRISINFO();
            IRISINFO right_iris_info = new IRISINFO();

            if (eye_type == EEyeType.LeftEye || eye_type == EEyeType.BothEye)
            {
                left_iris_info.LeftRight = EEyeType.LeftEye;
                left_iris_info.IrisCode = baLeftEyeData;
            }

            if (eye_type == EEyeType.RightEye || eye_type == EEyeType.BothEye)
            {
                right_iris_info.LeftRight = EEyeType.RightEye;
                right_iris_info.IrisCode = baRightEyeData;
            }

            if (_DeviceInstance != null)
            {
                if (eye_type == EEyeType.RightEye)
                    return _DeviceInstance.UpdateSingleIris(strUserID, eye_type, ref right_iris_info, iMinQuality);
                else if (eye_type == EEyeType.LeftEye)
                    return _DeviceInstance.UpdateSingleIris(strUserID, eye_type, ref left_iris_info, iMinQuality);
                else
                    return _DeviceInstance.UpdateBothIris(strUserID, ref right_iris_info, ref left_iris_info, iMinQuality);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateIrisByImage(string strUserID, EEyeType eye_type, EQualityType eQuality, int iMinQuality, byte[] baRightEyeData, byte[] baLeftEyeData)
        {
            if (_DeviceInstance != null)
            {
                if (eye_type == EEyeType.RightEye)
                    return _DeviceInstance.UpdateSingleIrisByImage(strUserID, eye_type, (object)baRightEyeData, eQuality, iMinQuality);
                else if (eye_type == EEyeType.LeftEye)
                    return _DeviceInstance.UpdateSingleIrisByImage(strUserID, eye_type, (object)baLeftEyeData, eQuality, iMinQuality);
                else
                    return _DeviceInstance.UpdateBothIrisByImage(strUserID, (object)baRightEyeData, (object)baLeftEyeData, eQuality, iMinQuality);

            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int ModifyUser(string strUserID, USERINFO info)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateUser(strUserID, ref info);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddCard(string strUserID, CARDINFO info)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AddCard(strUserID, ref info);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int ModifyCard(string strUserID, CARDINFO info)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateCard(strUserID, ref info);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteUser(string strUserID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteUser(strUserID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteAllUsers(USERFILTERINFO stUserFilterInfo)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteAllUsers(ref stUserFilterInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteIris(string strUserID, EEyeType eye_type)
        {
            if (_DeviceInstance != null)
            {
                int iResult = _DeviceInstance.DeleteIris(strUserID, eye_type);
                return iResult;
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int EnrollUser(string strUserID, EEyeType eye_type, EQualityType eQuality, int iMinQuality, EReceiveIrisType eIristype, bool bVerificationRequired, bool bUpdateIfExists)
        {
            ENROLLINFO info = new ENROLLINFO();
            info.FakeEyeLevel = Constants.DEFAULT_FAKE_EYE_LEVEL;
            info.CaptureTimeout = Constants.CAPTURE_TIME_OUT;
            info.QualityType = eQuality;
            info.ReceiveIrisType = eIristype;
            info.VerificationRequired = (byte)(bVerificationRequired == true ? 1 : 0);
            info.UpdateIrisExist = (byte)(bUpdateIfExists == true ? 1 : 0);
            info.MinQuality = iMinQuality;

            if (_DeviceInstance != null)
            {
                if (eye_type == EEyeType.BothEye)
                    return _DeviceInstance.EnrollBothIris(strUserID, ref info);
                if (eye_type == EEyeType.RightEye || eye_type == EEyeType.LeftEye)
                    return _DeviceInstance.EnrollSingleIris(strUserID, eye_type, ref info);
                else
                    return ErrorConstants.ICAMSDK_ERROR_IN_EYETYPE;
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int SetiCAMMode(EiCAMMode EiCAMMode, int iIdleTime)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.SetiCAMMode(EiCAMMode, iIdleTime);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteFaceImage(string strUserID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteFaceImage(strUserID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int StartFaceCapture(short shWidth, short shHeight, EFaceImageType eImageType, int iTimeOut)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.StartFaceCapture(shWidth, shHeight, eImageType, iTimeOut);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int FinishFaceCapture(short shWidth, short shHeight, EFaceImageType eType, int iFlashOn)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.FinishFaceCapture(shWidth, shHeight, eType, iFlashOn);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetSDKVersion(out string strVersion)
        {
            strVersion = string.Empty;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetSDKVersion(out strVersion);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int Reboot()
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.Reboot();
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int StartDetectSmartCard(int iTimeOut)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.StartDetectSmartCard(iTimeOut);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int CancelDetectSmartCard()
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.CancelDetectSmartCard();
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateCard(string strUserID, CARDINFO stCardInfo)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateCard(strUserID, ref stCardInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteCard(string strUserID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteCard(strUserID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UnAssignTimeGroup(string strUserID, int iTGID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UnAssignTimeGroup(strUserID, iTGID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AssignTimeGroup(string strUserID, int iTGID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AssignTimeGroup(strUserID, iTGID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsCount(TRANSACTIONFILTERINFO FilterInfo, out int iCount)
        {
            iCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsCount(FilterInfo, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsCountEx(int Filtertype, object FilterInfo, out int iCount)
        {
            iCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsCountEx(Filtertype, FilterInfo, out iCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsFirst(TRANSACTIONFILTERINFO stFilterInfo, out object objTrxnsList, out int iTranLogCount)
        {
            objTrxnsList = null;
            iTranLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsFirst(stFilterInfo, out objTrxnsList, out iTranLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsFirstEx(int iFilterType, object objFilterInfo, out object objTrxnsList, out int iTranLogCount)
        {
            objTrxnsList = null;
            iTranLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsFirstEx(iFilterType, objFilterInfo, out objTrxnsList, out iTranLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsNext(out object objTrxnsList, out int iTranLogCount)
        {
            objTrxnsList = null;
            iTranLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsNext(out objTrxnsList, out iTranLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTransactionsNextEx(out object objTrxnsList, out int iTranLogCount)
        {
            objTrxnsList = null;
            iTranLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTransactionsNextEx(out objTrxnsList, out iTranLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddTimeGroup(TIMEGROUPINFO stTIMEGROUPINFO, out int iTGID)
        {
            iTGID = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AddTimeGroup(ref stTIMEGROUPINFO, out iTGID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateTimeGroup(int iTGID, TIMEGROUPINFO stTIMEGROUPINFO)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateTimeGroup(iTGID, ref stTIMEGROUPINFO);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetSystemLogCount(string strStartDate, string strEndDate, out int iSystemLogCount)
        {
            iSystemLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetSystemLogCount(strStartDate, strEndDate, out iSystemLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetSystemLogFirst(string strStartDate, string strEndDate, out object objSystemlogsList, out int iSystemLogCount)
        {
            iSystemLogCount = 0;
            objSystemlogsList = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetSystemLogFirst(Constants.MAXBLOCK_SIZE, strStartDate, strEndDate, out objSystemlogsList, out iSystemLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetSystemLogNext(out object objSystemlogsList, out int iSystemLogCount)
        {
            iSystemLogCount = 0;
            objSystemlogsList = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetSystemLogNext(out objSystemlogsList, out iSystemLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteOperationLogs(string strStartDate, string strEndDate)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteOperationLogs(strStartDate, strEndDate);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteSystemLogs(string strStartDate, string strEndDate)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteSystemLogs(strStartDate, strEndDate);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteTimeGroup(int iTGID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteTimeGroup(iTGID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteTransactions(TRANSACTIONFILTERINFO stFilterInfo)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteTransactions(stFilterInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteTransactionsEx(int FilterType, object objFilterInfo)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteTransactionsEx(FilterType, objFilterInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetOperationLogCount(string strStartDate, string strEndDate, out int iOperationLogCount)
        {
            iOperationLogCount = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetOperationLogCount(strStartDate, strEndDate, out iOperationLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetOperationLogFirst(string strStartDate, string strEndDate, out object objOperationLogsList, out int iOperationLogCount)
        {
            iOperationLogCount = 0;
            objOperationLogsList = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetOperationLogFirst(Constants.MAXBLOCK_SIZE, strStartDate, strEndDate, out objOperationLogsList, out iOperationLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetOperationLogNext(out object objOperationLogsList, out int iOperationLogCount)
        {
            iOperationLogCount = 0;
            objOperationLogsList = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetOperationLogNext(out objOperationLogsList, out iOperationLogCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateHoliday(int iHID, HOLIDAYINFO stHOLIDAYINFO)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateHoliday(iHID, ref stHOLIDAYINFO);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddHoliday(HOLIDAYINFO stHOLIDAYINFO, out int iHID)
        {
            iHID = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AddHoliday(ref stHOLIDAYINFO, out iHID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteHoliday(int iHID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteHoliday(iHID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetAllTimeGroups(out object objTIMEGROUPINFO, out int iGroupCount)
        {
            iGroupCount = 0;
            objTIMEGROUPINFO = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetAllTimeGroups(out objTIMEGROUPINFO, out iGroupCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetTimeGroup(int iTimeGroupId, out TIMEGROUPINFO objTIMEGROUPINFO)
        {
            objTIMEGROUPINFO = default(TIMEGROUPINFO);

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetTimeGroup(iTimeGroupId, out objTIMEGROUPINFO);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int ChangeLoginDetails(string strOldUserName, string strNewUserName, string strOldPassword, string strNewPassword)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.ChangeLoginDetails(strOldUserName, strNewUserName, strOldPassword, strNewPassword);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetDateTime(out string strDateTime, out int iTimeZoneOffset)
        {
            strDateTime = string.Empty;
            iTimeZoneOffset = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetDateTime(out strDateTime, out iTimeZoneOffset);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetDeviceName(out string strDeviceName)
        {
            strDeviceName = string.Empty;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetDeviceName(out strDeviceName);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetDeviceSoftWareInfo(out DEVICE_SW_INFO stDeviceSWDetails)
        {
            stDeviceSWDetails = default(DEVICE_SW_INFO);

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetDeviceSoftWareInfo(out stDeviceSWDetails);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int SetDateTime(string strDateTime, int iTimeZoneOffset)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.SetDateTime(strDateTime, iTimeZoneOffset);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }
        /*
        ========================================================================
	    Added by Shresthi on 30-April-2015 - For SetRelay method 
        ========================================================================
        */
        public int SetRelay(int iRelayPortNum, int iRelayPortOperation)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.SetRelay(iRelayPortNum, iRelayPortOperation);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int SetDeviceName(string strDeviceName)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.SetDeviceName(strDeviceName);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetAllHolidays(out object objHOLIDAYINFO, out int iHolidayCount)
        {
            iHolidayCount = 0;
            objHOLIDAYINFO = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetAllHolidays(out objHOLIDAYINFO, out iHolidayCount);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetConfig(EConfigType eCfgType, out object objCfgData)
        {
            int lSize = 0;
            objCfgData = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetConfig(eCfgType, out objCfgData, out lSize);
            }
            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int SetConfig(EConfigType eCfgType, object objCfgData, int lCfgDataSize, out int bReboot)
        {
            bReboot = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.SetConfig(eCfgType, objCfgData, lCfgDataSize, out bReboot);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int GetRecogModeSchedule(int iScheduleID, out int iType, out object objScheduleInfo)
        {
            iType = 0;
            objScheduleInfo = null;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.GetRecogModeSchedule(iScheduleID, out iType, out objScheduleInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int AddRecogModeSchedule(int iType, object objScheduleInfo, out int iScheduleID)
        {
            iScheduleID = 0;

            if (_DeviceInstance != null)
            {
                return _DeviceInstance.AddRecogModeSchedule(iType, objScheduleInfo, out iScheduleID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int UpdateRecogModeSchedule(int iType, int iScheduleID, object objScheduleInfo)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.UpdateRecogModeSchedule(iType, iScheduleID, objScheduleInfo);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        public int DeleteRecogModeSchedule(int iType, int iScheduleID)
        {
            if (_DeviceInstance != null)
            {
                return _DeviceInstance.DeleteRecogModeSchedule(iType, iScheduleID);
            }

            return ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE;
        }

        #region Private Data Members

        int _ConnectionID;
        bool _IsDefault = false;
        iCAM70003SDKC _DeviceInstance = null;
        CONNECTION_OPTIONS _ConnectionOptions;

        #endregion
    }
}
