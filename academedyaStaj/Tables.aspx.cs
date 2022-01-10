using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Tables : System.Web.UI.Page
    {
        SqlConnectionControl Scc = new SqlConnectionControl();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                filllist(Scc.getdatabasename());
            }
        }
        public void filllist(SqlConnection conn)
        {
            conn.Open();
            SqlCommand bring = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", conn);
           
            tables.DataSource = bring.ExecuteReader();
            tables.DataTextField = "TABLE_NAME";
            tables.DataValueField = "TABLE_NAME";
            tables.DataBind();
            

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("createTable.aspx");
        }

       
    }
}