using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace HRM.DAL
{
    public class ExitInterviewDAL
    {
        public DataSet GetInterviews(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@InterviewerID", EmployeeID));

            objDA.sqlParam = objParam.ToArray();

            objDA.sqlCmdText = "hrm_ExitInterviews";
            return objDA.ExecuteDataSet();
        }
        public DataSet GetStatus(int ID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@ID", ID));

            objDA.sqlParam = objParam.ToArray();

            objDA.sqlCmdText = "[hrm_ExitInterviews_getstatus]";
            return objDA.ExecuteDataSet();
        }
    }
}
