using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.DAL;
using HRM.BOL;


namespace HRM.BAL
{
    public class PayrollPeriodBAL
    {
        public int Save(PayrollPeriodBOL objPayrollPeriod)
        {
            PayrollPeriodDAL objDAL = new PayrollPeriodDAL();
            return objDAL.Save(objPayrollPeriod);
        }

        public int Delete(int nPPId)
        {
            PayrollPeriodDAL objDAL = new PayrollPeriodDAL();
            return objDAL.Delete(nPPId);
        }

        public DataTable SelectAll(PayrollPeriodBOL objPayrollPeriod)
        {
            PayrollPeriodDAL objDAL = new PayrollPeriodDAL();
            return objDAL.SelectAll(objPayrollPeriod);
        }

        public PayrollPeriodBOL SearchById(int nPPId)
        {
            PayrollPeriodDAL objDAL = new PayrollPeriodDAL();
            return objDAL.SearchById(nPPId);
        }
    }
}
