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
  public class irisBackupDAL
    {
        private iCAMManagerAPI m_DeviceControl;
        HOLIDAYINFO stHolidayInfo;
        iCAM70003SDKC _DeviceInstance = null;
        CONNECTION_OPTIONS _ConnectionOptions;

      public int Save(string ip,string createdby)
      {
          DataAccess objDA = new DataAccess();
          List<SqlParameter> objParam = new List<SqlParameter>();
          try
          {

              objParam.Add(new SqlParameter("@BackupDevice", ip));

              objParam.Add(new SqlParameter("@CreatedBy", createdby));
         
                  objDA.sqlCmdText = "hrm_irisdata_insert";
              objDA.sqlParam = objParam.ToArray();
              int val = int.Parse(objDA.ExecuteScalar().ToString());
              return val;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

    
   
     
      public string backup (DataTable dTable, int backupid)
      {
      
         // TIMEGROUPINFO timegrpinfo = default(TIMEGROUPINFO);
          object objuserbackupInfo = null;
          object objcardbackupinfo = null;
          object objTransactionInfo = null;
          object objtimegrpinfo = null;
         int iTransactionLogCount = 0;
        
          
       
         
         // string UserID = "";
          string startdate="09-Mar-1785 00:00:00";
          string enddate="";
        
          object objsysinfo = null;
          IRISINFO righteyeinfo = default(IRISINFO);
          IRISINFO lefteyeinfo = default(IRISINFO);
          
          try
          {
              foreach (DataRow irow in dTable.Rows)
              {
                  //int iUserc = 0;
                  int iRetVal = 0;
                  int _ConnectionID = 0;
                

                  string irisId = irow["IrisId"].ToString();
                  string ipAddress = irow["IPAddresss"].ToString();
                  string securityId = irow["SecurityId"].ToString();
                  string userName = irow["UserName"].ToString();
                  string password = irow["Password"].ToString();
                   int result;
                   iCAM70003SDKC _DeviceInstance = IrisConnectivity.connectDevice(ipAddress,  securityId, userName, password, out result);

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
                    

                      GetIrisDetails.GetTransactionCount(out iTransactionLogCount, _DeviceInstance);
                    if (result != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE)
                     {
                          IrisConnectivity.Disconnect(_DeviceInstance);
                          string error = string.Format(@"ErrorCode : 0x{0:X}", result) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(result, string.Empty) + "." + Constants.TITLE;
                          return error;
                      }


                    if (result == ErrorConstants.IACDB_ERROR_NO_RECORD || result == ErrorConstants.RECOG70003SDK_ERR_NO_RECORD)
                      {
                          IrisConnectivity.Disconnect(_DeviceInstance);
                          string error = string.Format(@"ErrorCode : 0x{0:X}", result) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(result, string.Empty) + "." + Constants.TITLE;
                          return error;
                      }

                    if (result != ErrorConstants.ICAMSDK_ERROR_NONE || objuserbackupInfo == null)
                      {
                          string error = string.Format(@"ErrorCode : 0x{0:X}", result) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(result, string.Empty) + "." + Constants.TITLE;
                          return error;
                      }

                     
                      if (iTransactionLogCount > 0)
                      {
                          

                            
                              int Uresult;
                              GetIrisDetails.GetUserFirst(out Uresult, _DeviceInstance);

                              if (result != ErrorConstants.ICAMSDK_ERROR_NONE || objuserbackupInfo == null)
                              {
                                  IrisConnectivity.Disconnect(_DeviceInstance);
                                  string error = string.Format(@"ErrorCode : 0x{0:X}", Uresult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(Uresult, string.Empty) + "." + Constants.TITLE;
                                  return error;
                              }


                              SaveUserBackupUI(objuserbackupInfo, irisId, _DeviceInstance, backupid);

                          DataTable dt = getUserID(backupid);
                          for (int i = 0; i <= dt.Rows.Count; i++)
                          {
                              string UserID = dt.Rows[i]["UserID"].ToString();
                              int CardResult;
                              GetIrisDetails.GetCard(UserID, out CardResult, _DeviceInstance);

                              if (CardResult != ErrorConstants.ICAMSDK_ERROR_NONE || objcardbackupinfo == null)
                              {
                                  IrisConnectivity.Disconnect(_DeviceInstance);
                                  string error = string.Format(@"ErrorCode : 0x{0:X}", CardResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(CardResult, string.Empty) + "." + Constants.TITLE;
                                  return error;
                              }

                              SavecardBackupUI(objcardbackupinfo, irisId, _DeviceInstance, backupid);
                          }
                          int Tresult;
                          GetIrisDetails.GetTransactionFirst(out Tresult, _DeviceInstance);

                          if (Tresult != ErrorConstants.ICAMSDK_ERROR_NONE || objTransactionInfo == null)
                          {
                              IrisConnectivity.Disconnect(_DeviceInstance);
                              string error = string.Format(@"ErrorCode : 0x{0:X}", Tresult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(Tresult, string.Empty) + "." + Constants.TITLE;
                              return error;
                          }

                          savetransactionBackupUI(objTransactionInfo, irisId, _DeviceInstance, backupid);


                      
                          int altimResult;
                          GetIrisDetails.GetAllTimeGroup(out altimResult, _DeviceInstance);
                       
                          if (altimResult != ErrorConstants.ICAMSDK_ERROR_NONE || objtimegrpinfo == null)
                          {
                              IrisConnectivity.Disconnect(_DeviceInstance);
                              string error = string.Format(@"ErrorCode : 0x{0:X}", altimResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(altimResult, string.Empty) + "." + Constants.TITLE;
                              return error;
                          }
                              saveTimegrpBackupUI(objtimegrpinfo, irisId, _DeviceInstance, backupid);
                    
                          DataTable dtbl = getUserID(backupid);
                          for (int i = 0; i <= dtbl.Rows.Count; i++)
                          {
                              string UserID = dtbl.Rows[i]["UserID"].ToString();
                              int Lresult;
                              int Rresult;
                              GetIrisDetails.GetRightIris(UserID, out Rresult, _DeviceInstance);
                              GetIrisDetails.GetLeftIris(UserID, out Lresult, _DeviceInstance);
                              if (Rresult != ErrorConstants.ICAMSDK_ERROR_NONE)
                              {
                                  IrisConnectivity.Disconnect(_DeviceInstance);
                                  string error = string.Format(@"ErrorCode : 0x{0:X}", Rresult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(Rresult, string.Empty) + "." + Constants.TITLE;
                                  return error;
                              }

                              if (Lresult != ErrorConstants.ICAMSDK_ERROR_NONE)
                              {
                                  IrisConnectivity.Disconnect(_DeviceInstance);
                                  string error = string.Format(@"ErrorCode : 0x{0:X}", Lresult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(Lresult, string.Empty) + "." + Constants.TITLE;
                                  return error;
                              }
                              SaveIriscodeBackupUI(UserID, righteyeinfo, lefteyeinfo, irisId, _DeviceInstance, backupid);

                          }
                          int SysResult;
                          GetIrisDetails.GetSystemlogFirst(out SysResult, _DeviceInstance);
                          if (SysResult != ErrorConstants.ICAMSDK_ERROR_NONE || objsysinfo == null)
                          {
                              IrisConnectivity.Disconnect(_DeviceInstance);
                              string error = string.Format(@"ErrorCode : 0x{0:X}", SysResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(SysResult, string.Empty) + "." + Constants.TITLE;
                              return error;
                          }
                          SavesyslogBackupUI(Constants.MAXBLOCK_SIZE,startdate,enddate,objsysinfo, irisId, _DeviceInstance, backupid);
                          
                     }

                      IrisConnectivity.Disconnect(_DeviceInstance);
                  }
                  else
                  {
                      IrisConnectivity.Disconnect(_DeviceInstance);
                      string error = string.Format(@"ErrorCode : 0x{0:X}", iResult) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iResult, string.Empty) + "." + Constants.TITLE;
                      return error;
                  }
              }
              return "";
          }
          catch (Exception ex)
          {
              IrisConnectivity.Disconnect(_DeviceInstance);
              return ex.Message;
          }
      }


      private void SaveUserBackupUI(object objTransLog, string irisId,  iCAM70003SDKC _DeviceInstance,int backupid)
      {
          try
          {
              USERINFO stbackupLogInfo = default(USERINFO);
              int k = 0;
              int iSize = Marshal.SizeOf(stbackupLogInfo);
              byte[,] baTransactionLog = (byte[,])objTransLog;
              byte[] baTransactionLogData = new byte[baTransactionLog.Length];
              Buffer.BlockCopy(baTransactionLog, 0, baTransactionLogData, 0, baTransactionLog.Length);
              int iLen = baTransactionLog.GetLength(0);

              for (int i = 0; i < baTransactionLog.GetLength(0); i++)
              {
                  byte[] baBlockData = HRM.DAL.IrisUtility.GetBinaryData(baTransactionLogData, iSize, k);

                  if (baTransactionLogData != null)
                      stbackupLogInfo =  GetUserbackupLogInfoFromBinaryData(baBlockData, _DeviceInstance);

                  SaveuserbackupDetails(stbackupLogInfo, irisId, _DeviceInstance,backupid);
                  k = k + iSize;
              }
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
    
      private void SavesyslogBackupUI(int blocksize,string starttime,string endtime,object objsyslog, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              SYSTEMLOGINFO stbackupLogInfo = default(SYSTEMLOGINFO);
              int k = 0;
              int iSize = Marshal.SizeOf(stbackupLogInfo);
              byte[,] baTransactionLog = (byte[,])objsyslog;
              byte[] baTransactionLogData = new byte[baTransactionLog.Length];
              Buffer.BlockCopy(baTransactionLog, 0, baTransactionLogData, 0, baTransactionLog.Length);
              int iLen = baTransactionLog.GetLength(0);

              for (int i = 0; i < baTransactionLog.GetLength(0); i++)
              {
                  byte[] baBlockData = HRM.DAL.IrisUtility.GetBinaryData(baTransactionLogData, iSize, k);

                  if (baTransactionLogData != null)
                      stbackupLogInfo = GetsysinfobackupLogInfoFromBinaryData(baBlockData, _DeviceInstance);

                  SavesysinfobackupDetails(blocksize,starttime,endtime, stbackupLogInfo, irisId, _DeviceInstance, backupid);
                  k = k + iSize;
              }
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }

      private void SavecardBackupUI(object objTransLog, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              CARDINFO stbackupLogInfo = default(CARDINFO);
              int k = 0;
              int iSize = Marshal.SizeOf(stbackupLogInfo);
              byte[,] baTransactionLog = (byte[,])objTransLog;
              byte[] baTransactionLogData = new byte[baTransactionLog.Length];
              Buffer.BlockCopy(baTransactionLog, 0, baTransactionLogData, 0, baTransactionLog.Length);
              int iLen = baTransactionLog.GetLength(0);

              for (int i = 0; i < baTransactionLog.GetLength(0); i++)
              {
                  byte[] baBlockData = HRM.DAL.IrisUtility.GetBinaryData(baTransactionLogData, iSize, k);

                  if (baTransactionLogData != null)
                      stbackupLogInfo = GetcardbackupLogInfoFromBinaryData(baBlockData, _DeviceInstance);

                  SavecardbackupDetails(stbackupLogInfo, irisId, _DeviceInstance, backupid);
                  k = k + iSize;
              }
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void savetransactionBackupUI(object objTransLog, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
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

                  SaveTransactionbackupDetails(stTransactionLogInfo, irisId, _DeviceInstance, backupid);
                  k = k + iSize;
              }
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void saveTimegrpBackupUI(object objTransLog, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              TIMEGROUPINFO stTimeGrpLogInfo = default(TIMEGROUPINFO);
              int k = 0;
              int iSize = Marshal.SizeOf(stTimeGrpLogInfo);
              byte[,] baTransactionLog = (byte[,])objTransLog;
              byte[] baTransactionLogData = new byte[baTransactionLog.Length];
              Buffer.BlockCopy(baTransactionLog, 0, baTransactionLogData, 0, baTransactionLog.Length);
              int iLen = baTransactionLog.GetLength(0);

              for (int i = 0; i < baTransactionLog.GetLength(0); i++)
              {
                  byte[] baBlockData = HRM.DAL.IrisUtility.GetBinaryData(baTransactionLogData, iSize, k);

                  if (baTransactionLogData != null)
                      stTimeGrpLogInfo = GetTimeGroupLogInfoFromBinaryData(baBlockData, _DeviceInstance);

                  SaveTimegrpbackupDetails(stTimeGrpLogInfo, irisId, _DeviceInstance, backupid);
                  k = k + iSize;
              }
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SaveIriscodeBackupUI(String userid, object righteyeinfo, object lefteyeinfo, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              IRISINFO stbackupLogInfoR = default(IRISINFO);
              IRISINFO stbackupLogInfoL = default(IRISINFO);
              int k = 0;
              int RSize = Marshal.SizeOf(stbackupLogInfoR);
              int LSize = Marshal.SizeOf(stbackupLogInfoL);
              byte[,] barightLog = (byte[,])righteyeinfo;
              byte[,] baleftLog = (byte[,])lefteyeinfo;
              byte[] barightLogData = new byte[barightLog.Length];
              byte[] baleftLogData = new byte[baleftLog.Length];
              Buffer.BlockCopy(barightLog, 0, barightLogData, 0, barightLog.Length);
              Buffer.BlockCopy(baleftLog, 0, baleftLogData, 0, baleftLog.Length);
              int iLen = barightLog.GetLength(0);
              int iLength = baleftLog.GetLength(0);

              for (int i = 0; i < barightLog.GetLength(0); i++)
              {
                  for (int j = 0; j < baleftLog.GetLength(0); j++)
                  {

                      byte[] baBlockDataR = HRM.DAL.IrisUtility.GetBinaryData(barightLogData, RSize, k);

                      if (barightLogData != null)
                          stbackupLogInfoR = GetrightIrisLogInfoFromBinaryData(baBlockDataR, _DeviceInstance);
                      byte[] baBlockDataL = HRM.DAL.IrisUtility.GetBinaryData(baleftLogData, LSize, k);

                      if (baleftLogData != null)
                          stbackupLogInfoL = GetleftIrisLogInfoFromBinaryData(baBlockDataL, _DeviceInstance);

                     
                  }
                  SaveiriscodebackupDetails(stbackupLogInfoR, stbackupLogInfoL, userid, irisId, _DeviceInstance, backupid);
                  k = k + RSize;

                  k = k + LSize;
              }




          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }

      private static USERINFO GetUserbackupLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              USERINFO userbackupLogInfo = default(USERINFO);
              int iSize = Marshal.SizeOf(userbackupLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              userbackupLogInfo = (USERINFO)Marshal.PtrToStructure(iPtr, typeof(USERINFO));
              Marshal.FreeHGlobal(iPtr);

              return userbackupLogInfo;
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
 
      private static SYSTEMLOGINFO GetsysinfobackupLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              SYSTEMLOGINFO userbackupLogInfo = default(SYSTEMLOGINFO);
              int iSize = Marshal.SizeOf(userbackupLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              userbackupLogInfo = (SYSTEMLOGINFO)Marshal.PtrToStructure(iPtr, typeof(SYSTEMLOGINFO));
              Marshal.FreeHGlobal(iPtr);

              return userbackupLogInfo;
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private static CARDINFO GetcardbackupLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              CARDINFO userbackupLogInfo = default(CARDINFO);
              int iSize = Marshal.SizeOf(userbackupLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              userbackupLogInfo = (CARDINFO)Marshal.PtrToStructure(iPtr, typeof(CARDINFO));
              Marshal.FreeHGlobal(iPtr);

              return userbackupLogInfo;
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
      private static TIMEGROUPINFO GetTimeGroupLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              TIMEGROUPINFO timegrpLogInfo = default(TIMEGROUPINFO);
              int iSize = Marshal.SizeOf(timegrpLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              timegrpLogInfo = (TIMEGROUPINFO)Marshal.PtrToStructure(iPtr, typeof(TIMEGROUPINFO));
              Marshal.FreeHGlobal(iPtr);

              return timegrpLogInfo;
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private static IRISINFO GetrightIrisLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              IRISINFO timegrpLogInfo = default(IRISINFO);
              int iSize = Marshal.SizeOf(timegrpLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              timegrpLogInfo = (IRISINFO)Marshal.PtrToStructure(iPtr, typeof(IRISINFO));
              Marshal.FreeHGlobal(iPtr);

              return timegrpLogInfo;
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private static IRISINFO GetleftIrisLogInfoFromBinaryData(byte[] baData, iCAM70003SDKC _DeviceInstance)
      {
          try
          {
              IRISINFO timegrpLogInfo = default(IRISINFO);
              int iSize = Marshal.SizeOf(timegrpLogInfo);
              IntPtr iPtr = Marshal.AllocHGlobal(iSize);
              Marshal.Copy(baData, 0, iPtr, iSize);
              timegrpLogInfo = (IRISINFO)Marshal.PtrToStructure(iPtr, typeof(IRISINFO));
              Marshal.FreeHGlobal(iPtr);

              return timegrpLogInfo;
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SaveuserbackupDetails(USERINFO stbackupLogInfo, string irisId, iCAM70003SDKC _DeviceInstance,int backupid)
      {
          try
          {
              int backid = backupid;
              string userid = IrisUtility.ConvertByteArrToString(stbackupLogInfo.UserID);
              string fname = IrisUtility.ConvertByteArrToString(stbackupLogInfo.FirstName);
              string lname = IrisUtility.ConvertByteArrToString(stbackupLogInfo.LastName);
              int    pin   = (stbackupLogInfo.Pin);
              string timegrpIDlist = IrisUtility.ConvertByteArrToString(stbackupLogInfo.TimeGroupIDList);
              string eUserkind = ("" + stbackupLogInfo.EUserKind);
              string whicheye = ("" + stbackupLogInfo.WhichEye);
              string startdate =IrisUtility.ConvertByteArrToString (stbackupLogInfo.StartDate);
              string expDate = IrisUtility.ConvertByteArrToString(stbackupLogInfo.ExpireDate);
              string VisitorStarttime = IrisUtility.ConvertByteArrToString(stbackupLogInfo.VisitorStartTime);
              string visitrendTim = IrisUtility.ConvertByteArrToString(stbackupLogInfo.VisitorEndTime);


             

              //string s9 = GetTransactionLogKind(Convert.ToInt32(stbackupLogInfo.VisitorEndTime)); //Process Result
              //string s10 = stbackupLogInfo.HammingDistance.ToString();

              //string s12 = "";
              //if (stbackupLogInfo.LeftRight == (int)EEyeType.BothEye)
              //    s12 = "Both";
              //else if (stbackupLogInfo.LeftRight == (int)EEyeType.RightEye)
              //    s12 = "Right";
              //else if (stbackupLogInfo.LeftRight == (int)EEyeType.LeftEye)
              //    s12 = "Left";

              //string s13 = IrisUtility.ConvertByteArrToString(stbackupLogInfo.Message);

              DataAccess objDA = new DataAccess();

              //var sql = String.Format("delete from hrm_IrisTransactions where OccurDateTime = '{0}' and IrisId ='{1}' and UserId = '{2}' ", s1, endDate, s3);
              //objDA.sqlCmdText = sql;
              //objDA.ExecuteNonQueryInline();backid

              List<SqlParameter> objParam = new List<SqlParameter>();
              objParam.Add(new SqlParameter("@BackupID", backid));
              objParam.Add(new SqlParameter("@IrisId", irisId));
              objParam.Add(new SqlParameter("@UserID", userid));
              objParam.Add(new SqlParameter("@FirstName", fname));
              objParam.Add(new SqlParameter("@LastName", lname));
              objParam.Add(new SqlParameter("@Pin", pin));
              objParam.Add(new SqlParameter("@TimeGroupIDList", timegrpIDlist));
              objParam.Add(new SqlParameter("@EUserKind", eUserkind));
              objParam.Add(new SqlParameter("@WhichEye", whicheye));
              objParam.Add(new SqlParameter("@StartDate", startdate));
              objParam.Add(new SqlParameter("@ExpireDate", expDate));
              objParam.Add(new SqlParameter("@VisitorStartTime", VisitorStarttime));
              objParam.Add(new SqlParameter("@VisitorEndTime", visitrendTim));
              /*paramets only in db*/
             
              objParam.Add(new SqlParameter("@MI", ""));
              objParam.Add(new SqlParameter("@Sex", ""));
              objParam.Add(new SqlParameter("@ResidentNumber", ""));
              objParam.Add(new SqlParameter("@Department", ""));
              objParam.Add(new SqlParameter("@Rank", ""));
              objParam.Add(new SqlParameter("@LIrisCodeID", ""));
              objParam.Add(new SqlParameter("@RIrisCodeID", ""));
              objParam.Add(new SqlParameter("@PictureID", ""));
              objParam.Add(new SqlParameter("@OfficePhone", ""));
              objParam.Add(new SqlParameter("@HomePhone", ""));
              objParam.Add(new SqlParameter("@MobilePhone", ""));
              objParam.Add(new SqlParameter("@Memo1", ""));
              objParam.Add(new SqlParameter("@Memo2", ""));
              objParam.Add(new SqlParameter("@Memo3", ""));
              objParam.Add(new SqlParameter("@Memo4", ""));
              objParam.Add(new SqlParameter("@Memo5", ""));
              objParam.Add(new SqlParameter("@RemoteGroupIDList",""));
              objParam.Add(new SqlParameter("@StartDateTime", ""));
              objParam.Add(new SqlParameter("@EndDateTime", ""));
              objParam.Add(new SqlParameter("@CreateDateTime", ""));
              objParam.Add(new SqlParameter("@Address", ""));
              objParam.Add(new SqlParameter("@EMail", ""));
              objParam.Add(new SqlParameter("@UCID", ""));
              objParam.Add(new SqlParameter("@WarningEye", ""));
              objParam.Add(new SqlParameter("@CheckSum_User", ""));
              objParam.Add(new SqlParameter("@CheckSum_RIrisCode", ""));
              objParam.Add(new SqlParameter("@CheckSum_LIrisCode", ""));
              objParam.Add(new SqlParameter("@CheckSum_Card", ""));
              objParam.Add(new SqlParameter("@CheckSum_IrisInfo", ""));
              objParam.Add(new SqlParameter("@DeleteInfo", ""));
              objParam.Add(new SqlParameter("@UpdateDateTime", ""));
              objParam.Add(new SqlParameter("@PassByCard", ""));

              objDA.sqlCmdText = "hrm_iris_User_Insert_Update";
              objDA.sqlParam = objParam.ToArray();

              int val = int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
  
      private void SavesysinfobackupDetails(int blocksize,string startdate,string enddate, SYSTEMLOGINFO stbackupLogInfo, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              int backid = backupid;
              string devicename = IrisUtility.ConvertByteArrToString(stbackupLogInfo.DeviceName);
              string kind = (stbackupLogInfo.Kind).ToString();
              string msg = IrisUtility.ConvertByteArrToString(stbackupLogInfo.Message);
              string occrdate = IrisUtility.ConvertByteArrToString(stbackupLogInfo.OccurDate);

              DataAccess objDA = new DataAccess();

              List<SqlParameter> objParam = new List<SqlParameter>();
              objParam.Add(new SqlParameter("@OccurDateTime", occrdate));
              objParam.Add(new SqlParameter("@BackupID", backid));

              objParam.Add(new SqlParameter("@Message", msg));
              objParam.Add(new SqlParameter("@Kind", kind));
              objParam.Add(new SqlParameter("@IPAddress", ""));
              objParam.Add(new SqlParameter("@Name", ""));
              objParam.Add(new SqlParameter("@NodeID", ""));
           
         
              objParam.Add(new SqlParameter("@CreateDateTime", ""));


              objDA.sqlCmdText = "hrm_IrisSystemLog_Insert_Update";
              objDA.sqlParam = objParam.ToArray();

              int val = int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SavecardbackupDetails(CARDINFO stbackupLogInfo, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              int backid = backupid;
              string cardid = IrisUtility.ConvertByteArrToString(stbackupLogInfo.CardID);
              string cardnum = IrisUtility.ConvertByteArrToString(stbackupLogInfo.CardNumber);
             // int ecardkind =IrisUtility.ConvertByteArrToString(stbackupLogInfo.ECardKind);
            
             
            


            

              DataAccess objDA = new DataAccess();

              //var sql = String.Format("delete from hrm_IrisTransactions where OccurDateTime = '{0}' and IrisId ='{1}' and UserId = '{2}' ", s1, endDate, s3);
              //objDA.sqlCmdText = sql;
              //objDA.ExecuteNonQueryInline();backid

              List<SqlParameter> objParam = new List<SqlParameter>();
              objParam.Add(new SqlParameter("@BackupID", backid));
              objParam.Add(new SqlParameter("@IrisId", irisId));
              objParam.Add(new SqlParameter("@CardID", cardid));
              objParam.Add(new SqlParameter("@CardNumber", cardnum));
            //  objParam.Add(new SqlParameter("@CardKind", ));
              objParam.Add(new SqlParameter("@UUID", ""));
              objParam.Add(new SqlParameter("@UpdateDateTime", ""));
              objParam.Add(new SqlParameter("@DeleteInfo", ""));
              
           



              objDA.sqlCmdText = "hrm_IrisCard_Insert_Update";
              objDA.sqlParam = objParam.ToArray();

              int val = int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SaveTimegrpbackupDetails(TIMEGROUPINFO stTimegrpInfo, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {


              string name = IrisUtility.ConvertByteArrToString(stTimegrpInfo.Name);
              string tgrpID = stTimegrpInfo.TimeGroupID.ToString();
              //string timeinfo = IrisUtility.ConvertStringToByteArr(stTimegrpInfo.TIMEINFO);-->image type
              DataAccess objDA = new DataAccess();

              List<SqlParameter> objParam = new List<SqlParameter>();

              objParam.Add(new SqlParameter("@BackupID", backupid));
              objParam.Add(new SqlParameter("@Name", name));
              objParam.Add(new SqlParameter("@TimeGroupID", tgrpID));
              //objParam.Add(new SqlParameter("@TimeInfo", timeinfo));  --> image format
              objParam.Add(new SqlParameter("@Description", ""));
              objParam.Add(new SqlParameter("@CheckSum", ""));
              objParam.Add(new SqlParameter("@DeleteInfo", ""));
      
              objDA.sqlCmdText = "hrm_iris_time_group_Insert_Update";
              objDA.sqlParam = objParam.ToArray();

              int val = int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SaveTransactionbackupDetails(TRANSACTIONLOGINFO stTransactionInfo, string irisId,  iCAM70003SDKC _DeviceInstance,int backupid)
      {
          try
          {
              string ocurdate = IrisUtility.ConvertByteArrToString(stTransactionInfo.OccurDate);
              string devicename = IrisUtility.ConvertByteArrToString(stTransactionInfo.DeviceName);
              string userid = IrisUtility.ConvertByteArrToString(stTransactionInfo.UserID);
              string cardid = IrisUtility.ConvertByteArrToString(stTransactionInfo.CardID);
              string fname = IrisUtility.ConvertByteArrToString(stTransactionInfo.FirstName);
              string lname = IrisUtility.ConvertByteArrToString(stTransactionInfo.LastName);
              //string lname = IrisUtility.ConvertByteArrToString(stTransactionInfo.);
              string hd = stTransactionInfo.HammingDistance.ToString();

              string lftryt = "";
              if (stTransactionInfo.LeftRight == (int)EEyeType.BothEye)
                  lftryt = "Both";
              else if (stTransactionInfo.LeftRight == (int)EEyeType.RightEye)
                  lftryt = "Right";
              else if (stTransactionInfo.LeftRight == (int)EEyeType.LeftEye)
                  lftryt = "Left";

              string msg = IrisUtility.ConvertByteArrToString(stTransactionInfo.Message);

              DataAccess objDA = new DataAccess();

              
              List<SqlParameter> objParam = new List<SqlParameter>();

              objParam.Add(new SqlParameter("@BackupID", backupid));
              objParam.Add(new SqlParameter("@IrisId", irisId));
              objParam.Add(new SqlParameter("@UUID", ""));
              objParam.Add(new SqlParameter("@MI", ""));
              objParam.Add(new SqlParameter("@OccurDateTime", ocurdate));
              objParam.Add(new SqlParameter("@DeviceName", devicename));
              objParam.Add(new SqlParameter("@UserID", userid));
              objParam.Add(new SqlParameter("@CardID", cardid));
              objParam.Add(new SqlParameter("@FirstName", fname));
              objParam.Add(new SqlParameter("@LastName", lname));
              objParam.Add(new SqlParameter("@HammingDistance", hd));
              objParam.Add(new SqlParameter("@LeftRight", lftryt));
              objParam.Add(new SqlParameter("@NodeID", ""));
              objParam.Add(new SqlParameter("@Message", msg));
              objParam.Add(new SqlParameter("@Kind", ""));
              objParam.Add(new SqlParameter("@StableTimeInterval", ""));
              objParam.Add(new SqlParameter("@DecisionTimeInterval", ""));
              objParam.Add(new SqlParameter("@PIN", ""));
              objParam.Add(new SqlParameter("@RotationDegree", ""));
              objParam.Add(new SqlParameter("@Rank", ""));
              objParam.Add(new SqlParameter("@Department", ""));
              objDA.sqlCmdText = "hrm_IrisTransactionLog_Insert_Update";
              objDA.sqlParam = objParam.ToArray();

              int val = int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              _DeviceInstance.DisConnect();
              throw ex;
          }
      }
      private void SaveiriscodebackupDetails(IRISINFO irisinfoR,IRISINFO irisinfoL,string Userid, string irisId, iCAM70003SDKC _DeviceInstance, int backupid)
      {
          try
          {
              int backid = backupid;
              string uid = Userid;
              string iriscode = IrisUtility.ConvertByteArrToString(irisinfoR.IrisCode);//image type
              string iriscodever = irisinfoR.IrisCodeVersion.ToString();
              string leftryt =(irisinfoR.LeftRight).ToString();
            
              DataAccess objDA = new DataAccess();


              List<SqlParameter> objParam = new List<SqlParameter>();

              objParam.Add(new SqlParameter("@BackupID", backupid));
              objParam.Add(new SqlParameter("@IrisCodeID", irisId));
              objParam.Add(new SqlParameter("@UUID", ""));
           
              objParam.Add(new SqlParameter("@IrisCode", iriscode));
              objParam.Add(new SqlParameter("@IrisCodeVersion", iriscodever));
        
              objParam.Add(new SqlParameter("@LeftRight", leftryt));
              objParam.Add(new SqlParameter("@DeleteInfo", ""));

              objParam.Add(new SqlParameter("@IrisCodeInfoID", ""));
              objParam.Add(new SqlParameter("@IrisCodeType", ""));

              objDA.sqlCmdText = "hrm_IrisCode_Insert_Update";
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
      public DataTable getUserID(int backupid)
      {
          DataAccess objDA = new DataAccess();
          SqlParameter[] objParam = new SqlParameter[1];
          try
          {
              if (backupid > 0)
              {
                  objParam[0] = new SqlParameter("@BackUpID", backupid);
                  objDA.sqlParam = objParam;
              }

              objDA.sqlCmdText = "hrm_Iris_SelectUseronBackupID";
              return objDA.ExecuteDataSet().Tables[0];
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }

    }
}
