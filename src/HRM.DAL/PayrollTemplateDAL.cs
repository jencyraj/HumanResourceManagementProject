using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class PayrollTemplateDAL
    {
        public int Save(PayrollMasterBOL objPayrollMaster)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                int PMId = 0;
                objParam.Add(new SqlParameter("@PMId", objPayrollMaster.PMId));
                objParam.Add(new SqlParameter("@DesignationId", objPayrollMaster.DesignationId));
                if (objPayrollMaster.EmployeeId > 0)
                    objParam.Add(new SqlParameter("@EmployeeId", objPayrollMaster.EmployeeId));
                objParam.Add(new SqlParameter("@BasicSalary", objPayrollMaster.BasicSalary));
                objParam.Add(new SqlParameter("@gosiamount", objPayrollMaster.gosiamount));
                objParam.Add(new SqlParameter("@gositype", objPayrollMaster.gositype));
                objParam.Add(new SqlParameter("@gosialwtype", objPayrollMaster.gosialwtype));
                objParam.Add(new SqlParameter("@PPId", objPayrollMaster.PPId));
                objParam.Add(new SqlParameter("@Status", objPayrollMaster.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objPayrollMaster.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objPayrollMaster.ModifiedBy));
                objDA.sqlCmdText = "hrm_Payroll_Master_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                PMId = Util.ToInt(objDA.ExecuteScalar().ToString());
                if (PMId > 0)
                {
                    CheckPayroll(PMId);
                    SavePayrollAllowances(objPayrollMaster.PayrollAllowances, PMId);
                    SavePayrollDeductions(objPayrollMaster.PayrollDeductions, PMId);
                    SavePayrollTaxRules(objPayrollMaster.PayrollTaxRules, PMId);
                }
                return PMId;
            }
            catch
            {
                return 0;
            }
        }

        private void SavePayrollAllowances(List<PayrollAllowanceBOL> PayrollAllowances, int PMId)
        {
            foreach (PayrollAllowanceBOL PayrollAllowance in PayrollAllowances)
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                try
                {
                    objParam.Add(new SqlParameter("@PAId", PayrollAllowance.PAId));
                    objParam.Add(new SqlParameter("@PMId", PMId));
                    objParam.Add(new SqlParameter("@AllowanceId", PayrollAllowance.AllowanceId));
                    objParam.Add(new SqlParameter("@Taxable", PayrollAllowance.Taxable));
                    objParam.Add(new SqlParameter("@AlwType", PayrollAllowance.AlwType));
                    objParam.Add(new SqlParameter("@AlwAmount", PayrollAllowance.AlwAmount));
                    objParam.Add(new SqlParameter("@Status","Y"));
                    objParam.Add(new SqlParameter("@CreatedBy", PayrollAllowance.CreatedBy));
                   // objParam.Add(new SqlParameter("@ModifiedBy", PayrollAllowance.ModifiedBy));
                    objParam.Add(new SqlParameter("@AlwName", PayrollAllowance.AlwName));
                    objParam.Add(new SqlParameter("@AlwCode", PayrollAllowance.AlwCode));
                    objDA.sqlCmdText = "hrm_Payroll_Allowance_INSERT_UPDATE";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void SavePayrollDeductions(List<PayrollDeductionBOL> PayrollDeductions, int PMId)
        {
            foreach (PayrollDeductionBOL PayrollDeduction in PayrollDeductions)
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                try
                {
                    objParam.Add(new SqlParameter("@PDId", PayrollDeduction.PDId));
                    objParam.Add(new SqlParameter("@PMId", PMId));
                    objParam.Add(new SqlParameter("@DeductionId", PayrollDeduction.DeductionId));
                    objParam.Add(new SqlParameter("@DedType", PayrollDeduction.DedType));
                    objParam.Add(new SqlParameter("@DedAmount", PayrollDeduction.DedAmount));
                    objParam.Add(new SqlParameter("@Status", "Y"));
                    objParam.Add(new SqlParameter("@CreatedBy", PayrollDeduction.CreatedBy));
                  // objParam.Add(new SqlParameter("@ModifiedBy", PayrollDeduction.ModifiedBy));
                    objParam.Add(new SqlParameter("@DedName", PayrollDeduction.DedName));
                    objParam.Add(new SqlParameter("@DedCode", PayrollDeduction.DedCode));
                    objParam.Add(new SqlParameter("@TaxExemption", PayrollDeduction.TaxExemption));
                    objDA.sqlCmdText = "hrm_Payroll_Deduction_INSERT_UPDATE";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void SavePayrollTaxRules(List<PayrollTaxRuleBOL> PayrollTaxRules, int PMId)
        {
            foreach (PayrollTaxRuleBOL PayrollTaxRule in PayrollTaxRules)
            {
                DataAccess objDA = new DataAccess();
                List<SqlParameter> objParam = new List<SqlParameter>();
                try
                {
                    objParam.Add(new SqlParameter("@PTId", PayrollTaxRule.PTId));
                    objParam.Add(new SqlParameter("@PMId", PMId));
                    objParam.Add(new SqlParameter("@SalaryFrom", PayrollTaxRule.SalaryFrom));
                    objParam.Add(new SqlParameter("@SalaryTo", PayrollTaxRule.SalaryTo));
                    objParam.Add(new SqlParameter("@TaxPercentage", PayrollTaxRule.TaxPercentage));
                    objParam.Add(new SqlParameter("@ExemptedTaxAmount", PayrollTaxRule.ExemptedTaxAmount));
                    objParam.Add(new SqlParameter("@AdditionalTaxAmount", PayrollTaxRule.AdditionalTaxAmount));
                    objParam.Add(new SqlParameter("@Gender", PayrollTaxRule.Gender));
                    objParam.Add(new SqlParameter("@Status", PayrollTaxRule.Status));
                    objParam.Add(new SqlParameter("@CreatedBy", PayrollTaxRule.CreatedBy));
                    objParam.Add(new SqlParameter("@ModifiedBy", PayrollTaxRule.ModifiedBy));
                    objParam.Add(new SqlParameter("@TaxRuleId", PayrollTaxRule.TaxRuleId));
                    objDA.sqlCmdText = "hrm_Payroll_TaxRule_INSERT_UPDATE";
                    objDA.sqlParam = objParam.ToArray();
                    objDA.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #region Generate Monthlywise Salary slip
        public int SavePayrollPayslip(PayrollSalaryslipBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@Id", objBOL.Id));
                objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                objParam.Add(new SqlParameter("@BasicSalary", objBOL.BasicSalary));
                objParam.Add(new SqlParameter("@PMID", objBOL.PMID));
                //objParam.Add(new SqlParameter("@DedAmount", objBOL.Deduction));
               // objParam.Add(new SqlParameter("@Tax", objBOL.Tax));
                objParam.Add(new SqlParameter("@Month", objBOL.Month));
                objParam.Add(new SqlParameter("@Year", objBOL.Year));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@CreatedBy", objBOL.CreatedBy));

                objDA.sqlCmdText = "[hrm_Payroll_SalarySlip_INSERT_UPDATE]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SavePayrollPayslipAllowance(PayrollSalaryslipBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@SID", objBOL.Id));
                objParam.Add(new SqlParameter("@AID", objBOL.AID));
                objParam.Add(new SqlParameter("@AllowanceID", objBOL.AllowanceID));
                objParam.Add(new SqlParameter("@Taxable", objBOL.ATax));
                objParam.Add(new SqlParameter("@AlwType", objBOL.AllowanceType));
                objParam.Add(new SqlParameter("@AlwAmount", objBOL.AllowanceAmount));

                objDA.sqlCmdText = "[hrm_Payroll_SalarySlip_Allowance_INSERT_UPDATE]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SavePayrollPayslipDeduction(PayrollSalaryslipBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@SID", objBOL.Id));
                objParam.Add(new SqlParameter("@DID", objBOL.DID));
                objParam.Add(new SqlParameter("@DeductionID", objBOL.DeductionID));
                objParam.Add(new SqlParameter("@TaxExemption", objBOL.DTax));
                objParam.Add(new SqlParameter("@DedType", objBOL.DeductionType));
                objParam.Add(new SqlParameter("@DedAmount", objBOL.DeductionAmount));

                objDA.sqlCmdText = "[hrm_Payroll_SalarySlip_Deduction_INSERT_UPDATE]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataSet SelectPayrollTemplate(PayrollMasterBOL objBOL)
        {
            DataSet dsPT = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    objParam.Add(new SqlParameter("@PMId", objBOL.PMId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_PayrollTemplate_Select";
                dsPT = objDA.ExecuteDataSet();
            }
            catch
            {
                dsPT = new DataSet();
            }
            return dsPT;
        }

        public DataTable SelectAllPayrollTemplates()
        {
            DataTable dt = null;
            DataAccess objDA = new DataAccess();
            try
            {
                objDA.sqlCmdText = "hrm_PayrollTemplates_SelectAll";
                dt = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dt = new DataTable();
            }
            return dt;
        }
        public DataTable Check_Salaryslip(PayrollSalaryslipBOL objBOL)
        {
            DataTable dsPT = new DataTable();
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objParam.Add(new SqlParameter("@Month", objBOL.Month));
                    objParam.Add(new SqlParameter("@Year", objBOL.Year));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "[hrm_Payroll_SalarySlip_CheckExist]";
                dsPT = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dsPT = new DataTable();
            }
            return dsPT;
        }

        public DataSet PayrollTemplateforSalarySlip(PayrollMasterBOL objBOL, int nMonth, int nYear)
        {
            DataSet dsPT = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    objParam.Add(new SqlParameter("@DesignationId", objBOL.DesignationId));
                    objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                }
                objParam.Add(new SqlParameter("@BranchID", objBOL.BranchID));
                objParam.Add(new SqlParameter("@Month", nMonth));
                objParam.Add(new SqlParameter("@Year", nYear));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_PayrollTemplate_Defined";
                dsPT = objDA.ExecuteDataSet();
            }
            catch
            {
                dsPT = new DataSet();
            }
            return dsPT;
        }
        public DataSet PayrollSalarySlip(PayrollMasterBOL objBOL, int nMonth, int nYear)
        {
            DataSet dsPT = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
               
                objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                objParam.Add(new SqlParameter("@PMId", objBOL.PMId));
                objParam.Add(new SqlParameter("@sMon", nMonth));
                objParam.Add(new SqlParameter("@Year", nYear));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "[hrm_Payroll_Payslip_SelectAll]";
                dsPT = objDA.ExecuteDataSet();
            }
            catch
            {
                dsPT = new DataSet();
            }
            return dsPT;
        }
        private int CheckPayroll(int nPMId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@PMId", nPMId));
                objDA.sqlCmdText = "[hrm_Payroll_Checkpayroll]";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int nPMId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@PMId", nPMId));
               // objDA.sqlCmdText = "hrm_PayrollTemplate_Delete";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
