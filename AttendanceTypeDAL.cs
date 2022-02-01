using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class AttendanceTypeDAL
    {
        public int Save(AttendanceTypeBOL objAttendanceType)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@ATId", objAttendanceType.ATId));
                objParam.Add(new SqlParameter("@AttendanceType", objAttendanceType.AttendanceType));
                objParam.Add(new SqlParameter("@Category", objAttendanceType.Category));
                objParam.Add(new SqlParameter("@TypeKind", objAttendanceType.TypeKind));
                objParam.Add(new SqlParameter("@ATCode", objAttendanceType.ATCode));
                objParam.Add(new SqlParameter("@Status", objAttendanceType.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objAttendanceType.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objAttendanceType.ModifiedBy));
                objDA.sqlCmdText = "hrm_Attendance_Type_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return Util.ToInt(objDA.ExecuteScalar().ToString());
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int nATId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@ATId", nATId));
                objDA.sqlCmdText = "hrm_Attendance_Type_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(AttendanceTypeBOL objBOL)
        {
            DataTable dtAttendanceType = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.ATId > 0)
                        objParam.Add(new SqlParameter("@ATId", objBOL.ATId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Attendance_Type_SelectAll";
                dtAttendanceType = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtAttendanceType = new DataTable();
            }
            return dtAttendanceType;
        }

        public AttendanceTypeBOL SearchById(int nATId)
        {
            AttendanceTypeBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nATId > 0)
                {
                    objBOL = new AttendanceTypeBOL();
                    objBOL.ATId = nATId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.ATId = nATId;
                        objBOL.AttendanceType = "" + dt.Rows[0]["AttendanceType"];
                        objBOL.Category = "" + dt.Rows[0]["Category"];
                        objBOL.TypeKind = "" + dt.Rows[0]["TypeKind"];
                        objBOL.ATCode = "" + dt.Rows[0]["ATCode"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
                        objBOL.CreatedDate = Util.ToDateTime("" + dt.Rows[0]["CreatedDate"]);
                        objBOL.ModifiedBy = "" + dt.Rows[0]["ModifiedBy"];
                        objBOL.ModifiedDate = Util.ToDateTime("" + dt.Rows[0]["ModifiedDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }


    }
}
