using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HRM.DAL;
using HRM.BOL;
namespace HRM.BAL
{
   public class NotificationBAL
    {
       public int Savenotify(NotificationBOL objBOL)
       {
           return new NotificationDAL().Savenotify(objBOL);
       }
      public DataTable Selectbyid(int notificationid)
      
       {
           return new NotificationDAL().Selectbyid(notificationid);

       }
       public DataTable Selectall()
       {
           return new NotificationDAL().Selectall();

       }
       public int Delete(int notificationid)
       {
           return new NotificationDAL().Delete(notificationid);
       }
       public DataTable SelectNOTIFY()
       {
           return new NotificationDAL().SelectNOTIFY();
       }
    }
}
