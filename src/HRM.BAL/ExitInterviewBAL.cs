using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using HRM.DAL;

namespace HRM.BAL
{
    public class ExitInterviewBAL
    {
        public DataSet GetInterviews(int EmployeeID)
        {
            return new ExitInterviewDAL().GetInterviews(EmployeeID);
        }

        public DataSet GetStatus(int ID)
        {
            return new ExitInterviewDAL().GetStatus(ID);
        }
    }
}
