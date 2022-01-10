using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Activation : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=AcademedyaStajMain;integrated security=True");
        
        public string Databaseusername = "";
        protected void Page_Load(object sender, EventArgs e)
        {   if (Request.QueryString["activationcode"] != null)
            {   
               
                int idtoactive = int.Parse(Request.QueryString["activationcode"]);
                if (!isactive(idtoactive))
                {
                    activationinfo.Text = "Profiliniz zaten aktif durumdadır. Buradan giriş yapın.";
                }
                else
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("update Users Set activation=1 where id=@id", conn);
                    command.Parameters.AddWithValue("@id", idtoactive);
                    SqlDataReader dataread = command.ExecuteReader();
                 
                        conn.Close();
                    createDatabase(Databaseusername);
                        activationinfo.Text = "Aktivasyon işlemi başarıyla yapılmıştır.Buradan giriş yapabilirsiniz.";
                    activationinfo.ForeColor = System.Drawing.Color.Green;
                }
            }
        else
            {

            }
        }
  
        public bool isactive(int userid)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select username from Users where id=@id and activation=0", conn);
            command.Parameters.AddWithValue("@id", userid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Databaseusername = reader.GetValue(0).ToString();
                conn.Close();
                return true;
            }
            else
            {
               
                conn.Close();   
                return false;
            }

        }
        public void createDatabase(String getusername)
        {
            conn.Open();
            String commandtext = "Create Database " + getusername;
            SqlCommand command = new SqlCommand(commandtext, conn);
           
            int ithappen = command.ExecuteNonQuery();
            conn.Close();
        }
    }
}