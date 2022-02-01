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
    public class IrisDataDAL
    {
        private iCAMManagerAPI m_DeviceControl;
        HOLIDAYINFO stHolidayInfo;
        iCAM70003SDKC _DeviceInstance = null;
        CONNECTION_OPTIONS _ConnectionOptions;

        public DataTable SelectAll(int BranchID)
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

                objDA.sqlCmdText = "hrm_IrisData_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string SaveHolidays(DataTable dTable, int BranchID)
        {
            try
            {
                //HolidayDAL holidayDAL = new HolidayDAL();

                DataTable dtHolidays = SelectAllHolidays(BranchID);

                foreach (DataRow hrow in dtHolidays.Rows)
                {
                    //Insert into Iris
                    if (hrow["IrisHolidayId"] == DBNull.Value)
                    {
                        foreach (DataRow irow in dTable.Rows)
                        {
                            int iHID;
                            int iRetVal = 0;
                            int _ConnectionID = 0;
                            _DeviceInstance = new iCAM70003SDKC();
                            _ConnectionOptions = new CONNECTION_OPTIONS();
                            _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
                            _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

                            string ipAddress = irow["IPAddresss"].ToString();
                            string securityId = irow["SecurityId"].ToString();
                            string userName = irow["UserName"].ToString();
                            string password = irow["Password"].ToString();

                            int iResult = _DeviceInstance.Connect(ipAddress, securityId, userName, password, ref _ConnectionOptions, out _ConnectionID);
                            if (iResult == 0)
                            {
                                //m_DeviceControl = new iCAMManagerAPI();
                                //m_DeviceControl.IPAddress = "192.168.1.5";
                                //m_DeviceControl.SecurityID = "1111111111111111";
                                //m_DeviceControl.UserName = "iCAM7000";
                                //m_DeviceControl.Password = "iris7000";

                                string description = hrow["Description"].ToString();
                                DateTime dt = Convert.ToDateTime(hrow["Holiday"]);

                                stHolidayInfo = HRM.DAL.IrisUtility.SetHolidayDetails(description, dt.ToString("dd-MMM-yyyy"));
                                iRetVal = _DeviceInstance.AddHoliday(stHolidayInfo, out iHID);
                                int iVal = _DeviceInstance.DisConnect();

                                if (iRetVal != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE || iHID == 0)
                                {
                                    string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                                    return error;
                                }
                                else
                                {
                                    DataAccess objDA = new DataAccess();
                                    var sql = String.Format("update hrm_Holidays set IrisHolidayId = '{0}', IsIrisUpdated = 1 where [HolidayID] = '{1}'", iHID, hrow["HolidayID"]);
                                    objDA.sqlCmdText = sql;
                                    objDA.ExecuteNonQueryInline();
                                }
                            }
                            else
                            {
                                string error = string.Format(@"ErrorCode : 0x{0:X}", iResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iResult, string.Empty) + "." + Constants.TITLE;
                                return error;
                            }
                        }
                    }
                    else if (hrow["IsIrisUpdated"] != DBNull.Value && Convert.ToInt32(hrow["IsIrisUpdated"]) == 0)
                    {
                        //Delete from Iris
                        if (hrow["Status"].ToString() == "N")
                        {
                            foreach (DataRow irow in dTable.Rows)
                            {
                                int iHID;
                                int iRetVal = 0;
                                int _ConnectionID = 0;
                                _DeviceInstance = new iCAM70003SDKC();
                                _ConnectionOptions = new CONNECTION_OPTIONS();
                                _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
                                _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

                                string ipAddress = irow["IPAddresss"].ToString();
                                string securityId = irow["SecurityId"].ToString();
                                string userName = irow["UserName"].ToString();
                                string password = irow["Password"].ToString();

                                int iResult = _DeviceInstance.Connect(ipAddress, securityId, userName, password, ref _ConnectionOptions, out _ConnectionID);
                                if (iResult == 0)
                                {

                                    iHID = Convert.ToInt32(hrow["IrisHolidayId"]);

                                    iRetVal = _DeviceInstance.DeleteHoliday(iHID);
                                    int iVal = _DeviceInstance.DisConnect();

                                    if (iRetVal != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE)
                                    {
                                        string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                                        return error;
                                    }
                                    else
                                    {
                                        DataAccess objDA = new DataAccess();
                                        var sql = String.Format("update hrm_Holidays set IsIrisUpdated = 1 where [HolidayID] = '{0}'", hrow["HolidayID"]);
                                        objDA.sqlCmdText = sql;
                                        objDA.ExecuteNonQueryInline();
                                    }
                                }
                                else
                                {
                                    string error = string.Format(@"ErrorCode : 0x{0:X}", iResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iResult, string.Empty) + "." + Constants.TITLE;
                                    return error;
                                }
                            }
                        }
                        //Update Iris
                        else if (hrow["Status"].ToString() == "Y")
                        {
                            foreach (DataRow irow in dTable.Rows)
                            {
                                int iHID;
                                int iRetVal = 0;
                                int _ConnectionID = 0;
                                _DeviceInstance = new iCAM70003SDKC();
                                _ConnectionOptions = new CONNECTION_OPTIONS();
                                _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
                                _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

                                string ipAddress = irow["IPAddresss"].ToString();
                                string securityId = irow["SecurityId"].ToString();
                                string userName = irow["UserName"].ToString();
                                string password = irow["Password"].ToString();

                                int iResult = _DeviceInstance.Connect(ipAddress, securityId, userName, password, ref _ConnectionOptions, out _ConnectionID);
                                if (iResult == 0)
                                {

                                    iHID = Convert.ToInt32(hrow["IrisHolidayId"]);

                                    string description = hrow["Description"].ToString();
                                    DateTime dt = Convert.ToDateTime(hrow["Holiday"]);

                                    stHolidayInfo = HRM.DAL.IrisUtility.SetHolidayDetails(description, dt.ToString("dd-MMM-yyyy"));
                                    iRetVal = _DeviceInstance.UpdateHoliday(iHID, stHolidayInfo);
                                    int iVal = _DeviceInstance.DisConnect();

                                    if (iRetVal != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE)
                                    {
                                        string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                                        return error;
                                    }
                                    else
                                    {
                                        DataAccess objDA = new DataAccess();
                                        var sql = String.Format("update hrm_Holidays set IsIrisUpdated = 1 where [HolidayID] = '{0}'", hrow["HolidayID"]);
                                        objDA.sqlCmdText = sql;
                                        objDA.ExecuteNonQueryInline();
                                    }
                                }
                                else
                                {
                                    string error = string.Format(@"ErrorCode : 0x{0:X}", iResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iResult, string.Empty) + "." + Constants.TITLE;
                                    return error;
                                }
                            }
                        }

                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                int iVal = _DeviceInstance.DisConnect();
                return ex.Message;
            }
        }

        public DataTable SelectAllHolidays(int BranchID)
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
                objDA.sqlCmdText = "hrm_Holidays_SelectAll";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TRANSACTIONFILTERINFO SetFilter(TRANSACTIONFILTERINFO FilterInfo, string strDate, string endDate)
        {
            string strStartDate = null;
            string strEndDate = null;

            //FilterInfo.ProcessResult = Convert.ToByte(((ComboBoxItem)cmbTLProcessResult.SelectedItem).HiddenValue);

            //FilterInfo.FunctionKey = Convert.ToByte(GetFunctionKeyValue());

            //FilterInfo.bUserID = new byte[Constants.USERID_LENGTH];
            //HRM.DAL.IrisUtility.ConvertStringToByteArr("pass userid here", FilterInfo.bUserID);

            //FilterInfo.bCardID = new byte[Constants.CARDID_LENGTH];
            //HRM.DAL.IrisUtility.ConvertStringToByteArr("pass cardid here", FilterInfo.bCardID);


            FilterInfo.BlockSize = Constants.MAXBLOCK_SIZE;

            FilterInfo.FilterType = (int)ETransactionFilter.NO_FILTER;

            strStartDate = HRM.DAL.IrisUtility.FormatDateTime(strDate, Convert.ToInt32("0"), Convert.ToInt32("0"), Convert.ToInt32("0"));
            strEndDate = HRM.DAL.IrisUtility.FormatDateTime(endDate, Convert.ToInt32("0"), Convert.ToInt32("0"), Convert.ToInt32("0"));
            FilterInfo.bStartDate = new byte[Constants.DATE_SIZE];
            HRM.DAL.IrisUtility.ConvertStringToByteArr(strStartDate, FilterInfo.bStartDate);
            FilterInfo.bEndDate = new byte[Constants.DATE_SIZE];
            HRM.DAL.IrisUtility.ConvertStringToByteArr(strEndDate, FilterInfo.bEndDate);

            //FilterInfo.JobCode = new byte[Constants.JOBCODE_LENGTH];
            //HRM.DAL.IrisUtility.ConvertStringToByteArr("pass jobcode here", FilterInfo.JobCode);

            return FilterInfo;
        }

        public string SaveTransactions(DataTable dTable, string StartDate, string EndDate)
        {
            TRANSACTIONFILTERINFO FilterInfo = default(TRANSACTIONFILTERINFO);
            object objTransactionInfo = null;
            int iTransactionLogCount = 0;
            //int iNextCount = 0;
            int iFirstCount = 0;

            //FilterInfo = ApplyTransactionLogFilter(FilterInfo);
            FilterInfo = SetFilter(FilterInfo, StartDate, EndDate);
            FilterInfo.bIncludeUserName = 1;

            try
            {
                foreach (DataRow irow in dTable.Rows)
                {
                    int iRetVal = 0;
                    int _ConnectionID = 0;
                    _DeviceInstance = new iCAM70003SDKC();
                    _ConnectionOptions = new CONNECTION_OPTIONS();
                    _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
                    _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

                    string irisId = irow["IrisId"].ToString();
                    string ipAddress = irow["IPAddresss"].ToString();
                    string securityId = irow["SecurityId"].ToString();
                    string userName = irow["UserName"].ToString();
                    string password = irow["Password"].ToString();

                    int iResult;
                    if (_DeviceInstance.IsConnected() != 0)
                    {
                        iResult = 0;
                    }
                    else
                    {
                        iResult = _DeviceInstance.Connect(ipAddress, securityId, userName, password, ref _ConnectionOptions, out _ConnectionID);
                    }

                    if (iResult == 0)
                    {
                        iRetVal = _DeviceInstance.GetTransactionsCount(FilterInfo, out iTransactionLogCount);

                        if (iRetVal != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE)
                        {
                            int iVal = _DeviceInstance.DisConnect();
                            string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                            return error;
                        }

                        if (iRetVal == ErrorConstants.IACDB_ERROR_NO_RECORD || iRetVal == ErrorConstants.RECOG70003SDK_ERR_NO_RECORD)
                        {
                            int iVal = _DeviceInstance.DisConnect();
                            string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                            return error;
                        }

                        //if (iRetVal != ErrorConstants.ICAMSDK_ERROR_NONE || objTransactionInfo == null)
                        //{
                        //    string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                        //    return error;
                        //}

                        if (iTransactionLogCount > 0)
                        {
                            iRetVal = _DeviceInstance.GetTransactionsFirst(FilterInfo, out objTransactionInfo, out iFirstCount);

                            if (iRetVal != ErrorConstants.ICAMSDK_ERROR_NONE || objTransactionInfo == null)
                            {
                                int iVal = _DeviceInstance.DisConnect();
                                string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                                return error;
                            }

                            UpdateTransactionLogUI(objTransactionInfo, irisId, StartDate, EndDate, _DeviceInstance);

                            //for (int iTotalCount = iFirstCount; iTotalCount < iTransactionLogCount; )
                            //{
                                //iRetVal = _DeviceInstance.GetTransactionsNext(out objTransactionInfo, out iNextCount);
                                //m_MainForm.Progress.Value = iTotalCount * 10;

                                //if (iRetVal != ErrorConstants.ICAMSDK_ERROR_NONE || objTransactionInfo == null)
                                //{
                                //    string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;

                                //    return error;
                                //}

                                //UpdateTransactionLogUI(objTransactionInfo, irisId, StartDate, EndDate);

                                //if (iRetVal == 0 && objTransactionInfo != null)
                                //{
                                //    SaveTransactionDetails(objTransactionInfo);
                                //}
                            //}
                        }

                        int ival = _DeviceInstance.DisConnect();
                    }
                    else
                    {
                        _DeviceInstance.DisConnect();
                        string error = string.Format(@"ErrorCode : 0x{0:X}", iResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iResult, string.Empty) + "." + Constants.TITLE;
                        return error;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                int ival = _DeviceInstance.DisConnect();
                return ex.Message;
            }
        }

        private void UpdateTransactionLogUI(object objTransLog, string irisId, string startDate, string endDate, iCAM70003SDKC _DeviceInstance)
        {
            try
            {
                TRANSACTIONLOGINFO stTransactionLogInfo = default(TRANSACTIONLOGINFO);
                int k = 0;
                int iSize = Marshal.SizeOf(stTransactionLogInfo);
                byte[,] baTransactionLog = (byte[,])objTransLog;
                byte[] baTransactionLogData = new byte[baTransactionLog.Length];
                Buffer.BlockCopy(baTransactionLog, 0, baTransactionLogData, 0, baTransactionLog.Length);
                int iLen = baTransactionLog.GetLength(0);

                for (int i = 0; i < baTransactionLog.GetLength(0); i++)
                {
                    byte[] baBlockData = HRM.DAL.IrisUtility.GetBinaryData(baTransactionLogData, iSize, k);

                    if (baTransactionLogData != null)
                        stTransactionLogInfo = GetTransactionLogInfoFromBinaryData(baBlockData, _DeviceInstance);

                    SaveTransactionDetails(stTransactionLogInfo, irisId, startDate, endDate, _DeviceInstance);
                    k = k + iSize;
                }
            }
            catch (Exception ex)
            {
                _DeviceInstance.DisConnect();
                throw ex;
            }
        }

        private static TRANSACTIONLOGINFO GetTransactionLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
        {
            try
            {
                TRANSACTIONLOGINFO TransactionLogInfo = default(TRANSACTIONLOGINFO);
                int iSize = Marshal.SizeOf(TransactionLogInfo);
                IntPtr iPtr = Marshal.AllocHGlobal(iSize);
                Marshal.Copy(baData, 0, iPtr, iSize);
                TransactionLogInfo = (TRANSACTIONLOGINFO)Marshal.PtrToStructure(iPtr, typeof(TRANSACTIONLOGINFO));
                Marshal.FreeHGlobal(iPtr);

                return TransactionLogInfo;
            }
            catch (Exception ex)
            {
                _DeviceInstance.DisConnect();
                throw ex;
            }
        }

        private void SaveTransactionDetails(TRANSACTIONLOGINFO stTransactionInfo, string irisId, string startDate, string endDate, iCAM70003SDKC _DeviceInstance)
        {
            try
            {
                string s1 = IrisUtility.ConvertByteArrToString(stTransactionInfo.OccurDate);
                string s2 = IrisUtility.ConvertByteArrToString(stTransactionInfo.DeviceName);
                string s3 = IrisUtility.ConvertByteArrToString(stTransactionInfo.UserID);
                string s4 = IrisUtility.ConvertByteArrToString(stTransactionInfo.CardID);
                string s5 = IrisUtility.ConvertByteArrToString(stTransactionInfo.FirstName);
                string s6 = IrisUtility.ConvertByteArrToString(stTransactionInfo.LastName);
                string s7 = stTransactionInfo.FunctionKey.ToString();
                string s8 = IrisUtility.ConvertByteArrToString(stTransactionInfo.JobCode);
                string s9 = GetTransactionLogKind(Convert.ToInt32(stTransactionInfo.ProcessResult)); //Process Result
                string s10 = stTransactionInfo.HammingDistance.ToString();

                string s11 = "";
                if (stTransactionInfo.LeftRight == (int)EEyeType.BothEye)
                    s11 = "Both";
                else if (stTransactionInfo.LeftRight == (int)EEyeType.RightEye)
                    s11 = "Right";
                else if (stTransactionInfo.LeftRight == (int)EEyeType.LeftEye)
                    s11 = "Left";

                string s12 = IrisUtility.ConvertByteArrToString(stTransactionInfo.Message);

                DataAccess objDA = new DataAccess();

                //var sql = String.Format("delete from hrm_IrisTransactions where OccurDateTime = '{0}' and IrisId ='{1}' and UserId = '{2}' ", s1, endDate, s3);
                //objDA.sqlCmdText = sql;
                //objDA.ExecuteNonQueryInline();

                List<SqlParameter> objParam = new List<SqlParameter>();

                objParam.Add(new SqlParameter("@IrisId", irisId));
                //objParam.Add(new SqlParameter("@StartDate", startDate));
                //objParam.Add(new SqlParameter("@EndDate", endDate));
                objParam.Add(new SqlParameter("@OccurDateTime", s1));
                objParam.Add(new SqlParameter("@DeviceName", s2));
                objParam.Add(new SqlParameter("@UserID", s3));
                objParam.Add(new SqlParameter("@CardID", s4));
                objParam.Add(new SqlParameter("@FirstName", s5));
                objParam.Add(new SqlParameter("@LastName", s6));
                objParam.Add(new SqlParameter("@FunctionKey", s7));
                objParam.Add(new SqlParameter("@JobCode", s8));
                objParam.Add(new SqlParameter("@ProcessResult", s9));
                objParam.Add(new SqlParameter("@HammingDistance", s10));
                objParam.Add(new SqlParameter("@EyeType", s11));
                objParam.Add(new SqlParameter("@CreatedBy", ""));
                objParam.Add(new SqlParameter("@CreateDateTime", ""));
                objParam.Add(new SqlParameter("@Message", s12));
                objDA.sqlCmdText = "hrm_IrisTransactions_Delete_Insert";
                objDA.sqlParam = objParam.ToArray();

                int val = int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                _DeviceInstance.DisConnect();
                throw ex;
            }         
        }

        private TRANSACTIONFILTERINFO ApplyTransactionLogFilter(TRANSACTIONFILTERINFO FilterInfo)
        {
            int iFilterType = 0;

            FilterInfo.BlockSize = Constants.MAXBLOCK_SIZE;

            //Matched user ID incase User is identified/verified.
            //iFilterType = iFilterType | (int)ETransactionFilter.USER_ID;

            //FilterInfo.FilterType = (int)ETransactionFilter.NO_FILTER;

            //Transaction log generated as a result of what (user Identified/not identified..)
            //iFilterType = (int)ETransactionFilter.PROCESS_RESULT;

            //iFilterType = iFilterType | (int)ETransactionFilter.CARD_ID;

            //iFilterType = iFilterType | (int)ETransactionFilter.DATE_RANGE;

            //Contains functionkey pressed during the T&A after recognition Valid Range is 0-6 , 0- Timeout
            //iFilterType = iFilterType | (int)ETransactionFilter.FUNCTION_KEY;

            //iFilterType = iFilterType | (int)ETransactionFilter.JOB_CODE;

            //iFilterType = iFilterType | (int)ETransactionFilter.SET_SERVER_FLAG;

            FilterInfo.FilterType = iFilterType;

            return FilterInfo;
        }

        public string GetTransactionLogKind(int iKind)
        {
            switch (iKind)
            {
                case Constants.TRANSACTIONLOG_ACCEPTED:
                    return "Accepted";
                case Constants.TRANSACTIONLOG_ACCEPTED_INCORRECT_TIME:
                    return "Accepted (Incorrect time)";
                case Constants.TRANSACTIONLOG_ACCEPTED_NORMAL:
                    return "Accepted (Normal)";
                case Constants.TRANSACTIONLOG_ACCEPTED_SUPER_USER:
                    return "Accepted (Super user)";
                case Constants.TRANSACTIONLOG_ACCEPTED_WARNING_EYE:
                    return "Accepted (Warning eye)";
                case Constants.TRANSACTIONLOG_AUTHORIZED_BY_ACCESS_PANEL:
                    return "Authorized by access panel";
                case Constants.TRANSACTIONLOG_DENIED_EXPIRED:
                    return "Denied (Expired)";
                case Constants.TRANSACTIONLOG_DENIED_FAKE_EYE:
                    return "Denied (Fake eye)";
                case Constants.TRANSACTIONLOG_DENIED_INVALID_CARD_ID_IRIS_MATCHED:
                    return "Denied (Invalid Card ID, Iris matched)";
                case Constants.TRANSACTIONLOG_DENIED_IRIS_NOT_ENROLLED:
                    return "Denied (Iris not enrolled)";
                case Constants.TRANSACTIONLOG_DENIED_IRIS_NOT_MATCHED:
                    return "Denied (User ID found, but Iris not matched)";
                case Constants.TRANSACTIONLOG_DENIED_IRIS_NOT_TRIED:
                    return "Denied (User ID found, but Iris not tried)";
                case Constants.TRANSACTIONLOG_DENIED_NO_ACCESS_FOR_DOOR:
                    return "Denied (No access authority for door)";
                case Constants.TRANSACTIONLOG_DENIED_NO_ACCESS_FOR_TIME:
                    return "Denied (No access authority for time)";
                case Constants.TRANSACTIONLOG_DENIED_OR_NOT_VERIFIED:
                    return "Denied or Not Verified";
                case Constants.TRANSACTIONLOG_DENIED_OVER_TIME:
                    return "Denied (Overtime)";
                case Constants.TRANSACTIONLOG_DENIED_PARITY_ERROR:
                    return "Denied (Wiegand format error - Parity error)";
                case Constants.TRANSACTIONLOG_DENIED_USR_ID_NOT_ENROLLED:
                    return "Denied (User ID not enrolled)";
                case Constants.TRANSACTIONLOG_DENIED_WRONG_FACILITY_CODE:
                    return "Denied (Wiegand format error - Wrong facility code)";
                case Constants.TRANSACTIONLOG_DENIED_WRONG_FACILITY_CODE_PARITY_ERROR:
                    return "Denied (Wiegand format error - Wrong facility code and Parity error)";
                case Constants.TRANSACTIONLOG_IDENTIFIED:
                    return "Identified";
                case Constants.TRANSACTIONLOG_NOT_IDENTIFIED_FAKE_EYE:
                    return "Not identified(Fake eye)";
                case Constants.TRANSACTIONLOG_NOT_IDENTIFIED_NOT_ENROLLED:
                    return "Not identified(Not Enrolled)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_FAIL_TO_FIND_CARD_ID:
                    return "Not verified(Fail to find Card ID, But Matched in all DB)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_FAKE_EYE:
                    return "Not verified(Fake eye)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_NO_AUTHORITY_IN_VERIFICATION:
                    return "Not verified(No Authority in Verification)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_NOT_PASSED_LIVE_TEST:
                    return "Not verified(Not Passed the Live Eye Test)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_PASSED_EYE_TEST:
                    return "Not verified(Fail to find Card ID, But Matched in all DB, Not Passed the live Eye Test)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_TIME_OUT_IN_VERIFICATION:
                    return "Not verified(Time Out in Verification)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_FAILED:
                    return "Not verified(Verification failed but matched in all DB)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_FAILED_NOT_PASSED_LIVE_TEST:
                    return "Not verified(Verification failed but matched in all DB, Not Passed the Live Eye Test)";
                case Constants.TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_TIME_OUT:
                    return "Not verified(Fail to find Card ID, Verification Time Out)";
                case Constants.TRANSACTIONLOG_UNAUTHORIZED_BY_ACCESS_PANEL:
                    return "Unauthorized by access panel";
                case Constants.TRANSACTIONLOG_VERIFIED:
                    return "Denied (Invalid Card ID, Iris matched)";
                case Constants.TRANSACTIONLOG_IRIS_NOT_VERIFIED:
                    return "Denied (Iris not verified.)";
                case Constants.TRANSACTIONLOG_DENIED_CARDID_NOT_FOUND:
                    return "Denied (Unknown Card.)";
                default:
                case Constants.TRANSACTIONLOG_UNKNOWN:
                    return "Unknown Transaction Log";

            }
        }
        public DataTable SelectIrisDetails(IrisDataBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];

            int i = -1;

            try
            {
                if (objBOL.BranchId > 0)
                {
                    objParam[++i] = new SqlParameter("@BranchID", objBOL.BranchId);

                }
                if (objBOL.FromDate != "")
                {
                    string[] sDate = objBOL.FromDate.Split('/');
                    try
                    {
                        DateTime dt = DateTime.Parse(sDate[1] + '/' + sDate[0] + '/' + sDate[2]);
                        objParam[++i] = new SqlParameter("@FromDate", dt);
                    }
                    catch
                    {
                    }
                }
                if (objBOL.empname != "")
                    objParam[++i] = new SqlParameter("@empname", objBOL.empname);
                if (objBOL.todate != "")
                {
                    string[] sDate = objBOL.todate.Split('/');
                    try
                    {
                        DateTime dt = DateTime.Parse(sDate[1] + '/' + sDate[0] + '/' + sDate[2]);
                        objParam[++i] = new SqlParameter("@todate", dt);
                    }
                    catch
                    {
                    }
                }

                if (objBOL.EmpCode != "")
                    objParam[++i] = new SqlParameter("@EmpCode", objBOL.EmpCode);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Select_Iris_details";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
