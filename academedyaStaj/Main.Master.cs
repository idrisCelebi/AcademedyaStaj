using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Main : System.Web.UI.MasterPage
    {
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=AcademedyaStajMain;integrated security=True");
        protected void Page_Load(object sender, EventArgs e)
        {   if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                bringidenty();
            }
        }
        public void bringidenty()
        {
            conn.Open();
            SqlCommand bring = new SqlCommand("Select firstname+' '+lastname from Users where username=@username", conn);
            bring.Parameters.AddWithValue("@username", Session["username"]);
            SqlDataReader dataRead = bring.ExecuteReader();
            
            if(dataRead.Read())
            {
                showwho.Text = dataRead.GetValue(0).ToString();
            }
            conn.Close();
        }
    }
}