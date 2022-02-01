using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class AdvSalaryDAL
    {
        public int Save(AdvSalaryBOL objAdvSalary)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@AdvSalaryId", objAdvSalary.AdvSalaryId));
                objParam.Add(new SqlParameter("@EmployeeId", objAdvSalary.EmployeeId));
                objParam.Add(new SqlParameter("@Title", objAdvSalary.Title));
                objParam.Add(new SqlParameter("@Amount", objAdvSalary.Amount));
                objParam.Add(new SqlParameter("@SalaryDate", objAdvSalary.SalaryDate));
                objParam.Add(new SqlParameter("@SalaryDateAR", objAdvSalary.SalaryDateAR));
                objParam.Add(new SqlParameter("@AdditionalInfo", objAdvSalary.AdditionalInfo));
                objParam.Add(new SqlParameter("@Status", objAdvSalary.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objAdvSalary.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objAdvSalary.ModifiedBy));
                objDA.sqlCmdText = "hrm_AdvSalary_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nAdvSalaryId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@AdvSalaryId", nAdvSalaryId));
                objDA.sqlCmdText = "hrm_AdvSalary_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(AdvSalaryBOL objBOL)
        {
            DataTable dtAdvSalary = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.AdvSalaryId > 0)
                        objParam.Add(new SqlParameter("@AdvSalaryId", objBOL.AdvSalaryId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_AdvSalary_SelectAll";
                dtAdvSalary = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtAdvSalary = new DataTable();
            }
            return dtAdvSalary;
        }

        public AdvSalaryBOL SearchById(int nAdvSalaryId)
        {
            AdvSalaryBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nAdvSalaryId > 0)
                {
                    objBOL = new AdvSalaryBOL();
                    objBOL.AdvSalaryId = nAdvSalaryId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.AdvSalaryId = nAdvSalaryId;
                        objBOL.EmployeeId = Util.ToInt("" + dt.Rows[0]["EmployeeId"]);
                        objBOL.Title = "" + dt.Rows[0]["Title"];
                        objBOL.Amount = Util.ToDecimal("" + dt.Rows[0]["Amount"]);
                        objBOL.SalaryDate = "" + dt.Rows[0]["SalaryDate"];
                        objBOL.SalaryDateAR = "" + dt.Rows[0]["SalaryDateAR"];
                        objBOL.AdditionalInfo = "" + dt.Rows[0]["AdditionalInfo"];
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
    }
}
