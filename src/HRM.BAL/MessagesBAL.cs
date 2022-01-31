using System;
using System.Data;
using System.Text;

using HRM.BOL;
using HRM.DAL;

namespace HRM.BAL
{
    public class MessagesBAL
    {
        public int Save(MessagesBOL objBOL)
        {
            return new MessagesDAL().Save(objBOL);
        }

        public int Delete(int nEMID)
        {
            return new MessagesDAL().Delete(nEMID);
        }

        public DataTable SelectAll(MessagesBOL objBOL)
        {
            return new MessagesDAL().SelectAll(objBOL);
        }
    }
}
