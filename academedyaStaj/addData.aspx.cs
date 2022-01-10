using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class addData : System.Web.UI.Page
    {
        SqlConnectionControl Scc = new SqlConnectionControl();
        SqlConnection conn;
        Label ll;
        TextBox tb;

        List<string> columnlist = new List<string>();
        List<string> typelist = new List<string>();

      

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = Scc.getdatabasename();

            if (!IsPostBack)
            {
                allviews.ActiveViewIndex = 0;
                
            }
        }
        public void filllist()
        {
            conn.Open();
            SqlCommand bring = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", conn);

            DropDownList1.DataSource = bring.ExecuteReader();
            DropDownList1.DataTextField = "TABLE_NAME";
            DropDownList1.DataValueField = "TABLE_NAME";
            DropDownList1.DataBind();
            conn.Close();


        }

        protected void forwardbutton_Click(object sender, EventArgs e)
        {
            allviews.ActiveViewIndex = 1;
        }

        protected void View1_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { filllist(); }
            
        }

        protected void View2_Load(object sender, EventArgs e)
        {
            
            infotablename.Text = DropDownList1.SelectedValue;
            getcolumns();

        }
        
        public void getcolumns()
        {
            columnlist.Clear();
            typelist.Clear();
            
            conn.Open();
            String comm = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='"+ infotablename.Text+ "';";
            SqlCommand bring = new SqlCommand(comm, conn);
            SqlDataReader rdr = bring.ExecuteReader();
            while (rdr.Read())
            {
                columnlist.Add(rdr["COLUMN_NAME"].ToString());
                typelist.Add(rdr["DATA_TYPE"].ToString());
            }
            conn.Close();
            columnlist.RemoveAt(0);
            typelist.RemoveAt(0);
            for(int i=0;i<columnlist.Count;i++)
            {
                ll = new Label();
                ll.ID = "label" + i.ToString();
                ll.Text = columnlist[i];
                

                tb = new TextBox();
                tb.ID="textbox"+ i.ToString();
                tb.TextMode = (TextBoxMode)getTextmode(typelist[i]);
                tb.CssClass = "form-control-sm";

                PlaceHolder1.Controls.Add(ll);
                PlaceHolder1.Controls.Add(new HtmlGenericControl("  "));
                PlaceHolder1.Controls.Add(tb);
                PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));

            }

        }

        public int getTextmode(String typeColumn)
        {
            switch (typeColumn)
            {
                case "int":
                    return 9;
                case "datetime":
                    return 4;
                case "nvarchar":
                    return 0;
                case "uniqueidentifier":
                    return 0;
                default:
                    return 0;
            }


        }

        protected void addButton_Click(object sender, EventArgs e)
        {

            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            try
            {
                conn.Open();
                columnTextBox ip = new columnTextBox();
                ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
                for (int i = 0; i < columnlist.Count; i++)
                {

                    values.AppendFormat("'{0}'", ip.alltextbox[i].Text);

                    columns.AppendFormat("{0}", columnlist[i]);
                    if (i == columnlist.Count - 1)
                    {
                        columns.Append(")");
                        values.Append(")");

                    }
                    else
                    {
                        columns.Append(",");
                        values.Append(",");
                    }


                }
                String command = "insert into " + infotablename.Text + " (" + columns.ToString() + " values(" + values.ToString();
                SqlCommand addData = new SqlCommand(command, conn);
                int ithappen = addData.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Tables.aspx");

            }
            catch
            {
                Response.Write("Beklenmedik bir sorun oluştu");
            }
        }
    }

    public class columnTextBox
    {
        

        public List<TextBox> alltextbox { get; set; }
    }
}