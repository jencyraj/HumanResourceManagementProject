using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.BOL;
using System.Data.SqlClient;
using System.Data;

namespace HRM.DAL
{
  public  class EmpCodeSettDAL
    {
        public int Save(EmpCodeSettBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@CodeId", objOrg.CodeID);
                objParam[++i] = new SqlParameter("@CodeItem", objOrg.CodeItem);
                objParam[++i] = new SqlParameter("@SerialNoPrefix", objOrg.SerialNoPrefix);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@EcodeCtrStart", objOrg.EcodeCtrStart);
                objParam[++i] = new SqlParameter("@EmpCodeTotalLength", objOrg.EmpCodeTotalLength);
                objParam[++i] = new SqlParameter("@Value", objOrg.Value);
                objParam[++i] = new SqlParameter("@SettOrder", objOrg.SettOrder);
               
                objDA.sqlCmdText = "hrm_EmpCodeSett_Insert";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetNextEmployeeCode(string Item,string order)
        {

            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];
            int i = -1;
            try
            {
                objParam[++i] = new SqlParameter("@CodeItem", Item);
                objParam[++i]  = new SqlParameter("@UserID", order);
                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_EmpCodSett_GetNew";

                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        public int Delete(string nTID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CodeItem", nTID);
                objDA.sqlCmdText = "hrm_EmpCodSett_Delete";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public DataSet SelectAll()
        {
            DataAccess objDA = new DataAccess();

            try
            {
                objDA.sqlCmdText = "hrm_EmpCodeSett_Select";
                return objDA.ExecuteDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
