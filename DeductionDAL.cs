﻿using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class DeductionDAL
    {
        public int Save(DeductionBOL objDed)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@DedID", objDed.DedID);
                objParam[++i] = new SqlParameter("@DedCode", objDed.DedCode);
                objParam[++i] = new SqlParameter("@DeductionName", objDed.DeductionName);
                objParam[++i] = new SqlParameter("@DedAmount", objDed.DedAmount);
                objParam[++i] = new SqlParameter("@DedType", objDed.DedType);
                objParam[++i] = new SqlParameter("@TaxExemption", objDed.TaxExemption);
                objParam[++i] = new SqlParameter("@Status", "Y");
                objParam[++i] = new SqlParameter("@CreatedBy", objDed.CreatedBy);

                objDA.sqlCmdText = "hrm_Deductions_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nDedID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@DedID", nDedID);
                objDA.sqlCmdText = "hrm_Deductions_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(int nDedID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nDedID > 0)
                {
                    objParam[0] = new SqlParameter("@DedID", nDedID);
                    objDA.sqlParam = objParam;
                }
                objDA.sqlCmdText = "hrm_Deductions_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}