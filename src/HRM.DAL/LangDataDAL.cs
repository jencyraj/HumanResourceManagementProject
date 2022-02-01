using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;

namespace HRM.DAL
{
    public class LangDataDAL
    {
        public int Save(LangDataBOL objLangData)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            int LID = 0;
            try
            {
                objParam.Add(new SqlParameter("@LID", objLangData.LID));
                objParam.Add(new SqlParameter("@LangCultureName", objLangData.LangCultureName));
                objParam.Add(new SqlParameter("@LangKey", objLangData.LangKey));
                objParam.Add(new SqlParameter("@LangText",  objLangData.LangText ));
                objParam.Add(new SqlParameter("@Status", objLangData.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objLangData.CreatedBy));
                objParam.Add(new SqlParameter("@ModifiedBy", objLangData.ModifiedBy));
                objDA.sqlCmdText = "hrm_Lang_Data_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                LID = Util.ToInt(objDA.ExecuteScalar().ToString());
            }
            catch
            {
                LID = 0;
            }
            return LID;
        }

        public int Delete(int nLID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@LID", nLID));
                objDA.sqlCmdText = "hrm_Lang_Data_DELETE";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select(LangDataBOL objBOL)
        {
            DataTable dt = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL != null)
                {
                    if (objBOL.LID > 0)
                        objParam.Add(new SqlParameter("@LID", objBOL.LID));
                    if ("" + objBOL.LangCultureName != "")
                        objParam.Add(new SqlParameter("@LangCultureName", objBOL.LangCultureName));
                    if (objBOL.LangKey != null)
                        objParam.Add(new SqlParameter("@LangKey", objBOL.LangKey));
                    if (objBOL.LangText != null)
                        objParam.Add(new SqlParameter("@LangText", SqlDbType.NVarChar) { Value = objBOL.LangText});
                    objDA.sqlParam = objParam.ToArray();
                }
                objDA.sqlCmdText = "hrm_Lang_Data_Select";
                dt = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dt = new DataTable();
            }
            return dt;
        }

        public LangDataBOL SearchById(int nLID)
        {
            LangDataBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (nLID > 0)
                {
                    objBOL = new LangDataBOL();
                    objBOL.LID = nLID;
                    DataTable dt = Select(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.LID = nLID;
                        objBOL.LanguageId = Util.ToInt("" + dt.Rows[0]["LanguageId"]);
                        objBOL.LangKey = "" + dt.Rows[0]["LangKey"];
                        objBOL.LangText = "" + dt.Rows[0]["LangText"];
                        objBOL.LangCultureName = "" + dt.Rows[0]["LangCultureName"];
                        objBOL.LangName = "" + dt.Rows[0]["LangName"];
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

        public DataTable SelectLanguage(int nLanguageId)
        {
            DataTable dtAttendanceType = null;
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (nLanguageId > 0)
                    objParam.Add(new SqlParameter("@LanguageId", nLanguageId));
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Languages_SelectAll";
                dtAttendanceType = objDA.ExecuteDataSet().Tables[0];
            }
            catch
            {
                dtAttendanceType = new DataTable();
            }
            return dtAttendanceType;
        }

        public int SaveLanguage(int nLID, string sActive,string sDefault,string sStyle)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                objParam.Add(new SqlParameter("@LanguageID", nLID));
                objParam.Add(new SqlParameter("@Active", sActive));
                objParam.Add(new SqlParameter("@DefaultLang", sDefault));
                objParam.Add(new SqlParameter("@StyleSheets", sStyle));
                objDA.sqlCmdText = "hrm_Languages_insert_update";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
