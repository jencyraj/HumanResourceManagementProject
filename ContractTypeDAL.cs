using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class ContractTypeDAL
    {
        public int Save(ContractTypeBOL objContractType)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@CTId", objContractType.CTId));
                objParam.Add(new SqlParameter("@ContractTypeName", objContractType.ContractTypeName));
                objParam.Add(new SqlParameter("@Description", objContractType.Description));
                objParam.Add(new SqlParameter("@Status", objContractType.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objContractType.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objContractType.ModifiedBy));
                objDA.sqlCmdText = "hrm_ContractType_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nCTId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@CTId", nCTId));
                objDA.sqlCmdText = "hrm_ContractType_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(ContractTypeBOL objBOL)
        {
            DataTable dtContractType = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.CTId > 0)
                        objParam.Add(new SqlParameter("@CTId", objBOL.CTId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_ContractType_SelectAll";
                dtContractType = objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtContractType;
        }

        public ContractTypeBOL SearchById(int nCTId)
        {
            ContractTypeBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nCTId > 0)
                {
                    objBOL = new ContractTypeBOL();
                    objBOL.CTId = nCTId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.CTId = nCTId;
                        objBOL.ContractTypeName = "" + dt.Rows[0]["ContractTypeName"];
                        objBOL.Description = "" + dt.Rows[0]["Description"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
                        objBOL.CreatedDate = Util.ToDateTime("" + dt.Rows[0]["CreatedDate"]);
                        objBOL.ModifiedBy = "" + dt.Rows[0]["ModifiedBy"];
                        objBOL.ModifiedDate = Util.ToDateTime("" + dt.Rows[0]["ModifiedDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }
    }
}
