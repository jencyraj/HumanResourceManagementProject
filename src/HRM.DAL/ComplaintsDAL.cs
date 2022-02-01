using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class ComplaintsDAL
    {
        public int Save(ComplaintsBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@ComplaintID", objEmp.ComplaintID));
                objParam.Add(new SqlParameter("@EmployeeID", objEmp.EmployeeID));
                objParam.Add(new SqlParameter("@ComplaintBy", objEmp.ComplaintBy));
                objParam.Add(new SqlParameter("@ComplaintTitle", objEmp.ComplaintTitle));
                objParam.Add(new SqlParameter("@Description", objEmp.Description));
                objParam.Add(new SqlParameter("@Status", objEmp.Status));
                objParam.Add(new SqlParameter("@CreatedBy", objEmp.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Complaints_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nComplaintID, string sDeletedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@ComplaintID", nComplaintID);
                objParam[1] = new SqlParameter("@ModifiedBy", sDeletedBy);

                objDA.sqlCmdText = "hrm_Employee_Complaints_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(ComplaintsBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.ComplaintID > 0)
                    objParam.Add(new SqlParameter("@ComplaintID", objBOL.ComplaintID));
                //if (objBOL.EmployeeID > 0)
                //    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
                if ("" + objBOL.ComplaintTitle != "")
                    objParam.Add(new SqlParameter("@ComplaintTitle", objBOL.ComplaintTitle));
                if ("" + objBOL.Description != "")
                    objParam.Add(new SqlParameter("@Description", objBOL.Description));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Complaints_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ComplaintsBOL SelectByID(int complaintId)
        {
            ComplaintsBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (complaintId > 0)
                {
                    objBOL = new ComplaintsBOL();
                    objBOL.ComplaintID = complaintId;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.ComplaintID = complaintId;
                        objBOL.ComplaintTitle = "" + dt.Rows[0]["ComplaintTitle"];
                        objBOL.Description = "" + dt.Rows[0]["Description"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }

        public int UpdateStatus(int nComplaintID, string sUpdatedBy)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@ComplaintID", nComplaintID);
                objParam[1] = new SqlParameter("@ModifiedBy", sUpdatedBy);

                objDA.sqlCmdText = "hrm_Employee_Complaints_Update_Status";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(string complaintBy, string complaintAgainst)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@ComplaintByID", complaintBy));
                objParam.Add(new SqlParameter("@ComplaintAgainstID", complaintAgainst));
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Complaints_Search";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectEmployeeByComplaintID(int complaintId)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (complaintId > 0)
                    objParam.Add(new SqlParameter("@ComplaintID", complaintId));
                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Complaints_Select_By_Id";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
