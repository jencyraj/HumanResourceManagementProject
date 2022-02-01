using System;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    public class Util
    {
        public static string GetString(object sVal)
        {
            return Convert.ToString("" + sVal).Trim();
        }

        public static int ToInt(object sVal)
        {
            try
            {
                return int.Parse("" + sVal);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal ToDecimal(object sVal)
        {
            try
            {
                return decimal.Parse("" + sVal);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static DateTime ToDateTime(object sVal)
        {
            try
            {
                return DateTime.Parse("" + sVal);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}
