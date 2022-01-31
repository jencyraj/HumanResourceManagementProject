using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.DAL;
using HRM.BOL;
using System.Data;
namespace HRM.BAL
{
 public   class LeaveRuleBAL
    {
        public int Save(LeaveRuleBOL objLType)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.Save(objLType);
        }
        public int Update(LeaveRuleBOL objLType)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.Update(objLType);
        }
        public int Delete(int LRID, string sCreatedBy)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.Delete(LRID, sCreatedBy);
        }
        public int DeleteRow(int LRID)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.DeleteRow(LRID);
        }
        public DataTable Checkyear_timesheet()
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.Checkyear_timesheet();
        }
        public DataTable SelectAll(int LRID)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.SelectAll(LRID);
        }
       
        public DataTable bindleavtype()
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.bindleavtype();
        }
        public DataTable Select()
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.Select();
        }
        public DataTable rowSelect(int LDID)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.rowselect(LDID);
        }
        public DataTable SelectDetail(int LRID)
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.SelectDetail(LRID);
        }
        public DataTable CheckyearA()
        {
            LeaveRuleDAL objLT = new LeaveRuleDAL();
            return objLT.CheckyearA();
        }
    }
}
