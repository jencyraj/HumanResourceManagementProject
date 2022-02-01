using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;
using System.Data;

public partial class AttendanceRule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");


        if (!IsPostBack)
        {
            //rdbitems.Items[0].Text = hrmlang.GetString("useactualvalue");
            //rdbitems.Items[1].Text = hrmlang.GetString("userule");
            ddlrule.Items[0].Text = hrmlang.GetString("useactualvalue");
            ddlrule.Items[1].Text = hrmlang.GetString("userule");

            getActive_Rule();

        }
    }

    private void getActive_Rule()
    {
        DataTable DTRule = new AttendanceBAL().Get_Active_AttendanceRule();
        if (DTRule.Rows.Count > 0)
        {
            lblID.Text = DTRule.Rows[0]["RuleID"].ToString();
            string Chk = DTRule.Rows[0]["UseValue"].ToString();

            if (Chk == "Y")
                ddlrule.Items[0].Selected = true;
            else
                ddlrule.Items[0].Selected = false;

            string ChkR = DTRule.Rows[0]["UseRule"].ToString();

            if (ChkR == "Y")
                ddlrule.Items[1].Selected = true;
            else
                ddlrule.Items[1].Selected = false;

         //   pnlRule.Enabled = rbtnRule.Checked; // rdbitems.Items[1].Selected;
            pnlRule.Enabled = ddlrule.Items[1].Selected;

            string ChkA = DTRule.Rows[0]["Active"].ToString();
            if (ChkA == "Y")
                chkActive.Checked = true;
            txtZeroto.Text = DTRule.Rows[0]["ZeroAttendanceTo"].ToString();
            txtZerofrom.Text = DTRule.Rows[0]["ZeroAttendanceFrom"].ToString();
            txtfullfrom.Text = DTRule.Rows[0]["FullAttendanceFrom"].ToString();
            txtfullto.Text = DTRule.Rows[0]["FullAttendanceTo"].ToString();
            txthaffrom.Text = DTRule.Rows[0]["HalfAttendanceFrom"].ToString();
            txthafto.Text = DTRule.Rows[0]["HalfAttendanceTo"].ToString();
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        getActive_Rule();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AttendanceRuleBOL objBOL = new AttendanceRuleBOL();
            objBOL.RuleID = Util.ToInt(lblID.Text);

           string Value = ddlrule.SelectedItem.Value;
            if (Value == "")
                lblErr.Text = "Please select any rule..";
            else
            {
                objBOL.UseValue = ddlrule.Items[0].Selected ? "Y" : "N";
                objBOL.UseRule = ddlrule.Items[1].Selected ? "Y" : "N"; 
                objBOL.Active = chkActive.Checked ? "Y" : "N";
                objBOL.ZeroAttendanceFrom = Util.ToDecimal(txtZerofrom.Text);
                objBOL.ZeroAttendanceTo = Util.ToDecimal(txtZeroto.Text);
                objBOL.HalfAttendanceFrom = Util.ToDecimal(txthaffrom.Text);
                objBOL.HalfAttendanceTo = Util.ToDecimal(txthafto.Text);
                objBOL.FullAttendanceFrom = Util.ToDecimal(txtfullfrom.Text);
                objBOL.FullAttendanceTo = Util.ToDecimal(txtfullto.Text);
                objBOL.CreatedBy = User.Identity.Name;
                objBOL.ModifiedBy = User.Identity.Name;
          
                AttendanceBAL objBAL = new AttendanceBAL();
                objBAL.Save_Attendance_Rule(objBOL);
                lblMsg.Text = hrmlang.GetString("attendancerulesaved");
            }
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

   
    protected void ddlrule_SelectedIndexChanged(object sender, EventArgs e)
    {
       pnlRule.Visible=ddlrule.Items[1].Selected;
    }
}