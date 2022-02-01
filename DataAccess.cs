using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for DataAccess
/// </summary>
namespace HRM.DAL
{
    public class DataAccess
    {
        private SqlParameter[] m_sqlParam;
        private string m_sqlCmdText;
        private string m_connString;
        //private SqlConnection sqlConn;

        #region Constructor

        public DataAccess()
        {

            m_sqlParam = null;
            m_sqlCmdText = string.Empty;
            //sqlConn = null;
            m_connString = string.Empty;
        }

        public DataAccess(string sConnString)
        {
            m_sqlParam = null;
            m_sqlCmdText = string.Empty;
            m_connString = sConnString;
        }
        #endregion

        #region properties

        public string sqlCmdText
        {
            get
            {
                return m_sqlCmdText;
            }
            set
            {
                m_sqlCmdText = value;
            }

        }

        public SqlParameter[] sqlParam
        {
            get { return m_sqlParam; }
            set { m_sqlParam = value; }
        }
        #endregion
        #region Functions
        private string GetConnectionString()
        {
            if (m_connString == "")
                return ConfigurationManager.AppSettings["DBCONN"];
            else
                return m_connString;

        }
        public string Connectionstring()
        {
            if (m_connString == "")
                return ConfigurationManager.AppSettings["DBCONN"];
            else
                return m_connString;    
        }

        public Int32 ExecuteNonQueryInline()
        {
            int RetValue = 0;

            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteNonQuery(Connectionstring(), CommandType.Text, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteNonQuery(Connectionstring(), CommandType.Text, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Int32 ExecuteNonQuery()
        {
            int RetValue = 0;

            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteNonQuery(Connectionstring(), CommandType.StoredProcedure, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteNonQuery(Connectionstring(), CommandType.StoredProcedure, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public object ExecuteNonQueryReturn()
        {
            object RetValue = 0;


            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.StoredProcedure, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.StoredProcedure, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader ExecuteDataReader()
        {
            SqlDataReader RetValue = default(SqlDataReader);
            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteReader(Connectionstring(), CommandType.StoredProcedure, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteReader(Connectionstring(), CommandType.StoredProcedure, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object ExecuteScalar()
        {
            object RetValue = null;
            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.StoredProcedure, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.StoredProcedure, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteScalarInline()
        {
            object RetValue = null;
            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.Text, sqlCmdText, sqlParam);
                }
                else
                {
                    RetValue = SqlHelper.ExecuteScalar(Connectionstring(), CommandType.Text, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet()
        {
            DataSet RetValue = new DataSet();
            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteDataset(Connectionstring(), CommandType.StoredProcedure, sqlCmdText, sqlParam);
                }
                else
                {


                    RetValue = SqlHelper.ExecuteDataset(Connectionstring(), CommandType.StoredProcedure, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSetInline()
        {
            DataSet RetValue = new DataSet();
            try
            {
                if ((sqlParam != null))
                {
                    RetValue = SqlHelper.ExecuteDataset(Connectionstring(), CommandType.Text, sqlCmdText, sqlParam);
                }
                else
                {


                    RetValue = SqlHelper.ExecuteDataset(Connectionstring(), CommandType.Text, sqlCmdText);
                }
                return RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Destructor
        ~DataAccess()
        {
        }


        #endregion

    }
}
