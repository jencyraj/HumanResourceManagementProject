using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using HRM.BOL;
using HRM.BAL;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class AddTraining : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            DateTime dDate;
            if (Request["SpecificMonth"] != null)
            {
                dDate = Convert.ToDateTime(Request["SpecificMonth"]);
            }
            else
            {
                dDate = DateTime.Now;
            }

            events(dDate);


            GetTrainingTypes();
            GetEmployees();
            Bindlocations();


            GetDetails();
            ddlnatureoftraining.Items.Add(new ListItem(hrmlang.GetString("internal"), "I"));
            ddlnatureoftraining.Items.Add(new ListItem(hrmlang.GetString("external"), "E"));

            lblTrainingID.Text = Request.QueryString["id"];

            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");

            txtsubject.Attributes.Add("placeholder", hrmlang.GetString("trainingsubject"));
            txttitle.Attributes.Add("placeholder", hrmlang.GetString("trainingtitle"));
            txttrainer.Attributes.Add("placeholder", hrmlang.GetString("trainer"));
            txtlocation.Attributes.Add("placeholder", hrmlang.GetString("traininglocation"));
            txtsponsoredby.Attributes.Add("placeholder", hrmlang.GetString("trainingsponsored"));
            txtorganizedby.Attributes.Add("placeholder", hrmlang.GetString("trainingorganized"));
            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            txtadditionalinfo.Attributes.Add("placeholder", hrmlang.GetString("additionalinformation"));
            if (ddlnatureoftraining.SelectedItem.Value == "I")
            {
                Bindlocations();
            }
            else
            {
                txtlocation.Visible = true;
                ddltrainingloc.Visible = false;
            }
        }
    }
    public void events(DateTime dDate)
    {
        DataTable dtEvents = new DataTable();
        TrainingBAL BAl = new TrainingBAL();
        TrainingBOL BOl = new TrainingBOL();
        BOl.TrainingLID = Util.ToInt(ddltrainingloc.SelectedValue);
        dtEvents = BAl.SelectTrainingSchedule(BOl);
        DataTable datatable = new DataTable();
        datatable.Columns.Add("Day"); //day contains day of the month
        datatable.Columns.Add("Data"); //run time produce html & just place on it.
        DataRow oRow;
        for (int i = 1; i <= DateTime.DaysInMonth(dDate.Year, dDate.Month); i++)
        {

            DateTime dCalendarDay = new DateTime(dDate.Year, dDate.Month, i);

            if (i == 1)
            {

                if (dCalendarDay.DayOfWeek.ToString().ToLower() != "sunday")
                {
                    oRow = datatable.NewRow();
                    oRow["Data"] = "<br/><span style='color:Red'>Sun</span>";
                    datatable.Rows.Add(oRow);

                    if (dCalendarDay.DayOfWeek.ToString().ToLower() != "monday")
                    {
                        oRow = datatable.NewRow();
                        oRow["Data"] = "<br/><span style='color:Olive'>Mon</span>";
                        datatable.Rows.Add(oRow);

                        if (dCalendarDay.DayOfWeek.ToString().ToLower() != "tuesday")
                        {
                            oRow = datatable.NewRow();
                            oRow["Data"] = "<br/><span style='color:Olive'>Tue</span>";
                            datatable.Rows.Add(oRow);

                            if (dCalendarDay.DayOfWeek.ToString().ToLower() != "wednesday")
                            {
                                oRow = datatable.NewRow();
                                oRow["Data"] = "<br/><span style='color:Olive'>Wed</span>";
                                datatable.Rows.Add(oRow);

                                if (dCalendarDay.DayOfWeek.ToString().ToLower() != "thursday")
                                {
                                    oRow = datatable.NewRow();
                                    oRow["Data"] = "<br/><span style='color:Olive'>Thu</span>";
                                    datatable.Rows.Add(oRow);

                                    if (dCalendarDay.DayOfWeek.ToString().ToLower() != "friday")
                                    {
                                        oRow = datatable.NewRow();

                                        oRow["Data"] = "<br/><span style='color:Olive'>Fri</span>";
                                        datatable.Rows.Add(oRow);

                                        if (dCalendarDay.DayOfWeek.ToString().ToLower() != "saturday")
                                        {
                                            oRow = datatable.NewRow();
                                            oRow["Data"] = "<br/><span style='color:Olive'>Sat</span>";
                                            datatable.Rows.Add(oRow);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            oRow = datatable.NewRow();

            if (!bWorkingDay(dCalendarDay))
                oRow["Data"] = i.ToString() + "<br/><div style='color:Olive'>" + dCalendarDay.ToString("ddd") + " " + getEvents(dCalendarDay, dtEvents);
            else
                oRow["Data"] = i.ToString() + "<br/><div style='color:Red'>" + dCalendarDay.ToString("ddd") + " " + getEvents(dCalendarDay, dtEvents);

            oRow["Data"] =  oRow["Data"] + "</div>";
            datatable.Rows.Add(oRow);


        }

        dlCalendar.DataSource = datatable;
        dlCalendar.DataBind();

        // here just i am making current date block. 
        if (dDate.Year == DateTime.Now.Year && dDate.Month == DateTime.Now.Month)
        {
            foreach (DataListItem oItem in dlCalendar.Items)
            {
                if (oItem.ItemIndex == DateTime.Now.Day - 1)
                {
                    oItem.BorderStyle = BorderStyle.Solid;
                    oItem.BorderColor = System.Drawing.Color.Transparent;// System.Drawing.Color.DeepSkyBlue; 
                    oItem.BorderWidth = 2;
                }// if closed 
            }// for closed
        }// if closed

    }// !IsPostBack

    public bool bWorkingDay(DateTime bDate)
    {
        // here i am assuming the sunday as holiday but you can make it more efficiently
        // by using a databse if you want to generate country based calendar.
        // In different country they have different holidays schedule.
        if (bDate.ToString("ddd") == "Sun")
            return true;
        return false;
    }
    public string getEvents(DateTime dDate, DataTable dTable)
    {
        string str = "";
        string space = "<br />";

        System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
        KnownColor[] allColors = new KnownColor[colorsArray.Length];
        Array.Copy(colorsArray, allColors, colorsArray.Length);

        Random random = new Random();

        foreach (DataRow oItem in dTable.Rows)
        {
            int nPos = random.Next(27, allColors.Length - 1);

            if (("" + oItem["Fdate"]) == dDate.ToString())//"dd ,MMM, yyyy"
            {
                str = space + str + " <div onclick=\"showdata(this);\" style=\"float:left;margin-left:3px;border-radius:10px;cursor:pointer; background-color:" + allColors[nPos] + ";width:15px;height:15px;\" title=\"" + oItem["Title"] + "\">&nbsp;<p class=\"ptitle hide\">" + oItem["Title"] + "</p><p class=\"ptext hide\" >" + oItem["description"] + "</p></div>";
                space = "";
            }
        }
        return str;
    }


  
    protected void RfreshData(object sender, System.EventArgs e)
    {
        //redirect by date thats why page can render the calendar according to user selection
        Response.Redirect("AddTraining.aspx?SpecificMonth=" + ((DropDownList)sender).SelectedValue);
    }
    public void Bindlocations()
    {
        TrainingBAL objBAL = new TrainingBAL();
        DataTable DT = objBAL.selecttraining_Location();
        ddltrainingloc.DataSource = DT;
        ddltrainingloc.DataBind();


    }
    private void GetTrainingTypes()
    {
        TrainingTypeBAL objBAL = new TrainingTypeBAL();
        ddltrainingtype.DataSource = objBAL.SelectAll(0);
        ddltrainingtype.DataBind();
        ddltrainingtype.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }

    private void GetEmployees()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objEmp = new EmployeeBOL();
        ddlEmployees.DataSource = objBAL.Search(objEmp);
        ddlEmployees.DataBind();
    }




    private void GetDetails()
    {
        int ComID = Util.ToInt(Request.QueryString["id"]);
        if (ComID == 0) return;

        TrainingBAL objBAL = new TrainingBAL();
        TrainingBOL objCy = new TrainingBOL();

        objCy = objBAL.SelectByID(ComID);
        if (objCy == null) return;

        lblTrainingID.Text = ComID.ToString();

        string[] emplist = objCy.Employee.Split(',');
        for (int i = 0; i < emplist.Length; i++)
        {
            ListItem lItem = ddlEmployees.Items.FindByValue("" + emplist[i]);
            if (lItem != null)
            {
                lItem.Selected = true;
            }
        }
        //  ddlEmployees.SelectedValue = objCy.Employee;

        ddltrainingtype.SelectedValue = objCy.trainingtype.ToString();
        txtsubject.Text = objCy.subject;
        ddlnatureoftraining.SelectedValue = objCy.nature.ToString();
        if (objCy.nature == "I")
        {
            ddltrainingloc.Visible = true;
            txtlocation.Visible = false;
            ddltrainingloc.SelectedValue = objCy.TrainingLID.ToString();
        }
        else
        {
            ddltrainingloc.Visible = false;
            txtlocation.Visible = true;
            txtlocation.Text = objCy.location;
        }

        txttitle.Text = objCy.title;
        txttrainer.Text = objCy.trainer;

        txtsponsoredby.Text = objCy.sponseredby;
        txtorganizedby.Text = objCy.organizedby;
        if (!string.IsNullOrEmpty(objCy.fromdt))
        {
            Dtfrom.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            Dtfrom.SelectedCalendareDate = DateTime.ParseExact(objCy.fromdt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        if (!string.IsNullOrEmpty(objCy.todt))
        {
            Dtto.DefaultCalendarCulture = UserControls_ctlCalendar.DefaultCultureOption.Grgorian;
            Dtto.SelectedCalendareDate = DateTime.ParseExact(objCy.todt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        //  Dtto.Text = objCy.todt.ToString();
        txtDesc.Text = objCy.Description;
        txtadditionalinfo.Text = objCy.note;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //dvMsg.Visible = true;

        try
        {

            TrainingBAL objBAL = new TrainingBAL();
            TrainingBOL objCy = new TrainingBOL();
            //  ddlEmployees.SelectedValue = objCy.Employee.ToString();
            objCy.Employee = "";
            objCy.TrainingID = Util.ToInt(lblTrainingID.Text);
            for (int i = 0; i < ddlEmployees.Items.Count; i++)
            {
                if (ddlEmployees.Items[i].Selected)
                {
                    objCy.Employee = (objCy.Employee == "") ? ddlEmployees.Items[i].Value : objCy.Employee + "," + ddlEmployees.Items[i].Value;
                }
            }
            //  objCy.Employee = ddlEmployees.SelectedValue;
            objCy.trainingtype = Util.ToInt(ddltrainingtype.SelectedValue);
            objCy.subject = txtsubject.Text;
            objCy.nature = ddlnatureoftraining.SelectedValue;
            objCy.title = txttitle.Text;
            objCy.trainer = txttrainer.Text;
            if (ddlnatureoftraining.SelectedValue == "I")
            {
                objCy.location = ddltrainingloc.SelectedItem.Text;
                objCy.TrainingLID = Util.ToInt(ddltrainingloc.SelectedValue);
            }
            else
            {
                objCy.location = txtlocation.Text;
            }
            objCy.sponseredby = txtsponsoredby.Text;
            objCy.organizedby = txtorganizedby.Text;
            try
            {
                string[] CommissionDateArr = Dtfrom.getGregorianDateText.ToString().Split('/');
                objCy.fromdt = CommissionDateArr[1] + "/" + CommissionDateArr[0] + "/" + CommissionDateArr[2];


            }
            catch
            {
            }
            try
            {
                string[] Dtto1 = Dtto.getGregorianDateText.ToString().Split('/');
                objCy.todt = Dtto1[1] + "/" + Dtto1[0] + "/" + Dtto1[2];

            }
            catch
            {
            }
            if (ddlnatureoftraining.SelectedValue == "I")
            {
                objCy.TrainingLID = Util.ToInt(ddltrainingloc.SelectedValue);
                try
                {
                    DataTable DTb = new DataTable();
                    DTb = objBAL.SelectTrainingSchedule(objCy);
                    if (DTb.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTb.Rows.Count; i++)
                        {


                        }


                    }

                }
                catch
                {
                }
            }

            objCy.Description = txtDesc.Text;
            objCy.note = txtadditionalinfo.Text;

            objCy.Status = "Y";
            objCy.CreatedBy = User.Identity.Name;
            objBAL.Save(objCy);

            lblMsg.Text = hrmlang.GetString("trainingsaved");

            Response.Redirect("Training.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblTrainingID.Text = "";
        // ddlEmployees.SelectedIndex = 0;

        ddltrainingtype.SelectedIndex = 0;
        txtsubject.Text = "";
        ddlnatureoftraining.SelectedIndex = 0;
        txttitle.Text = "";
        txttrainer.Text = "";
        txtlocation.Text = "";
        txtsponsoredby.Text = "";
        txtorganizedby.Text = "";
        Dtfrom.ClearDate();// = DateTime.Today;
        Dtto.ClearDate();// = DateTime.Today;
        txtDesc.Text = "";
        txtadditionalinfo.Text = "";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTraining.aspx");
    }

    protected void ddlnatureoftraining_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnatureoftraining.SelectedItem.Value == "I")
        {
            ddltrainingloc.Visible = true;
            txtlocation.Visible = false;
            Bindlocations();
        }
        else
        {
            txtlocation.Visible = true;
            ddltrainingloc.Visible = false;
        }
    }
  
    private static bool CheckAlphaNumeric(string str)
    {

        return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");


    }

    protected void ddltrainingloc_SelectedIndexChanged(object sender, EventArgs e)
    {
        events(DateTime.Now);// ClientScript.RegisterStartupScript(this.GetType(), "onchange", "ShowCalendar();", true);
    }
    protected void dlCalendar_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        // Just populationg my header section
        DateTime dDate;
        if (Request["SpecificMonth"] != null)
            dDate = Convert.ToDateTime(Request["SpecificMonth"]);
        else
            dDate = DateTime.Now;
        if (e.Item.ItemType == ListItemType.Header)
        {
            DropDownList oPrev = (DropDownList)e.Item.FindControl("cboPrev");
            DropDownList oNext = (DropDownList)e.Item.FindControl("cboNext");
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("year4");
            dtYear.Columns.Add("sValue");
            //here i am assuming that when user click on 2009 he wants to see january 2009 calendar.
            //it will be more interective if you generate the current month(shown in the page) calendar.
            for (int i = DateTime.Today.Year; i > DateTime.Today.Year - 2; i--)
                dtYear.Rows.Add(i, "01 Jan, " + i.ToString());

            oPrev.DataTextField = "year4";
            oPrev.DataValueField = "sValue";
            oPrev.DataSource = dtYear;
            oPrev.DataBind();
            oNext.DataTextField = "year4";
            oNext.DataValueField = "sValue";
            oNext.DataSource = dtYear;
            oNext.DataBind();

            ((Label)e.Item.FindControl("lblLeft")).Text = "<a style=color:Black href=AddTraining.aspx?SpecificMonth=" + dDate.AddMonths(-1).ToString("dd-MMMM-yyyy") + ">" + dDate.AddMonths(-1).ToString("MMMM yyyy") + "</a>";
            ((Label)e.Item.FindControl("lblMiddle")).Text = dDate.ToString("MMMM yyyy");
            ((Label)e.Item.FindControl("lblRight")).Text = "<a style=color:Black href=AddTraining.aspx?SpecificMonth=" + dDate.AddMonths(+1).ToString("dd-MMMM-yyyy") + ">" + dDate.AddMonths(+1).ToString("MMMM yyyy") + "</a>";

        }
    }
}