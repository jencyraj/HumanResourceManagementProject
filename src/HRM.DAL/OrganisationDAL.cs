using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class OrganisationDAL
    {
        public int Save(OrganisationBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[25];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@CompanyID", objOrg.CompanyID);
                objParam[++i] = new SqlParameter("@CompanyName", objOrg.CompanyName);
                objParam[++i] = new SqlParameter("@Address", objOrg.Address);
                objParam[++i] = new SqlParameter("@City", objOrg.City);
                objParam[++i] = new SqlParameter("@State", objOrg.State);
                objParam[++i] = new SqlParameter("@CountryID", objOrg.CountryID);
                objParam[++i] = new SqlParameter("@ZipCode", objOrg.ZipCode);
                objParam[++i] = new SqlParameter("@Telephone", objOrg.Telephone);
                objParam[++i] = new SqlParameter("@Mobile", objOrg.Mobile);
                objParam[++i] = new SqlParameter("@Fax", objOrg.Fax);
                objParam[++i] = new SqlParameter("@Website", objOrg.Website);
                objParam[++i] = new SqlParameter("@Email", objOrg.Email);
                objParam[++i] = new SqlParameter("@EmployeeCount", objOrg.EmployeeCount);
                objParam[++i] = new SqlParameter("@RegistrationNo", objOrg.RegistrationNo);
                objParam[++i] = new SqlParameter("@VAT", objOrg.VAT);
                objParam[++i] = new SqlParameter("@CST", objOrg.CST);
                objParam[++i] = new SqlParameter("@PANNO", objOrg.PANNO);
                objParam[++i] = new SqlParameter("@ESINO", objOrg.ESI);
                objParam[++i] = new SqlParameter("@TINNO", objOrg.TIN);
                objParam[++i] = new SqlParameter("@PFNO", objOrg.PF);
                objParam[++i] = new SqlParameter("@DateFormat", objOrg.DateFormat); 
                objParam[++i] = new SqlParameter("@ContactName", objOrg.ContactName);
                objParam[++i] = new SqlParameter("@LogoName", objOrg.LogoName);
                objParam[++i] = new SqlParameter("@Status", objOrg.Status);

                objDA.sqlCmdText = "HRM_COMPANY_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nCompanyID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@CompanyID", nCompanyID);
                objDA.sqlCmdText = "HRM_COMPANY_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(OrganisationBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[20];
            int i = -1;

            try
            {
                if (objOrg.CompanyID > 0)
                    objParam[++i] = new SqlParameter("@CompanyID", objOrg.CompanyID);
                if ("" + objOrg.CompanyName != "")
                    objParam[++i] = new SqlParameter("@CompanyName", objOrg.CompanyName);
                if ("" + objOrg.Address != "")
                    objParam[++i] = new SqlParameter("@Address", objOrg.Address);
                if ("" + objOrg.City != "")
                    objParam[++i] = new SqlParameter("@City", objOrg.City);
                if ("" + objOrg.State != "")
                    objParam[++i] = new SqlParameter("@State", objOrg.State);
                if (objOrg.CountryID > 0)
                    objParam[++i] = new SqlParameter("@CountryID", objOrg.CountryID);
                if ("" + objOrg.ZipCode != "")
                    objParam[++i] = new SqlParameter("@ZipCode", objOrg.ZipCode);
                if ("" + objOrg.Telephone != "")
                    objParam[++i] = new SqlParameter("@Telephone", objOrg.Telephone);
                if ("" + objOrg.Mobile != "")
                    objParam[++i] = new SqlParameter("@Mobile", objOrg.Mobile);
                if ("" + objOrg.Fax != "")
                    objParam[++i] = new SqlParameter("@Fax", objOrg.Fax);
                if ("" + objOrg.Website != "")
                    objParam[++i] = new SqlParameter("@Website", objOrg.Website);
                if ("" + objOrg.Email != "")
                    objParam[++i] = new SqlParameter("@Email", objOrg.Email);
                if ("" + objOrg.RegistrationNo != "")
                    objParam[++i] = new SqlParameter("@RegistrationNo", objOrg.RegistrationNo);
                if ("" + objOrg.VAT != "")
                    objParam[++i] = new SqlParameter("@VAT", objOrg.VAT);
                if ("" + objOrg.ContactName != "")
                    objParam[++i] = new SqlParameter("@ContactName", objOrg.ContactName);
                if ("" + objOrg.Status != "")
                    objParam[++i] = new SqlParameter("@Status", objOrg.Status);

                objDA.sqlCmdText = "HRM_COMPANY_SELECT";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrganisationBOL Select()
        {
            DataAccess objDA = new DataAccess();
            OrganisationBOL objOrg = null;

            try
            {

                objDA.sqlCmdText = "HRM_COMPANY_SELECT";
                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objOrg = new OrganisationBOL();
                    objOrg.CompanyID = Util.ToInt("" + dReader["CompanyID"]);
                    objOrg.CompanyName = "" + dReader["CompanyName"];
                    objOrg.Address = "" + dReader["Address"];
                    objOrg.City = "" + dReader["City"];
                    objOrg.State = "" + dReader["State"];
                    objOrg.ZipCode = "" + dReader["ZipCode"];
                    objOrg.Telephone = "" + dReader["Telephone"];
                    objOrg.Mobile = "" + dReader["Mobile"];
                    objOrg.Fax = "" + dReader["Fax"];
                    objOrg.Website = "" + dReader["Website"];
                    objOrg.Email = "" + dReader["Email"];
                    objOrg.RegistrationNo = "" + dReader["RegistrationNo"];
                    objOrg.VAT = "" + dReader["VAT"];
                    objOrg.PANNO = "" + dReader["PANNO"];
                    objOrg.CST = "" + dReader["CST"];
                    objOrg.ESI = "" + dReader["ESINO"];
                    objOrg.TIN = "" + dReader["TINNO"];
                    objOrg.PF = "" + dReader["PFNO"];
                    objOrg.DateFormat = "" + dReader["DateFormat"];
                    objOrg.ContactName = "" + dReader["ContactName"];
                    objOrg.LogoName = "" + dReader["LogoName"];
                    objOrg.Status = "" + dReader["Status"];

                    objOrg.CountryID = Util.ToInt("" + dReader["CountryID"]);
                    objOrg.EmployeeCount = Util.ToInt("" + dReader["EmployeeCount"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objOrg;
        }
    }
}
