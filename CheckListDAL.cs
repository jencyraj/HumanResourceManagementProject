using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HRM.BOL;


namespace HRM.DAL
{
  public  class CheckListDAL
    {
      
      public int Save(CheckListBOL objOrg)
      {
          
          DataAccess objDA = new DataAccess();
          SqlParameter[] objParam = new SqlParameter[25];
          int i = -1;

          try
          {
              objParam[++i] = new SqlParameter("@ClID", objOrg.ClID);
              objParam[++i] = new SqlParameter("@Description", objOrg.Description);
              objParam[++i] = new SqlParameter("@Cltype", objOrg.Cltype);
              objParam[++i] = new SqlParameter("@Status", "Y");

              objDA.sqlCmdText = "hrm_CheckList_Insert_Update";
              objDA.sqlParam = objParam;
              return int.Parse(objDA.ExecuteScalar().ToString());
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

        public int Delete(int ChekLstID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@ClId", ChekLstID);
                objDA.sqlCmdText = "hrm_CheckList_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int ChekListID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
               
                objDA.sqlCmdText = "hrm_CheckList_Select";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
