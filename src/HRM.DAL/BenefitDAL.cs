using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using HRM.BOL;

namespace HRM.DAL
{
    public class BenefitDAL
    {

        #region BENEFIT TYPES

        public int SaveBenefitType(BenefitBOL objBF)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            if (objBF.BenefitTypeID > 0)
                objParam.Add(new SqlParameter("@BFTID", objBF.BenefitTypeID));

            objParam.Add(new SqlParameter("@BFType", objBF.BenefitType));
            objParam.Add(new SqlParameter("@Status", "Y"));
            objParam.Add(new SqlParameter("@ActivePack", objBF.ActivePack));
            objParam.Add(new SqlParameter("@CreatedBy", objBF.CreatedBy));

            objDA.sqlCmdText = "hrm_BenefitType_Insert_Update";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        public DataTable SelectBenefitType(int BenefitTypeID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            if (BenefitTypeID > 0)
                objParam.Add(new SqlParameter("@BFTID", BenefitTypeID));

            objDA.sqlCmdText = "hrm_BenefitType_Select";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }


        public int DeleteBenefitType(int BenefitTypeID, string sUser)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@BFTID", BenefitTypeID));
            objParam.Add(new SqlParameter("@CreatedBy", sUser));

            objDA.sqlCmdText = "hrm_BenefitType_Delete";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        #endregion

        #region BENEFITS NOT USING NOW

        public int SaveBenefit(BenefitBOL objBF)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            if (objBF.BenefitID > 0)
                objParam.Add(new SqlParameter("@BFID", objBF.BenefitID));

            objParam.Add(new SqlParameter("@BFTID", objBF.BenefitTypeID));
            objParam.Add(new SqlParameter("@BFDesc", objBF.BenefitDesc));
            objParam.Add(new SqlParameter("@Status", "Y"));
            objParam.Add(new SqlParameter("@CreatedBy", objBF.CreatedBy));

            objDA.sqlCmdText = "hrm_Benefits_Insert_Update";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        public DataTable SelectBenefit(int BenefitID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            if (BenefitID > 0)
                objParam.Add(new SqlParameter("@BFID", BenefitID));

            objDA.sqlCmdText = "hrm_Benefits_Select";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }


        public int DeleteBenefit(int BenefitID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@BFID", BenefitID));

            objDA.sqlCmdText = "hrm_Benefits_Delete";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        #endregion


        #region EMPLOYEE BENEFITS

        public int SaveEmployeeBenefits(BenefitBOL objBF)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@BFTID", objBF.BenefitTypeID));
            objParam.Add(new SqlParameter("@EmployeeID", objBF.EmployeeID));
            objParam.Add(new SqlParameter("@AvailedBy", objBF.AvailedBy));
            objParam.Add(new SqlParameter("@EmployeeAmount", objBF.EmployeeShare));
            objParam.Add(new SqlParameter("@org_Amount", objBF.CompanyShare));
            objParam.Add(new SqlParameter("@additionalinfo", objBF.AdditionalInfo));
            objParam.Add(new SqlParameter("@Status", "Y"));
            objParam.Add(new SqlParameter("@CreatedBy", objBF.CreatedBy));

            objDA.sqlCmdText = "hrm_EmployeeBenefit_Insert_Update";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        public DataTable SelectEmployeeBenefit(int EmployeeID)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@EmployeeID", EmployeeID));

            objDA.sqlCmdText = "hrm_EmployeeBenefit_Select";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteDataSet().Tables[0];
        }


        public int DeleteEmployeeBenefit(int BenefitTypeID, int EmployeeID, string sUser)
        {
            DataAccess objDA = new DataAccess();
            List<SqlParameter> objParam = new List<SqlParameter>();

            objParam.Add(new SqlParameter("@BFTID", BenefitTypeID));
            objParam.Add(new SqlParameter("@Employeeid", EmployeeID));
            objParam.Add(new SqlParameter("@CreatedBy", sUser));

            objDA.sqlCmdText = "hrm_EmployeeBenefit_Delete";
            objDA.sqlParam = objParam.ToArray();

            return objDA.ExecuteNonQuery();
        }

        #endregion

    }
}
