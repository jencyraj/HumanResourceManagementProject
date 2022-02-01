using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRM.BOL;
using HRM.BAL;
using System.Drawing;
public partial class LeaveRuleSettings : System.Web.UI.Page
{
    public int Mid = 0; static string CID = ""; static int flag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblErr.Text = "";


        txtDays.Attributes.Add("placeholder", hrmlang.GetString("enterminleav"));
        btnSave.Text = hrmlang.GetString("save");
        btnCancel.Text = hrmlang.GetString("cancel");
        lblLeaveTypeID.Text = "" + Request.QueryString["id"];

        if (!IsPostBack)
        {

            ViewState["permissions"] = Util.CheckPermission((DataSet)Session["PERMISSIONS"], "leaverulesettings.aspx");
            BindLeavtyp();
            FillBlankRow();
            GetMonthList();
            BindLeavtyp();
            if (lblLeaveTypeID.Text != "")
            {
                Mid = int.Parse(lblLeaveTypeID.Text);
                getleaveruledetail();
            }
            if (gvAdd.Rows.Count == 0)
            {
                FillBlankRow();
                check.Checked = true;
            }
        }
        string[] permissions = (string[])ViewState["permissions"];
        Pnlnew.Visible = (permissions[0] == "Y") ? true : false;
        pnllod.Visible = (permissions[0] == "Y") ? true : false;


    }


    private DataTable FillBlankRow()
    {
        DataTable dt = GetTable();

        dt.Rows.Add(-1, "", "", "", "", "", "", "", "", "", "", "", "");

        gvAdd.DataSource = dt;
        gvAdd.DataBind();

        gvAdd.Rows[0].FindControl("lnkEdit").Visible = false;
        gvAdd.Rows[0].FindControl("lnkDelete").Visible = false;

        return dt;
    }

    private void GetMonthList()
    {
        DataTable dt = new MonthsBAL().Select(Session["LanguageId"].ToString());
        ddlMonth.DataSource = dt;
        ddlMonth.DataBind();
        //ddlMonth.Items.Insert(0, new ListItem(hrmlang.GetString("select"), ""));
    }
    private void GetLeaveSelect()
    {
        LeaveRuleBAL objBAL = new LeaveRuleBAL();
        gvAdd.DataSource = objBAL.Select();
        gvAdd.DataBind();

    }


    private void GetLeaveRule()
    {
        LeaveRuleBAL objBAL = new LeaveRuleBAL();
        gvAdd.DataSource = objBAL.SelectDetail(Mid);
        gvAdd.DataBind();

    }
    private void getleaveruledetail()
    {

        LoadControl(Mid);
        gvAdd.Rows[0].FindControl("lnkDelete").Visible = true;
    }
    private void LoadControl(int sKey)
    {
        LeaveRuleBOL objBOL = new LeaveRuleBOL();
        objBOL.LRID = sKey;
        DataTable dTable = new LeaveRuleBAL().SelectDetail(sKey);

        DataTable dt = GetTable();
        if (dTable.Rows.Count > 0)
        {


            ddlMonth.Text = dTable.Rows[0]["StartMonth"].ToString();
            txtDays.Text = dTable.Rows[0]["MinimumLeaves"].ToString();
            txtdescr.Text = dTable.Rows[0]["Description"].ToString();
            txtYear.Text = dTable.Rows[0]["LRYear"].ToString();
            string chk = dTable.Rows[0]["DActive"].ToString();
            if (chk == "Inactive")
            {
                check.Checked = false;
            }
            else
            {
                check.Checked = true;
            }


            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DataRow drow;

                drow = dt.NewRow();
                string LT = dTable.Rows[i]["LateByType"].ToString();
                if (LT == "PM")
                {
                    drow["LateByTypeDesc"] = "PerMonth";
                }
                else
                {
                    drow["LateByTypeDesc"] = "PerDay";
                }
                drow["LateByType"] = LT;
                string LBT = dTable.Rows[i]["LossType"].ToString();
                drow["LossType"] = LBT;
                if (LBT == "L")
                {
                    drow["LossTypeDesc"] = "Leave";
                }
                else
                {
                    drow["LossTypeDesc"] = "Loss of Pay";
                }
                string LOT = dTable.Rows[i]["LossTime"].ToString();
                drow["LossTime"] = LOT;
                if (LOT == "H")
                {
                    drow["LossTimeDesc"] = "Hour";
                }

                else
                {
                    drow["LossTimeDesc"] = "Minute";
                }
                string LTO = dTable.Rows[i]["LateByTime"].ToString();
                drow["LateByTime"] = LTO;
                if (LTO == "M")
                {
                    drow["LateByTimeDesc"] = "Minute";
                }
                else
                {
                    drow["LateByTimeDesc"] = "Hour";
                }
                decimal LOV = Decimal.Parse(dTable.Rows[i]["LateByValue"].ToString());
                drow["LateByValue"] = LOV;

                drow["LossValue"] = "" + dTable.Rows[i]["LossValue"];
                string LTYP = dTable.Rows[i]["LeaveName"].ToString();
                drow["LeaveName"] = LTYP;

                int LD = int.Parse(dTable.Rows[i]["LDID"].ToString());
                drow["LDID"] = LD;
                drow["LTID"] = "" + dTable.Rows[i]["LTID"];
                drow["Status"] = "" + dTable.Rows[i]["Status"];
                dt.Rows.Add(drow);
            }

        }
        gvAdd.DataSource = (dt.Rows.Count == 0) ? FillBlankRow() : dt;
        gvAdd.DataBind();

    }
    public DataTable FillLeaveRuleDetail()
    {
        DataRow drow = null;
        DataTable dt = GetTable();

        if (gvAdd.Rows.Count > 0)
        {
            for (int i = 0; i < gvAdd.Rows.Count; i++)
            {
                if ("" + ((LinkButton)gvAdd.Rows[i].FindControl("lnkEdit")).CommandArgument == "-1") continue;
                drow = dt.NewRow();



                drow["LDID"] = ((LinkButton)gvAdd.Rows[i].FindControl("lnkEdit")).CommandArgument;
                drow["LateByType"] = ((Label)gvAdd.Rows[i].FindControl("lblLatByCd")).Text;
                drow["LossType"] = ((Label)gvAdd.Rows[i].FindControl("lblLossTypeCd")).Text;
                drow["LossTime"] = ((Label)gvAdd.Rows[i].FindControl("lblLossTimeCd")).Text;

                drow["LateByTime"] = ((Label)gvAdd.Rows[i].FindControl("lblLateTimCd")).Text;
                drow["LossTimeDesc"] = ((Label)gvAdd.Rows[i].FindControl("EddllossTime")).Text;
                drow["LateByTimeDesc"] = ((Label)gvAdd.Rows[i].FindControl("EddllatebyTime")).Text;
                drow["LossTypeDesc"] = ((Label)gvAdd.Rows[i].FindControl("Eddllosstype")).Text;
                drow["LateByValue"] = ((Label)gvAdd.Rows[i].FindControl("Etxtlatevalue")).Text;
                drow["LateByTypeDesc"] = ((Label)gvAdd.Rows[i].FindControl("Eddllateby")).Text;
                drow["LossValue"] = ((Label)gvAdd.Rows[i].FindControl("Etxtlossvalue")).Text;
                drow["LTID"] = ((Label)gvAdd.Rows[i].FindControl("Etxtleavtype")).Text;
                drow["LeaveName"] = ((Label)gvAdd.Rows[i].FindControl("txtleavname")).Text;
                drow["Status"] = "Y";

                dt.Rows.Add(drow);

            }
        }

        return dt;
    }

    private DataTable GetTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LDID");
        dt.Columns.Add("LateByType");
        dt.Columns.Add("LateByTypeDesc");

        dt.Columns.Add("LateByValue");

        dt.Columns.Add("LossType");
        dt.Columns.Add("LossTypeDesc");

        dt.Columns.Add("LossValue");

        dt.Columns.Add("LossTime");
        dt.Columns.Add("LossTimeDesc");

        dt.Columns.Add("LateByTime");
        dt.Columns.Add("LateByTimeDesc");
        dt.Columns.Add("LTID");
        dt.Columns.Add("LeaveName");
        dt.Columns.Add("Status");

        return dt;
    }

    public void removrowgrid()
    {
        DataRow drow = null;
        DataTable dt = GetTable();

        if (gvAdd.Rows.Count > 0)
        {
            for (int i = 0; i < gvAdd.Rows.Count; i++)
            {
                drow = dt.NewRow();




                drow["LDID"] = ((LinkButton)gvAdd.Rows[i].FindControl("lnkEdit")).CommandArgument;
                drow["LateByType"] = ((Label)gvAdd.Rows[i].FindControl("lblLatByCd")).Text;
                drow["LossType"] = ((Label)gvAdd.Rows[i].FindControl("lblLossTypeCd")).Text;
                drow["LossTime"] = ((Label)gvAdd.Rows[i].FindControl("lblLossTimeCd")).Text;
                drow["LateByTime"] = ((Label)gvAdd.Rows[i].FindControl("lblLateTimCd")).Text;
                drow["LossTimeDesc"] = ((Label)gvAdd.Rows[i].FindControl("EddllossTime")).Text;
                drow["LateByTimeDesc"] = ((Label)gvAdd.Rows[i].FindControl("EddllatebyTime")).Text;
                drow["LossTypeDesc"] = ((Label)gvAdd.Rows[i].FindControl("Eddllosstype")).Text;
                drow["LateByValue"] = ((Label)gvAdd.Rows[i].FindControl("Etxtlatevalue")).Text;
                drow["LateByTypeDesc"] = ((Label)gvAdd.Rows[i].FindControl("Eddllateby")).Text;
                drow["LossValue"] = ((Label)gvAdd.Rows[i].FindControl("Etxtlossvalue")).Text;
                drow["Status"] = "Y";
                dt.Rows.Add(drow);
            }
        }
        if (lblindex.Text != "")
        {
            dt.Rows.RemoveAt(int.Parse(lblindex.Text));
            gvAdd.DataSource = dt;
            gvAdd.DataBind();
        }

        if (gvAdd.Rows.Count == 0)
            FillBlankRow();

    }
    private void BindLeavtyp()
    {
        DataTable dt = new LeaveRuleBAL().bindleavtype();
        ViewState["LVT"] = dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LeaveRuleBAL objBAL = new LeaveRuleBAL();
            LeaveRuleBOL objBol = new LeaveRuleBOL();

            objBol.LRID = Util.ToInt(lblLeaveTypeID.Text);

            objBol.Month = Util.ToInt(ddlMonth.SelectedValue);
            objBol.Description = txtdescr.Text;
            objBol.CreatedBy = User.Identity.Name;
            objBol.minleav = decimal.Parse(txtDays.Text);
            objBol.Year = Util.ToInt(txtYear.Text);

            if (check.Checked)
            {
                objBol.Active = 1;
            }
            else
            {
                objBol.Active = 0;
            }
            objBol.DTLeaveRule = FillLeaveRuleDetail();
            if (check.Checked)
            {
                DataTable ckDT = objBAL.CheckyearA();
                if (ckDT.Rows.Count > 0)
                {
                    int year = int.Parse(ckDT.Rows[0]["Lyear"].ToString());
                    if (year == objBol.Year && "" + ckDT.Rows[0]["LRID"] != objBol.LRID.ToString())
                    {
                        check.Checked = false;

                        lblMsg.Text = hrmlang.GetString("alrdyactive");
                        return;
                    }
                }
            }
            if (objBol.LRID == 0)
            {
                objBAL.Save(objBol);
                Clear();
            }

            if (objBol.LRID != 0)
                objBAL.Update(objBol);

            lblMsg.Text = hrmlang.GetString("leaverulesaved");


        }

        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
        }

    }
    private void Clear()
    {
        lblLeaveTypeID.Text = "";
        txtDays.Text = "";
        txtdescr.Text = "";
        lblindex.Text = "";
        FillBlankRow();
        lblindex.Text = "";
        txtYear.Text = "";
        string[] permissions = (string[])ViewState["permissions"];
        pnllod.Visible = (permissions[0] == "Y") ? true : false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect("LeaveRuleSettings.aspx");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Clear();


    }


    protected void gvAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ADDKEY")
        {
            Button button = (Button)gvAdd.FooterRow.FindControl("btnAdd");
            if (button.Text == "Add")
            {
                DataTable leaveruleDT = FillLeaveRuleDetail();

                DropDownList ddllateby = (DropDownList)gvAdd.FooterRow.FindControl("ddllateby");
                DropDownList ddllosstype = (DropDownList)gvAdd.FooterRow.FindControl("ddllosstype");
                DropDownList ddllossTime = (DropDownList)gvAdd.FooterRow.FindControl("ddllossTime");
                DropDownList ddllatebyTime = (DropDownList)gvAdd.FooterRow.FindControl("ddllatebyTime");
                DropDownList ddlleavtype = (DropDownList)gvAdd.FooterRow.FindControl("ddlleavtype");
                GridView testGrid = (GridView)sender;
                TextBox txtlossvalue = (TextBox)testGrid.FooterRow.FindControl("txtlossvalue");
                TextBox txtlatevalue = (TextBox)testGrid.FooterRow.FindControl("txtlatevalue");

                DataRow dr = leaveruleDT.NewRow();
                dr[0] = "";
                dr[1] = ddllateby.SelectedValue;
                dr[2] = ddllateby.SelectedItem.Text;
                dr[3] = txtlatevalue.Text;
                dr[4] = ddllosstype.SelectedValue;
                dr[5] = ddllosstype.SelectedItem.Text;
                dr[6] = txtlossvalue.Text;
                dr[7] = ddllossTime.SelectedValue;
                dr[8] = ddllossTime.SelectedItem.Text;
                dr[9] = ddllatebyTime.SelectedValue;
                dr[10] = ddllatebyTime.SelectedItem.Text;
                dr[11] = ddlleavtype.SelectedValue;
                dr[12] = ddlleavtype.SelectedItem.Text;
                dr[13] = 'Y';
                leaveruleDT.Rows.Add(dr);


                gvAdd.DataSource = leaveruleDT;
                gvAdd.DataBind();
                HighlightDuplicate(gvAdd);
                gvAdd.Rows[0].FindControl("lnkEdit").Visible = true;
                gvAdd.Rows[0].FindControl("lnkDelete").Visible = true;
                if (flag != 0)
                {
                    button.Text = "Add";

                }
            }
            else
            {
                flag = 0;
                GridViewRow gRow = gvAdd.Rows[Util.ToInt(lblindex.Text)];

                ((Label)gRow.FindControl("Etxtlatevalue")).Text = ((TextBox)gvAdd.FooterRow.FindControl("txtlatevalue")).Text;

                ((Label)gRow.FindControl("Etxtlossvalue")).Text = ((TextBox)gvAdd.FooterRow.FindControl("txtlossvalue")).Text;
                ((Label)gRow.FindControl("Eddllateby")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllateby")).SelectedItem.Text;
                ((Label)gRow.FindControl("lblLatByCd")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllateby")).SelectedItem.Value;
                ((Label)gRow.FindControl("EddllatebyTime")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllatebyTime")).SelectedItem.Text;
                ((Label)gRow.FindControl("lblLateTimCd")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllatebyTime")).SelectedItem.Value;

                ((Label)gRow.FindControl("Eddllosstype")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllosstype")).SelectedItem.Text;
                ((Label)gRow.FindControl("lblLossTypeCd")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllosstype")).SelectedItem.Value;
                ((Label)gRow.FindControl("EddllossTime")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllossTime")).SelectedItem.Text;
                ((Label)gRow.FindControl("lblLossTimeCd")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddllossTime")).SelectedItem.Value;
                ((Label)gRow.FindControl("txtleavname")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddlleavtype")).SelectedItem.Text;
                ((Label)gRow.FindControl("Etxtleavtype")).Text = ((DropDownList)gvAdd.FooterRow.FindControl("ddlleavtype")).SelectedItem.Value;

                lblindex.Text = "";
                button.Text = "Add";
            }
        }

        if (e.CommandName.Equals("DEL"))
        {
            CID = e.CommandArgument.ToString();
            GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

            int RowIndex = clickedRow.RowIndex;
            lblindex.Text = Convert.ToString(RowIndex);
            if (CID != "")
            {
                LeaveRuleBAL objBAL = new LeaveRuleBAL();
                objBAL.DeleteRow(Util.ToInt(e.CommandArgument));
                //GetLeaveRule();
                removrowgrid();
                lblMsg.Text = hrmlang.GetString("leaveruledlt");

            }
            else
            {

                removrowgrid();


            }
        }

        if (e.CommandName.Equals("EDITLG"))
        {
            flag = 1;
            Button button = (Button)gvAdd.FooterRow.FindControl("btnAdd");

            if (button.Text == "Add")
            {
                lblindex.Text = "";

                GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                lblindex.Text = gRow.RowIndex.ToString();
                //  removrowgrid();
                ((TextBox)gvAdd.FooterRow.FindControl("txtlatevalue")).Text = ((Label)gRow.FindControl("Etxtlatevalue")).Text;
                ((TextBox)gvAdd.FooterRow.FindControl("txtlossvalue")).Text = ((Label)gRow.FindControl("Etxtlossvalue")).Text;

                ((DropDownList)gvAdd.FooterRow.FindControl("ddllateby")).SelectedValue = ((Label)gRow.FindControl("lblLatByCd")).Text;

                ((DropDownList)gvAdd.FooterRow.FindControl("ddllatebyTime")).SelectedValue = ((Label)gRow.FindControl("lblLateTimCd")).Text;
                ((DropDownList)gvAdd.FooterRow.FindControl("ddllosstype")).SelectedValue = ((Label)gRow.FindControl("lblLossTypeCd")).Text;
                ((DropDownList)gvAdd.FooterRow.FindControl("ddllossTime")).SelectedValue = ((Label)gRow.FindControl("lblLossTimeCd")).Text;
                flag = 0;
                button.Text = "Update";

            }


        }

    }

    public void HighlightDuplicate(GridView gridview)
    {
        for (int currentRow = 0; currentRow < gridview.Rows.Count - 1; currentRow++)
        {
            GridViewRow rowToCompare = gridview.Rows[currentRow];
            for (int otherRow = currentRow + 1; otherRow < gridview.Rows.Count; otherRow++)
            {
                GridViewRow row = gridview.Rows[otherRow];
                bool duplicateRow = true;
                //check Duplicate on column
                if ((rowToCompare.Cells[1].Text) != (row.Cells[1].Text))
                {
                    duplicateRow = false;
                }
                else if (duplicateRow)
                {
                    lblErr.Text = "Duplicate Entry not allowed";
                }
            }
        }
    }
    protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = hrmlang.GetString("latebyvalue");
            e.Row.Cells[1].Text = hrmlang.GetString("latebytype");
            e.Row.Cells[2].Text = hrmlang.GetString("latebytime");
            e.Row.Cells[3].Text = hrmlang.GetString("lossvalue");
            e.Row.Cells[4].Text = hrmlang.GetString("losstype");
            e.Row.Cells[5].Text = hrmlang.GetString("losstime");

        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            gvAdd.PagerSettings.PreviousPageText = hrmlang.GetString("previous");
            gvAdd.PagerSettings.NextPageText = hrmlang.GetString("next");
            gvAdd.PagerSettings.FirstPageText = hrmlang.GetString("first");
            gvAdd.PagerSettings.LastPageText = hrmlang.GetString("last");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlleavtype = (DropDownList)e.Row.FindControl("ddlleavtype");
            ddlleavtype.DataSource = (DataTable)ViewState["LVT"];
            ddlleavtype.DataValueField = "LeaveTypeID";
            ddlleavtype.DataTextField = "LeaveName";
            ddlleavtype.DataBind();
            ddlleavtype.Items.Insert(0, new ListItem("", "0"));
        }
    }

}