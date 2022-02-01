using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class CommissionDAL
    {
        public int Save(CommissionBOL objCommission)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();   
            try
            {
                objParam.Add(new SqlParameter("@CommissionId", objCommission.CommissionId));
                objParam.Add(new SqlParameter("@EmployeeId", objCommission.EmployeeId));
                objParam.Add(new SqlParameter("@Title", objCommission.Title));
                objParam.Add(new SqlParameter("@Amount", objCommission.Amount));
                objParam.Add(new SqlParameter("@CommissionDate", objCommission.CommissionDate));
                objParam.Add(new SqlParameter("@CommissionDateAR", objCommission.CommissionDateAR));
                objParam.Add(new SqlParameter("@Description", objCommission.Description));
                objParam.Add(new SqlParameter("@AdditionalInfo", objCommission.AdditionalInfo));
                objParam.Add(new SqlParameter("@Status", objCommission.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objCommission.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objCommission.ModifiedBy));
                objDA.sqlCmdText = "hrm_Commission_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nCommissionId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@CommissionId", nCommissionId));
                objDA.sqlCmdText = "hrm_Commission_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(CommissionBOL objBOL)
        {
            DataTable dtCommission = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.CommissionId > 0)
                        objParam.Add(new SqlParameter("@CommissionId", objBOL.CommissionId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Commission_SelectAll";
                dtCommission = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtCommission = new DataTable();
            }
            return dtCommission;
        }

        public CommissionBOL SearchById(int nCommissionId)
        {
            CommissionBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nCommissionId > 0)
                {
                    objBOL = new CommissionBOL();
                    objBOL.CommissionId = nCommissionId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.CommissionId = nCommissionId;
                        objBOL.EmployeeId = Util.ToInt("" + dt.Rows[0]["EmployeeId"]);
                        objBOL.Title = "" + dt.Rows[0]["Title"];
                        objBOL.Amount = Util.ToDecimal("" + dt.Rows[0]["Amount"]);
                        objBOL.CommissionDate = "" + dt.Rows[0]["CommissionDate"];
                        objBOL.CommissionDateAR = "" + dt.Rows[0]["CommissionDateAR"];
                        objBOL.Description = "" + dt.Rows[0]["Description"];
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
