using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class JobCandidates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "jobcandidates.aspx");
            GetJobTitles();
            Getstatus();
            GetJobCandidates();
          
        }
      
        
    }

    private void GetJobTitles()
    {
        JobTitleBAL objBAL = new JobTitleBAL();
        DataTable dt = objBAL.SelectAll();
        ddlJobTitle.DataSource = dt;
        ddlJobTitle.DataBind();
        ddlJobTitle.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }


    public void Getstatus()
    {

        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("new"), "NEW"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rwprogress"), "RWP"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("rwcompleted"), "RWC"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("slforinterview"), "SHI"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("ivsheduled"), "IVW"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("selected"), "SEL"));
        ddlStatus.Items.Add(new ListItem(hrmlang.GetString("joined"), "JND"));
        ddlStatus.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));

    }

    private void GetJobCandidates()
    {
        
        JobTitleBAL objBAL = new JobTitleBAL();

        int jobtitleid = Util.ToInt(ddlJobTitle.SelectedValue);
        String status = (ddlStatus.SelectedValue);
        gvCandidates.DataSource = objBAL.SelectJobCandidatesByJID(jobtitleid, status);
        gvCandidates.DataBind();

        if (gvCandidates.Rows.Count == 0)
        {
            lblErr.Visible = true;
            lblErr.Text = hrmlang.GetString("nodatafound");
        }
        else
        {
            lblErr.Visible = false;
        }
    }




  
   

    protected void gvCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCandidates.PageIndex = e.NewPageIndex;
        GetJobCandidates();
    }

   


    protected void gvCandidates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            JobTitleBAL objBAL = new JobTitleBAL();
            objBAL.DeleteCandidateprofile(Util.ToInt(e.CommandArgument));
          
          //  ClientScript.RegisterStartupScript(gvCandidates.GetType(), "onclick", "alert('" + hrmlang.GetString("trainingdeleted") + "');", true);
            GetJobCandidates();
        }

    }
    
    protected void gvCandidates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("cname");
            e.Row.Cells[1].Text = hrmlang.GetString("email");
            e.Row.Cells[2].Text = hrmlang.GetString("jobtitle");
            e.Row.Cells[3].Text = hrmlang.GetString("applieddate");
            e.Row.Cells[4].Text = hrmlang.GetString("status");
          
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvCandidates.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvCandidates.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvCandidates.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvCandidates.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
    }

   

    protected void btnSearchStatus_Click(object sender, EventArgs e)
    {
        GetJobCandidates();
        GetJobCandidates();

    }

    protected string GetUrl(object id)
    {
        return "candidateprofile.aspx?Candidate_id=" + id;
    }

    public string Statusdef(string status)
    {
        if (status == "NEW")
        {
           status= hrmlang.GetString("new");
        }
        else if (status == "RWP")
        {
            status = hrmlang.GetString("rwprogress");
        }
        else if (status == "RWC")
        {
            status = hrmlang.GetString("rwcompleted");
        }
        else if (status == "SHI")
        {
            status = hrmlang.GetString("slforinterview");
        }
        else if (status == "IVW")
        {
            status = hrmlang.GetString("ivsheduled");
        }
        else if (status == "SEL")
        {
            status = hrmlang.GetString("selected");
        }
        else if (status == "JND")
        {
            status = hrmlang.GetString("joined");
        }

        return status;
      
    }

}