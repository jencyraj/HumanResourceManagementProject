using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class ResignationDAL
    {
        public int Save(ResignationBOL objResgn)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@ResgnID", objResgn.ResgnID));
                objParam.Add(new SqlParameter("@EmployeeID", objResgn.EmployeeID));
                //objParam.Add(new SqlParameter("@ForwardedTo", objResgn.ForwardedTo));
                objParam.Add(new SqlParameter("@Reason", objResgn.Reason));
                objParam.Add(new SqlParameter("@AdditionalInfo", objResgn.AdditionalInfo));
                objParam.Add(new SqlParameter("@NoticeDate", objResgn.NoticeDate));
                objParam.Add(new SqlParameter("@ResgnDate", objResgn.ResgnDate));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@CreatedBy", objResgn.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Resignation_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Approve(ResignationBOL objResgn)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@ResgnID", objResgn.ResgnID));
                objParam.Add(new SqlParameter("@Approved", objResgn.Approved));
                objParam.Add(new SqlParameter("@ApprovedBy", objResgn.ApprovedBy));
                objParam.Add(new SqlParameter("@ApprovalReason", objResgn.ApprovalReason));
                objParam.Add(new SqlParameter("@IsExitInterview", objResgn.IsExitInterview));
                objParam.Add(new SqlParameter("@InterviewerId", objResgn.InterviewerId));
                objParam.Add(new SqlParameter("@InterviewDate", objResgn.InterviewDate));
                objDA.sqlCmdText = "hrm_Employee_Resignation_Approve";
                objDA.sqlParam = objParam.ToArray();
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update_Interview_Closed(ResignationBOL objTerm)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                objParam.Add(new SqlParameter("@RID", objTerm.ResgnID));
                objParam.Add(new SqlParameter("@Remarks", objTerm.InterviewRemarks));

                objDA.sqlCmdText = "[hrm_Employee_Resignation_UpdateInterviewClosed]";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int nResgnID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@ResgnID", nResgnID);
                objDA.sqlCmdText = "hrm_Employee_Resignation_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int cancel(int nResgnID, string status)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            try
            {
                objParam[0] = new SqlParameter("@ResgnID", nResgnID);
                objParam[1] = new SqlParameter("@reqStatus", status);

                objDA.sqlCmdText = "[hrm_Employee_Resignation_CANCEL]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(ResignationBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.ResgnID > 0)
                    objParam.Add(new SqlParameter("@ResgnID", objBOL.ResgnID));
                if (objBOL.EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
                if ("" + objBOL.Reason != "")
                    objParam.Add(new SqlParameter("@Reason", objBOL.Reason));
                if ("" + objBOL.AdditionalInfo != "")
                    objParam.Add(new SqlParameter("@AdditionalInfo", objBOL.AdditionalInfo));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Resignation_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ResignationBOL SelectByID(int RID)
        {
            ResignationBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            try
            {
                if (RID > 0)
                {
                    objBOL = new ResignationBOL();
                    objBOL.ResgnID = RID;
                    DataTable dt = SelectAll(objBOL);
                    if (dt.Rows.Count > 0)
                    {
                        objBOL.ResgnID = RID;
                        objBOL.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                        objBOL.Reason = "" + dt.Rows[0]["Reason"];
                        objBOL.Approved = "" + dt.Rows[0]["Approved"];
                        objBOL.NoticeDate =  DateTime.Parse("" + dt.Rows[0]["NoticeDate"]);
                        objBOL.ResgnDate =  DateTime.Parse("" + dt.Rows[0]["ResgnDate"]);
                        objBOL.AdditionalInfo = "" + dt.Rows[0]["AdditionalInfo"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }
        public DataTable SelectAll(int EmployeeId, int BranchId, string Status)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (EmployeeId > 0)
                    objParam.Add(new SqlParameter("@EmployeeId", EmployeeId));
                if (BranchId > 0)
                    objParam.Add(new SqlParameter("@BranchId", BranchId));
                if ("" + Status != "")
                    objParam.Add(new SqlParameter("@ApprovalStatus", Status));
               

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Resignation_SelectAll";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable Resignation_SelectById(ResignationBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@Resgnid", objBOL.ResgnID));


                objDA.sqlParam = objParam.ToArray();

                    objDA.sqlCmdText = "[hrm_Employee_Resignation_SelectById]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectAPDresignation(ResignationBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {
                if (objBOL.ResgnID > 0)
                    objParam.Add(new SqlParameter("@ResgnID", objBOL.ResgnID));
                if (objBOL.EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID));
                if ("" + objBOL.Reason != "")
                    objParam.Add(new SqlParameter("@Reason", objBOL.Reason));
                if ("" + objBOL.AdditionalInfo != "")
                    objParam.Add(new SqlParameter("@AdditionalInfo", objBOL.AdditionalInfo));
                if ("" + objBOL.Approved != "")
                    objParam.Add(new SqlParameter("@Approved", objBOL.Approved));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "[hrm_Employee_Approved_Resignation_Select]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
