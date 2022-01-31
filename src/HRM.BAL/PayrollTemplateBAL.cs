using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class PayrollTemplateBAL
    {
        public int Save(PayrollMasterBOL objPayrollTemplate)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.Save(objPayrollTemplate);
        }
        #region Generate Monthlywise Salary slip
        public int PayslipSave(PayrollSalaryslipBOL objPayrollTemplate)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.SavePayrollPayslip(objPayrollTemplate);
        }
        public int SavePayrollPayslipAllowance(PayrollSalaryslipBOL objPayrollTemplate)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.SavePayrollPayslipAllowance(objPayrollTemplate);
        }
        public int SavePayrollPayslipDeduction(PayrollSalaryslipBOL objPayrollTemplate)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.SavePayrollPayslipDeduction(objPayrollTemplate);
        }
        #endregion
        public DataSet SelectAll(PayrollMasterBOL objPayrollMaster)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.SelectPayrollTemplate(objPayrollMaster);
        }
        public DataTable Check_Salaryslip(PayrollSalaryslipBOL objPayrollMaster)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.Check_Salaryslip(objPayrollMaster);
        }
        public DataSet PayrollTemplateforSalarySlip(PayrollMasterBOL objBOL, int nMonth, int nYear)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.PayrollTemplateforSalarySlip(objBOL, nMonth, nYear);
        }

        public DataTable SelectAllPayrollTemplates()
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.SelectAllPayrollTemplates();
        }
        public DataSet PayrollSalarySlip(PayrollMasterBOL objBOL, int nMonth, int nYear)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.PayrollSalarySlip(objBOL, nMonth, nYear);
        }
        public int Delete(int nPMId)
        {
            PayrollTemplateDAL objDAL = new PayrollTemplateDAL();
            return objDAL.Delete(nPMId);
        }
    }
}
