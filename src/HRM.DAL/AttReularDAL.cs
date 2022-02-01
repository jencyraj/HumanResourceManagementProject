using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using HRM.BOL;

namespace HRM.DAL
{
    public class AttRegularDAL
    {
        public int Save(AttRegularBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", objBOL.ReqID));
            objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
            objParam.Add(new SqlParameter("@ReqMonth", objBOL.ReqMonth));
            objParam.Add(new SqlParameter("@ReqYear", objBOL.ReqYear));
            objParam.Add(new SqlParameter("@ReqReason", objBOL.ReqReason));
            objParam.Add(new SqlParameter("@Status", "Y"));
            objParam.Add(new SqlParameter("@CreatedBy", objBOL.CreatedBy));

            objDA.sqlCmdText = "hrm_Attendance_Request_Insert";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        public int Approve(int ReqID, string Approved_Rejected_By, string ApprovalStatus, string RejectReason)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ReqID));
            objParam.Add(new SqlParameter("@ApprovedBy", Approved_Rejected_By));
            objParam.Add(new SqlParameter("@RejectReason", RejectReason));
            objParam.Add(new SqlParameter("@Status", ApprovalStatus));

            objDA.sqlCmdText = "hrm_Attendance_Request_Approve";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        public int Delete(int ReqID, string DeletedBy)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ReqID));
            objParam.Add(new SqlParameter("@CreatedBy", DeletedBy));

            objDA.sqlCmdText = "hrm_Attendance_Request_Delete";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }
        public DataTable GetPayable_Overtime()
        {
            DataTable dtOvertime = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                 objDA.sqlParam = objParam.ToArray();

                 objDA.sqlCmdText = "[hrm_GetPayable_Overtime]";
                 dtOvertime = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtOvertime = new DataTable();
            }
            return dtOvertime;
        }
        public int Detail_Save(AttRegularBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ReqID", objWP.ReqID);
                objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                objParam[++i] = new SqlParameter("@Reqmon", objWP.ReqMonth);
                objParam[++i] = new SqlParameter("@Reqyear", objWP.ReqYear);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Reqreason", objWP.ReqReason);
                objParam[++i] = new SqlParameter("@Status", objWP.Status);

               // objParam[++i] = new SqlParameter("@DtAttendance", objWP.DTAttendance);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_AttendaneRegularize_INSERT]";

                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ReqDetail_Save(AttRegularBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ReqID", objWP.ReqID);
                objParam[++i] = new SqlParameter("@DetailID", objWP.ReqDetailID);
                objParam[++i] = new SqlParameter("@AttendanceDate", objWP.AttendanceDate);
                objParam[++i] = new SqlParameter("@AttendanceDay", objWP.AttendanceDay);
                objParam[++i] = new SqlParameter("@AttendanceMon", objWP.AttendanceMon);
                objParam[++i] = new SqlParameter("@Attendanceyear", objWP.Attendanceyear);

                // objParam[++i] = new SqlParameter("@DtAttendance", objWP.DTAttendance);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_AttendaneRegularize_details_INSERT]";

                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Update(AttRegularBOL objWP)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@ReqID", objWP.ReqID);
                objParam[++i] = new SqlParameter("@EmployeeID", objWP.EmployeeID);
                objParam[++i] = new SqlParameter("@Reqmon", objWP.ReqMonth);
                objParam[++i] = new SqlParameter("@Reqyear", objWP.ReqYear);
                objParam[++i] = new SqlParameter("@CreatedBy", objWP.CreatedBy);
                objParam[++i] = new SqlParameter("@Reqreason", objWP.ReqReason);
                objParam[++i] = new SqlParameter("@Status", objWP.Status);

                objParam[++i] = new SqlParameter("@DtAttendance", objWP.DTAttendance);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_AttendaneRegularize_Update]";



                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApproveRequest(AttRegularBOL objBOL)
        {

            try
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                
                objParam.Add(new SqlParameter("@ReqID", objBOL.ReqID));
                objParam.Add(new SqlParameter("@ReqDetailID", objBOL.ReqDetailID));
                objParam.Add(new SqlParameter("@EmpID", objBOL.EmployeeID));
                objParam.Add(new SqlParameter("@AttendanceID", objBOL.AttendanceId));
                objParam.Add(new SqlParameter("@AttendanceDate", objBOL.AttendanceDate));
                objParam.Add(new SqlParameter("@signin", objBOL.StartTime));
                objParam.Add(new SqlParameter("@signout", objBOL.EndTime));
                objParam.Add(new SqlParameter("@status", "Y"));
                objParam.Add(new SqlParameter("@ApprovedBy", objBOL.ApprovedBy));
                objParam.Add(new SqlParameter("@ApprovedStatus", objBOL.ApprovedStatus));
                objParam.Add(new SqlParameter("@RejectReason", objBOL.RejectReason));
                objParam.Add(new SqlParameter("@WSID", objBOL.WorkShiftId));
                objDA.sqlCmdText = "[hrm_Regularize_Request_Approve]";
                objDA.sqlParam = objParam.ToArray();



                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Approve(int reqID,int attndncID, int empID,DateTime Date,int signin,int signout,string status,DataTable DT)
        {
           
            try
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();

                objParam.Add(new SqlParameter("@DtAttendance", DT));
                objParam.Add(new SqlParameter("@Req", reqID));
                objParam.Add(new SqlParameter("@EmpID", empID));
                objParam.Add(new SqlParameter("@AttendancesDate", Date));
                objParam.Add(new SqlParameter("@signin", signin));
                objParam.Add(new SqlParameter("@signout", signout));
                objParam.Add(new SqlParameter("@status", status));
                objParam.Add(new SqlParameter("@AttendanceID", attndncID));
                objDA.sqlCmdText = "[hrm_Regularize_Request_Approve_OLD]";
                objDA.sqlParam = objParam.ToArray();



                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Select(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));

            objDA.sqlCmdText = "hrm_Attendance_Request_Select";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }
        public DataTable SelectBy(int ID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ID));

            objDA.sqlCmdText = "hrm_Attendance_Request_SelectBy";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }
        public DataTable ShowAttendance(int EmployeeID,int Mon,int year)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@EmpID", EmployeeID));
            objParam.Add(new SqlParameter("@salmonth", Mon));
            objParam.Add(new SqlParameter("@Salyear", year));
            objDA.sqlCmdText = "[hrm_Employee_TimeSheet]";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }
        public DataSet TimeSheet_CheckAttendance(int EmployeeID, int Mon, int year)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@EmpID", EmployeeID));
            objParam.Add(new SqlParameter("@salmonth", Mon));
            objParam.Add(new SqlParameter("@Salyear", year));
            objDA.sqlCmdText = "[hrm_TimeSheet_CheckAttendance]";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet();
        }
       
        public AttRegularBOL SelectByID(int ReqID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ReqID));

            objDA.sqlCmdText = "hrm_Attendance_Request_SelectByID";
            objDA.sqlParam = objParam.ToArray();

            SqlDataReader dReader = objDA.ExecuteDataReader();

            AttRegularBOL objBOL = null;

            while (dReader.Read())
            {
                objBOL = new AttRegularBOL();
              
               // objBOL.ApprovedStatus = "" + dReader["ApprovedStatus"];
                objBOL.CreatedBy = "" + dReader["CreatedBy"];
                objBOL.EmployeeID = Util.ToInt(dReader["EmployeeID"]);
              //  objBOL.RejectReason = "" + dReader["RejectReason"];
                objBOL.ReqMonth = Util.ToInt(dReader["ReqMonth"]);
                objBOL.ReqReason = "" + dReader["ReqReason"];
                objBOL.ReqYear = Util.ToInt(dReader["ReqYear"]);
                objBOL.Status = "" + dReader["Status"];

            }

            return objBOL;
        }

        public void ClearRequestDetail(int ReqID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ReqID));

            objDA.sqlCmdText = "hrm_Attendancerequest_detail_Clear";
            objDA.sqlParam = objParam.ToArray();

            objDA.ExecuteNonQuery();
        }


        public void CloseRequest(int ReqID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ReqID", ReqID));

            objDA.sqlCmdText = "hrm_Attendance_Request_Close";
            objDA.sqlParam = objParam.ToArray();

            objDA.ExecuteNonQuery();
        }
    }
}
