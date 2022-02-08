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
        SqlConnectionControl Scc = new SqlConnectionControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Session.Clear();
                if (Request.Cookies["username"] != null && Request.Cookies["password"]!=null)
                {
                    username.Text = Request.Cookies["username"].Value;
                    password.Attributes["value"] = Request.Cookies["password"].Value;
                    
                    rememberme.Checked = true;
                }
            }
            
        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            
            try
            {   
                string query = "Select activation from Users Where username='" + username.Text + "' And password=" + password.Text;
                int loginresult = Scc.loginUserType(query);
                if (loginresult==1)
                {                                                    
                        if (rememberme.Checked)
                        {
                            Response.Cookies["username"].Value = username.Text;                            
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["password"].Value = password.Text;
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                        Session["username"] = username.Text;                           
                            Response.Redirect("Tables.aspx",false);
                        }
                        else {
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
                        Session["username"] = username.Text;                           
                            Response.Redirect("Tables.aspx",false);                          
                        }                   
                                              
                }
                if(loginresult==0)
                {
                    infologin.Text = "Lütfen mail adresinize gelen doğrulama linkine tıklayarak üyeliğinizi aktif ediniz";
                    infologin.ForeColor = System.Drawing.Color.Red;
                }
                
                else if(loginresult==-1)
                {                   
                    infologin.Text = "Kullanıcı adı veya şifre hatalı!";
                    infologin.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);                          
            }          
        }
    }
}