using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BOL;
using HRM.BAL;

public partial class overtimeview : System.Web.UI.Page
{
   static int rid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            onload();
           
        }
     
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        OverTimeBAL objBAl = new OverTimeBAL();
        OverTimeBOL objBOL = new OverTimeBOL();
        objBOL.RId = rid;
        objBOL.MinimumHr =Util.ToDecimal( txtminhr.Text);
        objBOL.ApplicableSum = Util.ToDateTime(ctlCalDepDob.getGregorianDateText);
        if (check.Checked)
        {
            objBOL.Ruleapplicable = "Y";

        }
        else
        {
            objBOL.Ruleapplicable = "N";
        }
        if (objBOL.RId > 0)
        {
            objBOL.ModifiedBy = "" + Session["USERID"];
            
        }
        else
        {
            objBOL.CreatedBy = "" + Session["USERID"];
            
        
        }

        objBAl.Save(objBOL);
        lblMsg.Text = hrmlang.GetString("overtimerulesaved");

    }
    public void onload()
    {
        OverTimeBAL objBal = new OverTimeBAL();
        OverTimeBOL objbol = new OverTimeBOL();
        DataTable dt = new DataTable();
        dt = objBal.selectall(objbol);
        if (dt.Rows.Count > 0)
        {
            string y = dt.Rows[0]["Status"].ToString(); ;
            rid = Util.ToInt(dt.Rows[0]["RId"].ToString());
            txtminhr.Text = dt.Rows[0]["MinimumHr"].ToString();
            txtdate.Text = dt.Rows[0]["ApplicableSum"].ToString();
            check.Checked = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        OverTimeBOL objbol = new OverTimeBOL();
        txtdate.Text = "";
        txtminhr.Text = "";
        check.Checked = false;
     

    }
}