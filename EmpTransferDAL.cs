using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class EmpTransferDAL
    {
        public int Save(EmpTransferBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            try
            {

                objParam.Add(new SqlParameter("@TransferID", objEmp.TransferID));
                objParam.Add(new SqlParameter("@EmployeeID", objEmp.EmployeeID));
                objParam.Add(new SqlParameter("@TransferDate", objEmp.TransferDate));
                objParam.Add(new SqlParameter("@ForwardedTo", objEmp.ForwardTo));
                objParam.Add(new SqlParameter("@BranchTo", objEmp.BranchTo));
                objParam.Add(new SqlParameter("@DeptTo", objEmp.DeptTo));
                objParam.Add(new SqlParameter("@SubDeptTo", objEmp.SubDeptTo));
                objParam.Add(new SqlParameter("@ReportTo", objEmp.ReportTo));
                objParam.Add(new SqlParameter("@Status", "Y"));
                objParam.Add(new SqlParameter("@CreatedBy", objEmp.CreatedBy));

                objDA.sqlCmdText = "hrm_Employee_Transfer_INSERT_UPDATE";
                objDA.sqlParam = objParam.ToArray();
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nTransferID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@TransferID", nTransferID);
                objDA.sqlCmdText = "hrm_Employee_Transfer_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(EmpTransferBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();
            try
            {
                if (objBOL.TransferID > 0)
                    objParam.Add(new SqlParameter("@TransferID", objBOL.TransferID));
                if (objBOL.EmployeeID > 0)
                    objParam.Add(new SqlParameter("@EmployeeID", objBOL.EmployeeID)); 
                if (objBOL.ForwardTo > 0)
                    objParam.Add(new SqlParameter("@ForwardedTo", objBOL.ForwardTo));
                if (objBOL.BranchFrom > 0)
                    objParam.Add(new SqlParameter("@BranchFrom", objBOL.BranchFrom));
                if (objBOL.BranchTo > 0)
                    objParam.Add(new SqlParameter("@BranchTo", objBOL.BranchTo));
                if (objBOL.DeptFrom > 0)
                    objParam.Add(new SqlParameter("@DeptFrom", objBOL.DeptFrom));
                if (objBOL.DeptTo > 0)
                    objParam.Add(new SqlParameter("@DeptTo", objBOL.DeptTo));
                if (objBOL.SubDeptFrom > 0)
                    objParam.Add(new SqlParameter("@SubDeptFrom", objBOL.SubDeptFrom));
                if (objBOL.SubDeptTo > 0)
                    objParam.Add(new SqlParameter("@SubDeptTo", objBOL.SubDeptTo));
                if ("" + objBOL.Approve_Old_Branch != "")
                    objParam.Add(new SqlParameter("@Approve_Old_Branch", objBOL.Approve_Old_Branch));
                if ("" + objBOL.Approve_New_Branch != "")
                    objParam.Add(new SqlParameter("@Approve_New_Branch", objBOL.Approve_New_Branch));
                if (objBOL.ReportTo > 0)
                    objParam.Add(new SqlParameter("@ReportTo", objBOL.ReportTo));

                objDA.sqlParam = objParam.ToArray();
                objDA.sqlCmdText = "hrm_Employee_Transfer_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int ApprovalRejectByCurrentBranch(EmpTransferBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            try
            {

                objParam[0] = new SqlParameter("@TransferID", objEmp.TransferID);
                objParam[1] = new SqlParameter("@ApprovedBy", objEmp.ApprovedBy);
                objParam[2] = new SqlParameter("@Approve_Old_Branch", objEmp.Approve_Old_Branch);

                objDA.sqlCmdText = "hrm_Employee_Transfer_Approval";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApprovalRejectByNewBranch(EmpTransferBOL objEmp)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];

            try
            {

                objParam[0] = new SqlParameter("@TransferID", objEmp.TransferID);
                objParam[1] = new SqlParameter("@Approved_New_By", objEmp.Approved_New_By);
                objParam[2] = new SqlParameter("@Approve_New_Branch", objEmp.Approve_New_Branch);

                objDA.sqlCmdText = "hrm_Employee_Transfer_Approval_New_Branch";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
