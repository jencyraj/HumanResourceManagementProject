using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class TaxRuleDAL
    {
        public int Save(TaxRuleBOL objTaxRule)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@TaxRuleId", objTaxRule.TaxRuleId));
                objParam.Add(new SqlParameter("@SalaryFrom", objTaxRule.SalaryFrom));
                objParam.Add(new SqlParameter("@SalaryTo", objTaxRule.SalaryTo));
                objParam.Add(new SqlParameter("@TaxPercentage", objTaxRule.TaxPercentage));
                objParam.Add(new SqlParameter("@ExemptedTaxAmount", objTaxRule.ExemptedTaxAmount));
                objParam.Add(new SqlParameter("@AdditionalTaxAmount", objTaxRule.AdditionalTaxAmount));
                objParam.Add(new SqlParameter("@Gender", objTaxRule.Gender));
                objParam.Add(new SqlParameter("@Status", objTaxRule.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objTaxRule.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objTaxRule.ModifiedBy));
                objDA.sqlCmdText = "hrm_TaxRule_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nTaxRuleId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@TaxRuleId", nTaxRuleId));
                objDA.sqlCmdText = "hrm_TaxRule_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(TaxRuleBOL objBOL)
        {
            DataTable dtTaxRule = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.TaxRuleId > 0)
                        objParam.Add(new SqlParameter("@TaxRuleId", objBOL.TaxRuleId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_TaxRule_SelectAll";
                dtTaxRule = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtTaxRule = new DataTable();
            }
            return dtTaxRule;
        }

        public TaxRuleBOL SearchById(int nTaxRuleId)
        {
            TaxRuleBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nTaxRuleId > 0)
                {
                    objBOL = new TaxRuleBOL();
                    objBOL.TaxRuleId = nTaxRuleId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.TaxRuleId = nTaxRuleId;
                        objBOL.SalaryFrom = Util.ToDecimal(dt.Rows[0]["SalaryFrom"]);
                        objBOL.SalaryTo = Util.ToDecimal(dt.Rows[0]["SalaryTo"]);
                        objBOL.TaxPercentage = Util.ToDecimal(dt.Rows[0]["TaxPercentage"]);
                        objBOL.ExemptedTaxAmount = Util.ToDecimal(dt.Rows[0]["ExemptedTaxAmount"]);
                        objBOL.AdditionalTaxAmount = Util.ToDecimal(dt.Rows[0]["AdditionalTaxAmount"]);
                        objBOL.Gender = "" + dt.Rows[0]["Gender"];
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
