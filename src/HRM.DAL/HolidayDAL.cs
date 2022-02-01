using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using iCAM70003SDKCLib;

namespace HRM.DAL
{
    public class HolidayDAL
    {
        private iCAMManagerAPI m_DeviceControl;
        HOLIDAYINFO stHolidayInfo;
        iCAM70003SDKC _DeviceInstance = null;
        CONNECTION_OPTIONS _ConnectionOptions; 

        public int Save(HolidayBOL objHo)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@HolidayID", objHo.HolidayID);
                objParam[++i] = new SqlParameter("@BranchID", objHo.BranchID);
                objParam[++i] = new SqlParameter("@Description", objHo.Description);
                objParam[++i] = new SqlParameter("@Holiday", objHo.Holiday);
                objParam[++i] = new SqlParameter("@Comments", objHo.Comments);
                objParam[++i] = new SqlParameter("@Status", objHo.Status);
                objParam[++i] = new SqlParameter("@CreatedBy", objHo.CreatedBy);

                objDA.sqlCmdText = "hrm_Holidays_Insert_Update";
                objDA.sqlParam = objParam;
                int val =  int.Parse(objDA.ExecuteScalar().ToString());

                int iHID;
                int iRetVal = 0;
                int _ConnectionID = 0;
                _DeviceInstance = new iCAM70003SDKC();
                _ConnectionOptions = new CONNECTION_OPTIONS();
                _ConnectionOptions.lUserOptions = (int)EUserOptions.ALL_USER_OPTIONS_ENABLED;
                _ConnectionOptions.lLogOptions = (int)ELogOptions.ALL_LOG_OPTIONS_ENABLED;

                int iResult = _DeviceInstance.Connect("192.168.1.5", "1111111111111111", "iCAM7000", "iris7000", ref _ConnectionOptions, out _ConnectionID);
                if (iResult == 0)
                {
                    m_DeviceControl = new iCAMManagerAPI();
                    m_DeviceControl.IPAddress = "192.168.1.5";
                    m_DeviceControl.SecurityID = "1111111111111111";
                    m_DeviceControl.UserName = "iCAM7000";
                    m_DeviceControl.Password = "iris7000";

                    string dt = objHo.Holiday.ToString("dd-MMM-yyyy");
                    stHolidayInfo = HRM.DAL.IrisUtility.SetHolidayDetails(objHo.Description, dt);
                    iRetVal = _DeviceInstance.AddHoliday(stHolidayInfo, out iHID);
                    if (iRetVal != HRM.DAL.ErrorConstants.ICAMSDK_ERROR_NONE)
                    {
                        string error = string.Format(@"ErrorCode : 0x{0:X}", iRetVal) + Environment.NewLine + "Description: " + ErrorHandler.ProcessError(iRetVal, string.Empty) + "." + Constants.TITLE;
                        //return;
                    }

                    int iResult1 = _DeviceInstance.DisConnect();
                }


                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nHolidayID, string sCreatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@HolidayID", nHolidayID);
                objParam[1] = new SqlParameter("@CreatedBy", sCreatedBy);
                objDA.sqlCmdText = "hrm_Holidays_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                objDA.sqlCmdText = "hrm_Holidays_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHolidays(int branch, int month, int year)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;

            try
            {
                if(branch>0)
                    objParam[++i] = new SqlParameter("@branchid", branch);

                if (month > 0)
                    objParam[++i] = new SqlParameter("@month", month);

                if (year > 0)
                    objParam[++i] = new SqlParameter("@year", year);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Holidays_Select_Month_Year";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
