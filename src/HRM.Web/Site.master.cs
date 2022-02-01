using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using HRM.BAL;
using HRM.BOL;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected string strCULTURE = "en-US";
    protected string STYLESHEET = "LR";

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty("" + Session["LanguageId"]))
        //{
        //    Response.Redirect("signin.aspx");
        //}
        if (HttpContext.Current.User.Identity.Name == "")
            Response.Redirect("signin.aspx");
        else
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["usersettings"] != null)
                {
                    HttpCookie cookie = Request.Cookies["usersettings"];
                    Session["USERID"] = cookie.Values["ihrUserName"].ToString();
                }

                if ("" + Session["USERID"] == "")
                    Response.Redirect("SignIn.aspx");


                EmployeeBOL objEmp = GetUserData();
                BindLanguageDD();
                if (string.IsNullOrEmpty("" + Session["LanguageId"]))
                    Session["LanguageId"] = ddLanguages.SelectedValue;
                else
                {
                    ddLanguages.ClearSelection();
                    ddLanguages.SelectedValue = Session["LanguageId"].ToString();
                }

                GetPermissions(objEmp);
                GetCompanyDetails();
                SetLangData();

                GetMessages();
                GetAlerts();
            }

            if ("" + Session["EMPID"] == "")
                Response.Redirect("signin.aspx");
        }

        if ("" + Session["MULTILANG"] == "")
        {
            Session["LanguageId"] = "" + ConfigurationManager.AppSettings["DefaultLanguage"];
            ddLanguages.Visible = false;
        }
        else
        {
            ddLanguages.Visible = true;
            Session["LanguageId"] = ddLanguages.SelectedValue;
            ListItem lItem = ddlLg.Items.FindByValue(ddLanguages.SelectedValue);
            if (lItem != null)
                STYLESHEET = lItem.Text;
        }

        strCULTURE = "" + Session["LanguageId"];

        Session["STYLESHEET"] = STYLESHEET;
    }


    private void GetMessages()
    {
        MessagesBOL objBOL = new MessagesBOL();
        objBOL.SentTo = "" + Session["EMPID"];
        DataTable dTable = new MessagesBAL().SelectAll(objBOL);
        if (dTable.Rows.Count > 0)
        {
            DataView dView = dTable.DefaultView;
            dView.RowFilter = "msgread='N'";
            ltMsgCount.Text = dView.ToTable().Rows.Count.ToString();
            ltMsgHdr.Text = string.Format(hrmlang.GetString("youhavemsgs"), ltMsgCount.Text);

            for (int i = 2; i < dView.Count; i++)
                dView.Delete(i);
            rptrMsg.DataSource = dView.ToTable();
            rptrMsg.DataBind();
        }
    }

    private void GetAlerts()
    {
        DataTable dtAlerts = new AlertsBAL().Select(Util.ToInt("" + Session["EMPID"]));
        ViewState["ALERTCOUNT"] = dtAlerts.Rows.Count;

        if (dtAlerts.Rows.Count > 0)
            ltAlertCount.Text = "<span class=\"label label-warning\">" + dtAlerts.Rows.Count.ToString() + "</span>";
        else
            lblAlerts.Text = hrmlang.GetString("noalerts");

        if (dtAlerts.Rows.Count > 0)
        {
            DataView dView = dtAlerts.DefaultView;

            gvAlerts.DataSource = dView;
            gvAlerts.DataBind();

            for (int i = 5; i < dView.Count; i++)
                dView.Delete(i);

            rptrAlerts.DataSource = dView;
            rptrAlerts.DataBind();

        }
    }

    protected void ddLanguages_IndexChanged(object sender, EventArgs e)
    {
        string PageName = this.Page.ToString().Replace("ASP.", string.Empty).Replace("_", ".");
        PageName = PageName[0].ToString().ToUpper() + PageName.Substring(1) + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        HttpContext.Current.Cache["PageName"] = PageName;
        Response.Redirect(string.Format("RedirectLang.aspx?LanguageId={0}", ddLanguages.SelectedValue));
    }

    private void BindLanguageDD()
    {
        DataTable dt = new LangDataBAL().SelectLanguage(0);
        DataView dView = dt.DefaultView;
        dView.RowFilter = "Active='Y'";
        ddLanguages.DataValueField = "LangCulturename";
        ddLanguages.DataTextField = "LangName";
        ddLanguages.DataSource = dView.ToTable();
        ddLanguages.DataBind();

        ddlLg.DataSource = dView.ToTable();
        ddlLg.DataBind();
        DataRow[] dR = dt.Select("DefaultLang='Y'");
        if (dR.Length > 0)
        {
            ddLanguages.SelectedValue = "" + dR[0]["LangCulturename"];
            ddlLg.SelectedValue = "" + dR[0]["LangCulturename"];
        }
    }

    private void SetLangData()
    {
        DataTable dtLangData = HttpContext.Current.Cache["LangData"] as DataTable;
        if (dtLangData == null)
        {
            dtLangData = new LangDataBAL().Select(new LangDataBOL());
            HttpContext.Current.Cache.Insert("LangData", dtLangData, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        else if (dtLangData.Rows.Count == 0)
        {
            dtLangData = new LangDataBAL().Select(new LangDataBOL());
            HttpContext.Current.Cache.Insert("LangData", dtLangData, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
        }
    }

    private void GetCompanyDetails()
    {
        OrganisationBAL objBAL = new OrganisationBAL();
        OrganisationBOL objBOL = objBAL.Select();
        Session["COMPANYID"] = objBOL.CompanyID.ToString();
        if (objBOL != null)
        {
            if (objBOL.LogoName != "")
                imgLogo.ImageUrl = Session["BASEURL"] + "images/Logo/" + objBOL.LogoName;
            // imgLogo.ImageUrl = ConfigurationManager.AppSettings["ROOTURL"] + "images/Logo/" + objBOL.LogoName;

        }
    }

    private EmployeeBOL GetUserData()
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objEmp = new EmployeeBOL();
        objEmp.UserID = "" + Session["USERID"];// HttpContext.Current.User.Identity.Name;
        objEmp = objBAL.Select(objEmp);

        lbl_R1_UserID.Text = objEmp.FirstName + ((objEmp.LastName != "") ? " " + objEmp.LastName : ((objEmp.MiddleName != "") ? " " + objEmp.MiddleName : ""));
        lbl_R2_UserID.Text = objEmp.FirstName + ((objEmp.MiddleName != "") ? " " + objEmp.MiddleName : "") + ((objEmp.LastName != "") ? " " + objEmp.LastName : "");
        lbl_L_UserID.Text = objEmp.FirstName;
        lblDesgn.Text = objEmp.Designation.Designation;


        string sFileName = "" + ConfigurationManager.AppSettings["EMPPHOTO"] + objEmp.EmployeeID + "\\" + objEmp.PhotoName;
        if (System.IO.File.Exists(Server.MapPath(sFileName)))
        {
            imgPhoto.Src = "images/Employee/PHOTO/" + objEmp.EmployeeID + "/" + objEmp.PhotoName;
            imgPPhoto.Src = "images/Employee/PHOTO/" + objEmp.EmployeeID + "/" + objEmp.PhotoName;
        }
        else
        {
            imgPhoto.Src = "images/Employee/PHOTO/" + objEmp.Gender + "_NoPhoto.jpg";
            imgPPhoto.Src = "images/Employee/PHOTO/" + objEmp.Gender + "_NoPhoto.jpg";
        }

        if (objEmp.CreatedDate != null && objEmp.CreatedDate != DateTime.MinValue)
            lblStartDate.Text = objEmp.CreatedDate.ToString("MMM.") + " " + objEmp.CreatedDate.Year;

        Session["EMPID"] = objEmp.EmployeeID;
        Session["ROLEID"] = objEmp.RoleID;
        Session["BRANCHID"] = objEmp.BranchID;
        Session["EMAILID"] = (objEmp.WEmail == "") ? objEmp.HEmail : objEmp.WEmail;
        return objEmp;
    }

    public string GetPageName()
    {
        string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo info = new System.IO.FileInfo(path);
        return info.Name;
    }

    private void GetPermissions1(EmployeeBOL objEmp)
    {
        string pageName = GetPageName();

        string strCULTUREname = "" + Session["LanguageId"];

        DataSet dSet = null;
        if (Session["PERMISSIONS"] == null)
        {
            PermissionBAL objBAL = new PermissionBAL();
            dSet = objBAL.SelectAll(objEmp.Roles.RoleID, objEmp.EmployeeID, strCULTUREname);
            Session["PERMISSIONS"] = dSet;
        }
        else
        {
            dSet = (DataSet)Session["PERMISSIONS"];
            DataRow[] drLang = dSet.Tables[0].Select("LangCulturename='" + strCULTUREname + "'");
            if (drLang.Length == 0)
            {
                PermissionBAL objBAL = new PermissionBAL();
                dSet = objBAL.SelectAll(objEmp.Roles.RoleID, objEmp.EmployeeID, strCULTUREname);
                Session["PERMISSIONS"] = dSet;
            }
        }
        DataTable dt = dSet.Tables[1];
        DataTable dtMain = dSet.Tables[0];



        DataRow[] drMainList = dtMain.Select("PARENTMODULEID =0 OR MODULEID = PARENTMODULEID", "SORTORDER ASC");
        ltMenu.Text = "<ul class=\"sidebar-menu\">";
        dt.DefaultView.Sort = "SortOrder ASC";
        dtMain.DefaultView.Sort = "SortOrder ASC";
        string sMenuString = "";

        if (dt.Rows.Count > 0)
        {
            string sSubMenuString = "";

            for (int i = 0; i < drMainList.Length; i++)
            {
                DataRow drMain = drMainList[i];
                DataRow[] drSub = dt.Select("(ALLOWINSERT ='Y' OR ALLOWUPDATE='Y' OR ALLOWDELETE = 'Y' OR ALLOWVIEW ='Y') AND PARENTMODULEID=" + drMain["MODULEID"] + " and PARENTMODULEID<> ModuleID", "SortOrder ASC");
                if (drSub.Length == 0)
                {
                    DataRow[] drMSub = dt.Select("(ALLOWINSERT ='Y' OR ALLOWUPDATE='Y' OR ALLOWDELETE = 'Y' OR ALLOWVIEW ='Y') AND PARENTMODULEID=" + drMain["MODULEID"], "SortOrder ASC");
                    if (drMSub.Length == 0) continue;
                }

                bool bActive = false;

                if (drSub.Length > 0)
                {
                    sSubMenuString = "<ul class=\"treeview-menu\">";
                    foreach (DataRow dRow in drSub)
                    {
                        if (("" + dRow["PageName"]).ToLower() == "resetleavebalance.aspx") continue;
                        if (("" + dRow["PageName"]).ToLower() == pageName.ToLower())
                            bActive = true;

                        if (STYLESHEET == "LR")
                            sSubMenuString += "<li><a href=\"" + dRow["PageName"] + "\"><i class=\"fa fa-caret-right\"></i> " + dRow["ModuleName"] + "</a></li>";
                        else
                            sSubMenuString += "<li><a href=\"" + dRow["PageName"] + "\"><i class=\"fa fa-caret-left\"></i> " + dRow["ModuleName"] + "</a></li>";
                    }
                    sSubMenuString += "</ul>";
                }

                if (("" + drMain["PageName"]).ToLower() == pageName.ToLower())
                    bActive = true;

                if (bActive) //if (i == 0)
                {
                    if (i == 0)
                        sMenuString += "<li class=\"active\">";
                    else
                        sMenuString += "<li class=\"treeview active\">";
                }
                else
                {
                    sMenuString += "<li class=\"treeview\">";
                }

                if ("" + drMain["PageName"] == "")
                    sMenuString += "<a href=\"#\">";
                else
                    sMenuString += "<a href=\"" + drMain["PageName"] + "\">";
                sMenuString += "<i class=\"fa " + drMain["ImageCssName"] + "\"></i> <span>" + drMain["ModuleName"] + "</span>";

                if (drSub.Length > 0)
                    sMenuString += "<i class=\"fa fa-angle-left pull-right\"></i>";
                sMenuString += "</a>";
                sMenuString += sSubMenuString;
                sMenuString += "</li>";
                sSubMenuString = "";
            }
        }

        ltMenu.Text += sMenuString + "</ul>";
    }


    private void GetPermissions(EmployeeBOL objEmp)
    {
        string pageName = GetPageName();

        string strCULTUREname = "" + Session["LanguageId"];

        DataSet dSet = null;

        dSet = new PermissionBAL().GetPermissions(objEmp.Roles.RoleID, objEmp.EmployeeID, strCULTUREname);
        Session["PERMISSIONS"] = dSet;
        if (Session["PERMISSIONS"] == null)
        {
            PermissionBAL objBAL = new PermissionBAL();
            dSet = objBAL.GetPermissions(objEmp.Roles.RoleID, objEmp.EmployeeID, strCULTUREname);
            Session["PERMISSIONS"] = dSet;
        }
        else
        {
            dSet = (DataSet)Session["PERMISSIONS"];
            DataRow[] drLang = dSet.Tables[0].Select("LangCulturename='" + strCULTUREname + "'");
            if (drLang.Length == 0)
            {
                PermissionBAL objBAL = new PermissionBAL();
                dSet = objBAL.SelectAll(objEmp.Roles.RoleID, objEmp.EmployeeID, strCULTUREname);
                Session["PERMISSIONS"] = dSet;
            }
        }
        DataTable dt = dSet.Tables[0];
        DataTable dtMain = dSet.Tables[0];



        DataRow[] drMainList = dtMain.Select("PARENTMODULEID =0 OR MODULEID = PARENTMODULEID", "SORTORDER ASC");
        ltMenu.Text = "<ul class=\"sidebar-menu\">";
        dt.DefaultView.Sort = "SortOrder ASC";
        dtMain.DefaultView.Sort = "SortOrder ASC";
        string sMenuString = "";

        if (dt.Rows.Count > 0)
        {
            string sSubMenuString = "";

            for (int i = 0; i < drMainList.Length; i++)
            {
                DataRow drMain = drMainList[i];
                DataRow[] drSub = dt.Select("PARENTMODULEID=" + drMain["MODULEID"] + " and PARENTMODULEID<> ModuleID", "SortOrder ASC");
                if (drSub.Length == 0)
                {
                    DataRow[] drMSub = dt.Select("PARENTMODULEID=" + drMain["MODULEID"], "SortOrder ASC");
                    if (drMSub.Length == 0) continue;
                }

                bool bActive = false;

                if (drSub.Length > 0)
                {
                    sSubMenuString = "<ul class=\"treeview-menu\">";
                    foreach (DataRow dRow in drSub)
                    {
                        if (("" + dRow["PageName"]).ToLower() == "resetleavebalance.aspx") continue;
                        if (("" + dRow["PageName"]).ToLower() == pageName.ToLower())
                            bActive = true;

                        if (STYLESHEET == "LR")
                            sSubMenuString += "<li><a href=\"" + dRow["PageName"] + "\"><i class=\"fa fa-caret-right\"></i> " + dRow["ModuleName"] + "</a></li>";
                        else
                            sSubMenuString += "<li><a href=\"" + dRow["PageName"] + "\"><i class=\"fa fa-caret-left\"></i> " + dRow["ModuleName"] + "</a></li>";
                    }
                    sSubMenuString += "</ul>";
                }

                if (("" + drMain["PageName"]).ToLower() == pageName.ToLower())
                    bActive = true;

                if (bActive) //if (i == 0)
                {
                    if (i == 0)
                        sMenuString += "<li class=\"active\">";
                    else
                        sMenuString += "<li class=\"treeview active\">";
                }
                else
                {
                    sMenuString += "<li class=\"treeview\">";
                }

                if ("" + drMain["PageName"] == "")
                    sMenuString += "<a href=\"#\">";
                else
                {
                    if (("" + drMain["PageName"]).ToLower() == "dashboard.aspx")
                    {
                        //string baseUrl = Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
                        sMenuString += "<a  id=\"homelink\" >";
                    }
                    else
                        sMenuString += "<a href=\"" + drMain["PageName"] + "\">";
                }
                sMenuString += "<i class=\"fa " + drMain["ImageCssName"] + "\"></i> <span>" + drMain["ModuleName"] + "</span>";

                if (drSub.Length > 0)
                    sMenuString += "<i class=\"fa fa-angle-left pull-right\"></i>";
                sMenuString += "</a>";
                sMenuString += sSubMenuString;
                sMenuString += "</li>";
                sSubMenuString = "";
            }
        }

        ltMenu.Text += sMenuString + "</ul>";
    }

    protected void rptrMsg_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgAvatar = (Image)e.Item.FindControl("imgAvatar");
            Label lblImgName = (Label)e.Item.FindControl("lblImgName");
            Label lblSentBy = (Label)e.Item.FindControl("lblSentBy");
            Label lblSentDate = (Label)e.Item.FindControl("lblSentDate");

            string sFileName = "" + ConfigurationManager.AppSettings["EMPPHOTO"] + lblSentBy.Text + "\\" + lblImgName.Text;
            if (System.IO.File.Exists(Server.MapPath(sFileName)))
            {
                imgAvatar.ImageUrl = "images/Employee/PHOTO/" + lblSentBy.Text + "/" + lblImgName.Text;
            }
            else
            {
                imgAvatar.ImageUrl = "images/Employee/PHOTO/F_NoPhoto.jpg";
            }

            Literal ltTime = (Literal)e.Item.FindControl("ltTime");

            DateTime start = DateTime.Now;
            DateTime sentdate = DateTime.Parse(lblSentDate.Text);
            TimeSpan timeDiff = DateTime.Now - sentdate;
            double dDays = timeDiff.TotalDays;
            double dHr = timeDiff.TotalHours;
            double dMn = timeDiff.TotalMinutes;

            if (sentdate.Day == DateTime.Today.Day && sentdate.Month == DateTime.Today.Month && sentdate.Year == DateTime.Today.Year)
                ltTime.Text = "Today";
            else if (sentdate.Day == DateTime.Today.Day - 1 && dHr <= 24)
                ltTime.Text = "Yesterday";
            else if (dMn > 0 && dHr == 0)
                ltTime.Text = Math.Round(dMn).ToString() + ((Math.Round(dMn) == 1) ? " min" : " mins");
            else if (dHr > 0 && dDays == 0)
                ltTime.Text = Math.Round(dHr).ToString() + ((Math.Round(dHr) == 1) ? " hour" : " hours");
            else if (dDays > 0)
                ltTime.Text = Math.Round(dDays).ToString() + ((Math.Round(dDays) == 1) ? " day" : " days");

            Literal ltMsg = (Literal)e.Item.FindControl("ltMsg");


            string sMsg = "<b>" + DataBinder.Eval(e.Item.DataItem, "mailsubject") + " : </b>";
            sMsg += "" + DataBinder.Eval(e.Item.DataItem, "mailmsg");
            sMsg = (sMsg.Length > 40) ? sMsg.Substring(0, 40) : sMsg;
            ltMsg.Text = sMsg;

        }
    }
    protected void rptrAlerts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltAlertMsg = (Literal)e.Item.FindControl("ltAlertMsg");
            ltAlertMsg.Text = string.Format(hrmlang.GetString("youhavenotifications"), Util.ToInt(ViewState["ALERTCOUNT"]));
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            LinkButton lnkViewAlerts = (LinkButton)e.Item.FindControl("lnkViewAlerts");
            lnkViewAlerts.Text = hrmlang.GetString("viewall");
        }
    }
    protected void rptrAlerts_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "VIEWALERTS")
        {
            ScriptManager.RegisterStartupScript(rptrAlerts, rptrAlerts.GetType(), "onclick", " $('#dvAlerts').modal();", true);
        }
    }
    protected void gvAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            new AlertsBAL().Delete(Util.ToInt(e.CommandArgument));
            GetAlerts();
            ScriptManager.RegisterStartupScript(gvAlerts, gvAlerts.GetType(), "onclick", "alert('" + hrmlang.GetString("datadeleted") + "'); $('#dvAlerts').modal();", true);
        }
    }
    protected void gvAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Cells[0].Text = hrmlang.GetString("description");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
        }
    }
}
