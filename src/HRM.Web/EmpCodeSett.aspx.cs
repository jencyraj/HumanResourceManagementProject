using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.BOL;
using HRM.BAL;
using System.Data;
using System.Text;
public partial class EmpCodeSett : System.Web.UI.Page
{
    string M = ""; string Y = ""; string D = ""; string De = ""; string sn = ""; string ot = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";
        btnNew.Text = hrmlang.GetString("newrole");
       // txtOrder.Text = hrmlang.GetString("enterorder");
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        if (!IsPostBack)
        {
            GetEduLevel();
        }
        ival.Visible = false;
        ipfx.Visible = false;
        istrt.Visible = false;
        ilen.Visible = false;
        GetProfile();
        UserBAL objBAL = new UserBAL();
     
        lblEx.Text = hrmlang.GetString("example");
        viewempcode();
    }
  
    private void viewempcode()
    {
        EmployeeBOL objEmp = new EmployeeBOL();
        EmployeeBAL objBAL = new EmployeeBAL();

        try
        {

            objEmp.EmployeeID = Util.ToInt(lblID.Text);
            if (objEmp.EmployeeID == 0)
            {
                EmpCodeSettBAL objBAL1 = new EmpCodeSettBAL();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = objBAL1.SelectAll();
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {


                    string temp = Convert.ToString(row["CodeItem"]);
                    switch (temp)
                    {
                        case "Month":

                            M = DateTime.Now.ToString("MMM");
                            break;

                        case "Year":

                            Y = DateTime.Now.Year.ToString();
                            Y = Y.Substring(Y.Length - 2);
                            break;

                       
                        case "SerialNo":

                            EmpCodeSettBAL objBAL5 = new EmpCodeSettBAL();
                            DataTable dt5 = new DataTable();
                            DataSet d1 = new DataSet();
                            d1 = objBAL5.GetNextEmployeeCode("SerialNo", "");
                            dt5 = d1.Tables[0];
                            if (dt5.Rows.Count > 0)
                            {

                                sn = dt5.Rows[0]["EMPCODE"].ToString();
                                sn = sn.Substring(sn.Length - 2);
                            }

                            break;

                        case "OtherText":

                            EmpCodeSettBAL objBAL4 = new EmpCodeSettBAL();
                            DataTable dt3 = new DataTable();
                            DataSet d = new DataSet();
                            d = objBAL4.GetNextEmployeeCode("OtherText", "");
                            dt3 = d.Tables[0];
                            if (dt3.Rows.Count > 0)
                            {

                                ot = dt3.Rows[0]["EMPCODE"].ToString();
                                ot = ot.Substring(ot.Length - 2);
                            }



                            break;

                    }


                }

             string   empcode = new StringBuilder().Append(Y).Append(M).Append(D).Append(De).Append(sn).Append(ot).ToString();
             lblEx.Text = hrmlang.GetString("example") + empcode;

            }


          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetProfile()
    {

        if (Request.Cookies["usersettings"] != null)
        {
            HttpCookie cookie = Request.Cookies["usersettings"];
            Session["USERID"] = cookie.Values["ihrUserName"].ToString();
        }

        EmployeeBAL objBAL = new EmployeeBAL();
        EmployeeBOL objBOL = new EmployeeBOL();
        objBOL.UserID = "" + Session["USERID"];// HttpContext.Current.User.Identity.Name;
        objBOL = objBAL.Select(objBOL);
    }
    private void GetEduLevel()
    {
        EmpCodeSettBAL objBAL = new EmpCodeSettBAL();
        gvEduLevel.DataSource = objBAL.SelectAll();
        gvEduLevel.DataBind();
    }

    protected void gvEduLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("coditem");
            e.Row.Cells[1].Text = hrmlang.GetString("order");
          
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvEduLevel.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvEduLevel.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvEduLevel.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvEduLevel.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkEdit.Attributes.Add("title", hrmlang.GetString("edit"));
            lnkDelete.Attributes.Add("title", hrmlang.GetString("delete"));
            lnkDelete.OnClientClick = string.Format("return confirm('{0}');", hrmlang.GetString("cnfrmcodeitem"));
        }
    }

    protected void gvEduLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EDITBR"))
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            lblID.Text = e.CommandArgument.ToString();
            ddlItems.Text = Util.CleanString(row.Cells[0].Text);
            txtOrder.Text = Util.CleanString(row.Cells[1].Text);
        }

        if (e.CommandName.Equals("DEL"))
        {
            EmpCodeSettBAL objBAL = new EmpCodeSettBAL();
            objBAL.Delete(e.CommandArgument.ToString());
            lblMsg.Text = hrmlang.GetString("empcodedlt");
            GetEduLevel();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        try
        {
            EmpCodeSettBAL objBAL = new EmpCodeSettBAL();
            EmpCodeSettBOL objBol = new EmpCodeSettBOL();
            if (lblID.Text == "")
            {
                objBol.CodeID = 0;
            }
            else
            {
                 objBol.CodeID =int.Parse(lblID.Text);
            }
            objBol.CodeItem = ddlItems.SelectedItem.Text;
            objBol.SettOrder = int.Parse(txtOrder.Text);

            if (ddlItems.SelectedValue == "SerialNo")
            {
                objBol.SerialNoPrefix = txtprefix.Text;
                objBol.EcodeCtrStart = int.Parse(txtstart.Text);
                objBol.EmpCodeTotalLength = int.Parse(txtlength.Text);
            }
            else if(ddlItems.SelectedValue=="OtherText")
            {
                objBol.Value =txtvalue.Text;
            }
            
            
            objBAL.Save(objBol);

            EmployeeBAL objBAL1 = new EmployeeBAL();
            objBAL1.UpdateECodeSettings(txtprefix.Text, Util.ToInt(txtstart.Text), Util.ToInt(txtEmpCodeTotalLength.Text), "" + Session["USERID"]);
            lblMsg.Text = hrmlang.GetString("empcodsave");

            
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
             DS=    objBAL.GetNextEmployeeCode(ddlItems.SelectedItem.Text, "" + Session["USERID"]);
            DT=DS.Tables[0];
            lblEx.Text = hrmlang.GetString("example") + DT.Rows[0]["EMPCODE"].ToString();
            Clear();
            GetDataItem();
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }
    }

    private void Clear()
    {
        lblID.Text = "";
        ddlItems.Text = "";
        txtOrder.Text = "";
    }
   
    protected void gvEduLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEduLevel.PageIndex = e.NewPageIndex;
        GetEduLevel();
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
        if (ddlItems.SelectedItem.Text == "OtherText")
        {

            ival.Visible = true;
           
            
        }
        else if (ddlItems.SelectedItem.Text == "SerialNo")
        {
         
              
               ipfx.Visible = true;
               istrt.Visible = true;
               ilen.Visible = true;
           
        }
        else
        {
            ival.Visible = false;
            ipfx.Visible = false;
            istrt.Visible = false;
            ilen.Visible = false;
            
        }
    }
}