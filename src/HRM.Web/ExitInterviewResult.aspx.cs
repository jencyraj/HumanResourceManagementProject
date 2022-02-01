using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BOL;
using HRM.BAL;
public partial class ExitInterviewResult : System.Web.UI.Page
{
    public int Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblId.Text = "" + Request.QueryString["id"];
        lblexit.Text = "" + Request.QueryString["exittype"];
        if (!IsPostBack)
        {

        //    ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "ExitInterviewResult.aspx");

            if (lblId.Text != "")
            {
                Mid = int.Parse(lblId.Text);
                getassetDetail(Mid);
                GetExitInterviews();
                GetExitInterviews_status(Util.ToInt(lblId.Text));
            }

            term.Visible = term1.Visible = (lblexit.Text == "term") ? true : false;
            reg.Visible = reg1.Visible = (lblexit.Text == "reg") ? true : false;
          
        }
    }
    private void GetExitInterviews_status(int ID)
    {
        DataSet dSet = new ExitInterviewBAL().GetStatus(ID);
        DataTable DTRes = dSet.Tables[0];
        DataTable DTTer = dSet.Tables[1];
        if (lblexit.Text == "reg")
        {
            btnsave.Visible = btnclose.Visible = (DTRes.Rows.Count == 0) ? true : false;
        }
        else
        {
            btnsave.Visible = btnclose.Visible = (DTTer.Rows.Count == 0) ? true : false;
        }
       

    }
    private void GetExitInterviews()
    {
        DataSet dSet = new ExitInterviewBAL().GetInterviews(Util.ToInt(Session["EMPID"]));
       DataTable DTRes = dSet.Tables[0];
       DataTable DTTer = dSet.Tables[1];
       if (lblexit.Text == "term")
       {
           if (DTTer.Rows.Count > 0)
           {
               
               lblemp.Text = DTTer.Rows[0]["FirstName"].ToString();
               lblforwardto.Text = DTTer.Rows[0]["Iname"].ToString();
               lblIdate.Text = DTTer.Rows[0]["InterviewDate"].ToString();
               lblapp.Text = DTTer.Rows[0]["App"].ToString();


           }
       }
       else
       {

           if (DTRes.Rows.Count > 0)
           {
               
               lblemp1.Text = DTRes.Rows[0]["FirstName"].ToString();
                lblforwardto1.Text = DTRes.Rows[0]["NoticeDate"].ToString();
               lblIdate1.Text = DTRes.Rows[0]["App"].ToString();
               lblapp1.Text = DTRes.Rows[0]["ResgnDate"].ToString();
           }
       }
      
    }
    private void CHEKchekbox()
    {
        foreach (GridViewRow row in gvasset.Rows)
        {
            HiddenField hfAssetId = (HiddenField)row.FindControl("hfAssetId");
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk.Checked)
            {
                AssetsBAL objBAL = new AssetsBAL();
                AssetsBOL objBol = new AssetsBOL();

                objBAL.UpdateassetonEmployee(Util.ToInt(hfAssetId.Value));

                Bindgvterm();
              
            }
        }
    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "APPROVE")
        {
            try
            {

                foreach (GridViewRow row in gvasset.Rows)
                {
                    HiddenField hfAssetId = (HiddenField)row.FindControl("hfAssetId");
                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        AssetsBAL objBAL = new AssetsBAL();
                        AssetsBOL objBol = new AssetsBOL();

                        objBAL.UpdateassetonEmployee(Util.ToInt(hfAssetId.Value));

                        Bindgvterm();
                        lblMsg.Text = hrmlang.GetString("successapprvasset");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = hrmlang.GetString("assetapprvfail");
            }
          
        }
        else if (e.CommandName == "Close")
        {
            try
            {


                CHEKchekbox();
                    
                if (lblexit.Text == "term")
                {
                    TerminationBAL objBAL = new TerminationBAL();
                    TerminationBOL objBol = new TerminationBOL();
                    objBol.TID = Util.ToInt(lblId.Text);
                    objBol.InterviewRemarks = txtremark.Text;
                    objBAL.Update_Interview_Closed(objBol);
                }
                else if (lblexit.Text == "reg")
                {
                    ResignationBAL objBAL1 = new ResignationBAL();
                    ResignationBOL objBol1 = new ResignationBOL();
                    objBol1.ResgnID = Util.ToInt(lblId.Text);
                    objBol1.InterviewRemarks = txtremark.Text;
                    objBAL1.Update_Interview_Closed(objBol1);
                }
                lblMsg.Text = hrmlang.GetString("successclosasset");
            }
            catch (Exception ex)
            {
                lblErr.Text = hrmlang.GetString("assetapprvfail");
            }
        }
        else if (e.CommandName == "Cancel")
        {
            Response.Redirect("ExitInterview.aspx");
        }

    }
    private void Bindgvterm()
    {
        AssetsBOL objBOL1 = new AssetsBOL();
        objBOL1.EmployeeID = int.Parse(lblEid.Text);
        DataTable dTable = new AssetsBAL().SelectAsset(objBOL1.EmployeeID);


        if (dTable.Rows.Count > 0)
        {
            gvasset.DataSource = dTable;
            gvasset.DataBind();


        }
    }
    private void getassetDetail(int ID)
    {
        if (lblexit.Text == "term")
        {
            TerminationBOL objBOL = new TerminationBOL();
            objBOL.TID = ID;
            DataTable dTmable = new TerminationBAL().SelectAll(objBOL);
            if (dTmable.Rows.Count > 0)
            {
                lblEid.Text = dTmable.Rows[0]["EmployeeID"].ToString();
            }

          
        }
        else if (lblexit.Text == "reg")
        {
            ResignationBOL objBOL = new ResignationBOL();
            objBOL.ResgnID = ID;
            DataTable dTmable = new ResignationBAL().Resignation_SelectById(objBOL);
            if (dTmable.Rows.Count > 0)
            {
                lblEid.Text = dTmable.Rows[0]["EmployeeID"].ToString();
            }

        }

        AssetsBOL objBOL1 = new AssetsBOL();
        objBOL1.AssetID = int.Parse(lblEid.Text);
        DataTable dTable = new AssetsBAL().SelectAsset(objBOL1.AssetID);


        if (dTable.Rows.Count > 0)
        {
            gvasset.DataSource = dTable;
            gvasset.DataBind();


        }
    }

}