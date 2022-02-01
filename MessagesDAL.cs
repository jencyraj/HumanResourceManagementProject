using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class MessagesDAL
    {
        public int Save(MessagesBOL objOrg)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[5];
            int i = -1;

            try
            {
                objParam[++i] = new SqlParameter("@emailid", objOrg.EmailID);
                objParam[++i] = new SqlParameter("@mailsubject", objOrg.MailSubject);
                objParam[++i] = new SqlParameter("@mailmsg", objOrg.MailMessage);
                objParam[++i] = new SqlParameter("@sentBy", objOrg.SentBy);

                objDA.sqlCmdText = "HRM_Messages_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nEMID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@EMID", nEMID);
                objDA.sqlCmdText = "HRM_Messages_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(MessagesBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[3];
            int i = -1;
            try
            {
                if ("" + objBOL.EmailID != "")
                    objParam[++i] = new SqlParameter("@EmailID", objBOL.EmailID);

                if ("" + objBOL.SentTo != "")
                    objParam[++i] = new SqlParameter("@SentTo", objBOL.SentTo);

                if ("" + objBOL.SentBy != "")
                    objParam[++i] = new SqlParameter("@SentBy", objBOL.SentBy);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "HRM_Messages_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
