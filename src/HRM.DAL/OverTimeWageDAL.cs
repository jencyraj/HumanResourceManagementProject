using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
namespace HRM.DAL
{
  public  class OverTimeWageDAL
    {
      public DataTable SelectAll(OverTimeWageBOL objBOL)
      {
          DataTable dtovertime = null;
          DataAccess objDA = new DataAccess();
          List<SqlParameter> objParam = new List<SqlParameter>();
          try
          {
              if (objBOL != null)
              {
                  if (objBOL.EmployeeId > 0)
                      objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                  objParam.Add(new SqlParameter("@nMonth", objBOL.Month));
                  objParam.Add(new SqlParameter("@nYear", objBOL.Year));
                  objDA.sqlParam = objParam.ToArray();
              }
              objDA.sqlCmdText = "hrm_Salaryslip_OverTime";
              dtovertime = objDA.ExecuteDataSet().Tables[0];
          }
          catch
          {
              dtovertime = new DataTable();
          }
          return dtovertime;
      }
    }
}
