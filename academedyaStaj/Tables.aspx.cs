using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Tables : System.Web.UI.Page
    {
        SqlConnectionControl Scc;



        protected void Page_Load(object sender, EventArgs e)
        {
            Scc = new SqlConnectionControl(Session["username"].ToString());

            if (!IsPostBack)
            {
                filllist();
            }
        }
        public void filllist()
        {


            try
            {
               
                DataSet ds = new DataSet();
                String getString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                Scc.getdataAdapter(getString).Fill(ds);
                tables.DataTextField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                tables.DataValueField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                tables.DataSource = ds.Tables[0];
                tables.DataBind();

                Scc.closeDB(Scc._connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);


            }
         
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("createTable.aspx");
        }
        
       
    }
}