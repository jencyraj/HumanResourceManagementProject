using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;


namespace HRM.DAL
{
    public class EmailDAL
    {
        public int Save(EmailBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[8];

            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@FromEmail", objBOL.FromEmail);
                objParam[++i] = new SqlParameter("@FromName", objBOL.FromName);
                objParam[++i] = new SqlParameter("@SmtpAuth", objBOL.SmtpAuth);
                objParam[++i] = new SqlParameter("@SmtpSecurity", objBOL.SmtpSecurity);
                objParam[++i] = new SqlParameter("@SmtpPort", objBOL.SmtpPort);
                objParam[++i] = new SqlParameter("@SmtpUserName", objBOL.SmtpUserName);
                objParam[++i] = new SqlParameter("@SmtpPassword", objBOL.SmtpPassword);
                objParam[++i] = new SqlParameter("@SmtpHost", objBOL.SmtpHost);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Email_Insert_Update";
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmailBOL Select()
        {
            DataAccess objDA = new DataAccess();
            EmailBOL objEmail = null;

            try
            {

                objDA.sqlCmdText = "hrm_Email_Select";
                SqlDataReader dReader = objDA.ExecuteDataReader();
                while (dReader.Read())
                {
                    objEmail = new EmailBOL();
                    objEmail.FromEmail =  "" + dReader["FromEmail"] ;
                    objEmail.FromName = "" + dReader["FromName"];
                    objEmail.SmtpAuth = "" + dReader["SmtpAuth"];
                    objEmail.SmtpHost = "" + dReader["SmtpHost"];
                    objEmail.SmtpUserName = "" + dReader["SmtpUserName"];
                    objEmail.SmtpPassword = "" + dReader["SmtpPassword"];
                    objEmail.SmtpPort = Util.ToInt("" + dReader["SmtpPort"]);
                    objEmail.SmtpSecurity = "" + dReader["SmtpSecurity"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEmail;
        }
    }
}
