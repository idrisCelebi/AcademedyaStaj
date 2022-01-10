using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class login : System.Web.UI.Page
    {
        
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=AcademedyaStajMain;integrated security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Cookies["username"] != null)
                {
                    username.Text = Request.Cookies["username"].Value;
                }
            }
            
        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();

                string query = "Select activation from Users Where username=@username And password=@password";
                SqlCommand sqlCmd = new SqlCommand(cmdText: query, conn);

                sqlCmd.Parameters.AddWithValue("@username",username.Text);
                sqlCmd.Parameters.AddWithValue("@password",password.Text);
                
                SqlDataReader reader = sqlCmd.ExecuteReader();
                
                if (reader.Read())
                {
                    
                    if (int.Parse(reader.GetValue(0).ToString())==1)//aktivasyonu yapılmış mı kontrolü
                    {
                        
                        conn.Close();
                        if(rememberme.Checked)
                        {
                            Response.Cookies["username"].Value = username.Text;
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                            Session["username"] = username.Text;
                            

                            Response.Redirect("Tables.aspx");
                        }
                        else {
                            
                            Session["username"] = username.Text;
                           
                            Response.Redirect("Tables.aspx");
                           
                        }
                    

                    }
                    else
                    {
                        conn.Close();
                        infologin.Text = "Lütfen mail adresinize gelen doğrulama linkine tıklayarak üyeliğinizi aktif ediniz";
                        infologin.ForeColor = System.Drawing.Color.Red;
                    }
                    
                }
                else
                {
                    conn.Close();
                    infologin.Text = "Kullanıcı adı veya şifre hatalı!";
                    infologin.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch
            {

            }
        }
    }
}