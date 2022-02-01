using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class HourlyWageDAL
    {
        public int Save(HourlyWageBOL objHourlyWage)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@HourlyWageId", objHourlyWage.HourlyWageId));
                objParam.Add(new SqlParameter("@DesignationId", objHourlyWage.DesignationId));
                objParam.Add(new SqlParameter("@EmployeeId", objHourlyWage.EmployeeId));
                objParam.Add(new SqlParameter("@RegularHours", objHourlyWage.RegularHours));
                objParam.Add(new SqlParameter("@OverTimeHours", objHourlyWage.OverTimeHours));
                objParam.Add(new SqlParameter("@OverTimeWekend", objHourlyWage.OverTimewekend));
                objParam.Add(new SqlParameter("@AdditionalInfo", objHourlyWage.AdditionalInfo));
                objParam.Add(new SqlParameter("@HMonth", objHourlyWage.HMonth));
                objParam.Add(new SqlParameter("@HYear", objHourlyWage.HYear));
                objParam.Add(new SqlParameter("@ActiveWage", objHourlyWage.ActiveWage));
                objParam.Add(new SqlParameter("@Status", objHourlyWage.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objHourlyWage.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objHourlyWage.ModifiedBy));
                objParam.Add(new SqlParameter("@HourlyWagePresent", objHourlyWage.HourlyWagePresent));
                objDA.sqlCmdText = "hrm_HourlyWage_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nHourlyWageId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@HourlyWagesId", nHourlyWageId));
                objDA.sqlCmdText = "hrm_HourlyWages_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(HourlyWageBOL objBOL)
        {
            DataTable dtHourlyWage = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if(objBOL.DesignationId>0)
                        objParam.Add(new SqlParameter("@DesignationId",objBOL.DesignationId));
                    if (objBOL.HourlyWageId > 0)
                        objParam.Add(new SqlParameter("@HourlyWageId", objBOL.HourlyWageId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    if (objBOL.ActiveWage != "")
                        objParam.Add(new SqlParameter("@ActiveWage", objBOL.ActiveWage));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_HourlyWages_SelectAll";
                dtHourlyWage = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtHourlyWage = new DataTable();
            }
            return dtHourlyWage;
        }

        public HourlyWageBOL SearchById(int nHourlyWageId)
        {
            HourlyWageBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nHourlyWageId > 0)
                {
                    objBOL = new HourlyWageBOL();
                    objBOL.HourlyWageId = nHourlyWageId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.HourlyWageId = nHourlyWageId;
                        objBOL.DesignationId = Util.ToInt("" + dt.Rows[0]["DesignationId"]);
                        objBOL.EmployeeId = Util.ToInt("" + dt.Rows[0]["EmployeeId"]);
                        objBOL.RegularHours = Util.ToDecimal("" + dt.Rows[0]["RegularHours"]);
                        objBOL.OverTimeHours = Util.ToDecimal("" + dt.Rows[0]["OverTimeHours"]);
                        objBOL.OverTimewekend = Util.ToDecimal("" + dt.Rows[0]["OverTimeWekend"]);
                        objBOL.AdditionalInfo = "" + dt.Rows[0]["AdditionalInfo"];
                        objBOL.ActiveWage = "" + dt.Rows[0]["ActiveWage"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
                        objBOL.CreatedDate = Util.ToDateTime("" + dt.Rows[0]["CreatedDate"]);
                        objBOL.ModifiedBy = "" + dt.Rows[0]["ModifiedBy"];
                        objBOL.ModifiedDate = Util.ToDateTime("" + dt.Rows[0]["ModifiedDate"]);
                        objBOL.FirstName = "" + dt.Rows[0]["FirstName"];
                        objBOL.MiddleName = "" + dt.Rows[0]["MiddleName"];
                        objBOL.LastName = "" + dt.Rows[0]["LastName"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }

        public int ActiveHourWagePresent(int nEmployeeId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@EmployeeId", nEmployeeId));
                objDA.sqlCmdText = "hrm_ActiveHourWage_Present";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
