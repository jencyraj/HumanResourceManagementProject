using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace HRM.BOL
{
    public class PayrollMasterBOL
    {
        public int PMId;
        public int DesignationId;
        public int EmployeeId;
        public int BranchID;
	    public decimal BasicSalary;
        public decimal gosiamount;
        public string gositype;
        public int gosialwtype;
        public int PPId;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public List<PayrollAllowanceBOL> PayrollAllowances = new List<PayrollAllowanceBOL>();
        public List<PayrollDeductionBOL> PayrollDeductions = new List<PayrollDeductionBOL>();
        public List<PayrollTaxRuleBOL> PayrollTaxRules = new List<PayrollTaxRuleBOL>();
    }

    public class PayrollAllowanceBOL
    {
        public int PAId;
        public int PMId;
        public int AllowanceId;
        public string Taxable;
        public string AlwType;
        public decimal AlwAmount;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public string AlwName;
        public string AlwCode;
    }

    public class PayrollDeductionBOL
    {
        public int PDId;
        public int PMId;
        public int DeductionId;
	    public string DedType;
	    public decimal DedAmount;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public string DedName;
        public string DedCode;
        public string TaxExemption;
    }

    public class PayrollTaxRuleBOL
    {
        public int PTId;
        public int PMId;
        public decimal SalaryFrom;
	    public decimal SalaryTo;
	    public decimal TaxPercentage;
	    public decimal ExemptedTaxAmount;
	    public decimal AdditionalTaxAmount;
        public string Gender;
        public string Status;
        public string CreatedBy;
        public DateTime CreatedDate;
        public string ModifiedBy;
        public DateTime ModifiedDate;
        public int TaxRuleId;
    }

    public class PayrollSalaryslipBOL
    {
        public int Id;
        public int EmployeeId;
        public int PMID;
        public decimal BasicSalary;

        public int DeductionID;
        public int DID;
        public string DeductionType;
        public decimal DeductionAmount;
        public string DTax;

        public int AllowanceID;
        public int AID;
        public string AllowanceType;
        public decimal AllowanceAmount;
        public string ATax;

        public string Tax;
        public int Month;
        public int Year;
        public string Status;
        public string CreatedBy;
        public string ModifiedBy;
        
      
    }
}
