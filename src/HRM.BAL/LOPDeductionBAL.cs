using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HRM.BOL;
using HRM.DAL;
namespace HRM.BAL
{
   public class LOPDeductionBAL
    {
       public DataTable SelectAll(LOPDeductionBOL objOvertime)
       {
           LOPDeductionDAL objDAL = new LOPDeductionDAL();
           return objDAL.SelectAll(objOvertime);
       }
       public DataTable SelectLeaveType(LOPDeductionBOL objleave)
       {
           LOPDeductionDAL objLT = new LOPDeductionDAL();
           return objLT.SelectLeaveType(objleave);
       }
       public DataTable Get_LOP_WrkngHour(LOPDeductionBOL objleave)
       {
           LOPDeductionDAL objLT = new LOPDeductionDAL();
           return objLT.Get_LOP_WrkngHour(objleave);
       }
       public DataTable SelectMinLeav(LOPDeductionBOL objleave)
       {
           LOPDeductionDAL objLT = new LOPDeductionDAL();
           return objLT.SelectMinLeav(objleave);
       }
      
    }
}
