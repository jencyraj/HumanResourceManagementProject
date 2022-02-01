using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

/// <summary>
/// Summary description for ConvertDates
/// </summary>
public class ConvertDates
{
    private static HttpContext cur;
    private const int startGreg = 1900;
    private const int endGreg = 2100;
    private static string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
            "dd/MM/yyyy","d/M/yyyy",
            "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
            "yyyy-M-d","dd-MM-yyyy","d-M-yyyy",
            "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
            "yyyy M d","dd MM yyyy","d M yyyy",
            "dd M yyyy","d MM yyyy","MM/dd/yyyy"};
    private static CultureInfo arCul;
    private static CultureInfo enCul;

    private HijriCalendar h;
    private GregorianCalendar g;

	public ConvertDates()
	{
		//
		// TODO: Add constructor logic here
		//
       

        h = new HijriCalendar();
        g = new GregorianCalendar(GregorianCalendarTypes.USEnglish);

        arCul.DateTimeFormat.Calendar = h;
       // enCul.DateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
	}

    /// <summary>
    /// Convert Gregoian Date to it's equivalent Hijir Date
    /// </summary>
    /// <PARAM name="greg"></PARAM>
    /// <returns></returns>
    public static string GregToHijri(string greg)
    {
        cur = HttpContext.Current;

        arCul = new CultureInfo("ar-SA");
        enCul = new CultureInfo("en-US");

        greg = CleanString(greg);

        if (greg.Length <= 0)
        {

            //cur.Trace.Warn("GregToHijri :Date String is Empty");
            return "";
        }
        try
        {
            DateTime tempDate = DateTime.ParseExact(greg, allFormats,
                enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate.ToString("yyyy/MM/dd", arCul.DateTimeFormat);

        }
        catch (Exception ex)
        {
          //  cur.Trace.Warn("GregToHijri :" + greg.ToString() + "\n" + ex.Message);
            return "";
        }
    }

    public static string CleanString(string sVal)
    {
        sVal = sVal.Replace("&#39;", "'");
        return sVal.Replace("&nbsp;", "");
    }

    /// <summary>
    /// Convert Hijri Date to it's equivalent Gregorian Date and
    /// return it in specified format
    /// </summary>
    /// <PARAM name="greg"></PARAM>
    /// <PARAM name="format"></PARAM>
    /// <returns></returns>
    public string GregToHijri(string greg, string format)
    {

        if (greg.Length <= 0)
        {

           // cur.Trace.Warn("GregToHijri :Date String is Empty");
            return "";
        }
        try
        {

            DateTime tempDate = DateTime.ParseExact(greg, allFormats,   enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate.ToString(format, arCul.DateTimeFormat);

        }
        catch (Exception ex)
        {
            //cur.Trace.Warn("GregToHijri :" + greg.ToString() + "\n" + ex.Message);
            return "";
        }
    }
        
}