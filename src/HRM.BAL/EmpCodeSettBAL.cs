using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRM.DAL;
using HRM.BOL;
using System.Data;
namespace HRM.BAL
{
  public  class EmpCodeSettBAL
    {
        public int Save(EmpCodeSettBOL objTrainingType)
        {
            EmpCodeSettDAL objDAL = new EmpCodeSettDAL();
            return objDAL.Save(objTrainingType);
        }
        public DataSet GetNextEmployeeCode(string Item,string order)
        {
            EmpCodeSettDAL objDAL = new EmpCodeSettDAL();
            return objDAL.GetNextEmployeeCode(Item,order);
        }
        public int Delete(string nTID)
        {
            EmpCodeSettDAL objDAL = new EmpCodeSettDAL();
            return objDAL.Delete(nTID);
        }

        public DataSet SelectAll()
        {
            EmpCodeSettDAL objDAL = new EmpCodeSettDAL();
            return objDAL.SelectAll();
        }
    }
}
