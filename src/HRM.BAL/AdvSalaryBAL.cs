using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AdvSalaryBAL
    {
        public int Save(AdvSalaryBOL objAdvSalary)
        {
            AdvSalaryDAL objDAL = new AdvSalaryDAL();
            return objDAL.Save(objAdvSalary);
        }

        public int Delete(int nAdvSalaryId)
        {
            AdvSalaryDAL objDAL = new AdvSalaryDAL();
            return objDAL.Delete(nAdvSalaryId);
        }

        public DataTable SelectAll(AdvSalaryBOL objAdvSalary)
        {
            AdvSalaryDAL objDAL = new AdvSalaryDAL();
            return objDAL.SelectAll(objAdvSalary);
        }

        public AdvSalaryBOL SearchById(int nAdvSalaryId)
        {
            AdvSalaryDAL objDAL = new AdvSalaryDAL();
            return objDAL.SearchById(nAdvSalaryId);
        }
    }
}
