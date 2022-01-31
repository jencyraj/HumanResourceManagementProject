using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class ContractsBAL
    {
        public int Save(ContractsBOL objContracts)
        {
            ContractsDAL objDAL = new ContractsDAL();
            return objDAL.Save(objContracts);
        }

        public int Delete(int nCTID)
        {
            ContractsDAL objDAL = new ContractsDAL();
            return objDAL.Delete(nCTID);
        }

        public DataTable SelectAll(ContractsBOL objContracts)
        {
            ContractsDAL objDAL = new ContractsDAL();
            return objDAL.SelectAll(objContracts);
        }

        public ContractsBOL SearchById(int nContractsId)
        {
            ContractsDAL objDAL = new ContractsDAL();
            return objDAL.SearchById(nContractsId);
        }
    }
}
