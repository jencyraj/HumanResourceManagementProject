using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class ContractsDAL
    {
        public int Save(ContractsBOL objCn)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@ContractId", objCn.ContractId));
                objParam.Add(new SqlParameter("@ContractTypeID", objCn.ContractTypeID));
                objParam.Add(new SqlParameter("@Title", objCn.Title));
                objParam.Add(new SqlParameter("@Description", objCn.Description));
                objParam.Add(new SqlParameter("@DocName", objCn.DocName));
                objParam.Add(new SqlParameter("@Status", objCn.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objCn.CreatedBy));
                objDA.sqlCmdText = "hrm_Contracts_INSERT_UPDATE";
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
                objParam.Add(new SqlParameter("@ContractId", nCTId));
                objDA.sqlCmdText = "hrm_Contracts_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(ContractsBOL objBOL)
        {
            DataTable dtContracts = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.ContractId > 0)
                        objParam.Add(new SqlParameter("@ContractId", objBOL.ContractId));
                    if("" + objBOL.Title !="")
                        objParam.Add(new SqlParameter("@Title", objBOL.Title));
                    if ("" + objBOL.Description != "")
                        objParam.Add(new SqlParameter("@Description", objBOL.Description));
                    if(objBOL.ContractTypeID >0)
                        objParam.Add(new SqlParameter("@ContractTypeID", objBOL.ContractTypeID));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Contracts_Select";
                dtContracts = objDA.ExecuteDataSet().Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dtContracts;
        }

        public ContractsBOL SearchById(int nCTId)
        {
            ContractsBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nCTId > 0)
                {
                    objBOL = new ContractsBOL();
                    objBOL.ContractId = nCTId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.ContractId = nCTId;
                        objBOL.ContractTypeID = Util.ToInt("" + dt.Rows[0]["ContractTypeID"]);
                        objBOL.Title = "" + dt.Rows[0]["Title"];
                        objBOL.Description = "" + dt.Rows[0]["Description"];
                        objBOL.DocName = "" + dt.Rows[0]["DocName"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
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
