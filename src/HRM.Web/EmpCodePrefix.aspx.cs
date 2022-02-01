using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HRM.BAL;

public partial class EmpCodePrefix : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmployeeBAL objBAL = new EmployeeBAL();
            DataTable dT = objBAL.SelectEmpCodeSettings();
            txtEmpCodePrefix.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecodeprefix"));
            txtEmpCodeCtrPrefix.Attributes.Add("placeholder", hrmlang.GetString("enteremployeecodecounterprefix"));
            txtEmpCodeCtrStart.Attributes.Add("placeholder", hrmlang.GetString("entercounterstartsfrom"));
            txtEmpCodeTotalLength.Attributes.Add("placeholder", hrmlang.GetString("entertotallengthofemployeecode"));
            btnSave.Text = hrmlang.GetString("save");
            if (dT.Rows.Count > 0)
            {
                txtEmpCodeCtrPrefix.Text = "" + dT.Rows[0]["EmpCodeCtrPrefix"];
                txtEmpCodeCtrStart.Text = "" + dT.Rows[0]["EmpCodeCtrStart"];
                txtEmpCodePrefix.Text = "" + dT.Rows[0]["EmpCodePrefix"];
                txtEmpCodeTotalLength.Text = "" + dT.Rows[0]["EmpCodeTotalLength"];
            }

            lblEx.Text = hrmlang.GetString("example") + objBAL.GetNextEmployeeCode();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmployeeBAL objBAL = new EmployeeBAL();
        objBAL.UpdateEmpCodeSettings(txtEmpCodePrefix.Text, txtEmpCodeCtrPrefix.Text, Util.ToInt(txtEmpCodeCtrStart.Text), Util.ToInt(txtEmpCodeTotalLength.Text), User.Identity.Name);
        lblMsg.Text = hrmlang.GetString("datasaved");
        lblEx.Text = hrmlang.GetString("example") + objBAL.GetNextEmployeeCode();
    }
}