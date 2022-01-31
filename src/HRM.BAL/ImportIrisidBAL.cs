using System;
using System.Data;
using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
  public  class ImportIrisidBAL
    {
      public int UPDATE(string fname, string mname, string lname, string irisid, string createdby)
      {
          return new ImportIrisDAL().UPDATE(fname, mname, lname,irisid,createdby);
      }
      public DataTable Select(string createdby, string imptype)
      {
          return new ImportIrisDAL().Select(createdby, imptype);
      }
    }
}
