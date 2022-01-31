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
    public class AttendanceTypeBAL
    {
        public int Save(AttendanceTypeBOL objAttendanceType)
        {
            AttendanceTypeDAL objDAL = new AttendanceTypeDAL();
            return objDAL.Save(objAttendanceType);
        }

        public int Delete(int nATId)
        {
            AttendanceTypeDAL objDAL = new AttendanceTypeDAL();
            return objDAL.Delete(nATId);
        }

        public DataTable SelectAll(AttendanceTypeBOL objAttendanceType)
        {
            AttendanceTypeDAL objDAL = new AttendanceTypeDAL();
            return objDAL.SelectAll(objAttendanceType);
        }

        public AttendanceTypeBOL SearchById(int nATId)
        {
            AttendanceTypeDAL objDAL = new AttendanceTypeDAL();
            return objDAL.SearchById(nATId);
        }
    }
}
