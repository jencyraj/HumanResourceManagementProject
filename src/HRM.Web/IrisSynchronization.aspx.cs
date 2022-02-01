using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BAL;
using HRM.BOL;
using System.Data;
public partial class IrisSynchronization : System.Web.UI.Page
{
    DataTable dtDestination;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btncompare_Click(object sender, EventArgs e)
    {
        int Result;
        Delete_Device(txtmaster.Text, "" + Session["USERID"]);
        Delete_Device(txtdest.Text, "" + Session["USERID"]);
        DataSet DS_Table = new IrisSynchronizationBAL().GetCredentials(txtmaster.Text.Trim(), txtdest.Text.Trim(), out Result);

        if (Result == 0)
        {
            DataTable DT_Master = DS_Table.Tables[0];
            DataTable DT_Dest   = DS_Table.Tables[1];
            DataTable dtMaster = GetDevice_MasterDT(DT_Master);
            Insert_UserSynch(dtMaster,"M");
            gv_Synch.DataSource = dtMaster;
            gv_Synch.DataBind();
            Pnlsynch.Visible    = (dtMaster.Rows.Count > 0) ? true : false;
            dtDestination = GetDevice_DestDT(DT_Dest);
            Insert_UserSynch(dtDestination, "D");
            
        }
        else
        {
            if (Result == 1)
                lblErr.Text = txtmaster.Text + "" + hrmlang.GetString("masternotindevice");
            if (Result == 2)
                lblErr.Text = txtmaster.Text + "" + hrmlang.GetString("masternotinmaster");
            if (Result == 3)
                lblErr.Text = txtdest.Text + "" + hrmlang.GetString("masternotindevice");

        }


    }
    private DataTable GetDevice_MasterDT(DataTable DT)
    {
        DataTable dt_Table = null;
        if(DT.Rows.Count>0)
        {
        lblIp.Text = DT.Rows[0]["IPAddress"].ToString();
        lblSecurityId.Text = DT.Rows[0]["SecurityId"].ToString();
        lbluname.Text  = DT.Rows[0]["UserName"].ToString();
        lblpwd.Text   = DT.Rows[0]["Password"].ToString();
        IrisSynchronizationBAL objBAL = new IrisSynchronizationBAL();
        dt_Table = objBAL.GetDevice_DT(lblIp.Text, lblSecurityId.Text, lbluname.Text,lblpwd.Text);
        }
        return dt_Table;
    }
    private DataTable GetDevice_DestDT(DataTable DT)
    {
        DataTable dt_Table = null;
        if (DT.Rows.Count > 0)
        {
            lblDIp.Text = DT.Rows[0]["IPAddress"].ToString();
            lblDSecurityId.Text = DT.Rows[0]["SecurityId"].ToString();
            lblDuname.Text = DT.Rows[0]["UserName"].ToString();
            lblDpwd.Text = DT.Rows[0]["Password"].ToString();
            IrisSynchronizationBAL objBAL = new IrisSynchronizationBAL();
            dt_Table = objBAL.GetDevice_DT(lblDIp.Text, lblDSecurityId.Text, lblDuname.Text, lblDpwd.Text);
        }
        return dt_Table;
    }
    private void Delete_Device(string Device, string User)
    {
        IrisSynchronizationBAL objBAL = new IrisSynchronizationBAL();
        objBAL.DeleteIrisUser_Synch(Device, Util.ToInt(User));
    }
  

    private void Insert_UserSynch(DataTable DT,string Device)
    {
        IrisSynchronizationBOL objBOL = new IrisSynchronizationBOL();
        IrisSynchronizationBAL objBAL = new IrisSynchronizationBAL();
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            objBOL.UserId = Util.ToInt(DT.Rows[i]["UserID"]);
            objBOL.First = DT.Rows[i]["FirstName"].ToString();
            objBOL.Last = DT.Rows[i]["LastName"].ToString();
            objBOL.PIN = Util.ToInt(DT.Rows[i]["PIN"]);
            //Image TimeGroupIDList = DT.Rows[i]["TimeGroupIDList"];
            objBOL.Userkind = Util.ToInt(DT.Rows[i]["EUserKind"]);
            objBOL.Eye = Util.ToInt(DT.Rows[i]["EEyeType"]);
            objBOL.Start = DateTime.Parse(DT.Rows[i]["StartDate"].ToString());
            objBOL.End = DateTime.Parse(DT.Rows[i]["ExpireDate"].ToString());
            objBOL.CreatedBy = "" + Session["USERID"];
            if (Device == "M")
            {
                objBOL.DeviceIP = txtmaster.Text;

            }
            else
            {
                objBOL.DeviceIP = txtdest.Text;
            }
            objBAL.Iris_User_SynchSave(objBOL);
        }
    }
    
  
    private DataTable Get_table()
    {
        DataTable dt=new DataTable();
        dt.Columns.Add("UserID");
        dt.Rows.Add();
        return dt;

    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {     
            DataTable DT_User = Get_table();
            DataRow drow = null; string ErrMsg;
            IrisSynchronizationBAL objBAL = new IrisSynchronizationBAL();
            drow = DT_User.NewRow();
        if (e.CommandName == "Synch")
        {
            
            if (gv_Synch.Rows.Count > 0)
            {
                foreach (GridViewRow rows in gv_Synch.Rows)
                {
                    CheckBox chk = (CheckBox)rows.FindControl("chkSelect");
                    if (chk.Checked)
                    {

                      int  UserId = Util.ToInt(((Label)rows.FindControl("lbluserId")).Text);
                      drow["UserID"]=UserId;
                      DT_User.Rows.Add(drow);
                      string[] MasterDevice=new string[3];
                      MasterDevice[0] = lblIp.Text;
                      MasterDevice[1] = lblSecurityId.Text;
                      MasterDevice[2] = lbluname.Text;
                      MasterDevice[3] = lblpwd.Text;

                      string[] DestinationDevice = new string[3];
                      DestinationDevice[0] = lblDIp.Text;
                      DestinationDevice[1] = lblDSecurityId.Text;
                      DestinationDevice[2] = lblDuname.Text;
                      DestinationDevice[3] = lblDpwd.Text;
                      objBAL.BulkSynch(DT_User, MasterDevice, DestinationDevice, out ErrMsg);
                      if (ErrMsg == null)
                          lblMsg.Text = hrmlang.GetString("successynch");
                      else
                          lblMsg.Text = ErrMsg;
                      Clear();
                    }
                }
            }
        }
    }
    private void Clear()
    {
        lblIp.Text = "";
        lblSecurityId.Text ="";
        lbluname.Text = "";
        lblpwd.Text ="";
        lblDIp.Text ="";
        lblDSecurityId.Text = "";
        lblDuname.Text = "";
        lblDpwd.Text = "";
    }
    protected void gv_Synch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("userid");

            e.Row.Cells[1].Text = hrmlang.GetString("firstname");
            e.Row.Cells[2].Text = hrmlang.GetString("lastname");

            e.Row.Cells[3].Text = hrmlang.GetString("pin");
            e.Row.Cells[4].Text = hrmlang.GetString("iriseye");
            e.Row.Cells[5].Text = hrmlang.GetString("startdate");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            chkSelect.Visible = false;

            if (dtDestination.Rows.Count > 0)
            {
                DataRow[] dRow = dtDestination.Select("UserID=" + ((Label)e.Row.FindControl("lbluserId")).Text);
                if (dRow.Length > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
                    chkSelect.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gv_Synch.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gv_Synch.PagerSettings.NextPageText     = hrmlang.GetString("next");
            gv_Synch.PagerSettings.FirstPageText    = hrmlang.GetString("first");
            gv_Synch.PagerSettings.LastPageText     = hrmlang.GetString("last");
        }

    }
    protected void gv_Synch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Synch.PageIndex = e.NewPageIndex;
    }
}
                
            