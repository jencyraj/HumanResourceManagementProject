using iCAM70003SDKCLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HRM.DAL
{
    public class IrisUtility
    {
            /// <summary>
            /// Description : SetUserDetails , Forms the UserInfo Structure
            /// Return type	: static USERINFO
            /// Arguments	: string strFirstName,string strLastName,string strUserID,string strPIN,int iWarningEye,int iUserKind
            /// </summary>
            public static USERINFO SetUserDetails(string strFirstName, string strLastName, string strUserID, string strPIN, EUserKind eUserKind, byte[] baTimeGroupIDList, string strStartDate, string strExpireDate, string strVisitorStartTime, string strVisitorEndTime)
            {
                USERINFO stUserInfo = new USERINFO();
                if (!string.IsNullOrEmpty(strFirstName))
                {
                    stUserInfo.FirstName = new byte[HRM.DAL.Constants.FIRSTNAME_LENGTH];
                    ConvertStringToByteArr(strFirstName, stUserInfo.FirstName);
                }
                if (!string.IsNullOrEmpty(strLastName))
                {
                    stUserInfo.LastName = new byte[HRM.DAL.Constants.LASTNAME_LENGTH];
                    ConvertStringToByteArr(strLastName, stUserInfo.LastName);
                }
                if (!string.IsNullOrEmpty(strUserID))
                {
                    stUserInfo.UserID = new byte[HRM.DAL.Constants.USERID_LENGTH];
                    ConvertStringToByteArr(strUserID, stUserInfo.UserID);
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
                    ConvertStringToByteArr(strStartDate, stUserInfo.StartDate);
                }
                if (strExpireDate != null)
                {
                    stUserInfo.ExpireDate = new byte[HRM.DAL.Constants.DATE_SIZE];
                    ConvertStringToByteArr(strExpireDate, stUserInfo.ExpireDate);
                }
                if (strVisitorStartTime != null)
                {
                    stUserInfo.VisitorStartTime = new byte[HRM.DAL.Constants.TIME_SIZE];
                    ConvertStringToByteArr(strVisitorStartTime, stUserInfo.VisitorStartTime);
                }
                if (strVisitorEndTime != null)
                {
                    stUserInfo.VisitorEndTime = new byte[HRM.DAL.Constants.TIME_SIZE];
                    ConvertStringToByteArr(strVisitorEndTime, stUserInfo.VisitorEndTime);
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
                    ConvertStringToByteArr(strCardID, stCardInfo.CardID);
                }
                if (!string.IsNullOrEmpty(strCardNumber))
                {
                    stCardInfo.CardNumber = new byte[HRM.DAL.Constants.CARDNUMBER_LENGTH];
                    ConvertStringToByteArr(strCardNumber, stCardInfo.CardNumber);
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
                    ConvertStringToByteArr(strName, stTimeGroupInfo.Name);
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

            /// <summary>
            /// Description : SetHolidayDetails , Forms the HolidayInfo Structure
            /// Return type	: static HolidayInfo
            /// Arguments	: string strFirstName,string strLastName,string strUserID,string strPIN,int iWarningEye,int iUserKind
            /// </summary>
            public static HOLIDAYINFO SetHolidayDetails(string strHolidayName, string strHolidayDate)
            {
                HOLIDAYINFO stHolidayInfo = new HOLIDAYINFO();
                if (!string.IsNullOrEmpty(strHolidayName))
                {
                    stHolidayInfo.Name = new byte[HRM.DAL.Constants.HOLIDAY_NAME_SIZE];
                    ConvertStringToByteArr(strHolidayName, stHolidayInfo.Name);
                }
                if (!string.IsNullOrEmpty(strHolidayDate))
                {
                    stHolidayInfo.Date = new byte[HRM.DAL.Constants.HOLIDAY_DATE_SIZE];
                    ConvertStringToByteArr(strHolidayDate, stHolidayInfo.Date);
                }

                return stHolidayInfo;
            }

            public static int CompareDate(int iSYear, int iSMonth, int iSDay, int iEYear, int iEMonth, int iEDay)
            {
                DateTime dtStartDate = new DateTime(iSYear, iSMonth, iSDay);
                DateTime dhEndDate = new DateTime(iEYear, iEMonth, iEDay);
                int result = DateTime.Compare(dtStartDate, dhEndDate);

                if (result > 0)
                {
                    //MessageBox.Show("Set Again! Start Date Should be Ahead of the End Date.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return HRM.DAL.Constants.DATE_TIME_ERROR;
                }

                return 0;
            }

            public static int CompareDate(DateTime dtStartDate, DateTime dtEndDate)
            {
                if (dtStartDate != DateTime.MaxValue && dtEndDate != DateTime.MaxValue)
                {
                    if (DateTime.Compare(dtStartDate, dtEndDate) > 0)
                    {
                        //MessageBox.Show("Set again!. Start Date Should be Ahead of the End Date.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                }
                else if (dtStartDate == DateTime.MaxValue && dtEndDate == DateTime.MaxValue)
                {
                    //MessageBox.Show("Set Start Date and Expire Date Again.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return HRM.DAL.Constants.DATE_TIME_ERROR;
                }
                else
                {
                    if (dtStartDate == DateTime.MaxValue)
                    {
                        //MessageBox.Show("Set Start Date Again.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                    else
                    {
                        //MessageBox.Show("Set Expire Date Again.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                }

                return 0;
            }

            public static int CompareTime(decimal iSHour, decimal iSMin, decimal iEHour, decimal iEMin)
            {
                if (iSHour > iEHour)
                {
                    //MessageBox.Show("Set Again! Start Time Must be Previous to End Time.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return HRM.DAL.Constants.DATE_TIME_ERROR;
                }
                else if (iSHour == iEHour)
                {
                    if (iSMin >= iEMin)
                    {
                        //MessageBox.Show("Set Again! Start Time Must be Previous to End Time.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                }
                else if (iEHour == 24)
                {
                    if (iEMin > 0)
                    {
                        //MessageBox.Show("Set Again! End Time cannot be more than 24:00.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                }

                return 0;
            }

            public static int CompareTime(decimal iSHour, decimal iSMin, decimal iSSec, decimal iEHour, decimal iEMin, decimal iESec)
            {
                if (iSHour > iEHour)
                {
                    //MessageBox.Show("Set Again! Start Time Must be Previous to End Time.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return HRM.DAL.Constants.DATE_TIME_ERROR;
                }
                else if (iSHour == iEHour)
                {
                    if (iSMin > iEMin)
                    {
                        //MessageBox.Show("Set Again! Start Time Must be Previous to End Time.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                    else if (iSMin == iEMin)
                    {
                        if (iSSec >= iESec)
                        {
                            //MessageBox.Show("Set Again! Start Time Must be Previous to End Time.", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return HRM.DAL.Constants.DATE_TIME_ERROR;
                        }
                    }
                }
                else if (iEHour == 24)
                {
                    if (iEMin > 0)
                    {
                        //MessageBox.Show("Set Again! End Time cannot be more than 24:00:00", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                    else if (iESec > 0)
                    {
                        //MessageBox.Show("Set Again! End Time cannot be more than 24:00:00", HRM.DAL.Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return HRM.DAL.Constants.DATE_TIME_ERROR;
                    }
                }

                return 0;
            }

            public static string FormatDateTime(string strDate, int iHour, int iMin)
            {
                string strHour = string.Empty;
                string strMin = string.Empty;
                string strFormatDate = string.Empty;
                string strSec = "00";

                if (iHour == 24)
                    iMin = 0;

                string[] strDateTime = strDate.Split(' ');
                strDate = strDateTime[0];

                if (iHour < 10)
                    strHour += HRM.DAL.Constants.DEFAULT_VALUE;

                strHour += iHour.ToString();

                if (iMin < 10)
                    strMin += HRM.DAL.Constants.DEFAULT_VALUE;

                strMin += iMin.ToString();
                strFormatDate = strDate + " " + strHour + ":" + strMin + ":" + strSec;

                return strFormatDate;
            }

            public static string FormatDateTime(string strDate, int iHour, int iMin, int iSec)
            {
                string strHour = string.Empty;
                string strMin = string.Empty;
                string strSec = string.Empty;
                string strFormatDate = string.Empty;

                if (iHour == 24)
                    iMin = 0;

                string[] strDateTime = strDate.Split(' ');
                strDate = strDateTime[0];

                if (iHour < 10)
                    strHour += HRM.DAL.Constants.DEFAULT_VALUE;

                strHour += iHour.ToString();

                if (iMin < 10)
                    strMin += HRM.DAL.Constants.DEFAULT_VALUE;

                strMin += iMin.ToString();

                if (iSec < 10)
                    strSec += HRM.DAL.Constants.DEFAULT_VALUE;

                strSec += iSec.ToString();

                strFormatDate = strDate + " " + strHour + ":" + strMin + ":" + strSec;

                return strFormatDate;
            }

            public static string FormatTime(decimal iHour)
            {
                string strHour = string.Empty;

                if (iHour < 10)
                {
                    strHour += HRM.DAL.Constants.DEFAULT_VALUE;
                }

                strHour += iHour.ToString();

                return strHour;
            }

            /// <summary>
            /// Description : ConvertStringToByteArray
            /// Return type	: static void
            /// Arguments	: string strData,byte[] byData
            /// </summary>
            public static void ConvertStringToByteArr(string strData, byte[] byData)
            {
                if (strData != null)
                {
                    byte[] bytes = Encoding.Default.GetBytes(strData);
                    bytes.CopyTo(byData, 0);
                }

            }

            public static byte[] ConvertStructToByteArr(object obj)
            {
                int len = Marshal.SizeOf(obj);

                byte[] arr = new byte[len];

                IntPtr ptr = Marshal.AllocHGlobal(len);

                Marshal.StructureToPtr(obj, ptr, true);

                Marshal.Copy(ptr, arr, 0, len);

                Marshal.FreeHGlobal(ptr);

                return arr;
            }

            /// <summary>
            /// Description : ConvertByteArrayToString
            /// Return type	: static string
            /// Arguments	: byte[] byData
            /// </summary>
            public static string ConvertByteArrToString(byte[] byData)
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

            public static string GetIrisImageFormat(string strFilePath)
            {
                string strFormat = string.Empty;
                int iIndex = 0;

                if (!string.IsNullOrEmpty(strFilePath))
                {
                    iIndex = strFilePath.LastIndexOf("\\");

                    if (iIndex != -1)
                    {
                        strFormat = strFilePath.Substring(iIndex, strFilePath.Length - iIndex);

                        if (strFormat != string.Empty)
                        {
                            iIndex = strFormat.IndexOf(".");

                            if (iIndex != -1)
                                strFormat = strFormat.Substring(iIndex, strFormat.Length - iIndex);
                        }
                    }
                }

                return strFormat;
            }

            public static byte[] ReadIrisImageBinaryData(string strFileName, int iLength)
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length < iLength)//If Iris Image length is less than given length return don't assign
                    {
                        fs.Close();
                        return null;
                    }
                    byte[] buffer = new byte[iLength];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    return buffer;
                }
            }                       

            public static void ConvertByteArrayToFile(string directoryName, string fileName, byte[] data)
            {
                string completePath = Path.Combine(directoryName, fileName);

                using (FileStream fileStream = new FileStream(completePath, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                }
            }

            public static EEyeType GetSelectedEye(string strSelectedEye)
            {
                if (strSelectedEye != string.Empty)
                {
                    if (strSelectedEye == "Right")
                    {
                        return EEyeType.RightEye;
                    }
                    else if (strSelectedEye == "Left")
                    {
                        return EEyeType.LeftEye;
                    }
                    else if (strSelectedEye == "Both")
                    {
                        return EEyeType.BothEye;
                    }
                    else
                    {
                        return EEyeType.None;
                    }
                }
                else
                {
                    return EEyeType.None;
                }
            }

            //public static string GetIrisCodeDir(string strServerIP)
            //{
            //    return Path.GetDirectoryName(Application.ExecutablePath) + HRM.DAL.Constants.FILE_DELIMETER + HRM.DAL.Constants.FOLDER_IRIS_CODE + HRM.DAL.Constants.FILE_DELIMETER + strServerIP;
            //}

            //public static string GetIrisImageDir(string strServerIP)
            //{
            //    return Path.GetDirectoryName(Application.ExecutablePath) + HRM.DAL.Constants.FILE_DELIMETER + HRM.DAL.Constants.FOLDER_IRIS_IMAGE + HRM.DAL.Constants.FILE_DELIMETER + strServerIP;
            //}

            public static int ValidateIPAddress(string strIP)
            {
                string[] delim = { "." };
                StringSplitOptions op = new StringSplitOptions();
                string[] strIPFields = strIP.Split(delim, op);
                int iFieldCount = strIPFields.Length;


                if (iFieldCount != 4)
                    return 1;

                long lField1 = Convert.ToInt32(strIPFields[0]);
                long lField2 = Convert.ToInt32(strIPFields[1]);
                long lField3 = Convert.ToInt32(strIPFields[2]);
                long lField4 = Convert.ToInt32(strIPFields[3]);

                if (lField1 > 255 || lField2 > 255 || lField3 > 255 || lField3 > 255)
                    return 1;

                if (lField1 == 0 && lField2 == 0 && lField3 == 0 && lField4 == 0)
                    return 1;

                if (lField1 == 0)
                    return 1;

                return 0;
            }

            public static RECOGMODESCHEDULEINFO SetRecogScheduleDetails(string strName, int iDefaultMode, TIMEANDMODEINFO[] baSlotInfo)
            {
                RECOGMODESCHEDULEINFO stScheduleInfo = new RECOGMODESCHEDULEINFO();

                stScheduleInfo.TimeGroupID = 0;
                stScheduleInfo.DefaultRecogMode = iDefaultMode;

                if (!string.IsNullOrEmpty(strName))
                {
                    stScheduleInfo.Name = new byte[HRM.DAL.Constants.NAME_SIZE];
                    ConvertStringToByteArr(strName, stScheduleInfo.Name);
                }

                if (baSlotInfo == null)
                {
                    stScheduleInfo.TIMEINFO = null;
                }
                else
                {
                    stScheduleInfo.TIMEINFO = new TIMEANDMODEINFO[HRM.DAL.Constants.DAY_NUM];
                    stScheduleInfo.TIMEINFO = baSlotInfo;
                }

                return stScheduleInfo;
            }

            /// <summary>
            /// Description : Get Binary Data 
            /// Return type	: byte Array
            /// Arguments	: byte[] baBinaryData, int iSize, int iCounter
            /// </summary>
            public static byte[] GetBinaryData(byte[] baBinaryData, int iSize, int iCounter)
            {
                try
                {
                    byte[] baData = new byte[iSize];
                    Array.Copy(baBinaryData, iCounter, baData, 0, baData.Length);
                    return baData;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
}
