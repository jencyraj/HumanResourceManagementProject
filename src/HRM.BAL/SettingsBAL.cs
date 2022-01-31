using System;
using System.Data;

using HRM.DAL;

namespace HRM.BAL
{
    public class SettingsBAL
    {
        public int Save(string ConfigCode, string ConfigValue)
        {
            return new SettingsDAL().Save(ConfigCode, ConfigValue);
        }

        public DataTable SelectAll()
        {
            return new SettingsDAL().SelectAll();
        }
    }
}
