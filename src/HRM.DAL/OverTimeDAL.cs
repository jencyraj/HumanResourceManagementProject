using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class OverTimeDAL
    {
        public string Save(OverTimeBOL objovertime)
        {

            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@RId", objovertime.RId));
            
                objParam.Add(new SqlParameter("@MinimumHr", objovertime.MinimumHr));
                objParam.Add(new SqlParameter("@Ruleapplicable",objovertime.Ruleapplicable));
                objParam.Add(new SqlParameter("@ApplicableSum", objovertime.ApplicableSum));
                objParam.Add(new SqlParameter("@CreatedBy", objovertime.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objovertime.ModifiedBy));
                objDA.sqlCmdText = "hrm_Overtimerule_Insert_Update";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable selectall(OverTimeBOL objbol)
        {
            DataAccess objDA = new DataAccess();
            try
            {
               objDA.sqlCmdText = "hrm_Overtime_SelectAll";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        
    }
}

