using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
   public class OverTimeBAL
    {
       public string Save(OverTimeBOL objovertime)
       {
           return new OverTimeDAL().Save(objovertime); 
       }
       public DataTable selectall(OverTimeBOL objbol)
       {
           return new OverTimeDAL().selectall(objbol);
       }
    }
}
