using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
namespace HRM.DAL
{
   public class ImportIrisDAL
    {
       public int UPDATE( string fname, string mname,string lname,string irisid,string createdby)
       {
          
           DataAccess objDA = new DataAccess();
           List<SqlParameter> objParam = new List<SqlParameter>();
           try
           {

               objParam.Add(new SqlParameter("@FirstName", fname.Trim()));
               objParam.Add(new SqlParameter("@MiddleName", mname.Trim()));
               objParam.Add(new SqlParameter("@LastName", lname.Trim()));
               objParam.Add(new SqlParameter("@IrisId", irisid.Trim()));
               objParam.Add(new SqlParameter("@CreatedBy", createdby));
            
               objDA.sqlCmdText = "hrm_irisid_Insert_Update";

               objDA.sqlParam = objParam.ToArray();
               return Util.ToInt( objDA.ExecuteScalar());
           }
           catch (Exception ex)
           {
               throw ex;
           }
           
       }
       public DataTable Select( string createdby, string imptype)
       {
           DataAccess objDA = new DataAccess();
           List<SqlParameter> objParam = new List<SqlParameter>();
           try
           {

              
               objParam.Add(new SqlParameter("@CreatedBy", createdby));
               objParam.Add(new SqlParameter("@ImportType", imptype));
               objDA.sqlCmdText = "hrm_irisidimport_select";

               objDA.sqlParam = objParam.ToArray();
               return objDA.ExecuteDataSet().Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
