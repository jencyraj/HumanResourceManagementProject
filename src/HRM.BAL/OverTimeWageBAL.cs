using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.DAL;
using HRM.BOL;
namespace HRM.BAL
{
   public class OverTimeWageBAL
    {
       public DataTable SelectAll(OverTimeWageBOL objOvertime)
       {
           OverTimeWageDAL objDAL = new OverTimeWageDAL();
           return objDAL.SelectAll(objOvertime);
       }
    }
}
