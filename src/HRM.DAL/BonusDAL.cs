using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class BonusDAL
    {
        public int Save(BonusBOL objBonus)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@BonusId", objBonus.BonusId));
                objParam.Add(new SqlParameter("@EmployeeId", objBonus.EmployeeId));
                objParam.Add(new SqlParameter("@Title", objBonus.Title));
                objParam.Add(new SqlParameter("@Amount", objBonus.Amount));
                objParam.Add(new SqlParameter("@BonusDate", objBonus.BonusDate));
                objParam.Add(new SqlParameter("@BonusDateAR", objBonus.BonusDateAR));
                objParam.Add(new SqlParameter("@Description", objBonus.Description));
                objParam.Add(new SqlParameter("@AdditionalInfo", objBonus.AdditionalInfo));
                objParam.Add(new SqlParameter("@Status", objBonus.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objBonus.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objBonus.ModifiedBy));
                objDA.sqlCmdText = "hrm_Bonus_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nBonusId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@BonusId", nBonusId));
                objDA.sqlCmdText = "hrm_Bonus_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(BonusBOL objBOL)
        {
            DataTable dtBonus = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.BonusId > 0)
                        objParam.Add(new SqlParameter("@BonusId", objBOL.BonusId));
                    if (objBOL.EmployeeId > 0)
                        objParam.Add(new SqlParameter("@EmployeeId", objBOL.EmployeeId));
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Bonus_SelectAll"; 
                dtBonus = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtBonus = new DataTable();
            }
            return dtBonus;
        }

        public BonusBOL SearchById(int nBonusId)
        {
            BonusBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nBonusId > 0)
                {
                    objBOL = new BonusBOL();
                    objBOL.BonusId = nBonusId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.BonusId = nBonusId;
                        objBOL.EmployeeId = Util.ToInt("" + dt.Rows[0]["EmployeeId"]);
                        objBOL.Title = "" + dt.Rows[0]["Title"];
                        objBOL.Amount = Util.ToDecimal("" + dt.Rows[0]["Amount"]);
                        objBOL.BonusDate = "" + dt.Rows[0]["BonusDate"];
                        objBOL.BonusDateAR = "" + dt.Rows[0]["BonusDateAR"];
                        objBOL.Description = "" + dt.Rows[0]["Description"];
                        objBOL.AdditionalInfo = "" + dt.Rows[0]["AdditionalInfo"];
                        objBOL.Status = "" + dt.Rows[0]["Status"];
                        objBOL.CreatedBy = "" + dt.Rows[0]["CreatedBy"];
                        objBOL.CreatedDate = Util.ToDateTime("" + dt.Rows[0]["CreatedDate"]);
                        objBOL.ModifiedBy = "" + dt.Rows[0]["ModifiedBy"];
                        objBOL.ModifiedDate = Util.ToDateTime("" + dt.Rows[0]["ModifiedDate"]);
                        objBOL.FirstName = "" + dt.Rows[0]["FirstName"];
                        objBOL.MiddleName = "" + dt.Rows[0]["MiddleName"];
                        objBOL.LastName = "" + dt.Rows[0]["LastName"];
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
