using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class viewTable : System.Web.UI.Page
    {
        SqlConnectionControl Scc;
        GridView gr;
        DataTable dt;
        DataTable upgrade_dt;
        RegularExpressionValidator rev;
        RequiredFieldValidator rfv;
        TextBox tb;


        List<string> columnlist = new List<string>();

        public static int updateindex;
        public static int updaterow;

        protected void Page_Load(object sender, EventArgs e)
        {

            Scc = new SqlConnectionControl(Session["username"].ToString());
            if (!IsPostBack)
            {
                filllist();
            }
            if (allviews.ActiveViewIndex == 1)
            {
                getTable();
            }
            if (allviews.ActiveViewIndex==2)
            {
                getUpdateview();
            }
            var qs = Request.QueryString["view"];
            if (qs != null)
            {
                if (qs == "1")
                {
                    getTable();
                }
            }


        }
        protected void filllist()
        {
            allviews.ActiveViewIndex = 0;
            try
            {
                DataSet ds = new DataSet();
                String getString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                Scc.getdataAdapter(getString).Fill(ds);
                DropDownList1.DataTextField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                DropDownList1.DataValueField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataBind();
                if (Session["lastSelected"] != null)
                {
                    DropDownList1.SelectedValue = Session["lastSelected"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        protected void forwardbutton_Click(object sender, EventArgs e)
        {
            
            Session["lastSelected"] = DropDownList1.SelectedValue;
            getTable();

        } 
      
        void DeleteRow(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(gr.Rows[e.RowIndex].Cells[1].Text);
                String dcommand = "delete from " + infotablename.Text + " where id=" + index;
                Scc.basic(dcommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                getTable();
            }
        }
        protected void GridView1_RowUpdating(object sender, GridViewEditEventArgs e)
        {
            updateindex = Convert.ToInt32(gr.Rows[e.NewEditIndex].Cells[1].Text);
            Session["row"] = updateindex;
            updaterow = Convert.ToInt32(e.NewEditIndex);
            getUpdateview();

        }
        public void getUpdateview()
        {
            allviews.ActiveViewIndex = 2;
            if(updateindex==0)
            {
                updateindex = Convert.ToInt32(Session["row"]);
            }
            if (updateindex != 0)
            {
                columnlist.Clear();
                PlaceHolder2.Controls.Clear();
                String getrow = "Select * from " + infotablename.Text + " Where id=" + updateindex;
                upgrade_dt = new DataTable();
                upgrade_dt.Clear();
                Scc.getdataAdapter(getrow).Fill(upgrade_dt);
                for (int j = 1; j < upgrade_dt.Columns.Count; j++)
                {
                    tb = new TextBox();
                    tb.ID = "textboxcolumn" + j.ToString();
                    tb.CssClass = "form-control-sm";
                    tb.Text = upgrade_dt.Columns[j].ColumnName;
                    columnlist.Add(tb.Text);
                    tb.ReadOnly = true;
                    tb.ForeColor = System.Drawing.Color.Red;
                    PlaceHolder2.Controls.Add(tb);
                }
                HtmlGenericControl InsusControl = new HtmlGenericControl("br");
                PlaceHolder2.Controls.Add(InsusControl);
                for (int i = 1; i < upgrade_dt.Columns.Count; i++)
                {
                    String getValits = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + infotablename.Text + "' AND COLUMN_NAME='" + columnlist[i - 1] + "';";
                    tb = new TextBox();
                    tb.ID = "textboxvalue" + i.ToString();
                    tb.ReadOnly = false;
                    tb.TextMode = (TextBoxMode)getTextmode(Scc.getStringWC(getValits, "DATA_TYPE"));
                    tb.CssClass = "form-control-sm";
                    tb.Text = upgrade_dt?.Rows[0]?.ItemArray[i].ToString();
                    PlaceHolder2.Controls.Add(tb);
                    if (Scc.getStringWC(getValits, "DATA_TYPE") == "decimal")
                    {
                        rev = new RegularExpressionValidator();
                        rev.ID = "Regularvalidate" + i.ToString();
                        rev.ControlToValidate = "textboxvalue" + i.ToString();
                        rev.ErrorMessage = columnlist[i - 1] + " sütununa tam sayı veya ondalıklı sayı girebilirsin sadece";
                        rev.ValidationExpression = @"^[1-9]\d*(\.\d+)?$$";
                        rev.ForeColor = System.Drawing.Color.Red;
                        rev.Display = (ValidatorDisplay)2;
                        PlaceHolder2.Controls.Add(rev);
                    }
                    if (Scc.getStringWC(getValits, "IS_NULLABLE") == "NO")
                    {
                        rfv = new RequiredFieldValidator();
                        rfv.ID = "Requiredvalidate" + i.ToString();
                        rfv.ControlToValidate = "textboxvalue" + i.ToString();
                        rfv.ErrorMessage = columnlist[i - 1] + " sütunu boş kalamaz";
                        rfv.ForeColor = System.Drawing.Color.Red;
                        rfv.Display = (ValidatorDisplay)2;
                        PlaceHolder2.Controls.Add(rfv);
                    }
                }
                updateindex = 0;
            }
            
        }
        public int getTextmode(String typeColumn)
        {
            switch (typeColumn)
            {
                case "int":
                    return 9;
                case "date":
                    return 4;
                case "nvarchar":
                    return 0;
                case "decimal":
                    return 0;
                default:
                    return 0;
            }
        }
        public void getTable()
        {
            allviews.ActiveViewIndex = 1;
            infotablename.Text = DropDownList1.SelectedValue;
            try
            {
                PlaceHolder1.Controls.Clear();
                dt = new DataTable();
                gr = new GridView();
                String comm2 = "select * from " + infotablename.Text + "";
                Scc.getdataAdapter(comm2).Fill(dt);
                gr.UseAccessibleHeader = true;
                gr.AutoGenerateDeleteButton = true;
                gr.AutoGenerateEditButton = true;
                gr.RowCommand += new GridViewCommandEventHandler(gridView_RowCommand);
                gr.RowDeleting += new GridViewDeleteEventHandler(DeleteRow);
                gr.RowEditing += new GridViewEditEventHandler(GridView1_RowUpdating);
                

                gr.CssClass = "table table-bordered table-light table-hover";
                gr.DataSource = dt;
                gr.DataBind();
                PlaceHolder1.Controls.Add(gr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Edit")
            {
                
            }
        }
        protected void updateButton_Click(object sender, EventArgs e)
        {


            try
            {
                getupdatetextbox up = new getupdatetextbox();
                up.alltextboxwrite = PlaceHolder2.Controls.OfType<TextBox>().Where(x => x.ReadOnly == false).ToList();
                up.alltextboxfalse = PlaceHolder2.Controls.OfType<TextBox>().Where(x => x.ReadOnly == true).ToList();
                if (up.alltextboxwrite != null && up.alltextboxwrite.Count != 0)
                {
                    StringBuilder updateRowbuilder = new StringBuilder();
                    for (int i = 0; i < up.alltextboxwrite.Count; i++)
                    {
                        if (i == up.alltextboxwrite.Count - 1)
                        {
                            updateRowbuilder.AppendFormat("{0} = '{1}'", up.alltextboxfalse[i].Text, up.alltextboxwrite[i].Text);
                        }
                        else
                        {
                            updateRowbuilder.AppendFormat("{0} = '{1}',", up.alltextboxfalse[i].Text, up.alltextboxwrite[i].Text);
                        }
                    }
                    String updateRow = "Update " + infotablename.Text + " Set " + updateRowbuilder.ToString() + " Where id=" + Session["row"];
                    Scc.Begin_Transaction();
                    Scc.basic(updateRow);
                    Scc.Commit_Transaction();
                    getTable();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Scc.Rollback_Transaction();
            }

        }           
          
     
    }
    public class getupdatetextbox
    {
        public List<TextBox> alltextboxwrite { get; set; }
        public List<TextBox> alltextboxfalse { get; set; }
    }
}