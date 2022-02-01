using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;


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

    public static string CleanString(string sVal)
    {
        sVal = sVal.Replace("&#39;", "'");
        return sVal.Replace("&nbsp;", "");
    }

    public static DateTime ToDateTime(object sVal)
    {
        try
        {
            if ("" + sVal == "") return DateTime.MinValue;
            string[] sDate = sVal.ToString().Split('/');
            sVal = sDate[1] + "/" + sDate[0] + "/" + sDate[2];
            return DateTime.Parse("" + sVal);
        }
        catch (Exception)
        {
            return DateTime.MinValue;
        }
    }

    public static DateTime ToUSDateTime(object sVal)
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

    public static decimal Get_Hours_MinusFromHundred(decimal value1, decimal value2)
    {
        /* try
         {
             string[] sVal = value.ToString().Split('.');
             if (Util.ToDecimal(sVal[1]) > 59)
             {
                 sVal[1] = Convert.ToString(60 - (100 - (Util.ToDecimal(sVal[1]))));
                 sVal[1] = Convert.ToString(Util.ToDecimal(sVal[1]) / 100);
                 value = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);
             }
             return value;
         }
         catch (Exception ex)
         {
             return value;
         }*/

        try
        {
            string[] sVal = value1.ToString().Split('.');
            if (sVal.Length > 1)
                value1 = (Math.Floor(value1) * 60) + Util.ToDecimal(sVal[1]);
            else
                value1 = (Math.Floor(value1) * 60);

            string[] sVal2 = value2.ToString().Split('.');
            if (sVal2.Length > 1)
                value2 = (Math.Floor(value2) * 60) + Util.ToDecimal(sVal2[1]);
            else
                value2 = (Math.Floor(value2) * 60);

            value1 = value1 - value2;

            if (value1 > 0)
            {
                //  sVal = value1.ToString().Split('.');
                value1 = Math.Floor(value1 / 60) + ((Util.ToDecimal(value1) % 60) / 100);
                return value1;
            }
            else
                return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public static decimal Get_Hours(decimal value)
    {
        try
        {

            string[] sVal = value.ToString().Split('.');

            if (sVal.Length <= 1)
                return value;

            if (Util.ToDecimal(sVal[1]) > 59)
            {
                sVal[0] = (Util.ToDecimal(sVal[0]) + (Util.ToInt(sVal[1]) / 60)).ToString();
                sVal[1] ="" + (Util.ToInt(sVal[1]) % 100);
                sVal[1] = "" + ((Util.ToInt(sVal[1])) - ((Util.ToInt(sVal[1]) / 60)*60));
                sVal[1] = "" + (Util.ToDecimal(sVal[1]) / 100);
                value = Util.ToDecimal(sVal[0]) + Util.ToDecimal(sVal[1]);
            }
            return value;
        }
        catch (Exception ex)
        {
            return value;
        }

    }

    public static decimal Get_Regular_wage(decimal value, decimal rate, decimal rateover)
    {
        try
        {
            string[] sVal = value.ToString().Split('.');
            if (sVal.Length > 1)
                value = Math.Round((Util.ToDecimal(sVal[0]) + (Util.ToDecimal(sVal[1]) / 60)) * rate * rateover, 2);
            else
                value = Math.Round(Util.ToDecimal(sVal[0]) * rate * rateover, 2);

            return value;
        }
        catch (Exception ex)
        {
            return value;
        }

    }
    public static string RearrangeDateTime(object sVal)
    {
        try
        {
            if ("" + sVal == "") return "";
            string[] sDate = sVal.ToString().Split('/');
            sVal = sDate[1] + "/" + sDate[0] + "/" + sDate[2];
            return "" + sVal;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string[] CheckPermission(DataSet dSet, string sPageName)
    {
        /*string[] permissions = new string[4];

        if (dSet == null) return permissions;
        if (dSet.Tables.Count == 2)
        {
            DataTable dt = dSet.Tables[1];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dRow = dt.Select("lowerPageName='" + sPageName.ToLower() + "'");
                if (dRow.Length > 0)
                {
                    permissions[0] = "" + dRow[0]["ALLOWINSERT"];
                    permissions[1] = "" + dRow[0]["ALLOWUPDATE"];
                    permissions[2] = "" + dRow[0]["ALLOWDELETE"];
                    permissions[3] = "" + dRow[0]["ALLOWVIEW"];
                }
            }
        }

        if (permissions[0] == "N" && permissions[1] == "N" && permissions[2] == "N" && permissions[3] == "N")
        {
            ShowNoPermissionPage();
            return null;
        }
        else
            return permissions;*/

        string[] permissions = new string[4];

        if (dSet == null) return permissions;
        if (dSet.Tables.Count == 1)
        {
            DataTable dt = dSet.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dRow = dt.Select("lowerPageName='" + sPageName.ToLower() + "'");
                if (dRow.Length > 0)
                {
                    permissions[0] = "" + dRow[0]["ALLOWINSERT"];
                    permissions[1] = "" + dRow[0]["ALLOWUPDATE"];
                    permissions[2] = "" + dRow[0]["ALLOWDELETE"];
                    permissions[3] = "" + dRow[0]["ALLOWVIEW"];
                }
                else
                {
                    permissions[0] = "N";
                    permissions[1] = "N";
                    permissions[2] = "N";
                    permissions[3] = "N";
                }
            }
        }

        if (permissions[0] == "N" && permissions[1] == "N" && permissions[2] == "N" && permissions[3] == "N")
        {
            ShowNoPermissionPage();
            return null;
        }
        else
            return permissions;
    }

    public static string[] CheckPermission(DataSet dSet, string sPageName, bool redirect_to_access_denied)
    {
        string[] permissions = new string[4];

        if (dSet == null) return permissions;
        if (dSet.Tables.Count == 1)
        {
            DataTable dt = dSet.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dRow = dt.Select("lowerPageName='" + sPageName.ToLower() + "'");
                if (dRow.Length > 0)
                {
                    permissions[0] = "" + dRow[0]["ALLOWINSERT"];
                    permissions[1] = "" + dRow[0]["ALLOWUPDATE"];
                    permissions[2] = "" + dRow[0]["ALLOWDELETE"];
                    permissions[3] = "" + dRow[0]["ALLOWVIEW"];
                }
                else
                {
                    permissions[0] = "N";
                    permissions[1] = "N";
                    permissions[2] = "N";
                    permissions[3] = "N";
                }
            }
        }

        return permissions;
    }

    public static DataTable ReturnDT(string sFldID, string sFldName, DataTable dt)
    {
        DataRow dRow = dt.NewRow();
        dRow[sFldID] = "0";
        dRow[sFldName] = hrmlang.GetString("select");
        dt.Rows.InsertAt(dRow, 0);
        return dt;
    }


    public static void ShowNoPermissionPage()
    {
        System.Web.HttpContext.Current.Response.Redirect("AccessDenied.aspx");
    }

    public static void Log(string sErrMsg)
    {
        string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

        string Errordir = ConfigurationManager.AppSettings["LogPath"];
        Errordir = HttpContext.Current.Server.MapPath(Errordir);
        if (!System.IO.Directory.Exists(Errordir))
        {
            System.IO.Directory.CreateDirectory(Errordir);
        }

        string dtval = DateTime.Now.ToString("dd-MM-yyyy");
        StreamWriter sw = new StreamWriter(Errordir + "\\" + dtval + "ihrm_log.txt", true);

        sw.WriteLine(sLogFormat + sErrMsg + "\n");
        sw.Flush();
        sw.Close();
    }

    public static void ErrorLog(string sErrMsg)
    {
        string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
        string Errordir = ConfigurationManager.AppSettings["ErrorPath"];

        Errordir = HttpContext.Current.Server.MapPath(Errordir);
        if (!System.IO.Directory.Exists(Errordir))
        {
            System.IO.Directory.CreateDirectory(Errordir);
        }

        string dtval = DateTime.Now.ToString("dd-MM-yyyy");
        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["ErrorPath"] + "\\" + dtval + "ihrm_error.txt", true);
        sw.WriteLine(sLogFormat + sErrMsg);

        sw.Flush();
        sw.Close();
    }


}
