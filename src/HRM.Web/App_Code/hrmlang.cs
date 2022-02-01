using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Resources;
using System.Xml;

using HRM.BOL;
using HRM.BAL;

/// <summary>
/// Summary description for hrmlang
/// </summary>
public class hrmlang
{
    public hrmlang()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetStringOld(string sLangKey)
    {
        string LangText = "";
        DataTable dt = HttpContext.Current.Cache.Get("LangData") as DataTable;
        if (dt == null)
        {
            dt = new LangDataBAL().Select(new LangDataBOL());
            HttpContext.Current.Cache.Insert("LangData", dt, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        string LangCultureName = "" + System.Web.HttpContext.Current.Session["LanguageId"];
        if (LangCultureName == "")
            LangCultureName = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"];
        DataRow[] row = dt.Select(string.Format("LangCultureName='{0}' AND LangKey LIKE '{1}'", LangCultureName, sLangKey));
        if (row != null)
        {
            if (row.Length > 0)
                LangText = "" + row[0]["LangText"];
        }
        return LangText.Trim();
    }


    #region LANG VIA XML

    public static string GetStringXML(string sLangKey)
    {
        string LangText = "";

        string LangCultureName = "" + System.Web.HttpContext.Current.Session["LanguageId"];
        if (LangCultureName == "")
            LangCultureName = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"];

        string sPath = System.Web.HttpContext.Current.Server.MapPath("StringResources");

        if (System.IO.Directory.Exists(sPath))
        {
            if (System.IO.File.Exists("StringResources/" + LangCultureName + ".xml") == false)
                GenerateLangXML(LangCultureName);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(System.Web.HttpContext.Current.Server.MapPath("StringResources/" + LangCultureName + ".xml"));

            XmlNodeList xnList = xml.SelectNodes("/LangData");
            foreach (XmlNode xn in xnList)
                LangText = xn[sLangKey].InnerText;
        }

        return LangText.Trim();
    }


    public static void GenerateLangXML(string LangCultureName)
    {
        LangDataBOL objBOL = new LangDataBOL();
        objBOL.LangCultureName = LangCultureName;

        DataTable dt = new LangDataBAL().Select(objBOL);

        if (dt.Rows.Count == 0) return;

        string sPath = System.Web.HttpContext.Current.Server.MapPath("StringResources");

        if (!System.IO.Directory.Exists(sPath))
            System.IO.Directory.CreateDirectory(sPath);

        string sFilePath = sPath + "/" + LangCultureName + ".xml";


        XmlTextWriter writer = new XmlTextWriter(sFilePath, null);
        writer.WriteStartDocument(true);
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 2;


        writer.WriteStartElement("LangData");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            writer.WriteStartElement("" + dt.Rows[i]["LangKey"]);
            writer.WriteString(ReplaceSpecialChars("" + dt.Rows[i]["LangText"]));
            writer.WriteEndElement();
        }

        writer.WriteEndElement();//</LangData>

        writer.WriteEndDocument();
        writer.Close();
    }

    private static string ReplaceSpecialChars(string sVal)
    {
        if (sVal == "")
            sVal = " ";

        return sVal.Replace("&", "amp;");
    }

    private string SetSpecialChars(string sVal)
    {
        return sVal.Replace("amp;", "&");
    }

    #endregion

    public static void GenerateLangResourceFile(string LangCultureName)
    {
        LangDataBOL objBOL = new LangDataBOL();
        objBOL.LangCultureName = LangCultureName;

        DataTable dt = new LangDataBAL().Select(objBOL);

        if (dt.Rows.Count == 0) return;

        string sPath = System.Web.HttpContext.Current.Server.MapPath("StringResources");

        if (!System.IO.Directory.Exists(sPath))
            System.IO.Directory.CreateDirectory(sPath);

        string sExtn = ".resx";
        string resourcefile = "\\hrm-" + LangCultureName;

        if (System.IO.File.Exists(sPath + resourcefile + sExtn))
        {
            // System.IO.File.Copy(sPath + resourcefile, sPath + DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            System.IO.File.Copy(sPath + resourcefile + sExtn, sPath + resourcefile + DateTime.Now.ToString("ddMMMyyyyHHmmss") + sExtn);
            System.IO.File.Delete(sPath + resourcefile + sExtn);
        }

        using (ResXResourceWriter resx = new ResXResourceWriter(System.Web.HttpContext.Current.Server.MapPath("StringResources") + resourcefile + sExtn))
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                resx.AddResource("" + dt.Rows[i]["LangKey"], "" + dt.Rows[i]["LangText"]);
            }
        }
    }



    public static string ReadResourceValue(string LangCultureName, string Langkey)
    {

        string resourceValue = string.Empty;
        try
        {

            string resourceFile = "hrm-" + LangCultureName + ".resx";

            string FilePath = System.Web.HttpContext.Current.Server.MapPath("StringResources");

            if (!System.IO.Directory.Exists(FilePath))
                GenerateLangResourceFile(LangCultureName);

            if (!System.IO.File.Exists(FilePath + "\\" + resourceFile))
                GenerateLangResourceFile(LangCultureName);


            ResXResourceReader resReader = new ResXResourceReader(FilePath + "\\" + resourceFile);

            //Enumerate the elements within the resx file and dispaly them

            foreach (DictionaryEntry d in resReader)
            {
                if ("" + d.Key == Langkey)
                {
                    resourceValue = "" + d.Value;
                    break;
                }

            }

            //Close the resxReader

            resReader.Close();


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            resourceValue = string.Empty;
        }
        return resourceValue;
    }


    public static string GetString(string sLangKey)
    {

        string LangCultureName = "" + System.Web.HttpContext.Current.Session["LanguageId"];
        if (LangCultureName == "")
            LangCultureName = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"];

        return ReadResourceValue(LangCultureName, sLangKey);

    }
}