using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
  public  class NotificationDAL
    {
      public int Savenotify(NotificationBOL objBOL)
      {

          DataAccess objDA = new DataAccess();
          List<SqlParameter> objParam = new List<SqlParameter>();
          try
          {
              objParam.Add(new SqlParameter("@Notification", objBOL.Notification));
              objParam.Add(new SqlParameter("@StartDate", objBOL.StartDate));
              objParam.Add(new SqlParameter("@EndDate", objBOL.EndDate));
              objParam.Add(new SqlParameter("@CreatedBy", objBOL.CreatedBy));
              objParam.Add(new SqlParameter("@ModifiedBy", objBOL.ModifiedBy));
              objParam.Add(new SqlParameter("@NotifyID", objBOL.NotifyID));
              objDA.sqlCmdText = "hrm_Notification_Insert_Update";
              objDA.sqlParam = objParam.ToArray();
              return objDA.ExecuteNonQuery();

          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public DataTable Selectall()
      {
          DataAccess objDA = new DataAccess();
         
          try
          {
              objDA.sqlCmdText = "hrm_Notification_Selectall";
              SqlDataReader dReader = objDA.ExecuteDataReader();
              return objDA.ExecuteDataSet().Tables[0];

          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public DataTable Selectbyid(int notificationid)
      {
          DataAccess objDA = new DataAccess();
           SqlParameter[] objParam = new SqlParameter[1];
          try
          {
              objParam[0] = new SqlParameter("@NotifyID", notificationid);
              objDA.sqlCmdText = "hrm_Notification_Selectall";
              SqlDataReader dReader = objDA.ExecuteDataReader();
              objDA.sqlParam = objParam;
              return objDA.ExecuteDataSet().Tables[0];

          }
          catch (Exception ex)
          {
              throw ex;
          }

      }

      public int Delete(int notificationid)
      {
          DataAccess objDA = new DataAccess();
          SqlParameter[] objParam = new SqlParameter[1];

          try
          {
              objParam[0] = new SqlParameter("@NotifyID", notificationid);
              objDA.sqlCmdText = "hrm_Notification_Delete";
              objDA.sqlParam = objParam;
              return objDA.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public DataTable SelectNOTIFY()
      {
          DataAccess objDA = new DataAccess();

          try
          {
              objDA.sqlCmdText = "hrm_Notification_Scroll";
              SqlDataReader dReader = objDA.ExecuteDataReader();
              return objDA.ExecuteDataSet().Tables[0];

          }
          catch (Exception ex)
          {
              throw ex;
          }

      }

    }
}
