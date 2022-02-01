using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class PayrollPeriodDAL
    {
        public int Save(PayrollPeriodBOL objPayrollPeriod)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@PPId", objPayrollPeriod.PPId));
                objParam.Add(new SqlParameter("@Title", objPayrollPeriod.Title));
                objParam.Add(new SqlParameter("@DayStart", objPayrollPeriod.DayStart));
                objParam.Add(new SqlParameter("@MonthStart", objPayrollPeriod.MonthStart));
                objParam.Add(new SqlParameter("@YearStart", objPayrollPeriod.YearStart));
                objParam.Add(new SqlParameter("@DayEnd", objPayrollPeriod.DayEnd));
                objParam.Add(new SqlParameter("@MonthEnd", objPayrollPeriod.MonthEnd));
                objParam.Add(new SqlParameter("@YearEnd", objPayrollPeriod.YearEnd));
                objParam.Add(new SqlParameter("@Active", objPayrollPeriod.Active));
                objParam.Add(new SqlParameter("@Status", objPayrollPeriod.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objPayrollPeriod.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objPayrollPeriod.ModifiedBy));
                objDA.sqlCmdText = "hrm_Payroll_Period_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return Util.ToInt(objDA.ExecuteScalar().ToString());
            }
            catch
            {
                return 0;   
            }
        }

        public int Delete(int nPPId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@PPId", nPPId));
                objDA.sqlCmdText = "hrm_Payroll_Period_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(PayrollPeriodBOL objBOL)
        {
            DataTable dtPayrollPeriod = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.PPId > 0)
                        objParam.Add(new SqlParameter("@PPId", objBOL.PPId));
                    if (objBOL.Year != null)
                        objParam.Add(new SqlParameter("@Year", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Payroll_Period_SelectAll";
                dtPayrollPeriod = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtPayrollPeriod = new DataTable();
            }
            return dtPayrollPeriod;
        }

        public PayrollPeriodBOL SearchById(int nPPId)
        {
            PayrollPeriodBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nPPId > 0)
                {
                    objBOL = new PayrollPeriodBOL();
                    objBOL.PPId = nPPId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.PPId = nPPId;
                        objBOL.Title = "" + dt.Rows[0]["Title"];
                        objBOL.DayStart = Util.ToInt(dt.Rows[0]["DayStart"]);
                        objBOL.MonthStart = Util.ToInt(dt.Rows[0]["MonthStart"]);
                        objBOL.YearStart = Util.ToInt(dt.Rows[0]["YearStart"]);
                        objBOL.DayEnd = Util.ToInt(dt.Rows[0]["DayEnd"]);
                        objBOL.MonthEnd = Util.ToInt(dt.Rows[0]["MonthEnd"]);
                        objBOL.YearEnd = Util.ToInt(dt.Rows[0]["YearEnd"]);
                        objBOL.Active = "" + dt.Rows[0]["Active"];
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
