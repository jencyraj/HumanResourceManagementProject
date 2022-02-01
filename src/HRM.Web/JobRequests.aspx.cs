using HRM.BAL;
using HRM.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JobRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "jobrequests.aspx");
            GetJobTitles();

        }


        string[] permissions = (string[])ViewState["permissions"];
        pnlNew.Visible = (permissions[0] == "Y") ? true : false;

        btnNew.Text = hrmlang.GetString("newjobrequest");
    }

    private void GetJobTitles()
    {
        JobTitleBAL objBAL = new JobTitleBAL();
        gvDepts.DataSource = objBAL.SelectAll();
        gvDepts.DataBind();
    }

    protected void gvDepts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DEL"))
        {
            JobTitleBAL objBAL = new JobTitleBAL();
            objBAL.Delete(Util.ToInt(e.CommandArgument));
            lblMsg.Text = hrmlang.GetString("datadeleted");
            GetJobTitles();
        }

        if (e.CommandName.Equals("PUBLISH"))
        {
            JobTitleBOL objBol = new JobTitleBOL();
            JobTitleBAL objBAL = new JobTitleBAL();

            objBol.JID = Util.ToInt(e.CommandArgument);
            objBol.Published = "Y";
            objBol.PublishedBy = "";

            objBAL.UpdatePublishedType(objBol);
            lblMsg.Text = hrmlang.GetString("jobpublished");
            GetJobTitles();
        }
        if (e.CommandName.Equals("UNPUBLISH"))
        {
            JobTitleBOL objBol = new JobTitleBOL();
            JobTitleBAL objBAL = new JobTitleBAL();

            objBol.JID = Util.ToInt(e.CommandArgument);
            objBol.Published = "N";
            objBol.PublishedBy = "";

            objBAL.UpdatePublishedType(objBol);
            lblMsg.Text = hrmlang.GetString("jobunpublished");
            GetJobTitles();
        }

    }

    protected void gvDepts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepts.PageIndex = e.NewPageIndex;
        GetJobTitles();
    }

    protected void gvDepts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("jobtitle");
            e.Row.Cells[1].Text = hrmlang.GetString("numberofpositions");
            e.Row.Cells[2].Text = hrmlang.GetString("jobpostdescription");
            e.Row.Cells[3].Text = hrmlang.GetString("closingdate");
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvDepts.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvDepts.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvDepts.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvDepts.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["permissions"] != null)
            {
                string publishedstatus = DataBinder.Eval(e.Row.DataItem, "Published").ToString();

                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnkPublish = (LinkButton)e.Row.FindControl("lnkPublish");
                LinkButton lnkUnPublish = (LinkButton)e.Row.FindControl("lnkUnPublish");

                string[] permissions = (string[])ViewState["permissions"];

                lnkEdit.Visible = (permissions[1] == "Y") ? true : false;
                lnkDelete.Visible = (permissions[2] == "Y") ? true : false;
                lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
                lnkEdit.Attributes.Add("title", hrmlang.GetString("publish"));
                lnkDelete.Attributes.Add("title", hrmlang.GetString("unpublish"));
                lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("deletequestion"));
                lnkPublish.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("publishquestion"));
                lnkUnPublish.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("unpublishquestion"));

                if (publishedstatus == "Y")
                {
                    lnkPublish.Visible = false;
                    lnkUnPublish.Visible = true;
                }
                else
                {
                    lnkPublish.Visible = true;
                    lnkUnPublish.Visible = false;
                }
            }

            string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
            decodedText = decodedText.Replace("<br>", "");
            decodedText = (decodedText.Length > 30) ? decodedText.Substring(0, 30) + "..." : decodedText;
            e.Row.Cells[2].Text = decodedText;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JobTitle.aspx");
    }
}