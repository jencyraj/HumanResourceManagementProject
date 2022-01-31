using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class ContractTypeBAL
    {
        public int Save(ContractTypeBOL objContractType)
        {
            ContractTypeDAL objDAL = new ContractTypeDAL();
            return objDAL.Save(objContractType);
        }

        public int Delete(int nCTID)
        {
            ContractTypeDAL objDAL = new ContractTypeDAL();
            return objDAL.Delete(nCTID);
        }

        public DataTable SelectAll(ContractTypeBOL objContractType)
        {
            ContractTypeDAL objDAL = new ContractTypeDAL();
            return objDAL.SelectAll(objContractType);
        }

        public ContractTypeBOL SearchById(int nContractTypeId)
        {
            ContractTypeDAL objDAL = new ContractTypeDAL();
            return objDAL.SearchById(nContractTypeId);
        }
    }
}
