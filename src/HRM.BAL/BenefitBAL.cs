using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class BenefitBAL
    {

        #region BENEFIT TYPES

        public int SaveBenefitType(BenefitBOL objBF)
        {
            return new BenefitDAL().SaveBenefitType(objBF);
        }

        public DataTable SelectBenefitType(int BenefitTypeID)
        {
            return new BenefitDAL().SelectBenefitType(BenefitTypeID);
        }


        public int DeleteBenefitType(int BenefitTypeID, string sUser)
        {
            return new BenefitDAL().DeleteBenefitType(BenefitTypeID, sUser);
        }

        #endregion

        #region BENEFITS

        public int SaveBenefit(BenefitBOL objBF)
        {
            return new BenefitDAL().SaveBenefit(objBF);
        }

        public DataTable SelectBenefit(int BenefitID)
        {
            return new BenefitDAL().SelectBenefit(BenefitID);
        }


        public int DeleteBenefit(int BenefitID)
        {
            return new BenefitDAL().DeleteBenefit(BenefitID);
        }

        #endregion

        public int SaveEmployeeBenefits(BenefitBOL objBF)
        {
            return new BenefitDAL().SaveEmployeeBenefits(objBF);
        }

        public DataTable SelectEmployeeBenefit(int EmployeeID)
        {
            return new BenefitDAL().SelectEmployeeBenefit(EmployeeID);
        }


        public int DeleteEmployeeBenefit(int BenefitTypeID, int EmployeeID, string sUser)
        {
            return new BenefitDAL().DeleteEmployeeBenefit(BenefitTypeID, EmployeeID, sUser);
        }

    }
}
