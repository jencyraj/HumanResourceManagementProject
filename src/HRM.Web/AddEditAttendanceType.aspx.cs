using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;

public partial class AddEditAttendanceType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtAttendanceType.Attributes.Add("placeholder", hrmlang.GetString("enterattendancetype"));
        txtATCode.Attributes.Add("placeholder", hrmlang.GetString("enterattendancecode"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            ddCategory.Items.Clear();
           ddCategory.Items.Add(new ListItem (hrmlang.GetString("attendance"),"A"));
           ddCategory.Items.Add(new ListItem (hrmlang.GetString("leave"),"L"));
           ddCategory.Items.Add(new ListItem (hrmlang.GetString("holiday"),"H"));
           ddCategory.Items[0].Selected = true;

           ddTypeKind.Items.Clear();
           ddTypeKind.Items.Add(new ListItem(hrmlang.GetString("day"), "D"));
           ddTypeKind.Items.Add(new ListItem(hrmlang.GetString("hour"), "H"));
           ddTypeKind.Items[0].Selected = true;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int nATId = Util.ToInt(Request.QueryString["id"]);
                LoadDetails(nATId);
            }
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SAVE")
        {
            try
            {
                SaveAttendanceType();
                lblMsg.Text = hrmlang.GetString("attentencesaved");
            }
            catch
            {
                lblErr.Text = "Some error occured. Attendance Type not saved";
            }
        }
        if (e.CommandName == "CANCEL")
        {
            Response.Redirect("AttendanceType.aspx");
        }

      
    }

    private void LoadDetails(int nATId)
    {
        AttendanceTypeBOL objBOL = new AttendanceTypeBOL();
        objBOL = new AttendanceTypeBAL().SearchById(nATId);
        txtAttendanceType.Text = objBOL.AttendanceType;
        ddCategory.SelectedValue = objBOL.Category;
        ddTypeKind.SelectedValue = objBOL.TypeKind;
        txtATCode.Text = objBOL.ATCode;
    }

    private void SaveAttendanceType()
    {
        int nATId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            nATId = Util.ToInt(Request.QueryString["id"]);
        }
        AttendanceTypeBOL objBOL = new AttendanceTypeBOL();
        objBOL.ATId = nATId;
        objBOL.AttendanceType = txtAttendanceType.Text.Trim();
        objBOL.Category = ddCategory.SelectedValue;
        objBOL.TypeKind = ddTypeKind.SelectedValue;
        objBOL.ATCode = txtATCode.Text;
        objBOL.Status = "Y";
        objBOL.CreatedBy = User.Identity.Name;
        objBOL.ModifiedBy = User.Identity.Name;
        AttendanceTypeBAL objBAL = new AttendanceTypeBAL();
        objBAL.Save(objBOL);
    }

}