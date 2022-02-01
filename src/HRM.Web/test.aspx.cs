using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            initCalendar();

    }

    private void initCalendar()
    {
        DateTime dt;

        //Create an object of Hijri Calendar
        HijriCalendar hijriCal = new HijriCalendar();

        //Selects the Date of the four months after
        calHijriCalendar.SelectedDate = hijriCal.AddMonths(DateTime.Now, 4).Date;
        calHijriCalendar.VisibleDate = calHijriCalendar.SelectedDate;

        //Convert the seleted Date to Hijri Format
        dt = Convert.ToDateTime(calHijriCalendar.SelectedDate);

        //Sets the date in Arabic format
        CultureInfo ci = new CultureInfo("ar-SA", false);
        ci.DateTimeFormat.Calendar = new System.Globalization.HijriCalendar();
        ci.DateTimeFormat.ShortDatePattern = "" + calHijriCalendar.SelectedDate;

        //Displays it on the Web Form
        Response.Write(dt.Date.ToString("D", ci));
    }
}