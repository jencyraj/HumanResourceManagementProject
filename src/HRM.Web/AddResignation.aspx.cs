using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BOL;
using HRM.BAL;

public partial class AddResignation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        //dvMsg.Visible = false;

        if (!IsPostBack)
        {
            lblResignationID.Text = "" + Request.QueryString["id"];
            btnSave.Text = hrmlang.GetString("save");
            btnCancel.Text = hrmlang.GetString("cancel");
            txtDesc.Attributes.Add("placeholder", hrmlang.GetString("enterdescription"));
            txtadditionalinfo.Attributes.Add("placeholder", hrmlang.GetString("additionalinformation")); 
            GetDetails();
            
        }
    }
    private void GetDetails()
    {
        int Resgnid = Util.ToInt(Request.QueryString["id"]);
        lblResignationID.Text = Request.QueryString["id"];
        if (Resgnid == 0) return;
      
        ResignationBAL objBAL = new ResignationBAL();
        ResignationBOL objCy = new ResignationBOL();
        if(Resgnid>0)
        {
        objCy.ResgnID = Resgnid;
        objCy = objBAL.SelectByID(Resgnid);
        Dtnotice.SelectedCalendareDate = objCy.NoticeDate;
        Dtresignation.SelectedCalendareDate = objCy.ResgnDate;
        txtDesc.Text = objCy.Reason;
        txtadditionalinfo.Text = objCy.AdditionalInfo;
       
        
           
        }
        else
        {
            lblResignationID.Text = "";
            ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('No data exists');", true);
            Clear();
        }
        

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ResignationBAL objBAL = new ResignationBAL();
            ResignationBOL objCy = new ResignationBOL();
            
            objCy.ResgnID = Util.ToInt(lblResignationID.Text);
            try
            {
                string[] NoticeDateArr = Dtnotice.getGregorianDateText.ToString().Split('/');
                objCy.NoticeDate = DateTime.Parse(NoticeDateArr[1] + "/" + NoticeDateArr[0] + "/" + NoticeDateArr[2]);
                if (objCy.NoticeDate < DateTime.Now.Date)
                {
                    ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('Please select a Valid Date');", true);// changed on 30 sept 2016 By Jency Raj
                    return;
               
                }
            }
            catch
            {
            }
            try
            {
                string[] Dtto1 = Dtresignation.getGregorianDateText.ToString().Split('/');
                objCy.ResgnDate = DateTime.Parse(Dtto1[1] + "/" + Dtto1[0] + "/" + Dtto1[2]);
                if (objCy.ResgnDate < DateTime.Now.Date || objCy.ResgnDate == DateTime.Now.Date)
                {
                    ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('Please select a Valid Date');", true);// changed on 30 sept 2016 By Jency Raj
                   
                    return;
                }

            }
            catch
            {
            }
           // return;
            objCy.Reason = txtDesc.Text;
            objCy.AdditionalInfo = txtadditionalinfo.Text.Trim();
            objCy.EmployeeID = Util.ToInt(Session["EMPID"]);
            objCy.Status = "Y";
            objCy.CreatedBy = Util.ToInt(Session["EMPID"]); 
            objBAL.Save(objCy);

            lblMsg.Text = hrmlang.GetString("Resignationsaved");

            Response.Redirect("Resignation.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblResignationID.Text = "";
        Dtnotice.ClearDate();// = DateTime.Today;
        Dtresignation.ClearDate();// = DateTime.Today;
        txtDesc.Text = "";
        txtadditionalinfo.Text = "";
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if ("" + Request.QueryString["id"] == "")
        {
            Clear();
        }
        else
        {
            Response.Redirect("Resignation.aspx");
        }

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddResignation.aspx");
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        try
        {
            ResignationBAL objBAL = new ResignationBAL();
            ResignationBOL objCy = new ResignationBOL();

          //  objCy.ResgnID = Util.ToInt(lblResignationID.Text);
            try
            {
                string[] NoticeDateArr = Dtnotice.getGregorianDateText.ToString().Split('/');
                objCy.NoticeDate = DateTime.Parse(NoticeDateArr[1] + "/" + NoticeDateArr[0] + "/" + NoticeDateArr[2]);
                if (objCy.NoticeDate < DateTime.Now.Date)
                {
                    ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('Please select a Valid Date');", true);// changed on 30 sept 2016 By Jency Raj
                    return;
                }
            }
            catch
            {
            }
            try
            {
                string[] Dtto1 = Dtresignation.getGregorianDateText.ToString().Split('/');
                objCy.ResgnDate = DateTime.Parse(Dtto1[1] + "/" + Dtto1[0] + "/" + Dtto1[2]);
                if (objCy.ResgnDate < DateTime.Now.Date || objCy.ResgnDate == DateTime.Now.Date)
                {
                    ClientScript.RegisterStartupScript(btnSave.GetType(), "ONCLICK", "alert('Please select a Valid Date');", true);// changed on 30 sept 2016 By Jency Raj
                    return;
                }
            }
            catch
            {
            }
            objCy.Reason = txtDesc.Text;
            objCy.AdditionalInfo = txtadditionalinfo.Text;
            objCy.EmployeeID = Util.ToInt(Session["EMPID"]);
            objCy.Status = "P";
            objCy.CreatedBy = int.Parse(User.Identity.Name);
            objBAL.Save(objCy);

            lblMsg.Text = hrmlang.GetString("Resignationsaved");

            Response.Redirect("Resignation.aspx");
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }
}