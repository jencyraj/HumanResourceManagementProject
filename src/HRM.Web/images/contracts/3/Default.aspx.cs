using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    ArrayList arr = new ArrayList();
    int count = 0;
    
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        Session.Add("arrcon", arr);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        PlaceHolder plc = (PlaceHolder)e.Row.Cells[1].FindControl("PlaceHolder1");
        Label lbl = (Label)e.Row.Cells[0].FindControl("Label1");

        Label lblinsert = new Label();
        CheckBox chk = new CheckBox();
        TextBox txt = new TextBox();
        chk.Text = "tttttttt";
        lblinsert.Text = "xxxxxxxxx";
        
        if (lbl != null)
        {
            switch(lbl.Text)
            {
                case "1":plc.Controls.Add(lblinsert);
                        arr.Add(lblinsert);break;
                case "2":plc.Controls.Add(chk);
                        arr.Add(chk);break;
                case "3": plc.Controls.Add(txt);
                        arr.Add(txt); break;
                default: lblinsert.Text = " Dummy ";
                        plc.Controls.Add(lblinsert);
                        arr.Add(lblinsert); break;
                
            }
            
            
        }

    }
    
    protected void PlaceHolder1_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            arr = (ArrayList)Session["arrcon"];
            if (arr != null)
            {

                if (arr[count] is TextBox)
                {
                    TextBox txt = (TextBox)arr[count];
                    txt.Text = Request.Form[txt.UniqueID].ToString();
                    ((PlaceHolder)this.GridView1.Rows[count].Cells[1].FindControl("PlaceHolder1")).Controls.Add(txt);
                }
                else if (arr[count] is Label)
                {                
                    ((PlaceHolder)this.GridView1.Rows[count].Cells[1].FindControl("PlaceHolder1")).Controls.Add((Control)arr[count]);

                }
                else if (arr[count] is CheckBox)
                {
                    CheckBox chk = (CheckBox)arr[count];
                    if (Request.Form[chk.UniqueID] != null)
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                    ((PlaceHolder)this.GridView1.Rows[count].Cells[1].FindControl("PlaceHolder1")).Controls.Add(chk);
                }
               count++;
            }
        }
        
    }
    
}
