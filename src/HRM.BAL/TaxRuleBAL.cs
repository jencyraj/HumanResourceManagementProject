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
    public class TaxRuleBAL
    {
        public int Save(TaxRuleBOL objTaxRule)
        {
            TaxRuleDAL objDAL = new TaxRuleDAL();
            return objDAL.Save(objTaxRule);
        }

        public int Delete(int nTaxRuleID)
        {
            TaxRuleDAL objDAL = new TaxRuleDAL();
            return objDAL.Delete(nTaxRuleID);
        }

        public DataTable SelectAll(TaxRuleBOL objTaxRule)
        {
            TaxRuleDAL objDAL = new TaxRuleDAL();
            return objDAL.SelectAll(objTaxRule);
        }

        public TaxRuleBOL SearchById(int nTaxRuleID)
        {
            TaxRuleDAL objDAL = new TaxRuleDAL();
            return objDAL.SearchById(nTaxRuleID);
        }
    }
}
