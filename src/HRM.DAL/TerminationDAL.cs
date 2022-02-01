using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class TerminationDAL
    {
        public int Save(TerminationBOL objTerm)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@TID", objTerm.TID));
                objParam.Add(new SqlParameter("@EmployeeID", objTerm.EmployeeID));
                objParam.Add(new SqlParameter("@ForwardedTo", objTerm.ForwardedTo));
                objParam.Add(new SqlParameter("@Reason", objTerm.Reason));
               // objParam.Add(new SqlParameter("@AdditionalInfo", objTerm.AdditionalInfo));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@CreatedBy", objTerm.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Termination_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Approve(TerminationBOL objTerm)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@TID", objTerm.TID));
                objParam.Add(new SqlParameter("@Approved", objTerm.Approved));
                objParam.Add(new SqlParameter("@ApprovedBy", objTerm.ApprovedBy));
                objParam.Add(new SqlParameter("@ApprovalReason", objTerm.ApprovalReason));
                objParam.Add(new SqlParameter("@IsExitInterview", objTerm.IsExitInterview));
                objParam.Add(new SqlParameter("@InterviewerId", objTerm.InterviewerId));
                objParam.Add(new SqlParameter("@InterviewDate", objTerm.InterviewDate));

                objDA.sqlCmdText = "hrm_Employee_Termination_Approve";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nTID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@TID", nTID);
                objDA.sqlCmdText = "hrm_Employee_Termination_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(TerminationBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.TID > 0)
                    objParam.Add(new SqlParameter("@TID", objBOL.TID));
                if (objBOL.EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
                if (objBOL.CreatedBy > 0)
                    objParam.Add(new SqlParameter("@CreatedBy", objBOL.CreatedBy));
                if (objBOL.ForwardedTo > 0)
                    objParam.Add(new SqlParameter("@ForwardedTo", objBOL.ForwardedTo));
                if ("" + objBOL.Reason != "")
                    objParam.Add(new SqlParameter("@Reason", objBOL.Reason));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Termination_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int Update_Interview_Closed(TerminationBOL objTerm)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@TID", objTerm.TID));
                objParam.Add(new SqlParameter("@Remarks", objTerm.InterviewRemarks));

                objDA.sqlCmdText = "[hrm_Employee_Termination_UpdateInterviewClosed]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TerminationBOL SelectByID(int TID)
        {
            TerminationBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (TID > 0)
                {
                    objBOL = new TerminationBOL();
                    objBOL.TID = TID;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.TID = TID;
                        objBOL.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                        objBOL.ForwardedTo = Convert.ToInt32(dt.Rows[0]["ForwardedTo"]);
                        objBOL.Reason = "" + dt.Rows[0]["Reason"];
                        objBOL.Approved = "" + dt.Rows[0]["Approved"];
                        objBOL.EmployeeName = "" + dt.Rows[0]["EFName"] + " " + dt.Rows[0]["EMName"] + " " + dt.Rows[0]["ELName"];
                        objBOL.ForwardedToName = "" + dt.Rows[0]["FFName"] + " " + dt.Rows[0]["FMName"] + " " + dt.Rows[0]["FLName"]; 
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
